using System;
using System.IO;
using System.Web;

namespace Egyptian_Arab_Units_Converter
{
    public partial class SiteMaster : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e) { }

        protected string Active(string page)
        {
            string cur = Path.GetFileNameWithoutExtension(Request.Path);
            if (string.IsNullOrEmpty(cur)) cur = "Default";
            return string.Equals(cur, page, StringComparison.OrdinalIgnoreCase) ? "on" : "";
        }

        // Exposed helpers used in the markup
        protected string NavOn(string page) => Active(page);
        protected string GoldOn() => Active("Gold");
        protected string Lang => SiteLang.Current;
        protected string IsRtl => SiteLang.IsRtl ? "rtl" : "ltr";
        protected string L(string key) => SiteLang.T(key);

        protected string ToggleUrl()
        {
            var other = SiteLang.IsRtl ? "en" : "ar";
            var q = HttpUtility.ParseQueryString(Request.QueryString.ToString());
            q["lang"] = other;
            return Request.Path + "?" + q.ToString();
        }
    }
}