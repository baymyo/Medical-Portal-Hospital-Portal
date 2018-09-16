using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace baymyoStatic.panel.ascx
{
    public partial class kategori : System.Web.UI.UserControl
    {
        protected override void OnInit(EventArgs e)
        {
            try
            {
                CustomizeControl1.FormTitle = string.Format(Settings.FormTitleFormat, "Kategori", "Ekleme/Düzeltme Formu");
                CustomizeControl1.MessageText = MessageBox.Show(DialogResult.Info, string.Format("<b>'{0}'</b> modülü için kategori tanımlamaktasınız! Yeni ana kategori ekleyebilmek için <b>'Yeni Kategori'</b> sekmesine yada güncellemek istiyorsanız kategori adına tıklayınız ve kutucuğa kategori adını yazınız kaydet butonuna tıklayınız. <b>'{0}'</b> modülü için alt kategori {1}", Request.QueryString["mdl"].ToUpper(), Core.IsParentCategory(Request.QueryString["mdl"]) ? "<b>ekleyebilirsiniz!</b><br/>* Alt Kategori eklemek istediğiniz ana kategoriyi seçiniz ve kutucuğa gerekli kategori adını giriniz ve kaydet butonuna tıklayınız." : "<b><u>tanımlayamazsınız!</u></b>"));
                CustomizeControl1.UpdateVisible = true;
                CustomizeControl1.RemoveVisible = !BAYMYO.UI.Converts.NullToString(Request.QueryString["kid"]).Equals("0");

                TreeView trv = new TreeView();
                trv.ID = "Kategoriler";
                trv.Width = 300;
                trv.ExpandDepth = 1;
                trv.ShowLines = true;
                trv.DataSourceID = "hierarDataSource";
                trv.SelectedNodeChanged += trv_SelectedNodeChanged;

                trv.NodeStyle.HorizontalPadding = Unit.Pixel(5);
                trv.NodeStyle.VerticalPadding = Unit.Pixel(5);

                trv.RootNodeStyle.BackColor = System.Drawing.Color.WhiteSmoke;
                trv.RootNodeStyle.BorderColor = System.Drawing.Color.Gray;
                trv.RootNodeStyle.ForeColor = System.Drawing.Color.OrangeRed;
                trv.RootNodeStyle.HorizontalPadding = Unit.Pixel(5);
                trv.RootNodeStyle.VerticalPadding = Unit.Pixel(5);

                trv.SelectedNodeStyle.BackColor = System.Drawing.Color.Orange;
                trv.SelectedNodeStyle.BorderColor = System.Drawing.Color.OrangeRed;
                trv.SelectedNodeStyle.ForeColor = System.Drawing.Color.White;
                trv.SelectedNodeStyle.HorizontalPadding = Unit.Pixel(5);
                trv.SelectedNodeStyle.VerticalPadding = Unit.Pixel(5);

                trv.HoverNodeStyle.BackColor = System.Drawing.Color.Wheat;
                trv.SelectedNodeStyle.BorderColor = System.Drawing.Color.OrangeRed;
                trv.HoverNodeStyle.ForeColor = System.Drawing.Color.OrangeRed;

                CustomizeControl1.AddControl("Kategoriler", trv);

                TextBox txt = new TextBox();
                txt.ID = "Adi";
                txt.CssClass = "form-control";
                txt.MaxLength = 35;
                CustomizeControl1.AddControl("Adı", txt, "Not: Sadece <b>Güncelleme</b> işlemi yaparken bu alanı boş bıraktığınızda kategori adı değişmeyecektir!");

                txt = new TextBox();
                txt.ID = "Aciklama";
                txt.CssClass = "form-control";
                txt.TextMode = TextBoxMode.MultiLine;
                txt.MaxLength = 150;
                CustomizeControl1.AddControl("Açıklama", txt, "Description alanına SEO için eklenecektir. 150 karakter giriniz.");

                txt = new TextBox();
                txt.ID = "Etiket";
                txt.CssClass = "form-control";
                txt.MaxLength = 100;
                CustomizeControl1.AddControl("Etiket", txt, string.Format("Lütfen virgül({0}) ile ayrıarak ve boşluk bırakmadan yazınız! Örnek: elma{0}meyve{0}sebze{0}bahçe", Settings.SplitFormat));

                //txt = new TextBox();
                //txt.ID = "Renk";
                //txt.CssClass = "form-control";
                //txt.MaxLength = 7;
                //CustomizeControl1.AddControl("Renk Kodu", txt, "<a href=\"http://www.google.com/design/spec/style/color.html#color-color-palette\" target=\"_blank\"><b>Google renk paleti için tıklayın.</b></a>&nbsp;&nbsp;Örnek: <b>#cf0a0a</b>");

                //DropDownList ddl = new DropDownList();
                //ddl.ID = "Menu";
                //ddl.Width = 275;
                //ddl.CssClass = "form-control";
                //ddl.DataValueField = "Key";
                //ddl.DataTextField = "Value";
                //ddl.DataSource = Core.GetCategoryMenuTypes();
                //ddl.DataBind();
                //CustomizeControl1.AddControl("Menü Durumu", ddl);

                //ddl = new DropDownList();
                //ddl.ID = "Sira";
                //ddl.Width = 275;
                //ddl.CssClass = "form-control";
                //ddl.DataValueField = "Key";
                //ddl.DataTextField = "Value";
                //ddl.DataSource = Core.GetOrderNumbers();
                //ddl.DataBind();
                //CustomizeControl1.AddControl("Sira", ddl);

                //ddl = new DropDownList();
                //ddl.ID = "Dil";
                //ddl.Width = 275;
                //ddl.CssClass = "form-control";
                //ddl.DataValueField = "Key";
                //ddl.DataTextField = "Value";
                //ddl.DataSource = Core.GetLanguages();
                //ddl.DataBind();
                //CustomizeControl1.AddControl("Dil", ddl);

                CheckBox chk = new CheckBox();
                chk.ID = "Aktif";
                CustomizeControl1.AddControl("Yayımla", chk);

                CustomizeControl1.SubmitClick += new CustomizeControl.ButtonEvent(CustomizeControl1_SubmitClick);
                CustomizeControl1.UpdateClick += new CustomizeControl.ButtonEvent(CustomizeControl1_UpdateClick);
                CustomizeControl1.RemoveClick += new CustomizeControl.ButtonEvent(CustomizeControl1_RemoveClick);

            }
            catch (Exception ex)
            {
                CustomizeControl1.MessageText = MessageBox.Show(DialogResult.Error, ex.Message);
            }
            base.OnInit(e);
        }

        void trv_SelectedNodeChanged(object sender, EventArgs e)
        {
            Kategori k = KategoriMethods.GetKategori(Request.QueryString["mdl"], (sender as TreeView).SelectedNode.DataPath);
            ((TextBox)this.CustomizeControl1.ControlList["Adi"]).Text = k.Adi;
            //((DropDownList)this.CustomizeControl1.ControlList["Menu"]).SelectedValue = k.Menu.ToString();
            //((DropDownList)this.CustomizeControl1.ControlList["Sira"]).SelectedIndex = k.Sira;
            //((TextBox)this.CustomizeControl1.ControlList["Renk"]).Text = k.Renk;
            ((TextBox)this.CustomizeControl1.ControlList["Aciklama"]).Text = k.Aciklama;
            ((TextBox)this.CustomizeControl1.ControlList["Etiket"]).Text = k.Etiket;
            ((CheckBox)this.CustomizeControl1.ControlList["Aktif"]).Checked = k.Aktif;
        }

        void CustomizeControl1_SubmitClick(SortedDictionary<string, Control> controls)
        {
            try
            {
                TreeView TW_Kategori = ((TreeView)controls["Kategoriler"]);
                if (TW_Kategori.SelectedNode == null)
                    ((TreeView)controls["Kategoriler"]).Nodes[0].Select();
                if (TW_Kategori.SelectedNode.DataPath.Split(',').Length >= 5)
                {
                    CustomizeControl1.MessageText = MessageBox.Show(DialogResult.Stop, "Alt kategori ekleme limitini doldurdurunuz, bu bölümde daha fazla alt kategori oluşturamazsınız.");
                    return;
                }
                if (!string.IsNullOrEmpty(Request.QueryString["mdl"]) & !string.IsNullOrEmpty(((TextBox)controls["Adi"]).Text))
                    using (Kategori m = new Kategori())
                    {
                        string orjinalID = null, parentID = TW_Kategori.SelectedNode.DataPath + ',';
                        int index = TW_Kategori.SelectedNode.ChildNodes.Count - 1;
                        bool parentActive = Core.IsParentCategory(Request.QueryString["mdl"]);
                        if (!parentActive)
                        {
                            parentID = "0";
                            if (TW_Kategori.SelectedNode.Parent != null)
                            {
                                if (TW_Kategori.SelectedNode.Parent.ChildNodes.Count > 0)
                                    orjinalID = (BAYMYO.UI.Converts.NullToInt(TW_Kategori.SelectedNode.Parent.ChildNodes[TW_Kategori.SelectedNode.Parent.ChildNodes.Count - 1].DataPath) + 1).ToString("000#");
                            }
                            else if (TW_Kategori.SelectedNode.ChildNodes.Count > 0)
                                orjinalID = (BAYMYO.UI.Converts.NullToInt(TW_Kategori.SelectedNode.ChildNodes[TW_Kategori.SelectedNode.ChildNodes.Count - 1].DataPath) + 1).ToString("000#");
                            else
                                orjinalID = 1.ToString("000#");
                        }
                        else if (parentID.Equals("0,"))
                        {
                            if (index >= 0)
                                orjinalID = (BAYMYO.UI.Converts.NullToInt(TW_Kategori.SelectedNode.ChildNodes[index].DataPath) + 1).ToString("000#");
                            else
                                orjinalID = 1.ToString("000#");
                        }
                        else if (TW_Kategori.SelectedNode.ChildNodes.Count > 0)
                        {
                            if (index >= 0)
                                if (TW_Kategori.SelectedNode.ChildNodes[index].DataPath.Contains(","))
                                {
                                    string[] kategoriler = new string[] { };
                                    kategoriler = TW_Kategori.SelectedNode.ChildNodes[index].DataPath.Split(',');
                                    orjinalID = parentID + (BAYMYO.UI.Converts.NullToInt(kategoriler[kategoriler.Length - 1]) + 1).ToString("000#");
                                }
                                else
                                    orjinalID = parentID + (BAYMYO.UI.Converts.NullToInt(TW_Kategori.SelectedNode.ChildNodes[index].DataPath) + 1).ToString("000#");
                        }
                        else
                        {
                            if (index >= 0)
                                orjinalID = parentID + (BAYMYO.UI.Converts.NullToInt(TW_Kategori.SelectedNode.ChildNodes[index].DataPath) + 1).ToString("000#");
                            else
                                orjinalID = parentID + 1.ToString("000#");
                        }

                        m.ID = orjinalID;
                        if (!string.IsNullOrEmpty(m.ID))
                        {
                            if (parentID.Length > 1)
                                m.ParentID = parentID.Remove(parentID.Length - 1);
                            else
                                m.ParentID = "0";
                            m.ModulID = Request.QueryString["mdl"];
                            m.Adi = BAYMYO.UI.Commons.SubStringText(((TextBox)controls["Adi"]).Text, 35);
                            m.Dil = "tr-TR";
                            m.Menu = 0;
                            m.Sira = 0;
                            //m.Menu = byte.Parse(((DropDownList)controls["Menu"]).SelectedValue);
                            //m.Sira = (byte)((DropDownList)controls["Sira"]).SelectedIndex;
                            //m.Dil = ((DropDownList)controls["Dil"]).SelectedValue;
                            //m.Renk = BAYMYO.UI.Commons.SubStringText(((TextBox)controls["Renk"]).Text, 7);
                            m.Aciklama = BAYMYO.UI.Commons.SubStringText(((TextBox)controls["Aciklama"]).Text, 150);
                            m.Etiket = BAYMYO.UI.Commons.SubStringText(((TextBox)controls["Etiket"]).Text, 100);
                            if (Core.IsUserAdmin)
                                m.Aktif = ((CheckBox)controls["Aktif"]).Checked;
                            else
                                m.Aktif = false;
                            switch (KategoriMethods.Insert(m))
                            {
                                case "ADI":
                                    CustomizeControl1.MessageText = MessageBox.Show(DialogResult.Error, "Eklemek istediğiniz isimde başka bir kategori bulunmaktadır, lütfen kategori adını kontrol ediniz tekrar deneyiniz!");
                                    break;
                                default:
                                    TW_Kategori.DataBind();
                                    Core.CreateCategoryMaps(m.ModulID);
                                    jSonData.CreateData(m.ModulID);
                                    CustomizeControl1.MessageText = MessageBox.Show(ProccesType.Insert, DialogResult.Succes);
                                    Core.ClearControls(controls);
                                    ((TextBox)controls["Adi"]).Focus();
                                    break;
                            }
                        }
                    }
            }
            catch (Exception ex)
            {
                CustomizeControl1.MessageText = MessageBox.Show(DialogResult.Error, ex.Message);
            }
        }
        void CustomizeControl1_UpdateClick(SortedDictionary<string, Control> controls)
        {
            try
            {
                if (!string.IsNullOrEmpty(Request.QueryString["mdl"]))
                    using (Kategori m = KategoriMethods.GetKategori(Request.QueryString["mdl"], BAYMYO.UI.Converts.NullToString(((TreeView)controls["Kategoriler"]).SelectedNode.DataPath)))
                    {
                        if (!string.IsNullOrEmpty(m.ID))
                        {
                            if (!string.IsNullOrEmpty(((TextBox)controls["Adi"]).Text))
                                m.Adi = BAYMYO.UI.Commons.SubStringText(((TextBox)controls["Adi"]).Text, 35);
                            //m.Menu = byte.Parse(((DropDownList)controls["Menu"]).SelectedValue);
                            //m.Sira = (byte)((DropDownList)controls["Sira"]).SelectedIndex;
                            //m.Dil = ((DropDownList)controls["Dil"]).SelectedValue;
                            if (Core.IsUserAdmin)
                                m.Aktif = ((CheckBox)controls["Aktif"]).Checked;
                            else
                                m.Aktif = false;
                            //if (!string.IsNullOrEmpty(((TextBox)controls["Renk"]).Text))
                            //    m.Renk = BAYMYO.UI.Commons.SubStringText(((TextBox)controls["Renk"]).Text, 7);
                            if (!string.IsNullOrEmpty(((TextBox)controls["Aciklama"]).Text))
                                m.Aciklama = BAYMYO.UI.Commons.SubStringText(((TextBox)controls["Aciklama"]).Text, 150);
                            if (!string.IsNullOrEmpty(((TextBox)controls["Etiket"]).Text))
                                m.Etiket = BAYMYO.UI.Commons.SubStringText(((TextBox)controls["Etiket"]).Text, 100);
                            switch (KategoriMethods.Update(m))
                            {
                                case "ADI":
                                    CustomizeControl1.MessageText = MessageBox.Show(DialogResult.Error, "Eklemek istediğiniz isimde başka bir kategori bulunmaktadır, lütfen kategori adını kontrol ediniz tekrar deneyiniz!");
                                    break;
                                default:
                                    Core.CreateCategoryMaps(m.ModulID);
                                    jSonData.CreateData(m.ModulID);
                                    ((TreeView)controls["Kategoriler"]).DataBind();
                                    CustomizeControl1.MessageText = MessageBox.Show(ProccesType.Update, DialogResult.Succes);
                                    ((TextBox)controls["Adi"]).Focus();
                                    break;
                            }
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
                if (!BAYMYO.UI.Converts.NullToString(((TreeView)controls["Kategoriler"]).SelectedNode.DataPath).Equals("0"))
                    if (KategoriMethods.Delete(Request.QueryString["mdl"], BAYMYO.UI.Converts.NullToString(((TreeView)controls["Kategoriler"]).SelectedNode.DataPath)) > 0)
                    {
                        ((TreeView)controls["Kategoriler"]).DataBind();
                        Core.CreateCategoryMaps(Request.QueryString["mdl"]);
                        jSonData.CreateData(Request.QueryString["mdl"]);
                        CustomizeControl1.MessageText = MessageBox.Show(ProccesType.Delete, DialogResult.Succes);
                        Core.ClearControls(controls);
                        ((TextBox)controls["Adi"]).Focus();
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