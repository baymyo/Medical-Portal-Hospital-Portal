using System;
using System.Collections.Generic;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace baymyoStatic.panel.ascx
{
    public partial class portalayarlari : System.Web.UI.UserControl
    {
        protected override void OnInit(EventArgs e)
        {
            CustomizeControl1.FormTitle = string.Format(Settings.FormTitleFormat, "Portal", "Ayarları");
            CustomizeControl1.RemoveVisible = false;

            //CustomizeControl1.AddTitle("Site Logoları");

            //Image img = new Image();
            //img.ID = "FaviconImage";
            //img.ImageUrl = Settings.ImagesPath + "favicon.ico";
            //CustomizeControl1.AddControl("Favicon", img, "24x24 yada 16x16 <b>*.ico</b> resim dosyasıdır.");

            //FileUpload flu = new FileUpload();
            //flu.ID = "FaviconLogo";
            //CustomizeControl1.AddControl("Yeni Favicon", flu);

            //img = new Image();
            //img.ID = "SiteLogoImage";
            //img.ImageUrl = Settings.ImagesPath + "logo.png";
            //CustomizeControl1.AddControl("Üst Logo", img);

            //flu = new FileUpload();
            //flu.ID = "SiteLogo";
            //CustomizeControl1.AddControl("Yeni Üst Logo", flu, "Genişlik(W):250px - Yükseklik(H):67px");

            //img = new Image();
            //img.ID = "FooterLogoImage";
            //img.ImageUrl = Settings.ImagesPath + "footerLogo.png";
            //CustomizeControl1.AddControl("Alt Logo", img);

            //flu = new FileUpload();
            //flu.ID = "FooterLogo";
            //CustomizeControl1.AddControl("Yeni Alt Logo", flu);

            //CustomizeControl1.AddTitle("Foto Galeri Logo");

            //img = new Image();
            //img.ID = "GaleriLogoImage";
            //img.ImageUrl = Settings.ImagesPath + "galeriLogo.png";
            //CustomizeControl1.AddControl("Foto Galeri Logo", img);

            //flu = new FileUpload();
            //flu.ID = "GaleriLogo";
            //CustomizeControl1.AddControl("Yeni Galeri Logo", flu);

            //CustomizeControl1.AddTitle("Web TV Logo");

            //img = new Image();
            //img.ID = "VideoLogoImage";
            //img.ImageUrl = Settings.ImagesPath + "videoLogo.png";
            //CustomizeControl1.AddControl("Foto Video Logo", img);

            //flu = new FileUpload();
            //flu.ID = "VideoLogo";
            //CustomizeControl1.AddControl("Yeni Video Logo", flu);

            using (Portal p = PortalMethods.Read())
            {
                //CustomizeControl1.AddTitle("Site Bilgileri");

                TextBox txt = new TextBox();
                txt.ID = "Title";
                txt.CssClass = "form-control";
                txt.MaxLength = 150;
                txt.Text = p.Title;
                CustomizeControl1.AddControl("Başlık", txt);

                txt = new TextBox();
                txt.ID = "Description";
                txt.CssClass = "form-control";
                txt.MaxLength = 200;
                txt.Text = p.Description;
                CustomizeControl1.AddControl("Description", txt);

                txt = new TextBox();
                txt.ID = "Keywords";
                txt.CssClass = "form-control";
                txt.MaxLength = 250;
                txt.Text = p.Keywords;
                CustomizeControl1.AddControl("Keywords", txt);

                txt = new TextBox();
                txt.ID = "CookieName";
                txt.CssClass = "form-control";
                txt.MaxLength = 20;
                txt.Text = p.CookieName;
                CustomizeControl1.AddControl("Cookie Name", txt);

                txt = new TextBox();
                txt.ID = "Copyright";
                txt.CssClass = "form-control";
                txt.Text = BAYMYO.UI.FileIO.ReadText(Server.MapPath(Settings.ViewPath + "Copyright.view"));
                CustomizeControl1.AddControl("Copyright", txt);

                CustomizeControl1.AddTitle("Site Map Ayarları");

                DropDownList ddl = new DropDownList();
                ddl.ID = "ChangeFreq";
                ddl.Width = 300;
                ddl.CssClass = "form-control";
                ddl.Items.Insert(0, new ListItem("Always (Herzaman güncellenir.)", "always"));
                ddl.Items.Insert(1, new ListItem("Hourly (Saatlik güncellenir.)", "hourly"));
                ddl.Items.Insert(2, new ListItem("Daily (Günlük güncellenir.)", "daily"));
                ddl.Items.Insert(3, new ListItem("Weekly (Haftalık güncellenir.)", "weekly"));
                ddl.Items.Insert(4, new ListItem("Monthly (Aylık güncellenir.)", "monthly"));
                ddl.Items.Insert(5, new ListItem("Yearly (Yıllık güncellenir.)", "yearly"));
                ddl.Items.Insert(6, new ListItem("Never (Asla güncellenmez!)", "never"));
                ddl.SelectedValue = p.ChangeFreq;
                CustomizeControl1.AddControl("ChangeFreq", ddl, "Site maps dosyasıda bulunan bağlantıların güncellenme aralığı.");

                ddl = new DropDownList();
                ddl.ID = "Priority";
                ddl.Width = 300;
                ddl.CssClass = "form-control";
                ddl.Items.Insert(0, new ListItem("0.1 zaman aralığı.", "0.1"));
                ddl.Items.Insert(1, new ListItem("0.2 zaman aralığı.", "0.2"));
                ddl.Items.Insert(2, new ListItem("0.3 zaman aralığı.", "0.3"));
                ddl.Items.Insert(3, new ListItem("0.4 zaman aralığı.", "0.4"));
                ddl.Items.Insert(4, new ListItem("0.5 zaman aralığı.", "0.5"));
                ddl.Items.Insert(5, new ListItem("0.6 zaman aralığı.", "0.6"));
                ddl.Items.Insert(6, new ListItem("0.7 zaman aralığı.", "0.7"));
                ddl.Items.Insert(7, new ListItem("0.8 zaman aralığı.", "0.8"));
                ddl.Items.Insert(8, new ListItem("0.9 zaman aralığı.", "0.9"));
                ddl.Items.Insert(9, new ListItem("1.0 zaman aralığı.", "1.0"));
                ddl.SelectedValue = p.Priority;
                CustomizeControl1.AddControl("Priority", ddl);

                CustomizeControl1.AddTitle("Sayfa İçerik Tanımları");

                //txt = new TextBox();
                //txt.ID = "InformationLinks";
                //txt.CssClass = "form-control";
                //txt.Text = p.InformationLinks;
                //CustomizeControl1.AddControl("Künye Link", txt, "Burada belirteceğiniz site bağlantısı,<b><a href=\"/kunye.html\" target=\"_blank\">kunye.html</a>,<a href=\"/info.html\" target=\"_blank\">info.html</a>,<a href=\"/information.html\" target=\"_blank\">information.html</a></b> olarak kısaltılacaktır.");

                //txt = new TextBox();
                //txt.ID = "AboutMeLinks";
                //txt.CssClass = "form-control";
                //txt.Text = p.AboutMeLinks;
                //CustomizeControl1.AddControl("Hakkinda Link", txt, "Burada belirteceğiniz site bağlantısı,<b><a href=\"/hakkinda.html\" target=\"_blank\">hakkinda.html</a>,<a href=\"/hakkimizda.html\" target=\"_blank\">hakkimizda.html</a>,<a href=\"/aboutme.html\" target=\"_blank\">aboutme.html</a>,<a href=\"/aboutus.html\" target=\"_blank\">aboutus.html</a></b> olarak kısaltılacaktır.");

                //ddl = new DropDownList();
                //ddl.ID = "IsCategoryColor";
                //ddl.Width = 300;
                //ddl.Items.Insert(0, new ListItem("Kategori renkleri pasif!", "0"));
                //ddl.Items.Insert(1, new ListItem("Kategori renkleri arkaplan olarak aktif!", "1"));
                //ddl.SelectedIndex = BAYMYO.UI.Converts.NullToByte(p.IsCategoryColor);
                //CustomizeControl1.AddControl("Kategorilerde", ddl);

                //ddl = new DropDownList();
                //ddl.ID = "AllCategories";
                //ddl.Width = 300;
                //ddl.Items.Insert(0, new ListItem("Menü'de 'TÜMÜ' öğesini gizle!", "0"));
                //ddl.Items.Insert(1, new ListItem("Menü'de 'TÜMÜ' öğesini göster!", "1"));
                //ddl.SelectedIndex = BAYMYO.UI.Converts.NullToByte(p.IsAllCategories);
                //CustomizeControl1.AddControl("Kategorilerde", ddl);

                //ddl = new DropDownList();
                //ddl.ID = "AccountMaps";
                //ddl.Width = 300;
                //ddl.Items.Insert(0, new ListItem("Haritayı Gizle!", "0"));
                //ddl.Items.Insert(1, new ListItem("Haritayı Göster!", "1"));
                //ddl.SelectedIndex = BAYMYO.UI.Converts.NullToByte(p.IsAccountMapsVisible);
                //CustomizeControl1.AddControl("Yazarlar Sayfası", ddl);

                //ddl = new DropDownList();
                //ddl.ID = "AddNews";
                //ddl.Width = 300;
                //ddl.CssClass = "form-control";
                //ddl.Items.Insert(0, new ListItem("Haber gönder sistemi kapalı!", "0"));
                //ddl.Items.Insert(1, new ListItem("Haber gönder sistemi açık!", "1"));
                //ddl.SelectedIndex = BAYMYO.UI.Converts.NullToByte(p.IsAddNews);
                //CustomizeControl1.AddControl("Haber Gönder", ddl, "Üye olmak gibi kısıtlaması olmayan sistemdir. Haber Gönder bağlantısı için <b><a target=\"_blank\" href=\"" + Settings.VirtualPath + "?go=addnews\">buraya tıklayın.</a></b>");

                //ddl = new DropDownList();
                //ddl.ID = "VideoView";
                //ddl.Width = 300;
                //ddl.CssClass = "form-control";
                //ddl.Items.Insert(0, new ListItem("Haber içerisinde 'VIDEO' gösterim kapalı!", "0"));
                //ddl.Items.Insert(1, new ListItem("Haber başında 'VIDEO' göster!", "1"));
                //ddl.Items.Insert(2, new ListItem("Haber sonunda 'VIDEO' göster!", "2"));
                //ddl.SelectedIndex = BAYMYO.UI.Converts.NullToByte(p.IsVideoView);
                //CustomizeControl1.AddControl("Video Gösterim", ddl, "İlişkili <b>VIDEO</b> haber içerisinde gösterime açmak yada kapatmak içindir ilişkili <b>VIDEO</b> bağlantısını <b>kaldırmaz</b>!");

                //ddl = new DropDownList();
                //ddl.ID = "CounterView";
                //ddl.Width = 300;
                //ddl.CssClass = "form-control";
                //ddl.Items.Insert(0, new ListItem("Gizle! Gösterilmez ama sayaç çalışır!", "0"));
                //ddl.Items.Insert(1, new ListItem("Tekil olarak göster!", "1"));
                //ddl.Items.Insert(2, new ListItem("Çoğul olarak göster!", "2"));
                //ddl.SelectedIndex = BAYMYO.UI.Converts.NullToByte(p.CounterView);
                //CustomizeControl1.AddControl("Gösterim Sayısı", ddl);

                //ddl = new DropDownList();
                //ddl.ID = "LinkTarget";
                //ddl.Width = 300;
                //ddl.CssClass = "form-control";
                //ddl.Items.Insert(0, new ListItem("Aynı pencerede açtır!", "_self"));
                //ddl.Items.Insert(1, new ListItem("Yeni pencerede açtır!", "_blank"));
                //ddl.SelectedValue = p.LinkTarget;
                //CustomizeControl1.AddControl("Tüm Bağlantıları", ddl, "Bağlantıları <b>Yeni Pencerede</b> açtırmak sayfanızın görüntülenme oranını artırmaktadır. (Tavsiye edilir.)");

                //ddl = new DropDownList();
                //ddl.ID = "FlashOrder";
                //ddl.Width = 300;
                //ddl.CssClass = "form-control";
                //ddl.Items.Insert(0, new ListItem("Manşet sıralaması kapalı!", "0"));
                //ddl.Items.Insert(1, new ListItem("Manşet sıralaması açık!", "1"));
                //ddl.SelectedIndex = BAYMYO.UI.Converts.NullToByte(p.IsFlashOrder);
                //CustomizeControl1.AddControl("Manşet Sıra", ddl);

                txt = new TextBox();
                txt.ID = "Categories";
                txt.CssClass = "form-control";
                txt.MaxLength = 100;
                txt.Text = string.Format("{0};{1};{2};{3}", p.Category1.ID, p.Category2.ID, p.Category3.ID, p.Category4.ID);
                CustomizeControl1.AddControl("Kategoriler", txt, "Renkli Kategori kutularındaki <b>ID</b>'leri bu kısımda sırasıyla noktalı virgül(;) ile ayırarak tanımlanır. (<b>Uzmanla yapınız!</b>)");

                //txt = new TextBox();
                //txt.ID = "WheaterCity";
                //txt.CssClass = "form-control";
                //txt.MaxLength = 100;
                //txt.Text = p.WheaterCity;
                //CustomizeControl1.AddControl("Hava Durumu", txt, "Hava durumu getirilecek <b>İL</b>'leri bu kısımda sırasıyla noktalı virgül(;) ile ayırarak tanımlanır. 4 şehir girebilirsiniz. (<b>Uzmanla yapınız!</b>)");

                CustomizeControl1.AddTitle("İletişim Bilgileri");

                txt = new TextBox();
                txt.ID = "ContactName";
                txt.CssClass = "form-control";
                txt.MaxLength = 50;
                txt.Text = p.ContactName;
                CustomizeControl1.AddControl("Görünen Adı", txt, "Mail gönderimlerinde kullanılacak isimlendirme. Örnek: <b>(?) Site Yönetimi</b> yada <b>sitenizinadi.com</b> gibi isimler verebilirsiniz.");

                txt = new TextBox();
                txt.ID = "ContactMail";
                txt.CssClass = "form-control";
                txt.MaxLength = 90;
                txt.Text = p.ContactMail;
                txt.TextMode = TextBoxMode.Email;
                CustomizeControl1.AddControl("Mail Adresi", txt);

                CustomizeControl1.AddTitle("SMTP(Mail) Ayarları");

                txt = new TextBox();
                txt.ID = "SmtpMail";
                txt.CssClass = "form-control";
                txt.MaxLength = 90;
                txt.Text = p.SmtpMail;
                txt.TextMode = TextBoxMode.Email;
                CustomizeControl1.AddControl("Mail", txt);

                txt = new TextBox();
                txt.ID = "SmtpPassword";
                txt.CssClass = "form-control";
                txt.MaxLength = 50;
                txt.Text = p.SmtpPassword;
                CustomizeControl1.AddControl("Password", txt);

                txt = new TextBox();
                txt.ID = "SmtpHost";
                txt.CssClass = "form-control";
                txt.MaxLength = 50;
                txt.Text = p.SmtpHost;
                CustomizeControl1.AddControl("Host", txt);

                txt = new TextBox();
                txt.ID = "SmtpPort";
                txt.CssClass = "form-control";
                txt.MaxLength = 3;
                txt.TextMode = TextBoxMode.Number;
                txt.Text = p.SmtpPort.ToString();
                CustomizeControl1.AddControl("Port", txt);

                ddl = new DropDownList();
                ddl.ID = "SmtpEnableSsl";
                ddl.Width = 300;
                ddl.CssClass = "form-control";
                ddl.Items.Insert(0, new ListItem("Hayır", "0"));
                ddl.Items.Insert(1, new ListItem("Evet", "1"));
                ddl.SelectedIndex = (p.SmtpEnableSsl) ? 1 : 0;
                CustomizeControl1.AddControl("EnableSsl", ddl);

                CustomizeControl1.AddTitle("Sosyal Platform / Google Analytics");

                txt = new TextBox();
                txt.ID = "GoogleAnalytics";
                txt.CssClass = "form-control";
                txt.Height = 50;
                txt.TextMode = TextBoxMode.MultiLine;
                txt.Text = BAYMYO.UI.FileIO.ReadText(Server.MapPath(Settings.ViewPath + "GoogleAnalytics.view"));
                CustomizeControl1.AddControl("Google Analytics", txt);

                txt = new TextBox();
                txt.ID = "GoogleMaps";
                txt.CssClass = "form-control";
                txt.Text = p.GoogleMaps;
                CustomizeControl1.AddControl("Google Maps Key", txt);

                txt = new TextBox();
                txt.ID = "GooglePlusLinks";
                txt.CssClass = "form-control";
                txt.Text = p.GooglePlusLinks;
                CustomizeControl1.AddControl("Google Link", txt);

                txt = new TextBox();
                txt.ID = "FaceBookLinks";
                txt.CssClass = "form-control";
                txt.Text = p.FaceBookLinks;
                CustomizeControl1.AddControl("FaceBook Link", txt);

                txt = new TextBox();
                txt.ID = "TwitterLinks";
                txt.CssClass = "form-control";
                txt.Text = p.TwitterLinks;
                CustomizeControl1.AddControl("Twitter Link", txt);

                txt = new TextBox();
                txt.ID = "YouTubeLinks";
                txt.CssClass = "form-control";
                txt.Text = p.YouTubeLinks;
                CustomizeControl1.AddControl("YouTube Link", txt);

                txt = new TextBox();
                txt.ID = "InstagramLinks";
                txt.CssClass = "form-control";
                txt.Text = p.InstagramLinks;
                CustomizeControl1.AddControl("Instagram Link", txt);

                txt = new TextBox();
                txt.ID = "FeedBurnerLinks";
                txt.CssClass = "form-control";
                txt.Text = p.FeedBurnerLinks;
                CustomizeControl1.AddControl("FeedBurner Link", txt, "Örnek: <b><a href=\"http://feeds.feedburner.com/baymyo\">http://feeds.feedburner.com/baymyo</a></b> gibi kayıt yaptırınız.");

                //CustomizeControl1.AddTitle("Facebook Comments <var>(Yönetebilmeniz için aşağıdaki ayarları yapmanız gereklidir.)</var>");

                //txt = new TextBox();
                //txt.ID = "FaceBookApi";
                //txt.CssClass = "form-control";
                //txt.Text = p.FaceBookApi;
                //CustomizeControl1.AddControl("Api Key", txt, "<b>242442489108173</b> uygulamanızın <b>facebook api key</b>ini giriniz.");

                //txt = new TextBox();
                //txt.ID = "FaceBookAdminUrl";
                //txt.CssClass = "form-control";
                //txt.Text = p.FaceBookAdminUrl;
                //CustomizeControl1.AddControl("Admin Url", txt, "<b>https://www.facebook.com/baymyo</b> uygulamaya bağlı <b>admin</b>in sayfa bağlantısını giriniz.");

                //ddl = new DropDownList();
                //ddl.ID = "FaceBookComment";
                //ddl.Width = 300;
                //ddl.CssClass = "form-control";
                //ddl.Items.Insert(0, new ListItem("Hayır, gösterilmesin!", "0"));
                //ddl.Items.Insert(1, new ListItem("Evet, aktif olsun!", "1"));
                //ddl.SelectedIndex = (p.FaceBookComment) ? 1 : 0;
                //CustomizeControl1.AddControl("Comments Enabled", ddl);

                CustomizeControl1.SubmitClick += new CustomizeControl.ButtonEvent(CustomizeControl1_SubmitClick);
            }
            base.OnInit(e);
        }

        void CustomizeControl1_SubmitClick(SortedDictionary<string, Control> controls)
        {
            try
            {
                string[] categories = ((TextBox)controls["Categories"]).Text.Split(';');
                if (!string.IsNullOrEmpty(((TextBox)controls["Title"]).Text)
                    & !string.IsNullOrEmpty(((TextBox)controls["Description"]).Text)
                    & !string.IsNullOrEmpty(((TextBox)controls["Keywords"]).Text)
                    & categories.Length >= 3)
                {
                    using (Portal p = new Portal())
                    {
                        //if ((controls["SiteLogo"] as FileUpload).HasFile)
                        //    if (BAYMYO.UI.FileIO.Remove(Server.MapPath(Settings.ImagesPath + "logo.png")))
                        //        BAYMYO.UI.FileIO.Upload(controls["SiteLogo"] as FileUpload, Server.MapPath(Settings.ImagesPath), "logo.png");

                        //if ((controls["FooterLogo"] as FileUpload).HasFile)
                        //    if (BAYMYO.UI.FileIO.Remove(Server.MapPath(Settings.ImagesPath + "footerLogo.png")))
                        //        BAYMYO.UI.FileIO.Upload(controls["FooterLogo"] as FileUpload, Server.MapPath(Settings.ImagesPath), "footerLogo.png");

                        //if ((controls["GaleriLogo"] as FileUpload).HasFile)
                        //    if (BAYMYO.UI.FileIO.Remove(Server.MapPath(Settings.ImagesPath + "galeriLogo.png")))
                        //        BAYMYO.UI.FileIO.Upload(controls["GaleriLogo"] as FileUpload, Server.MapPath(Settings.ImagesPath), "galeriLogo.png");

                        //if ((controls["VideoLogo"] as FileUpload).HasFile)
                        //    if (BAYMYO.UI.FileIO.Remove(Server.MapPath(Settings.ImagesPath + "videoLogo.png")))
                        //        BAYMYO.UI.FileIO.Upload(controls["VideoLogo"] as FileUpload, Server.MapPath(Settings.ImagesPath), "videoLogo.png");

                        //if ((controls["FaviconLogo"] as FileUpload).HasFile)
                        //    if (BAYMYO.UI.FileIO.Remove(Server.MapPath(Settings.ImagesPath + "favicon.ico")))
                        //        BAYMYO.UI.FileIO.Upload(controls["FaviconLogo"] as FileUpload, Server.MapPath(Settings.ImagesPath), "favicon.ico");

                        BAYMYO.UI.FileIO.WriteText(Server.MapPath(Settings.ViewPath + "Copyright.view"), ((TextBox)controls["Copyright"]).Text, System.Text.Encoding.UTF8);
                        BAYMYO.UI.FileIO.WriteText(Server.MapPath(Settings.ViewPath + "GoogleAnalytics.view"), ((TextBox)controls["GoogleAnalytics"]).Text, System.Text.Encoding.UTF8);

                        p.Title = ((TextBox)controls["Title"]).Text;
                        p.Description = ((TextBox)controls["Description"]).Text;
                        p.Keywords = ((TextBox)controls["Keywords"]).Text;
                        p.CookieName = ((TextBox)controls["CookieName"]).Text;
                        p.ContactName = ((TextBox)controls["ContactName"]).Text;
                        p.ContactMail = ((TextBox)controls["ContactMail"]).Text;
                        p.SmtpMail = ((TextBox)controls["SmtpMail"]).Text;
                        p.SmtpPassword = ((TextBox)controls["SmtpPassword"]).Text;
                        p.SmtpHost = ((TextBox)controls["SmtpHost"]).Text;
                        p.SmtpPort = BAYMYO.UI.Converts.NullToInt(((TextBox)controls["SmtpPort"]).Text);
                        p.SmtpEnableSsl = BAYMYO.UI.Converts.NullToBool(((DropDownList)controls["SmtpEnableSsl"]).SelectedValue);
                        p.ChangeFreq = BAYMYO.UI.Converts.NullToString(((DropDownList)controls["ChangeFreq"]).SelectedValue);
                        p.Priority = BAYMYO.UI.Converts.NullToString(((DropDownList)controls["Priority"]).SelectedValue);
                        //p.InformationLinks = ((TextBox)controls["InformationLinks"]).Text;
                        //p.AboutMeLinks = ((TextBox)controls["AboutMeLinks"]).Text;
                        p.GoogleMaps = ((TextBox)controls["GoogleMaps"]).Text;
                        p.GooglePlusLinks = ((TextBox)controls["GooglePlusLinks"]).Text;
                        p.FaceBookLinks = ((TextBox)controls["FaceBookLinks"]).Text;
                        p.TwitterLinks = ((TextBox)controls["TwitterLinks"]).Text;
                        p.YouTubeLinks = ((TextBox)controls["YouTubeLinks"]).Text;
                        p.InstagramLinks = ((TextBox)controls["InstagramLinks"]).Text;
                        p.FeedBurnerLinks = ((TextBox)controls["FeedBurnerLinks"]).Text;
                        //p.FaceBookApi = ((TextBox)controls["FaceBookApi"]).Text;
                        //p.FaceBookAdminUrl = ((TextBox)controls["FaceBookAdminUrl"]).Text;
                        //p.FaceBookComment = BAYMYO.UI.Converts.NullToBool(((DropDownList)controls["FaceBookComment"]).SelectedValue);
                        //p.LinkTarget = ((DropDownList)controls["LinkTarget"]).SelectedValue;
                        p.Category1 = PortalMethods.GetCategory(categories[0]);
                        p.Category2 = PortalMethods.GetCategory(categories[1]);
                        p.Category3 = PortalMethods.GetCategory(categories[2]);
                        p.Category4 = PortalMethods.GetCategory(categories[3]);
                        //p.WheaterCity = ((TextBox)controls["WheaterCity"]).Text;
                        //p.IsAccountMapsVisible = BAYMYO.UI.Converts.NullToBool(((DropDownList)controls["AccountMaps"]).SelectedValue);
                        //p.CounterView = (CounterViewType)((DropDownList)controls["CounterView"]).SelectedIndex;
                        //p.IsCategoryColor = BAYMYO.UI.Converts.NullToBool(((DropDownList)controls["IsCategoryColor"]).SelectedValue);
                        //p.IsAllCategories = BAYMYO.UI.Converts.NullToBool(((DropDownList)controls["AllCategories"]).SelectedValue);
                        //p.IsAddNews = BAYMYO.UI.Converts.NullToBool(((DropDownList)controls["AddNews"]).SelectedValue);
                        //p.IsFlashOrder = BAYMYO.UI.Converts.NullToBool(((DropDownList)controls["FlashOrder"]).SelectedValue);
                        //p.IsVideoView = BAYMYO.UI.Converts.NullToByte(((DropDownList)controls["VideoView"]).SelectedValue);
                        if (PortalMethods.Save(p))
                            CustomizeControl1.MessageText = MessageBox.Show(ProccesType.Insert, DialogResult.Succes);
                        else
                            CustomizeControl1.MessageText = MessageBox.Show(ProccesType.Insert, DialogResult.Stop);
                    }
                }
            }
            catch (Exception ex)
            {
                CustomizeControl1.MessageText = MessageBox.Show(DialogResult.Error, ex.Message);
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
        }
    }
}