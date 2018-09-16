using System;
using System.Collections.Generic;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace baymyoStatic.common.ascx
{
    public partial class myaccount : System.Web.UI.UserControl
    {
        protected override void OnInit(EventArgs e)
        {
            if (!Core.IsUserActive)
            {
                CustomizeControl1.PanelVisible = false;
                Response.Redirect(Settings.VirtualPath + "?go=login", false);
                return;
            }

            using (Hesap hsp = Core.CurrentUser)
            {
                if (!string.IsNullOrEmpty(hsp.ID))
                {
                    this.Page.Title = hsp.Adi + " " + hsp.Soyadi + " - Hesap Ayarları";
                    System.Web.UI.HtmlControls.HtmlMeta meta = new System.Web.UI.HtmlControls.HtmlMeta();
                    meta.Attributes.Add("name", "googlebot");
                    meta.Attributes.Add("content", "noindex");
                    this.Page.Header.Controls.Add(meta);

                    CustomizeControl1.AddTitle("Hesap Bilgileri");
                    CustomizeControl1.RemoveVisible = false;
                    TextBox txt = new TextBox();
                    txt.ID = "hspAdi";
                    txt.Text = hsp.Adi;
                    txt.CssClass = "form-control";
                    txt.MaxLength = 18;
                    CustomizeControl1.AddControl("Adı", txt);

                    txt = new TextBox();
                    txt.ID = "hspSoyadi";
                    txt.CssClass = "form-control";
                    txt.Text = hsp.Soyadi;
                    txt.MaxLength = 15;
                    CustomizeControl1.AddControl("Soyadı", txt);

                    txt = new TextBox();
                    txt.ID = "hspMail";
                    txt.CssClass = "form-control";
                    txt.Text = hsp.Mail;
                    txt.MaxLength = 60;
                    txt.Enabled = true;
                    txt.ReadOnly = true;
                    CustomizeControl1.AddControl("Mail", txt, "Sisteme giriş yapmak için kullanılacaktır.");

                    txt = new TextBox();
                    txt.ID = "hspSifre";
                    txt.CssClass = "form-control noHtml";
                    txt.ToolTip = hsp.Sifre;
                    txt.TextMode = TextBoxMode.Password;
                    txt.MaxLength = 25;
                    CustomizeControl1.AddControl("Şifre", txt, "Şifreyi değiştirmek istemiyorsanız boş bırakınız!");

                    DateTimeControl cnt = this.Page.LoadControl(Settings.DateTimeControlPath) as DateTimeControl;
                    cnt.ID = "DogumTarihi";
                    cnt.FormatType = FormatTypes.BirthDate;
                    CustomizeControl1.AddControl("Doğum Tarihi", cnt, "* Seçilmesi zorunlu alan.");
                    cnt.Date = hsp.DogumTarihi;

                    DropDownList ddl = new DropDownList();
                    ddl.ID = "hspCinsiyet";
                    ddl.Width = 195;
                    ddl.DataValueField = "Key";
                    ddl.DataTextField = "Value";
                    ddl.DataSource = Core.GetSexTypes();
                    ddl.DataBind();
                    ddl.SelectedValue = BAYMYO.UI.Converts.NullToByte(hsp.Cinsiyet).ToString();
                    CustomizeControl1.AddControl("Cinsiyet", ddl);

                    CheckBox chk = new CheckBox();
                    chk.ID = "hspAbonelik";
                    chk.Checked = hsp.Abonelik;
                    CustomizeControl1.AddControl("Abonelik", chk);

                    if (hsp.ProfilObject != null)
                        switch (hsp.Tipi)
                        {
                            case AccountType.Admin:
                            case AccountType.Private:
                            case AccountType.Doctor:
                            case AccountType.Editor:
                                CustomizeControl1.AddTitle("Profil Bilgileri");

                                Image img = new Image();
                                img.ID = "prfImageUrl";
                                img.Width = 210;
                                img.ImageUrl = Settings.ImagesPath + ((!string.IsNullOrEmpty(hsp.ProfilObject.ResimUrl)) ? "profil/" + hsp.ProfilObject.ResimUrl : "yok.png");
                                CustomizeControl1.AddControl("Fotoğraf", img);

                                FileUpload flu = new FileUpload();
                                flu.ID = "prfResimUrl";
                                CustomizeControl1.AddControl("Yeni Fotoğraf", flu, "Genişlik(W):160px - Yükseklik(H):170px");

                                txt = new TextBox();
                                txt.ID = "prfUrl";
                                txt.Text = hsp.ProfilObject.Url;
                                txt.CssClass = "form-control";
                                txt.MaxLength = 50;
                                CustomizeControl1.AddControl("Url", txt, "Sadece küçük harf ve en az '<b>4</b>' karakter den oluşan içerik girebilirsiniz. Ör. " + Settings.SiteUrl + "<b class=\"toolTip titleFormat1\" title=\"Adres çubuğunda sitemizin adının yanına '/' ters slaş yaparak burada belirteceğiniz isim ile profilinizin görüntülenmesini sağlar.\">adisoyadi</b>");

                                txt = new TextBox();
                                txt.ID = "prfAdi";
                                txt.Text = hsp.ProfilObject.Adi;
                                txt.CssClass = "form-control";
                                txt.MaxLength = 50;
                                CustomizeControl1.AddControl("Başlık", txt, "Profilde gösterilecek olan <b>başlıktır</b>. 'Ör. Sezgin'nin Sayfasına Hoş Geldiniz!'");

                                txt = new TextBox();
                                txt.ID = "prfMail";
                                txt.Text = hsp.ProfilObject.Mail;
                                txt.CssClass = "form-control";
                                txt.MaxLength = 60;
                                CustomizeControl1.AddControl("Profil Maili", txt, "Profilde gösterilecek olan <b>'Mail'</b> adresidir. 'Not: Profiliniz üzerinden bu adrese mail gönderebilecekler!'");

                                txt = new TextBox();
                                txt.ID = "prfWeb";
                                txt.Text = hsp.ProfilObject.Web;
                                txt.CssClass = "form-control noHtml";
                                txt.MaxLength = 60;
                                CustomizeControl1.AddControl("Web Adresi", txt, "Profilde gösterilecek olan <b>'Web Site'</b> adresidir.");

                                txt = new TextBox();
                                txt.ID = "prfTelefon";
                                txt.Text = hsp.ProfilObject.Telefon;
                                txt.CssClass = "form-control noHtml isNumber";
                                txt.MaxLength = 16;
                                CustomizeControl1.AddControl("Telefon", txt, "Profilde gösterilecek olan <b>'Telefon'</b> numarasıdır. Ör. <b>0326 6XX 2X 0X</b>");

                                txt = new TextBox();
                                txt.ID = "prfGSM";
                                txt.Text = hsp.ProfilObject.GSM;
                                txt.CssClass = "form-control noHtml isNumber";
                                txt.MaxLength = 16;
                                CustomizeControl1.AddControl("GSM", txt, "Profilde gösterilecek olan <b>'GSM'</b> numarasıdır. Ör. <b>0544 2XX 4X 5X</b>");

                                ddl = new DropDownList();
                                ddl.ID = "RprfSehir";
                                ddl.Width = 250;
                                ddl.DataMember = "Sehir";
                                ddl.DataValueField = "Adi";
                                ddl.DataTextField = "Adi";
                                SehirCollection sehirler = SehirMethods.GetSelect();
                                sehirler.Insert(0, new Sehir(0, ""));
                                ddl.DataSource = sehirler;
                                ddl.DataBind();
                                ddl.SelectedValue = hsp.ProfilObject.Sehir;
                                CustomizeControl1.AddControl("Şehir (İL)", ddl, "<b>Şehir</b> adı harita üzerinde bulunmasını sağlayacaktır.");

                                ddl = new DropDownList();
                                ddl.ID = "prfMeslekID";
                                ddl.Width = 250;
                                ddl.DataMember = "kategori";
                                ddl.DataValueField = "id";
                                ddl.DataTextField = "adi";
                                ddl.DataSource = KategoriMethods.GetMenu("meslek", true);
                                ddl.DataBind();
                                ddl.SelectedValue = hsp.ProfilObject.Meslek;
                                CustomizeControl1.AddControl("Meslek", ddl, "* Seçmesi zorunlu alan.");

                                ddl = new DropDownList();
                                ddl.ID = "prfEgitimID";
                                ddl.Width = 250;
                                ddl.DataMember = "kategori";
                                ddl.DataValueField = "id";
                                ddl.DataTextField = "adi";
                                ddl.DataSource = KategoriMethods.GetMenu("egitim", true);
                                ddl.DataBind();
                                ddl.SelectedValue = hsp.ProfilObject.Egitim;
                                CustomizeControl1.AddControl("Eğitim", ddl, "* Seçmesi zorunlu alan.");

                                txt = new TextBox();
                                txt.ID = "prfHakkimda";
                                txt.Text = hsp.ProfilObject.Hakkimda;
                                txt.CssClass = "form-control noHtml";
                                txt.Height = 150;
                                txt.TextMode = TextBoxMode.MultiLine;
                                txt.MaxLength = 500;
                                CustomizeControl1.AddControl("Hakkimda", txt, "Bu alana <b>500</b> karaktere kadar bilgi girişi yapabilirsiniz.");

                                CustomizeControl1.SubmitClick += new CustomizeControl.ButtonEvent(editorHesap_SubmitClick);
                                break;
                            default:
                                CustomizeControl1.SubmitClick += new CustomizeControl.ButtonEvent(standartHesap_SubmitClick);
                                break;
                        }
                }
            }
            base.OnInit(e);
        }

        void standartHesap_SubmitClick(SortedDictionary<string, Control> controls)
        {
            if (Core.IsUserActive
                & !string.IsNullOrEmpty(((TextBox)controls["hspAdi"]).Text)
                & !string.IsNullOrEmpty(((TextBox)controls["hspMail"]).Text))
                using (Hesap hsp = Core.CurrentUser)
                {
                    hsp.IP = Context.Request.ServerVariables["REMOTE_ADDR"].ToString();
                    hsp.Adi = ((TextBox)controls["hspAdi"]).Text;
                    hsp.Soyadi = ((TextBox)controls["hspSoyadi"]).Text;
                    hsp.Mail = ((TextBox)controls["hspMail"]).Text;
                    if (!string.IsNullOrEmpty((controls["hspSifre"] as TextBox).Text.Trim()))
                    {
                        string sifre = System.Web.Security.FormsAuthentication.HashPasswordForStoringInConfigFile((controls["hspSifre"] as TextBox).Text, "md5");
                        if (!hsp.Sifre.Equals(sifre))
                        {
                            hsp.Sifre = sifre;
                            string m_MailMesaj = BAYMYO.UI.FileIO.ReadText(Server.MapPath(Settings.ViewPath) + "PasswordNew.view");
                            m_MailMesaj = m_MailMesaj.Replace("%SiteUrl%", Settings.SiteUrl);
                            m_MailMesaj = m_MailMesaj.Replace("%SiteTitle%", Settings.Site.Title);
                            m_MailMesaj = m_MailMesaj.Replace("%VirtualPath%", Settings.VirtualPath);
                            m_MailMesaj = m_MailMesaj.Replace("%IP%", Context.Request.ServerVariables["REMOTE_ADDR"].ToString());
                            m_MailMesaj = m_MailMesaj.Replace("%ID%", hsp.ID.ToString());
                            m_MailMesaj = m_MailMesaj.Replace("%Adi%", hsp.Adi).Replace("%Soyadi%", hsp.Soyadi);
                            m_MailMesaj = m_MailMesaj.Replace("%Mail%", hsp.Mail);
                            m_MailMesaj = m_MailMesaj.Replace("%Sifre%", ((TextBox)controls["hspSifre"]).Text);
                            Core.SendMail(hsp.Mail, hsp.Adi + " " + hsp.Soyadi, "Şifre Değiştirildi", m_MailMesaj, true);
                            m_MailMesaj = null;
                        }
                    }
                    hsp.DogumTarihi = ((DateTimeControl)controls["DogumTarihi"]).Date;
                    hsp.Cinsiyet = Core.GetSexType(BAYMYO.UI.Converts.NullToByte(((DropDownList)controls["hspCinsiyet"]).SelectedValue));
                    hsp.OnayKodu = Core.GenerateSecurityCode();
                    hsp.Abonelik = ((CheckBox)controls["hspAbonelik"]).Checked;

                    if (!string.IsNullOrEmpty(hsp.ID))
                        switch (HesapMethods.Update(hsp))
                        {
                            case "EMAIL":
                                CustomizeControl1.MessageText = MessageBox.Show(DialogResult.Stop, "Kayıt olmak istediğiniz <b>'E-Mail'</b> adresi başkası tarafından kullanılıyor! Lütfen başka bir 'E-Mail' adresi ile tekrar deneyiniz yada eğer bu e-mail adresinin sizin olduğundan eminseniz şifremi unuttum kısımından tekrar şifre talebinde bulununuz!");
                                break;
                            default:
                                CustomizeControl1.MessageText = MessageBox.Show(ProccesType.Update, DialogResult.Succes);
                                break;
                        }
                }
        }

        void editorHesap_SubmitClick(SortedDictionary<string, Control> controls)
        {
            if (Core.IsUserActive
                & !string.IsNullOrEmpty(((TextBox)controls["hspAdi"]).Text)
                & !string.IsNullOrEmpty(((TextBox)controls["hspMail"]).Text)
                & ((TextBox)controls["prfUrl"]).Text.Length >= 4
                & !string.IsNullOrEmpty(((TextBox)controls["prfAdi"]).Text)
                & !string.IsNullOrEmpty(((TextBox)controls["prfMail"]).Text)
                & ((DropDownList)controls["prfMeslekID"]).SelectedIndex > 0
                & ((DropDownList)controls["prfEgitimID"]).SelectedIndex > 0)
                using (Hesap hsp = Core.CurrentUser)
                {
                    hsp.IP = Context.Request.ServerVariables["REMOTE_ADDR"].ToString();
                    hsp.Adi = ((TextBox)controls["hspAdi"]).Text;
                    hsp.Soyadi = ((TextBox)controls["hspSoyadi"]).Text;
                    hsp.Mail = ((TextBox)controls["hspMail"]).Text;
                    if (!string.IsNullOrEmpty((controls["hspSifre"] as TextBox).Text.Trim()))
                    {
                        string sifre = System.Web.Security.FormsAuthentication.HashPasswordForStoringInConfigFile((controls["hspSifre"] as TextBox).Text, "md5");
                        if (!hsp.Sifre.Equals(sifre))
                        {
                            hsp.Sifre = sifre;
                            string m_MailMesaj = BAYMYO.UI.FileIO.ReadText(Server.MapPath(Settings.ViewPath) + "PasswordNew.view");
                            m_MailMesaj = m_MailMesaj.Replace("%SiteUrl%", Settings.SiteUrl);
                            m_MailMesaj = m_MailMesaj.Replace("%SiteTitle%", Settings.Site.Title);
                            m_MailMesaj = m_MailMesaj.Replace("%VirtualPath%", Settings.VirtualPath);
                            m_MailMesaj = m_MailMesaj.Replace("%IP%", Context.Request.ServerVariables["REMOTE_ADDR"].ToString());
                            m_MailMesaj = m_MailMesaj.Replace("%ID%", hsp.ID.ToString());
                            m_MailMesaj = m_MailMesaj.Replace("%Adi%", hsp.Adi).Replace("%Soyadi%", hsp.Soyadi);
                            m_MailMesaj = m_MailMesaj.Replace("%Mail%", hsp.Mail);
                            m_MailMesaj = m_MailMesaj.Replace("%Sifre%", ((TextBox)controls["hspSifre"]).Text);
                            Core.SendMail(hsp.Mail, hsp.Adi + " " + hsp.Soyadi, "Şifre Değiştirildi", m_MailMesaj, true);
                            m_MailMesaj = null;
                        }
                    }
                    hsp.DogumTarihi = ((DateTimeControl)controls["DogumTarihi"]).Date;
                    hsp.Cinsiyet = Core.GetSexType(BAYMYO.UI.Converts.NullToByte(((DropDownList)controls["hspCinsiyet"]).SelectedValue));
                    hsp.OnayKodu = Core.GenerateSecurityCode();
                    hsp.Abonelik = ((CheckBox)controls["hspAbonelik"]).Checked;

                    hsp.ProfilObject.Url = ((TextBox)controls["prfUrl"]).Text;
                    hsp.ProfilObject.Adi = BAYMYO.UI.Commons.SubStringText(((TextBox)controls["prfAdi"]).Text, 50);
                    hsp.ProfilObject.Mail = ((TextBox)controls["prfMail"]).Text;
                    hsp.ProfilObject.Web = BAYMYO.UI.Commons.SubStringText(((TextBox)controls["prfWeb"]).Text, 60).ToLower().Replace("http://", "");
                    hsp.ProfilObject.Telefon = ((TextBox)controls["prfTelefon"]).Text;
                    hsp.ProfilObject.GSM = ((TextBox)controls["prfGSM"]).Text;
                    hsp.ProfilObject.Sehir = BAYMYO.UI.Converts.NullToString(((DropDownList)controls["RprfSehir"]).SelectedValue);
                    hsp.ProfilObject.Meslek = ((DropDownList)controls["prfMeslekID"]).SelectedValue;
                    hsp.ProfilObject.Egitim = ((DropDownList)controls["prfEgitimID"]).SelectedValue;
                    hsp.ProfilObject.Hakkimda = BAYMYO.UI.Commons.SubStringText(((TextBox)controls["prfHakkimda"]).Text, 500);
                    if (!string.IsNullOrEmpty(hsp.ID))
                    {
                        switch (HesapMethods.Update(hsp))
                        {
                            case "EMAIL":
                                CustomizeControl1.MessageText = MessageBox.Show(DialogResult.Stop, "Kayıt olmak istediğiniz <b>'E-Mail'</b> adresi başkası tarafından kullanılıyor! Lütfen başka bir 'E-Mail' adresi ile tekrar deneyiniz yada eğer bu e-mail adresinin sizin olduğundan eminseniz şifremi unuttum kısımından tekrar şifre talebinde bulununuz!");
                                break;
                            default:
                                if ((controls["prfResimUrl"] as FileUpload).HasFile)
                                    if (BAYMYO.UI.FileIO.Remove(Server.MapPath(Settings.ImagesPath + "profil/" + hsp.ProfilObject.ResimUrl)))
                                        hsp.ProfilObject.ResimUrl = BAYMYO.UI.FileIO.UploadImage(controls["prfResimUrl"] as FileUpload, hsp.Adi + " " + hsp.Soyadi, Server.MapPath(Settings.ImagesPath + "profil/"), 260, true); ;
                                if (string.IsNullOrEmpty(hsp.ProfilObject.ID))
                                {
                                    hsp.ProfilObject.ID = hsp.ID;
                                    switch (ProfilMethods.Insert(hsp.ProfilObject))
                                    {
                                        case "":
                                        case "0":
                                            MessageBox.Show(Page, "Profil bilgilerinizi kontrol edip tekrar deneyiniz!");
                                            break;
                                        case "URL":
                                            CustomizeControl1.MessageText = MessageBox.Show(DialogResult.Warning, "Belirttiğiniz <b>'URL'</b> başka bir kullanıcı tarafından kullanılmaktadır. Lütfen başka bir <b>'URL'</b> yazarak tekrar deneyiniz.");
                                            break;
                                        case "ADI":
                                            CustomizeControl1.MessageText = MessageBox.Show(DialogResult.Warning, "Belirttiğiniz <b>'BAŞLIK'</b> başka bir kullanıcı tarafından kullanılmaktadır. Lütfen 'Profil Başlığınızı' kontrol ediniz ve tekrar deneyiniz.");
                                            break;
                                        default:
                                            if ((controls["prfResimUrl"] as FileUpload).HasFile)
                                                ((Image)controls["prfImageUrl"]).ImageUrl = Settings.ImagesPath + "profil/" + hsp.ProfilObject.ResimUrl;
                                            CustomizeControl1.MessageText = MessageBox.Show(ProccesType.Update, DialogResult.Succes);
                                            break;
                                    }
                                }
                                else
                                {
                                    switch (ProfilMethods.Update(hsp.ProfilObject))
                                    {
                                        case "":
                                        case "0":
                                            MessageBox.Show(Page, "Profil bilgilerinizi kontrol edip tekrar deneyiniz!");
                                            break;
                                        case "URL":
                                            CustomizeControl1.MessageText = MessageBox.Show(DialogResult.Warning, "Belirttiğiniz <b>'URL'</b> başka bir kullanıcı tarafından kullanılmaktadır. Lütfen başka bir <b>'URL'</b> yazarak tekrar deneyiniz.");
                                            break;
                                        case "ADI":
                                            CustomizeControl1.MessageText = MessageBox.Show(DialogResult.Warning, "Belirttiğiniz <b>'BAŞLIK'</b> başka bir kullanıcı tarafından kullanılmaktadır. Lütfen 'Profil Başlığınızı' kontrol ediniz ve tekrar deneyiniz.");
                                            break;
                                        default:
                                            if ((controls["prfResimUrl"] as FileUpload).HasFile)
                                                ((Image)controls["prfImageUrl"]).ImageUrl = Settings.ImagesPath + "profil/" + hsp.ProfilObject.ResimUrl;
                                            CustomizeControl1.MessageText = MessageBox.Show(ProccesType.Update, DialogResult.Succes);
                                            break;
                                    }
                                }
                                break;
                        }
                    }
                }
        }

        protected void Page_Load(object sender, EventArgs e)
        {

        }
    }
}