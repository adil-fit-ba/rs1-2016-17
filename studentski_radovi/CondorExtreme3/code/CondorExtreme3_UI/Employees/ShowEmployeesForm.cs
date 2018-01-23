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

namespace CondorExtreme3_UI.Employees
{
    public partial class ShowEmployeesForm : Form
    {
        public WebAPIHelper Service { get; set; }
        public ShowEmployeesForm()
        {
            InitializeComponent();
        }

       

        void BindForm()
        {
            Service = new WebAPIHelper("http://localhost:61732/", "api/Employees");
            HttpResponseMessage Response = Service.GetResponse();


            if (Response.IsSuccessStatusCode)
            {

                List<Object> Employees = Response.Content.ReadAsAsync<List<Object>>().Result;
                grdEmployees.DataSource = Employees;
                grdEmployees.Columns["EmployeeID"].Visible = false;
            }
            else
            {
                MessageBox.Show("Error status code: " + Response.StatusCode + " Message: " + Response.ReasonPhrase);
            }
        }



        private void btnAddEmployee_Click(object sender, EventArgs e)
        {
            AddEmployeeForm form = new AddEmployeeForm();
            if(form.ShowDialog()==DialogResult.OK)
            {
                BindForm();

            }
        }

        private void ShowEmployeesForm_Load(object sender, EventArgs e)
        {
            BindForm();
        }
    }
}
