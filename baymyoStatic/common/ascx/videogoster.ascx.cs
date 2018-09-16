using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace baymyoStatic.common.ascx
{
    public partial class videogoster : System.Web.UI.UserControl
    {
        public Kategori KategoriBilgi
        {
            get { return (Cache["KategoriBilgi"] == null) ? new Kategori() : Cache["KategoriBilgi"] as Kategori; }
            set { Cache["KategoriBilgi"] = value; }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            //if (!Page.IsPostBack)
            //{
            using (Video m = VideoMethods.GetVideo(BAYMYO.UI.Converts.NullToInt64(Request.QueryString["vid"])))
            {
                if (m != null)
                {
                    this.Page.Title = m.Baslik;// +" | " + Settings.Site.Title;
                    this.Page.MetaDescription = m.Baslik + " videosunu izlemek için tıklayın.";
                    this.Page.MetaKeywords = m.Etiket;
                    string etiket = m.Etiket;
                    if (string.IsNullOrEmpty(etiket))
                        etiket = m.Baslik;
                    switch (Core.CurrentUser.Tipi)
                    {
                        case AccountType.Admin:
                            View(m);
                            break;
                        case AccountType.Private:
                        case AccountType.Doctor:
                        case AccountType.Editor:
                            if (!m.Aktif & !BAYMYO.UI.Converts.NullToGuidString(Core.CurrentUser.ID).Equals(m.HesapID))
                            {
                                CommentControl1.Visible = false;
                                ltrContent.Text = MessageBox.Show(DialogResult.Warning, "Bu içerik gösterime kapatılmıştır. Kimler yayından kaldırabilir yazarı yada yöneticilerimiz tarafından yayından kaldırılabilir.");
                                return;
                            }
                            else
                                View(m);
                            break;
                        case AccountType.None:
                        case AccountType.Standart:
                            if (!m.Aktif)
                            {
                                CommentControl1.Visible = false;
                                ltrContent.Text = MessageBox.Show(DialogResult.Warning, "Bu içerik gösterime kapatılmıştır. Kimler yayından kaldırabilir yazarı yada yöneticilerimiz tarafından yayından kaldırılabilir.");
                                return;
                            }
                            else
                                View(m);
                            break;
                    }
                }
            }
            //}
            base.OnInit(e);
        }

        private void View(Video m)
        {
            using (Hesap hsp = HesapMethods.GetHesap(m.HesapID))
            {
                if (Core.IsMobileBrowser())
                    m.Embed = m.Embed.Replace("width=\"728\" height=\"410\"", "width=\"100%\" height=\"250\"");

                CommentControl1.IsCommandActive = BAYMYO.UI.Converts.NullToGuidString(Core.CurrentUser.ID).Equals(m.HesapID);
                CommentControl1.Visible = m.Yorum;
                CommentControl1.ModulID = "video";
                CommentControl1.IcerikID = Request.QueryString["vid"];
                ltrContent.Text = BAYMYO.UI.FileIO.ReadText(Server.MapPath(Settings.ViewPath + "VideoView.view"));
                //Icerik Bilgisi
                if (!BAYMYO.UI.Converts.NullToString(KategoriBilgi.ModulID).Equals("video") || !BAYMYO.UI.Converts.NullToString(KategoriBilgi.ID).Equals(m.KategoriID))
                    KategoriBilgi = KategoriMethods.GetKategori("video", m.KategoriID);
                if (KategoriBilgi.Aktif)
                {
                    this.Page.Title += " | " + KategoriBilgi.Adi.ToUpper();
                    ltrContent.Text = ltrContent.Text.Replace("%Renk%", KategoriBilgi.Renk);
                }
                string imageUrl = Settings.SiteImageUrl + "video/" + m.ResimUrl;//, embedUrl = "";
                                                                                //System.Text.RegularExpressions.Match m2 = System.Text.RegularExpressions.Regex.Match(m.Embed, @"src=\""(.*?)\""", System.Text.RegularExpressions.RegexOptions.Singleline);
                                                                                //if (m2.Success)
                                                                                //    embedUrl = m2.Groups[1].Value;
                                                                                //else
                                                                                //    embedUrl = "";
                                                                                //ltrContent.Text = ltrContent.Text.Replace("%EmbedUrl%", embedUrl);
                ltrContent.Text = ltrContent.Text.Replace("%ImageUrl%", imageUrl);
                ltrContent.Text = ltrContent.Text.Replace("%Kategori%", string.Format("<a id=\"ctgALink\" href=\"{0}\" target=\"_blank\" alt=\"{2}\">{1}</a>", Core.CreateLink("videokategori", KategoriBilgi.ID, KategoriBilgi.Adi), KategoriBilgi.Adi, KategoriBilgi.ID));
                //ltrContent.Text = ltrContent.Text.Replace("%ResimUrl%", ((!string.IsNullOrEmpty(m.ResimUrl)) ? "<img class=\"image left\" src=" + Settings.ImagesPath + "video/" + m.ResimUrl + " alt=\"%Baslik%\" />" : ""));
                ltrContent.Text = ltrContent.Text.Replace("%Baslik%", m.Baslik.Replace('"', '\''));
                ltrContent.Text = ltrContent.Text.Replace("%Ozet%", KategoriBilgi.Adi + " konusuna bağlı en son videoları izleyin.");
                ltrContent.Text = ltrContent.Text.Replace("%KayitTarihiSEO%", m.KayitTarihi.ToString("yyyy-MM-ddTHH:mm:ssZ"));
                ltrContent.Text = ltrContent.Text.Replace("%KayitTarihi%", m.KayitTarihi.ToString("dd/MM/yyyy ddddd HH:mm"));
                string etiketler = string.Empty, etiketQuery = string.Empty, siteUrl = Settings.SiteUrl.Replace("http:", "");
                foreach (string item in m.Etiket.Split(','))
                {
                    etiketler += string.Format("<a href=\"{0}{1}\" target=\"_blank\"><strong>{2}</strong></a>, ", Settings.SiteUrl.Replace("http:", ""), Core.CreateLink("videoetiket", "", item), item.Trim());
                    etiketQuery += " or i.yoneticionay=1 and i.aktif=1 and i.etiket like <%USTENTIRNAK%>%" + item.Trim() + ",%<%USTENTIRNAK%>";
                }
                if (!BAYMYO.UI.Converts.NullToString(Session["etiketSession"]).Equals(etiketQuery))
                    Session["etiketSession"] = etiketQuery;
                ltrContent.Text = ltrContent.Text.Replace("%Etiket%", "<b>Etiketler;</b> " + etiketler);
                //Hesap Bilgileri
                switch (hsp.Tipi)
                {
                    case AccountType.Admin:
                        ltrContent.Text = ltrContent.Text.Replace("%Ekleyen%", "");
                        break;
                    default:
                        ltrContent.Text = ltrContent.Text.Replace("%Ekleyen%", "<b class=\"left\">Ekleyen;&nbsp;</b><a href=\"%Url%\" target=\"_blank\"><b class=\"left\">%Adi% %Soyadi%</b><img class=\"toolTip left\" style=\"margin-left: 3px;\" src=\"%ImagesPath%icons/10/expand.png\" title=\"%Adi% %Soyadi% sayfasına git.\" alt=\"Yazarın sayfasına git.\" border=\"0\" /><div class=\"clear\"></div></a>");
                        ltrContent.Text = ltrContent.Text.Replace("%Adi%", hsp.Adi);
                        ltrContent.Text = ltrContent.Text.Replace("%Soyadi%", hsp.Soyadi);
                        break;
                }
                ltrContent.Text = ltrContent.Text.Replace("%Url%", Settings.VirtualPath + hsp.ProfilObject.Url);
                ltrContent.Text = ltrContent.Text.Replace("%ProfilAdi%", hsp.ProfilObject.Adi);
                ltrContent.Text = ltrContent.Text.Replace("%ImagesPath%", Settings.ImagesPath);
                //ltrContent.Text = ltrContent.Text.Replace("%Meslek%", KategoriMethods.GetKategori("meslek", hsp.ProfilObject.Meslek).Adi);

                string modulID = CommentControl1.ModulID;
                if (m.Uye)
                {
                    if (Core.IsUserActive)
                    {
                        Core.ViewCounter(modulID, m.ID);
                        ltrContent.Text = ltrContent.Text.Replace("%Icerik%", m.Embed.Trim());
                    }
                    else
                    {
                        CommentControl1.Visible = false;
                        ltrContent.Text = ltrContent.Text.Replace("%Icerik%", string.Format("..<br/><br/><br/>Devamını okumak ve yapılan yorumları görmek için sizde <a href=\"{0}?l=1&ReturnUrl={1}\"><b>Üye Girişi</b></a> yapınız yada <a href=\"{0}?l=2&type=standart&ReturnUrl={1}\"><b>Ücretsiz Kayıt</b></a> olunuz.", Settings.VirtualPath, Request.RawUrl));
                    }
                }
                else
                {
                    Core.ViewCounter(modulID, m.ID);
                    ltrContent.Text = ltrContent.Text.Replace("%Icerik%", m.Embed.Trim());
                }
                //Gösterim Bilgisi
                switch (Settings.Site.CounterView)
                {
                    case CounterViewType.Hidden:
                        m.GosterimSayi = false;
                        ltrContent.Text = ltrContent.Text.Replace("%GosterimSayi%", "");
                        ltrContent.Text = ltrContent.Text.Replace("%Gosterim%", "");
                        break;
                    default:
                        int viewCount = GosterimMethods.Count(modulID, m.ID);
                        ltrContent.Text = ltrContent.Text.Replace("%GosterimSayi%", viewCount.ToString());
                        ltrContent.Text = ltrContent.Text.Replace("%Gosterim%", m.GosterimSayi ? string.Format("<small>Gösterim: </small>{0}", viewCount) : "");
                        break;
                }
                modulID = null;
                GetOtherData(m);
            }
        }

        private void GetOtherData(Video m)
        {
            using (BAYMYO.UI.Web.CustomSqlQuery data = new BAYMYO.UI.Web.CustomSqlQuery(rptOther, "video", "guncellemetarihi desc", "id<>?id and yoneticionay=1 and aktif=1 and kategoriid like ?kategoriid"))
            {
                data.Top = 10;
                data.Parameters.Add("id", m.ID + "%", BAYMYO.MultiSQLClient.MSqlDbType.BigInt);
                data.Parameters.Add("kategoriid", m.KategoriID + "%", BAYMYO.MultiSQLClient.MSqlDbType.VarChar);
                data.Execute();
            }
        }
    }
}