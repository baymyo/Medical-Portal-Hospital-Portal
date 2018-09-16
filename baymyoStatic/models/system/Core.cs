using System;
using System.Collections.Generic;
using System.Web;

namespace baymyoStatic
{
    public static class Core
    {
        #region --- User Info ---
        public static Hesap CurrentUser
        {
            get
            {
                if (HttpContext.Current.Session["UserInfo"] != null)
                    return HttpContext.Current.Session["UserInfo"] as Hesap;
                else
                    return new Hesap();
            }
        }
        public static bool IsUserActive
        {
            get
            {
                return (!string.IsNullOrEmpty(CurrentUser.ID) & CurrentUser.Aktif);
            }
        }
        public static bool IsUserAdmin
        {
            get
            {
                switch (Core.CurrentUser.Tipi)
                {
                    case AccountType.Admin:
                    case AccountType.Doctor:
                    case AccountType.Private:
                        return true;
                    default:
                        return false;
                }
            }
        }
        public static string GenerateSecurityCode()
        {
            return Guid.NewGuid().ToString().Replace("-", "").Substring(0, 5);
        }
        //public static string UserLoginUrl(string value)
        //{
        //    switch (value)
        //    {
        //        case "0":
        //        case "logout":
        //            return "\\modul\\default\\logout.ascx";
        //        case "1":
        //        case "login":
        //            return "\\modul\\default\\login.ascx";
        //        case "2":
        //        case "register":
        //            return "\\modul\\default\\register.ascx";
        //        case "3":
        //        case "remember":
        //            return "\\modul\\default\\remember.ascx";
        //        case "4":
        //        case "account":
        //            return "\\modul\\default\\account.ascx";
        //        case "5":
        //        case "myaccount":
        //            return "\\modul\\hesap\\hesap.ascx";
        //        default:
        //            return string.Empty;
        //    }
        //}
        /// <summary>
        /// Guid değerinin null olup olmadığını öğrenmek için hazırlanmıştır.
        /// </summary>
        /// <param name="p">Guid türünden değişken alır.</param>
        /// <returns>Eğer içerisi boş ise "True" değilse "False" döner.</returns>
        public static bool IsNullGuid(Guid p)
        {
            if (p == null)
                return true;
            else if (p.ToString().Equals("00000000-0000-0000-0000-000000000000"))
                return true;

            return false;
        }
        #endregion

        #region --- Data ---
        public static AnketPuan CurrentSurveyVote
        {
            get
            {
                if (HttpContext.Current.Request.Cookies[Settings.Site.CookieName + "_Survey"] != null)
                {
                    HttpCookie c = HttpContext.Current.Request.Cookies[Settings.Site.CookieName + "_Survey"];
                    return new AnketPuan("", "", BAYMYO.UI.Converts.NullToInt64(c["AnketID"]), BAYMYO.UI.Converts.NullToInt64(c["SoruID"]), BAYMYO.UI.Converts.NullToString(c["IP"]));
                }
                return new AnketPuan();
            }
            set
            {
                string cookieName = Settings.Site.CookieName + "_Survey";
            createCookie:
                if (HttpContext.Current.Request.Cookies[cookieName] == null)
                {
                    HttpCookie tempCookie = new HttpCookie(cookieName);
                    tempCookie["AnketID"] = value.AnketID.ToString();
                    tempCookie["SoruID"] = value.SoruID.ToString();
                    tempCookie["IP"] = value.IP;
                    tempCookie.Expires = DateTime.Now.AddDays(30);
                    HttpContext.Current.Response.Cookies.Add(tempCookie);
                }
                else
                {
                    HttpContext.Current.Request.Cookies.Remove(cookieName);
                    goto createCookie;
                }
                cookieName = null;
            }
        }

        public static bool IsParentCategory(string modulID)
        {
            switch (modulID)
            {
                //case "makale":
                //    return true;
                case "haber":
                    return true;
                default:
                    return false;
            }
        }
        public static string DateFormating(object dateTime)
        {
            if (dateTime != null)
            {
                DateTime tempDateTime = BAYMYO.UI.Converts.NullToDateTime(dateTime);
                TimeSpan s = DateTime.Now - tempDateTime;
                if (s.Days > 120)
                    return tempDateTime.ToLongDateString();
                else if (s.Days > 30)
                {
                    int gun = (s.Days % 30);
                    if (gun > 0)
                        return (s.Days / 30) + " ay " + gun + " gün önce";
                    else
                        return (s.Days / 30) + " ay önce";
                }
                else if (s.Days > 0)
                    return s.Days + " gün önce";
                else if (s.Hours > 0)
                    return s.Hours + " saat önce";
                else if (s.Minutes > 0)
                    return s.Minutes + " dk. önce";
                else
                    return s.Seconds + " sn. önce";
            }

            return "<b>Çok Yeni</b>";
        }
        public static void GetProccesList(string modulName, System.Web.UI.WebControls.DropDownList ddl)
        {
            int index = 0;
            ddl.Items.Clear();
            ddl.Items.Insert(index++, "İşlem Seçiniz!");
            switch (modulName)
            {
                case "bakim":
                    ddl.Items.Insert(index++, "Optimize Table");
                    ddl.Items.Insert(index++, "Analyze Table");
                    ddl.Items.Insert(index++, "Check Table");
                    ddl.Items.Insert(index++, "Repair Table");
                    break;
                case "hesap":
                    ddl.Items.Insert(index++, "Yorum yazma aktif");
                    ddl.Items.Insert(index++, "Yorum yazma kililtle");
                    ddl.Items.Insert(index++, "Abonelik aktif olsun");
                    ddl.Items.Insert(index++, "Abonelik pasif olsun");
                    ddl.Items.Insert(index++, "Aktivasyon onayı aktif");
                    ddl.Items.Insert(index++, "Aktivasyon onayı kililtle");
                    ddl.Items.Insert(index++, "Hesab(ları) aktif et");
                    ddl.Items.Insert(index++, "Hesab(ları) kililtle");
                    break;
                case "mesaj":
                    ddl.Items.Insert(index++, "Mesaj(ları) Onayla!");
                    ddl.Items.Insert(index++, "Mesaj(ları) Kilitle!");
                    ddl.Items.Insert(index++, "Mesaj(ları) aktif et");
                    ddl.Items.Insert(index++, "Mesaj(ları) pasif et");
                    break;
                case "sayfa":
                    ddl.Items.Insert(index++, "Sayfa(ları) üst menü'e al");
                    ddl.Items.Insert(index++, "Sayfa(ları) alt menü'e al");
                    ddl.Items.Insert(index++, "Sayfa(ları) sol menü'e al");
                    ddl.Items.Insert(index++, "Sayfa(ları) sağ menü'e al");
                    ddl.Items.Insert(index++, "Sayfa(ları) aktif et");
                    ddl.Items.Insert(index++, "Sayfa(ları) pasif et");
                    break;
                case "yorum":
                    ddl.Items.Insert(index++, "Yorum(ları) Onayla!");
                    ddl.Items.Insert(index++, "Yorum(ları) Kilitle!");
                    ddl.Items.Insert(index++, "Yorum(ları) aktif et");
                    ddl.Items.Insert(index++, "Yorum(ları) pasif et");
                    ddl.Items.Insert(index++, "Yorum(ları) Yayımla!");
                    ddl.Items.Insert(index++, "Yorum(ları) Sil (X)");
                    break;
                case "manset":
                    ddl.Items.Insert(index++, "Manşet(leri) aktif et");
                    ddl.Items.Insert(index++, "Manşet(leri) pasif et");
                    //ddl.Items.Insert(index++, "Manşet(leri) Sil (X)");
                    break;
                case "haber":
                    ddl.Items.Insert(index++, "Haber(leri) Onayla!");
                    ddl.Items.Insert(index++, "Haber(leri) Kilitle!");
                    ddl.Items.Insert(index++, "Haber(leri) aktif et");
                    ddl.Items.Insert(index++, "Haber(leri) pasif et");
                    break;
                case "makale":
                    ddl.Items.Insert(index++, "Makale(leri) Onayla!");
                    ddl.Items.Insert(index++, "Makale(leri) Kilitle!");
                    ddl.Items.Insert(index++, "Makale(leri) aktif et");
                    ddl.Items.Insert(index++, "Makale(leri) pasif et");
                    break;
                case "video":
                    ddl.Items.Insert(index++, "Video(ları) Onayla!");
                    ddl.Items.Insert(index++, "Video(ları) Kilitle!");
                    ddl.Items.Insert(index++, "Video(ları) aktif et");
                    ddl.Items.Insert(index++, "Video(ları) pasif et");
                    break;
                case "album":
                    ddl.Items.Insert(index++, "Albüm(leri) aktif et");
                    ddl.Items.Insert(index++, "Albüm(leri) pasif et");
                    ddl.Items.Insert(index++, "Albüm(leri) Sil (X)");
                    break;
                case "galeri":
                    ddl.Items.Insert(index++, "Resmi Kapak Yap");
                    ddl.Items.Insert(index++, "Resim(leri) Sil (X)");
                    break;
                case "anket":
                    ddl.Items.Insert(index++, "Anket(leri) aktif et");
                    ddl.Items.Insert(index++, "Anket(leri) pasif et");
                    ddl.Items.Insert(index++, "Anket(leri) Sil (X)");
                    break;
                case "resmiilan":
                    ddl.Items.Insert(index++, "Resmi İlan(ları) aktif et");
                    ddl.Items.Insert(index++, "Resmi İlan(ları) pasif et");
                    ddl.Items.Insert(index++, "Resmi İlan(ları) Sil (X)");
                    break;
                case "firma":
                    ddl.Items.Insert(index++, "Firma(ları) Onayla!");
                    ddl.Items.Insert(index++, "Firma(ları) Kilitle!");
                    ddl.Items.Insert(index++, "Firma(ları) aktif et");
                    ddl.Items.Insert(index++, "Firma(ları) pasif et");
                    ddl.Items.Insert(index++, "Firma(ları) Sil (X)");
                    break;
                case "seriilan":
                    ddl.Items.Insert(index++, "Seri İlan(ları) Onayla!");
                    ddl.Items.Insert(index++, "Seri İlan(ları) Kilitle!");
                    ddl.Items.Insert(index++, "Seri İlan(ları) aktif et");
                    ddl.Items.Insert(index++, "Seri İlan(ları) pasif et");
                    ddl.Items.Insert(index++, "Seri İlan(ları) Sil (X)");
                    break;
                case "astroloji":
                    ddl.Items.Insert(index++, "Burç(ları) Onayla!");
                    ddl.Items.Insert(index++, "Burç(ları) Kilitle!");
                    ddl.Items.Insert(index++, "Burç(ları) aktif et");
                    ddl.Items.Insert(index++, "Burç(ları) pasif et");
                    ddl.Items.Insert(index++, "Burç(ları) Sil (X)");
                    break;
                    //case "reklam":
                    //    ddl.Items.Insert(index++, "Reklam(ları) aktif et");
                    //    ddl.Items.Insert(index++, "Reklam(ları) pasif et");
                    //    break;
            }
        }

