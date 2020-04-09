using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace solpr
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        internal static Form1 mf;
        internal static SignIn st;

        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            mf = new Form1();
            st = new SignIn();
            Application.Run(st);
        }
    }
}
