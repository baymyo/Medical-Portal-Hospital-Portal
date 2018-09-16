using System;
using System.Collections.Generic;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace baymyoStatic.panel.ascx
{
    public partial class portalstyle : System.Web.UI.UserControl
    {
        protected override void OnInit(EventArgs e)
        {

            CustomizeControl1.FormTitle = string.Format(Settings.FormTitleFormat, "Portal", "Tasarımı");
            CustomizeControl1.RemoveVisible = false;
            using (PortalStyle p = PortalStyleMethods.Read())
            {
                TextBox txt = new TextBox();
                txt.ID = "CssCategory";
                txt.CssClass = "form-control";
                txt.MaxLength = 7;
                txt.Text = p.CssCategory;
                CustomizeControl1.AddControl("Kategori Arkaplan", txt, "<a href=\"http://www.google.com/design/spec/style/color.html#color-color-palette\" target=\"_blank\"><b>Google renk paleti için tıklayın.</b></a>&nbsp;&nbsp;Örnek: <b>#cf0a0a</b>");

                CustomizeControl1.AddTitle("Sondakika ve Finans Bilgileri Bandı");

                DropDownList ddl = new DropDownList();
                ddl.ID = "CssBand";
                ddl.CssClass = "form-control";
                ddl.Width = 300;
                ddl.Items.AddRange(PortalStyleMethods.GetCssStyles());
                ddl.SelectedValue = p.CssBand;
                CustomizeControl1.AddControl("Arkaplan", ddl, "<a class=\"toolTip\" alt=\"Sondakika haber bandı\" title=\"<img src=" + Settings.ImagesPath + "band.jpg>\"><b>Sondakika ve Finans bandı</b></a> arkaplanı rengi.");

                ddl = new DropDownList();
                ddl.ID = "CssBandOther";
                ddl.CssClass = "form-control";
                ddl.Width = 300;
                ddl.Items.AddRange(PortalStyleMethods.GetCssStyles());
                ddl.SelectedValue = p.CssBandOther;
                CustomizeControl1.AddControl("Başlık", ddl, "<a class=\"toolTip\" alt=\"Sondakika haber bandı\" title=\"<img src=" + Settings.ImagesPath + "band.jpg>\"><b>Sondakika ve Finans bandı</b></a> başlık arkaplanı rengi.");

                CustomizeControl1.AddTitle("Flaş Manşet Bloğu");

                ddl = new DropDownList();
                ddl.ID = "CssFlashNews";
                ddl.CssClass = "form-control";
                ddl.Width = 300;
                ddl.Items.AddRange(PortalStyleMethods.GetCssStyles());
                ddl.SelectedValue = p.CssFlashNews;
                CustomizeControl1.AddControl("Arkaplan", ddl, "Tek Haber bandı <a class=\"toolTip\" alt=\"Son dakika\" title=\"<img src=" + Settings.ImagesPath + "manset-flas.png>\"><b>Flash Manşet</b></a> arkaplanı renk.");

                ddl = new DropDownList();
                ddl.ID = "CssFlashNewsOther";
                ddl.CssClass = "form-control";
                ddl.Width = 300;
                ddl.Items.AddRange(PortalStyleMethods.GetCssStyles());
                ddl.SelectedValue = p.CssFlashNewsOther;
                CustomizeControl1.AddControl("Başlık", ddl, "Tek Haber bandı <a class=\"toolTip\" alt=\"Son dakika\" title=\"<img src=" + Settings.ImagesPath + "manset-flas.png>\"><b>Flash Manşet</b></a> başlık arkaplanı rengi.");

                CustomizeControl1.AddTitle("Kayıt ve İletişim Formları");

                ddl = new DropDownList();
                ddl.ID = "CssForm";
                ddl.CssClass = "form-control";
                ddl.Width = 300;
                ddl.Items.AddRange(PortalStyleMethods.GetCssStyles());
                ddl.SelectedValue = p.CssForm;
                CustomizeControl1.AddControl("Form 1.Renk", ddl, "<a class=\"toolTip\" alt=\"Liste başlıkları\" title=\"<img src=" + Settings.ImagesPath + "forms.png>\"><b>Kayıt ve İletişim Formu</b></a> 1. başlık ve buton arkaplan rengi.");

                ddl = new DropDownList();
                ddl.ID = "CssFormOther";
                ddl.CssClass = "form-control";
                ddl.Width = 300;
                ddl.Items.AddRange(PortalStyleMethods.GetCssStyles());
                ddl.SelectedValue = p.CssFormOther;
                CustomizeControl1.AddControl("Form 2.Renk", ddl, "<a class=\"toolTip\" alt=\"Kayıt ve İletişim Formu\" title=\"<img src=" + Settings.ImagesPath + "forms.png>\"><b>Kayıt ve İletişim Formu</b></a> 2. başlık arkaplan rengi.");

                CustomizeControl1.AddTitle("Köşe Yazıları Bloğu");

                ddl = new DropDownList();
                ddl.ID = "CssArticleNews";
                ddl.CssClass = "form-control";
                ddl.Width = 300;
                ddl.Items.AddRange(PortalStyleMethods.GetCssStyles());
                ddl.SelectedValue = p.CssArticleNews;
                CustomizeControl1.AddControl("Arkaplan", ddl, "<a class=\"toolTip\" alt=\"Köşe Yazıları\" title=\"<img src=" + Settings.ImagesPath + "kose-yazi.png>\"><b>Köşe Yazıları blok</b></a> arkaplanı renk.");

                ddl = new DropDownList();
                ddl.ID = "CssArticleNewsOther";
                ddl.CssClass = "form-control";
                ddl.Width = 300;
                ddl.Items.AddRange(PortalStyleMethods.GetCssStyles());
                ddl.SelectedValue = p.CssArticleNewsOther;
                CustomizeControl1.AddControl("Başlık ve Buton", ddl, "<a class=\"toolTip\" alt=\"Köşe Yazıları\" title=\"<img src=" + Settings.ImagesPath + "kose-yazi.png>\"><b>Köşe Yazıları blok</b></a> başlık ve button arkaplanı rengi.");

                CustomizeControl1.AddTitle("Son Gelişmeler Manşet Bloğu");

                ddl = new DropDownList();
                ddl.ID = "CssLastNews";
                ddl.CssClass = "form-control";
                ddl.Width = 300;
                ddl.Items.AddRange(PortalStyleMethods.GetCssStyles());
                ddl.SelectedValue = p.CssLastNews;
                CustomizeControl1.AddControl("Arkaplan", ddl, "Son gelişmeler <a class=\"toolTip\" alt=\"Son dakika\" title=\"<img src=" + Settings.ImagesPath + "son-dakika.png>\"><b>Blok Manşet</b></a> arkaplanı renk.");

                ddl = new DropDownList();
                ddl.ID = "CssLastNewsOther";
                ddl.CssClass = "form-control";
                ddl.Width = 300;
                ddl.Items.AddRange(PortalStyleMethods.GetCssStyles());
                ddl.SelectedValue = p.CssLastNewsOther;
                CustomizeControl1.AddControl("Başlık ve Buton", ddl, "Son gelişmeler <a class=\"toolTip\" alt=\"Son dakika\" title=\"<img src=" + Settings.ImagesPath + "son-dakika.png>\"><b>Blok Manşet</b></a> başlık ve button arkaplanı rengi.");

                CustomizeControl1.AddTitle("Öne Çıkanlar Bloğu");

                ddl = new DropDownList();
                ddl.ID = "CssHitNews";
                ddl.CssClass = "form-control";
                ddl.Width = 300;
                ddl.Items.AddRange(PortalStyleMethods.GetCssStyles());
                ddl.SelectedValue = p.CssHitNews;
                CustomizeControl1.AddControl("Arkaplan", ddl, "<a class=\"toolTip\" alt=\"Hit News\" title=\"<img src=" + Settings.ImagesPath + "one-cikanlar.png>\"><b>Öne Çıkanlar</b></a> arkaplanı renk.");

                ddl = new DropDownList();
                ddl.ID = "CssHitNewsOther";
                ddl.CssClass = "form-control";
                ddl.Width = 300;
                ddl.Items.AddRange(PortalStyleMethods.GetCssStyles());
                ddl.SelectedValue = p.CssHitNewsOther;
                CustomizeControl1.AddControl("Başlık ve Buton", ddl, "<a class=\"toolTip\" alt=\"Hit News\" title=\"<img src=" + Settings.ImagesPath + "one-cikanlar.png>\"><b>Öne Çıkanlar</b></a> başlık ve button arkaplanı rengi.");

                CustomizeControl1.AddTitle("Gazeteler Bloğu");

                ddl = new DropDownList();
                ddl.ID = "CssPaper";
                ddl.CssClass = "form-control";
                ddl.Width = 300;
                ddl.Items.AddRange(PortalStyleMethods.GetCssStyles());
                ddl.SelectedValue = p.CssPaper;
                CustomizeControl1.AddControl("Arkaplan", ddl, "<a class=\"toolTip\" alt=\"Hit News\" title=\"<img src=" + Settings.ImagesPath + "gazeteler.png>\"><b>Gazeteler</b></a> arkaplanı renk.");

                ddl = new DropDownList();
                ddl.ID = "CssPaperOther";
                ddl.CssClass = "form-control";
                ddl.Width = 300;
                ddl.Items.AddRange(PortalStyleMethods.GetCssStyles());
                ddl.SelectedValue = p.CssPaperOther;
                CustomizeControl1.AddControl("Başlık ve Buton", ddl, "<a class=\"toolTip\" alt=\"Hit News\" title=\"<img src=" + Settings.ImagesPath + "gazeteler.png>\"><b>Gazeteler</b></a> başlık ve button arkaplanı rengi.");

                CustomizeControl1.AddTitle("Liste Başlık Style ve Görünümü");

                ddl = new DropDownList();
                ddl.ID = "CssListTitle";
                ddl.CssClass = "form-control";
                ddl.Width = 300;
                ddl.Items.AddRange(PortalStyleMethods.GetCssStyles());
                ddl.SelectedValue = p.CssListTitle;
                CustomizeControl1.AddControl("Başlık", ddl, "<a class=\"toolTip\" alt=\"Liste başlıkları\" title=\"<img src=" + Settings.ImagesPath + "lists.jpg>\"><b>Liste başlıkları</b></a> arkaplan renk.");

                ddl = new DropDownList();
                ddl.ID = "CssListViewName";
                ddl.CssClass = "form-control";
                ddl.Width = 300;
                ddl.Items.Insert(0, new ListItem("Tek sıra olarak göster!", "single-list"));
                ddl.Items.Insert(1, new ListItem("Çift sıra olarak göster!", "double-list"));
                ddl.SelectedValue = p.CssListViewName;
                CustomizeControl1.AddControl("Liste Görünümü", ddl);

                CustomizeControl1.SubmitClick += new CustomizeControl.ButtonEvent(CustomizeControl1_SubmitClick);
            }
            base.OnInit(e);
        }

        void CustomizeControl1_SubmitClick(SortedDictionary<string, Control> controls)
        {
            try
            {
                if (!string.IsNullOrEmpty(((TextBox)controls["CssCategory"]).Text))
                {
                    using (PortalStyle p = new PortalStyle())
                    {
                        p.CssCategory = ((TextBox)controls["CssCategory"]).Text;
                        p.CssListTitle = ((DropDownList)controls["CssListTitle"]).SelectedValue;
                        p.CssForm = ((DropDownList)controls["CssForm"]).SelectedValue;
                        p.CssFormOther = ((DropDownList)controls["CssFormOther"]).SelectedValue;
                        p.CssBand = ((DropDownList)controls["CssBand"]).SelectedValue;
                        p.CssBandOther = ((DropDownList)controls["CssBandOther"]).SelectedValue;
                        p.CssFlashNews = ((DropDownList)controls["CssFlashNews"]).SelectedValue;
                        p.CssFlashNewsOther = ((DropDownList)controls["CssFlashNewsOther"]).SelectedValue;
                        p.CssArticleNews = ((DropDownList)controls["CssArticleNews"]).SelectedValue;
                        p.CssArticleNewsOther = ((DropDownList)controls["CssArticleNewsOther"]).SelectedValue;
                        p.CssLastNews = ((DropDownList)controls["CssLastNews"]).SelectedValue;
                        p.CssLastNewsOther = ((DropDownList)controls["CssLastNewsOther"]).SelectedValue;
                        p.CssHitNews = ((DropDownList)controls["CssHitNews"]).SelectedValue;
                        p.CssHitNewsOther = ((DropDownList)controls["CssHitNewsOther"]).SelectedValue;
                        p.CssPaper = ((DropDownList)controls["CssPaper"]).SelectedValue;
                        p.CssPaperOther = ((DropDownList)controls["CssPaperOther"]).SelectedValue;
                        p.CssListViewName = BAYMYO.UI.Converts.NullToString(((DropDownList)controls["CssListViewName"]).SelectedValue);
                        if (PortalStyleMethods.Save(p))
                            CustomizeControl1.MessageText = MessageBox.Show(ProccesType.Insert, DialogResult.Succes);
                        else
                            CustomizeControl1.MessageText = MessageBox.Show(ProccesType.Insert, DialogResult.Stop);
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