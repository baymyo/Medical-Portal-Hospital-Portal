using System;
using System.Collections.Generic;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace baymyoStatic.panel.ascx
{
    public partial class galeri : System.Web.UI.UserControl
    {
        protected override void OnInit(EventArgs e)
        {
            try
            {
                CustomizeControl1.FormTitle = string.Format(Settings.FormTitleFormat, "Galeri", "Ekleme/Düzeltme Formu");
                using (Album m = AlbumMethods.GetAlbum(BAYMYO.UI.Converts.NullToInt64(Request.QueryString["raid"])))
                {
                    bool notNull = (m.ID > 0), isAdmin = Core.IsUserAdmin;
                    if (notNull)
                    {
                        CustomizeControl1.RemoveVisible = isAdmin;
                        CreateLinks(m);
                    }

                    TextBox txt = new TextBox();
                    txt.ID = "Adi";
                    txt.CssClass = "form-control";
                    txt.Text = m.Adi;
                    txt.MaxLength = 75;
                    CustomizeControl1.AddControl("Album Adı", txt);

                    txt = new TextBox();
                    txt.ID = "Etiket";
                    txt.CssClass = "form-control";
                    txt.Text = m.Etiket;
                    txt.MaxLength = 100;
                    CustomizeControl1.AddControl("Etiket", txt, string.Format("Lütfen virgül({0}) ile ayrıarak ve boşluk bırakmadan yazınız! Örnek: elma{0}meyve{0}sebze{0}bahçe", Settings.SplitFormat));

                    DropDownList ddl = new DropDownList();
                    ddl.ID = "Kategori";
                    ddl.Width = 250;
                    ddl.CssClass = "form-control";
                    ddl.DataMember = "kategori";
                    ddl.DataValueField = "id";
                    ddl.DataTextField = "adi";
                    ddl.DataSource = KategoriMethods.GetMenu("galeri", true);
                    ddl.DataBind();
                    ddl.SelectedValue = BAYMYO.UI.Converts.NullToString(m.KategoriID);
                    CustomizeControl1.AddControl("Kategori", ddl, "<a href=\"" + Settings.PanelPath + "?go=kategori&mdl=galeri\">[+] Yeni Kategori</a>");

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

                    int index = 1;
                    CustomizeControl1.AddTitle(index + ". Resim Bilgileri");

                    FileUpload flu = new FileUpload();
                    flu.ID = "Resim" + index;
                    flu.CssClass = "form-control";
                    CustomizeControl1.AddControl("Resim", flu, "Genişlik(W):600px");

                    txt = new TextBox();
                    txt.ID = "Aciklama" + index;
                    txt.TextMode = TextBoxMode.MultiLine;
                    txt.CssClass = "form-control";
                    txt.MaxLength = 500;
                    CustomizeControl1.AddControl("Açıklama", txt);

                    index++;
                    CustomizeControl1.AddTitle(index + ". Resim Bilgileri");

                    flu = new FileUpload();
                    flu.ID = "Resim" + index;
                    flu.CssClass = "form-control";
                    CustomizeControl1.AddControl("Resim", flu, "Genişlik(W):600px");

                    txt = new TextBox();
                    txt.ID = "Aciklama" + index;
                    txt.TextMode = TextBoxMode.MultiLine;
                    txt.CssClass = "form-control";
                    txt.MaxLength = 500;
                    CustomizeControl1.AddControl("Açıklama", txt);

                    index++;
                    CustomizeControl1.AddTitle(index + ". Resim Bilgileri");

                    flu = new FileUpload();
                    flu.ID = "Resim" + index;
                    flu.CssClass = "form-control";
                    CustomizeControl1.AddControl("Resim", flu, "Genişlik(W):600px");

                    txt = new TextBox();
                    txt.ID = "Aciklama" + index;
                    txt.TextMode = TextBoxMode.MultiLine;
                    txt.CssClass = "form-control";
                    txt.MaxLength = 500;
                    CustomizeControl1.AddControl("Açıklama", txt);

                    RadioButtonList radioList = new RadioButtonList();
                    radioList.ID = "Kapak";
                    radioList.RepeatDirection = RepeatDirection.Horizontal;
                    radioList.Items.Add("1. Resim");
                    radioList.Items.Add("2. Resim");
                    radioList.Items.Add("3. Resim");
                    CustomizeControl1.AddControl("Kapak Olarak", radioList);

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

        void CreateLinks(Album m)
        {
            CustomizeControl1.StatusText = string.Format(Settings.ShortcutFormat, Core.CreateLink("galeri", m.ID, m.Adi), "galeri", m.ID);
        }

        void CustomizeControl1_SubmitClick(SortedDictionary<string, Control> controls)
        {
            try
            {
                bool IsInsert = false;
                if (!string.IsNullOrEmpty(((TextBox)controls["Adi"]).Text)
                    & ((DropDownList)controls["Kategori"]).SelectedIndex > 0)
                    using (Album m = AlbumMethods.GetAlbum(BAYMYO.UI.Converts.NullToInt64(Request.QueryString["raid"])))
                    {
                        m.Adi = ((TextBox)controls["Adi"]).Text.Trim();
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
                            if (AlbumMethods.Update(m) > 0)
                                CustomizeControl1.MessageText = MessageBox.Show(ProccesType.Update, DialogResult.Succes);
                        }
                        else
                        {
                            m.HesapID = Core.CurrentUser.ID;
                            m.KayitTarihi = m.GuncellemeTarihi;
                            m.ID = AlbumMethods.Insert(m);
                            if (m.ID > 0)
                            {
                                IsInsert = true;
                                CustomizeControl1.MessageText = MessageBox.Show(ProccesType.Insert, DialogResult.Succes);
                            }
                        }

                        if (m.ID > 0)
                        {
                            foreach (ListItem item in ((RadioButtonList)controls["Kapak"]).Items)
                                if (item.Selected)
                                {
                                    GaleriMethods.Update(m.ID);
                                    break;
                                }
                            const int pixel = 728;
                            string path = Server.MapPath(Settings.ImagesPath + "album/" + m.ID + "/");
                            if ((controls["Resim1"] as FileUpload).HasFile)
                                using (Galeri r = new Galeri())
                                {
                                    r.AlbumID = m.ID;
                                    r.ResimUrl = BAYMYO.UI.FileIO.UploadImage(controls["Resim1"] as FileUpload, m.Adi, path, pixel, true);
                                    r.Aciklama = BAYMYO.UI.Converts.NullToString(((TextBox)controls["Aciklama1"]).Text).Trim();
                                    r.Kapak = ((RadioButtonList)controls["Kapak"]).Items[0].Selected;
                                    r.KayitTarihi = DateTime.Now;
                                    GaleriMethods.Insert(r);
                                }

                            if ((controls["Resim2"] as FileUpload).HasFile)
                                using (Galeri r = new Galeri())
                                {
                                    r.AlbumID = m.ID;
                                    r.ResimUrl = BAYMYO.UI.FileIO.UploadImage(controls["Resim2"] as FileUpload, m.Adi, path, pixel, true);
                                    r.Aciklama = BAYMYO.UI.Converts.NullToString(((TextBox)controls["Aciklama2"]).Text).Trim();
                                    r.Kapak = ((RadioButtonList)controls["Kapak"]).Items[1].Selected;
                                    r.KayitTarihi = DateTime.Now;
                                    GaleriMethods.Insert(r);
                                }

                            if ((controls["Resim3"] as FileUpload).HasFile)
                                using (Galeri r = new Galeri())
                                {
                                    r.AlbumID = m.ID;
                                    r.ResimUrl = BAYMYO.UI.FileIO.UploadImage(controls["Resim3"] as FileUpload, m.Adi, path, pixel, true);
                                    r.Aciklama = BAYMYO.UI.Converts.NullToString(((TextBox)controls["Aciklama3"]).Text).Trim();
                                    r.Kapak = ((RadioButtonList)controls["Kapak"]).Items[2].Selected;
                                    r.KayitTarihi = DateTime.Now;
                                    GaleriMethods.Insert(r);
                                }

                            foreach (ListItem item in ((RadioButtonList)controls["Kapak"]).Items)
                                item.Selected = false;

                            if (IsInsert)
                            {
                                Core.CreateContents("galeri");
                                jSonData.CreateData("galeriler");
                                Response.Redirect(Settings.PanelPath + "?go=galeri&raid=" + m.ID, false);
                            }
                            else
                                GetDataPaging(m.ID);
                        }
                    }
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
                using (Album m = AlbumMethods.GetAlbum(BAYMYO.UI.Converts.NullToInt64(Request.QueryString["vid"])))
                {
                    if (m.ID > 0)
                    {
                        if (BAYMYO.UI.FileIO.RemoveDirectory(Server.MapPath(Settings.ImagesPath + "album/" + m.ID + "/")))
                        {
                            Core.RemoveForeignKey("galeri", m.ID.ToString());
                            GaleriMethods.Delete(m.ID);
                            if (AlbumMethods.Delete(m) > 0)
                            {
                                Core.CreateContents("galeri");
                                CustomizeControl1.MessageText = MessageBox.Show(ProccesType.Delete, DialogResult.Succes);
                                Core.ClearControls(controls);
                                GetDataPaging(m.ID);
                            }
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
            if (!this.Page.IsPostBack)
            {
                Core.GetProccesList("galeri", ddlIslemler);
                GetDataPaging(Request.QueryString["raid"]);
            }
        }

        private void GetDataPaging(object id)
        {
            using (BAYMYO.UI.Web.DataPagers data = new BAYMYO.UI.Web.DataPagers(dataGrid1, "galeri", "kayittarihi asc", "albumid=?albumid", 5))
            {
                data.Parameters.Add("albumid", BAYMYO.UI.Converts.NullToInt64(id));
                data.PageNumberTargetControl = pageNumberLiteral;
                data.Binding();
                jopSet.Visible = (dataGrid1.Rows.Count > 0);
            }
        }

        protected void btnSaveChanges_Click(object sender, EventArgs e)
        {
            try
            {
                Int64 albumID = BAYMYO.UI.Converts.NullToInt64(Request.QueryString["raid"]);
                if (ddlIslemler.SelectedIndex > 0 & albumID > 0)
                {
                    if (ddlIslemler.SelectedIndex == 1)
                    {
                        foreach (GridViewRow item in dataGrid1.Rows)
                            if (((CheckBox)item.Cells[0].FindControl("chkSelected")).Checked)
                            {
                                GaleriMethods.Update(albumID);
                                Core.Update("galeri", "kapak", BAYMYO.UI.Converts.NullToGuidString(dataGrid1.DataKeys[item.RowIndex][0]), true);
                                break;
                            }
                    }
                    else if (ddlIslemler.SelectedIndex == 2)
                    {
                        foreach (GridViewRow item in dataGrid1.Rows)
                            if (((CheckBox)item.Cells[0].FindControl("chkSelected")).Checked)
                                if (BAYMYO.UI.FileIO.Remove(Server.MapPath(Settings.ImagesPath + "album/" + albumID + "/" + dataGrid1.DataKeys[item.RowIndex][1].ToString())))
                                    GaleriMethods.Delete(BAYMYO.UI.Converts.NullToGuidString(dataGrid1.DataKeys[item.RowIndex][0]));
                    }
                    Core.CreateContents("galeri");
                    GetDataPaging(albumID);
                }
            }
            catch (Exception ex)
            {
                CustomizeControl1.MessageText = MessageBox.Show(DialogResult.Error, ex.Message);
            }
        }
    }
}