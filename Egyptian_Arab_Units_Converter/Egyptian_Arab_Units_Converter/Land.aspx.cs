using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web.UI.WebControls;

namespace Egyptian_Arab_Units_Converter
{
    public partial class LandPage : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                try
                {
                    DataTable dt = Db.GetUnits("Land");
                    foreach (var d in new[] { ddlLandFrom, ddlLandTo })
                    {
                        d.DataSource = dt;
                        d.DataTextField = "NameAr";
                        d.DataValueField = "Id";
                        d.DataBind();
                    }
                    ddlLandFrom.SelectedIndex = 0;
                    ddlLandTo.SelectedIndex = 4;
                }
                catch (Exception ex) { ShowError(SiteLang.T("ErrDb") + " (" + ex.Message + ")"); }
            }

            txtLandAmount.Attributes["placeholder"] = SiteLang.T("AmountPh");
            btnLand.Text = SiteLang.T("Convert");
        }

        protected void btnLand_Click(object sender, EventArgs e)
        {
            decimal amt;
            if (!Num.TryParse(txtLandAmount.Text, out amt) || amt < 0)
            {
                ShowError(SiteLang.T("ErrAmount"));
                return;
            }
            try
            {
                DataTable units = Db.GetUnits("Land");
                DataRow from = Row(units, ddlLandFrom.SelectedValue),
                        to = Row(units, ddlLandTo.SelectedValue);

                decimal baseM2 = amt * (decimal)from["ToBase"];
                decimal res = baseM2 / (decimal)to["ToBase"];

                litLandBig.Text = string.Format(
                    "<span class='n'>{0}</span> {1} <span class='eq'>=</span> <span class='n res'>{2}</span> {3}",
                    Num.Fmt(amt), from["NameAr"], Num.Fmt(res), to["NameAr"]);

                var all = new List<object>();
                foreach (DataRow r in units.Rows)
                {
                    if ((int)r["Id"] == (int)from["Id"]) continue;
                    all.Add(new
                    {
                        Value = Num.Fmt(baseM2 / (decimal)r["ToBase"]),
                        Name = r["NameAr"]
                    });
                }
                rptLandAll.DataSource = all;
                rptLandAll.DataBind();

                decimal fed = units.AsEnumerable()
                                   .Where(r => (string)r["NameAr"] == "فدان")
                                   .Select(r => (decimal)r["ToBase"]).First();
                long sahm = (long)Math.Round(baseM2 / fed * 576m);
                litFeddanSplit.Text = sahm > 0
                    ? string.Format(
                        "<div class='contract'><b>{0}</b><span>{1} فدان و {2} قيراط و {3} سهم</span></div>",
                        SiteLang.T("ContractFormat"), sahm / 576, (sahm % 576) / 24, sahm % 24)
                    : "";

                phLandEmpty.Visible = false;
                phLandResult.Visible = true;
                Db.Log("Land", string.Format("{0} {1} = {2} {3}",
                    Num.Fmt(amt), from["NameAr"], Num.Fmt(res), to["NameAr"]));
            }
            catch (Exception ex) { ShowError(SiteLang.T("ErrConvert") + ex.Message); }
        }

        static DataRow Row(DataTable dt, string id) =>
            dt.AsEnumerable().First(r => r["Id"].ToString() == id);

        void ShowError(string msg) { lblError.Text = msg; lblError.Visible = true; }
    }
}