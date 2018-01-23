using Microsoft.AspNet.Identity;
using Microsoft.Reporting.WebForms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace eAkademija.Web.Reports.Instruktor
{
    public partial class KurseviByInstruktor : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string loggedInUserId = User.Identity.GetUserId();

            if (!IsPostBack)
            {
                string id = Request.QueryString["instruktorId"];

                if(id.Equals(loggedInUserId))
                {
                    var podaciZaglavlje = Model_KurseviByInstruktor.GetZaglavlje(id);
                    Rv_KurseviByInstruktor.LocalReport.DataSources.Add(new ReportDataSource("Zaglavlje", podaciZaglavlje));

                    var podaciTijelo = Model_KurseviByInstruktor.GetTijelo(id);
                    Rv_KurseviByInstruktor.LocalReport.DataSources.Add(new ReportDataSource("Tijelo", podaciTijelo));

                    Rv_KurseviByInstruktor.LocalReport.ReportPath = Server.MapPath("") + "/Rpt_KurseviByInstruktor.rdlc";
                    Rv_KurseviByInstruktor.DataBind();
                    Rv_KurseviByInstruktor.LocalReport.Refresh();
                }
                else
                {
                    Response.Write("ID instruktora je neispravan!");
                }

                
            }
        }
    }
}