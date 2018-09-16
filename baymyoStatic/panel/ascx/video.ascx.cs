using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace baymyoStatic.panel.ascx
{
    public partial class video : System.Web.UI.UserControl
    {
        protected override void OnInit(EventArgs e)
        {
            try
            {
                CustomizeControl1.FormTitle = string.Format(Settings.FormTitleFormat, "Video", "Ekleme/Düzeltme Formu");
                if (Request.QueryString["vid"] != null)
                    ViewState["tempID"] = Request.QueryString["vid"];
                using (Video m = VideoMethods.GetVideo(BAYMYO.UI.Converts.NullToInt64(ViewState["tempID"])))
                {
                    bool notNull = (m.ID > 0), isAdmin = Core.IsUserAdmin;
                    if (notNull) Default(m, isAdmin);

                    TextBox txt = new TextBox();
                    txt.ID = "Baslik";
                    txt.CssClass = "form-control";
                    txt.Text = m.Baslik;
                    txt.MaxLength = 75;
                    txt.ClientIDMode = ClientIDMode.Static;
                    CustomizeControl1.AddControl("Başlık", txt);

                    txt = new TextBox();
                    txt.ID = "Embed";
                    txt.CssClass = "form-control";
                    txt.Text = m.Embed;
                    txt.TextMode = TextBoxMode.MultiLine;
                    txt.MaxLength = 750;
                    txt.ClientIDMode = ClientIDMode.Static;
                    CustomizeControl1.AddControl("Embed", txt, "Her hangi bir video sitesinden 'embed' kodu almanız gereklidir.");

                    txt = new TextBox();
                    txt.ID = "Etiket";
                    txt.CssClass = "form-control";
                    txt.Text = m.Etiket;
                    txt.MaxLength = 100;
                    txt.ClientIDMode = ClientIDMode.Static;
                    CustomizeControl1.AddControl("Etiket", txt, string.Format("Lütfen virgül({0}) ile ayrıarak ve boşluk bırakmadan yazınız! Örnek: elma{0}meyve{0}sebze{0}bahçe", Settings.SplitFormat));

                    DropDownList ddl = new DropDownList();
                    ddl.ID = "Kategori";
                    ddl.Width = 250;
                    ddl.CssClass = "form-control";
                    ddl.DataMember = "kategori";
                    ddl.DataValueField = "id";
                    ddl.DataTextField = "adi";
                    ddl.DataSource = KategoriMethods.GetMenu("video", true);
                    ddl.DataBind();
                    ddl.SelectedValue = BAYMYO.UI.Converts.NullToString(m.KategoriID);
                    CustomizeControl1.AddControl("Kategori", ddl, "<a href=\"" + Settings.PanelPath + "?go=kategori&mdl=video\">[+] Yeni Kategori</a>");

                    Image img = new Image();
                    img.ID = "VideoResim";
                    img.Width = 250;
                    img.ToolTip = m.ResimUrl;
                    img.ClientIDMode = ClientIDMode.Static;
                    if (!string.IsNullOrEmpty(m.ResimUrl))
                        img.ImageUrl = Settings.ImagesPath + "video/" + m.ResimUrl;
                    else
                        img.ImageUrl = Settings.ImagesPath + "admin-yok.png";
                    CustomizeControl1.AddControl("Resim", img);

                    txt = new TextBox();
                    txt.ID = "ImgUrl";
                    txt.CssClass = "form-control";
                    txt.ClientIDMode = ClientIDMode.Static;
                    CustomizeControl1.AddControl("Image Url", txt, "Video bağlantısı eklendiğinde otomatik olarak resim adresi alır.");

                    FileUpload flu = new FileUpload();
                    flu.ID = "ResimUrl";
                    flu.CssClass = "form-control";
                    flu.ToolTip = m.ResimUrl;
                    CustomizeControl1.AddControl("Resim Ekle", flu, "Genişlik(W):300px - Yükseklik(H):150px");

                    CheckBoxList chkList = new CheckBoxList();
                    chkList.ID = "chkList";
                    chkList.RepeatDirection = RepeatDirection.Horizontal;
                    chkList.Items.Add("Gösterim Sayı");
                    chkList.Items[0].Selected = notNull ? m.GosterimSayi : true;
                    chkList.Items.Add("Üyelere Özel");
                    chkList.Items[1].Selected = m.Uye;
                    chkList.Items.Add("Yorumları Göster");
                    chkList.Items[2].Selected = notNull ? m.Yorum : false;
                    chkList.Items.Add("Yönetici Onayı");
                    chkList.Items[3].Selected = notNull ? m.YoneticiOnay : isAdmin;
                    chkList.Items[3].Enabled = isAdmin;
                    chkList.Items.Add("Yayımla");
                    chkList.Items[4].Selected = notNull ? m.Aktif : isAdmin;
                    chkList.Items[4].Enabled = isAdmin;
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

        void Default(Video m, bool isAdmin)
        {
            if (Core.CurrentUser.Tipi == AccountType.Editor & m.HesapID != Core.CurrentUser.ID)
                Response.Redirect(Settings.VirtualPath + "panel", false);
            ViewState["tempID"] = m.ID;
            CustomizeControl1.RemoveVisible = isAdmin;
            CustomizeControl1.StatusText = string.Format(Settings.ShortcutFormat, Core.CreateLink("video", m.ID, m.Baslik), "video", m.ID);
        }

        void CustomizeControl1_SubmitClick(SortedDictionary<string, Control> controls)
        {
            try
            {
                if (!string.IsNullOrEmpty(((TextBox)controls["Baslik"]).Text)
                & !string.IsNullOrEmpty(((TextBox)controls["Embed"]).Text)
                & ((DropDownList)controls["Kategori"]).SelectedIndex > 0)
                    using (Video m = VideoMethods.GetVideo(BAYMYO.UI.Converts.NullToInt64(ViewState["tempID"])))
                    {
                        m.Baslik = ((TextBox)controls["Baslik"]).Text.Trim();
                        m.Embed = ((TextBox)controls["Embed"]).Text;
                        m.Etiket = ((TextBox)controls["Etiket"]).Text;
                        m.KategoriID = BAYMYO.UI.Converts.NullToString(((DropDownList)controls["Kategori"]).SelectedValue);
                        m.GuncellemeTarihi = DateTime.Now;
                        m.GosterimSayi = ((CheckBoxList)controls["chkList"]).Items[0].Selected;
                        m.Uye = ((CheckBoxList)controls["chkList"]).Items[1].Selected;
                        m.Yorum = ((CheckBoxList)controls["chkList"]).Items[2].Selected;
                        if (Core.IsUserAdmin)
                        {
                            m.YoneticiOnay = ((CheckBoxList)controls["chkList"]).Items[3].Selected;
                            m.Aktif = ((CheckBoxList)controls["chkList"]).Items[4].Selected;
                        }
                        else
                        {
                            m.YoneticiOnay = false;
                            m.Aktif = false;
                        }
                        if (m.ID > 0)
                        {
                            if ((controls["ResimUrl"] as FileUpload).HasFile)
                            {
                                if (BAYMYO.UI.FileIO.Remove(Server.MapPath(Settings.ImagesPath + "video/" + m.ResimUrl)))
                                    m.ResimUrl = BAYMYO.UI.FileIO.UploadImage(controls["ResimUrl"] as FileUpload, Server.MapPath(Settings.ImagesPath + "video/"), 360, true);
                            }
                            else if (!string.IsNullOrEmpty(((TextBox)controls["ImgUrl"]).Text))
                                m.ResimUrl = BAYMYO.UI.FileIO.DownloadImage(((TextBox)controls["ImgUrl"]).Text, Server.MapPath(Settings.ImagesPath + "video/"), m.Baslik);
                            if (VideoMethods.Update(m) > 0)
                            {
                                Core.CreateContents("video");
                                jSonData.CreateData("videolar");
                                CustomizeControl1.MessageText = MessageBox.Show(ProccesType.Update, DialogResult.Succes);
                                ((Image)controls["VideoResim"]).ImageUrl = Settings.ImagesPath + "video/" + m.ResimUrl;
                                ((TextBox)controls["Baslik"]).Focus();
                            }
                        }
                        else
                        {
                            m.HesapID = Core.CurrentUser.ID;
                            if ((controls["ResimUrl"] as FileUpload).HasFile)
                                m.ResimUrl = BAYMYO.UI.FileIO.UploadImage(controls["ResimUrl"] as FileUpload, Server.MapPath(Settings.ImagesPath + "video/"), 360, true);
                            else
                                m.ResimUrl = BAYMYO.UI.FileIO.DownloadImage(((TextBox)controls["ImgUrl"]).Text, Server.MapPath(Settings.ImagesPath + "video/"), m.Baslik);
                            m.KayitTarihi = m.GuncellemeTarihi;
                            m.ID = VideoMethods.Insert(m);
                            if (m.ID > 0)
                            {
                                Default(m, Core.IsUserAdmin);
                                Core.CreateContents("video");
                                jSonData.CreateData("videolar");
                                CustomizeControl1.MessageText = MessageBox.Show(ProccesType.Insert, DialogResult.Succes);
                                ((Image)controls["VideoResim"]).ImageUrl = Settings.ImagesPath + "video/" + m.ResimUrl;
                                ((TextBox)controls["Baslik"]).Focus();
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
                using (Video m = VideoMethods.GetVideo(BAYMYO.UI.Converts.NullToInt64(Request.QueryString["vid"])))
                {
                    if (m.ID > 0)
                    {
                        BAYMYO.UI.FileIO.Remove(Server.MapPath(Settings.ImagesPath + "video/" + m.ResimUrl));
                        Core.RemoveForeignKey("video", m.ID.ToString());
                        if (VideoMethods.Delete(m) > 0)
                        {
                            Core.CreateContents("video");
                            jSonData.CreateData("videolar");
                            CustomizeControl1.MessageText = MessageBox.Show(ProccesType.Delete, DialogResult.Succes);
                            ((Image)controls["VideoResim"]).ImageUrl = Settings.ImagesPath + "admin-yok.png";
                            Response.Redirect(Settings.PanelPath + "?go=video", false);
                        }
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