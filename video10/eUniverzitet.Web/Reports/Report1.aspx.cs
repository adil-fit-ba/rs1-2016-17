using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Microsoft.Reporting.WebForms;

namespace eUniverzitet.Web.Reports
{
    public partial class Report1 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                int odsjekId =  int.Parse(Request.QueryString["odsjekId"]);

                var podaciHeader = Report1_Model.GetHeader(odsjekId);
                ReportViewer1.LocalReport.DataSources.Add(new ReportDataSource("Header", podaciHeader));

                var podaciBody = Report1_Model.GetBody(odsjekId);
                ReportViewer1.LocalReport.DataSources.Add(new ReportDataSource("Body", podaciBody));

                ReportViewer1.LocalReport.ReportPath = Server.MapPath("") + "/Report1.rdlc";
                ReportViewer1.DataBind();
                ReportViewer1.LocalReport.Refresh();
            }
        }
    }
}