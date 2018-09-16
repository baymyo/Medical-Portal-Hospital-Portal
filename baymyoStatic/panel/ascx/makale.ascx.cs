using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace baymyoStatic.panel.ascx
{
    public partial class makale : System.Web.UI.UserControl
    {
        protected override void OnInit(EventArgs e)
        {
            try
            {
                CustomizeControl1.FormTitle = string.Format(Settings.FormTitleFormat, "Makale", "Ekleme/Düzeltme Formu");
                if (Request.QueryString["mklid"] != null)
                    ViewState["tempID"] = Request.QueryString["mklid"];
                using (Makale m = MakaleMethods.GetMakale(BAYMYO.UI.Converts.NullToInt64(ViewState["tempID"])))
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
                    txt.MaxLength = 150;
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

                    //DateTimeControl cnt = this.Page.LoadControl(Settings.DateTimeControlPath) as DateTimeControl;
                    //cnt.ID = "Tarih";
                    //cnt.FormatType = FormatTypes.DateTime;
                    //CustomizeControl1.AddControl("Tarih", cnt, "* Seçilmesi zorunlu alan.");
                    //cnt.Date = m.KayitTarihi;

                    DropDownList ddl = new DropDownList();
                    ddl.ID = "Yazar";
                    ddl.Width = 250;
                    ddl.CssClass = "form-control";
                    ddl.DataMember = "hesap";
                    ddl.DataValueField = "id";
                    ddl.DataTextField = "adi";
                    using (BAYMYO.UI.Web.CustomSqlQuery data = new BAYMYO.UI.Web.CustomSqlQuery(ddl, "select id, concat_ws(' ',adi,soyadi) as adi from hesap where tipi in(1,2,5)"))
                    {
                        data.Execute();
                    }
                    ddl.Items.Insert(0, new ListItem("<Seçiniz>", ""));
                    ddl.SelectedValue = BAYMYO.UI.Converts.NullToString(m.HesapID);
                    CustomizeControl1.AddControl("Yazarlar", ddl, "<a href=\"" + Settings.PanelPath + "?go=hesap&type=2\">[+] Yeni Yazar</a> (Not: Buraya sadece <b>Admin</b>, <b>Moderatör</b> ve <b>Private</b> olan yazarlar getirilir.)");

                    ddl = new DropDownList();
                    ddl.ID = "Kategori";
                    ddl.Width = 250;
                    ddl.CssClass = "form-control";
                    ddl.DataMember = "kategori";
                    ddl.DataValueField = "id";
                    ddl.DataTextField = "adi";
                    List<Kategori> kategoriler = KategoriMethods.GetMenu("makale", true);
                    ListItem item = null;
                    foreach (Kategori kategori in kategoriler)
                    {
                        switch (kategori.ParentID)
                        {
                            case "":
                                item = new ListItem(kategori.Adi, kategori.ID);
                                item.Attributes.CssStyle.Value = "padding-left: 5px;background: #f5f5f5; color: #454545;";
                                break;
                            case "0":
                                item = new ListItem(kategori.Adi, kategori.ID);
                                item.Attributes.CssStyle.Value = "padding-left: 25px;background: #f5f5f5; color: #fe760c; font-weight: bold;";
                                break;
                            default:
                                item = new ListItem(kategori.Adi, kategori.ID);
                                item.Attributes.CssStyle.Value = string.Format("padding-left: {0}px;background: #f5f5f5; color: #454545;", (BAYMYO.UI.Converts.NullToInt(kategori.ParentID.Split(',').Length + 1) * 25));
                                break;
                        }
                        ddl.Items.Add(item);
                    }
                    kategoriler.Clear();
                    ddl.SelectedValue = BAYMYO.UI.Converts.NullToString(m.KategoriID);
                    CustomizeControl1.AddControl("Kategori", ddl, "<a href=\"" + Settings.PanelPath + "?go=kategori&mdl=makale\">[+] Yeni Kategori</a>");

                    Image img = new Image();
                    img.ID = "BuyukResim";
                    img.ToolTip = m.ResimUrl;
                    if (!string.IsNullOrEmpty(m.ResimUrl))
                        img.ImageUrl = Settings.ImagesPath + "makale/" + m.ResimUrl;
                    else
                        img.ImageUrl = Settings.ImagesPath + "admin-yok.png";
                    CustomizeControl1.AddControl("Makale Resim", img);

                    FileUpload flu = new FileUpload();
                    flu.ID = "ResimUrl";
                    flu.ToolTip = m.ResimUrl;
                    flu.CssClass = "form-control";
                    CustomizeControl1.AddControl("Resim Ekle", flu, "Genişlik(W):728px - Yükseklik(H):300px");

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

        void Default(Makale m, bool isAdmin)
        {
            if (Core.CurrentUser.Tipi == AccountType.Editor & m.HesapID != Core.CurrentUser.ID)
                Response.Redirect(Settings.VirtualPath + "panel", false);
            ViewState["tempID"] = m.ID;
            CustomizeControl1.RemoveVisible = isAdmin;
            CustomizeControl1.StatusText = string.Format(Settings.ShortcutFormat, Core.CreateLink("makale", m.ID, m.Baslik), "makale", m.ID);
        }

        void CustomizeControl1_SubmitClick(SortedDictionary<string, Control> controls)
        {
            try
            {
                if (!string.IsNullOrEmpty(((TextBox)controls["Baslik"]).Text)
                    & !string.IsNullOrEmpty(((TextBox)controls["Ozet"]).Text)
                    & !string.IsNullOrEmpty(((CKEditor.NET.CKEditorControl)controls["Icerik"]).Text)
                    & ((DropDownList)controls["Kategori"]).SelectedIndex > 0)
                    using (Makale m = MakaleMethods.GetMakale(BAYMYO.UI.Converts.NullToInt64(ViewState["tempID"])))
                    {
                        m.Baslik = BAYMYO.UI.Commons.SubStringText(((TextBox)controls["Baslik"]).Text, 75);
                        m.Ozet = BAYMYO.UI.Commons.SubStringText(((TextBox)controls["Ozet"]).Text, 150);
                        m.Icerik = ((CKEditor.NET.CKEditorControl)controls["Icerik"]).Text;
                        m.Etiket = BAYMYO.UI.Commons.SubStringText(((TextBox)controls["Etiket"]).Text, 100);
                        m.HesapID = BAYMYO.UI.Converts.NullToString(((DropDownList)controls["Yazar"]).SelectedValue);
                        m.KategoriID = BAYMYO.UI.Converts.NullToString(((DropDownList)controls["Kategori"]).SelectedValue);
                        //m.KayitTarihi = ((DateTimeControl)controls["Tarih"]).Date;
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
                                if (BAYMYO.UI.FileIO.Remove(Server.MapPath(Settings.ImagesPath + "makale/" + m.ResimUrl)))
                                    m.ResimUrl = BAYMYO.UI.FileIO.UploadImage(controls["ResimUrl"] as FileUpload, m.Baslik, Server.MapPath(Settings.ImagesPath + "makale/"), 728, true);
                            if (MakaleMethods.Update(m) > 0)
                            {
                                if (!string.IsNullOrEmpty(m.ResimUrl))
                                    ((Image)controls["BuyukResim"]).ImageUrl = Settings.ImagesPath + "makale/" + m.ResimUrl;
                                Core.CreateContents("makale");
                                jSonData.CreateData("makaleler");
                                CustomizeControl1.MessageText = MessageBox.Show(ProccesType.Update, DialogResult.Succes);
                            }
                        }
                        else
                        {
                            m.ResimUrl = BAYMYO.UI.FileIO.UploadImage(controls["ResimUrl"] as FileUpload, m.Baslik, Server.MapPath(Settings.ImagesPath + "makale/"), 728, true);
                            m.KayitTarihi = m.GuncellemeTarihi;
                            m.ID = MakaleMethods.Insert(m);
                            if (m.ID > 0)
                            {
                                if (!string.IsNullOrEmpty(m.ResimUrl))
                                    ((Image)controls["BuyukResim"]).ImageUrl = Settings.ImagesPath + "makale/" + m.ResimUrl;
                                Default(m, Core.IsUserAdmin);
                                Core.CreateContents("makale");
                                jSonData.CreateData("makaleler");
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
        }
        void CustomizeControl1_RemoveClick(SortedDictionary<string, Control> controls)
        {
            try
            {
                using (Makale m = MakaleMethods.GetMakale(BAYMYO.UI.Converts.NullToInt64(ViewState["tempID"])))
                {
                    if (m.ID > 0)
                    {
                        BAYMYO.UI.FileIO.Remove(Server.MapPath(Settings.ImagesPath + "makale/" + m.ResimUrl));
                        Core.RemoveForeignKey("makale", m.ID.ToString());
                        if (MakaleMethods.Delete(m) > 0)
                        {
                            Core.CreateContents("makale");
                            jSonData.CreateData("makaleler");
                            CustomizeControl1.MessageText = MessageBox.Show(ProccesType.Delete, DialogResult.Succes);
                            Response.Redirect(Settings.PanelPath + "?go=makale", false);
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