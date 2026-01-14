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
            
            // 重要：確保使用 TLS 1.2，否則 GitHub 會拒絕連線
            System.Net.ServicePointManager.SecurityProtocol = System.Net.SecurityProtocolType.Tls12;
            
            // 指向 GitHub 上的 update.xml
            AutoUpdaterDotNET.AutoUpdater.ReportErrors = true; // 啟用錯誤回報，方便除錯
            AutoUpdaterDotNET.AutoUpdater.Start("https://raw.githubusercontent.com/zz90051523/BurnIn_Temperature_simu/master/update.xml");
            
            Application.Run(new BurnIn_Temperature_Simu());
        }
    }
}
