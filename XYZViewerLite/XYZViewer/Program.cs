using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace Automation
{
    static class Program
    {
        /// <summary>
        /// 應用程式的主要進入點。
        /// </summary>
        [STAThread]
        static void Main(string[] arg)
        {
            //arg = new string[] { "D:\\Test\\111.ply", "D:\\Test\\111.opt" };
            
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new FormMain(arg));
        }
    }
}
