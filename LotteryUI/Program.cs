using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LotteryUI
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            //try { 
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);
                Application.Run(new Main());
            //}
            //catch (Exception E)
            //{
            //    MessageBox.Show("ERROR: " + E.Message);
            //    MessageBox.Show("ERROR: " + E.InnerException.Message);
            //}
        }
    }
}
