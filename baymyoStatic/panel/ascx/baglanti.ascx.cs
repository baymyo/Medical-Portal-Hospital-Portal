using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace baymyoStatic.panel.ascx
{
    public partial class baglanti : System.Web.UI.UserControl
    {
        protected override void OnInit(EventArgs e)
        {
            try
            {
                CustomizeControl1.FormTitle = string.Format(Settings.FormTitleFormat, "Bağlantı", "Tanımlama");
                if (Request.QueryString["fid"] != null)
                    ViewState["tempID"] = Request.QueryString["fid"];
                using (Firma m = FirmaMethods.GetFirma(BAYMYO.UI.Converts.NullToInt64(ViewState["tempID"])))
                {
                    bool notNull = (m.ID > 0), isAdmin = Core.IsUserAdmin;
                    if (notNull) Default(m, isAdmin);

                    Image img = new Image();
                    img.ID = "BuyukResim";
                    img.Width = 250;
                    img.ToolTip = m.ResimUrl;
                    if (!string.IsNullOrEmpty(m.ResimUrl))
                        img.ImageUrl = Settings.ImagesPath + "firma/b/" + m.ResimUrl;
                    else
                        img.ImageUrl = Settings.ImagesPath + "admin-yok.png";
                    CustomizeControl1.AddControl("Büyük Resim", img);

                    FileUpload flu = new FileUpload();
                    flu.ID = "ResimUrl";
                    flu.ToolTip = m.ResimUrl;
                    CustomizeControl1.AddControl("Resim Ekle", flu);

                    img = new Image();
                    img.ID = "KucukResim";
                    img.Width = 150;
                    if (!string.IsNullOrEmpty(m.ResimUrl))
                        img.ImageUrl = Settings.ImagesPath + "firma/" + m.ResimUrl;
                    else
                        img.ImageUrl = Settings.ImagesPath + "admin-yok.png";
                    CustomizeControl1.AddControl("Küçük Resim", img);

                    flu = new FileUpload();
                    flu.ID = "KucukResimUrl";
                    CustomizeControl1.AddControl("Küçük Resim Ekle", flu, "<b>Bu alanda resim seçmezseniz büyük resim küçültülecektir.</b> Genişlik(W):350px/Yükseklik(H):140px");

                    TextBox txt = new TextBox();
                    txt.ID = "Adi";
                    txt.CssClass = "form-control";
                    txt.Text = m.Adi;
                    txt.MaxLength = 75;
                    CustomizeControl1.AddControl("Bağlantı Adı", txt);

                    txt = new TextBox();
                    txt.ID = "Yetkili";
                    txt.CssClass = "form-control";
                    txt.Text = m.Yetkili;
                    txt.MaxLength = 40;
                    CustomizeControl1.AddControl("Yetkili", txt);

                    txt = new TextBox();
                    txt.ID = "Adres";
                    txt.CssClass = "form-control";
                    txt.Text = m.Adres;
                    txt.TextMode = TextBoxMode.MultiLine;
                    CustomizeControl1.AddControl("Adres", txt);

                    txt = new TextBox();
                    txt.ID = "Mail";
                    txt.CssClass = "form-control";
                    txt.Text = m.Mail;
                    txt.MaxLength = 60;
                    CustomizeControl1.AddControl("Mail", txt);

                    txt = new TextBox();
                    txt.ID = "Web";
                    txt.CssClass = "form-control";
                    txt.Text = m.Web;
                    txt.MaxLength = 60;
                    CustomizeControl1.AddControl("Web", txt);

                    txt = new TextBox();
                    txt.ID = "Telefon1";
                    txt.CssClass = "form-control";
                    txt.Text = m.Telefon1;
                    txt.MaxLength = 16;
                    CustomizeControl1.AddControl("Telefon (1)", txt);

                    txt = new TextBox();
                    txt.ID = "Telefon2";
                    txt.CssClass = "form-control";
                    txt.Text = m.Telefon2;
                    txt.MaxLength = 16;
                    CustomizeControl1.AddControl("Telefon (2)", txt);

                    txt = new TextBox();
                    txt.ID = "GSM";
                    txt.CssClass = "form-control";
                    txt.Text = m.GSM;
                    txt.MaxLength = 16;
                    CustomizeControl1.AddControl("GSM", txt);

                    DropDownList ddl = new DropDownList();
                    ddl.ID = "Kategori";
                    ddl.Width = 250;
                    ddl.DataMember = "kategori";
                    ddl.DataValueField = "id";
                    ddl.DataTextField = "adi";
                    ddl.CssClass = "form-control";
                    ddl.DataSource = KategoriMethods.GetMenu("firma", true);
                    ddl.DataBind();
                    ddl.SelectedValue = BAYMYO.UI.Converts.NullToString(m.KategoriID);
                    CustomizeControl1.AddControl("Kategori", ddl, "<a href=\"" + Settings.PanelPath + "?go=kategori&mdl=firma\">[+] Yeni Kategori</a>");

                    ddl = new DropDownList();
                    ddl.ID = "Sehir";
                    ddl.Width = 250;
                    ddl.DataMember = "Sehir";
                    ddl.DataValueField = "Adi";
                    ddl.DataTextField = "Adi";
                    ddl.CssClass = "form-control";
                    SehirCollection sehirler = SehirMethods.GetSelect();
                    sehirler.Insert(0, new Sehir(0, ""));
                    ddl.DataSource = sehirler;
                    ddl.DataBind();
                    ddl.Text = BAYMYO.UI.Converts.NullToString(m.Sehir);
                    CustomizeControl1.AddControl("Şehir (İL)", ddl);

                    CheckBoxList chkList = new CheckBoxList();
                    chkList.ID = "chkList";
                    chkList.RepeatDirection = RepeatDirection.Horizontal;
                    chkList.Items.Add("Gösterim Sayı");
                    chkList.Items[0].Selected = (notNull) ? m.GosterimSayi : true;
                    chkList.Items.Add("Yönetici Onayı");
                    chkList.Items[1].Selected = (notNull) ? m.YoneticiOnay : isAdmin;
                    chkList.Items[1].Enabled = isAdmin;
                    chkList.Items.Add("Yayımla");
                    chkList.Items[2].Selected = (notNull) ? m.Aktif : isAdmin;
                    chkList.Items[2].Enabled = isAdmin;
                    CustomizeControl1.AddControl("Seçimler", chkList);

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

        void Default(Firma m, bool isAdmin)
        {
            if (Core.CurrentUser.Tipi == AccountType.Editor & m.HesapID != Core.CurrentUser.ID)
                Response.Redirect(Settings.VirtualPath + "panel", false);
            ViewState["tempID"] = m.ID;
            CustomizeControl1.RemoveVisible = isAdmin;
            CustomizeControl1.StatusText = string.Format(Settings.ShortcutFormat, Core.CreateLink("firma", m.ID, m.Adi), "firma", m.ID);
        }

        void CustomizeControl1_SubmitClick(SortedDictionary<string, Control> controls)
        {
            try
            {
                string tempBaslik = null, tempPath = null;
                if (Core.IsUserActive
                    & !string.IsNullOrEmpty(((TextBox)controls["Adi"]).Text)
                    & !string.IsNullOrEmpty(((TextBox)controls["Adres"]).Text))
                    using (Firma m = FirmaMethods.GetFirma(BAYMYO.UI.Converts.NullToInt64(ViewState["tempID"])))
                    {
                        bool isAdmin = Core.IsUserAdmin;
                        m.Adi = ((TextBox)controls["Adi"]).Text.Trim();
                        m.Yetkili = ((TextBox)controls["Yetkili"]).Text;
                        m.Adres = ((TextBox)controls["Adres"]).Text;
                        m.Mail = ((TextBox)controls["Mail"]).Text;
                        m.Web = BAYMYO.UI.Converts.NullToString(((TextBox)controls["Web"]).Text).ToLower().Replace("http://", "");
                        m.Telefon1 = ((TextBox)controls["Telefon1"]).Text;
                        m.Telefon2 = ((TextBox)controls["Telefon2"]).Text;
                        m.GSM = ((TextBox)controls["GSM"]).Text;
                        m.KategoriID = BAYMYO.UI.Converts.NullToString(((DropDownList)controls["Kategori"]).SelectedValue);
                        m.Sehir = BAYMYO.UI.Converts.NullToString(((DropDownList)controls["Sehir"]).SelectedValue);
                        m.GuncellemeTarihi = DateTime.Now;
                        m.GosterimSayi = ((CheckBoxList)controls["chkList"]).Items[0].Selected;
                        if (isAdmin)
                        {
                            m.YoneticiOnay = ((CheckBoxList)controls["chkList"]).Items[1].Selected;
                            m.Aktif = ((CheckBoxList)controls["chkList"]).Items[2].Selected;
                        }
                        else
                        {
                            m.YoneticiOnay = false;
                            m.Aktif = false;
                        }
                        tempBaslik = Core.ReplaceToLover(m.Adi);
                        tempPath = Settings.ImagesPath + "firma/";
                        if (m.ID > 0)
                        {
                            if ((controls["ResimUrl"] as FileUpload).HasFile)
                            {
                                bool sil = BAYMYO.UI.FileIO.Remove(Server.MapPath(tempPath + m.ResimUrl));
                                sil = BAYMYO.UI.FileIO.Remove(Server.MapPath(tempPath + "b/" + m.ResimUrl));
                                if (sil)
                                {
                                    m.ResimUrl = BAYMYO.UI.FileIO.UploadImage(controls["ResimUrl"] as FileUpload, tempBaslik, Server.MapPath(tempPath + "b/"), 728, true);
                                    if ((controls["KucukResimUrl"] as FileUpload).HasFile)
                                        BAYMYO.UI.FileIO.UploadImage(controls["KucukResimUrl"] as FileUpload, m.ResimUrl, Server.MapPath(tempPath), 170, true, false);
                                    else
                                        BAYMYO.UI.FileIO.ResizeImage(Server.MapPath(tempPath + "b/" + m.ResimUrl), Server.MapPath(tempPath + m.ResimUrl), 170, System.IO.Path.GetExtension(m.ResimUrl).ToLower());
                                }
                            }
                            else if ((controls["KucukResimUrl"] as FileUpload).HasFile)
                            {
                                if (BAYMYO.UI.FileIO.Remove(Server.MapPath(tempPath + m.ResimUrl)))
                                    BAYMYO.UI.FileIO.UploadImage(controls["KucukResimUrl"] as FileUpload, m.ResimUrl, Server.MapPath(tempPath), 170, true, false);
                            }
                            if (FirmaMethods.Update(m) > 0)
                            {
                                Core.CreateContents("firma");
                                jSonData.CreateData("baglantilar");
                                CustomizeControl1.MessageText = MessageBox.Show(ProccesType.Update, DialogResult.Succes);
                            }
                        }
                        else
                        {
                            m.HesapID = Core.CurrentUser.ID;
                            m.KayitTarihi = m.GuncellemeTarihi;
                            m.ResimUrl = BAYMYO.UI.FileIO.UploadImage(controls["KucukResimUrl"] as FileUpload, tempBaslik, Server.MapPath(tempPath), 170, true);
                            if ((controls["ResimUrl"] as FileUpload).HasFile)
                                BAYMYO.UI.FileIO.UploadImage(controls["ResimUrl"] as FileUpload, m.ResimUrl, Server.MapPath(tempPath + "b/"), 728, true, false);
                            m.ID = FirmaMethods.Insert(m);
                            if (m.ID > 0)
                            {
                                Core.CreateContents("firma");
                                jSonData.CreateData("baglantilar");
                                Default(m, isAdmin);
                                CustomizeControl1.MessageText = MessageBox.Show(ProccesType.Insert, DialogResult.Succes);
                                ((TextBox)controls["Adi"]).Focus();
                            }
                        }
                        if (!string.IsNullOrEmpty(m.ResimUrl))
                        {
                            ((Image)controls["BuyukResim"]).ImageUrl = Settings.ImagesPath + "firma/b/" + m.ResimUrl;
                            ((Image)controls["KucukResim"]).ImageUrl = Settings.ImagesPath + "firma/" + m.ResimUrl;
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
                if (Core.IsUserActive & ViewState["tempID"] != null)
                    using (Firma m = FirmaMethods.GetFirma(BAYMYO.UI.Converts.NullToInt64(ViewState["tempID"])))
                    {
                        BAYMYO.UI.FileIO.Remove(Server.MapPath(Settings.JSonPath + "maps/firma/" + m.ID + ".js"));
                        BAYMYO.UI.FileIO.Remove(Server.MapPath(Settings.ImagesPath + "firma/b/" + m.ResimUrl));
                        BAYMYO.UI.FileIO.Remove(Server.MapPath(Settings.ImagesPath + "firma/" + m.ResimUrl));
                        if (FirmaMethods.Delete(m) > 0)
                        {
                            ((Image)controls["BuyukResim"]).ImageUrl = Settings.ImagesPath + "admin-yok.png";
                            ((Image)controls["KucukResim"]).ImageUrl = Settings.ImagesPath + "admin-yok.png";
                            Core.CreateContents("firma");
                            jSonData.CreateData("baglantilar");
                            ViewState["tempID"] = null;
                            CustomizeControl1.MessageText = MessageBox.Show(ProccesType.Delete, DialogResult.Succes);
                            Core.ClearControls(controls);
                            ((TextBox)controls["Adi"]).Focus();
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