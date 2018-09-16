using System;
using System.Security.Principal;
using System.Web;
using System.Web.Security;
using System.Linq;
using System.Collections.Generic;

namespace baymyoStatic
{
    public class Global : System.Web.HttpApplication
    {
        protected void Session_Start(Object sender, EventArgs e)
        {
            try
            {
                //if (!Core.ValidateCode())
                //    Response.Redirect(string.Format("http://www.baymyo.com/?ref=lisans&site={0}", Request.Url.OriginalString), false);
                if (Session["UserInfo"] == null)
                    FormsAuthentication.SignOut();
            }
            catch (Exception)
            {
            }
        }

        protected void Application_Start()
        {
            try
            {
                Application["kategoriler"] = KategoriMethods.Read();
            }
            catch (Exception)
            {
            }
        }

        protected void Application_BeginRequest(Object sender, EventArgs e)
        {
            try
            {
                string pageName = "Default.aspx", virtualPath = "~/Default.aspx", ex = System.IO.Path.GetExtension(Request.Path);
                #region ---Passed---
                switch (ex)
                {
                    case ".js":
                    case ".css":
                    case ".jpg":
                    case ".jpeg":
                    case ".gif":
                    case ".png":
                    case ".ashx":
                    case ".view":
                    case ".xml":
                        return;
                }
                #endregion
                string[] path = Request.Path.Replace(".aspx", "").Replace(".html", "").Split('/');
                if (path.Length > 2)
                    #region ---New URL---
                    switch (path[1])
                    {
                        case "":
                        case "4":
                        case "Settings":
                        case "Categories":
                        case "PortalStyle":
                            break;
                        case "meslekler":
                            Context.RewritePath(virtualPath, "", "go=doktor&type=authours&job=" + path[2], true);
                            break;
                        case "haber":
                            Context.RewritePath(virtualPath, "", "go=habergoster&hid=" + path[2], true);
                            break;
                        case "haberler":
                        case "arsiv":
                        case "news":
                        case "archive":
                            Context.RewritePath(virtualPath, "", "go=haberliste&kid=" + Core.FindCategoryID("haber", path[2]) + "&ctg=" + path[2], true);
                            break;
                        case "sondakika":
                        case "yerel":
                            Context.RewritePath(virtualPath, "", "go=haberyerel&city=" + path[2], true);
                            break;
                        case "etiket":
                        case "tags":
                            Context.RewritePath(virtualPath, "", "go=haberliste&t=" + path[2], true);
                            break;
                        case "anket":
                            Context.RewritePath(virtualPath, "", "go=anketgoster&ankid=" + path[2], true);
                            break;
                        case "makale":
                        case "article":
                            Context.RewritePath(virtualPath, "", "go=makalegoster&mklid=" + path[2], true);
                            break;
                        case "makaleguncelle":
                            Context.RewritePath(virtualPath, "", "go=makale&mklid=" + path[2], true);
                            break;
                        case "makaleetiket":
                            Context.RewritePath(virtualPath, "", "go=makaleliste&t=" + path[2], true);
                            break;
                        case "makaleler":
                        case "articles":
                            Context.RewritePath(virtualPath, "", "go=makaleliste&kid=" + Core.FindCategoryID("makale", path[2]), true);
                            break;
                        case "yazilar":
                        case "writer":
                            Context.RewritePath(virtualPath, "", "go=makaleliste&url=" + path[2], true);
                            break;
                        case "yazar":
                        case "yazarlar":
                        case "profil":
                        case "user":
                        case "authour":
                            Context.RewritePath(virtualPath, "", "go=profil&type=profil&url=" + path[2], true);
                            break;
                        case "hakkinda":
                        case "aboutme":
                            Context.RewritePath(virtualPath, "", "go=profil&type=about&url=" + path[2], true);
                            break;
                        case "iletisim":
                        case "contact":
                            Context.RewritePath(virtualPath, "", "go=profil&type=contact&url=" + path[2], true);
                            break;
                        case "videoguncelle":
                            Context.RewritePath(virtualPath, "", "go=video&vid=" + path[2], true);
                            break;
                        case "video":
                            Context.RewritePath(virtualPath, "", "go=videogoster&mdl=video&vid=" + path[2], true);
                            break;
                        case "videolar":
                            Context.RewritePath(virtualPath, "", "go=videoliste&mdl=video&kid=" + Core.FindCategoryID("video", path[2]), true);
                            break;
                        case "videoetiket":
                            Context.RewritePath(virtualPath, "", "go=videoliste&mdl=video&t=" + path[2], true);
                            break;
                        case "galeri":
                            Context.RewritePath(virtualPath, "", "go=galerigoster&mdl=galeri&raid=" + path[2], true);
                            break;
                        case "galeriler":
                            Context.RewritePath(virtualPath, "", "go=galeriliste&mdl=galeri&kid=" + Core.FindCategoryID("galeri", path[2]), true);
                            break;
                        case "galerietiket":
                            Context.RewritePath(virtualPath, "", "go=galeriliste&mdl=galeri&t=" + path[2], true);
                            break;
                        case "baglanti":
                            Context.RewritePath(virtualPath, "", "go=baglantigoster&fid=" + path[2], true);
                            break;
                        case "baglantiguncelle":
                            Context.RewritePath(virtualPath, "", "go=baglanti&fid=" + path[2], true);
                            break;
                        case "baglantilar":
                            Context.RewritePath(virtualPath, "", "go=baglantiliste&kid=" + Core.FindCategoryID("firma", path[2]), true);
                            break;
                        case "baglantisehir":
                            Context.RewritePath(virtualPath, "", "go=baglantiliste&city=" + path[2], true);
                            break;
                        case "ilan":
                        case "seriilan":
                            Context.RewritePath(virtualPath, "", "go=seriilangoster&srlid=" + path[2], true);
                            break;
                        case "seriilanguncelle":
                            Context.RewritePath(virtualPath, "", "go=seriilan&srlid=" + path[2], true);
                            break;
                        case "ilanlar":
                        case "seriilanlar":
                            Context.RewritePath(virtualPath, "", "go=seriilanliste&kid=" + Core.FindCategoryID("seriilan", path[2]), true);
                            break;
                        case "seriilansehir":
                            Context.RewritePath(virtualPath, "", "go=seriilanliste&city=" + path[2], true);
                            break;
                        case "resmiilan":
                            Context.RewritePath(virtualPath, "", "go=resmiilangoster&rilnid=" + path[2], true);
                            break;
                        case "resmiilansehir":
                            Context.RewritePath(virtualPath, "", "go=resmiilanliste&city=" + path[2], true);
                            break;
                        case "mesaj":
                            Context.RewritePath(virtualPath, "", "go=mesajgoster&mid=" + path[2], true);
                            break;
                        case "mesajliste":
                            Context.RewritePath(virtualPath, "", "go=mesajliste&hspid=" + path[2], true);
                            break;
                        case "mesajyanitla":
                            Context.RewritePath(virtualPath, "", "go=mesaj&mid=" + path[2], true);
                            break;
                        case "astroloji":
                            Context.RewritePath(virtualPath, "", "go=astrolojigoster&astid=" + Core.GetAstrolgyValue(path[2]), true);
                            break;
                        case "sayfa":
                            Context.RewritePath(virtualPath, "", "go=sayfagoster&sid=" + path[2], true);
                            break;
                        case "gazete":
                            Context.RewritePath("~/common/service/ShowPaper.aspx", "", "news=" + path[2], true);
                            break;
                        case "eczane":
                            Context.RewritePath("~/common/service/PharmacyOnDuty.aspx", "", "plaka=" + path[2], true);
                            break;
                        default:
                            return;
                    }
                #endregion
                else
                    switch (ex)
                    {
                        case ".aspx":
                        case ".html":
                            #region ---Settings Link---
                            switch (Request.Path)
                            {
                                case "/kunye.html":
                                case "/info.html":
                                case "/information.html":
                                    path = null;
                                    Context.RewritePath(Settings.VirtualPath + pageName, "", "go=sayfagoster&sid=" + Settings.Site.InformationLinks, true);
                                    return;
                                case "/hakkinda.html":
                                case "/hakkimizda.html":
                                case "/aboutme.html":
                                case "/aboutus.html":
                                    path = null;
                                    Context.RewritePath(Settings.VirtualPath + pageName, "", "go=sayfagoster&sid=" + Settings.Site.AboutMeLinks, true);
                                    return;
                                case "/contact.html":
                                case "/iletisim.html":
                                    path = null;
                                    Context.RewritePath(Settings.VirtualPath + pageName, "", "go=contact", true);
                                    return;
                            }
                            #endregion
                            path = null;
                            break;
                        case "":
                            #region ---URL LINK---
                            string[] url = Request.Path.Replace(".aspx", "").Replace(".html", "").Split('/');
                            if (url.Length == 2)
                            {
                                switch (url[1])
                                {
                                    case "":
                                    case "4":
                                    case "Settings":
                                    case "PortalStyle":
                                    case "Categories":
                                        break;
                                    case "rss":
                                        Context.RewritePath(Settings.VirtualPath + "common/xml/haberrss.xml", "", "", true);
                                        break;
                                    case "0":
                                    case "logout":
                                        Context.RewritePath(Settings.VirtualPath + pageName, "", "go=logout", true);
                                        break;
                                    case "1":
                                    case "login":
                                        Context.RewritePath(Settings.VirtualPath + pageName, "", "go=login", true);
                                        break;
                                    case "2":
                                    case "register":
                                        Context.RewritePath(Settings.VirtualPath + pageName, "", "go=register", true);
                                        break;
                                    case "3p":
                                    case "remember":
                                    case "password":
                                        Context.RewritePath(Settings.VirtualPath + pageName, "", "go=remember&r=sifre", true);
                                        break;
                                    case "3a":
                                    case "activation":
                                        Context.RewritePath(Settings.VirtualPath + pageName, "", "go=remember&r=aktivasyon", true);
                                        break;
                                    case "5":
                                    case "hesabim":
                                    case "account":
                                    case "myaccount":
                                        Context.RewritePath(Settings.VirtualPath + pageName, "", "go=myaccount", true);
                                        break;
                                    case "home":
                                    case "index":
                                    case "anasayfa":
                                        Context.RewritePath(Settings.VirtualPath + pageName, "", "ref=" + url[1], true);
                                        break;
                                    case "contact":
                                    case "iletisim":
                                        Context.RewritePath(Settings.VirtualPath + pageName, "", "go=contact", true);
                                        break;
                                    case "maps":
                                    case "harita":
                                        Context.RewritePath(Settings.VirtualPath + pageName, "", "go=harita", true);
                                        break;
                                    case "habergonder":
                                        Context.RewritePath(Settings.VirtualPath + pageName, "", "go=addnews", true);
                                        break;
                                    case "haber":
                                    case "haberler":
                                    case "news":
                                    case "archive":
                                        Context.RewritePath(Settings.VirtualPath + pageName, "", "go=haberliste", true);
                                        break;
                                    case "anket":
                                    case "anketler":
                                        Context.RewritePath(Settings.VirtualPath + pageName, "", "go=anketliste", true);
                                        break;
                                    case "authours":
                                    case "yazar":
                                    case "yazarlar":
                                    case "doktor":
                                    case "doktorlar":
                                        Context.RewritePath(Settings.VirtualPath + pageName, "", "go=doktor&type=authours", true);
                                        break;
                                    case "yenimakale":
                                        Context.RewritePath(Settings.VirtualPath + pageName, "", "go=makale", true);
                                        break;
                                    case "makalelerim":
                                        Context.RewritePath(Settings.VirtualPath + pageName, "", "go=makaleler", true);
                                        break;
                                    case "makale":
                                    case "makaleler":
                                        Context.RewritePath(Settings.VirtualPath + pageName, "", "go=makaleliste", true);
                                        break;
                                    case "yenivideo":
                                        Context.RewritePath(Settings.VirtualPath + pageName, "", "go=video", true);
                                        break;
                                    case "videolarim":
                                        Context.RewritePath(Settings.VirtualPath + pageName, "", "go=videolar", true);
                                        break;
                                    case "video":
                                    case "videolar":
                                    case "videogaleri":
                                        Context.RewritePath(Settings.VirtualPath + pageName, "", "go=videoliste", true);
                                        break;
                                    case "galeri":
                                    case "galeriler":
                                    case "fotogaleri":
                                        Context.RewritePath(Settings.VirtualPath + pageName, "", "go=galeriliste", true);
                                        break;
                                    case "yenibaglanti":
                                        Context.RewritePath(Settings.VirtualPath + pageName, "", "go=baglanti", true);
                                        break;
                                    case "baglantilarim":
                                        Context.RewritePath(Settings.VirtualPath + pageName, "", "go=baglantilarim", true);
                                        break;
                                    case "baglanti":
                                    case "baglantilar":
                                    case "baglantiliste":
                                        Context.RewritePath(Settings.VirtualPath + pageName, "", "go=baglantiliste", true);
                                        break;
                                    case "yeniilan":
                                        Context.RewritePath(Settings.VirtualPath + pageName, "", "go=seriilan", true);
                                        break;
                                    case "ilanlarim":
                                        Context.RewritePath(Settings.VirtualPath + pageName, "", "go=seriilanlarim", true);
                                        break;
                                    case "ilan":
                                    case "ilanlar":
                                    case "seriilan":
                                    case "seriilanlar":
                                        Context.RewritePath(Settings.VirtualPath + pageName, "", "go=seriilanliste", true);
                                        break;
                                    case "resmiilan":
                                    case "resmiilanlar":
                                        Context.RewritePath(Settings.VirtualPath + pageName, "", "go=resmiilanliste", true);
                                        break;
                                    case "mesajlar":
                                        Context.RewritePath(Settings.VirtualPath + pageName, "", "go=mesajlar", true);
                                        break;
                                    case "soru":
                                    case "sorular":
                                        Context.RewritePath(Settings.VirtualPath + pageName, "", "go=mesajliste", true);
                                        break;
                                    case "sorduklarim":
                                        Context.RewritePath(Settings.VirtualPath + pageName, "", "go=sorduklarim", true);
                                        break;
                                    case "astroloji":
                                        Context.RewritePath(Settings.VirtualPath + pageName, "", "go=astrolojigoster", true);
                                        break;
                                    case "eczaneler":
                                        Context.RewritePath(Settings.VirtualPath + "common/service/PharmacyOnDuty.aspx", "", "plaka=1", true);
                                        break;
                                    case "gazeteler":
                                        Context.RewritePath(Settings.VirtualPath + "common/service/ShowPaper.aspx", "", "news=aksam", true);
                                        break;
                                    case "yorumlar":
                                        Context.RewritePath(Settings.VirtualPath + pageName, "", "go=yorumlar", true);
                                        break;
                                    case "yorumlarim":
                                        Context.RewritePath(Settings.VirtualPath + pageName, "", "go=yorumlarim", true);
                                        break;
                                    case "siteneekle":
                                        Context.RewritePath(Settings.VirtualPath + pageName, "", "go=foryou", true);
                                        break;
                                    case "yerel":
                                        Context.RewritePath(Settings.VirtualPath + pageName, "", "go=haberyerel&city=", true);
                                        break;
                                    case "izmir":
                                    case "adana":
                                    case "hatay":
                                    case "mardin":
                                    case "ankara":
                                    case "antalya":
                                    case "istanbul":
                                    case "diyarbakir":
                                        Context.RewritePath(Settings.VirtualPath + pageName, "", "go=haberyerel&city=" + url[1], true);
                                        break;
                                    case "ataturk":
                                    case "besiktas":
                                    case "fenerbahce":
                                    case "galatasaray":
                                    case "trabzonspor":
                                    case "iskenderun":
                                    case "belen":
                                    case "arsuz":
                                    case "karaagac":
                                    case "akp":
                                    case "chp":
                                    case "mhp":
                                        Context.RewritePath(Settings.VirtualPath + pageName, "", "go=haberliste&t=" + url[1], true);
                                        break;
                                    default:
                                        if (url[1].Length >= 4 & !Settings.InValidUrl.Contains(";" + url[1].ToLower() + ";"))
                                            Context.RewritePath(Settings.VirtualPath + pageName, "", "go=profil&type=profil&url=" + url[1], true);
                                        break;
                                }
                            }
                            url = null;
                            #endregion
                            break;
                    }
            }
            catch (Exception)
            {
            }
        }

        protected void Application_AuthenticateRequest(Object sender, EventArgs e)
        {
            try
            {
                if (HttpContext.Current.User != null)
                    if (HttpContext.Current.User.Identity.IsAuthenticated)
                        if (HttpContext.Current.User.Identity is FormsIdentity)
                        {
                            FormsIdentity id = (FormsIdentity)HttpContext.Current.User.Identity;
                            FormsAuthenticationTicket ticket = id.Ticket;
                            HttpContext.Current.User = new GenericPrincipal(id, ticket.UserData.Split(','));
                        }
            }
            catch (Exception)
            {
            }
        }

        protected void Application_Error(object sender, EventArgs e)
        {
        }
    }
}