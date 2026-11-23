using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;

public static class Db
{
    static string Cs { get { return ConfigurationManager.ConnectionStrings["UnitsDB"].ConnectionString; } }

    public static DataTable GetUnits(string category)
    {
        var dt = new DataTable();
        using (var cn = new SqlConnection(Cs))
        using (var da = new SqlDataAdapter("SELECT Id, NameAr, ToBase, Note FROM Units WHERE Category=@c ORDER BY SortOrder", cn))
        {
            da.SelectCommand.Parameters.AddWithValue("@c", category);
            da.Fill(dt);
        }
        return dt;
    }

    public static decimal? GetLatestPrice(int karat)
    {
        using (var cn = new SqlConnection(Cs))
        using (var cmd = new SqlCommand("SELECT TOP 1 PricePerGram FROM GoldPrices WHERE Karat=@k ORDER BY CreatedAt DESC", cn))
        {
            cmd.Parameters.AddWithValue("@k", karat);
            cn.Open();
            object o = cmd.ExecuteScalar();
            return o == null ? (decimal?)null : Convert.ToDecimal(o);
        }
    }

    public static void SavePrice(int karat, decimal price)
    {
        Exec("INSERT INTO GoldPrices (Karat, PricePerGram) VALUES (@a,@b)", "@a", karat, "@b", price);
    }

    public static void Log(string category, string details)
    {
        try { Exec("INSERT INTO ConversionHistory (Category, Details) VALUES (@a,@b)", "@a", category, "@b", details); }
        catch { /* السجل اختياري - لا نوقف المستخدم */ }
    }

    public static DataTable GetHistory(int top)
    {
        var dt = new DataTable();
        using (var cn = new SqlConnection(Cs))
        using (var da = new SqlDataAdapter("SELECT TOP (@n) Category, Details, CreatedAt FROM ConversionHistory ORDER BY Id DESC", cn))
        {
            da.SelectCommand.Parameters.AddWithValue("@n", top);
            da.Fill(dt);
        }
        return dt;
    }

    static void Exec(string sql, string p1, object v1, string p2, object v2)
    {
        using (var cn = new SqlConnection(Cs))
        using (var cmd = new SqlCommand(sql, cn))
        {
            cmd.Parameters.AddWithValue(p1, v1);
            cmd.Parameters.AddWithValue(p2, v2);
            cn.Open();
            cmd.ExecuteNonQuery();
        }
    }
}

public static class Num
{
    // يقبل أرقام عربية (٠-٩) وفاصلة عربية (٫) وفاصلة الآلاف
    public static bool TryParse(string s, out decimal value)
    {
        value = 0;
        if (string.IsNullOrWhiteSpace(s)) return false;
        var sb = new System.Text.StringBuilder();
        foreach (char ch in s.Trim())
        {
            if (ch >= '\u0660' && ch <= '\u0669') sb.Append((char)('0' + (ch - '\u0660')));
            else if (ch >= '\u06F0' && ch <= '\u06F9') sb.Append((char)('0' + (ch - '\u06F0')));
            else if (ch == '\u066B' || ch == ',') sb.Append(ch == ',' ? "" : ".");
            else if (ch == '\u066C' || ch == ' ') continue;
            else sb.Append(ch);
        }
        return decimal.TryParse(sb.ToString(), NumberStyles.Number, CultureInfo.InvariantCulture, out value);
    }

    public static string Fmt(decimal v, int max = 4)
    {
        return Math.Round(v, max).ToString("#,##0." + new string('#', max), CultureInfo.InvariantCulture);
    }
}
