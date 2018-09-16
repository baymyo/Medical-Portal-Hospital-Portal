using System;
using System.Collections.Generic;
using System.Web;

namespace baymyoStatic
{
    public class Category
    {
        public string ID { get; set; }
        public string Color { get; set; }
        public string Value { get; set; }
        public bool IsActive { get; set; } = false;
    }

    public class Portal : IDisposable
    {
        #region IDisposable Members
        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }
        #endregion

        public string Title { get; set; }
        public string Description { get; set; }
        public string Keywords { get; set; }
        public string CookieName { get; set; }

        public string ContactName { get; set; }
        public string ContactMail { get; set; }

        public string SmtpMail { get; set; }
        public string SmtpPassword { get; set; }
        public string SmtpHost { get; set; }
        public int SmtpPort { get; set; }
        public bool SmtpEnableSsl { get; set; }

        public string ChangeFreq { get; set; }
        public string Priority { get; set; }

        public string AboutMeLinks { get; set; }
        public string InformationLinks { get; set; }

        public string GoogleMaps { get; set; }
        public string GooglePlusLinks { get; set; }
        public string FaceBookLinks { get; set; }
        public string TwitterLinks { get; set; }
        public string InstagramLinks { get; set; }
        public string YouTubeLinks { get; set; }
        public string FeedBurnerLinks { get; set; }
        public string FaceBookAdminUrl { get; set; }
        public string FaceBookApi { get; set; }
        public bool FaceBookComment { get; set; }

        public string LinkTarget { get; set; }
        public string WheaterCity { get; set; }
        public Category Category1 { get; set; }
        public Category Category2 { get; set; }
        public Category Category3 { get; set; }
        public Category Category4 { get; set; }
        public bool IsAccountMapsVisible { get; set; }
        public CounterViewType CounterView { get; set; }

        public bool IsCategoryColor { get; set; }
        public bool IsAllCategories { get; set; }
        public bool IsAddNews { get; set; }
        public byte IsVideoView { get; set; }

        public bool IsFlashOrder { get; set; }

        public Portal()
        {
            Title = Description = Keywords = ContactName = CookieName = ContactMail = SmtpMail = SmtpPassword = SmtpHost = GoogleMaps = GooglePlusLinks = FaceBookLinks = TwitterLinks = InstagramLinks = YouTubeLinks = FeedBurnerLinks = string.Empty;
            SmtpEnableSsl = FaceBookComment = IsAddNews = IsFlashOrder = false;
            IsVideoView = 0;
            ChangeFreq = "hourly";
            Priority = "0.5";
            InformationLinks = "/kunye-page-5.html";
            AboutMeLinks = "/";
            FaceBookAdminUrl = "https://www.facebook.com/baymyo";
            FaceBookApi = "242442489108173";
            IsCategoryColor = IsAllCategories = IsAccountMapsVisible = true;
            LinkTarget = "_self";
            WheaterCity = "İstanbul;Ankara;Adana;Erzurum";
            Category1 = new Category() { ID = "0002", Color = "#34495e", Value = "Ekonomi" };
            Category2 = new Category() { ID = "0003", Color = "#4DB707", Value = "Spor" };
            Category3 = new Category() { ID = "0004", Color = "#FF3760", Value = "Magazin" };
            Category4 = new Category() { ID = "0009", Color = "#959582", Value = "Sağlık" };
            CounterView = CounterViewType.Single;
        }

        public override string ToString()
        {
            return this.Title;
        }
    }

    public class PortalMethods
    {
        public static string FilePath { get { return HttpContext.Current.Server.MapPath(Settings.ViewPath) + "Settings"; } }

        public static Category GetCategory(string pid)
        {
            Category rv = new Category();
            using (Kategori k = KategoriMethods.GetKategori("haber", pid))
            {
                if (!string.IsNullOrEmpty(k.ID))
                {
                    rv.ID = k.ID;
                    rv.Color = k.Renk;
                    rv.Value = k.Adi;
                    rv.IsActive = true;
                }
                else
                {
                    rv.ID = "00000";
                    rv.Color = rv.Value = string.Empty;
                    rv.IsActive = false;
                }
            }
            return rv;
        }

        public static Portal Read()
        {
            Portal rv = null;
            try
            {
                //if (HttpContext.Current.Cache["siteSettings"] == null)
                using (System.IO.StreamReader sr = new System.IO.StreamReader(FilePath))
                {
                    System.Web.Script.Serialization.JavaScriptSerializer javaScriptSerializer = new System.Web.Script.Serialization.JavaScriptSerializer();
                    rv = javaScriptSerializer.Deserialize<Portal>(sr.ReadToEnd());
                    //HttpContext.Current.Cache["siteSettings"] = rv;
                    sr.Close();
                }
                //else
                //    rv = HttpContext.Current.Cache["siteSettings"] as Portal;
            }
            catch (Exception)
            {
                return new Portal();
            }
            return rv;
        }

        public static bool Save(Portal data)
        {
            try
            {
                if (data != null)
                {
                    System.Web.Script.Serialization.JavaScriptSerializer javaScriptSerializer = new System.Web.Script.Serialization.JavaScriptSerializer();
                    string jsondata = javaScriptSerializer.Serialize(data);
                    if (!string.IsNullOrEmpty(jsondata))
                    {
                        using (System.IO.StreamWriter sw = new System.IO.StreamWriter(FilePath))
                        {
                            sw.Write(jsondata);
                            sw.Close();
                        }
                        return true;
                    }
                }
                return false;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}