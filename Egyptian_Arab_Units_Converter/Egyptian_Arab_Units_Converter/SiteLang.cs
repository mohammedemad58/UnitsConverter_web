using System;
using System.Collections.Generic;
using System.Web;

namespace Egyptian_Arab_Units_Converter
{
    /// <summary>
    /// Language state + translation lookup.
    /// Language is stored on the session and can be forced with ?lang=ar|en.
    /// </summary>
    public static class SiteLang
    {
        public const string SessionKey = "lang";

        public static string Current
        {
            get
            {
                var ctx = HttpContext.Current;
                if (ctx == null) return "ar";

                var q = ctx.Request.QueryString["lang"];
                if (!string.IsNullOrEmpty(q))
                {
                    var v = q.ToLowerInvariant().StartsWith("en") ? "en" : "ar";
                    ctx.Session[SessionKey] = v;
                    return v;
                }

                var s = ctx.Session[SessionKey] as string;
                if (!string.IsNullOrEmpty(s)) return s;

                var accept = ctx.Request.UserLanguages;
                if (accept != null && accept.Length > 0 &&
                    accept[0].ToLowerInvariant().StartsWith("en"))
                    return "en";

                return "ar";
            }
        }

        public static bool IsRtl => Current == "ar";

        public static string T(string key)
        {
            if (string.IsNullOrEmpty(key)) return "";
            var table = Current == "en" ? En : Ar;
            string val;
            if (table.TryGetValue(key, out val)) return val;
            if (Ar.TryGetValue(key, out val)) return val;
            return key;
        }