        public static string GetNobetciEczane(byte sehirid)
        {
            switch (sehirid)
            {
                case 1: return "http://www.adanaeo.org.tr/?pg=nobetci&s=3";
                case 2: return "http://www.adiyamaneo.org.tr/";
                case 3: return "http://afyoneczaciodasi.org.tr/";
                case 4: return "http://www.agrisaglik.gov.tr/tr-TR/AnaSayfa/Oku/agri-merkez-eczaneler";
                case 5: return "http://www.amasyaeo.org.tr/";
                case 6: return "http://eos.aeo.org.tr/PublicSayfalar/NobetAra.aspx";
                case 7: return "http://www.antalyaeo.org.tr/?p=nobetci&s=3";
                case 8: return "http://artvinsaglik.gov.tr/" + DateTime.Now.Year + "/online/nobetcieczaneler/";
                case 9: return "http://www.aydineczaciodasi.org.tr/?pg=nobetci";
                case 10: return "http://www.balikesireczaciodasi.org.tr/";
                case 11: return "http://www.bilecikdh.gov.tr/?sayfa=nobeczane";
                case 12: return "http://www.binsm.gov.tr/index.php/subeler/35";
                case 13: return "http://www.bitlis.bel.tr/yeni/icerik_detay.php?menu=1&kategori=10&altkategori=14&icerik=79";
                case 14: return "http://www.bolusaglik.gov.tr/eczane-iletisim.html";
                case 15: return "http://www.burdureo.org.tr/";
                case 16: return "http://www.beo.org.tr/modules.php?name=nobet2006";
                case 17: return "http://www.canakkaleeo.org.tr/?tur=nobetci";
                case 18: return "http://www.haber18.com/ana/eczane/nobetcieczane.asp";
                case 19: return "http://www.corumeo.org/nobetci_eczane.asp";
                case 20: return "http://www.denizlieczaciodasi.org.tr/";
                case 21: return "http://www.diyarbakireo.org.tr/nobetci_eczane.asp";
                case 22: return "http://www.edirneeo.org.tr/?p=nobetci";
                case 23: return "http://www.elazigeczaciodasi.org.tr/nobetci_eczaneler.asp?AAKat=6";
                case 24: return "http://erzincaneo.org/index.php?option=com_content&view=category&id=84&Itemid=466";
                case 25: return "http://www.erzurumeo.org.tr/";
                case 26: return "http://www.eskisehir26.net/Eskisehir-Nobetci-Eczaneler/";
                case 27: return "http://www.gaziantepeo.org.tr/?p=nobetci";
                case 28: return "http://www.giresunsaglik.gov.tr/yeni/index.php/nobetci-eczaneler/";
                case 29: return "http://www.gumushanesaglik.gov.tr/Nobetci.asp";
                case 30: return "http://www.hsm.gov.tr/index.php?sayfa=sayfa&id=17";
                case 31: return "http://www.hatayeo.org.tr/?p=nobetci";
                case 32: return "http://www.ispartaeo.org.tr/?s=3&pg=nobetci";
                case 33: return "http://www.mersineczaciodasi.org.tr/?pg=nobetci&s=3";
                case 34: return "http://www.istanbuleczaciodasi.org.tr/nobetler.php?t=i&ilceID=30";
                case 35: return "http://www.izmireczaciodasi.org.tr/NobetciEczane.asp";
                case 36: return "http://www.karshsm.gov.tr/halksagligi/viewpage.php?page_id=30";
                case 37: return "http://www.kastamonueo.org/?&Bid=1121686&/N%C3%96BETC%C4%B0-ECZANELER";
                case 38: return "http://www.kayserieo.org.tr/";
                case 39: return "http://www.kirklareli.saglik.gov.tr/nobetcieczane.php";
                case 40: return "http://www.kirsehirhalksagligi.gov.tr/nobetci.asp";
                case 41: return "http://www.kocaelieo.org.tr/?pg=nobetci";
                case 42: return "http://www.keo.org.tr/index.php?option=com_wrapper&Itemid=70";
                case 43: return "http://www.kutahyasaglik.gov.tr/index.php?opt=detay&id=948";
                case 44: return "http://www.malatyaeczaciodasi.org.tr/?s=haberayrinti&haberid=2240";
                case 45: return "http://www.manisahaberleri.com/nobetci-eczaneler.aspx";
                case 46: return "http://kahramanmaraseo.org/";
                case 47: return "http://www.mardineczaciodasi.org.tr/Wts.aspx";
                case 48: return "http://www.muglaeczaciodasi.org.tr/?p=nobetci";
                case 49: return "http://www.mus.gov.tr/nobetcieczaneler.aspx";
                case 50: return "http://www.nevsehireo.org.tr/";
                case 51: return "http://nigde.bel.tr/bildirimler/nobetci-eczane";
                case 52: return "http://www.ordueczaciodasi.org.tr/";
                case 53: return "http://www.rsm.gov.tr/tr/sayfa.php?ID=774";
                case 54: return "http://tr.seo.org.tr/";
                case 55: return "http://www.samsuneczaciodasi.org.tr/?s=3&p=nobetci";
                case 56: return "http://www.siirtsaglik.gov.tr/index.php?option=com_content&view=article&id=174&Itemid=60";
                case 57: return "http://www.sinopadh.gov.tr/NobetciEczaneler.aspx";
                case 58: return "http://sivaseo.org.tr/";
                case 59: return "http://www.teo.org.tr/nobetci_eczaneler.aspx";
                case 60: return "http://www.tokateo.org/infusions/nobetci_eczane_panel/tamliste.php";
                case 61: return "http://www.trabzoneczaciodasi.org.tr/?pg=nobetci";
                case 62: return "http://www.tunceliemek.com.tr/nobetci_ecz1.asp";
                case 63: return "http://www.sanliurfaeo.org.tr/?p=nobetci";
                case 64: return "http://www.usakeczaciodasi.org.tr/index2.php?tur=nobetci";
                case 65: return "http://www.vaneczaciodasi.org.tr/";
                case 66: return "http://www.yozgateo.org.tr/index2.php?tur=nobetci";
                case 67: return "http://www.zonguldakeczaciodasi.org.tr/?p=nobetci";
                case 68: return "http://www.aksarayeo.org/?pg=nobetci&s=3";
                case 69: return "http://www.bayburtsm.gov.tr/Yonetim-file-eczaneliste";
                case 70: return "http://www.karamaneo.org.tr/";
                case 71: return "http://eos.aeo.org.tr/PublicSayfalar/NobetKartBastir.aspx";
                case 72: return "http://www.batmaneczaciodasi.org.tr/nobetci_eczane.asp";
                case 73: return "http://www.sirnakeo.com/sirnakeo/basliklar.asp?anakategori=N%D6BET%C7%DD%20ECZANELER";
                case 74: return "http://www.bartinsaglik.gov.tr/";
                case 75: return "http://www.ardahansm.gov.tr/EtkilesimliTakvim.aspx";
                case 76: return "http://www.igdirdh.gov.tr/icerik.php?cid=86";
                case 77: return "http://yalovasaglik.gov.tr/Icerik/IcerikDetay.aspx?IcerikID=130";
                case 78: return "http://www.karabukhsm.gov.tr/icerik.asp?i_id=208";
                case 79: return "http://www.kilisburda.com/nobetci-eczaneler/";
                case 80: return "http://www.osmaniyeeczaciodasi.org.tr/";
                case 81: return "http://duzceeo.org";
                default: return string.Empty;
            }
        }

