using System;
using System.Data;
using System.Linq;
using System.Web.UI.WebControls;

namespace Egyptian_Arab_Units_Converter
{
    public partial class GoldPage : System.Web.UI.Page
    {
        static readonly int[] Karats = { 24, 22, 21, 18, 14, 12 };

        protected void Page_Load(object sender, EventArgs e)
        {
            txtGWAmount.Attributes["placeholder"] = SiteLang.T("WeightPh");
            txtGKGrams.Attributes["placeholder"] = SiteLang.T("GramsPh");
            txtPPrice.Attributes["placeholder"] = SiteLang.T("GramPricePh");

            btnGW.Text = SiteLang.T("ConvertWeight");
            btnKarat.Text = SiteLang.T("ConvertKarat");
            btnPrice.Text = SiteLang.T("CalcTotal");

            if (ddlMakingType.Items.Count == 3)
            {
                ddlMakingType.Items[0].Text = SiteLang.T("MakingGram");
                ddlMakingType.Items[1].Text = SiteLang.T("MakingPct");
                ddlMakingType.Items[2].Text = SiteLang.T("MakingTotal");
            }

            if (IsPostBack) return;
            try
            {
                BindUnits(ddlGWFrom, ddlGWTo, "GoldWeight", 1, 0);
                foreach (var ddl in new[] { ddlKFrom, ddlKTo, ddlPKarat })
                    foreach (int k in Karats)
                        ddl.Items.Add(new ListItem(
                            (SiteLang.IsRtl ? "عيار " : "Karat ") + k, k.ToString()));
                ddlKFrom.SelectedValue = "21";
                ddlKTo.SelectedValue = "18";
                ddlPKarat.SelectedValue = "21";
                FillSavedPrice();
            }
            catch (Exception ex) { ShowError(SiteLang.T("ErrDb") + " (" + ex.Message + ")"); }
        }

        protected void btnGW_Click(object sender, EventArgs e)
        {
            decimal amt;
            if (!Num.TryParse(txtGWAmount.Text, out amt) || amt < 0) { ShowError(SiteLang.T("ErrGoldWeight")); return; }
            try
            {
                DataTable units = Db.GetUnits("GoldWeight");
                DataRow from = Row(units, ddlGWFrom.SelectedValue), to = Row(units, ddlGWTo.SelectedValue);
                decimal res = amt * (decimal)from["ToBase"] / (decimal)to["ToBase"];
                string line = string.Format("{0} {1} = {2} {3}",
                    Num.Fmt(amt), from["NameAr"], Num.Fmt(res), to["NameAr"]);
                litGW.Text = "<div class='big-sm'>" + line + "</div>";
                pnlGW.Visible = true;
                Db.Log("Gold", line);
            }
            catch (Exception ex) { ShowError(SiteLang.T("ErrConvert") + ex.Message); }
        }

        protected void btnKarat_Click(object sender, EventArgs e)
        {
            decimal g;
            if (!Num.TryParse(txtGKGrams.Text, out g) || g < 0) { ShowError(SiteLang.T("ErrGrams")); return; }
            int kf = int.Parse(ddlKFrom.SelectedValue), kt = int.Parse(ddlKTo.SelectedValue);
            decimal res = g * kf / kt;
            decimal pure = g * kf / 24m;
            string line = string.Format("{0} g {1} = {2} g {3}",
                Num.Fmt(g), KLabel(kf), Num.Fmt(res), KLabel(kt));
            litKarat.Text = "<div class='big-sm'>" + line + "</div><div class='mini'>"
                + SiteLang.T("GoldPure24") + Num.Fmt(pure)
                + (SiteLang.IsRtl ? " جرام" : " g") + "</div>";
            pnlKarat.Visible = true;
            Db.Log("Gold", line);
        }

        protected void btnPrice_Click(object sender, EventArgs e)
        {
            decimal g, price, making = 0;
            if (!Num.TryParse(txtPGrams.Text, out g) || g <= 0) { ShowError(SiteLang.T("ErrGramPositive")); return; }
            if (!Num.TryParse(txtPPrice.Text, out price) || price <= 0) { ShowError(SiteLang.T("ErrPrice")); return; }
            if (!string.IsNullOrWhiteSpace(txtMaking.Text) && !Num.TryParse(txtMaking.Text, out making))
            { ShowError(SiteLang.T("ErrMaking")); return; }

            int k = int.Parse(ddlPKarat.SelectedValue);
            decimal gold = g * price, mk;
            switch (ddlMakingType.SelectedValue)
            {
                case "percent": mk = gold * making / 100m; break;
                case "total": mk = making; break;
                default: mk = g * making; break;
            }
            decimal total = gold + mk;

            string goldRow = SiteLang.T("GoldPriceRow") + " (" + Num.Fmt(g) + " g " + KLabel(k) + ")";
            litPrice.Text = string.Format(
                "<table class='sum'>" +
                "<tr><td>{0}</td><td>{1}</td></tr>" +
                "<tr><td>{2}</td><td>{3}</td></tr>" +
                "<tr class='tot'><td>{4}</td><td>{5} {6}</td></tr></table>",
                goldRow, Num.Fmt(gold, 2),
                SiteLang.T("MakingRow"), Num.Fmt(mk, 2),
                SiteLang.T("TotalRow"), Num.Fmt(total, 2), SiteLang.T("EGP"));
            pnlPrice.Visible = true;

            try { if (chkSavePrice.Checked) Db.SavePrice(k, price); } catch { }
            Db.Log("Gold", string.Format("{0} g {1} @ {2} + making {3} = {4} EGP",
                Num.Fmt(g), KLabel(k), Num.Fmt(price, 2), Num.Fmt(mk, 2), Num.Fmt(total, 2)));
        }

        protected void ddlPKarat_Changed(object sender, EventArgs e) { FillSavedPrice(); }

        string KLabel(int k) => (SiteLang.IsRtl ? "عيار " : "k") + k;

        void FillSavedPrice()
        {
            try
            {
                decimal? p = Db.GetLatestPrice(int.Parse(ddlPKarat.SelectedValue));
                txtPPrice.Text = p.HasValue ? Num.Fmt(p.Value, 2).Replace(",", "") : "";
            }
            catch { }
        }

        static void BindUnits(DropDownList a, DropDownList b, string cat, int selA, int selB)
        {
            DataTable dt = Db.GetUnits(cat);
            foreach (var d in new[] { a, b })
            { d.DataSource = dt; d.DataTextField = "NameAr"; d.DataValueField = "Id"; d.DataBind(); }
            a.SelectedIndex = selA; b.SelectedIndex = selB;
        }

        static DataRow Row(DataTable dt, string id) =>
            dt.AsEnumerable().First(r => r["Id"].ToString() == id);

        void ShowError(string msg) { lblError.Text = msg; lblError.Visible = true; }
    }
}