        // =====================  ARABIC =====================
        private static readonly Dictionary<string, string> Ar =
            new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase)
        {
            { "SiteTitle",       "محول المقاييس المحلية" },
            { "SiteTag",         "أراضي • ذهب • تحويلات مصرية" },
            { "Home",            "الرئيسية" },
            { "LandConversion",  "تحويل الأراضي" },
            { "Gold",            "الذهب" },
            { "GoldConversion",  "تحويل الذهب" },
            { "GoldWeight",      "تحويل الوزن" },
            { "GoldWeightSub",   "جرام • أوقية • جنيه ذهب" },
            { "GoldKarat",       "تحويل العيار" },
            { "GoldKaratSub",    "من 24 إلى 21 و 18" },
            { "GoldPrice",       "السعر والمصنعية" },
            { "GoldPriceSub",    "احسب إجمالي القطعة" },
            { "History",         "سجل التحويلات" },
            { "MainNav",         "التنقل الرئيسي" },
            { "Menu",            "القائمة" },
            { "Close",           "إغلاق" },
            { "Nav",             "التنقل" },
            { "SwitchLang",      "تغيير اللغة" },
            { "QuickLinks",      "روابط سريعة" },
            { "Info",            "معلومات" },
            { "About",           "عن الموقع" },
            { "Privacy",         "سياسة الخصوصية" },
            { "Contact",         "اتصل بنا" },
            { "FooterDesc",      "أداة عربية موثوقة لتحويل مقاييس الأراضي والذهب إلى الوحدات المصرية الشائعة. القيم للاسترشاد فقط وليست بديلاً عن المساحة الرسمية أو أسعار الصاغة." },
            { "Rights",          "جميع الحقوق محفوظة" },

            { "HeroTitle",       "الفدان والقيراط والسهم والذهب بكل العيارات في مكان واحد" },
            { "HeroSub",         "المقاييس اللي بنستخدمها في عقود الشراء والعقارات والصاغة، بتتحول بدقة وبسرعة من غير ما تحتاج تحسبها بإيدك." },
            { "HeroLandCta",     "حوّل مساحة أرض" },
            { "HeroGoldCta",     "احسب الذهب" },
            { "RefTitle",        "معادلات الأراضي" },
            { "ChooseTitle",     "اختار اللي محتاجه" },
            { "CatLandTitle",    "تحويل الأراضي" },
            { "CatLandSub",      "من فدان لقيراط وسهم وقصبة ومتر مربع، مع صيغة العقود الجاهزة." },
            { "CatLandGo",       "ابدأ التحويل" },
            { "CatGoldWTitle",   "وزن الذهب" },
            { "CatGoldWSub",     "جرام وأوقية وكيلو وجنيه ذهب، بدقة الأوقية التروي." },
            { "CatGoldWGo",      "حوّل الوزن" },
            { "CatGoldKTitle",   "عيارات الذهب" },
            { "CatGoldKSub",     "حوّل من عيار 24 أو 22 أو 21 أو 18 لأي عيار بنفس كمية الذهب الخالص." },
            { "CatGoldKGo",      "حوّل العيار" },
            { "CatGoldPTitle",   "السعر والمصنعية" },
            { "CatGoldPSub",     "اكتب الوزن وسعر الجرام والمصنعية واعرف إجمالي القطعة." },
            { "CatGoldPGo",      "احسب الإجمالي" },
            { "How1T",           "نتائج دقيقة" },
            { "How1D",           "المعاملات متخزنة في قاعدة بيانات وتقدر تعدلها." },
            { "How2T",           "أرقام عربي وإنجليزي" },
            { "How2D",           "اكتب ٣٫٥ أو 3.5 والاتنين شغالين." },
            { "How3T",           "سجل بكل عملية" },
            { "How3D",           "آخر التحويلات بتتحفظ وتقدر تراجعها." },

            { "LandTitle",       "تحويل الأراضي" },
            { "LandSub",         "فدان • قيراط • سهم • قصبة • متر مربع • هكتار" },
            { "LandInputTitle",  "أدخل المساحة" },
            { "Amount",          "المساحة" },
            { "AmountPh",        "مثال: 3.5" },
            { "From",            "من" },
            { "To",              "إلى" },
            { "Convert",         "حوّل" },
            { "LandHint",        "الفدان = 24 قيراط • القيراط = 24 سهم • الفدان = 4200.833 م²" },
            { "LandEmpty",       "اكتب المساحة واختر الوحدتين، وهتظهر النتيجة وكل المعادلات هنا." },
            { "LandAlsoEq",      "يعادل أيضاً" },
            { "ContractFormat",  "بصيغة العقود" },
            { "ErrAmount",       "اكتب مساحة صحيحة (أرقام فقط)." },
            { "ErrDb",           "تعذر الاتصال بقاعدة البيانات. تأكد من تشغيل Database.sql ومن الـ connectionString." },
            { "ErrConvert",      "حدث خطأ أثناء التحويل: " },

            { "GoldPageTitle",   "حاسبة الذهب" },
            { "GoldPageSub",     "وزن • عيار • سعر ومصنعية" },
            { "WeightTitle",     "تحويل الوزن" },
            { "Weight",          "الوزن" },
            { "WeightPh",        "مثال: 1" },
            { "ConvertWeight",   "حوّل الوزن" },
            { "WeightHint",      "جنيه الذهب = 8 جرام عيار 21 • الأوقية = 31.1035 جرام" },
            { "KaratTitle",      "تحويل العيار" },
            { "Grams",           "الوزن بالجرام" },
            { "GramsPh",         "مثال: 10" },
            { "FromKarat",       "من عيار" },
            { "ToKarat",         "إلى عيار" },
            { "ConvertKarat",    "حوّل العيار" },
            { "KaratHint",       "بيحسب نفس كمية الذهب الخالص بعد التحويل." },
            { "PriceTitle",      "السعر والمصنعية" },
            { "GramsShort",      "الوزن (جرام)" },
            { "Karat",           "العيار" },
            { "GramPrice",       "سعر الجرام (جنيه)" },
            { "GramPricePh",     "سعر جرام العيار المختار" },
            { "Making",          "المصنعية" },
            { "MakingType",      "نوعها" },
            { "MakingGram",      "جنيه للجرام" },
            { "MakingPct",       "نسبة % من الذهب" },
            { "MakingTotal",     "مبلغ ثابت" },
            { "SavePrice",       "احفظ السعر لهذا العيار" },
            { "CalcTotal",       "احسب الإجمالي" },
            { "GoldPure24",      "الذهب الخالص (24): " },
            { "GoldPriceRow",    "ثمن الذهب" },
            { "MakingRow",       "المصنعية" },
            { "TotalRow",        "الإجمالي" },
            { "EGP",             "جنيه" },
            { "ErrGoldWeight",   "اكتب وزناً صحيحاً." },
            { "ErrGrams",        "اكتب الوزن بالجرام." },
            { "ErrGramPositive", "اكتب وزناً صحيحاً بالجرام." },
            { "ErrPrice",        "اكتب سعر الجرام." },
            { "ErrMaking",       "المصنعية لازم تكون رقم." },

            { "HistoryTitle",    "سجل التحويلات" },
            { "HistorySub",      "آخر 30 عملية تحويل تمت على الموقع" },
            { "TagLand",         "أراضي" },
            { "TagGold",         "ذهب" },
            { "NoHistory",       "لسه مفيش تحويلات. جرّب تحويل من صفحة الأراضي أو الذهب." },
            { "ErrHistory",      "تعذر قراءة السجل من قاعدة البيانات." },
        };