        public static System.Data.DataTable GetCHAKategorileri()
        {
            System.Data.DataTable dt = new System.Data.DataTable("MessageStates");
            dt.Columns.Add("Key", typeof(Int16));
            dt.Columns.Add("Value", typeof(string));

            System.Data.DataRow dr = dt.NewRow();
            dr[0] = 1;
            dr[1] = "GÜNCEL";
            dt.Rows.Add(dr);

            dr = dt.NewRow();
            dr[0] = 2;
            dr[1] = "SPOR";
            dt.Rows.Add(dr);

            dr = dt.NewRow();
            dr[0] = 3;
            dr[1] = "EKONOMİ";
            dt.Rows.Add(dr);

            dr = dt.NewRow();
            dr[0] = 4;
            dr[1] = "DÜNYA";
            dt.Rows.Add(dr);

            dr = dt.NewRow();
            dr[0] = 1004;
            dr[1] = "KÜLTÜR SANAT";
            dt.Rows.Add(dr);

            dr = dt.NewRow();
            dr[0] = 1005;
            dr[1] = "POLİTİKA";
            dt.Rows.Add(dr);

            dr = dt.NewRow();
            dr[0] = 12;
            dr[1] = "HEPSİ";
            dt.Rows.Add(dr);

            return dt;
        }
        public static System.Data.DataTable GetCihanHaberAjansi(string rssPath)
        {
            System.Data.DataTable dt = new System.Data.DataTable();
            dt.Columns.Add("id", typeof(int));
            dt.Columns.Add("title");
            dt.Columns.Add("link");
            dt.Columns.Add("pubDate");
            dt.Columns.Add("guid");
            dt.Columns.Add("description");
            dt.Columns.Add("picturelink");
            dt.Columns.Add("pictureview");
            dt.Columns.Add("city");
            try
            {
                using (System.Net.WebClient webClient = new System.Net.WebClient())
                {
                    webClient.Encoding = System.Text.Encoding.Default;
                    using (System.IO.MemoryStream stream = new System.IO.MemoryStream(webClient.DownloadData(rssPath)))
                    {
                        System.Xml.XmlDocument doc = new System.Xml.XmlDocument();
                        doc.Load(stream);

                        System.Xml.XmlNamespaceManager expr = new System.Xml.XmlNamespaceManager(doc.NameTable);
                        expr.AddNamespace("media", "http://search.yahoo.com/mrss/");

                        System.Xml.XmlNode root = doc.SelectSingleNode("//rss/channel/item", expr);
                        System.Xml.XmlNodeList nodeList = root.SelectNodes("//rss/channel/item");

                        int id = 0; object[] values; string city;
                        foreach (System.Xml.XmlNode chileNode in nodeList)
                        {
                            values = new object[9];
                            values[0] = id++;
                            values[1] = chileNode.SelectSingleNode("title").InnerText.Replace('‘', '\'').Replace('’', '\'').Replace('“', '"').Replace('”', '"');
                            values[2] = chileNode.SelectSingleNode("link").InnerText;
                            values[3] = chileNode.SelectSingleNode("pubDate").InnerText;
                            values[4] = chileNode.SelectSingleNode("guid").InnerText;
                            values[5] = chileNode.SelectSingleNode("description").InnerText.Replace('‘', '\'').Replace('’', '\'').Replace('“', '"').Replace('”', '"');
                            foreach (System.Xml.XmlNode item in chileNode.SelectNodes("media:content", expr))
                            {
                                values[6] += ';' + item.Attributes["url"].Value;
                                values[7] += string.Format("<img src=\"{0}\" style=\"width: 32%; float:left;  border: 1px solid #808080; margin-top: 5px; margin-right: 5px;\"/>", item.Attributes["url"].Value);
                            }
                            city = values[5].ToString();
                            values[8] = System.Threading.Thread.CurrentThread.CurrentCulture.TextInfo.ToTitleCase(city.Substring(0, city.IndexOf('(')).Trim().ToLower());
                            city = "";
                            dt.Rows.Add(values);
                        }
                        id = 0;
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dt;
        }

        public static System.Data.DataTable GetIHAKategorileri()
        {
            System.Data.DataTable dt = new System.Data.DataTable("IHA");
            dt.Columns.Add("Key", typeof(Int16));
            dt.Columns.Add("Value", typeof(string));

            System.Data.DataRow dr = dt.NewRow();
            dr[0] = 0;
            dr[1] = "Hepsi";
            dt.Rows.Add(dr);

            dr = dt.NewRow();
            dr[0] = 8;
            dr[1] = "Genel";
            dt.Rows.Add(dr);

            dr = dt.NewRow();
            dr[0] = 10;
            dr[1] = "Gündem";
            dt.Rows.Add(dr);

            dr = dt.NewRow();
            dr[0] = 5;
            dr[1] = "Asayis";
            dt.Rows.Add(dr);

            dr = dt.NewRow();
            dr[0] = 14;
            dr[1] = "Bilim Ve Teknoloji";
            dt.Rows.Add(dr);

            dr = dt.NewRow();
            dr[0] = 16;
            dr[1] = "Çevre";
            dt.Rows.Add(dr);

            dr = dt.NewRow();
            dr[0] = 6;
            dr[1] = "Dünya";
            dt.Rows.Add(dr);

            dr = dt.NewRow();
            dr[0] = 13;
            dr[1] = "Eğitim";
            dt.Rows.Add(dr);

            dr = dt.NewRow();
            dr[0] = 9;
            dr[1] = "Ekonomi";
            dt.Rows.Add(dr);

            dr = dt.NewRow();
            dr[0] = 15;
            dr[1] = "Kültür-Sanat";
            dt.Rows.Add(dr);

            dr = dt.NewRow();
            dr[0] = 2;
            dr[1] = "Magazin";
            dt.Rows.Add(dr);

            dr = dt.NewRow();
            dr[0] = 4;
            dr[1] = "Politika";
            dt.Rows.Add(dr);

            dr = dt.NewRow();
            dr[0] = 12;
            dr[1] = "Sağlık";
            dt.Rows.Add(dr);

            dr = dt.NewRow();
            dr[0] = 3;
            dr[1] = "Spor";
            dt.Rows.Add(dr);

            dr = dt.NewRow();
            dr[0] = 7;
            dr[1] = "Yerel";
            dt.Rows.Add(dr);

            return dt;
        }
        public static System.Data.DataTable GetIhlasHaberAjansi(string rssPath)
        {
            System.Data.DataTable dt = new System.Data.DataTable();
            dt.Columns.Add("id", typeof(int));
            dt.Columns.Add("title");
            dt.Columns.Add("link");
            dt.Columns.Add("pubDate");
            dt.Columns.Add("description");
            dt.Columns.Add("category");
            dt.Columns.Add("picturelink");
            dt.Columns.Add("pictureview");
            dt.Columns.Add("city");
            dt.Columns.Add("subtitle");
            dt.Columns.Add("picturesmall");
            try
            {
                using (System.Net.WebClient webClient = new System.Net.WebClient())
                {
                    webClient.Encoding = System.Text.Encoding.Default;
                    using (System.IO.MemoryStream stream = new System.IO.MemoryStream(webClient.DownloadData(rssPath)))
                    {
                        System.Xml.XmlDocument doc = new System.Xml.XmlDocument();
                        doc.Load(stream);
                        System.Xml.XmlNode root = doc.SelectSingleNode("//rss/channel");
                        System.Xml.XmlNodeList nodeList = root.SelectNodes("//rss/channel/item");
                        System.Xml.XmlNodeList images, smallimg;
                        int id = 0;
                        foreach (System.Xml.XmlNode chileNode in nodeList)
                        {
                            object[] values = new object[11];
                            values[0] = id++;
                            //BAŞLIK
                            values[1] = chileNode.SelectSingleNode("title").InnerText.Replace('‘', '\'').Replace('’', '\'').Replace('“', '"').Replace('”', '"');
                            //BAĞLANTI
                            values[2] = chileNode.SelectSingleNode("link").InnerText;
                            //YAYIN TARİHİ
                            values[3] = chileNode.SelectSingleNode("pubDate").InnerText;
                            //İÇERİK
                            values[4] = chileNode.SelectSingleNode("description").InnerText.Replace('‘', '\'').Replace('’', '\'').Replace('“', '"').Replace('”', '"');
                            //KATEGORİ
                            values[5] = chileNode.SelectSingleNode("Kategori").InnerText;
                            //FOTOĞRAF
                            values[6] = "";
                            values[7] = "";

                            images = chileNode.SelectNodes(".//images/image");
                            smallimg = chileNode.SelectNodes(".//small_images/small_image");
                            for (int i = 0; i < images.Count; i++)
                            {
                                values[6] += ';' + images[i].InnerText;
                                values[10] += ';' + smallimg[i].InnerText;
                                values[7] += string.Format("<a href=\"{0}\" class=\"toolTip\" title=\"{2}. Resim\" target=\"_blank\"><img src=\"{1}\" style=\"float:left;  border: 1px solid #808080; margin-top: 5px; margin-right: 5px;\"/></a>", images[i].InnerText, smallimg[i].InnerText, (i + 1));
                            }
                            images = smallimg = null;

                            //ŞEHİR
                            values[8] = chileNode.SelectSingleNode("Sehir").InnerText;

                            foreach (System.Xml.XmlNode aciklama in chileNode.SelectNodes(".//Aciklamalar/Aciklama"))
                                values[9] = aciklama.InnerText.Replace('‘', '\'').Replace('’', '\'').Replace('“', '"').Replace('”', '"'); ;
                            dt.Rows.Add(values);
                        }
                        id = 0;
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dt;
        }
        public static System.Data.DataTable GetIhlasHaberAjansGaleri(string rssPath)
        {
            System.Data.DataTable dt = new System.Data.DataTable();
            dt.Columns.Add("id", typeof(int));
            dt.Columns.Add("title");
            dt.Columns.Add("link");
            dt.Columns.Add("pubDate");
            dt.Columns.Add("description");
            dt.Columns.Add("category");
            dt.Columns.Add("picturelink");
            dt.Columns.Add("pictureview");
            dt.Columns.Add("city");
            dt.Columns.Add("subtitle");
            dt.Columns.Add("picturesmall");
            try
            {
                using (System.Net.WebClient webClient = new System.Net.WebClient())
                {
                    webClient.Encoding = System.Text.Encoding.Default;
                    using (System.IO.MemoryStream stream = new System.IO.MemoryStream(webClient.DownloadData(rssPath)))
                    {
                        System.Xml.XmlDocument doc = new System.Xml.XmlDocument();
                        doc.Load(stream);
                        System.Xml.XmlNode root = doc.SelectSingleNode("//rss/channel");
                        System.Xml.XmlNodeList nodeList = root.SelectNodes("//rss/channel/item");
                        System.Xml.XmlNodeList images, smallimg;
                        int id = 0;
                        foreach (System.Xml.XmlNode chileNode in nodeList)
                        {
                            images = chileNode.SelectNodes(".//images/image");
                            if (images.Count >= 3)
                            {
                                object[] values = new object[11];

                                values[0] = id++;
                                //BAŞLIK
                                values[1] = chileNode.SelectSingleNode("title").InnerText.Replace('‘', '\'').Replace('’', '\'').Replace('“', '"').Replace('”', '"');
                                //BAĞLANTI
                                values[2] = chileNode.SelectSingleNode("link").InnerText;
                                //YAYIN TARİHİ
                                values[3] = chileNode.SelectSingleNode("pubDate").InnerText;
                                //İÇERİK
                                values[4] = chileNode.SelectSingleNode("description").InnerText.Replace('‘', '\'').Replace('’', '\'').Replace('“', '"').Replace('”', '"');
                                //KATEGORİ
                                values[5] = chileNode.SelectSingleNode("Kategori").InnerText;
                                //FOTOĞRAF
                                values[6] = "";
                                values[7] = "";

                                smallimg = chileNode.SelectNodes(".//small_images/small_image");
                                for (int i = 0; i < images.Count; i++)
                                {
                                    values[6] += ';' + images[i].InnerText;
                                    values[10] += ';' + smallimg[i].InnerText;
                                    values[7] += string.Format("<a href=\"{0}\" class=\"toolTip\" title=\"{2}. Resim\" target=\"_blank\"><img src=\"{1}\" style=\"float:left;  border: 1px solid #808080; margin-top: 5px; margin-right: 5px;\"/></a>", images[i].InnerText, smallimg[i].InnerText, (i + 1));
                                }

                                //ŞEHİR
                                values[8] = chileNode.SelectSingleNode("Sehir").InnerText;

                                foreach (System.Xml.XmlNode aciklama in chileNode.SelectNodes(".//Aciklamalar/Aciklama"))
                                    values[9] = aciklama.InnerText.Replace('‘', '\'').Replace('’', '\'').Replace('“', '"').Replace('”', '"'); ;
                                dt.Rows.Add(values);
                            }
                            images = smallimg = null;
                        }
                        id = 0;
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dt;
        }
        public static System.Data.DataTable GetIhlasHaberAjansiWebTv(string rssPath)
        {
            System.Data.DataTable dt = new System.Data.DataTable();
            dt.Columns.Add("id", typeof(int));
            dt.Columns.Add("title");
            dt.Columns.Add("link");
            dt.Columns.Add("pubDate");
            dt.Columns.Add("description");
            dt.Columns.Add("category");
            dt.Columns.Add("videolink");
            dt.Columns.Add("videoview");
            dt.Columns.Add("city");
            dt.Columns.Add("picturelink");
            try
            {
                using (System.Net.WebClient webClient = new System.Net.WebClient())
                {
                    webClient.Encoding = System.Text.Encoding.Default;
                    using (System.IO.MemoryStream stream = new System.IO.MemoryStream(webClient.DownloadData(rssPath)))
                    {
                        System.Xml.XmlDocument doc = new System.Xml.XmlDocument();
                        doc.Load(stream);
                        System.Xml.XmlNode root = doc.SelectSingleNode("//rss/channel");
                        System.Xml.XmlNodeList nodeList = root.SelectNodes("//rss/channel/item");
                        System.Xml.XmlNodeList videos;
                        int id = 0;
                        foreach (System.Xml.XmlNode chileNode in nodeList)
                        {
                            object[] values = new object[10];
                            values[0] = id++;
                            //BAŞLIK
                            values[1] = chileNode.SelectSingleNode("title").InnerText.Replace('‘', '\'').Replace('’', '\'').Replace('“', '"').Replace('”', '"');
                            //BAĞLANTI
                            values[2] = chileNode.SelectSingleNode("link").InnerText;
                            //YAYIN TARİHİ
                            values[3] = chileNode.SelectSingleNode("pubDate").InnerText;
                            //İÇERİK
                            values[4] = chileNode.SelectSingleNode("description").InnerText.Replace('‘', '\'').Replace('’', '\'').Replace('“', '"').Replace('”', '"');
                            //KATEGORİ
                            values[5] = chileNode.SelectSingleNode("Kategori").InnerText;
                            //FOTOĞRAF
                            values[6] = "";
                            values[7] = "";
                            values[9] = "";
                            videos = chileNode.SelectNodes(".//videos/video");
                            for (int i = 0; i < videos.Count; i++)
                            {
                                values[6] += ';' + videos[i].SelectSingleNode("path").InnerText;
                                values[9] += ';' + videos[i].SelectSingleNode("Path_Gif").InnerText;
                                values[7] += string.Format("<a href=\"{0}\" class=\"toolTip\" title=\"{2}. Video Farklı Kaydet\" target=\"_blank\"><img src=\"{1}\" style=\"float:left;  border: 1px solid #808080; margin-top: 5px; margin-right: 5px;\"/></a>", videos[i].SelectSingleNode("path").InnerText, videos[i].SelectSingleNode("Path_Gif").InnerText, (i + 1));
                            }
                            videos = null;
                            //ŞEHİR
                            values[8] = chileNode.SelectSingleNode("Sehir").InnerText;

                            dt.Rows.Add(values);
                        }
                        id = 0;
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dt;
        }

        public static System.Data.DataTable GetOrderNumbers()
        {
            System.Data.DataTable dt = new System.Data.DataTable("OrderNumbers");
            dt.Columns.Add("Key", typeof(byte));
            dt.Columns.Add("Value", typeof(string));
            System.Data.DataRow dr = dt.NewRow();
            for (int i = 0; i < 25; i++)
            {
                dr = dt.NewRow();
                dr[0] = i.ToString();
                dr[1] = (i + 1) + ". Sıra";
                dt.Rows.Add(dr);
            }
            return dt;
        }
        public static System.Data.DataTable GetOrderNumbers(byte count)
        {
            System.Data.DataTable dt = new System.Data.DataTable("OrderNumbers");
            dt.Columns.Add("Key", typeof(byte));
            dt.Columns.Add("Value", typeof(string));
            System.Data.DataRow dr = dt.NewRow();
            for (int i = 0; i < count; i++)
            {
                dr = dt.NewRow();
                dr[0] = i.ToString();
                dr[1] = (i + 1) + ". Sıra";
                dt.Rows.Add(dr);
            }
            return dt;
        }
        public static System.Data.DataTable GetAdvertTypes()
        {
            System.Data.DataTable dt = new System.Data.DataTable("AdvertTypes");
            dt.Columns.Add("Key", typeof(string));
            dt.Columns.Add("Value", typeof(string));
            System.Data.DataRow dr = dt.NewRow();
            dr[0] = "970x90x1";
            dr[1] = "970x90 Anket Bölümü";

            dt.Rows.Add(dr); dr = dt.NewRow();
            dr[0] = "970x90x2";
            dr[1] = "970x90 Gazeteler";
            dt.Rows.Add(dr);

            dr = dt.NewRow();
            dr[0] = "728x90x1";
            dr[1] = "728x90 Gazeteler";
            dt.Rows.Add(dr);

            dr = dt.NewRow();
            dr[0] = "728x90x2";
            dr[1] = "728x90 Üst Blok";
            dt.Rows.Add(dr);

            dr = dt.NewRow();
            dr[0] = "728x90x3";
            dr[1] = "728x90-1 Orta Blok";
            dt.Rows.Add(dr);

            dr = dt.NewRow();
            dr[0] = "728x90x4";
            dr[1] = "728x90-2 Orta Blok";
            dt.Rows.Add(dr);

            dr = dt.NewRow();
            dr[0] = "728x90x5";
            dr[1] = "728x90-3 Orta Blok";
            dt.Rows.Add(dr);

            dr = dt.NewRow();
            dr[0] = "728x90x6";
            dr[1] = "728x90 Yorum Üstü";
            dt.Rows.Add(dr);

            dr = dt.NewRow();
            dr[0] = "728x90x7";
            dr[1] = "728x90 Haber Kategori Sayfası";
            dt.Rows.Add(dr);

            dr = dt.NewRow();
            dr[0] = "120x600x1";
            dr[1] = "120x600 Sol/Sag Blok";
            dt.Rows.Add(dr);

            dr = dt.NewRow();
            dr[0] = "336x280x1";
            dr[1] = "336x280-1 Sol/Sag Blok";
            dt.Rows.Add(dr);

            dr = dt.NewRow();
            dr[0] = "336x280x2";
            dr[1] = "336x280-2 Sol/Sag Blok";
            dt.Rows.Add(dr);

            dr = dt.NewRow();
            dr[0] = "336x280x3";
            dr[1] = "336x280-3 Sol/Sag Blok";
            dt.Rows.Add(dr);

            dr = dt.NewRow();
            dr[0] = "336x280x4";
            dr[1] = "336x280 Mobil 1";
            dt.Rows.Add(dr);

            dr = dt.NewRow();
            dr[0] = "336x280x5";
            dr[1] = "336x280 Mobil 2";
            dt.Rows.Add(dr);

            dr = dt.NewRow();
            dr[0] = "336x280x6";
            dr[1] = "336x280 Mobil 3";
            dt.Rows.Add(dr);

            dr = dt.NewRow();
            dr[0] = "336x280x7";
            dr[1] = "336x280 Mobil Yorum Üstü";
            dt.Rows.Add(dr);

            //dr = dt.NewRow();
            //dr[0] = "250x250x4";
            //dr[1] = "250x250 İçerik Arası";
            //dt.Rows.Add(dr);

            return dt;
        }
        public static System.Data.DataTable GetLanguages()
        {
            System.Data.DataTable dt = new System.Data.DataTable("Languages");
            dt.Columns.Add("Key", typeof(string));
            dt.Columns.Add("Value", typeof(string));
            System.Data.DataRow dr = dt.NewRow();
            dr[0] = "tr-TR";
            dr[1] = "Türkçe";
            dt.Rows.Add(dr);

            dr = dt.NewRow();
            dr[0] = "en-US";
            dr[1] = "İngilizce";
            dt.Rows.Add(dr);

            dr = dt.NewRow();
            dr[0] = "de-DE";
            dr[1] = "Almanca";
            dt.Rows.Add(dr);

            return dt;
        }
        public static System.Data.DataTable GetFlashLocations()
        {
            System.Data.DataTable dt = new System.Data.DataTable("FlashLocations");
            dt.Columns.Add("Key", typeof(byte));
            dt.Columns.Add("Value", typeof(string));
            dt.Columns.Add("Display", typeof(string));

            System.Data.DataRow dr = dt.NewRow();
            dr[0] = 1;
            dr[1] = "<a class=\"toolTip\" alt=\"Ana Manşet\" title=\"<img src=" + Settings.ImagesPath + "manset-ana.png>\">Ana Manşet</a>";
            dr[2] = "Ana Manşet";
            dt.Rows.Add(dr);

            dr = dt.NewRow();
            dr[0] = 2;
            dr[1] = "<a class=\"toolTip\" alt=\"Sür Manşet\" title=\"<img src=" + Settings.ImagesPath + "manset-sur.png>\">Sür Manşet</a>";
            dr[2] = "Sür Manşet";
            dt.Rows.Add(dr);

            dr = dt.NewRow();
            dr[0] = 3;
            dr[1] = "<a class=\"toolTip\" alt=\"Blok Manşet\" title=\"<img src=" + Settings.ImagesPath + "manset-blok.png>\">Blok Manşet</a>";
            dr[2] = "Blok Manşet";
            dt.Rows.Add(dr);

            dr = dt.NewRow();
            dr[0] = 4;
            dr[1] = "<a class=\"toolTip\" alt=\"Süper Manşet\" title=\"<img src=" + Settings.ImagesPath + "manset-super.png>\">Super Manşet</a>";
            dr[2] = "Süper Manşet";
            dt.Rows.Add(dr);

            dr = dt.NewRow();
            dr[0] = 5;
            dr[1] = "<a class=\"toolTip\" alt=\"Flaş Manşet\" title=\"<img src=" + Settings.ImagesPath + "manset-flas.png>\">Flaş Manşet</a>";
            dr[2] = "Flaş Manşet";
            dt.Rows.Add(dr);

            dr = dt.NewRow();
            dr[0] = 6;
            dr[1] = "<a class=\"toolTip\" alt=\"Haber Manşet\" title=\"<img src=" + Settings.ImagesPath + "manset-icerik.png>\">Haber Manşet</a>";
            dr[2] = "Haber Manşet";
            dt.Rows.Add(dr);

            dr = dt.NewRow();
            dr[0] = 7;
            dr[1] = "<a class=\"toolTip\" alt=\"Galeri Manşet\" title=\"<img src=" + Settings.ImagesPath + "manset-galeri.png>\">Galeri Manşet</a>";
            dr[2] = "Galeri Manşet";
            dt.Rows.Add(dr);

            dr = dt.NewRow();
            dr[0] = 8;
            dr[1] = "<a class=\"toolTip\" alt=\"Video Manşet\" title=\"<img src=" + Settings.ImagesPath + "manset-video.png>\">Video Manşet</a>";
            dr[2] = "Video Manşet";
            dt.Rows.Add(dr);

            dr = dt.NewRow();
            dr[0] = 9;
            dr[1] = "<a class=\"toolTip\" alt=\"Seri İlan Manşet\" title=\"<img src=" + Settings.ImagesPath + "manset-firma-seriilan.png>\">Seri İlan Manşet</a>";
            dr[2] = "Seri İlan Manşet";
            dt.Rows.Add(dr);

            dr = dt.NewRow();
            dr[0] = 10;
            dr[1] = "<a class=\"toolTip\" alt=\"Firma Rehberi Manşet\" title=\"<img src=" + Settings.ImagesPath + "manset-firma-seriilan.png>\">Firma Rehberi Manşet</a>";
            dr[2] = "Firmalar Manşet";
            dt.Rows.Add(dr);

            dr = dt.NewRow();
            dr[0] = 11;
            dr[1] = "<a class=\"toolTip\" alt=\"Kategori 1\" title=\"<img src=" + Settings.ImagesPath + "manset-icerik.png>\">" + Settings.Site.Category1.Value + " Manşet</a>";
            dr[2] = Settings.Site.Category1.Value;
            dt.Rows.Add(dr);

            dr = dt.NewRow();
            dr[0] = 12;
            dr[1] = "<a class=\"toolTip\" alt=\"Kategori 2\" title=\"<img src=" + Settings.ImagesPath + "manset-icerik.png>\">" + Settings.Site.Category2.Value + " Manşet</a>";
            dr[2] = Settings.Site.Category2.Value;
            dt.Rows.Add(dr);

            dr = dt.NewRow();
            dr[0] = 13;
            dr[1] = "<a class=\"toolTip\" alt=\"Kategori 3\" title=\"<img src=" + Settings.ImagesPath + "manset-icerik.png>\">" + Settings.Site.Category3.Value + " Manşet</a>";
            dr[2] = Settings.Site.Category3.Value;
            dt.Rows.Add(dr);

            dr = dt.NewRow();
            dr[0] = 14;
            dr[1] = "<a class=\"toolTip\" alt=\"Kategori 4\" title=\"<img src=" + Settings.ImagesPath + "manset-icerik.png>\">" + Settings.Site.Category4.Value + " Manşet</a>";
            dr[2] = Settings.Site.Category4.Value;
            dt.Rows.Add(dr);

            return dt;
        }
        public static System.Data.DataTable GetMessageStates()
        {
            System.Data.DataTable dt = new System.Data.DataTable("MessageStates");
            dt.Columns.Add("Key", typeof(byte));
            dt.Columns.Add("Value", typeof(string));
            System.Data.DataRow dr = dt.NewRow();
            dr[0] = 1;
            dr[1] = "Okunmadı olarak işaretlendi!";
            dt.Rows.Add(dr);

            dr = dt.NewRow();
            dr[0] = 2;
            dr[1] = "Okundu olarak işaretlendi.";
            dt.Rows.Add(dr);

            return dt;
        }
        public static System.Data.DataTable GetMainViewStates()
        {
            System.Data.DataTable dt = new System.Data.DataTable("MainViewStates");
            dt.Columns.Add("Key", typeof(bool));
            dt.Columns.Add("Value", typeof(string));
            System.Data.DataRow dr = dt.NewRow();
            dr[0] = 0;
            dr[1] = "Gizle, Sadece Kategoriler de gösterilir.";
            dt.Rows.Add(dr);

            dr = dt.NewRow();
            dr[0] = 1;
            dr[1] = "Göster, Tüm sayfalar da gösterilir.";
            dt.Rows.Add(dr);

            return dt;
        }
        public static System.Data.DataTable GetPublishStates()
        {
            System.Data.DataTable dt = new System.Data.DataTable("PublishStates");
            dt.Columns.Add("Key", typeof(bool));
            dt.Columns.Add("Value", typeof(string));
            System.Data.DataRow dr = dt.NewRow();
            dr[0] = 0;
            dr[1] = "Sadece Ben ve Üye";
            dt.Rows.Add(dr);

            dr = dt.NewRow();
            dr[0] = 1;
            dr[1] = "Herkese Açık!";
            dt.Rows.Add(dr);

            return dt;
        }

        public static System.Data.DataTable GetPiyasaBilgileri()
        {
            System.Data.DataTable XMLData = CreatePiyasaTable();
            try
            {
                string path = HttpContext.Current.Server.MapPath(Settings.XmlPath + "piyasalar.xml");
                if (!System.IO.File.Exists(path))
                    XMLData.WriteXml(path);
                XMLData.ReadXml(path);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return XMLData;
        }
        public static System.Data.DataTable CreatePiyasaTable()
        {
            System.Data.DataTable XMLData = new System.Data.DataTable("Piyasalar");
            try
            {
                XMLData.Columns.Add(new System.Data.DataColumn("ID", typeof(int)));
                XMLData.Columns.Add(new System.Data.DataColumn("date", typeof(string)));
                XMLData.Columns.Add(new System.Data.DataColumn("code", typeof(string)));
                XMLData.Columns.Add(new System.Data.DataColumn("lastValue", typeof(string)));
                XMLData.Columns.Add(new System.Data.DataColumn("lastChange", typeof(string)));
                XMLData.Columns.Add(new System.Data.DataColumn("status", typeof(string)));

                XMLData.Columns["ID"].AllowDBNull = false;
                XMLData.Columns["ID"].AutoIncrement = true;
                XMLData.Columns["ID"].AutoIncrementSeed = 1;
                XMLData.Columns["ID"].AutoIncrementStep = 1;
                XMLData.Columns["ID"].Unique = true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return XMLData;
        }

        public static System.Data.DataTable GetHavaDurumlari()
        {
            System.Data.DataTable XMLData = CreateHavaDurumuTable();
            try
            {
                string path = HttpContext.Current.Server.MapPath(Settings.XmlPath + "havadurumu.xml");
                if (!System.IO.File.Exists(path))
                    XMLData.WriteXml(path);
                XMLData.ReadXml(path);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return XMLData;
        }
        public static System.Data.DataTable CreateHavaDurumuTable()
        {
            System.Data.DataTable XMLData = new System.Data.DataTable("XMLHDListe");
            try
            {
                XMLData.Columns.Add(new System.Data.DataColumn("ID", typeof(int)));
                XMLData.Columns.Add(new System.Data.DataColumn("tarih", typeof(string)));
                XMLData.Columns.Add(new System.Data.DataColumn("tarihgelen", typeof(string)));
                XMLData.Columns.Add(new System.Data.DataColumn("sehir", typeof(string)));
                XMLData.Columns.Add(new System.Data.DataColumn("resim", typeof(string)));
                XMLData.Columns.Add(new System.Data.DataColumn("bilgi", typeof(string)));
                XMLData.Columns.Add(new System.Data.DataColumn("endusuk", typeof(string)));
                XMLData.Columns.Add(new System.Data.DataColumn("enyuksek", typeof(string)));

                XMLData.Columns["ID"].AllowDBNull = false;
                XMLData.Columns["ID"].AutoIncrement = true;
                XMLData.Columns["ID"].AutoIncrementSeed = 1;
                XMLData.Columns["ID"].AutoIncrementStep = 1;
                XMLData.Columns["ID"].Unique = true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return XMLData;
        }

        public static System.Data.DataTable GetPuanDurumlari()
        {
            System.Data.DataTable XMLData = CreatePuanDurumuTable();
            try
            {
                string path = HttpContext.Current.Server.MapPath(Settings.XmlPath + "puandurumu.xml");
                if (!System.IO.File.Exists(path))
                    XMLData.WriteXml(path);
                XMLData.ReadXml(path);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return XMLData;
        }
        public static System.Data.DataTable CreatePuanDurumuTable()
        {
            System.Data.DataTable XMLData = new System.Data.DataTable("XMLPNDListe");
            try
            {
                XMLData.Columns.Add(new System.Data.DataColumn("ID", typeof(int)));
                XMLData.Columns.Add(new System.Data.DataColumn("durum", typeof(string)));
                XMLData.Columns.Add(new System.Data.DataColumn("sira", typeof(string)));
                XMLData.Columns.Add(new System.Data.DataColumn("takim", typeof(string)));
                XMLData.Columns.Add(new System.Data.DataColumn("o", typeof(string)));
                XMLData.Columns.Add(new System.Data.DataColumn("g", typeof(string)));
                XMLData.Columns.Add(new System.Data.DataColumn("b", typeof(string)));
                XMLData.Columns.Add(new System.Data.DataColumn("m", typeof(string)));
                XMLData.Columns.Add(new System.Data.DataColumn("a", typeof(string)));
                XMLData.Columns.Add(new System.Data.DataColumn("y", typeof(string)));
                XMLData.Columns.Add(new System.Data.DataColumn("ay", typeof(string)));
                XMLData.Columns.Add(new System.Data.DataColumn("p", typeof(string)));

                XMLData.Columns["ID"].AllowDBNull = false;
                XMLData.Columns["ID"].AutoIncrement = true;
                XMLData.Columns["ID"].AutoIncrementSeed = 1;
                XMLData.Columns["ID"].AutoIncrementStep = 1;
                XMLData.Columns["ID"].Unique = true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return XMLData;
        }

        public static System.Data.DataTable GetFikstur()
        {
            System.Data.DataTable XMLData = CreateFiksturTable();
            try
            {
                string path = HttpContext.Current.Server.MapPath(Settings.XmlPath + "fikstur.xml");
                if (!System.IO.File.Exists(path))
                    XMLData.WriteXml(path);
                XMLData.ReadXml(path);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return XMLData;
        }
        public static System.Data.DataTable CreateFiksturTable()
        {
            System.Data.DataTable XMLData = new System.Data.DataTable("XMLFIXListe");
            try
            {
                XMLData.Columns.Add(new System.Data.DataColumn("ID", typeof(int)));
                XMLData.Columns.Add(new System.Data.DataColumn("hafta", typeof(string)));
                XMLData.Columns.Add(new System.Data.DataColumn("tarih", typeof(string)));
                XMLData.Columns.Add(new System.Data.DataColumn("takim1", typeof(string)));
                XMLData.Columns.Add(new System.Data.DataColumn("skor1", typeof(string)));
                XMLData.Columns.Add(new System.Data.DataColumn("skor2", typeof(string)));
                XMLData.Columns.Add(new System.Data.DataColumn("takim2", typeof(string)));

                XMLData.Columns["ID"].AllowDBNull = false;
                XMLData.Columns["ID"].AutoIncrement = true;
                XMLData.Columns["ID"].AutoIncrementSeed = 1;
                XMLData.Columns["ID"].AutoIncrementStep = 1;
                XMLData.Columns["ID"].Unique = true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return XMLData;
        }

        public static string GetAstrolgyTitle(object value)
        {
            string format = "<b>{0}</b> Burcu Günlük Yorumu";
            switch (BAYMYO.UI.Converts.NullToByte(value))
            {
                case 1:
                    return string.Format(format, "Koç");
                case 2:
                    return string.Format(format, "Boğa");
                case 3:
                    return string.Format(format, "İkizler");
                case 4:
                    return string.Format(format, "Yengeç");
                case 5:
                    return string.Format(format, "Aslan");
                case 6:
                    return string.Format(format, "Başak");
                case 7:
                    return string.Format(format, "Terazi");
                case 8:
                    return string.Format(format, "Akrep");
                case 9:
                    return string.Format(format, "Yay");
                case 10:
                    return string.Format(format, "Oğlak");
                case 11:
                    return string.Format(format, "Kova");
                case 12:
                    return string.Format(format, "Balık");
                default:
                    return "Belirtilmedi";
            }
        }
        public static string GetAstrolgyName(object value)
        {
            switch (BAYMYO.UI.Converts.NullToByte(value))
            {
                case 1:
                    return "Koç";
                case 2:
                    return "Boğa";
                case 3:
                    return "İkizler";
                case 4:
                    return "Yengeç";
                case 5:
                    return "Aslan";
                case 6:
                    return "Başak";
                case 7:
                    return "Terazi";
                case 8:
                    return "Akrep";
                case 9:
                    return "Yay";
                case 10:
                    return "Oğlak";
                case 11:
                    return "Kova";
                case 12:
                    return "Balık";
                default:
                    return "Belirtilmedi";
            }
        }
        public static byte GetAstrolgyValue(string value)
        {
            switch (value)
            {
                case "koc":
                    return 1;
                case "boga":
                    return 2;
                case "ikizler":
                    return 3;
                case "yengec":
                    return 4;
                case "aslan":
                    return 5;
                case "basak":
                    return 6;
                case "terazi":
                    return 7;
                case "akrep":
                    return 8;
                case "yay":
                    return 9;
                case "oglak":
                    return 10;
                case "kova":
                    return 11;
                case "balik":
                    return 12;
                default:
                    return 1;
            }
        }
        public static System.Data.DataTable GetAstrolgy()
        {
            System.Data.DataTable dt = new System.Data.DataTable("Astrolgy");
            dt.Columns.Add("Key", typeof(byte));
            dt.Columns.Add("Value", typeof(string));
            dt.Columns.Add("Desc", typeof(string));
            System.Data.DataRow dr = dt.NewRow();
            dr[0] = 1;
            dr[1] = "Koç";
            dr[2] = "21 Mart - 20 Nisan";
            dt.Rows.Add(dr);

            dr = dt.NewRow();
            dr[0] = 2;
            dr[1] = "Boğa";
            dr[2] = "21 Nisan - 21 Mayıs";
            dt.Rows.Add(dr);

            dr = dt.NewRow();
            dr[0] = 3;
            dr[1] = "İkizler";
            dr[2] = "22 Mayıs - 21 Haziran";
            dt.Rows.Add(dr);

            dr = dt.NewRow();
            dr[0] = 4;
            dr[1] = "Yengeç";
            dr[2] = "22 Haziran - 23 Temmuz";
            dt.Rows.Add(dr);

            dr = dt.NewRow();
            dr[0] = 5;
            dr[1] = "Aslan";
            dr[2] = "24 Temmuz - 23 Ağustos";
            dt.Rows.Add(dr);

            dr = dt.NewRow();
            dr[0] = 6;
            dr[1] = "Başak";
            dr[2] = "24 Ağustos - 23 Eylül";
            dt.Rows.Add(dr);

            dr = dt.NewRow();
            dr[0] = 7;
            dr[1] = "Terazi";
            dr[2] = "24 Eylül - 23 Ekim";
            dt.Rows.Add(dr);

            dr = dt.NewRow();
            dr[0] = 8;
            dr[1] = "Akrep";
            dr[2] = "24 Ekim - 22 Kasım";
            dt.Rows.Add(dr);

            dr = dt.NewRow();
            dr[0] = 9;
            dr[1] = "Yay";
            dr[2] = "23 Kasım - 22 Aralık";
            dt.Rows.Add(dr);

            dr = dt.NewRow();
            dr[0] = 10;
            dr[1] = "Oğlak";
            dr[2] = "23 Aralık - 20 Ocak";
            dt.Rows.Add(dr);

            dr = dt.NewRow();
            dr[0] = 11;
            dr[1] = "Kova";
            dr[2] = "21 Ocak - 19 Şubat";
            dt.Rows.Add(dr);

            dr = dt.NewRow();
            dr[0] = 12;
            dr[1] = "Balık";
            dr[2] = "20 Şubat - 20 Mart";
            dt.Rows.Add(dr);

            return dt;
        }

        public static System.Data.DataTable GetCategoryMenuTypes()
        {
            System.Data.DataTable dt = new System.Data.DataTable("CategoryMenuTypes");
            dt.Columns.Add("Key", typeof(byte));
            dt.Columns.Add("Value", typeof(string));
            System.Data.DataRow dr = dt.NewRow();
            dr[0] = 0;
            dr[1] = "Kategori Ana Menü'de GİZLE!";
            dt.Rows.Add(dr);

            dr = dt.NewRow();
            dr[0] = 1;
            dr[1] = "Kategori Ana Menü'de Gösterilir.";
            dt.Rows.Add(dr);

            dr = dt.NewRow();
            dr[0] = 2;
            dr[1] = "Kategori Açılır Menü'de Gösterilir.";
            dt.Rows.Add(dr);

            return dt;
        }

        public static string GetMenuType(byte id)
        {
            switch (id)
            {
                case 1:
                    return "Üst Menü";
                case 2:
                    return "Alt Menü";
                case 3:
                    return "Sol Menü";
                case 4:
                    return "Sağ Menü";
                case 5:
                    return "Tüm Menüler";
                default:
                    return "Seçiniz";
            }
        }
        public static System.Data.DataTable GetMenuTypes()
        {
            System.Data.DataTable dt = new System.Data.DataTable("PageMenuTypes");
            dt.Columns.Add("Key", typeof(byte));
            dt.Columns.Add("Value", typeof(string));
            System.Data.DataRow dr = dt.NewRow();
            dr[0] = 0;
            dr[1] = "<Seçiniz>";
            dt.Rows.Add(dr);

            dr = dt.NewRow();
            dr[0] = 1;
            dr[1] = "Üst Menü";
            dt.Rows.Add(dr);

            dr = dt.NewRow();
            dr[0] = 2;
            dr[1] = "Alt Menü";
            dt.Rows.Add(dr);

            dr = dt.NewRow();
            dr[0] = 3;
            dr[1] = "Sol Menü";
            dt.Rows.Add(dr);

            dr = dt.NewRow();
            dr[0] = 4;
            dr[1] = "Sağ Menü";
            dt.Rows.Add(dr);

            dr = dt.NewRow();
            dr[0] = 5;
            dr[1] = "Tüm Menüler";
            dt.Rows.Add(dr);

            return dt;
        }

        public static SexType GetSexType(byte id)
        {
            switch (id)
            {
                case 1:
                    return SexType.Erkek;
                case 2:
                    return SexType.Bayan;
                default:
                    return SexType.Belirtilmedi;
            }
        }
        public static System.Data.DataTable GetSexTypes()
        {
            System.Data.DataTable dt = new System.Data.DataTable("SexTypes");
            dt.Columns.Add("Key", typeof(byte));
            dt.Columns.Add("Value", typeof(string));
            System.Data.DataRow dr = dt.NewRow();
            dr[0] = 0;
            dr[1] = "Belirtilmedi";
            dt.Rows.Add(dr);

            dr = dt.NewRow();
            dr[0] = 1;
            dr[1] = "Erkek";
            dt.Rows.Add(dr);

            dr = dt.NewRow();
            dr[0] = 2;
            dr[1] = "Bayan";
            dt.Rows.Add(dr);

            return dt;
        }

        public static AccountType GetAccountType(byte id)
        {
            switch (id)
            {
                case 1:
                    return AccountType.Admin;
                case 2:
                    return AccountType.Doctor;
                case 3:
                    return AccountType.Editor;
                case 4:
                    return AccountType.Standart;
                case 5:
                    return AccountType.Private;
                default:
                    return AccountType.None;
            }
        }
        public static string GetAccountTypeName(object value)
        {
            switch (BAYMYO.UI.Converts.NullToByte(value))
            {
                case 1:
                    return "admin";
                case 2:
                    return "moderator";
                case 3:
                    return "editor";
                case 5:
                    return "private";
                default:
                    return "standart";
            }
        }
        public static System.Data.DataTable GetAccountTypes()
        {
            System.Data.DataTable dt = new System.Data.DataTable("AccountTypes");
            dt.Columns.Add("Key", typeof(byte));
            dt.Columns.Add("Value", typeof(string));
            System.Data.DataRow dr = dt.NewRow();
            dr[0] = 0;
            dr[1] = "<Seçiniz>";
            dt.Rows.Add(dr);

            dr = dt.NewRow();
            dr[0] = 1;
            dr[1] = "Admin";
            dt.Rows.Add(dr);

            dr = dt.NewRow();
            dr[0] = 2;
            dr[1] = "Doktor";
            dt.Rows.Add(dr);

            dr = dt.NewRow();
            dr[0] = 3;
            dr[1] = "Editor";
            dt.Rows.Add(dr);

            dr = dt.NewRow();
            dr[0] = 4;
            dr[1] = "Standart";
            dt.Rows.Add(dr);

            dr = dt.NewRow();
            dr[0] = 5;
            dr[1] = "Private";
            dt.Rows.Add(dr);

            return dt;
        }

        public static string GetAdvertFromName(object value)
        {
            switch (BAYMYO.UI.Converts.NullToByte(value))
            {
                case 1:
                    return "Sahibinden";
                case 2:
                    return "Dükkandan";
                default:
                    return "Belirtilmedi";
            }
        }
        public static System.Data.DataTable GetAdvertFroms()
        {
            System.Data.DataTable dt = new System.Data.DataTable("AdvertFrom");
            dt.Columns.Add("Key", typeof(byte));
            dt.Columns.Add("Value", typeof(string));
            System.Data.DataRow dr = dt.NewRow();
            dr[0] = 0;
            dr[1] = "Hiçbiri";
            dt.Rows.Add(dr);

            dr = dt.NewRow();
            dr[0] = 1;
            dr[1] = "Sahibinden";
            dt.Rows.Add(dr);

            dr = dt.NewRow();
            dr[0] = 2;
            dr[1] = "Dükkandan";
            dt.Rows.Add(dr);
            return dt;
        }
        #endregion

        #region --- Image/Icons ---
        public static string GetModulIcon(object obj)
        {
            switch (obj.ToString())
            {
                case "":
                case "0":
                    return "";
                default:
                    return "<img class=\"toolTip\" alt=\"" + obj + "\" title=\"" + obj + "\" src=\"" + Settings.IconsPath + "32/admin-" + obj + ".png\"/>";
            }
        }
        public static string StatePhotoGaleri(object obj)
        {
            switch (obj.ToString())
            {
                case "":
                case "0":
                    return "";
                default:
                    return "<img width=\"17\" height=\"13\" class=\"toolTip\" alt=\"Fotoğraf Galeri\" title=\"Fotoğraf Galeri\" src=\"" + Settings.ImagesPath + "photo.png\"/>";
            }
        }
        public static string StateVideoGaleri(object obj)
        {
            switch (obj.ToString())
            {
                case "":
                case "0":
                    return "";
                default:
                    return "<img width=\"15\" height=\"13\" class=\"toolTip\" alt=\"Video Galeri\" title=\"Video Galeri\" src=\"" + Settings.ImagesPath + "camera.png\"/>";
            }
        }
        public static string StateActivation(object obj)
        {
            switch (obj.ToString())
            {
                case "1":
                case "True":
                case "true":
                    return "<img class=\"toolTip\" alt=\"Mail ile aktivasyon işlemi gerçekleştirildi.\" title=\"Mail ile aktivasyon işlemi gerçekleştirildi.\" src=\"" + Settings.IconsPath + "admin/address.png\"/>";
                default:
                    return "<img class=\"toolTip\" alt=\"Mail ile aktivasyon bekleniyor!\" title=\"Mail ile aktivasyon bekleniyor!\" src=\"" + Settings.IconsPath + "admin/address-lock.png\"/>";
            }
        }
        public static string StateFollowMe(object obj)
        {
            switch (obj.ToString())
            {
                case "1":
                case "True":
                case "true":
                    return "<img class=\"toolTip\" alt=\"Abonelik durumu aktif.\" title=\"Abonelik durumu aktif.\" src=\"" + Settings.IconsPath + "admin/email.png\"/>";
                default:
                    return "<img class=\"toolTip\" alt=\"Abonelik durumu pasif.\" title=\"Abonelik durumu pasif!\" src=\"" + Settings.IconsPath + "admin/email-lock.png\"/>";
            }
        }
        public static string StateComment(object obj)
        {
            switch (obj.ToString())
            {
                case "1":
                case "True":
                case "true":
                    return "<img class=\"toolTip\" alt=\"Yorum yazabilir\" title=\"Yorum yazabilir\" src=\"" + Settings.IconsPath + "admin/comment.png\"/>";
                default:
                    return "<img class=\"toolTip\" alt=\"Kilitlendi\" title=\"Kilitlendi!\" src=\"" + Settings.IconsPath + "admin/lock.png\"/>";
            }
        }
        public static string StateContent(object obj)
        {
            switch (obj.ToString())
            {
                case "1":
                case "True":
                case "true":
                    return "<img class=\"toolTip\" alt=\"Aktif\" title=\"Aktif\" src=\"" + Settings.IconsPath + "admin/check.png\"/>";
                default:
                    return "<img class=\"toolTip\" alt=\"Kilitlendi!\" title=\"Kilitlendi!\" src=\"" + Settings.IconsPath + "admin/lock.png\"/>";
            }
        }
        public static string StateVideo(object obj)
        {
            switch (obj.ToString())
            {
                case "1":
                case "True":
                case "true":
                    return "<img class=\"toolTip\" alt=\"Videolu İçerik.\" title=\"Videolu İçerik.\" src=\"" + Settings.IconsPath + "admin/video.png\"/>";
                default:
                    return "<img class=\"toolTip\" alt=\"Video Yok!\" title=\"Video Yok!\" src=\"" + Settings.IconsPath + "admin/video-lock.png\"/>";
            }
        }
        public static string StatePhoto(object obj)
        {
            switch (obj.ToString())
            {
                case "1":
                case "True":
                case "true":
                    return "<img class=\"toolTip\" alt=\"Video var\" title=\"Videolu İçerik.\" src=\"" + Settings.IconsPath + "admin/photo.png\"/>";
                default:
                    return "<img class=\"toolTip\" alt=\"Video yok\" title=\"Video Yok!\" src=\"" + Settings.IconsPath + "admin/photo-lock.png\"/>";
            }
        }
        public static string StateView(object obj)
        {
            switch (obj.ToString())
            {
                case "1":
                case "True":
                case "true":
                    return "<img class=\"toolTip\" alt=\"Herkese Açık\" title=\"Herkese Açık!\" src=\"" + Settings.IconsPath + "12/global.png\"/>";
                default:
                    return "<img class=\"toolTip\" alt=\"Kilitlendi\" title=\"Kilitlendi!\" src=\"" + Settings.IconsPath + "12/content-0.png\"/>";
            }
        }
        public static string StateArrow(object obj, byte size)
        {
            switch (obj.ToString())
            {
                case "1":
                case "up":
                    return string.Format("<img width=\"{0}\" height=\"{0}\" class=\"toolTip\" alt=\"Yükseldi\" title=\"Yükseldi\" src=\"" + Settings.IconsPath + "{0}/arrow_upper.png\"/>", size);
                case "2":
                case "down":
                    return string.Format("<img width=\"{0}\" height=\"{0}\" class=\"toolTip\" alt=\"Düşüşte\" title=\"Düşüşte\" src=\"" + Settings.IconsPath + "{0}/arrow_down.png\"/>", size); ;
                default:
                    return string.Format("<img width=\"{0}\" height=\"{0}\" class=\"toolTip\" alt=\"Değişmedi\" title=\"Değişmedi\" src=\"" + Settings.IconsPath + "{0}/dot.png\"/>", size); ;
            }
        }
        public static string StateAdmin(object obj)
        {
            switch (obj.ToString())
            {
                case "1":
                case "True":
                case "true":
                    return "<img class=\"toolTip\" alt=\"Yönetici Onaylı İçerik.\" title=\"Yönetici Onaylı İçerik.\" src=\"" + Settings.IconsPath + "admin/onay.png\"/>";
                default:
                    return "<img class=\"toolTip\" alt=\"Yönetici Onayı Bekliyor.\" title=\"Yönetici Onayı Bekliyor!\" src=\"" + Settings.IconsPath + "admin/onay-lock.png\"/>";
            }
        }
        public static string StateAdmin(object obj, byte px)
        {
            switch (obj.ToString())
            {
                case "1":
                case "True":
                case "true":
                    return "<img class=\"toolTip\" alt=\"Yönetici Onaylı İçerik.\" title=\"Yönetici Onaylı İçerik.\" src=\"" + Settings.IconsPath + px + "/editor.png\"/>";
                default:
                    return "<img class=\"toolTip\" alt=\"Yönetici Onayı Bekliyor!\" title=\"Yönetici Onayı Bekliyor!\" src=\"" + Settings.IconsPath + px + "/editor-lock.png\"/>";
            }
        }
        #endregion

        #region --- Other Method ---
        public static string Compute(string textToHash)
        {
            System.Security.Cryptography.SHA1CryptoServiceProvider cmpt = new System.Security.Cryptography.SHA1CryptoServiceProvider();
            byte[] byteH = cmpt.ComputeHash(System.Text.Encoding.UTF8.GetBytes(textToHash));
            cmpt.Clear();
            return Convert.ToBase64String(byteH);
        }

        public static bool IsMobileBrowser()
        {
            HttpContext context = HttpContext.Current;
            if (context.Request.Browser.IsMobileDevice)
                return true;
            if (context.Request.ServerVariables["HTTP_X_WAP_PROFILE"] != null)
                return true;
            if (context.Request.ServerVariables["HTTP_ACCEPT"] != null && context.Request.ServerVariables["HTTP_ACCEPT"].ToLower().Contains("wap"))
                return true;
            if (context.Request.ServerVariables["HTTP_USER_AGENT"] != null)
            {
                string user_agent = context.Request.ServerVariables["HTTP_USER_AGENT"];
                if (user_agent.Contains("windows") && !user_agent.Contains("windows ce"))
                    return false;
                string pattern = "up.browser|up.link|windows ce|iphone|android|iemobile|mini|mmp|symbian|midp|wap|phone|pocket|mobile|pda|psp";
                System.Text.RegularExpressions.MatchCollection mc = System.Text.RegularExpressions.Regex.Matches(user_agent, pattern, System.Text.RegularExpressions.RegexOptions.IgnoreCase);
                if (mc.Count > 0)
                    return true;
                string popUA = "|acs-|alav|alca|amoi|audi|aste|avan|benq|bird|blac|blaz|brew|cell|cldc|cmd-|dang|doco|eric|hipt|inno|ipaq|java|jigs|kddi|keji|leno|lg-c|lg-d|lg-g|lge-|maui|maxo|midp|mits|mmef|mobi|mot-|moto|mwbp|nec-|newt|noki|opwv|palm|pana|pant|pdxg|phil|play|pluc|port|prox|qtek|qwap|sage|sams|sany|sch-|sec-|send|seri|sgh-|shar|sie-|siem|smal|smar|sony|sph-|symb|t-mo|teli|tim-|tosh|tsm-|upg1|upsi|vk-v|voda|w3c |wap-|wapa|wapi|wapp|wapr|webc|winw|winw|xda|xda-|";
                if (popUA.Contains("|" + user_agent.Substring(0, 4) + "|"))
                    return true;
                user_agent = pattern = popUA = null;
            }
            return false;
        }

        public static List<LinkItem> LinkFinder(string file)
        {
            List<LinkItem> list = new List<LinkItem>();
            System.Text.RegularExpressions.MatchCollection m1 = System.Text.RegularExpressions.Regex.Matches(file, @"(<a.*?>.*?</a>)", System.Text.RegularExpressions.RegexOptions.Singleline);
            foreach (System.Text.RegularExpressions.Match m in m1)
            {
                string value = m.Groups[1].Value;
                LinkItem i = new LinkItem();
                System.Text.RegularExpressions.Match m2 = System.Text.RegularExpressions.Regex.Match(value, @"href=\""(.*?)\""", System.Text.RegularExpressions.RegexOptions.Singleline);
                if (m2.Success)
                    i.Href = m2.Groups[1].Value;
                i.Text = System.Text.RegularExpressions.Regex.Replace(value, @"\s*<.*?>\s*", "", System.Text.RegularExpressions.RegexOptions.Singleline);
                list.Add(i);
            }
            return list;
        }
        public static string CreateLink(string modulName, object icerikID, object baslik)
        {
            try
            {
                string linkFormat = "{0}{1}/{2}/{3}.html", categoryFormat = "{0}{1}/{3}/";
                switch (modulName)
                {
                    case "videoetiket":
                    case "galerietiket":
                    case "makaleetiket":
                    case "haberetiket":
                        return string.Format(categoryFormat, "", modulName.Replace("haber", ""), "",
                            BAYMYO.UI.Commons.ClearInvalidCharacter(Core.ReplaceToLover(BAYMYO.UI.Converts.NullToString(baslik))
                            .Replace('?', '-').Replace('*', '-').Replace('+', '-').Replace('“', '-').Replace('”', '-').Replace('‘', '-').Replace('’', '-').Replace('\'', '-').Replace('.', '-'))).Replace("---", "-").Replace("--", "-");
                }
                string tempBaslik = Core.ReplaceToLover(BAYMYO.UI.Converts.NullToString(baslik));
                tempBaslik = BAYMYO.UI.Commons.ClearInvalidCharacter(tempBaslik).Replace("---", "-").Replace("--", "-");
                switch (modulName)
                {
                    case "diger":
                        return "";
                    case "astroloji":
                        return string.Format(categoryFormat, Settings.VirtualPath, "astroloji", icerikID, tempBaslik);
                    case "habersehir":
                        return string.Format(categoryFormat, Settings.VirtualPath, "sondakika", icerikID, tempBaslik);
                    case "haberyerel":
                        return string.Format(categoryFormat, Settings.VirtualPath, "yerel", icerikID, tempBaslik);
                    case "haberkategori":
                        return string.Format(categoryFormat, Settings.VirtualPath, "haberler", icerikID, tempBaslik);
                    case "haberliste":
                        return string.Format(categoryFormat, Settings.VirtualPath, "arsiv", icerikID, tempBaslik);
                    case "meslekkategori":
                        return string.Format(categoryFormat, Settings.VirtualPath, "meslekler", icerikID, tempBaslik);
                    case "makalekategori":
                        return string.Format(categoryFormat, Settings.VirtualPath, "makaleler", icerikID, tempBaslik);
                    case "videokategori":
                        return string.Format(categoryFormat, Settings.VirtualPath, "videolar", icerikID, tempBaslik);
                    case "firmakategori":
                        return string.Format(categoryFormat, Settings.VirtualPath, "firmalar", icerikID, tempBaslik);
                    case "seriilankategori":
                        return string.Format(categoryFormat, Settings.VirtualPath, "ilanlar", icerikID, tempBaslik);
                    case "galerikategori":
                        return string.Format(categoryFormat, Settings.VirtualPath, "galeriler", icerikID, tempBaslik);
                    case "profil":
                        return string.Format(categoryFormat, Settings.VirtualPath, "yazarlar", "", icerikID);
                    case "yazarhakkinda":
                        return string.Format(categoryFormat, Settings.VirtualPath, "hakkinda", "", icerikID);
                    case "makaleyazar":
                        return string.Format(categoryFormat, Settings.VirtualPath, "yazilar", "", icerikID);
                    case "iletisim":
                        return string.Format(categoryFormat, Settings.VirtualPath, "iletisim", "", icerikID);
                    case "firmasehir":
                    case "seriilansehir":
                    case "resmiilansehir":
                        return string.Format(categoryFormat, Settings.VirtualPath, modulName, "", Core.ReplaceToLover(icerikID.ToString()));
                    case "gazete":
                        return string.Format(categoryFormat, Settings.VirtualPath, modulName, "", icerikID);
                    case "seriilan":
                        return string.Format(linkFormat, Settings.VirtualPath, "ilan", icerikID, tempBaslik);
                    case "haber":
                    case "anket":
                    case "sayfa":
                    case "eczane":
                    case "resmiilan":
                    case "galeri":
                    case "video":
                    case "videoguncelle":
                    case "makale":
                    case "makaleguncelle":
                    case "firma":
                    case "firmaguncelle":
                    case "seriilanguncelle":
                    case "mesaj":
                    case "mesajyanitla":
                    case "mesajliste":
                    case "baglanti":
                        return string.Format(linkFormat, Settings.VirtualPath, modulName, icerikID, tempBaslik);
                    default:
                        return "#";
                }
            }
            catch (Exception)
            {
                return "#";
            }
        }
        public static string FindCategoryID(string modulName, string id)
        {
            try
            {
                return (HttpContext.Current.Application["kategoriler"] as Dictionary<string, object>)[modulName + "-" + id].ToString();
            }
            catch (Exception)
            {

                return "0";
            }
        }

        static void CreateContentMAP(string modulName)
        {
            try
            {
                string siteUrl = Settings.SiteUrl.Remove(Settings.SiteUrl.LastIndexOf('/'), 1);
                System.Text.StringBuilder siteMap = new System.Text.StringBuilder();
                siteMap.Append("<?xml version=\"1.0\" encoding=\"UTF-8\"?> ");
                siteMap.Append("<urlset xmlns=\"http://www.sitemaps.org/schemas/sitemap/0.9\" xmlns:xsi=\"http://www.w3.org/2001/XMLSchema-instance\" xsi:schemaLocation=\"http://www.sitemaps.org/schemas/sitemap/0.9 http://www.sitemaps.org/schemas/sitemap/0.9/sitemap.xsd\"> ");
                siteMap.AppendFormat("<url><loc>{0}</loc><lastmod>{1:yyyy-MM-dd}</lastmod><changefreq>" + Settings.Site.ChangeFreq + "</changefreq><priority>" + Settings.Site.Priority + "</priority></url>", Settings.SiteUrl, DateTime.Now);
                string siteMapFormat = "<url><loc>" + siteUrl + "{0}</loc><lastmod>{1:yyyy-MM-dd}</lastmod><changefreq>" + Settings.Site.ChangeFreq + "</changefreq><priority>" + Settings.Site.Priority + "</priority></url>";
                switch (modulName)
                {
                    case "haber":
                        System.Text.StringBuilder newsMap = new System.Text.StringBuilder();
                        newsMap.Append("<?xml version=\"1.0\" encoding=\"UTF-8\"?> ");
                        newsMap.Append("<urlset xmlns=\"http://www.sitemaps.org/schemas/sitemap/0.9\" xmlns:news=\"http://www.google.com/schemas/sitemap-news/0.9\"> ");
                        string googleNewsFormat = "<url><loc>" + siteUrl + "{0}</loc><news:news><news:publication><news:name>{1}</news:name><news:language>tr</news:language></news:publication><news:genres>PressRelease, Blog</news:genres><news:publication_date>{2:yyyy-MM-ddTHH:mm:ss+01:00}</news:publication_date><news:title>{3}</news:title><news:geo_locations>{4}, Türkiye</news:geo_locations><news:keywords>{5}</news:keywords></news:news></url>";
                        System.Text.StringBuilder yandexMap = new System.Text.StringBuilder();
                        string yandexRss = "", yandexItem = "", newsLink = "";
                        using (System.Data.DataTable table = new System.Data.DataTable("haber"))
                        {
                            using (BAYMYO.UI.Web.CustomSqlQuery data = new BAYMYO.UI.Web.CustomSqlQuery(table, "SELECT h.resimurl,h.baslik, h.ozet,k.adi,CONCAT(hs.adi,' ',hs.soyadi) yazar, h.kayittarihi,h.sehir,h.etiket,h.id FROM haber h inner join kategori k on k.modulid='haber' and k.id=h.kategoriid INNER JOIN hesap hs on hs.id=h.hesapid where h.yoneticionay=1 and h.aktif=1 order by h.kayittarihi desc LIMIT 30"))
                            {
                                data.Execute();
                                yandexRss = BAYMYO.UI.FileIO.ReadText(HttpContext.Current.Server.MapPath(Settings.ViewPath) + "yandexhead.view");
                                yandexRss = string.Format(yandexRss, Settings.Site.Title, siteUrl, Settings.Site.Description, siteUrl + Settings.ImagesPath + "logo.png");
                                yandexItem = BAYMYO.UI.FileIO.ReadText(HttpContext.Current.Server.MapPath(Settings.ViewPath) + "yandexitem.view");
                                foreach (System.Data.DataRow item in table.Rows)
                                {
                                    newsLink = Core.CreateLink(modulName, item[8], item[1]);
                                    siteMap.AppendFormat(siteMapFormat, newsLink, item[5]);
                                    newsMap.AppendFormat(googleNewsFormat, newsLink, Settings.Site.Title, item[5], item[1], item[6], item[7]);
                                    yandexMap.AppendFormat(yandexItem, item[0], item[1], item[2], item[3], item[4], item[5], siteUrl, newsLink);
                                }
                                yandexRss = yandexRss.Replace("%ITEMS%", yandexMap.ToString());
                            }
                            newsMap.Append(" </urlset>");
                            BAYMYO.UI.FileIO.WriteText(HttpContext.Current.Server.MapPath(Settings.XmlPath) + "googlenews.xml", newsMap.ToString(), System.Text.Encoding.UTF8);
                            BAYMYO.UI.FileIO.WriteText(HttpContext.Current.Server.MapPath(Settings.XmlPath) + "yandexnews.xml", yandexRss, System.Text.Encoding.UTF8);
                            newsMap.Clear();
                            yandexMap.Clear();
                            yandexRss = yandexItem = null;
                        }
                        break;
                    case "makale":
                        using (MakaleCollection articles = MakaleMethods.GetList(System.Data.CommandType.Text, "select * from makale m where m.yoneticionay=1 and m.aktif=1 order by m.kayittarihi desc limit 25", null))
                        {
                            foreach (Makale m in articles)
                                siteMap.AppendFormat(siteMapFormat, Core.CreateLink(modulName, m.ID, m.Baslik), m.GuncellemeTarihi);
                        }
                        break;
                    case "video":
                        using (VideoCollection videos = VideoMethods.GetList(System.Data.CommandType.Text, "select * from video v where v.yoneticionay=1 and v.aktif=1 order by v.kayittarihi desc limit 25", null))
                        {
                            foreach (Video m in videos)
                                siteMap.AppendFormat(siteMapFormat, Core.CreateLink(modulName, m.ID, m.Baslik), m.GuncellemeTarihi);
                        }
                        break;
                    case "galeri":
                        using (AlbumCollection albums = AlbumMethods.GetList(System.Data.CommandType.Text, "select * from album a where a.yoneticionay=1 and a.aktif=1 order by a.kayittarihi desc limit 25", null))
                        {
                            foreach (Album m in albums)
                                siteMap.AppendFormat(siteMapFormat, Core.CreateLink(modulName, m.ID, m.Adi), m.GuncellemeTarihi);
                        }
                        break;
                    case "firma":
                        using (FirmaCollection bussines = FirmaMethods.GetList(System.Data.CommandType.Text, "select * from firma f where f.yoneticionay=1 and f.aktif=1 order by f.kayittarihi desc limit 25", null))
                        {
                            foreach (Firma m in bussines)
                                siteMap.AppendFormat(siteMapFormat, Core.CreateLink(modulName, m.ID, m.Adi), m.GuncellemeTarihi);
                        }
                        break;
                    case "seriilan":
                        using (SeriIlanCollection classifields = SeriIlanMethods.GetList(System.Data.CommandType.Text, "select * from seriilan s where s.yoneticionay=1 and s.aktif=1 order by s.kayittarihi desc limit 25", null))
                        {
                            foreach (SeriIlan m in classifields)
                                siteMap.AppendFormat(siteMapFormat, Core.CreateLink(modulName, m.ID, m.Baslik), m.GuncellemeTarihi);
                        }
                        break;
                }
                siteMap.Append(" </urlset>");
                BAYMYO.UI.FileIO.WriteText(HttpContext.Current.Server.MapPath(Settings.XmlPath) + modulName + "map.xml", siteMap.ToString(), System.Text.Encoding.UTF8);
                siteMap.Clear();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        static void CreateContentRSS(string modulName)
        {
            try
            {
                string siteUrl = Settings.SiteUrl.Remove(Settings.SiteUrl.LastIndexOf('/'), 1);
                System.Text.StringBuilder sbRSSListe = new System.Text.StringBuilder();
                sbRSSListe.Append("<rss version=\"2.0\">");
                sbRSSListe.Append("<channel>");
                sbRSSListe.Append("<title>" + Settings.Site.Title + " " + modulName + " Bölümü RSS</title>");
                sbRSSListe.Append("<link>" + Settings.SiteUrl + "</link>");
                sbRSSListe.Append("<description>" + modulName + " bölümü rss içeriği güncel konuları buradan takip edebilirsiniz.</description>");
                sbRSSListe.Append("<language>tr-TR</language>");
                string siteRssFormat = "<item><pubdate>{0}</pubdate><author>{1}</author><category>{2}</category><title>{3}</title><link>" + siteUrl + "{4}</link><description><![CDATA[ {5} ]]></description></item>";
                Kategori k = new Kategori();
                switch (modulName)
                {
                    case "haber":
                        using (HaberCollection haberler = HaberMethods.GetList(System.Data.CommandType.Text, "select * from haber h where h.yoneticionay=1 and h.aktif=1 order by h.kayittarihi desc limit 30", null))
                        {
                            foreach (Haber m in haberler)
                            {
                                if (k.ID != m.KategoriID)
                                    k = KategoriMethods.GetKategori(modulName, m.KategoriID);
                                sbRSSListe.AppendFormat(siteRssFormat, m.GuncellemeTarihi, "Editor", k.Adi, m.Baslik, Core.CreateLink(modulName, m.ID, m.Baslik), m.Ozet);
                            }
                        }
                        break;
                    case "makale":
                        using (HesapCollection hesaplar = HesapMethods.GetList(System.Data.CommandType.Text, "select * from hesap where tipi in(1,2,5) limit 30", null))
                        {
                            foreach (Hesap h in hesaplar)
                            {
                                using (Makale m = MakaleMethods.GetMakale(h.ID))
                                {
                                    if (m.ID > 0)
                                    {
                                        if (k.ID != m.KategoriID)
                                            k = KategoriMethods.GetKategori(modulName, m.KategoriID);
                                        sbRSSListe.AppendFormat(siteRssFormat, m.GuncellemeTarihi, h.Adi + " " + h.Soyadi, k.Adi, m.Baslik, Core.CreateLink(modulName, m.ID, m.Baslik), m.Ozet);
                                    }
                                }
                            }
                        }
                        break;
                    case "video":
                        using (VideoCollection videos = VideoMethods.GetList(System.Data.CommandType.Text, "select * from video v where v.yoneticionay=1 and v.aktif=1 order by v.guncellemetarihi desc limit 25", null))
                        {
                            foreach (Video m in videos)
                            {
                                if (k.ID != m.KategoriID)
                                    k = KategoriMethods.GetKategori(modulName, m.KategoriID);
                                sbRSSListe.AppendFormat(siteRssFormat, m.GuncellemeTarihi, "Editor", k.Adi, m.Baslik, Core.CreateLink(modulName, m.ID, m.Baslik), "Bu video ve bu kategoride daha fazlasını görüntülemek için tıklayın.");
                            }
                        }
                        break;
                    case "galeri":
                        using (AlbumCollection albums = AlbumMethods.GetList(System.Data.CommandType.Text, "select * from album a where a.yoneticionay=1 and a.aktif=1 order by a.guncellemetarihi desc limit 25", null))
                        {
                            foreach (Album m in albums)
                            {
                                if (k.ID != m.KategoriID)
                                    k = KategoriMethods.GetKategori(modulName, m.KategoriID);
                                sbRSSListe.AppendFormat(siteRssFormat, m.GuncellemeTarihi, "Editor", k.Adi, m.Adi, Core.CreateLink(modulName, m.ID, m.Adi), "Bu albümün fotoğraflarını ve bu kategoride daha fazlasını görüntülemek için tıklayın.");
                            }

                        }
                        break;
                    case "firma":
                        using (FirmaCollection bussines = FirmaMethods.GetList(System.Data.CommandType.Text, "select * from firma f where f.yoneticionay=1 and f.aktif=1 order by f.guncellemetarihi desc limit 25", null))
                        {
                            foreach (Firma m in bussines)
                            {
                                if (k.ID != m.KategoriID)
                                    k = KategoriMethods.GetKategori(modulName, m.KategoriID);
                                sbRSSListe.AppendFormat(siteRssFormat, m.GuncellemeTarihi, "Editor", k.Adi, m.Adi, Core.CreateLink(modulName, m.ID, m.Adi), "Bu firma hakkında bilgi ve adresini görüntülemek için tıklayın.");
                            }
                        }
                        break;
                    case "seriilan":
                        using (SeriIlanCollection classifields = SeriIlanMethods.GetList(System.Data.CommandType.Text, "select * from seriilan s where s.yoneticionay=1 and s.aktif=1 order by s.guncellemetarihi desc limit 25", null))
                        {
                            foreach (SeriIlan m in classifields)
                            {
                                if (k.ID != m.KategoriID)
                                    k = KategoriMethods.GetKategori(modulName, m.KategoriID);
                                sbRSSListe.AppendFormat(siteRssFormat, m.GuncellemeTarihi, "Editor", k.Adi, m.Baslik, Core.CreateLink(modulName, m.ID, m.Baslik), "Bu seri ilan hakkında bilgi ve detayları görüntülemek için tıklayın.");
                            }
                        }
                        break;
                }
                sbRSSListe.Append("</channel>");
                sbRSSListe.Append("</rss>");
                BAYMYO.UI.FileIO.WriteText(HttpContext.Current.Server.MapPath(Settings.XmlPath) + modulName + "rss.xml", sbRSSListe.ToString(), System.Text.Encoding.UTF8);
                sbRSSListe.Clear();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public static void CreateContents(string modulName)
        {
            try
            {
                // MAPS OLUŞTURMA İŞLEMİ
                CreateContentMAP(modulName);
                // RSS OLUŞTURMA İŞLEMİ
                CreateContentRSS(modulName);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public static void CreateCategoryMaps(string modulName)
        {
            try
            {
                List<Kategori> tumKategoriler, anaKategoriler, altKategoriler_1, altKategoriler_2, altKategoriler_3, altKategoriler_4;
                tumKategoriler = KategoriMethods.GetMenu(modulName, true);
                anaKategoriler = tumKategoriler.FindAll(delegate (Kategori p) { return (p.ParentID == "0"); });
                System.Text.StringBuilder maps = new System.Text.StringBuilder();
                string tempModulName = modulName, siteUrl = Settings.SiteUrl.Remove(Settings.SiteUrl.LastIndexOf('/'), 1), altParent_1 = string.Empty, altParent_2 = string.Empty, altParent_3 = string.Empty, altParent_4 = string.Empty;
                string mainFormat = "<url><loc>" + siteUrl + "{0}</loc><lastmod>{1:yyyy-MM-dd}</lastmod><changefreq>" + Settings.Site.ChangeFreq + "</changefreq><priority>" + Settings.Site.Priority + "</priority></url>",
                    parentFormat = string.Empty,
                    descFormat = string.Empty;
                // GOOGLE SITEMAPS
                maps.Append("<?xml version=\"1.0\" encoding=\"UTF-8\"?> ");
                maps.Append("<urlset xmlns=\"http://www.sitemaps.org/schemas/sitemap/0.9\" xmlns:xsi=\"http://www.w3.org/2001/XMLSchema-instance\" xsi:schemaLocation=\"http://www.sitemaps.org/schemas/sitemap/0.9 http://www.sitemaps.org/schemas/sitemap/0.9/sitemap.xsd\"> ");
                maps.AppendFormat("<url><loc>{0}</loc><lastmod>{1:yyyy-MM-dd}</lastmod><changefreq>" + Settings.Site.ChangeFreq + "</changefreq><priority>" + Settings.Site.Priority + "</priority></url>", Settings.SiteUrl, DateTime.Now);
                modulName += "kategori";
                switch (modulName)
                {
                    case "haberkategori":
                        foreach (Kategori ustK in anaKategoriler)
                            maps.AppendFormat(mainFormat, Core.CreateLink("haberliste", ustK.ID, ustK.Adi), DateTime.Now);
                        break;
                    default:
                        foreach (Kategori ustK in anaKategoriler)
                            maps.AppendFormat(mainFormat, Core.CreateLink(modulName, ustK.ID, ustK.Adi), DateTime.Now);
                        break;
                }
                maps.Append(" </urlset>");
                switch (modulName)
                {
                    default:
                        BAYMYO.UI.FileIO.WriteText(HttpContext.Current.Server.MapPath(Settings.XmlPath) + modulName + ".xml", maps.ToString(), System.Text.Encoding.UTF8);
                        break;
                }
                maps.Clear();
                // ASP.NET SITEMAPS
                switch (tempModulName)
                {
                    case "haber":
                        altParent_1 = altParent_2 = altParent_3 = altParent_4 = string.Empty;
                        mainFormat = "<siteMapNode url=\"{0}\" title=\"{1}\" description=\"{2}\">{3}</siteMapNode>";
                        parentFormat = "<siteMapNode url=\"{0}\" title=\"{1}\" description=\"{2}\" />";
                        descFormat = " hakkındaki içeriklere erişebilmek için tıklayınız.";
                        maps.Append("<?xml version=\"1.0\" encoding=\"utf-8\" ?>");
                        maps.Append("<siteMap xmlns=\"http://schemas.microsoft.com/AspNet/SiteMap-File-1.0\" >");
                        maps.AppendFormat("<siteMapNode url=\"{0}\" title=\"{1}\" description=\"{2}\">", Settings.SiteUrl, "Anasayfa", Settings.Site.Description);
                        foreach (Kategori ustK in anaKategoriler)
                        {
                            altKategoriler_1 = tumKategoriler.FindAll(delegate (Kategori p) { return (p.ParentID == ustK.ID); });
                            if (altKategoriler_1.Count > 0)
                            {
                                foreach (Kategori altK_1 in altKategoriler_1)
                                {
                                    altKategoriler_2 = tumKategoriler.FindAll(delegate (Kategori p) { return (p.ParentID == altK_1.ID); });
                                    foreach (Kategori altK_2 in altKategoriler_2)
                                    {
                                        altKategoriler_3 = tumKategoriler.FindAll(delegate (Kategori p) { return (p.ParentID == altK_2.ID); });
                                        foreach (Kategori altK_3 in altKategoriler_3)
                                        {
                                            altKategoriler_4 = tumKategoriler.FindAll(delegate (Kategori p) { return (p.ParentID == altK_3.ID); });
                                            foreach (Kategori altK_4 in altKategoriler_4)
                                                altParent_4 += string.Format(parentFormat, Core.CreateLink(modulName, altK_4.ID, altK_4.Adi), altK_4.Adi, altK_4.Adi + descFormat);

                                            if (!string.IsNullOrEmpty(altParent_4))
                                                altParent_3 += string.Format(mainFormat, Core.CreateLink(modulName, altK_3.ID, altK_3.Adi), altK_3.Adi, altK_3.Adi + descFormat, altParent_4);
                                            else
                                                altParent_3 += string.Format(parentFormat, Core.CreateLink(modulName, altK_3.ID, altK_3.Adi), altK_3.Adi, altK_3.Adi + descFormat);
                                            altParent_4 = string.Empty;
                                        }

                                        if (!string.IsNullOrEmpty(altParent_3))
                                            altParent_2 += string.Format(mainFormat, Core.CreateLink(modulName, altK_2.ID, altK_2.Adi), altK_2.Adi, altK_2.Adi + descFormat, altParent_3);
                                        else
                                            altParent_2 += string.Format(parentFormat, Core.CreateLink(modulName, altK_2.ID, altK_2.Adi), altK_2.Adi, altK_2.Adi + descFormat);
                                        altParent_3 = string.Empty;
                                    }

                                    if (!string.IsNullOrEmpty(altParent_2))
                                        altParent_1 += string.Format(mainFormat, Core.CreateLink(modulName, altK_1.ID, altK_1.Adi), altK_1.Adi, altK_1.Adi + descFormat, altParent_2);
                                    else
                                        altParent_1 += string.Format(parentFormat, Core.CreateLink(modulName, altK_1.ID, altK_1.Adi), altK_1.Adi, altK_1.Adi + descFormat);
                                    altParent_2 = string.Empty;
                                }

                                if (!string.IsNullOrEmpty(altParent_1))
                                    maps.AppendFormat(mainFormat, Core.CreateLink(modulName, ustK.ID, ustK.Adi), ustK.Adi, ustK.Adi + descFormat, altParent_1);
                                else
                                    maps.AppendFormat(parentFormat, Core.CreateLink(modulName, ustK.ID, ustK.Adi), ustK.Adi, ustK.Adi + descFormat);
                                altParent_1 = string.Empty;
                            }
                            else
                                maps.AppendFormat(parentFormat, Core.CreateLink(modulName, ustK.ID, ustK.Adi), ustK.Adi, ustK.Adi + descFormat);
                        }
                        maps.Append("</siteMapNode>");
                        maps.Append("</siteMap>");
                        BAYMYO.UI.FileIO.WriteText(HttpContext.Current.Server.MapPath(Settings.VirtualPath) + "Web.sitemap", maps.ToString(), System.Text.Encoding.UTF8);
                        maps.Clear();
                        mainFormat = parentFormat = descFormat = null;
                        tempModulName = modulName = altParent_1 = altParent_2 = altParent_3 = altParent_4 = null;
                        tumKategoriler = anaKategoriler = altKategoriler_1 = altKategoriler_2 = altKategoriler_3 = altKategoriler_4 = null;
                        break;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static void ViewCounter(string modulName, object contentID)
        {
            if (BAYMYO.UI.Converts.NullToDateTime(HttpContext.Current.Cache[modulName + "Sleep"]) <= DateTime.Now)
                using (Gosterim g = new Gosterim())
                {
                    g.IP = HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"].ToString();
                    g.ModulID = modulName;
                    g.HesapID = BAYMYO.UI.Converts.NullToString(Core.CurrentUser.ID);
                    g.IcerikID = BAYMYO.UI.Converts.NullToString(contentID);
                    g.KayitTarihi = DateTime.Now;
                    GosterimMethods.Insert(g);
                    HttpContext.Current.Cache[modulName + "Sleep"] = DateTime.Now.AddMinutes(3);
                }
        }
        public static bool RemoveForeignKey(string modulName, string icerikID)
        {
            using (Manset m = MansetMethods.GetManset(icerikID))
            {
                if (m != null)
                {
                    string removeUrl = HttpContext.Current.Server.MapPath(Settings.ImagesPath + "manset/" + m.ModulID + "/");
                    BAYMYO.UI.FileIO.Remove(removeUrl + m.ResimBuyuk);
                    MansetMethods.Delete(m);
                }
            }
            Delete("yorum", modulName, icerikID);
            Delete("gosterim", modulName, icerikID);
            return true;
        }

        public static string ReplaceToLover(string p)
        {
            return p.ToLower()
                .Replace('ş', 's')
                .Replace('ç', 'c')
                .Replace('ı', 'i')
                .Replace('ğ', 'g')
                .Replace('ö', 'o')
                .Replace('ü', 'u');
        }

        public static void ClearMetaTags(System.Web.UI.Page p)
        {
            for (int i = 0; i < p.Header.Controls.Count; i++)
                if (p.Header.Controls[i] is System.Web.UI.HtmlControls.HtmlMeta)
                    p.Header.Controls.RemoveAt(i);
        }
        public static void ClearControls(SortedDictionary<string, System.Web.UI.Control> controls)
        {
            foreach (string item in controls.Keys)
            {
                if (controls[item] is System.Web.UI.WebControls.TextBox)
                    ((System.Web.UI.WebControls.TextBox)controls[item]).Text = string.Empty;
                if (controls[item] is CKEditor.NET.CKEditorControl)
                    ((CKEditor.NET.CKEditorControl)controls[item]).Text = string.Empty;
                else if (controls[item] is System.Web.UI.WebControls.DropDownList)
                    if (((System.Web.UI.WebControls.DropDownList)controls[item]).Items.Count > 0)
                        ((System.Web.UI.WebControls.DropDownList)controls[item]).SelectedIndex = 0;
            }
        }

        public static int Update(string query)
        {
            int rowsAffected = 0;
            using (BAYMYO.MultiSQLClient.MConnection cnn = new BAYMYO.MultiSQLClient.MConnection(BAYMYO.MultiSQLClient.MClientProvider.MySQL))
            {
                switch (cnn.State)
                {
                    case System.Data.ConnectionState.Closed:
                        cnn.Open();
                        break;
                }
                using (BAYMYO.MultiSQLClient.MCommand cmd = new BAYMYO.MultiSQLClient.MCommand(System.Data.CommandType.Text, query, cnn))
                {
                    rowsAffected = cmd.ExecuteNonQuery();
                }
                switch (cnn.State)
                {
                    case System.Data.ConnectionState.Open:
                        cnn.Close();
                        break;
                }
            }
            return rowsAffected;
        }
        public static int Update(string tableName, string columnName, object id, object value)
        {
            int rowsAffected = 0;
            using (BAYMYO.MultiSQLClient.MConnection cnn = new BAYMYO.MultiSQLClient.MConnection(BAYMYO.MultiSQLClient.MClientProvider.MySQL))
            {
                switch (cnn.State)
                {
                    case System.Data.ConnectionState.Closed:
                        cnn.Open();
                        break;
                }
                using (BAYMYO.MultiSQLClient.MCommand cmd = new BAYMYO.MultiSQLClient.MCommand(System.Data.CommandType.Text, string.Format("update {0} set {1}=?value where id=?id", tableName, columnName), cnn))
                {
                    cmd.Parameters.Add("id", id);
                    cmd.Parameters.Add("value", value);
                    rowsAffected = cmd.ExecuteNonQuery();
                }
                switch (cnn.State)
                {
                    case System.Data.ConnectionState.Open:
                        cnn.Close();
                        break;
                }
            }
            return rowsAffected;
        }
        public static int Delete(string tableName, string modulName, object contentID)
        {
            int rowsAffected = 0;
            using (BAYMYO.MultiSQLClient.MConnection cnn = new BAYMYO.MultiSQLClient.MConnection(BAYMYO.MultiSQLClient.MClientProvider.MySQL))
            {
                switch (cnn.State)
                {
                    case System.Data.ConnectionState.Closed:
                        cnn.Open();
                        break;
                }
                using (BAYMYO.MultiSQLClient.MCommand cmd = new BAYMYO.MultiSQLClient.MCommand(System.Data.CommandType.Text, string.Format("delete from {0} where modulid=?modulid and icerikid=?value", tableName), cnn))
                {
                    cmd.Parameters.Add("modulid", modulName);
                    cmd.Parameters.Add("value", contentID);
                    rowsAffected = cmd.ExecuteNonQuery();
                }
                switch (cnn.State)
                {
                    case System.Data.ConnectionState.Open:
                        cnn.Close();
                        break;
                }
            }
            return rowsAffected;
        }

        public static bool SendMail(string receiveMail, string receiveName, string subject, string bodyText, bool isBodyHtml)
        {
            try
            {
                return BAYMYO.UI.Mail.Send(receiveMail, receiveName, Settings.Site.ContactMail, Settings.Site.ContactName, subject, bodyText, isBodyHtml,
                    Settings.Site.SmtpHost, Settings.Site.SmtpPort, Settings.Site.SmtpEnableSsl, Settings.Site.SmtpMail, Settings.Site.SmtpPassword);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public static bool SendMail(string receiveMail, string receiveName, string sendingMail, string sendingName, string subject, string bodyText, bool isBodyHtml)
        {
            try
            {
                return BAYMYO.UI.Mail.Send(receiveMail, receiveName, sendingMail, sendingName, subject, bodyText, isBodyHtml,
                    Settings.Site.SmtpHost, Settings.Site.SmtpPort, Settings.Site.SmtpEnableSsl, Settings.Site.SmtpMail, Settings.Site.SmtpPassword);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static bool ValidateCode()
        {
            try
            {
                string m_FilePath = HttpContext.Current.Server.MapPath(Settings.ImagesPath + "transparent.png");
                if (System.IO.File.Exists(m_FilePath))
                {
                    string[] m_Read = BAYMYO.UI.FileIO.ReadText(m_FilePath).Split('|');
                    string m_Hosts = Compute(HttpContext.Current.Request.Url.Host.Replace("www.", "").Replace(":80", "").Replace("/", "")), m_Code = Compute("GO&MYO");
                    if (!BAYMYO.UI.Converts.NullToGuid(m_Read[0]).Equals(Guid.NewGuid()) & m_Read[1].Equals(m_Hosts) & m_Read[2].Equals(m_Code))
                        return true;
                    else
                        return false;
                }
                return false;
            }
            catch (Exception)
            {
                return false;
            }
        }
        #endregion
    }
}