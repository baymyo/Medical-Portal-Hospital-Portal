using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace baymyoStatic.common.ascx
{
    public partial class profil : System.Web.UI.UserControl
    {
        enum ViewType
        {
            Profil,
            About,
            Contact
        }

        void ScreenClear()
        {
            //CommentControl1.Visible = false;
            contact1.Visible = false;
            //mapsArea.Visible = false;
            //lastArticle.Visible = false;
        }

        ViewType viewPage = ViewType.Profil;
        protected override void OnInit(EventArgs e)
        {
            if (!string.IsNullOrEmpty(Request.QueryString["type"]))
            {
                ScreenClear();
                switch (Request.QueryString["type"])
                {
                    case "about":
                        viewPage = ViewType.About;
                        //CommentControl1.Visible = true;
                        break;
                    case "contact":
                        viewPage = ViewType.Contact;
                        contact1.Visible = true;
                        break;
                    default:
                        viewPage = ViewType.Profil;
                        //lastArticle.Visible = true;
                        //if (!string.IsNullOrWhiteSpace(Settings.Site.GoogleMaps))
                        //{
                        //    this.Page.ClientScript.RegisterClientScriptInclude("googleMapsJS", Settings.Site.GoogleMaps);
                        //    mapsArea.Visible = true;
                        //}
                        break;
                }
            }
            base.OnInit(e);
        }

        public string GetUniqueID = "nan";
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                try
                {
                    if (!string.IsNullOrEmpty(Request.QueryString["url"]))
                    {
                        using (Hesap hsp = HesapMethods.GetHesapUrl(Request.QueryString["url"]))
                        {
                            string meslek = KategoriMethods.GetKategori("meslek", hsp.ProfilObject.Meslek).Adi;
                            if (string.IsNullOrEmpty(hsp.ID))
                            {
                                this.ltrContent.Text = MessageBox.Show(DialogResult.Stop, "Şuan erişmeye çalıştığınız sayfa bulunamadı lütfen gitmek istediğiniz adresi kontrol edin ve tekrar deneyiniz!<span class=\"right\"><a href=\"/home\"><b>Anasayfaya geri dön!</b></a>&nbsp;-&nbsp;<a href=\"/contact\"><b>Hatalı sayfa bildirimi!</b></a></span>");
                                ScreenClear();
                                return;
                            }
                            GetUniqueID = hsp.ID.ToString();
                            this.ltrTitle.Text = hsp.ProfilObject.Adi;
                            this.Page.Title = meslek + " " + hsp.Adi + ' ' + hsp.Soyadi;
                            BAYMYO.UI.Web.Pages.AddMetaTag(this.Page, hsp.Adi + " " + hsp.Soyadi + " hakkında ", hsp.ProfilObject.Hakkimda);
                            //switch (hsp.Tipi)
                            //{
                            //    case AccountType.Admin:
                            //    case AccountType.Private:
                            //    case AccountType.Doctor:
                            //    case AccountType.Editor:
                            //        if (!string.IsNullOrEmpty(hsp.ProfilObject.Adi))
                            //            this.Page.Title = hsp.ProfilObject.Adi;
                            //        else
                            //            this.Page.Title = meslek + " " + hsp.Adi + ' ' + hsp.Soyadi;
                            //        BAYMYO.UI.Web.Pages.AddMetaTag(this.Page, this.Page.Title + " hakkında ", hsp.ProfilObject.Hakkimda);
                            //        break;
                            //    default:
                            //        this.Page.Title = meslek + " " + hsp.Adi + ' ' + hsp.Soyadi;
                            //        BAYMYO.UI.Web.Pages.AddMetaTag(this.Page, hsp.Adi + " " + hsp.Soyadi + " hakkında ", hsp.ProfilObject.Hakkimda);
                            //        break;
                            //}

                            if (!hsp.Aktif & !Core.CurrentUser.Tipi.Equals(AccountType.Admin))
                            {
                                //CommentControl1.Visible = false;
                                contact1.Visible = false;
                                //lastArticle.Visible = false;
                                ltrContent.Text = MessageBox.Show(DialogResult.Warning, "Şuan erişmek istediğiniz <b>'Sayfa'</b> gösterime kapalı durumdadır nedenleri aşağıda belirtilmiştir. <br/>* Üyelik başvuru inceleme altında olabilir.<br/>* Yada yöneticilerimiz tarafından editör üyeliği durdurulmuş olabilir.");
                                return;
                            }
                            ltrContent.Text = BAYMYO.UI.FileIO.ReadText(Server.MapPath(Settings.ViewPath + "ProfilView.view"));
                            //Hesap Bilgi
                            ltrContent.Text = ltrContent.Text.Replace("%ImagesPath%", Settings.ImagesPath);
                            ltrContent.Text = ltrContent.Text.Replace("%Adi%", hsp.Adi);
                            ltrContent.Text = ltrContent.Text.Replace("%Soyadi%", hsp.Soyadi);
                            ltrContent.Text = ltrContent.Text.Replace("%KayitTarihi%", hsp.KayitTarihi.ToShortDateString());
                            //Profil Bilgi
                            ltrContent.Text = ltrContent.Text.Replace("%Url%", Settings.VirtualPath + hsp.ProfilObject.Url);
                            ltrContent.Text = ltrContent.Text.Replace("%Meslek%", meslek);
                            ltrContent.Text = ltrContent.Text.Replace("%Egitim%", KategoriMethods.GetKategori("egitim", hsp.ProfilObject.Egitim).Adi);
                            ltrContent.Text = ltrContent.Text.Replace("%ResimUrl%", Settings.ImagesPath + ((!string.IsNullOrEmpty(hsp.ProfilObject.ResimUrl)) ? "profil/" + hsp.ProfilObject.ResimUrl : "yok.png"));
                            ltrContent.Text = ltrContent.Text.Replace("%ProfilAdi%", hsp.ProfilObject.Adi);
                            ltrContent.Text = ltrContent.Text.Replace("%Mail%", string.IsNullOrEmpty(hsp.ProfilObject.Mail) ? "***@*****.***" : string.Format("<a href=\"mailto:{0}?subject={1}\">{0}</a>", hsp.ProfilObject.Mail, Settings.Site.Title + " sitesi uzerinden gonderildi!"));
                            ltrContent.Text = ltrContent.Text.Replace("%Web%", string.IsNullOrEmpty(hsp.ProfilObject.Web) ? "http://www.*" : string.Format("<a href=\"http://{0}?ref={1}\" target=\"_blank\">{0}</a>", hsp.ProfilObject.Web, Settings.SiteUrl));
                            ltrContent.Text = ltrContent.Text.Replace("%Telefon%", string.IsNullOrEmpty(hsp.ProfilObject.Telefon) ? "*** *** ** **" : hsp.ProfilObject.Telefon);
                            ltrContent.Text = ltrContent.Text.Replace("%GSM%", string.IsNullOrEmpty(hsp.ProfilObject.GSM) ? "*** *** ** **" : hsp.ProfilObject.GSM);
                            ltrContent.Text = ltrContent.Text.Replace("%Sehir%", string.IsNullOrEmpty(hsp.ProfilObject.Sehir) ? "********" : hsp.ProfilObject.Sehir);
                            ltrContent.Text = ltrContent.Text.Replace("%Cinsiyet%", hsp.Cinsiyet.ToString());
                            //Profildeki Baglantilar
                            ltrContent.Text = ltrContent.Text.Replace("%Iletisim%", Core.CreateLink("iletisim", hsp.ProfilObject.Url, "go"));
                            ltrContent.Text = ltrContent.Text.Replace("%Makaleleri%", Core.CreateLink("makaleyazar", hsp.ProfilObject.Url, hsp.Adi + " " + hsp.Soyadi));
                            ltrContent.Text = ltrContent.Text.Replace("%Mesajlari%", Core.CreateLink("mesajliste", hsp.ID, hsp.Adi + " " + hsp.Soyadi));
                            ltrContent.Text = ltrContent.Text.Replace("%YazarHakkinda%", Core.CreateLink("yazarhakkinda", hsp.ProfilObject.Url, "go"));
                            switch (viewPage)
                            {
                                case ViewType.Profil:
                                    #region --- Profil ---
                                    this.Page.Title += " - Profil Sayfası";
                                    if (!string.IsNullOrEmpty(hsp.ProfilObject.Hakkimda))
                                    {
                                        ltrContent.Text = ltrContent.Text.Replace("%DetailBlock%", BAYMYO.UI.FileIO.ReadText(Server.MapPath(Settings.ViewPath + "ProfilAboutUs.view")));
                                        ltrContent.Text = ltrContent.Text.Replace("%Hakkimda%", hsp.ProfilObject.Hakkimda);
                                    }
                                    else
                                        ltrContent.Text = ltrContent.Text.Replace("%DetailBlock%", "");
                                    Core.ViewCounter("profil", hsp.ID);
                                    GetData(hsp.ID);
                                    #endregion
                                    break;
                                case ViewType.About:
                                    this.Page.Title += " - Hakkında";
                                    //if (Core.CurrentUser.ProfilObject != null)
                                    //    CommentControl1.IsCommandActive = BAYMYO.UI.Converts.NullToString(Core.CurrentUser.ProfilObject.Url).Equals(hsp.ProfilObject.Url);
                                    //else
                                    //    CommentControl1.IsCommandActive = false;
                                    //CommentControl1.ModulID = "yazarhakkinda";
                                    //CommentControl1.IcerikID = Request.QueryString["url"];
                                    ltrContent.Text = ltrContent.Text.Replace("%DetailBlock%", "");
                                    break;
                                case ViewType.Contact:
                                    this.Page.Title += " - İletişim Formu";
                                    contact1.HesapID = hsp.ID.ToString();
                                    ltrContent.Text = ltrContent.Text.Replace("%DetailBlock%", "");
                                    break;
                            }
                            //Gösterim Bilgisi
                            switch (Settings.Site.CounterView)
                            {
                                case CounterViewType.Hidden:
                                    ltrContent.Text = ltrContent.Text.Replace("%Gosterim%", "");
                                    return;
                                default:
                                    ltrContent.Text = ltrContent.Text.Replace("%Gosterim%", "PR:&nbsp;" + GosterimMethods.Count("profil", hsp.ID));
                                    break;
                            }
                        }
                    }
                    else
                        Response.Redirect(Settings.VirtualPath + "?ref=profil", false);
                }
                catch (Exception ex)
                {
                    Response.Redirect(Settings.VirtualPath + "?ref=profil-error&ex=" + ex.Message, false);
                }
            }
        }

        private void GetData(string pHesapID)
        {
            //using (BAYMYO.UI.Web.CustomSqlQuery data = new BAYMYO.UI.Web.CustomSqlQuery(rptMakaleler, "makale", "kayittarihi desc", "yoneticionay=1 and aktif=1"))
            //{
            //    data.Where += " and hesapid=?hesapid";
            //    data.Parameters.Add("hesapid", pHesapID, BAYMYO.MultiSQLClient.MSqlDbType.VarChar);
            //    data.Top = 5;
            //    data.Execute();
            //    if (rptMakaleler.Items.Count < 1)
            //    {
            //        ltrMessage.Visible = true;
            //        ltrMessage.Text = "<div style=\"display:inline-block;\">" + MessageBox.IsNotViews() + "</div>";
            //    }
            //}
        }
    }
}