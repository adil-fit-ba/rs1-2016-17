using CondorExtreme3_UI.Employees;
using CondorExtreme3_UI.Index;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CondorExtreme3_UI
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

            EmployeeLoginForm frm = new EmployeeLoginForm();
            if (frm.ShowDialog()==DialogResult.OK)
            {
                Application.Run(new Index.IndexForm());

            }



        }
    }
}
