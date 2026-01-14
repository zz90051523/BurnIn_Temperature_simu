using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BurnIn_Temperature_simu
{
    internal static class Program
    {
        /// <summary>
        /// 應用程式的主要進入點。
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            // [AutoUpdater] 設定自動更新
            
            // 重要：確保使用 TLS 1.2
            System.Net.ServicePointManager.SecurityProtocol = System.Net.SecurityProtocolType.Tls12;
            
            // 設定為 GitHub API 模式
            AutoUpdaterDotNET.AutoUpdater.ReportErrors = true;
            AutoUpdaterDotNET.AutoUpdater.HttpUserAgent = "BurnIn_Temperature_simu";
            AutoUpdaterDotNET.AutoUpdater.ParseUpdateInfoEvent += AutoUpdaterOnParseUpdateInfoEvent;

            // [Diagnostic] 手動測試連線，確認 .NET 是否能連上 GitHub
            try
            {
                using (var client = new System.Net.WebClient())
                {
                    // 嘗試自動偵測 Proxy
                    client.Proxy = System.Net.WebRequest.DefaultWebProxy;
                    client.Proxy.Credentials = System.Net.CredentialCache.DefaultCredentials;
                    
                    client.Headers.Add("User-Agent", "BurnIn_Temperature_simu");
                    string response = client.DownloadString("https://api.github.com/repos/zz90051523/BurnIn_Temperature_simu/releases/latest");
                    // System.Windows.Forms.MessageBox.Show("[Diagnostic] 連線成功! 長度: " + response.Length);
                }
            }
            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show($"[Diagnostic] 手動連線測試失敗:\n{ex.Message}\n{ex.InnerException?.Message}");
            }

            AutoUpdaterDotNET.AutoUpdater.Start("https://api.github.com/repos/zz90051523/BurnIn_Temperature_simu/releases/latest");
            
            Application.Run(new BurnIn_Temperature_Simu());
        }

        private static void AutoUpdaterOnParseUpdateInfoEvent(AutoUpdaterDotNET.ParseUpdateInfoEventArgs args)
        {
            try 
            {
                // 用於除錯：顯示接收到的資料
                // System.Windows.Forms.MessageBox.Show("Received Data: " + args.RemoteData);

                // 解析 GitHub API 回傳的 JSON
                dynamic json = new System.Web.Script.Serialization.JavaScriptSerializer().Deserialize<dynamic>(args.RemoteData);
                
                string version = json["tag_name"]; // e.g., "v1.0.4"
                if (version.StartsWith("v")) version = version.Substring(1); // 去掉 'v'
                
                string changelog = json["body"];
                
                // 取得第一個 asset (通常是 zip 檔)
                var assets = json["assets"] as Array;
                string url = "";
                if (assets != null && assets.Length > 0)
                {
                     dynamic firstAsset = assets.GetValue(0);
                     url = firstAsset["browser_download_url"];
                }

                // 用於除錯：顯示解析後的資料
                // System.Windows.Forms.MessageBox.Show($"Parsed - Version: {version}, URL: {url}");

                args.UpdateInfo = new AutoUpdaterDotNET.UpdateInfoEventArgs
                {
                    CurrentVersion = version,
                    ChangelogURL = json["html_url"], // 使用 Release 頁面作為 Changelog
                    DownloadURL = url,
                    Mandatory = new AutoUpdaterDotNET.Mandatory { Value = false }
                };
            }
            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show($"解析更新資料失敗: {ex.Message}\nStack: {ex.StackTrace}");
            }
        }
    }
}
