using Microsoft.Reporting.WebForms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace eAkademija.Web.Reports.Instruktor
{
    public partial class KursById : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                int id = int.Parse(Request.QueryString["id"]);

                var podaciZaglavlje = Model_KursById.GetZaglavlje(id);
                Rv_KursById.LocalReport.DataSources.Add(new ReportDataSource("Zaglavlje", podaciZaglavlje));

                var podaciTijelo = Model_KursById.GetTijelo(id);
                Rv_KursById.LocalReport.DataSources.Add(new ReportDataSource("Tijelo", podaciTijelo));

                Rv_KursById.LocalReport.ReportPath = Server.MapPath("") + "/Rpt_KursById.rdlc";
                Rv_KursById.DataBind();
                Rv_KursById.LocalReport.Refresh();
            }
        }
    }
}