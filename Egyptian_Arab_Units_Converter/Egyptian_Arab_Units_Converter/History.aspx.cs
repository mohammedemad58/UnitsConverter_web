using System;
namespace Egyptian_Arab_Units_Converter
{
    public partial class HistoryPage : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                var h = Db.GetHistory(30);
                rptHistory.DataSource = h;
                rptHistory.DataBind();
                lblNoHistory.Text = SiteLang.T("NoHistory");
                lblNoHistory.Visible = h.Rows.Count == 0;
            }
            catch
            {
                lblNoHistory.Text = SiteLang.T("ErrHistory");
                lblNoHistory.Visible = true;
            }
        }
    }
}