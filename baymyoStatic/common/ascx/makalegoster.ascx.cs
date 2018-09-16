using System;
using System.Web.UI;

namespace baymyoStatic.common.ascx
{
    public partial class makalegoster : System.Web.UI.UserControl
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
                using (Makale m = MakaleMethods.GetMakale(BAYMYO.UI.Converts.NullToInt64(Request.QueryString["mklid"])))
                {
                    if (m != null)
                    {
                        this.Page.Title = m.Baslik;// +" | " + Settings.Site.Title;
                        this.Page.MetaDescription = m.Ozet;
                        this.Page.MetaKeywords = m.Etiket;
                        string etiket = m.Etiket;
                        if (string.IsNullOrEmpty(etiket))
                            etiket = m.Ozet;

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
                                    ltrContent.Text = MessageBox.Show(DialogResult.Warning, "Bu içerik gösterime kapatılmıştır. Yazarı yada yöneticilerimiz tarafından yayından kaldırılmış bir içerik olabilir yada bağlantı adresinin doğru olduğundan eminseniz lütfen durumu yöneticilerimize bildiriniz.");
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
                                    ltrContent.Text = MessageBox.Show(DialogResult.Warning, "Bu içerik gösterime kapatılmıştır. Bu içeriği kimler yayından kaldırabilir yazarı yada yöneticilerimiz tarafından yayından kaldırılabilir.");
                                    return;
                                }
                                else
                                    View(m);
                                break;
                        }
                    }
                }
            }
            base.OnInit(e);
        }

        private void View(Makale m)
        {
            using (Hesap hsp = HesapMethods.GetHesap(m.HesapID))
            {
                this.Page.Title = m.Baslik + " - " + hsp.Adi + " " + hsp.Soyadi;
                #region --- html-meta ---
                string imageUrl = Settings.SiteImageUrl + "profil/" + hsp.ProfilObject.ResimUrl, meslek= KategoriMethods.GetKategori("meslek", hsp.ProfilObject.Meslek).Adi;
                #endregion
                CommentControl1.IsCommandActive = BAYMYO.UI.Converts.NullToGuidString(Core.CurrentUser.ID).Equals(m.HesapID);
                CommentControl1.Visible = m.Yorum;
                CommentControl1.ModulID = "makale";
                CommentControl1.IcerikID = Request.QueryString["mklid"];
                ltrContent.Text = BAYMYO.UI.FileIO.ReadText(Server.MapPath(Settings.ViewPath + "ArticleView.view"));
                //Icerik Bilgisi
                ltrContent.Text = ltrContent.Text.Replace("%ImagesPath%", Settings.ImagesPath);
                if (!BAYMYO.UI.Converts.NullToString(KategoriBilgi.ModulID).Equals("makale") || !BAYMYO.UI.Converts.NullToString(KategoriBilgi.ID).Equals(m.KategoriID))
                    KategoriBilgi = KategoriMethods.GetKategori("makale", m.KategoriID);
                if (KategoriBilgi.Aktif)
                    this.Page.Title += " | " + KategoriBilgi.Adi.ToUpper();// +" - " + m.Etiket;
                ltrContent.Text = ltrContent.Text.Replace("%Renk%", KategoriBilgi.Renk);
                ltrContent.Text = ltrContent.Text.Replace("%Kategori%", string.Format("<a href=\"{0}\" target=\"_blank\">{1}</a>", Core.CreateLink("makalekategori", KategoriBilgi.ID, KategoriBilgi.Adi), KategoriBilgi.Adi));
                ltrContent.Text = ltrContent.Text.Replace("%ResimUrl%", "<div class=\"profil-img\"><a href=\"%Url%\" target=\"_blank\"><img class=\"image left\" src=" + Settings.ImagesPath + "profil/" + hsp.ProfilObject.ResimUrl + " alt=\"%Adi% %Soyadi%\" /></a>" + string.Format("<span class=\"links\"><a class='toolTip' title='Yazarın profilini görüntülemek için buraya tıklayın.' href=\"/{0}\" target=\"_blank\"><b>{4} {1} {2}</b></a>&nbsp;<br/><span style='font-size:12px;color:#454545;margin-bottom: 8px;display:block;'>{3}</span>", hsp.ProfilObject.Url, hsp.Adi, hsp.Soyadi, hsp.ProfilObject.Mail, meslek) + "</span></div>");
                ltrContent.Text = ltrContent.Text.Replace("%SpotBaslik%", ((!string.IsNullOrEmpty(m.ResimUrl)) ? string.Format("<figure class=\"photo\"><img itemprop=\"image\" src=\"{0}\" alt=\"%Adi% %Soyadi%\" title=\"%Adi% %Soyadi%\" /><meta itemprop=\"interactionCount\" content=\"UserComments:{1}\" /><meta itemprop=\"thumbnailUrl\" content=\"{0}\" /></figure>", Settings.SiteImageUrl + "makale/" + m.ResimUrl, YorumMethods.Count(m.ID.ToString())) : ""));
                ltrContent.Text = ltrContent.Text.Replace("%Baslik%", m.Baslik.Replace('"', '\''));
                ltrContent.Text = ltrContent.Text.Replace("%Body%", BAYMYO.UI.Web.Pages.ClearHtml(m.Icerik).Replace('"', '\''));
                ltrContent.Text = ltrContent.Text.Replace("%BaslikDiger%", hsp.ProfilObject.Adi + " " + KategoriBilgi.Adi.ToLower() + " kategorisindeki makaleleri ve röportajları.");
                ltrContent.Text = ltrContent.Text.Replace("%Ozet%", m.Ozet.Replace('"', '\''));
                ltrContent.Text = ltrContent.Text.Replace("%KayitTarihiSEO%", m.KayitTarihi.ToString("yyyy-MM-ddTHH:mm:ssZ"));
                ltrContent.Text = ltrContent.Text.Replace("%KayitTarihi%", m.KayitTarihi.ToString("dd MMMMM yyyy ddddd - HH:mm:ss"));
                ltrContent.Text = ltrContent.Text.Replace("%ImageUrl%", imageUrl);

                string etiketler = string.Empty;
                foreach (string item in m.Etiket.Split(','))
                    etiketler += string.Format("<a href=\"{0}{1}\" target=\"_blank\"><strong>{2}</strong></a>, ", Settings.SiteUrl.Replace("http:", ""), Core.CreateLink("makaleetiket", "", item), item.Trim());
                ltrContent.Text = ltrContent.Text.Replace("%Etiket%", "<p><b>Etiketler&nbsp;//</b>&nbsp;" + etiketler + "</p>");
                //Hesap ve Profil Bilgileri
                ltrContent.Text = ltrContent.Text.Replace("%Adi%", hsp.Adi);
                ltrContent.Text = ltrContent.Text.Replace("%Soyadi%", hsp.Soyadi);
                ltrContent.Text = ltrContent.Text.Replace("%Url%", Settings.VirtualPath + hsp.ProfilObject.Url);
                ltrContent.Text = ltrContent.Text.Replace("%ProfilAdi%", hsp.ProfilObject.Adi);
                ltrContent.Text = ltrContent.Text.Replace("%Meslek%", meslek);
                m.Icerik += string.Format("<div class=\"clear\"></div><a class='toolTip' title='Yazarın tüm yazılarını görüntülemek için buraya tıklayın.' class href=\"{0}\"><u><b>Diğer tüm yazıları için buraya tıklayın!</b></u></a><br/>", Core.CreateLink("makaleyazar", hsp.ProfilObject.Url, hsp.Adi + " " + hsp.Soyadi));
                string modulID = CommentControl1.ModulID;
                if (m.Uye)
                {
                    if (Core.IsUserActive)
                    {
                        Core.ViewCounter(modulID, m.ID);
                        ltrContent.Text = ltrContent.Text.Replace("%Icerik%", m.Icerik);
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
                    ltrContent.Text = ltrContent.Text.Replace("%Icerik%", m.Icerik);
                }
                //Gösterim Bilgisi
                switch (Settings.Site.CounterView)
                {
                    case CounterViewType.Hidden:
                        m.GosterimSayi = false;
                        break;

                }
                ltrContent.Text = ltrContent.Text.Replace("%Gosterim%", m.GosterimSayi ? string.Format(" - Bu {0} <b class=\"toolTip\" style=\"cursor:pointer;\" title=\"Bu oran {0} tekil izlenme sayısını gösterir.\">{1}</b> kere okundu.", modulID, GosterimMethods.Count(modulID, m.ID)) : "");
                modulID = null;
            }
        }
    }
}