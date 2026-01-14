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
            // [AutoUpdater] 設定自動更新
            
            // 重要：確保使用 TLS 1.2
            System.Net.ServicePointManager.SecurityProtocol = System.Net.SecurityProtocolType.Tls12;
            
            // 設定為 GitHub API 模式
            AutoUpdaterDotNET.AutoUpdater.ReportErrors = true;
            AutoUpdaterDotNET.AutoUpdater.HttpUserAgent = "BurnIn_Temperature_simu";
            AutoUpdaterDotNET.AutoUpdater.ParseUpdateInfoEvent += AutoUpdaterOnParseUpdateInfoEvent;
            
            // 啟用「稍後提醒」按鈕 (讓使用者可以選擇不立即更新)
            AutoUpdaterDotNET.AutoUpdater.ShowRemindLaterButton = true;
            
            // 強制以管理員身分執行更新 (因為安裝檔通常需要 Admin 權限)
            AutoUpdaterDotNET.AutoUpdater.RunUpdateAsAdmin = true;

            // 設定 AutoUpdater Proxy
            AutoUpdaterDotNET.AutoUpdater.Proxy = System.Net.WebRequest.DefaultWebProxy;
            if (AutoUpdaterDotNET.AutoUpdater.Proxy != null)
            {
                AutoUpdaterDotNET.AutoUpdater.Proxy.Credentials = System.Net.CredentialCache.DefaultCredentials;
            }

            AutoUpdaterDotNET.AutoUpdater.Start("https://api.github.com/repos/zz90051523/BurnIn_Temperature_simu/releases/latest");
            
            Application.Run(new BurnIn_Temperature_Simu());
        }

        private static void AutoUpdaterOnParseUpdateInfoEvent(AutoUpdaterDotNET.ParseUpdateInfoEventArgs args)
        {
            try 
            {
                // Debug: 進入解析事件
                MessageBox.Show("[Debug] 收到 GitHub 回應，開始解析...", "Debug Info");

                // 解析 GitHub API 回傳的 JSON
                if (string.IsNullOrEmpty(args.RemoteData))
                {
                     MessageBox.Show("[Debug] GitHub 回傳資料為空!", "Error");
                     return;
                }

                dynamic json = new System.Web.Script.Serialization.JavaScriptSerializer().Deserialize<dynamic>(args.RemoteData);
                
                string version = json["tag_name"]; // e.g., "v1.0.4"
                if (version.StartsWith("v")) version = version.Substring(1); // 去掉 'v'
                
                string changelog = json["body"];
                
                // 取得第一個 asset (通常是 zip 檔，現在我們要抓 exe)
                var assets = json["assets"] as Array;
                string url = "";
                if (assets != null && assets.Length > 0)
                {
                     // 尋找 .exe 結尾的 asset
                     foreach (dynamic asset in assets)
                     {
                         string name = asset["name"];
                         if (name.EndsWith(".exe", StringComparison.OrdinalIgnoreCase))
                         {
                             url = asset["browser_download_url"];
                             break;
                         }
                     }
                     // 如果找不到 exe，回退到第一個 asset (可能是 zip)
                     if (string.IsNullOrEmpty(url))
                     {
                         dynamic firstAsset = assets.GetValue(0);
                         url = firstAsset["browser_download_url"];
                     }
                }

                args.UpdateInfo = new AutoUpdaterDotNET.UpdateInfoEventArgs
                {
                    CurrentVersion = version,
                    ChangelogURL = json["html_url"], // 使用 Release 頁面作為 Changelog
                    DownloadURL = url,
                    Mandatory = new AutoUpdaterDotNET.Mandatory { Value = false }
                };
                
                // Debug: 顯示解析結果
                 //MessageBox.Show($"解析成功!\n版本: {version}\nURL: {url}", "Debug Info");
                // TODO: 測試完成後請註解掉下面這行
                MessageBox.Show($"[Debug] 解析成功!\nGitHub 版本: {version}\n下載網址: {url}", "Debug Info");
            }
            catch (Exception ex)
            {
                MessageBox.Show("更新檢查解析錯誤: " + ex.Message + "\n" + ex.StackTrace);
            }
        }
    }
}
