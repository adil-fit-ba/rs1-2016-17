using Microsoft.Reporting.WebForms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace eAkademija.Web.Reports.Student
{
    public partial class MojiKurseviReport1 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

            if (!IsPostBack)
            {
                string korisnikId = Request.QueryString["korisnikId"];

                var podaciZaglavlje = MojiKurseviReport.GetZaglavlje(korisnikId);
                ReportViewer1.LocalReport.DataSources.Add(new ReportDataSource("Header", podaciZaglavlje));

                var podaciTijelo = MojiKurseviReport.GetTijelo(korisnikId);
                ReportViewer1.LocalReport.DataSources.Add(new ReportDataSource("Body", podaciTijelo));

                ReportViewer1.LocalReport.ReportPath = Server.MapPath("") + "/Report1.rdlc";
                ReportViewer1.DataBind();
                ReportViewer1.LocalReport.Refresh();
            }


        }
    }
}