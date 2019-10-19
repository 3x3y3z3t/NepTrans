using ExwSharp;
using System;
using System.Windows.Forms;

namespace NepTrans
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Logger.LogTarget = LogTarget.Console | LogTarget.File;
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form1());

            //Application.Run(new SummaryReportForm(null));



            Console.Write("\r\nPress Enter key to exit...");
            Console.Read();
        }
    }
}
