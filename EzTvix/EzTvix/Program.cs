using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace EzTvix
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
            try
            {

                string[] args = Environment.GetCommandLineArgs();

                // The first commandline argument is always the executable path itself.
                if (args.Length > 1)
                {
                    if (Array.IndexOf(args, "/ThemeBuilder") != -1)
                    {
                        Application.Run(new ThemeBuilder());
                    }
                    else
                    {
                        Application.Run(new MainForm());
                    }
                }
                else
                {
                    Application.Run(new MainForm());
                }
            }
            catch (Exception e)
            {
                MessageBox.Show(
                    e.Message + "\r\n" + e.StackTrace, 
                    e.InnerException + " - " + e.Source, 
                    MessageBoxButtons.OK, 
                    MessageBoxIcon.Exclamation, 
                    MessageBoxDefaultButton.Button1);
            }
        }
    }
}
