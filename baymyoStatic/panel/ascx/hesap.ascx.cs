using System;
using System.Collections.Generic;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace baymyoStatic.panel.ascx
{
    public partial class hesap : System.Web.UI.UserControl
    {
        protected override void OnInit(EventArgs e)
        {
            try
            {
                CustomizeControl1.FormTitle = string.Format(Settings.FormTitleFormat, "Hesap", "Ekleme/Düzeltme Formu");
                using (Hesap hsp = HesapMethods.GetHesap(BAYMYO.UI.Converts.NullToGuidString(Request.QueryString["uid"])))
                {
                    bool notNull = !string.IsNullOrEmpty(Request.QueryString["uid"]);
                    CustomizeControl1.RemoveVisible = notNull;
                    if (notNull) CustomizeControl1.StatusText = string.Format("<div style=\"margin-top: 5px !important;padding-top: 5px !important;border-top: dashed 1px #c5c5c5;\"><a class=\"toolTip\" title=\"Hesap listesine geri dönmek için tıklayın!\" href=\"{1}\" target=\"_blank\"><b>Hesap Listesi!</b></a>&nbsp;-&nbsp;<a class=\"toolTip\" title=\"Önizleme için tıklayın!\" href=\"{0}\" target=\"_blank\"><b>Önizleme Yap!</b></a></div>", Settings.VirtualPath + hsp.ProfilObject.Url, Settings.PanelPath + "?go=hesapliste");
                    //if (hsp.ProfilObject == null)
                    //    hsp.ProfilObject = new Profil();
                    TextBox txt = new TextBox();
                    txt.ID = "Adi";
                    txt.CssClass = "form-control";
                    txt.Text = hsp.Adi;
                    txt.MaxLength = 18;
                    CustomizeControl1.AddControl("Adı", txt);

                    txt = new TextBox();
                    txt.ID = "Soyadi";
                    txt.CssClass = "form-control";
                    txt.Text = hsp.Soyadi;
                    txt.MaxLength = 15;
                    CustomizeControl1.AddControl("Soyadı", txt);

                    txt = new TextBox();
                    txt.ID = "Mail";
                    txt.CssClass = "form-control";
                    txt.Text = hsp.Mail;
                    txt.TextMode = TextBoxMode.Email;
                    txt.MaxLength = 60;
                    CustomizeControl1.AddControl("Mail", txt, "Sisteme giriş yapmak için kullanılacaktır.");

                    txt = new TextBox();
                    txt.ID = "Sifre";
                    txt.CssClass = "form-control";
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
                    ddl.ID = "Cinsiyet";
                    ddl.Width = 195;
                    ddl.CssClass = "form-control";
                    ddl.DataValueField = "Key";
                    ddl.DataTextField = "Value";
                    ddl.DataSource = Core.GetSexTypes();
                    ddl.DataBind();
                    ddl.SelectedValue = BAYMYO.UI.Converts.NullToByte(hsp.Cinsiyet).ToString();
                    CustomizeControl1.AddControl("Cinsiyet", ddl);

                    AccountType tipi;
                    if (!string.IsNullOrEmpty(Request.QueryString["type"]))
                        tipi = Core.GetAccountType(BAYMYO.UI.Converts.NullToByte(Request.QueryString["type"]));
                    else
                        tipi = hsp.Tipi;

                    ddl = new DropDownList();
                    ddl.ID = "Tipi";
                    ddl.Width = 195;
                    ddl.CssClass = "form-control";
                    ddl.DataValueField = "Key";
                    ddl.DataTextField = "Value";
                    ddl.DataSource = Core.GetAccountTypes();
                    ddl.DataBind();
                    ddl.SelectedValue = BAYMYO.UI.Converts.NullToByte(tipi).ToString();
                    CustomizeControl1.AddControl("Hesap Türü", ddl, "(Not: Sadece Admin yönetim panelini görebilir!)");

                    CheckBoxList chkList = new CheckBoxList();
                    chkList.ID = "chkList";
                    chkList.RepeatDirection = RepeatDirection.Horizontal;
                    chkList.Items.Add("Abonelik");
                    chkList.Items[0].Selected = notNull ? hsp.Abonelik : true;
                    chkList.Items.Add("Aktivasyon");
                    chkList.Items[1].Selected = notNull ? hsp.Aktivasyon : true;
                    chkList.Items.Add("Yorum Yapabilir");
                    chkList.Items[2].Selected = notNull ? hsp.Yorum : true;
                    chkList.Items.Add("Hesap Durumu");
                    chkList.Items[3].Selected = notNull ? hsp.Aktif : true;
                    CustomizeControl1.AddControl("Seçimler", chkList);
                    switch (tipi)
                    {
                        case AccountType.Admin:
                        case AccountType.Private:
                        case AccountType.Doctor:
                        case AccountType.Editor:
                            CustomizeControl1.AddTitle("Profil Bilgileri");
                            Image img = new Image();
                            img.ID = "prfImageUrl";
                            img.Width = 210;
                            img.ImageUrl = Settings.ImagesPath + ((!string.IsNullOrEmpty(hsp.ProfilObject.ResimUrl)) ? "profil/" + hsp.ProfilObject.ResimUrl : "profil/noavatar.png");
                            CustomizeControl1.AddControl("Fotoğraf", img);

                            FileUpload flu = new FileUpload();
                            flu.ID = "prfResimUrl";
                            flu.CssClass = "form-control";
                            CustomizeControl1.AddControl("Yeni Fotoğraf", flu, "Genişlik(W):160px - Yükseklik(H):170px");

                            txt = new TextBox();
                            txt.ID = "prfUrl";
                            txt.Text = hsp.ProfilObject.Url;
                            txt.CssClass = "form-control";
                            txt.MaxLength = 50;
                            CustomizeControl1.AddControl("Url", txt, "Profil bağlantı adresi olacaktır ve sadece küçük harfler girebilirsiniz. Ör. " + Settings.SiteUrl + "<b class=\"toolTip titleFormat1\" title=\"Adres çubuğunda sitemizin adının yanına '/' ters slaş yaparak burada belirteceğiniz isim ile profilinizin görüntülenmesini sağlar.\">adisoyadi</b>");

                            txt = new TextBox();
                            txt.ID = "prfAdi";
                            txt.Text = hsp.ProfilObject.Adi;
                            txt.CssClass = "form-control";
                            txt.MaxLength = 50;
                            CustomizeControl1.AddControl("Başlık", txt, "Profilde gösterilecek olan <b>başlıktır</b>. 'Ör. Sezgin'in Sayfasına Hoş Geldiniz!'");

                            txt = new TextBox();
                            txt.ID = "prfMail";
                            txt.Text = hsp.ProfilObject.Mail;
                            txt.CssClass = "form-control";
                            txt.TextMode = TextBoxMode.Email;
                            txt.MaxLength = 60;
                            CustomizeControl1.AddControl("Profil Maili", txt, "Profilde gösterilecek olan <b>'Mail'</b> adresidir. 'Not: Profiliniz üzerinden bu adrese mail gönderebilecekler!'");

                            txt = new TextBox();
                            txt.ID = "prfWeb";
                            txt.Text = hsp.ProfilObject.Web;
                            txt.CssClass = "form-control";
                            txt.MaxLength = 60;
                            CustomizeControl1.AddControl("Web Adresi", txt, "Profilde gösterilecek olan <b>'Web Site'</b> adresidir.");

                            txt = new TextBox();
                            txt.ID = "prfTelefon";
                            txt.Text = hsp.ProfilObject.Telefon;
                            txt.CssClass = "form-control";
                            txt.TextMode = TextBoxMode.Phone;
                            txt.MaxLength = 16;
                            CustomizeControl1.AddControl("Telefon", txt, "Profilde gösterilecek olan <b>'Telefon'</b> numarasıdır. Ör. <b>0326 6XX 2X 0X</b>");

                            txt = new TextBox();
                            txt.ID = "prfGSM";
                            txt.Text = hsp.ProfilObject.GSM;
                            txt.CssClass = "form-control";
                            txt.TextMode = TextBoxMode.Phone;
                            txt.MaxLength = 16;
                            CustomizeControl1.AddControl("GSM", txt, "Profilde gösterilecek olan <b>'GSM'</b> numarasıdır. Ör. <b>0544 2XX 4X 5X</b>");

                            ddl = new DropDownList();
                            ddl.ID = "Sehir";
                            ddl.Width = 250;
                            ddl.CssClass = "form-control";
                            ddl.DataMember = "Sehir";
                            ddl.DataValueField = "Adi";
                            ddl.DataTextField = "Adi";
                            SehirCollection sehirler = SehirMethods.GetSelect();
                            sehirler.Insert(0, new Sehir(0, ""));
                            ddl.DataSource = sehirler;
                            ddl.DataBind();
                            ddl.SelectedValue = BAYMYO.UI.Converts.NullToString(hsp.ProfilObject.Sehir);
                            CustomizeControl1.AddControl("Şehir (İL)", ddl, "<b>Şehir</b> adı harita üzerinde bulunmasını sağlayacaktır.");

                            ddl = new DropDownList();
                            ddl.ID = "prfMeslekID";
                            ddl.Width = 250;
                            ddl.CssClass = "form-control";
                            ddl.DataMember = "kategori";
                            ddl.DataValueField = "id";
                            ddl.DataTextField = "adi";
                            ddl.DataSource = KategoriMethods.GetMenu("meslek", true);
                            ddl.DataBind();
                            ddl.SelectedValue = hsp.ProfilObject.Meslek;
                            CustomizeControl1.AddControl("Meslek", ddl, "<a href=\"" + Settings.PanelPath + "?go=kategori&mdl=meslek\">[+] Yeni Meslek Tanımla</a>");

                            ddl = new DropDownList();
                            ddl.ID = "prfEgitimID";
                            ddl.Width = 250;
                            ddl.CssClass = "form-control";
                            ddl.DataMember = "kategori";
                            ddl.DataValueField = "id";
                            ddl.DataTextField = "adi";
                            ddl.DataSource = KategoriMethods.GetMenu("egitim", true);
                            ddl.DataBind();
                            ddl.SelectedValue = hsp.ProfilObject.Egitim;
                            CustomizeControl1.AddControl("Eğitim", ddl, "<a href=\"" + Settings.PanelPath + "?go=kategori&mdl=egitim\">[+] Yeni Eğitim Durumu</a>");

                            txt = new TextBox();
                            txt.ID = "prfHakkimda";
                            txt.Text = hsp.ProfilObject.Hakkimda;
                            txt.CssClass = "form-control noHtml";
                            txt.Height = 150;
                            txt.TextMode = TextBoxMode.MultiLine;
                            txt.MaxLength = 500;
                            CustomizeControl1.AddControl("Hakkimda", txt, "Bu alana <b>500</b> karaktere kadar bilgi girişi yapabilirsiniz.");

                            CustomizeControl1.AddTitle("Hesap Yetkileri");
                            chkList = new CheckBoxList();
                            chkList.ID = "chkSecure";
                            chkList.Font.Bold = true;
                            chkList.RepeatColumns = 5;
                            chkList.RepeatDirection = RepeatDirection.Horizontal;
                            chkList.Items.Add(new ListItem("Panel", "P"));
                            chkList.Items.Add(new ListItem("Ayarlar", "A"));
                            //chkList.Items.Add(new ListItem("Ajans", "J"));
                            chkList.Items.Add(new ListItem("Manşet", "T"));
                            chkList.Items.Add(new ListItem("Haber", "H"));
                            chkList.Items.Add(new ListItem("Makale", "M"));
                            //chkList.Items.Add(new ListItem("Reklam", "R"));
                            //chkList.Items.Add(new ListItem("Resmi İlan", "I"));
                            chkList.Items.Add(new ListItem("Mesaj", "Q"));
                            //chkList.Items.Add(new ListItem("Firma", "F"));
                            //chkList.Items.Add(new ListItem("Seri İlan", "S"));
                            chkList.Items.Add(new ListItem("Video", "V"));
                            chkList.Items.Add(new ListItem("Galeri", "G"));
                            chkList.Items.Add(new ListItem("Yorum", "Y"));
                            chkList.Items.Add(new ListItem("<a class=\"toolTip\" href=\"#\">Diğerleri ..</a>", "O"));
                            if (!string.IsNullOrEmpty(hsp.Roller))
                                foreach (string rol in hsp.Roller.Split(','))
                                    if (chkList.Items.FindByValue(rol) != null)
                                        chkList.Items.FindByValue(rol).Selected = true;
                            CustomizeControl1.AddControl("Yetkiler", chkList);
                            CustomizeControl1.SubmitClick += new CustomizeControl.ButtonEvent(editorHesap_SubmitClick);
                            break;
                        default:
                            CustomizeControl1.SubmitClick += new CustomizeControl.ButtonEvent(standartHesap_SubmitClick);
                            break;
                    }
                    CustomizeControl1.RemoveClick += new CustomizeControl.ButtonEvent(hesap_RemoveClick);
                }
            }
            catch (Exception ex)
            {
                CustomizeControl1.MessageText = MessageBox.Show(DialogResult.Error, ex.Message);
            }
            base.OnInit(e);
        }

        void standartHesap_SubmitClick(SortedDictionary<string, Control> controls)
        {
            try
            {
                if (!string.IsNullOrEmpty(((TextBox)controls["Adi"]).Text)
                    & !string.IsNullOrEmpty(((TextBox)controls["Mail"]).Text))
                    using (Hesap hsp = HesapMethods.GetHesap(BAYMYO.UI.Converts.NullToGuidString(Request.QueryString["uid"])))
                    {
                        hsp.IP = Context.Request.ServerVariables["REMOTE_ADDR"].ToString();
                        hsp.Adi = ((TextBox)controls["Adi"]).Text;
                        hsp.Soyadi = ((TextBox)controls["Soyadi"]).Text;
                        hsp.Mail = ((TextBox)controls["Mail"]).Text;
                        if (!string.IsNullOrEmpty((controls["Sifre"] as TextBox).Text.Trim()))
                        {
                            string sifre = System.Web.Security.FormsAuthentication.HashPasswordForStoringInConfigFile((controls["Sifre"] as TextBox).Text, "md5");
                            if (!(controls["Sifre"] as TextBox).ToolTip.Equals(sifre))
                                hsp.Sifre = sifre;
                        }
                        hsp.OnayKodu = Core.GenerateSecurityCode();
                        hsp.Roller = "U";
                        hsp.DogumTarihi = ((DateTimeControl)controls["DogumTarihi"]).Date;
                        hsp.Cinsiyet = Core.GetSexType(BAYMYO.UI.Converts.NullToByte(((DropDownList)controls["Cinsiyet"]).SelectedValue));
                        hsp.Tipi = Core.GetAccountType(BAYMYO.UI.Converts.NullToByte(((DropDownList)controls["Tipi"]).SelectedValue));
                        hsp.Abonelik = ((CheckBoxList)controls["chkList"]).Items[0].Selected;
                        hsp.Aktivasyon = ((CheckBoxList)controls["chkList"]).Items[1].Selected;
                        hsp.Yorum = ((CheckBoxList)controls["chkList"]).Items[2].Selected;
                        if (Core.IsUserAdmin)
                            hsp.Aktif = ((CheckBoxList)controls["chkList"]).Items[3].Selected;
                        else
                            hsp.Aktif = false;
                        if (!string.IsNullOrEmpty(hsp.ID))
                        {
                            switch (HesapMethods.Update(hsp))
                            {
                                case "EMAIL":
                                    CustomizeControl1.MessageText = MessageBox.Show(DialogResult.Stop, "Kayıt olmak istediğiniz <b>'E-Mail'</b> adresi başkası tarafından kullanılıyor! Lütfen başka bir 'E-Mail' adresi ile tekrar deneyiniz yada eğer bu e-mail adresinin sizin olduğundan eminseniz şifremi unuttum kısımından tekrar şifre talebinde bulununuz!");
                                    break;
                                default:
                                    CustomizeControl1.MessageText = MessageBox.Show(ProccesType.Update, DialogResult.Succes);
                                    if (!string.IsNullOrEmpty(hsp.ProfilObject.ID))
                                        ProfilMethods.Delete(hsp.ProfilObject);
                                    break;
                            }
                        }
                        else
                        {
                            hsp.KayitTarihi = DateTime.Now;
                            string result = BAYMYO.UI.Converts.NullToString(HesapMethods.Insert(hsp));
                            switch (result)
                            {
                                case "EMAIL":
                                    CustomizeControl1.MessageText = MessageBox.Show(DialogResult.Stop, "Kayıt olmak istediğiniz 'E-Mail' adresi başkası tarafından kullanılıyor! Lütfen başka bir 'E-Mail' adresi ile tekrar deneyiniz yada eğer bu e-mail adresinin sizin olduğundan eminseniz şifremi unuttum kısımından tekrar şifre talebinde bulununuz!");
                                    break;
                                default:
                                    if (!string.IsNullOrEmpty(result))
                                    {
                                        CustomizeControl1.MessageText = MessageBox.Show(ProccesType.Insert, DialogResult.Succes);
                                        Core.ClearControls(controls);
                                    }
                                    else
                                        CustomizeControl1.MessageText = MessageBox.Show(DialogResult.Error, "Üyelik işleminiz gerçekleştirilemiyor. Lütfen bilgilerinizi kontrol edip tekrar deneyiniz!");
                                    break;
                            }
                        }
                    }
                else
                    CustomizeControl1.MessageText = MessageBox.IsNotNull();
            }
            catch (Exception ex)
            {
                CustomizeControl1.MessageText = MessageBox.Show(DialogResult.Error, ex.Message);
                jSonData.CreateData("doktorlar");
            }
        }
        void editorHesap_SubmitClick(SortedDictionary<string, Control> controls)
        {
            try
            {
                if (!string.IsNullOrEmpty(Request.QueryString["uid"]))
                    ViewState["TempID"] = Request.QueryString["uid"];
                if (!string.IsNullOrEmpty(((TextBox)controls["Adi"]).Text)
                    & !string.IsNullOrEmpty(((TextBox)controls["Mail"]).Text)
                    & !string.IsNullOrEmpty(((TextBox)controls["prfUrl"]).Text)
                    & !string.IsNullOrEmpty(((TextBox)controls["prfAdi"]).Text)
                    & ((DropDownList)controls["prfMeslekID"]).SelectedIndex > 0
                    & ((DropDownList)controls["prfEgitimID"]).SelectedIndex > 0)
                    using (Hesap hsp = HesapMethods.GetHesap(BAYMYO.UI.Converts.NullToGuidString(ViewState["TempID"])))
                    {
                        hsp.IP = Context.Request.ServerVariables["REMOTE_ADDR"].ToString();
                        hsp.Adi = BAYMYO.UI.Commons.SubStringText(((TextBox)controls["Adi"]).Text, 18).Trim();
                        hsp.Soyadi = BAYMYO.UI.Commons.SubStringText(((TextBox)controls["Soyadi"]).Text, 15).Trim();
                        hsp.Mail = ((TextBox)controls["Mail"]).Text;
                        if (!string.IsNullOrEmpty((controls["Sifre"] as TextBox).Text.Trim()))
                        {
                            string sifre = System.Web.Security.FormsAuthentication.HashPasswordForStoringInConfigFile((controls["Sifre"] as TextBox).Text, "md5");
                            if (!(controls["Sifre"] as TextBox).ToolTip.Equals(sifre))
                                hsp.Sifre = sifre;
                        }
                        hsp.OnayKodu = Core.GenerateSecurityCode();
                        hsp.DogumTarihi = ((DateTimeControl)controls["DogumTarihi"]).Date;
                        hsp.Cinsiyet = Core.GetSexType(BAYMYO.UI.Converts.NullToByte(((DropDownList)controls["Cinsiyet"]).SelectedValue));
                        hsp.Tipi = Core.GetAccountType(BAYMYO.UI.Converts.NullToByte(((DropDownList)controls["Tipi"]).SelectedValue));
                        using (CheckBoxList chkSecure = ((CheckBoxList)controls["chkSecure"]))
                        {
                            hsp.Roller = string.Empty;
                            for (int i = 0; i < chkSecure.Items.Count; i++)
                                if (chkSecure.Items[i].Selected)
                                    hsp.Roller += chkSecure.Items[i].Value + ",";
                        }
                        hsp.Abonelik = ((CheckBoxList)controls["chkList"]).Items[0].Selected;
                        hsp.Aktivasyon = ((CheckBoxList)controls["chkList"]).Items[1].Selected;
                        hsp.Yorum = ((CheckBoxList)controls["chkList"]).Items[2].Selected;
                        hsp.Aktif = ((CheckBoxList)controls["chkList"]).Items[3].Selected;
                        bool isEditor = true;
                        switch (hsp.Tipi)
                        {
                            case AccountType.Admin:
                            case AccountType.Private:
                            case AccountType.Doctor:
                            case AccountType.Editor:
                                hsp.Roller += "E,U";
                                hsp.ProfilObject.Url = ((TextBox)controls["prfUrl"]).Text;
                                hsp.ProfilObject.Adi = BAYMYO.UI.Commons.SubStringText(((TextBox)controls["prfAdi"]).Text, 50).Trim();
                                hsp.ProfilObject.Mail = ((TextBox)controls["prfMail"]).Text;
                                hsp.ProfilObject.Web = BAYMYO.UI.Commons.SubStringText(((TextBox)controls["prfWeb"]).Text, 60).ToLower().Replace("http://", "");
                                hsp.ProfilObject.Telefon = ((TextBox)controls["prfTelefon"]).Text;
                                hsp.ProfilObject.GSM = ((TextBox)controls["prfGSM"]).Text;
                                hsp.ProfilObject.Sehir = BAYMYO.UI.Converts.NullToString(((DropDownList)controls["Sehir"]).SelectedValue);
                                hsp.ProfilObject.Meslek = ((DropDownList)controls["prfMeslekID"]).SelectedValue;
                                hsp.ProfilObject.Egitim = ((DropDownList)controls["prfEgitimID"]).SelectedValue;
                                hsp.ProfilObject.Hakkimda = BAYMYO.UI.Commons.SubStringText(((TextBox)controls["prfHakkimda"]).Text, 500);
                                break;
                            default:
                                hsp.Roller = "U";
                                isEditor = false;
                                break;
                        }
                        if (!string.IsNullOrEmpty(hsp.ID))
                        {
                            switch (HesapMethods.Update(hsp))
                            {
                                case "":
                                case "0":
                                    if (!string.IsNullOrEmpty(hsp.ProfilObject.ID) & !isEditor)
                                    {
                                        BAYMYO.UI.FileIO.Remove(Server.MapPath(Settings.ImagesPath + "profil/" + hsp.ProfilObject.ResimUrl));
                                        ProfilMethods.Delete(hsp.ProfilObject);
                                    }
                                    break;
                                case "EMAIL":
                                    CustomizeControl1.MessageText = MessageBox.Show(DialogResult.Stop, "Kayıt olmak istediğiniz <b>'E-Mail'</b> adresi başkası tarafından kullanılıyor! Lütfen başka bir 'E-Mail' adresi ile tekrar deneyiniz yada eğer bu e-mail adresinin sizin olduğundan eminseniz şifremi unuttum kısımından tekrar şifre talebinde bulununuz!");
                                    break;
                                default:
                                    if ((controls["prfResimUrl"] as FileUpload).HasFile & isEditor)
                                        if (BAYMYO.UI.FileIO.Remove(Server.MapPath(Settings.ImagesPath + "profil/" + hsp.ProfilObject.ResimUrl)))
                                            hsp.ProfilObject.ResimUrl = BAYMYO.UI.FileIO.UploadImage(controls["prfResimUrl"] as FileUpload, hsp.Adi + " " + hsp.Soyadi, Server.MapPath(Settings.ImagesPath + "profil/"), 260, true); ;
                                    if (string.IsNullOrEmpty(hsp.ProfilObject.ID) & isEditor)
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
                                                ViewState["TempID"] = string.Empty;
                                                CustomizeControl1.MessageText = MessageBox.Show(ProccesType.Update, DialogResult.Succes);
                                                break;
                                        }
                                    }
                                    else if (!string.IsNullOrEmpty(hsp.ProfilObject.ID) & isEditor)
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
                                                ViewState["TempID"] = string.Empty;
                                                CustomizeControl1.MessageText = MessageBox.Show(ProccesType.Update, DialogResult.Succes);
                                                break;
                                        }
                                    }
                                    break;
                            }
                        }
                        else
                        {
                            hsp.KayitTarihi = DateTime.Now;
                            string hid = BAYMYO.UI.Converts.NullToGuidString(HesapMethods.Insert(hsp));
                            if (!string.IsNullOrEmpty(hid) & isEditor)
                            {
                                ViewState["TempID"] = hid;
                                hsp.ID = hid;
                                hsp.ProfilObject.ID = hsp.ID;
                                hsp.ProfilObject.ResimUrl = BAYMYO.UI.FileIO.UploadImage(controls["prfResimUrl"] as FileUpload, hsp.Adi + " " + hsp.Soyadi, Server.MapPath(Settings.ImagesPath + "profil/"), 260, true); ;
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
                                        ViewState["TempID"] = string.Empty;
                                        CustomizeControl1.MessageText = MessageBox.Show(ProccesType.Insert, DialogResult.Succes);
                                        break;
                                }
                                Core.ClearControls(controls);
                            }
                        }
                    }
                else
                    CustomizeControl1.MessageText = MessageBox.IsNotNull();
            }
            catch (Exception ex)
            {
                CustomizeControl1.MessageText = MessageBox.Show(DialogResult.Error, ex.Message);
            }
            finally
            {
                jSonData.CreateData("doktorlar");
            }
        }
        void hesap_RemoveClick(SortedDictionary<string, Control> controls)
        {
            try
            {
                if (!string.IsNullOrEmpty(Request.QueryString["uid"]))
                {
                    using (Hesap hsp = HesapMethods.GetHesap(BAYMYO.UI.Converts.NullToGuidString(Request.QueryString["uid"])))
                    {
                        BAYMYO.UI.FileIO.Remove(Server.MapPath(Settings.JSonPath + "maps/" + hsp.ID + ".js"));
                        MakaleMethods.Delete(hsp.ID);
                        if (!string.IsNullOrEmpty(hsp.ProfilObject.ID))
                        {
                            if (ProfilMethods.Delete(hsp.ProfilObject) > 0)
                            {
                                BAYMYO.UI.FileIO.Remove(Server.MapPath(Settings.ImagesPath + "profil/" + hsp.ProfilObject.ResimUrl));
                                HesapMethods.Delete(hsp);
                                CustomizeControl1.MessageText = MessageBox.Show(ProccesType.Delete, DialogResult.Succes);
                                Core.ClearControls(controls);
                            }
                        }
                        else
                        {
                            HesapMethods.Delete(hsp);
                            CustomizeControl1.MessageText = MessageBox.Show(ProccesType.Delete, DialogResult.Succes);
                            Core.ClearControls(controls);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                CustomizeControl1.MessageText = MessageBox.Show(DialogResult.Error, ex.Message);
            }
            finally
            {
                jSonData.CreateData("doktorlar");
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {

        }
    }
}