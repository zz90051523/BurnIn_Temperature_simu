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
            // 請確保 AutoUpdaterDotNET 套件已安裝 (NuGet: AutoUpdater.NET.Official)
             AutoUpdaterDotNET.AutoUpdater.Start("https://raw.githubusercontent.com/zz90051523/BurnIn_Temperature_simu/master/update.xml");


            Application.Run(new BurnIn_Temperature_Simu());
        }
    }
}