        // =====================  ENGLISH =====================
        private static readonly Dictionary<string, string> En =
            new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase)
        {
            { "SiteTitle",       "Egyptian Units Converter" },
            { "SiteTag",         "Land • Gold • Egyptian conversions" },
            { "Home",            "Home" },
            { "LandConversion",  "Land conversion" },
            { "Gold",            "Gold" },
            { "GoldConversion",  "Gold conversion" },
            { "GoldWeight",      "Weight conversion" },
            { "GoldWeightSub",   "Gram • Ounce • Gold pound" },
            { "GoldKarat",       "Karat conversion" },
            { "GoldKaratSub",    "24 to 21 and 18" },
            { "GoldPrice",       "Price & making charge" },
            { "GoldPriceSub",    "Calculate total item" },
            { "History",         "Conversion history" },
            { "MainNav",         "Main navigation" },
            { "Menu",            "Menu" },
            { "Close",           "Close" },
            { "Nav",             "Navigation" },
            { "SwitchLang",      "Switch language" },
            { "QuickLinks",      "Quick links" },
            { "Info",            "Information" },
            { "About",           "About" },
            { "Privacy",         "Privacy policy" },
            { "Contact",         "Contact us" },
            { "FooterDesc",      "A reliable bilingual tool for converting Egyptian land and gold measurements to commonly used units. Values are for guidance only and not a substitute for official measurements or market gold prices." },
            { "Rights",          "All rights reserved" },

            { "HeroTitle",       "Feddan, qirat, sahm and gold karats — all in one place" },
            { "HeroSub",         "The measurements used in purchase contracts, real estate and gold shops, converted quickly and accurately so you don't need to compute them yourself." },
            { "HeroLandCta",     "Convert land area" },
            { "HeroGoldCta",     "Calculate gold" },
            { "RefTitle",        "Land formulas" },
            { "ChooseTitle",     "Choose what you need" },
            { "CatLandTitle",    "Land conversion" },
            { "CatLandSub",      "Feddan, qirat, sahm, qasaba and square metre — with ready contract phrasing." },
            { "CatLandGo",       "Start conversion" },
            { "CatGoldWTitle",   "Gold weight" },
            { "CatGoldWSub",     "Gram, ounce, kilo and gold pound with troy-ounce precision." },
            { "CatGoldWGo",      "Convert weight" },
            { "CatGoldKTitle",   "Gold karats" },
            { "CatGoldKSub",     "Convert between 24, 22, 21 and 18 karats keeping the same pure-gold content." },
            { "CatGoldKGo",      "Convert karat" },
            { "CatGoldPTitle",   "Price & making charge" },
            { "CatGoldPSub",     "Enter weight, gram price and making charge to get the total." },
            { "CatGoldPGo",      "Calculate total" },
            { "How1T",           "Accurate results" },
            { "How1D",           "Conversion factors are stored in the database and fully editable." },
            { "How2T",           "Arabic & English digits" },
            { "How2D",           "Type ٣٫٥ or 3.5 — both work." },
            { "How3T",           "Full history" },
            { "How3D",           "Every conversion is logged so you can review it later." },

            { "LandTitle",       "Land conversion" },
            { "LandSub",         "Feddan • Qirat • Sahm • Qasaba • m² • Hectare" },
            { "LandInputTitle",  "Enter area" },
            { "Amount",          "Area" },
            { "AmountPh",        "e.g. 3.5" },
            { "From",            "From" },
            { "To",              "To" },
            { "Convert",         "Convert" },
            { "LandHint",        "1 feddan = 24 qirat • 1 qirat = 24 sahm • 1 feddan = 4,200.833 m²" },
            { "LandEmpty",       "Enter the area and pick the two units — the result and all equivalences will appear here." },
            { "LandAlsoEq",      "Also equals" },
            { "ContractFormat",  "Contract format" },
            { "ErrAmount",       "Please enter a valid area (numbers only)." },
            { "ErrDb",           "Could not connect to the database. Make sure Database.sql is run and the connection string is set." },
            { "ErrConvert",      "Conversion error: " },

            { "GoldPageTitle",   "Gold calculator" },
            { "GoldPageSub",     "Weight • Karat • Price & making" },
            { "WeightTitle",     "Weight conversion" },
            { "Weight",          "Weight" },
            { "WeightPh",        "e.g. 1" },
            { "ConvertWeight",   "Convert weight" },
            { "WeightHint",      "1 gold pound = 8 g of 21k • 1 troy ounce = 31.1035 g" },
            { "KaratTitle",      "Karat conversion" },
            { "Grams",           "Weight in grams" },
            { "GramsPh",         "e.g. 10" },
            { "FromKarat",       "From karat" },
            { "ToKarat",         "To karat" },
            { "ConvertKarat",    "Convert karat" },
            { "KaratHint",       "Keeps the same pure-gold content after conversion." },
            { "PriceTitle",      "Price & making charge" },
            { "GramsShort",      "Weight (g)" },
            { "Karat",           "Karat" },
            { "GramPrice",       "Gram price (EGP)" },
            { "GramPricePh",     "Price of the selected karat" },
            { "Making",          "Making charge" },
            { "MakingType",      "Type" },
            { "MakingGram",      "EGP per gram" },
            { "MakingPct",       "% of gold value" },
            { "MakingTotal",     "Fixed amount" },
            { "SavePrice",       "Save price for this karat" },
            { "CalcTotal",       "Calculate total" },
            { "GoldPure24",      "Pure gold (24k): " },
            { "GoldPriceRow",    "Gold value" },
            { "MakingRow",       "Making charge" },
            { "TotalRow",        "Total" },
            { "EGP",             "EGP" },
            { "ErrGoldWeight",   "Please enter a valid weight." },
            { "ErrGrams",        "Please enter weight in grams." },
            { "ErrGramPositive", "Please enter a valid weight in grams." },
            { "ErrPrice",        "Please enter the gram price." },
            { "ErrMaking",       "Making charge must be a number." },

            { "HistoryTitle",    "Conversion history" },
            { "HistorySub",      "Last 30 conversions performed on the site" },
            { "TagLand",         "Land" },
            { "TagGold",         "Gold" },
            { "NoHistory",       "No conversions yet. Try converting from the Land or Gold page." },
            { "ErrHistory",      "Could not read history from the database." },
        };
    }
}