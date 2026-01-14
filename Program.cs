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
            AutoUpdaterDotNET.AutoUpdater.ReportErrors = false; // 關閉預設錯誤視窗，改由 Event 自行處理 (如果有需要)
            AutoUpdaterDotNET.AutoUpdater.HttpUserAgent = "BurnIn_Temperature_simu";
            AutoUpdaterDotNET.AutoUpdater.ParseUpdateInfoEvent += AutoUpdaterOnParseUpdateInfoEvent;
            
            // 強制以管理員身分執行更新
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

        private static void AutoUpdaterOnCheckForUpdateEvent(AutoUpdaterDotNET.UpdateInfoEventArgs args)
        {
            if (args.Error != null)
            {
                // 如果是網路錯誤，可以選擇忽略或記錄 log
                return;
            }

            if (args.IsUpdateAvailable)
            {
                UpdateForm updateForm = new UpdateForm(args);
                if (updateForm.ShowDialog() == DialogResult.OK)
                {
                    if (AutoUpdaterDotNET.AutoUpdater.DownloadUpdate(args))
                    {
                        Application.Exit();
                    }
                }
            }
        }

        private static void AutoUpdaterOnParseUpdateInfoEvent(AutoUpdaterDotNET.ParseUpdateInfoEventArgs args)
        {
            try 
            {
                if (string.IsNullOrEmpty(args.RemoteData)) return;

                dynamic json = new System.Web.Script.Serialization.JavaScriptSerializer().Deserialize<dynamic>(args.RemoteData);
                
                string version = json["tag_name"]; 
                if (version.StartsWith("v")) version = version.Substring(1);
                
                string changelog = json["body"]; // 或 json["html_url"]
                
                var assets = json["assets"] as Array;
                string url = "";
                if (assets != null && assets.Length > 0)
                {
                     foreach (dynamic asset in assets)
                     {
                         if (asset["name"].EndsWith(".exe", StringComparison.OrdinalIgnoreCase))
                         {
                             url = asset["browser_download_url"];
                             break;
                         }
                     }
                     if (string.IsNullOrEmpty(url))
                     {
                         dynamic firstAsset = assets.GetValue(0);
                         url = firstAsset["browser_download_url"];
                     }
                }

                args.UpdateInfo = new AutoUpdaterDotNET.UpdateInfoEventArgs
                {
                    CurrentVersion = version,
                    ChangelogURL = json["body"], // 這裡改用 body (Release Note 文字) 讓介面顯示
                    DownloadURL = url,
                    Mandatory = new AutoUpdaterDotNET.Mandatory { Value = false },
                    InstallerArgs = "/SILENT /SP- /CLOSEAPPLICATIONS /RESTARTAPPLICATIONS" // 在這裡設定靜默安裝參數
                };
            }
            catch (Exception)
            {
                // 解析失敗忽略
            }
        }
    }
}
