using System;
using System.IO;
using System.Windows.Forms;

namespace VkBot
{
    class Program
    {
        /// <summary>
        /// Главная точка входа для приложения.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            // Application.Run(new Form1());
            FileCheck();
        }

        public static void FileCheck()
        {
            if (!File.Exists("accounts.dll"))
            {

                Application.Run(new LoginForm());
            }
            Application.Run(new Form1());
        }
    }
}
