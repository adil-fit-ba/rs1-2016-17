using CondorExtreme3_UI.Helper;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CondorExtreme3_UI.Index
{
    public partial class EmployeeLoginForm : Form
    {
         

        public WebAPIHelper Service { get; set; }


        public EmployeeLoginForm()
        {
            InitializeComponent();
        }

        private void btnSignin_Click(object sender, EventArgs e)
        {
            Service = new WebAPIHelper("http://localhost:61732/", "api/Employees");

            HttpResponseMessage Response = Service.GetResponse(txtUsername.Text);

            if (Response.IsSuccessStatusCode)
            {
                dynamic Emp = Response.Content.ReadAsAsync<Object>().Result;

                if(Emp.PasswordHash==UIHelper.GenerateHash(txtPassword.Text, Emp.PasswordSalt))
                {
                    MessageBox.Show("Welcome " + Emp.FirstName + " " + Emp.LastName + "!");
                    DialogResult = DialogResult.OK;
                }
                else
                {
                    MessageBox.Show("Username or password is incorrect!","Warning",MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            else
            {
                MessageBox.Show("Error status code: " + Response.StatusCode + " Message: " + Response.ReasonPhrase);
            }



        }
    }
}
