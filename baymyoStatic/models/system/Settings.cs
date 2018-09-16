using System.Web;

/// <summary>
/// Summary description for Settings
/// </summary>
namespace baymyoStatic
{
    /// <summary>
    /// Uygulamanın kodlama içerisinde kullanılan ayarlarının tanımlandığı sabit özellikleri içerir.
    /// </summary>
    public static class Settings
    {
        /// <summary>
        /// Site ayarlarının tutulduğu özellik.
        /// </summary>
        public static Portal Site
        {
            get
            {
                try
                {
                    if (HttpContext.Current.Cache["PortalSettings"] == null)
                    {
                        HttpContext.Current.Cache.Insert("PortalSettings", PortalMethods.Read(), new System.Web.Caching.CacheDependency(PortalMethods.FilePath), System.DateTime.Now.AddMinutes(60), System.Web.Caching.Cache.NoSlidingExpiration);
                        return HttpContext.Current.Cache["PortalSettings"] as Portal;
                    }
                    else
                        return HttpContext.Current.Cache["PortalSettings"] as Portal;
                }
                catch (System.Exception)
                {
                    return PortalMethods.Read();
                }
            }
        }
        /// <summary>
        /// Kayıt ekranlarındaki form başlık bilgisi için oluşturulan biçim.
        /// </summary>
        public static string FormTitleFormat = "<div class=\"page-header\"><h2><strong>{0}</strong> {1}</h2></div>";
        /// <summary>
        /// Kayıt formlarındaki açıklama alanlarında virgül gösterimlerini kırmızı renkli yapmak için hazırlanan değişken.
        /// </summary>
        public const string SplitFormat = "<span style=\"font-size:12pt;font-weight:bolder;color:#cc0000\">,</span>";
        /// <summary>
        /// Kayıt ekranlarındaki kısayol bağlantılarını içerir.
        /// </summary>
        public const string ShortcutFormat = "<div style=\"margin-top: 5px !important;padding-top: 5px !important;border-top: dashed 1px #c5c5c5;\"><a class=\"toolTip\" title=\"Ekranı temizlemek ve yeni kayıt eklemek için tıklayın!\" href=\"/panel/?go={1}\"><b>Yeni İçerik Ekle</b></a>&nbsp;&nbsp;-&nbsp;&nbsp;<a class=\"toolTip\" title=\"Listeye dönmek için tıklayın.\" href=\"/panel/?go={1}liste\" target=\"_blank\"><b>Kayıt Listesi</b></a>&nbsp;&nbsp;-&nbsp;&nbsp;<a class=\"toolTip\" title=\"Listeye dönmek için tıklayın.\" href=\"{0}\" target=\"_blank\"><b>Önizleme</b></a></div>";
        /// <summary>
        /// Özelleştirilmiş tarih saat kontrolü yolu.
        /// </summary>
        public const string DateTimeControlPath = "~/common/control/DateTimeControl.ascx";
        /// <summary>
        /// Özelleştirilmiş seriler için kayıt kontrolü yolu.
        /// </summary>
        public const string NumbersControlPath = "~/common/control/NumbersControl.ascx";
        /// <summary>
        /// Statik kullanılan ID alanının ön eki ...
        /// </summary>
        public const string StaticPrefix = "static_";
        /// <summary>
        /// Sitenin orjinal bağlantı adresini dönderir.
        /// </summary>
        public static string SiteUrl
        {
            get { return "http://" + HttpContext.Current.Request.Url.Host + "/"; }
        }
        /// <summary>
        /// Site bağlantısı ile beraber fotoğraf klasör yolunu içerir.
        /// </summary>
        public static string SiteImageUrl
        {
            get { return SiteUrl + "common/images/"; }
        }
        /// <summary>
        /// Yorumlar için engellenen argo kelimeler.
        /// </summary>
        public const string InSlangyUrl = ";orospu;amciklar;pezevenk;amcik;anasinisikim;anasini;dolandirici;fahise;gotveren;hirsiz;ibne;kaltak;orospu;pezevenk;pust;sikerim;sikim;sikisken;sokarim;sulalesini;yarak;yarrakkafa;amkafa;amcikkafa;amciksuyu;amsuyu;aminakoyarim;aminakoyum;gottensikim;aminisikim;amcikkafali;amsuyu;aminakoydugum;sikisken;sevisken;senisikim;onusikim;gottensikim;ananinaminisikim;bacinisikim;karinisikim;sulalenisikim;anneannenisikim;annenisikim;sex;seks;seksi;seksseks;sexsex;sexybaby;seksibebek;seksikiz;duldul;dulkari;dulkiz;kizlikzari;kizlikzaripatlak;patlakzar;";
        /// <summary>
        /// Üye olurken engellenen rumuzlar.
        /// </summary>
        public const string InValidUrl = ";turkiyeli;turkluk;turkum;milletim;miliyetci;irkci;irk;istiklal;ataturk;mustafakemalataturk;kemalataturk;mustafaataturk;http;https;www;wwwwww;images;common;adminpanel;adminpanel;administrator;aboutus;logout;register;remember;account;myaccount;password;activation;contact;article;articleadd;articleekle;telefon;mesajlar;alisveris;alisverisler;anasayfa;yonetim;panel;yonetimpanel;yonetimpaneli;iletisim;hakkimizda;makale;makaleekle;makaleadd;yenimakale;makalelerim;makaleler;makalelerim;yenimakale;haberler;mesajlar;sorular;sorduklarim;okuduklarim;yorumlarim;cevaplar;cevaplanlar;cevapladiklarim;";
        /// <summary>
        /// Başlangıç yolu tüm kullanıcı klasör yolları bundan türetilir. Bu yol web.config içerisinde tanımlanır.
        /// </summary>
        public static string VirtualPath
        {
            get { return System.Configuration.ConfigurationManager.AppSettings["VirtualPath"]; }
        }
        /// <summary>
        /// Panel başlangıç yolu tüm klasör yolları bundan türetilir. Bu yol web.config içerisinde tanımlanır.
        /// </summary>
        public static string PanelPath
        {
            get { return System.Configuration.ConfigurationManager.AppSettings["PanelPath"]; }
        }
        /// <summary>
        /// Yönetici işlemlerinin yapıldığı "User Control" dosyaları bu yol altında barınır.
        /// </summary>
        public static string PanelAscxPath
        {
            get { return Settings.PanelPath + "ascx/"; }
        }
        /// <summary>
        /// JavaScript dosyaları bu yol altında barınır.
        /// </summary>
        public static string ScriptPath
        {
            get { return Settings.VirtualPath + "scripts/"; }
        }
        /// <summary>
        /// Ortak klasörlerin yada içeriklerin bulunduğu yol. Bir çok klasör yolu buradan türetilir.
        /// </summary>
        public static string CommonPath
        {
            get { return Settings.VirtualPath + "common/"; }
        }
        /// <summary>
        /// StyleSheet dosyları bu yolda barınır.
        /// </summary>
        public static string CssPath
        {
            get { return Settings.CommonPath + "css/"; }
        }
        /// <summary>
        /// Sistemde kullanılan fotoğraflar bu yol altına barınır.
        /// </summary>
        public static string ImagesPath
        {
            get { return Settings.CommonPath + "images/"; }
        }
        /// <summary>
        /// Kullanıcı tarafından erişilen tüm "User Control" dosyaları bu yol altında barınır.
        /// </summary>
        public static string AscxPath
        {
            get { return Settings.CommonPath + "ascx/"; }
        }
        /// <summary>
        /// HTML gösterim alanları bu yol altında barınır.
        /// </summary>
        public static string ViewPath
        {
            get { return Settings.CommonPath + "views/"; }
        }
        /// <summary>
        /// JSon kayıtları bu yol altında barınır.
        /// </summary>
        public static string JSonPath
        {
            get { return Settings.CommonPath + "json/"; }
        }
        public static string IconsPath
        {
            get { return Settings.ImagesPath + "icons/"; }
        }
        public static string HtmlPath
        {
            get { return Settings.CommonPath + "html/"; }
        }
        /// <summary>
        /// XML kayıtları bu yol atlında barınır.
        /// </summary>
        public static string XmlPath
        {
            get { return Settings.CommonPath + "xml/"; }
        }
        /// <summary>
        /// Servis dosyaları bu yol atlında barınır.
        /// </summary>
        public static string ServicePath
        {
            get { return Settings.CommonPath + "service/"; }
        }
    }
}