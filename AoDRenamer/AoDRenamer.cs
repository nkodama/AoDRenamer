using System;
using System.IO;
using System.Windows.Forms;

namespace AoDRenamer
{
    /// <summary>
    ///     アプリケーションクラス
    /// </summary>
    public static class AoDRenamer
    {
        /// <summary>
        ///     バージョン名
        /// </summary>
        public static string VersionName = "";

        /// <summary>
        ///     エントリーポイント
        /// </summary>
        [STAThread]
        public static void Main()
        {
            if (File.Exists("version.ini"))
            {
                var sr = new StreamReader("version.ini");
                VersionName = sr.ReadLine();
                sr.Close();
            }

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            Application.Run(new MainForm());
        }
    }
}