using System;
using System.Collections.Generic;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace baymyoStatic.panel.ascx
{
    public partial class mesaj : System.Web.UI.UserControl
    {
        protected override void OnInit(EventArgs e)
        {
            try
            {
                CustomizeControl1.FormTitle = string.Format(Settings.FormTitleFormat, "Soru", "Ekleme/Yanıtlama Formu");
                if (Request.QueryString["mid"] != null)
                    ViewState["tempID"] = Request.QueryString["mid"];
                using (Mesaj m = MesajMethods.GetMesaj(BAYMYO.UI.Converts.NullToInt64(ViewState["tempID"])))
                {
                    bool notNull = (m.ID > 0), isAdmin = Core.IsUserAdmin;
                    if (notNull) Default(m, isAdmin);

                    TextBox txt = new TextBox();
                    txt.ID = "Adi";
                    txt.CssClass = "form-control";
                    txt.Text = m.Adi;
                    txt.MaxLength = 35;
                    CustomizeControl1.AddControl("Adı", txt);

                    txt = new TextBox();
                    txt.ID = "Mail";
                    txt.CssClass = "form-control";
                    txt.Text = m.Mail;
                    txt.MaxLength = 60;
                    txt.TextMode = TextBoxMode.Email;
                    CustomizeControl1.AddControl("Mail", txt);

                    txt = new TextBox();
                    txt.ID = "Telefon";
                    txt.CssClass = "form-control";
                    txt.Text = m.Telefon;
                    txt.MaxLength = 16;
                    txt.TextMode = TextBoxMode.Phone;
                    CustomizeControl1.AddControl("Telefon", txt);

                    txt = new TextBox();
                    txt.ID = "Konu";
                    txt.CssClass = "form-control";
                    txt.Text = m.Konu;
                    txt.MaxLength = 50;
                    CustomizeControl1.AddControl("Konu", txt);

                    txt = new TextBox();
                    txt.ID = "Icerik";
                    txt.CssClass = "form-control";
                    txt.Text = m.Icerik;
                    txt.TextMode = TextBoxMode.MultiLine;
                    txt.MaxLength = 1000;
                    txt.Height = 200;
                    CustomizeControl1.AddControl("Soru", txt);

                    txt = new TextBox();
                    txt.ID = "Yanit";
                    txt.CssClass = "form-control";
                    txt.Text = m.Yanit;
                    txt.TextMode = TextBoxMode.MultiLine;
                    txt.MaxLength = 1500;
                    txt.Height = 200;
                    CustomizeControl1.AddControl("Yanit", txt);

                    DropDownList ddl = new DropDownList();
                    ddl.ID = "Durum";
                    ddl.Width = 450;
                    ddl.CssClass = "form-control";
                    ddl.DataMember = "Durumlar";
                    ddl.DataValueField = "Key";
                    ddl.DataTextField = "Value";
                    ddl.DataSource = Core.GetMessageStates();
                    ddl.DataBind();
                    ddl.SelectedValue = m.Durum.ToString();
                    CustomizeControl1.AddControl("Durum", ddl);

                    ddl = new DropDownList();
                    ddl.ID = "Aktif";
                    ddl.Width = 450;
                    ddl.CssClass = "form-control";
                    ddl.DataMember = "PublishStates";
                    ddl.DataValueField = "Key";
                    ddl.DataTextField = "Value";
                    ddl.DataSource = Core.GetPublishStates();
                    ddl.DataBind();
                    ddl.SelectedValue = m.Aktif.ToString();
                    CustomizeControl1.AddControl("Kime Görünsün", ddl);

                    CheckBox chk = new CheckBox();
                    chk.ID = "MailGonder";
                    chk.Checked = false;
                    CustomizeControl1.AddControl("Mail Gönder", chk);

                    CustomizeControl1.SubmitClick += new CustomizeControl.ButtonEvent(CustomizeControl1_SubmitClick);
                    CustomizeControl1.RemoveClick += new CustomizeControl.ButtonEvent(CustomizeControl1_RemoveClick);
                }
            }
            catch (Exception ex)
            {
                CustomizeControl1.MessageText = MessageBox.Show(DialogResult.Error, ex.Message);
            }
            base.OnInit(e);
        }

        void Default(Mesaj m, bool isAdmin)
        {
            if (Core.CurrentUser.Tipi == AccountType.Editor & m.HesapID != Core.CurrentUser.ID)
                Response.Redirect(Settings.VirtualPath + "panel", false);
            ViewState["tempID"] = m.ID;
            CustomizeControl1.RemoveVisible = isAdmin;
            CustomizeControl1.StatusText = string.Format(Settings.ShortcutFormat, Core.CreateLink("mesaj", m.ID, m.Konu), "mesaj", m.ID);
        }

        void CustomizeControl1_SubmitClick(SortedDictionary<string, Control> controls)
        {
            try
            {
                if (!string.IsNullOrEmpty(((TextBox)controls["Adi"]).Text)
                    & !string.IsNullOrEmpty(((TextBox)controls["Mail"]).Text)
                    & !string.IsNullOrEmpty(((TextBox)controls["Icerik"]).Text)
                    & !string.IsNullOrEmpty(((TextBox)controls["Yanit"]).Text))
                    using (Mesaj m = MesajMethods.GetMesaj(BAYMYO.UI.Converts.NullToInt64(ViewState["tempID"])))
                    {
                        m.Adi = ((TextBox)controls["Adi"]).Text;
                        m.Mail = ((TextBox)controls["Mail"]).Text;
                        m.Telefon = ((TextBox)controls["Telefon"]).Text;
                        m.Konu = ((TextBox)controls["Konu"]).Text;
                        m.Icerik = ((TextBox)controls["Icerik"]).Text;
                        m.Yanit = ((TextBox)controls["Yanit"]).Text;
                        m.GuncellemeTarihi = DateTime.Now;
                        m.Durum = BAYMYO.UI.Converts.NullToByte(((DropDownList)controls["Durum"]).SelectedValue);
                        if (Core.IsUserAdmin)
                            m.Aktif = BAYMYO.UI.Converts.NullToBool(((DropDownList)controls["Aktif"]).SelectedValue);
                        else
                            m.Aktif = false;
                        if (m.ID > 0)
                        {
                            if (MesajMethods.Update(m) > 0)
                            {
                                if (((CheckBox)controls["MailGonder"]).Checked)
                                {
                                    if (Core.SendMail(m.Mail, m.Adi, Settings.Site.ContactMail, Settings.Site.ContactName, m.Konu, m.Icerik, true))
                                        MessageBox.Show(Page, "Güncelleme ve Mail gönderme işleminiz başarılı bir şekilde tamamlandı.!");
                                    else
                                        MessageBox.Show(Page, "Mail gönderilemedi fakat güncelleme işlemi tamamlandı!");
                                }
                                else
                                    CustomizeControl1.MessageText = MessageBox.Show(ProccesType.Update, DialogResult.Succes);
                            }
                        }
                        else
                        {
                            m.HesapID = Core.CurrentUser.ID;
                            m.KayitTarihi = m.GuncellemeTarihi;
                            m.ID = MesajMethods.Insert(m);
                            if (m.ID > 0)
                            {
                                Default(m, Core.IsUserAdmin);
                                CustomizeControl1.MessageText = MessageBox.Show(ProccesType.Insert, DialogResult.Succes);
                                ((TextBox)controls["Adi"]).Focus();
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
        }

        void CustomizeControl1_RemoveClick(SortedDictionary<string, Control> controls)
        {
            try
            {
                Int64 id = BAYMYO.UI.Converts.NullToInt64(ViewState["tempID"]);
                if (id > 0)
                {
                    Core.RemoveForeignKey("mesaj", id.ToString());
                    if (MesajMethods.Delete(id) > 0)
                    {
                        CustomizeControl1.MessageText = MessageBox.Show(ProccesType.Delete, DialogResult.Succes);
                        Response.Redirect(Settings.PanelPath + "?go=mesaj", false);
                    }
                }
                id = 0;
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