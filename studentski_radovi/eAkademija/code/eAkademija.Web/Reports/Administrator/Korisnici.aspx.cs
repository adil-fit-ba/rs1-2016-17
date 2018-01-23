using Microsoft.Reporting.WebForms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace eAkademija.Web.Reports.Administrator
{
    public partial class Korisnici : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

            if (!IsPostBack)
            {
                string instruktorId = Request.QueryString["korisnikId"];

                var podaciZaglavlje = Model_Korisnici.GetZaglavlje(instruktorId);
                Rv_Korisnici.LocalReport.DataSources.Add(new ReportDataSource("Header", podaciZaglavlje));

                var podaciTijelo = Model_Korisnici.GetTijelo(instruktorId);
                Rv_Korisnici.LocalReport.DataSources.Add(new ReportDataSource("Body", podaciTijelo));

                Rv_Korisnici.LocalReport.ReportPath = Server.MapPath("") + "/Korisnici.rdlc";
                Rv_Korisnici.DataBind();
                Rv_Korisnici.LocalReport.Refresh();
            }
        }
    }
}