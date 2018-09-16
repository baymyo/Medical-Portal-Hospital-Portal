using System;
using System.Collections.Generic;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace baymyoStatic.common.ascx
{
    public partial class account : System.Web.UI.UserControl
    {
        protected override void OnInit(EventArgs e)
        {
            if (Core.IsUserActive)
            {
                CustomizeControl1.MessageText = MessageBox.AccessDenied();
                CustomizeControl1.PanelVisible = false;
                return;
            }
            switch (Request.QueryString["r"])
            {
                case "sifre":
                    TextBox txt = new TextBox();
                    txt.ID = "sifre";
                    CustomizeControl1.AddControl("Yeni Şifre", txt);
                    CustomizeControl1.SubmitClick += new CustomizeControl.ButtonEvent(CustomizeControl1_SubmitClick);
                    break;
            }
            base.OnInit(e);
        }

        void CustomizeControl1_SubmitClick(SortedDictionary<string, Control> controls)
        {
            if (!string.IsNullOrEmpty(Request.QueryString["g"]) & !string.IsNullOrEmpty(Request.QueryString["s"]))
            {
                using (TextBox txtSifre = controls["sifre"] as TextBox)
                {
                    if (!string.IsNullOrEmpty(txtSifre.Text.Trim()))
                    {
                        using (BAYMYO.MultiSQLClient.MParameterCollection param = new BAYMYO.MultiSQLClient.MParameterCollection())
                        {
                            param.Add("id", Request.QueryString["g"], BAYMYO.MultiSQLClient.MSqlDbType.VarChar);
                            param.Add("onaykodu", Request.QueryString["s"], BAYMYO.MultiSQLClient.MSqlDbType.VarChar);
                            using (Hesap m = HesapMethods.GetHesap(System.Data.CommandType.Text, "select * from hesap where id=?id and onaykodu=?onaykodu and aktivasyon=1 and aktif=1 limit 1", param))
                            {
                                if (!string.IsNullOrEmpty(m.ID))
                                {
                                    m.OnayKodu = Core.GenerateSecurityCode();
                                    m.Sifre = System.Web.Security.FormsAuthentication.HashPasswordForStoringInConfigFile(txtSifre.Text.Trim(), "md5");
                                    if (!string.IsNullOrEmpty(HesapMethods.Update(m)))
                                    {
                                        string m_MailMesaj = BAYMYO.UI.FileIO.ReadText(Server.MapPath(Settings.ViewPath) + "PasswordNew.view");
                                        m_MailMesaj = m_MailMesaj.Replace("%SiteUrl%", Settings.SiteUrl);
                                        m_MailMesaj = m_MailMesaj.Replace("%SiteTitle%", Settings.Site.Title);
                                        m_MailMesaj = m_MailMesaj.Replace("%VirtualPath%", Settings.VirtualPath);
                                        m_MailMesaj = m_MailMesaj.Replace("%ReturnUrl", Request.QueryString["ReturnUrl"]);
                                        m_MailMesaj = m_MailMesaj.Replace("%IP%", Context.Request.ServerVariables["REMOTE_ADDR"].ToString());
                                        m_MailMesaj = m_MailMesaj.Replace("%ID%", m.ID.ToString());
                                        m_MailMesaj = m_MailMesaj.Replace("%Adi%", m.Adi).Replace("%Soyadi%", m.Soyadi);
                                        m_MailMesaj = m_MailMesaj.Replace("%Mail%", m.Mail);
                                        m_MailMesaj = m_MailMesaj.Replace("%Sifre%", txtSifre.Text);
                                        Core.SendMail(m.Mail, m.Adi + " " + m.Soyadi, "Şifre Değiştirildi", m_MailMesaj, true);
                                        CustomizeControl1.MessageText = MessageBox.Show(DialogResult.Succes, string.Format("Şifre değiştirme işleminiz başarılı bir şekilde gerçekleştirildi. Lütfen sisteme giriş yapmak için <a class=\"toolTip\" title=\"Üye girişi yapmak için tıklayın!\" href=\"{0}\">buraya tıklayın</a>.", Settings.VirtualPath + "?go=login"));
                                        CustomizeControl1.PanelVisible = false;
                                        m_MailMesaj = null;
                                    }
                                    else
                                    {
                                        CustomizeControl1.MessageText = MessageBox.UnSuccessful();
                                        CustomizeControl1.PanelVisible = false;
                                    }
                                }
                                else
                                {
                                    CustomizeControl1.MessageText = MessageBox.UnSuccessful();
                                    CustomizeControl1.PanelVisible = false;
                                }
                            }
                        }
                    }
                }
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                if (!string.IsNullOrEmpty(Request.QueryString["g"]) & !string.IsNullOrEmpty(Request.QueryString["s"]))
                {
                    switch (Request.QueryString["r"])
                    {
                        case "sifre":
                            this.Page.Title = "Yeni Şifre İşlemi";
                            CustomizeControl1.MessageText = MessageBox.Show(DialogResult.Info, "Yeni şifrenizi ilgili alana giriniz ve gönder düğmesine tıklayınız.");
                            CustomizeControl1.FormTitle = string.Format(Settings.FormTitleFormat, "Şifre", "Değiştirme Formu");
                            CustomizeControl1.PanelVisible = true;
                            break;
                        case "aktivasyon":
                            this.Page.Title = "Aktivasyon İşlemi";
                            CustomizeControl1.PanelVisible = false;
                            using (BAYMYO.MultiSQLClient.MParameterCollection param = new BAYMYO.MultiSQLClient.MParameterCollection())
                            {
                                param.Add("id", Request.QueryString["g"], BAYMYO.MultiSQLClient.MSqlDbType.VarChar);
                                param.Add("onaykodu", Request.QueryString["s"], BAYMYO.MultiSQLClient.MSqlDbType.VarChar);
                                using (Hesap m = HesapMethods.GetHesap(System.Data.CommandType.Text, "select * from hesap where id=?id and onaykodu=?onaykodu and aktivasyon=0 and aktif=0 limit 1", param))
                                {
                                    if (!string.IsNullOrEmpty(m.ID))
                                    {
                                        m.OnayKodu = Core.GenerateSecurityCode();
                                        m.Aktivasyon = true;
                                        switch (m.Tipi)
                                        {
                                            case AccountType.Admin:
                                            case AccountType.Private:
                                            case AccountType.Doctor:
                                            case AccountType.Editor:
                                                m.Aktif = false;
                                                break;
                                            default:
                                                m.Aktif = true;
                                                break;
                                        }
                                        if (!string.IsNullOrEmpty(HesapMethods.Update(m)))
                                            CustomizeControl1.MessageText = MessageBox.Show(DialogResult.Succes, string.Format("Aktivasyon işleminiz başarılı bir şekilde gerçekleştirildi. Lütfen sisteme giriş yapmak için <a class=\"toolTip\" title=\"Üye girişi yapmak için tıklayın!\" href=\"{0}\">buraya tıklayın</a>.", Settings.VirtualPath + "?go=login"));
                                        else
                                            CustomizeControl1.MessageText = MessageBox.UnSuccessful();
                                    }
                                    else
                                        CustomizeControl1.MessageText = MessageBox.Show(DialogResult.Warning, string.Format("Bu işlemi daha önce gerçekleştirdiğiniz için tekrar aktivasyon işlemi gerçekleştiremezsiniz. Lütfen sisteme giriş yapmak için <a class=\"toolTip\" title=\"Üye girişi yapmak için tıklayın!\" href=\"{0}\">buraya tıklayın</a>.", Settings.VirtualPath + "?go=login"));
                                }
                            }
                            break;
                        default:
                            CustomizeControl1.MessageText = MessageBox.UnSuccessful();
                            CustomizeControl1.PanelVisible = false;
                            return;
                    }
                }
                else
                {
                    CustomizeControl1.MessageText = MessageBox.UnSuccessful();
                    CustomizeControl1.PanelVisible = false;
                }
            }
            CustomizeControl1.IsValidated = true;
        }
    }
}