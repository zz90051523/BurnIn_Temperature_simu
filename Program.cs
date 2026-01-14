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
            // [Velopack] 初始化：處理安裝、移除、更新等事件
            // 這行必須放在 Main 的最前面
            Velopack.VelopackApp.Build().Run();

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            
            // [Velopack] 背景檢查更新
            System.Threading.Tasks.Task.Run(async () =>
            {
                try
                {
                    // 使用 GitHub 作為更新來源
                    // 注意：此處 URL 為您 GitHub Repo 的首頁
                    var mgr = new Velopack.UpdateManager(new Velopack.Sources.GithubSource("https://github.com/zz90051523/BurnIn_Temperature_simu", null, false));

                    // 檢查是否有新版本
                    var newVersion = await mgr.CheckForUpdatesAsync();
                    if (newVersion != null)
                    {
                        // 下載更新
                        await mgr.DownloadUpdatesAsync(newVersion);

                        // 提示使用者或直接重啟套用 (這裡示範自動重啟並套用)
                        // 若要提示使用者，可以用 Invoke 回到 UI Thread 跳出視窗
                        mgr.ApplyUpdatesAndRestart(newVersion);
                    }
                }
                catch (Exception ex)
                {
                    // 更新失敗，暫時忽略或紀錄 Log
                    Console.WriteLine("Update failed: " + ex.ToString());
                }
            });

            Application.Run(new BurnIn_Temperature_Simu());
        }
    }
}
