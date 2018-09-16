using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace baymyoStatic.panel.ascx
{
    public partial class haber : System.Web.UI.UserControl
    {
        protected override void OnInit(EventArgs e)
        {
            try
            {
                CustomizeControl1.FormTitle = string.Format(Settings.FormTitleFormat, "Haber", "Ekleme/Düzeltme Formu");
                if (Request.QueryString["hid"] != null)
                    ViewState["tempID"] = Request.QueryString["hid"];
                using (Haber m = HaberMethods.GetHaber(BAYMYO.UI.Converts.NullToInt64(ViewState["tempID"])))
                {
                    bool notNull = (m.ID > 0), isAdmin = Core.IsUserAdmin;
                    if (notNull) Default(m, isAdmin);

                    TextBox txt = new TextBox();
                    txt.ID = "Baslik";
                    txt.CssClass = "form-control";
                    txt.Text = m.Baslik;
                    txt.MaxLength = 75;
                    CustomizeControl1.AddControl("Başlık", txt);

                    txt = new TextBox();
                    txt.ID = "Ozet";
                    txt.CssClass = "form-control";
                    txt.Text = m.Ozet;
                    txt.TextMode = TextBoxMode.MultiLine;
                    txt.MaxLength = 250;
                    CustomizeControl1.AddControl("Özet", txt, "Liste ve RSS'ler için gösterilecek içeriktir.");

                    CKEditor.NET.CKEditorControl fck = new CKEditor.NET.CKEditorControl();
                    fck.ID = "Icerik";
                    fck.Height = 400;
                    fck.Text = m.Icerik;
                    CustomizeControl1.AddControl("Editör", fck);

                    txt = new TextBox();
                    txt.ID = "Etiket";
                    txt.CssClass = "form-control";
                    txt.Text = m.Etiket;
                    txt.MaxLength = 100;
                    CustomizeControl1.AddControl("Etiket", txt, string.Format("Lütfen virgül({0}) ile ayrıarak ve boşluk bırakmadan yazınız! Örnek: elma{0}meyve{0}sebze{0}bahçe", Settings.SplitFormat));

                    DropDownList ddl = null;
                    //    = new DropDownList();
                    //ddl.ID = "Galeri";
                    //ddl.Width = 746;
                    //ddl.CssClass = "form-control";
                    //ddl.DataMember = "album";
                    //ddl.DataValueField = "id";
                    //ddl.DataTextField = "adi";
                    //AlbumCollection albumler = AlbumMethods.GetSelect(20);
                    //albumler.Insert(0, new Album { ID = 0, Adi = "<Seçiniz>" });
                    //ddl.DataSource = albumler;
                    //ddl.DataBind();
                    //ddl.SelectedValue = BAYMYO.UI.Converts.NullToString(m.Galeri);
                    //CustomizeControl1.AddControl("İlgili Galeri", ddl, m.Galeri > 0 ? string.Format("İlgili <b>Galeri</b>'ye resim yüklemek yada düzeltmek için <a href=\"{0}\" target=\"_blank\">buraya tıklayın.</a>", Settings.PanelPath + "?go=galeri&raid=" + m.Galeri) : "");

                    Image img = new Image();
                    img.ID = "BuyukResim";
                    img.Width = 250;
                    img.ToolTip = m.ResimUrl;
                    if (!string.IsNullOrEmpty(m.ResimUrl))
                        img.ImageUrl = Settings.ImagesPath + "haber/b/" + m.ResimUrl;
                    else
                        img.ImageUrl = Settings.ImagesPath + "admin-yok.png";
                    CustomizeControl1.AddControl("Büyük Resim", img);

                    FileUpload flu = new FileUpload();
                    flu.ID = "ResimUrl";
                    flu.ToolTip = m.ResimUrl;
                    CustomizeControl1.AddControl("Resim Ekle", flu, "Tavsiye edilen, Genişlik(W):728px - Yükseklik(H):300px");

                    img = new Image();
                    img.ID = "KucukResim";
                    img.Width = 150;
                    if (!string.IsNullOrEmpty(m.ResimUrl))
                        img.ImageUrl = Settings.ImagesPath + "haber/" + m.ResimUrl;
                    else
                        img.ImageUrl = Settings.ImagesPath + "admin-yok.png";
                    CustomizeControl1.AddControl("Küçük Resim", img);

                    flu = new FileUpload();
                    flu.ID = "KucukResimUrl";
                    CustomizeControl1.AddControl("Küçük Resim Ekle", flu, "<b>Bu alanda resim seçmezseniz büyük resim küçültülecektir.</b> Genişlik(W):350px/Yükseklik(H):140px");

                    ddl = new DropDownList();
                    ddl.ID = "Kategori";
                    ddl.Width = 300;
                    ddl.CssClass = "form-control";
                    ddl.DataMember = "kategori";
                    ddl.DataValueField = "id";
                    ddl.DataTextField = "adi";
                    List<Kategori> kategoriler = KategoriMethods.GetMenu("haber", true);
                    ListItem item = null;
                    foreach (Kategori kategori in kategoriler)
                    {
                        switch (kategori.ParentID)
                        {
                            case "":
                                item = new ListItem(kategori.Adi, kategori.ID);
                                item.Attributes.CssStyle.Value = "padding-left: 5px; background: #f5f5f5; color: #454545;";
                                break;
                            case "0":
                                item = new ListItem(kategori.Adi, kategori.ID);
                                item.Attributes.CssStyle.Value = "padding-left: 25px; background: #f5f5f5; color: #fe760c; font-weight: bold;";
                                break;
                            default:
                                item = new ListItem(kategori.Adi, kategori.ID);
                                item.Attributes.CssStyle.Value = string.Format("padding-left: {0}px; background: #f5f5f5; color: #454545;", (BAYMYO.UI.Converts.NullToInt(kategori.ParentID.Split(',').Length + 1) * 25));
                                break;
                        }
                        ddl.Items.Add(item);
                    }
                    kategoriler.Clear();
                    ddl.SelectedValue = BAYMYO.UI.Converts.NullToString(m.KategoriID);
                    CustomizeControl1.AddControl("Kategori", ddl, "<a href=\"" + Settings.PanelPath + "?go=kategori&mdl=haber\">[+] Yeni Kategori</a>");

                    //ddl = new DropDownList();
                    //ddl.ID = "Sehir";
                    //ddl.Width = 300;
                    //ddl.CssClass = "form-control";
                    //ddl.DataMember = "Sehir";
                    //ddl.DataValueField = "Adi";
                    //ddl.DataTextField = "Adi";
                    //SehirCollection sehirler = SehirMethods.GetSelect();
                    //sehirler.Insert(0, new Sehir(0, ""));
                    //ddl.DataSource = sehirler;
                    //ddl.DataBind();
                    //ddl.SelectedValue = BAYMYO.UI.Converts.NullToString(m.Sehir);
                    //CustomizeControl1.AddControl("Şehir (İL)", ddl);

                    //ddl = new DropDownList();
                    //ddl.ID = "Anasayfa";
                    //ddl.Width = 300;
                    //ddl.CssClass = "form-control";
                    //ddl.DataMember = "MainViewStates";
                    //ddl.DataValueField = "Key";
                    //ddl.DataTextField = "Value";
                    //ddl.DataSource = Core.GetMainViewStates();
                    //ddl.DataBind();
                    //ddl.SelectedValue = notNull ? m.Anasayfa.ToString() : "1";
                    //CustomizeControl1.AddControl("Anasayfa", ddl);

                    //DateTimeControl cnt = this.Page.LoadControl(Settings.DateTimeControlPath) as DateTimeControl;
                    //cnt.ID = "Tarih";
                    //cnt.FormatType = FormatTypes.DateTime;
                    //CustomizeControl1.AddControl("Yayın Tarihi", cnt, "* Seçilmesi zorunlu alan. Belirtilen tarihte haber yayımlanacaktır.");
                    //cnt.Date = (m.ID > 1) ? m.KayitTarihi : DateTime.Now;

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

                    //CustomizeControl1.AddTitle("HABER İÇERİSİNE VİDEO EKLE");

                    //ddl = new DropDownList();
                    //ddl.ID = "Video";
                    //ddl.Width = 746;
                    //ddl.CssClass = "form-control";
                    //ddl.DataMember = "video";
                    //ddl.DataValueField = "id";
                    //ddl.DataTextField = "baslik";
                    //VideoCollection videolar = VideoMethods.GetSelect(m.Video, 20);
                    //Video v = VideoMethods.GetVideo(m.Video);
                    //if (v.ID > 0)
                    //    videolar.Add(v);
                    //videolar.Insert(0, new Video { ID = 0, Baslik = "<Seçiniz>" });
                    //ddl.DataSource = videolar;
                    //ddl.DataBind();
                    //ddl.SelectedValue = BAYMYO.UI.Converts.NullToString(m.Video);
                    //CustomizeControl1.AddControl("İlgili Video", ddl, m.Video > 0 ? string.Format("İlgili <b>Video</b>'yu güncellemek için <a href=\"{0}\" target=\"_blank\">buraya tıklayın.</a>", Settings.PanelPath + "?go=video&vid=" + m.Video) : "Eğer listeden video seçilirse aşağıdaki embed kod ve kategori dikkate alınmayacaktır.");

                    //txt = new TextBox();
                    //txt.ID = "HaberEmbed";
                    //txt.Text = v.Embed;
                    //txt.CssClass = "form-control";
                    //txt.TextMode = TextBoxMode.MultiLine;
                    //txt.MaxLength = 750;
                    //txt.ClientIDMode = ClientIDMode.Static;
                    //CustomizeControl1.AddControl("Video Embed", txt, "Her hangi bir video sitesinden 'embed' kodu almanız gereklidir.");

                    //ddl = new DropDownList();
                    //ddl.ID = "KategoriVideo";
                    //ddl.Width = 250;
                    //ddl.CssClass = "form-control";
                    //ddl.DataMember = "kategori";
                    //ddl.DataValueField = "id";
                    //ddl.DataTextField = "adi";
                    //ddl.DataSource = KategoriMethods.GetMenu("video", true);
                    //ddl.DataBind();
                    //ddl.SelectedValue = BAYMYO.UI.Converts.NullToString(v.KategoriID);
                    //CustomizeControl1.AddControl("Video Kategorileri", ddl, "Sadece <b>Embed</b> kod girildiğinde kategori seçilmezse video yüklenmez.");

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

        void Default(Haber m, bool isAdmin)
        {
            if (Core.CurrentUser.Tipi == AccountType.Editor & m.HesapID != Core.CurrentUser.ID)
                Response.Redirect(Settings.VirtualPath + "panel", false);
            ViewState["tempID"] = m.ID;
            CustomizeControl1.RemoveVisible = isAdmin;
            CustomizeControl1.StatusText = string.Format(Settings.ShortcutFormat, Core.CreateLink("haber", m.ID, m.Baslik), "haber", m.ID);
        }

        void CustomizeControl1_SubmitClick(SortedDictionary<string, Control> controls)
        {
            string tempBaslik = null, tempPath = null, tempEmbed = null;
            try
            {
                if (!string.IsNullOrEmpty(((TextBox)controls["Baslik"]).Text)
                    & !string.IsNullOrEmpty(((TextBox)controls["Ozet"]).Text)
                    & !string.IsNullOrEmpty(((CKEditor.NET.CKEditorControl)controls["Icerik"]).Text)
                    & ((DropDownList)controls["Kategori"]).SelectedIndex > 0)
                    using (Haber m = HaberMethods.GetHaber(BAYMYO.UI.Converts.NullToInt64(ViewState["tempID"])))
                    {
                        bool isAdmin = Core.IsUserAdmin;
                        m.Baslik = BAYMYO.UI.Commons.SubStringText(((TextBox)controls["Baslik"]).Text, 75);
                        m.Ozet = BAYMYO.UI.Commons.SubStringText(((TextBox)controls["Ozet"]).Text, 250);
                        m.Icerik = ((CKEditor.NET.CKEditorControl)controls["Icerik"]).Text;
                        m.Etiket = BAYMYO.UI.Commons.SubStringText(((TextBox)controls["Etiket"]).Text, 100);
                        //m.Sehir = BAYMYO.UI.Converts.NullToString(((DropDownList)controls["Sehir"]).SelectedValue);
                        m.KategoriID = BAYMYO.UI.Converts.NullToString(((DropDownList)controls["Kategori"]).SelectedValue);
                        //m.KayitTarihi = ((DateTimeControl)controls["Tarih"]).Date;
                        m.GuncellemeTarihi = DateTime.Now;
                        //m.Galeri = BAYMYO.UI.Converts.NullToInt64(((DropDownList)controls["Galeri"]).SelectedValue);
                        m.Anasayfa = true;
                        //m.Anasayfa = BAYMYO.UI.Converts.NullToBool(((DropDownList)controls["Anasayfa"]).SelectedValue);
                        m.GosterimSayi = ((CheckBoxList)controls["chkList"]).Items[0].Selected;
                        m.Uye = ((CheckBoxList)controls["chkList"]).Items[1].Selected;
                        m.Yorum = ((CheckBoxList)controls["chkList"]).Items[2].Selected;

                        if (isAdmin)
                        {
                            m.YoneticiOnay = ((CheckBoxList)controls["chkList"]).Items[3].Selected;
                            m.Aktif = ((CheckBoxList)controls["chkList"]).Items[4].Selected;
                        }
                        else
                        {
                            m.YoneticiOnay = false;
                            m.Aktif = false;
                        }

                        #region --- VIDEO YÜKLEME ---
                        //Int64 tempVideoID = BAYMYO.UI.Converts.NullToInt64(((DropDownList)controls["Video"]).SelectedValue);
                        //tempBaslik = Core.ReplaceToLover(m.Baslik);
                        //tempEmbed = ((TextBox)controls["HaberEmbed"]).Text;
                        //using (Video v = VideoMethods.GetVideo(m.Video))
                        //{
                        //    if (v.ID <= 0
                        //        & tempVideoID <= 0
                        //        & !string.IsNullOrEmpty(tempEmbed)
                        //        & ((DropDownList)controls["KategoriVideo"]).SelectedIndex > 0)
                        //    {
                        //        if (!string.IsNullOrWhiteSpace(youtubeImage.Value))
                        //            v.ResimUrl = BAYMYO.UI.FileIO.DownloadImage(youtubeImage.Value, Server.MapPath(Settings.ImagesPath + "video/"), m.Baslik);
                        //        else if ((controls["ResimUrl"] as FileUpload).HasFile)
                        //            v.ResimUrl = BAYMYO.UI.FileIO.UploadImage(controls["ResimUrl"] as FileUpload, tempBaslik,
                        //                Server.MapPath(Settings.ImagesPath + "video/"), 360, true, false);
                        //        else if (!(controls["ResimUrl"] as FileUpload).HasFile & !string.IsNullOrEmpty(m.ResimUrl))
                        //        {
                        //            v.ResimUrl = m.ResimUrl;
                        //            BAYMYO.UI.FileIO.ResizeImage(Server.MapPath(Settings.ImagesPath + "haber/" + m.ResimUrl),
                        //                Server.MapPath(Settings.ImagesPath + "video/" + m.ResimUrl), 360, System.IO.Path.GetExtension(m.ResimUrl).ToLower());
                        //        }

                        //        v.HesapID = Core.CurrentUser.ID;
                        //        v.Baslik = m.Baslik;
                        //        v.KategoriID = BAYMYO.UI.Converts.NullToString(((DropDownList)controls["KategoriVideo"]).SelectedValue);
                        //        v.Embed = tempEmbed;
                        //        v.Etiket = m.Etiket;
                        //        v.GosterimSayi = m.GosterimSayi;
                        //        v.KayitTarihi = m.KayitTarihi;
                        //        v.GuncellemeTarihi = m.GuncellemeTarihi;
                        //        v.Uye = m.Uye;
                        //        v.Yorum = m.Yorum;
                        //        v.YoneticiOnay = m.YoneticiOnay;
                        //        v.Aktif = m.Aktif;
                        //        m.Video = VideoMethods.Insert(v);
                        //        if (m.Video > 0)
                        //        {
                        //            Core.CreateContents("video");
                        //            jSonData.CreateData("videolar");
                        //        }
                        //    }
                        //    else if (
                        //        v.ID > 0
                        //        & v.ID.Equals(tempVideoID)
                        //        & !string.IsNullOrEmpty(tempEmbed)
                        //        & !BAYMYO.UI.Converts.NullToString(v.Embed).Equals(tempEmbed))
                        //    {
                        //        if (!string.IsNullOrWhiteSpace(youtubeImage.Value))
                        //            v.ResimUrl = BAYMYO.UI.FileIO.DownloadImage(youtubeImage.Value, Server.MapPath(Settings.ImagesPath + "video/"), m.Baslik);
                        //        else if ((controls["ResimUrl"] as FileUpload).HasFile)
                        //        {
                        //            v.ResimUrl = m.ResimUrl;
                        //            if (BAYMYO.UI.FileIO.Remove(Server.MapPath(Settings.ImagesPath + "video/" + m.ResimUrl)))
                        //                v.ResimUrl = BAYMYO.UI.FileIO.UploadImage(controls["ResimUrl"] as FileUpload, tempBaslik,
                        //                    Server.MapPath(Settings.ImagesPath + "video/"), 360, true, false);
                        //        }
                        //        v.Embed = tempEmbed;
                        //        VideoMethods.Update(v);
                        //    }
                        //    else
                        //        m.Video = tempVideoID;
                        //}
                        #endregion

                        if (m.ID > 0)
                        {
                            //m.HesapID = Core.CurrentUser.ID;
                            tempPath = Settings.ImagesPath + "haber/";
                            tempBaslik = Core.ReplaceToLover(m.Baslik);
                            if ((controls["ResimUrl"] as FileUpload).HasFile)
                            {
                                bool sil = BAYMYO.UI.FileIO.Remove(Server.MapPath(tempPath + m.ResimUrl));
                                sil = BAYMYO.UI.FileIO.Remove(Server.MapPath(tempPath + "b/" + m.ResimUrl));
                                if (sil)
                                {
                                    m.ResimUrl = BAYMYO.UI.FileIO.UploadImage(controls["ResimUrl"] as FileUpload, tempBaslik, Server.MapPath(tempPath + "b/"), 728, true);
                                    if ((controls["KucukResimUrl"] as FileUpload).HasFile)
                                        BAYMYO.UI.FileIO.UploadImage(controls["KucukResimUrl"] as FileUpload, m.ResimUrl, Server.MapPath(tempPath), 360, true, false);
                                    else
                                        BAYMYO.UI.FileIO.ResizeImage(Server.MapPath(tempPath + "b/" + m.ResimUrl), Server.MapPath(tempPath + m.ResimUrl), 230,
                                            System.IO.Path.GetExtension(m.ResimUrl).ToLower());
                                }
                            }
                            else if ((controls["KucukResimUrl"] as FileUpload).HasFile)
                            {
                                if (string.IsNullOrEmpty(m.ResimUrl))
                                    m.ResimUrl = BAYMYO.UI.FileIO.UploadImage(controls["KucukResimUrl"] as FileUpload, tempBaslik, Server.MapPath(tempPath + ""), 360, true);
                                if (BAYMYO.UI.FileIO.Remove(Server.MapPath(tempPath + m.ResimUrl)))
                                    BAYMYO.UI.FileIO.UploadImage(controls["KucukResimUrl"] as FileUpload, m.ResimUrl, Server.MapPath(tempPath), 360, true, false);
                            }

                            if (HaberMethods.Update(m) > 0)
                            {
                                if (!string.IsNullOrEmpty(m.ResimUrl))
                                {
                                    ((Image)controls["BuyukResim"]).ImageUrl = Settings.ImagesPath + "haber/b/" + m.ResimUrl;
                                    ((Image)controls["KucukResim"]).ImageUrl = Settings.ImagesPath + "haber/" + m.ResimUrl;
                                }
                                CustomizeControl1.MessageText = MessageBox.Show(ProccesType.Update, DialogResult.Succes);
                            }
                        }
                        else
                        {
                            m.HesapID = Core.CurrentUser.ID;
                            m.KayitTarihi = DateTime.Now;
                            //Resim Yukleme
                            tempBaslik = Core.ReplaceToLover(m.Baslik);
                            tempPath = Settings.ImagesPath + "haber/";
                            m.ResimUrl = BAYMYO.UI.FileIO.UploadImage(controls["ResimUrl"] as FileUpload, tempBaslik, Server.MapPath(tempPath + "b/"), 728, true);

                            if ((controls["KucukResimUrl"] as FileUpload).HasFile)
                                BAYMYO.UI.FileIO.UploadImage(controls["KucukResimUrl"] as FileUpload, m.ResimUrl, Server.MapPath(tempPath), 356, true, false);
                            else if (!string.IsNullOrEmpty(m.ResimUrl))
                                BAYMYO.UI.FileIO.ResizeImage(Server.MapPath(tempPath + "b/" + m.ResimUrl), Server.MapPath(tempPath + m.ResimUrl), 230,
                                    System.IO.Path.GetExtension((controls["ResimUrl"] as FileUpload).FileName).ToLower());

                            m.ID = HaberMethods.Insert(m);
                            if (m.ID > 0)
                            {
                                if (!string.IsNullOrEmpty(m.ResimUrl))
                                {
                                    ((Image)controls["BuyukResim"]).ImageUrl = Settings.ImagesPath + "haber/b/" + m.ResimUrl;
                                    ((Image)controls["KucukResim"]).ImageUrl = Settings.ImagesPath + "haber/" + m.ResimUrl;
                                }
                                Default(m, isAdmin);
                                CustomizeControl1.MessageText = MessageBox.Show(ProccesType.Insert, DialogResult.Succes);
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
            finally
            {
                jSonData.CreateData("haberler");
                Core.CreateContents("haber");
                tempBaslik = tempPath = tempEmbed = null;
            }
        }
        void CustomizeControl1_RemoveClick(SortedDictionary<string, Control> controls)
        {
            try
            {
                using (Haber m = HaberMethods.GetHaber(BAYMYO.UI.Converts.NullToInt64(ViewState["tempID"])))
                {
                    if (m.ID > 0)
                    {
                        BAYMYO.UI.FileIO.Remove(Server.MapPath(Settings.ImagesPath + "haber/b/" + m.ResimUrl));
                        BAYMYO.UI.FileIO.Remove(Server.MapPath(Settings.ImagesPath + "haber/" + m.ResimUrl));
                        Core.RemoveForeignKey("haber", m.ID.ToString());
                        if (HaberMethods.Delete(m) > 0)
                        {
                            ((Image)controls["BuyukResim"]).ImageUrl = Settings.ImagesPath + "admin-yok.png";
                            ((Image)controls["KucukResim"]).ImageUrl = Settings.ImagesPath + "admin-yok.png";
                            jSonData.CreateData("haberler");
                            Core.CreateContents("haber");
                            CustomizeControl1.MessageText = MessageBox.Show(ProccesType.Delete, DialogResult.Succes);
                            Response.Redirect(Settings.PanelPath + "?go=haber", true);
                        }
                    }
                    else
                        CustomizeControl1.MessageText = MessageBox.Show(ProccesType.Delete, DialogResult.Warning);
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