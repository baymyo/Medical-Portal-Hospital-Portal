using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace baymyoStatic.common.ascx
{
    public partial class habergoster : System.Web.UI.UserControl
    {
        public Kategori KategoriBilgi
        {
            get { return (Cache["KategoriBilgi"] == null) ? new Kategori() : Cache["KategoriBilgi"] as Kategori; }
            set { Cache["KategoriBilgi"] = value; }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                try
                {
                    using (Haber m = HaberMethods.GetHaber(BAYMYO.UI.Converts.NullToInt64(Request.QueryString["hid"])))
                    {
                        if (m != null)
                        {
                            #region --- html-meta ---
                            //Core.ClearMetaTags(this.Page);
                            this.Page.Title = m.Baslik;// +" | " + Settings.Site.Title;
                            this.Page.MetaDescription = m.Ozet;
                            this.Page.MetaKeywords = m.Etiket;
                            string etiket = m.Etiket;
                            if (string.IsNullOrEmpty(etiket))
                                etiket = m.Ozet;
                            string imageUrl = Settings.SiteImageUrl + "haber/" + m.ResimUrl;
                            #endregion
                            if (!m.Aktif)
                            {
                                ltrContent.Text = MessageBox.Show(DialogResult.Warning, "Bu içerik gösterime kapatılmıştır. Bağlantının doğru olduğundan eminseniz lütfen bu durumu yöneticilerimize bildiriniz!");
                                return;
                            }
                            CommentControl1.IsCommandActive = BAYMYO.UI.Converts.NullToGuidString(Core.CurrentUser.ID).Equals(m.HesapID);
                            CommentControl1.Visible = m.Yorum;
                            CommentControl1.ModulID = "haber";
                            CommentControl1.IcerikID = Request.QueryString["hid"];
                            using (Hesap hsp = HesapMethods.GetHesap(m.HesapID))
                            {
                                //Icerik Bilgisi
                                ltrContent.Text = BAYMYO.UI.FileIO.ReadText(Server.MapPath(Settings.ViewPath + "NewsView.view"));
                                //string ilgiliVideo = "", ilgiliFoto = "", fotogaleri = "", videogaleri = "";
                                //if (m.Video > 0)
                                //{
                                //    videogaleri = string.Format("<div class=\"clear\"></div><a href=\"{0}\"><img style=\"float:left\" itemprop=\"image\" src=\"/images/video-tikla.png\" alt=\"{1} videosunu izlemek için tıklayın.\" title=\"{1} videosunu izlemek için tıklayın.\" /></a>", Core.CreateLink("video", m.Video, "video galeri " + m.Baslik), m.Baslik);
                                //    ilgiliVideo = string.Format("<span class=\"video\"><a href=\"{0}\">{1}</a></span>", Core.CreateLink("video", m.Video, m.Baslik + " video"), "Web<br/>TV");
                                //}
                                //if (m.Galeri > 0)
                                //{
                                //    fotogaleri = string.Format("<div class=\"clear\"></div><a href=\"{0}\"><img style=\"float:left\" itemprop=\"image\" src=\"/images/foto-galeri-tikla.png\" alt=\"{1} foto galeriyi gezinmek için tıklayın.\" title=\"{1} foto galeriyi gezinmek için tıklayın.\" /></a>", Core.CreateLink("galeri", m.Galeri, "foto galeri " + m.Baslik), m.Baslik);
                                //    ilgiliFoto = string.Format("<span class=\"galeri\"><a href=\"{0}\">{1}</a></span>", Core.CreateLink("galeri", m.Galeri, m.Baslik + " fotogaleri"), "Foto<br/>Galeri");
                                //}

                                ltrContent.Text = ltrContent.Text.Replace("%ImagesPath%", Settings.ImagesPath);
                                if (!BAYMYO.UI.Converts.NullToString(KategoriBilgi.ModulID).Equals("haber") || !BAYMYO.UI.Converts.NullToString(KategoriBilgi.ID).Equals(m.KategoriID))
                                {
                                    KategoriBilgi = KategoriMethods.GetKategori("haber", m.KategoriID);
                                    if (KategoriBilgi.Aktif)
                                        this.Page.Title += " | " + KategoriBilgi.Adi;
                                }
                                if (!string.IsNullOrWhiteSpace(m.Sehir) & !KategoriBilgi.Adi.ToLower().Equals(m.Sehir.ToLower()))
                                    this.Page.Title += " | " + m.Sehir;
                                ltrContent.Text = ltrContent.Text.Replace("%Renk%", KategoriBilgi.Renk);
                                ltrContent.Text = ltrContent.Text.Replace("%Kategori%", string.Format("<a href=\"{0}\" target=\"_blank\">{1}</a>", Core.CreateLink("haberkategori", KategoriBilgi.ID, KategoriBilgi.Adi), KategoriBilgi.Adi));
                                if (!string.IsNullOrEmpty(m.ResimUrl))
                                {
                                    string url = Settings.ImagesPath + "haber/b/" + m.ResimUrl;
                                    if (!System.IO.File.Exists(Server.MapPath(url)))
                                        ltrContent.Text = ltrContent.Text.Replace("%ResimUrl%", "");
                                    else
                                        ltrContent.Text = ltrContent.Text.Replace("%ResimUrl%", string.Format("<img itemprop=\"image\" src=\"{0}\" alt=\"%Baslik%\" title=\"%Baslik%\" />", url));
                                }
                                else
                                    ltrContent.Text = ltrContent.Text.Replace("%ResimUrl%", "");
                                ltrContent.Text = ltrContent.Text.Replace("%ImageUrl%", imageUrl);
                                ltrContent.Text = ltrContent.Text.Replace("%Baslik%", m.Baslik.Replace('"', '\''));
                                ltrContent.Text = ltrContent.Text.Replace("%Body%", BAYMYO.UI.Web.Pages.ClearHtml(m.Icerik).Replace('"', '\''));
                                ltrContent.Text = ltrContent.Text.Replace("%BaslikDiger%", m.Sehir + " haber son dakika " + KategoriBilgi.Adi.ToLower() + " haberleri.");
                                ltrContent.Text = ltrContent.Text.Replace("%SpotBaslik%", "");
                                ltrContent.Text = ltrContent.Text.Replace("%Ozet%", m.Ozet.Replace('"', '\''));
                                ltrContent.Text = ltrContent.Text.Replace("%KayitTarihiSEO%", m.KayitTarihi.ToString("yyyy-MM-ddTHH:mm:ssZ"));
                                ltrContent.Text = ltrContent.Text.Replace("%KayitTarihi%", m.KayitTarihi.ToString("dd/MM/yyyy ddddd HH:mm"));
                                ltrContent.Text = ltrContent.Text.Replace("%Sehir%", m.Sehir);
                                //int indexOf = -1;
                                //string etiketler = string.Empty, etiketQuery = string.Empty; etiket = string.Empty;
                                //foreach (string item in m.Etiket.Split(','))
                                //{
                                //    etiket = string.Format("<a href=\"{0}{1}\" target=\"_blank\"><strong>{2}</strong></a>", Settings.SiteUrl.Replace("http:", ""), Core.CreateLink("haberetiket", "", item), item.Trim());
                                //    etiketQuery += " or i.yoneticionay=1 and i.aktif=1 and i.etiket like <%USTENTIRNAK%>%" + item.Trim() + ",%<%USTENTIRNAK%>";
                                //    indexOf = m.Icerik.IndexOf(item.Trim());
                                //    if (indexOf >= 0)
                                //    {
                                //        m.Icerik = m.Icerik.Remove(indexOf, item.Trim().Length);
                                //        m.Icerik = m.Icerik.Insert(indexOf, etiket);
                                //    }
                                //    indexOf = -1;
                                //    etiketler += etiket + ", ";
                                //}
                                //if (!BAYMYO.UI.Converts.NullToString(Session["etiketSession"]).Equals(etiketQuery))
                                //    Session["etiketSession"] = etiketQuery;
                                //ltrContent.Text = ltrContent.Text.Replace("%Etiket%", "<p><b>Etiketler&nbsp;//</b>&nbsp;" + etiketler + "</p>");
                                //etiketler = null; etiket = null; indexOf = 0;
                                //Hesap Bilgileri
                                ltrContent.Text = ltrContent.Text.Replace("%Adi%", hsp.Adi);
                                ltrContent.Text = ltrContent.Text.Replace("%Soyadi%", hsp.Soyadi);
                                ltrContent.Text = ltrContent.Text.Replace("%Url%", Settings.VirtualPath + hsp.ProfilObject.Url);
                                ltrContent.Text = ltrContent.Text.Replace("%ProfilAdi%", hsp.ProfilObject.Adi.Replace('"', '\''));
                                //ltrContent.Text = ltrContent.Text.Replace("%Meslek%", KategoriMethods.GetKategori("meslek", hsp.ProfilObject.Meslek).Adi);
                                string modulID = CommentControl1.ModulID, 
                                    icerik = string.Empty;
                                if (m.Uye & !Core.CurrentUser.Tipi.Equals(AccountType.Admin))
                                {
                                    if (Core.IsUserActive)
                                    {
                                        //Core.ViewCounter(modulID, m.ID);
                                        //if (Settings.Site.IsVideoView > 0 & m.Video > 0)
                                        //    using (Video v = VideoMethods.GetVideo(m.Video))
                                        //    {
                                        //        if (Core.IsMobileBrowser())
                                        //            v.Embed = v.Embed.Replace("width=\"728\" height=\"410\"", "width=\"100%\" height=\"250\"");
                                        //        switch (Settings.Site.IsVideoView)
                                        //        {
                                        //            case 1:
                                        //                icerik = string.Format("<div class=\"clear\"></div>{0}<div class=\"clear\" style=\"margin-bottom:5px\"></div>", v.Embed) + m.Icerik;
                                        //                break;
                                        //            case 2:
                                        //                icerik = m.Icerik + string.Format("<div class=\"clear\">&nbsp;</div>{0}<div class=\"clear\">&nbsp;</div>", v.Embed);
                                        //                break;
                                        //            default:
                                        //                icerik = m.Icerik;
                                        //                break;
                                        //        }
                                        //    }
                                        //else
                                        icerik = m.Icerik;
                                        this.ltrContent.Text = this.ltrContent.Text.Replace("%Icerik%", icerik); //+ fotogaleri + videogaleri);
                                    }
                                    else
                                    {
                                        CommentControl1.Visible = false;
                                        this.ltrContent.Text = this.ltrContent.Text.Replace("%Icerik%", string.Format("..<br/><div class=\"clear\">&nbsp;</div>Devamını okumak ve yapılan yorumları görmek için sizde <a href=\"{0}?l=1&ReturnUrl={1}\"><b>Üye Girişi</b></a> yapınız yada <a href=\"{0}?l=2&type=standart&ReturnUrl={1}\"><b>Ücretsiz Kayıt</b></a> olunuz.", Settings.VirtualPath, Request.RawUrl));
                                    }
                                }
                                else
                                {
                                    //Core.ViewCounter(modulID, m.ID);
                                    //if (Settings.Site.IsVideoView > 0 & m.Video > 0)
                                    //    using (Video v = VideoMethods.GetVideo(m.Video))
                                    //    {
                                    //        if (Core.IsMobileBrowser())
                                    //            v.Embed = v.Embed.Replace("width=\"728\" height=\"410\"", "width=\"100%\" height=\"250\"");
                                    //        switch (Settings.Site.IsVideoView)
                                    //        {
                                    //            case 1:
                                    //                icerik = string.Format("<div class=\"clear\"></div>{0}<div class=\"clear\" style=\"margin-bottom:5px\"></div>", v.Embed) + m.Icerik;
                                    //                break;
                                    //            case 2:
                                    //                icerik = m.Icerik + string.Format("<div class=\"clear\">&nbsp;</div>{0}<div class=\"clear\">&nbsp;</div>", v.Embed);
                                    //                break;
                                    //            default:
                                    //                icerik = m.Icerik;
                                    //                break;
                                    //        }
                                    //    }
                                    //else
                                    icerik = m.Icerik;
                                    this.ltrContent.Text = this.ltrContent.Text.Replace("%Icerik%", icerik);// + fotogaleri + videogaleri);
                                }
                                ////Gösterim Bilgisi
                                //switch (Settings.Site.CounterView)
                                //{
                                //    case CounterViewType.Hidden:
                                //        m.GosterimSayi = false;
                                //        break;
                                //}
                                //this.ltrContent.Text = this.ltrContent.Text.Replace("%Gosterim%", m.GosterimSayi ? string.Format(" - Bu {0} <b class=\"toolTip\" style=\"cursor:pointer;\" title=\"Bu oran {0} tekil izlenme sayısını gösterir.\">{1}</b> kere okundu.", modulID, GosterimMethods.Count(modulID, m.ID)) : "");
                                modulID = icerik = null;
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    ltrContent.Text = MessageBox.Show(DialogResult.Error, "Son dakika Türkiye gündemi ve haberleri, intertnet'deki en son gelişmeler'den ilk sizin haberiniz olsun. Bu habere teknik bir arızadan dolayı geçici bir süre için erişim sağlanamayacaktır.<br/>//Sistem Mesajı: " + ex.Message + "!");
                }
            }
            base.OnInit(e);
        }
    }
}