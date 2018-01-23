using CondorExtreme3_UI.Helper;
using CondorExtreme3_UI.Models;
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
    public partial class AddEmployeeForm : Form
    {
        public WebAPIHelper Service { get; set; }

        public AddEmployeeForm()
        {
            InitializeComponent();
        }


        private void AddEmployeeForm_Load(object sender, EventArgs e)
        {
            Service = new WebAPIHelper("http://localhost:61732/", "api/Cities");
            HttpResponseMessage Response= Service.GetResponse();
            List<Object>cities= Response.Content.ReadAsAsync<List<Object>>().Result;
            cmbCities.DataSource = cities;
            cmbCities.DisplayMember = "Name";
            cmbCities.ValueMember = "CityID";

            string[] Genders = { "Male", "Female" };
            cmbGender.DataSource = Genders;

        }

        private void btnSubmitEmployee_Click(object sender, EventArgs e)
        {
            Service = new WebAPIHelper("http://localhost:61732/", "api/Employees");

            
            //string PasswordHash= ;
            // = ;
            //int CityID = SelectedCity.CityID;
            //string datetime=dtpBirthDate.Value.ToString();

            dynamic SelectedCity = cmbCities.SelectedItem;

            string PasswordSalt = UIHelper.GenerateSalt();


            EmployeeAdd obj = new EmployeeAdd();
            obj.FirstName = txtFirstName.Text;
            obj.LastName = txtLastName.Text;
            obj.CityBirthID = Int32.Parse(cmbCities.SelectedValue.ToString());
            obj.BirthDate = dtpBirthDate.Value;
            obj.Gender = (cmbGender.SelectedItem.ToString() == "Male") ? false : true;
            obj.CurriculumVitae = txtCurriculumVitae.Text;
            obj.Email = txtEmail.Text;
            obj.Username = txtUsername.Text;
            obj.PhoneNumber = txtPhoneNumber.Text;
            obj.PasswordSalt = PasswordSalt;
            obj.PasswordHash = UIHelper.GenerateHash(txtPassword.Text, PasswordSalt);
            obj.IsDeleted = false;
            



           HttpResponseMessage Response= Service.PostResponse(obj);

            if (Response.IsSuccessStatusCode)
            {
                MessageBox.Show("Employee added successfully!");
                DialogResult = DialogResult.OK;
                Close();
            }
            else
            {
                MessageBox.Show("Error status code: " + Response.StatusCode + " Message: " + Response.ReasonPhrase);
            }
        }
    }
}
