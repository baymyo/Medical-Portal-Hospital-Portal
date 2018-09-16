using System;
using System.Collections.Generic;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace baymyoStatic.common.ascx
{
    public partial class register : System.Web.UI.UserControl
    {
        protected override void OnInit(EventArgs e)
        {
            if (Core.IsUserActive)
            {
                Response.Redirect(Settings.VirtualPath + "?go=myaccount", false);
                return;
            }
            CustomizeControl1.RemoveVisible = false;
            switch (BAYMYO.UI.Converts.NullToString(Request.QueryString["type"]))
            {
                case "2":
                case "editor":
                    CustomizeControl1.FormTitle = string.Format(Settings.FormTitleFormat, "Editör", "Başvuru Formu");
                    StandartHesap("Editör ");
                    EditorHesap();
                    CustomizeControl1.SubmitClick += new CustomizeControl.ButtonEvent(editorHesap_SubmitClick);
                    break;
                case "4":
                case "standart":
                    CustomizeControl1.FormTitle = string.Format(Settings.FormTitleFormat, "Kayıt", "Ol");
                    StandartHesap("");
                    CustomizeControl1.SubmitClick += new CustomizeControl.ButtonEvent(standartHesap_SubmitClick);
                    break;
                default:
                    CustomizeControl1.Visible = false;
                    break;
            }
            base.OnInit(e);
        }

        void StandartHesap(string onEk)
        {
            TextBox txt = new TextBox();
            txt.ID = "rgsAdi";
            txt.CssClass = "form-control";
            txt.MaxLength = 18;
            CustomizeControl1.AddControl(onEk + "Adı", txt);

            txt = new TextBox();
            txt.ID = "rgsSoyadi";
            txt.MaxLength = 15;
            CustomizeControl1.AddControl(onEk + "Soyadı", txt);

            txt = new TextBox();
            txt.ID = "rgsMail";
            txt.CssClass = "form-control";
            txt.TextMode = TextBoxMode.Email;
            txt.MaxLength = 60;
            CustomizeControl1.AddControl("Giriş Maili", txt, "* Bu mail adresi kimseyle paylaşılmaz sadece sisteme giriş için kullanılır.");

            txt = new TextBox();
            txt.ID = "rgsSifre";
            txt.CssClass = "form-control";
            txt.TextMode = TextBoxMode.Password;
            txt.MaxLength = 25;
            CustomizeControl1.AddControl("Şifre", txt, "* Sisteme giriş yapmanız için gerekli olacak.");

            DateTimeControl cnt = this.Page.LoadControl(Settings.DateTimeControlPath) as DateTimeControl;
            cnt.ID = "rgsDogumTarihi";
            cnt.FormatType = FormatTypes.BirthDate;
            CustomizeControl1.AddControl("Doğum Tarihi", cnt, "* Seçilmesi zorunlu alan.");

            DropDownList ddl = new DropDownList();
            ddl.ID = "rgsCinsiyet";
            ddl.Width = 195;
            ddl.CssClass = "form-control";
            ddl.DataValueField = "Key";
            ddl.DataTextField = "Value";
            ddl.DataSource = Core.GetSexTypes();
            ddl.DataBind();
            CustomizeControl1.AddControl("Cinsiyet", ddl);

            CheckBox chk = new CheckBox();
            chk.ID = "rgsAbonelik";
            chk.Checked = true;
            CustomizeControl1.AddControl("Abonelik", chk);
        }

        void EditorHesap()
        {
            CustomizeControl1.AddTitle("Profil Bilgileri");

            Image img = new Image();
            img.ID = "RprfImageUrl";
            img.Width = 210;
            img.ImageUrl = Settings.ImagesPath + "yok.png";
            CustomizeControl1.AddControl("Fotoğraf", img);

            FileUpload flu = new FileUpload();
            flu.ID = "RprfResimUrl";
            CustomizeControl1.AddControl("Yeni Fotoğraf", flu, "Genişlik(W):160px - Yükseklik(H):170px");

            TextBox txt = new TextBox();
            txt.ID = "RprfUrl";
            txt.CssClass = "form-control";
            txt.MaxLength = 50;
            CustomizeControl1.AddControl("Url", txt, "Profil bağlantı adresi olacaktır ve sadece küçük harfler girebilirsiniz. Ör. " + Settings.SiteUrl + "<b class=\"toolTip titleFormat1\" title=\"Adres çubuğunda sitemizin adının yanına '/' ters slaş yaparak burada belirteceğiniz isim ile profilinizin görüntülenmesini sağlar.\">adisoyadi</b>");

            txt = new TextBox();
            txt.ID = "RprfAdi";
            txt.CssClass = "form-control";
            txt.MaxLength = 50;
            CustomizeControl1.AddControl("Başlık", txt, "Profilde gösterilecek olan <b>başlıktır</b>. 'Ör. Sezgin'nin Sayfasına Hoş Geldiniz!'");

            txt = new TextBox();
            txt.ID = "RprfMail";
            txt.CssClass = "form-control";
            txt.TextMode = TextBoxMode.Email;
            txt.MaxLength = 60;
            CustomizeControl1.AddControl("Profil Maili", txt, "Profilde gösterilecek olan <b>'Mail'</b> adresidir. 'Not: Profiliniz üzerinden bu adrese mail gönderebilecekler!'");

            txt = new TextBox();
            txt.ID = "RprfWeb";
            txt.CssClass = "form-control";
            txt.TextMode = TextBoxMode.Url;
            txt.MaxLength = 60;
            CustomizeControl1.AddControl("Web Adresi", txt, "Profilde gösterilecek olan <b>'Web Site'</b> adresidir.");

            txt = new TextBox();
            txt.ID = "RprfTelefon";
            txt.CssClass = "form-control";
            txt.TextMode = TextBoxMode.Phone;
            txt.MaxLength = 16;
            CustomizeControl1.AddControl("Telefon", txt, "Profilde gösterilecek olan <b>'Telefon'</b> numarasıdır. Ör. <b>0326 6XX 2X 0X</b>");

            txt = new TextBox();
            txt.ID = "RprfGSM";
            txt.CssClass = "form-control";
            txt.TextMode = TextBoxMode.Phone;
            txt.MaxLength = 16;
            CustomizeControl1.AddControl("GSM", txt, "Profilde gösterilecek olan <b>'GSM'</b> numarasıdır. Ör. <b>0544 2XX 4X 5X</b>");

            DropDownList ddl = new DropDownList();
            ddl.ID = "RprfSehir";
            ddl.Width = 250;
            ddl.CssClass = "form-control";
            ddl.DataMember = "Sehir";
            ddl.DataValueField = "Adi";
            ddl.DataTextField = "Adi";
            SehirCollection sehirler = SehirMethods.GetSelect();
            sehirler.Insert(0, new Sehir(0, ""));
            ddl.DataSource = sehirler;
            ddl.DataBind();
            CustomizeControl1.AddControl("Şehir (İL)", ddl, "<b>Şehir</b> adı harita üzerinde bulunmasını sağlayacaktır.");

            ddl = new DropDownList();
            ddl.ID = "RprfMeslekID";
            ddl.Width = 250;
            ddl.CssClass = "form-control";
            ddl.DataMember = "kategori";
            ddl.DataValueField = "id";
            ddl.DataTextField = "adi";
            ddl.DataSource = KategoriMethods.GetMenu("meslek", true);
            ddl.DataBind();
            CustomizeControl1.AddControl("Meslek", ddl, "* Seçmesi zorunlu alan.");

            ddl = new DropDownList();
            ddl.ID = "RprfEgitimID";
            ddl.Width = 250;
            ddl.CssClass = "form-control";
            ddl.DataMember = "kategori";
            ddl.DataValueField = "id";
            ddl.DataTextField = "adi";
            ddl.DataSource = KategoriMethods.GetMenu("egitim", true);
            ddl.DataBind();
            CustomizeControl1.AddControl("Eğitim", ddl, "* Seçmesi zorunlu alan.");

            txt = new TextBox();
            txt.ID = "RprfHakkimda";
            txt.CssClass = "form-control";
            txt.Height = 150;
            txt.TextMode = TextBoxMode.MultiLine;
            txt.MaxLength = 500;
            CustomizeControl1.AddControl("Hakkimda", txt, "Bu alana <b>500</b> karaktere kadar bilgi girişi yapabilirsiniz.");
        }

        void standartHesap_SubmitClick(SortedDictionary<string, Control> controls)
        {
            try
            {
                if (!string.IsNullOrEmpty(((TextBox)controls["rgsAdi"]).Text)
                    & !string.IsNullOrEmpty(((TextBox)controls["rgsMail"]).Text)
                    & !string.IsNullOrEmpty(((TextBox)controls["rgsSifre"]).Text))
                    using (Hesap hsp = new Hesap())
                    {
                        hsp.IP = Context.Request.ServerVariables["REMOTE_ADDR"].ToString();
                        hsp.Adi = ((TextBox)controls["rgsAdi"]).Text;
                        hsp.Soyadi = ((TextBox)controls["rgsSoyadi"]).Text;
                        hsp.Mail = ((TextBox)controls["rgsMail"]).Text;
                        hsp.Sifre = System.Web.Security.FormsAuthentication.HashPasswordForStoringInConfigFile((controls["rgsSifre"] as TextBox).Text, "md5");
                        hsp.Roller = "U";
                        hsp.OnayKodu = Core.GenerateSecurityCode();
                        hsp.DogumTarihi = ((DateTimeControl)controls["rgsDogumTarihi"]).Date;
                        hsp.Cinsiyet = Core.GetSexType(BAYMYO.UI.Converts.NullToByte(((DropDownList)controls["rgsCinsiyet"]).SelectedValue));
                        hsp.Tipi = AccountType.Standart;
                        hsp.Abonelik = ((CheckBox)controls["rgsAbonelik"]).Checked;
                        hsp.Yorum = true;
                        hsp.Aktivasyon = false;
                        hsp.Aktif = false;
                        hsp.KayitTarihi = DateTime.Now;
                        string result = BAYMYO.UI.Converts.NullToString(HesapMethods.Insert(hsp));
                        switch (result)
                        {
                            case "EMAIL":
                                CustomizeControl1.MessageText = MessageBox.Show(DialogResult.Stop, "Kayıt olmak istediğiniz <b>'E-Mail'</b> adresi başkası tarafından kullanılıyor! Lütfen başka bir 'E-Mail' adresi ile tekrar deneyiniz yada eğer bu e-mail adresinin sizin olduğundan eminseniz şifremi unuttum kısımından tekrar şifre talebinde bulununuz!");
                                break;
                            default:
                                if (!result.Equals(BAYMYO.UI.Converts.NullToGuidString(null)))
                                {
                                    hsp.ID = result;
                                    Success(hsp);
                                }
                                else
                                    CustomizeControl1.MessageText = MessageBox.Show(DialogResult.Error, "Üyelik işleminiz gerçekleştirilemiyor. Lütfen bilgilerinizi kontrol edip tekrar deneyiniz!");
                                break;
                        }
                    }
            }
            catch (Exception)
            {
                CustomizeControl1.MessageText = MessageBox.Show(DialogResult.Error, "Üyelik işleminiz gerçekleştirilemiyor. Lütfen bilgilerinizi kontrol edip tekrar deneyiniz!");
            }
        }

        void editorHesap_SubmitClick(SortedDictionary<string, Control> controls)
        {
            try
            {
                if (!string.IsNullOrEmpty(((TextBox)controls["rgsAdi"]).Text)
                    & !string.IsNullOrEmpty(((TextBox)controls["rgsMail"]).Text)
                    & !string.IsNullOrEmpty(((TextBox)controls["rgsSifre"]).Text)
                    & !string.IsNullOrEmpty(((TextBox)controls["RprfUrl"]).Text)
                    & !string.IsNullOrEmpty(((TextBox)controls["RprfAdi"]).Text)
                    & !string.IsNullOrEmpty(((TextBox)controls["RprfMail"]).Text)
                    & ((DropDownList)controls["RprfMeslekID"]).SelectedIndex > 0
                    & ((DropDownList)controls["RprfEgitimID"]).SelectedIndex > 0)
                {
                    if (Settings.InSlangyUrl.Contains(";" + ((TextBox)controls["RprfUrl"]).Text + ";"))
                    {
                        CustomizeControl1.MessageText = MessageBox.Show(DialogResult.Error, string.Format("<b>{0}</b> editör için belirtiğiniz <b>'{1}'</b> Url argo kelime içeriyor, yöneticilerimiz küfürlü içeriklere onay vermemektedir. Lütfen argo içermeyen bir <b>'URL'</b> girerek ve tekrar deneyiniz.", ((TextBox)controls["rgsAdi"]).Text, ((TextBox)controls["RprfUrl"]).Text));
                        return;
                    }
                    else if (Settings.InValidUrl.Contains(";" + ((TextBox)controls["RprfUrl"]).Text + ";"))
                    {
                        CustomizeControl1.MessageText = MessageBox.Show(DialogResult.Stop, string.Format("<b>{0}</b> editör için belirtiğiniz <b>'{1}'</b> Url sistemimiz tarafından kullanılıyor. Lütfen farklı bir <b>'URL'</b> girerek ve tekrar deneyiniz.", ((TextBox)controls["rgsAdi"]).Text, ((TextBox)controls["RprfUrl"]).Text));
                        return;
                    }
                    else if (((TextBox)controls["RprfUrl"]).Text.Length < 6)
                    {
                        CustomizeControl1.MessageText = MessageBox.Show(DialogResult.Warning, string.Format("<b>{0}</b> editör için belirtiğiniz <b>'{1}'</b> Url en az 6 karakter olmalıdır. Lütfen farklı bir <b>'URL'</b> girerek ve tekrar deneyiniz.", ((TextBox)controls["rgsAdi"]).Text, ((TextBox)controls["RprfUrl"]).Text));
                        return;
                    }
                    using (Hesap hsp = HesapMethods.GetHesap(BAYMYO.UI.Converts.NullToGuidString(ViewState["TempID"])))
                    {
                        //Hesap Bilgileri
                        hsp.IP = Context.Request.ServerVariables["REMOTE_ADDR"].ToString();
                        hsp.Adi = ((TextBox)controls["rgsAdi"]).Text;
                        hsp.Soyadi = ((TextBox)controls["rgsSoyadi"]).Text;
                        hsp.Mail = ((TextBox)controls["rgsMail"]).Text;
                        hsp.Sifre = System.Web.Security.FormsAuthentication.HashPasswordForStoringInConfigFile((controls["rgsSifre"] as TextBox).Text, "md5");
                        hsp.Roller = "E,U";
                        hsp.Tipi = AccountType.Editor;
                        hsp.OnayKodu = Core.GenerateSecurityCode();
                        hsp.DogumTarihi = ((DateTimeControl)controls["rgsDogumTarihi"]).Date;
                        hsp.Cinsiyet = Core.GetSexType(BAYMYO.UI.Converts.NullToByte(((DropDownList)controls["rgsCinsiyet"]).SelectedValue));
                        hsp.Abonelik = ((CheckBox)controls["rgsAbonelik"]).Checked;
                        hsp.Yorum = true;
                        hsp.Aktivasyon = false;
                        hsp.Aktif = false;
                        hsp.KayitTarihi = DateTime.Now;
                        //Profil Bilgileri
                        hsp.ProfilObject.Url = ((TextBox)controls["RprfUrl"]).Text;
                        hsp.ProfilObject.Adi = BAYMYO.UI.Commons.SubStringText(((TextBox)controls["RprfAdi"]).Text, 50);
                        hsp.ProfilObject.Mail = ((TextBox)controls["RprfMail"]).Text;
                        hsp.ProfilObject.Web = BAYMYO.UI.Commons.SubStringText(((TextBox)controls["RprfWeb"]).Text, 60).ToLower().Replace("http://", "");
                        hsp.ProfilObject.Telefon = ((TextBox)controls["RprfTelefon"]).Text;
                        hsp.ProfilObject.GSM = ((TextBox)controls["RprfGSM"]).Text;
                        hsp.ProfilObject.Sehir = BAYMYO.UI.Converts.NullToString(((DropDownList)controls["RprfSehir"]).SelectedValue);
                        hsp.ProfilObject.Meslek = ((DropDownList)controls["RprfMeslekID"]).SelectedValue;
                        hsp.ProfilObject.Egitim = ((DropDownList)controls["RprfEgitimID"]).SelectedValue;
                        hsp.ProfilObject.Hakkimda = BAYMYO.UI.Commons.SubStringText(((TextBox)controls["RprfHakkimda"]).Text, 500);
                        if (!string.IsNullOrEmpty(hsp.ID))
                        {
                            switch (HesapMethods.Update(hsp))
                            {
                                case "EMAIL":
                                    CustomizeControl1.MessageText = MessageBox.Show(DialogResult.Stop, "Kayıt olmak istediğiniz <b>'E-Mail'</b> adresi başkası tarafından kullanılıyor! Lütfen başka bir 'E-Mail' adresi ile tekrar deneyiniz yada eğer bu e-mail adresinin sizin olduğundan eminseniz şifremi unuttum kısımından tekrar şifre talebinde bulununuz!");
                                    break;
                                default:
                                    if ((controls["RprfResimUrl"] as FileUpload).HasFile)
                                        if (BAYMYO.UI.FileIO.Remove(Server.MapPath(Settings.ImagesPath + "profil/" + hsp.ProfilObject.ResimUrl)))
                                            hsp.ProfilObject.ResimUrl = BAYMYO.UI.FileIO.UploadImage(controls["RprfResimUrl"] as FileUpload, hsp.Adi + " " + hsp.Soyadi, Server.MapPath(Settings.ImagesPath + "profil/"), 260, true); ;
                                    if (string.IsNullOrEmpty(hsp.ProfilObject.ID))
                                    {
                                        hsp.ProfilObject.ID = hsp.ID;
                                        switch (ProfilMethods.Insert(hsp.ProfilObject))
                                        {
                                            case "":
                                            case "0":
                                                CustomizeControl1.MessageText = MessageBox.Show(DialogResult.Stop, "Profil bilgilerinizi kontrol ediniz ve tekrar deneyiniz!");
                                                break;
                                            case "URL":
                                                CustomizeControl1.MessageText = MessageBox.Show(DialogResult.Warning, "Belirttiğiniz <b>'URL'</b> başka bir kullanıcı tarafından kullanılmaktadır. Lütfen başka bir <b>'URL'</b> yazarak tekrar deneyiniz.");
                                                break;
                                            case "ADI":
                                                CustomizeControl1.MessageText = MessageBox.Show(DialogResult.Warning, "Belirttiğiniz <b>'BAŞLIK'</b> başka bir kullanıcı tarafından kullanılmaktadır. Lütfen 'Profil Başlığınızı' kontrol ediniz ve tekrar deneyiniz.");
                                                break;
                                            default:
                                                Success(hsp);
                                                break;
                                        }
                                    }
                                    else
                                    {
                                        switch (ProfilMethods.Update(hsp.ProfilObject))
                                        {
                                            case "":
                                            case "0":
                                                CustomizeControl1.MessageText = MessageBox.Show(DialogResult.Stop, "Profil bilgilerinizi kontrol ediniz ve tekrar deneyiniz!");
                                                break;
                                            case "URL":
                                                CustomizeControl1.MessageText = MessageBox.Show(DialogResult.Warning, "Belirttiğiniz <b>'URL'</b> başka bir kullanıcı tarafından kullanılmaktadır. Lütfen başka bir <b>'URL'</b> yazarak tekrar deneyiniz.");
                                                break;
                                            case "ADI":
                                                CustomizeControl1.MessageText = MessageBox.Show(DialogResult.Warning, "Belirttiğiniz <b>'BAŞLIK'</b> başka bir kullanıcı tarafından kullanılmaktadır. Lütfen 'Profil Başlığınızı' kontrol ediniz ve tekrar deneyiniz.");
                                                break;
                                            default:
                                                Success(hsp);
                                                break;
                                        }
                                    }
                                    break;
                            }
                        }
                        else
                        {
                            hsp.KayitTarihi = DateTime.Now;
                            string result = HesapMethods.Insert(hsp);
                            switch (result)
                            {
                                case "EMAIL":
                                    CustomizeControl1.MessageText = MessageBox.Show(DialogResult.Stop, "Kayıt olmak istediğiniz <b>'E-Mail'</b> adresi başkası tarafından kullanılıyor! Lütfen başka bir 'E-Mail' adresi ile tekrar deneyiniz yada eğer bu e-mail adresinin sizin olduğundan eminseniz şifremi unuttum kısımından tekrar şifre talebinde bulununuz!");
                                    break;
                                default:
                                    string hid = BAYMYO.UI.Converts.NullToGuidString(result);
                                    if (!hid.Equals(BAYMYO.UI.Converts.NullToGuidString(null)))
                                    {
                                        ViewState["TempID"] = hid;
                                        hsp.ID = hid;
                                        hsp.ProfilObject.ID = hid;
                                        hsp.ProfilObject.ResimUrl = BAYMYO.UI.FileIO.UploadImage(controls["RprfResimUrl"] as FileUpload, hsp.Adi + " " + hsp.Soyadi, Server.MapPath(Settings.ImagesPath + "profil/"), 260, true);
                                        switch (ProfilMethods.Insert(hsp.ProfilObject))
                                        {
                                            case "":
                                            case "0":
                                                CustomizeControl1.MessageText = MessageBox.Show(DialogResult.Stop, "Profil bilgilerinizi kontrol ediniz ve tekrar deneyiniz!");
                                                break;
                                            case "URL":
                                                CustomizeControl1.MessageText = MessageBox.Show(DialogResult.Warning, "Belirttiğiniz <b>'URL'</b> başka bir kullanıcı tarafından kullanılmaktadır. Lütfen başka bir <b>'URL'</b> yazarak tekrar deneyiniz.");
                                                break;
                                            case "ADI":
                                                CustomizeControl1.MessageText = MessageBox.Show(DialogResult.Warning, "Belirttiğiniz <b>'BAŞLIK'</b> başka bir kullanıcı tarafından kullanılmaktadır. Lütfen 'Profil Başlığınızı' kontrol ediniz ve tekrar deneyiniz.");
                                                break;
                                            default:
                                                Success(hsp);
                                                break;
                                        }
                                    }
                                    else
                                        CustomizeControl1.MessageText = MessageBox.Show(DialogResult.Error, "Üyelik işleminiz gerçekleştirilemiyor. Lütfen bilgilerinizi kontrol edip tekrar deneyiniz!");
                                    break;
                            }
                        }
                    }
                }
            }
            catch (Exception)
            {
                CustomizeControl1.MessageText = MessageBox.Show(DialogResult.Error, "Üyelik işleminiz gerçekleştirilemiyor. Lütfen bilgilerinizi kontrol edip tekrar deneyiniz!");
            }
        }

        void Success(Hesap hsp)
        {
            try
            {
                CustomizeControl1.ControlList.Clear();
                CustomizeControl1.PanelVisible = false;
                string m_MailMesaj = BAYMYO.UI.FileIO.ReadText(Server.MapPath(Settings.ViewPath) + "Activation.view");
                m_MailMesaj = m_MailMesaj.Replace("%SiteUrl%", Settings.SiteUrl);
                m_MailMesaj = m_MailMesaj.Replace("%SiteTitle%", Settings.Site.Title);
                m_MailMesaj = m_MailMesaj.Replace("%VirtualPath%", Settings.VirtualPath);
                m_MailMesaj = m_MailMesaj.Replace("%ReturnUrl%", Request.QueryString["ReturnUrl"]);
                m_MailMesaj = m_MailMesaj.Replace("%ID%", hsp.ID.ToString());
                m_MailMesaj = m_MailMesaj.Replace("%Adi%", hsp.Adi).Replace("%Soyadi%", hsp.Soyadi);
                m_MailMesaj = m_MailMesaj.Replace("%OnayKodu%", hsp.OnayKodu);
                Core.SendMail(hsp.Mail, hsp.Adi + " " + hsp.Soyadi, "Aktivasyon Maili", m_MailMesaj, true);
                m_MailMesaj = null;
                Response.Redirect(Settings.VirtualPath + "?go=register&r=aktivasyon", false);
            }
            catch (Exception)
            {
                CustomizeControl1.MessageText = MessageBox.Show(DialogResult.Stop, string.Format("Hesabınız oluşturuldu fakat sunucmuzdaki yoğunluk sebebi ile <b>Aktivasyon Maili</b> <u>gönderilemedi</u>! Lütfen tekrar <b>Aktivasyon Maili</b> talebinde bulunmak için <a href=\"{0}\" target=\"_self\">buraya tıklayın</a>.", Settings.VirtualPath + "?go=register&r=aktivasyon"));
                CustomizeControl1.ControlList.Clear();
                CustomizeControl1.PanelVisible = false;
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            CustomizeControl1.IsValidated = true;
        }
    }
}