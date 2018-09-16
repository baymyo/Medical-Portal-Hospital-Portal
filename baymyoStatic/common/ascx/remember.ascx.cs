using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace baymyoStatic.common.ascx
{
    public partial class remember : System.Web.UI.UserControl
    {
        protected override void OnInit(EventArgs e)
        {
            if (Core.IsUserActive)
            {
                CustomizeControl1.MessageText = MessageBox.AccessDenied();
                CustomizeControl1.PanelVisible = false;
                return;
            }
            //<a class=\"toolTip\" title=\"Yeni Kullanıcı kayıtı için tıkla.\" href=\"{0}?l=2\">Yeni Üye Kayıt</a>&nbsp;&nbsp;-&nbsp;&nbsp;
            switch (Request.QueryString["r"])
            {
                case "sifre":
                    CustomizeControl1.FormTitle = string.Format(Settings.FormTitleFormat, "Şifre", "Hatırlatma Formu");
                    CustomizeControl1.MessageText = MessageBox.Show(DialogResult.Info, "Yeni <b>Şifre</b> talebinde bulunmak için aşağıdaki alana mail adresinizi giriniz ve mail adresinize gönderilen güvenlik bağlantısına tıklayınız. Karşınıza gelen ekranda yeni şifrenizi girerek işleminizi gerçekşleştiriniz.");
                    //CustomizeControl1.StatusText = string.Format("<a rel=\"nofollow\" class=\"toolTip\" title=\"Kullanıcı girişi yapmak için tıkla!\" href=\"{0}?go=login\">Giriş Ekranı</a>&nbsp;&nbsp;-&nbsp;&nbsp;<a rel=\"nofollow\" class=\"toolTip\" title=\"Aktivasyon talep formu için tıklayın.\" href=\"{0}?go=register&r=aktivasyon\">Aktivasyon Kodu</a>", Settings.VirtualPath);
                    this.Page.Title = "Yeni Şifre Talep Formu";
                    break;
                case "aktivasyon":
                    CustomizeControl1.FormTitle = string.Format(Settings.FormTitleFormat, "Aktivasyon", "Talep Formu");
                    CustomizeControl1.MessageText = MessageBox.Show(DialogResult.Info, "Yeni <b>Aktivasyon Kodu</b> talebinde bulunmak için aşağıdaki kutuya mail adresinizi yazınız ve mailinize gönderilen güvenlik bağlantısına tıklayarak aktivasyon işleminizi gerçekleştiriniz.");
                    //CustomizeControl1.StatusText = string.Format("<a rel=\"nofollow\" class=\"toolTip\" title=\"Kullanıcı girişi yapmak için tıkla!\" href=\"{0}?go=login\">Giriş Ekranı</a>&nbsp;&nbsp;-&nbsp;&nbsp;<a rel=\"nofollow\" class=\"toolTip\" title=\"Şifre hatırlatma ekranı için tıklayın.\" href=\"{0}?go=register&r=sifre\">Şifremi Unuttum</a>", Settings.VirtualPath);
                    this.Page.Title = "Yeni Aktivasyon Kodu Talep Formu";
                    break;
                default:
                    CustomizeControl1.MessageText = MessageBox.UnSuccessful();
                    CustomizeControl1.PanelVisible = false;
                    return;
            }
            TextBox txt = new TextBox();
            txt.ID = "Mail";
            txt.MaxLength = 60;
            txt.CssClass = "form-control";
            txt.TextMode = TextBoxMode.Email;
            CustomizeControl1.AddControl("Mail", txt);
            CustomizeControl1.SubmitClick += new CustomizeControl.ButtonEvent(CustomizeControl1_SubmitClick);
            base.OnInit(e);
        }

        void CustomizeControl1_SubmitClick(SortedDictionary<string, Control> controls)
        {
            using (TextBox txtMail = controls["Mail"] as TextBox)
            {
                if (!string.IsNullOrEmpty(txtMail.Text.Trim()))
                {
                    using (BAYMYO.MultiSQLClient.MParameterCollection param = new BAYMYO.MultiSQLClient.MParameterCollection())
                    {
                        string query = "", subject = "", message = "", info = "", warning = "";
                        switch (Request.QueryString["r"])
                        {
                            case "sifre":
                                subject = "Şifre Değiştirme Talebi";
                                message = "PasswordChanged.view";
                                query = "select * from hesap where mail=?mail and aktivasyon=1 and aktif=1 limit 1";
                                info = "Şifre değiştirme işleminizin tamamlanması için mail adresinize gönderilen bilgileri kontrol ederek onaylayınız.";
                                warning = "Şifre talebi işleminizi gerçekleştiremiyoruz bunun sebebi yazmış olduğunuz mail adresine kayıtlı bir üyeliğin bulunamamasıdır! Üyelik işlemlerinizi daha önce aktif ettiğinizden emin olunuz.";
                                break;
                            case "aktivasyon":
                                subject = "Aktivasyon Talebi";
                                message = "Activation.view";
                                query = "select * from hesap where mail=?mail and aktivasyon=0 and aktif=0 limit 1";
                                info = "Aktivasyon işleminizin tamamlanması için mail adresinize gönderilen bilgileri kontrol ederek onaylayınız.";
                                warning = "Aktivasyon talep işleminizi gerçekleştiremiyoruz bunun sebebi yazmış olduğunuz mail adresi daha önce bu işlemi gerçekleştirmiş yada sistemimizde kayıtlı böyle bir mail adresi olmayabilir.";
                                break;
                        }
                        if (!string.IsNullOrEmpty(query))
                        {
                            param.Add("mail", txtMail.Text.Trim(), BAYMYO.MultiSQLClient.MSqlDbType.VarChar);
                            using (Hesap m = HesapMethods.GetHesap(System.Data.CommandType.Text, query, param))
                            {
                                if (!string.IsNullOrEmpty(m.ID))
                                {
                                    string m_MailMesaj = BAYMYO.UI.FileIO.ReadText(Server.MapPath(Settings.ViewPath) + message);
                                    m_MailMesaj = m_MailMesaj.Replace("%SiteUrl%", Settings.SiteUrl);
                                    m_MailMesaj = m_MailMesaj.Replace("%SiteTitle%", Settings.Site.Title);
                                    m_MailMesaj = m_MailMesaj.Replace("%VirtualPath%", Settings.VirtualPath);
                                    m_MailMesaj = m_MailMesaj.Replace("%IP%", Context.Request.ServerVariables["REMOTE_ADDR"].ToString());
                                    m_MailMesaj = m_MailMesaj.Replace("%ID%", m.ID.ToString());
                                    m_MailMesaj = m_MailMesaj.Replace("%Adi%", m.Adi).Replace("%Soyadi%", m.Soyadi);
                                    m_MailMesaj = m_MailMesaj.Replace("%Mail%", m.Mail);
                                    m_MailMesaj = m_MailMesaj.Replace("%OnayKodu%", m.OnayKodu);
                                    try
                                    {
                                        Core.SendMail(m.Mail, m.Adi + " " + m.Soyadi, subject, m_MailMesaj, true);
                                        CustomizeControl1.MessageText = MessageBox.Show(DialogResult.Info, info);
                                        ((TextBox)controls["Mail"]).Text = string.Empty;
                                        ((TextBox)controls["Mail"]).Enabled = false;
                                        CustomizeControl1.PanelVisible = false;
                                        CustomizeControl1.SubmitEnabled = true;
                                    }
                                    catch (Exception)
                                    {
                                        CustomizeControl1.MessageText = MessageBox.Show(DialogResult.Error, "Sunucularımızdaki yoğunlukdan dolayı mail gönderme işlemi şuan için başarısızlıkla sonuçlandı. Lütfen bu işleminizi daha sonra tekrar deneyiniz.");
                                    }
                                    m_MailMesaj = null;
                                }
                                else
                                    CustomizeControl1.MessageText = MessageBox.Show(DialogResult.Warning, warning);
                            }
                        }
                        query = null; subject = null; message = null;
                    }
                }
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            CustomizeControl1.IsValidated = true;
        }
    }
}