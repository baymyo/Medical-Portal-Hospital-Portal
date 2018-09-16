using System;
using System.Collections.Generic;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace baymyoStatic.panel.ascx
{
    public partial class manset : System.Web.UI.UserControl
    {
        void CreateXml()
        {
            try
            {
                using (System.Data.DataTable dt = new System.Data.DataTable("manset"))
                {
                    using (BAYMYO.UI.Web.CustomSqlQuery query = new BAYMYO.UI.Web.CustomSqlQuery(dt, "manset", "guncellemetarihi desc", "aktif=1"))
                    {
                        query.Top = 25;
                        query.Execute();
                        dt.WriteXml(Server.MapPath(Settings.XmlPath + "manset.xml"), System.Data.XmlWriteMode.WriteSchema);
                    }
                }
            }
            catch (Exception ex)
            {
                CustomizeControl1.MessageText = MessageBox.Show(DialogResult.Error, ex.Message);
            }
        }

        string modulID, icerikID, modulPath;
        protected override void OnInit(EventArgs e)
        {
            try
            {
                CustomizeControl1.FormTitle = string.Format(Settings.FormTitleFormat, "Manşet", "Ekleme/Düzeltme Formu");
                modulID = BAYMYO.UI.Converts.NullToString(Request.QueryString["mdl"]).Trim();
                if (string.IsNullOrEmpty(modulID))
                    modulID = "diger";
                icerikID = BAYMYO.UI.Converts.NullToString(Request.QueryString["mcid"]).Trim();
                modulPath = Settings.ImagesPath + "manset/" + modulID + "/";
                CustomizeControl1.MessageText = MessageBox.Show(DialogResult.Info, string.Format("<b>'{0}'</b> modülü için manşet tanımlamaktasınız! <b>'{0}'</b> manşet için gerekli bilgileri doldurunuz ve kaydet butonuna tıklayınız.", modulID.ToUpper()));
                using (Manset m = MansetMethods.GetManset(BAYMYO.UI.Converts.NullToString(Request.QueryString["mid"]).Trim()))
                {
                    bool notNull = !string.IsNullOrEmpty(m.ID), isAdmin = Core.IsUserAdmin;
                    CustomizeControl1.RemoveVisible = notNull & isAdmin;

                    string baglanti = m.Baglanti;
                    if (string.IsNullOrEmpty(baglanti))
                        baglanti = Core.CreateLink(modulID, icerikID, m.Baslik1);

                    Image img = new Image();
                    img.ID = "BuyukResim";
                    img.ToolTip = m.ResimBuyuk;
                    if (!string.IsNullOrEmpty(m.ResimBuyuk))
                        img.ImageUrl = modulPath + m.ResimBuyuk;
                    else
                        img.ImageUrl = Settings.ImagesPath + "yok.png";
                    CustomizeControl1.AddControl("Büyük Resim", img);

                    FileUpload flu = new FileUpload();
                    flu.ID = "ResimBuyuk";
                    flu.ToolTip = m.ResimBuyuk;
                    CustomizeControl1.AddControl("Büyük Resim", flu, "Süper Manşet: Genişlik(W) 825px");

                    TextBox txt = new TextBox();
                    txt.ID = "Baslik1";
                    txt.CssClass = "form-control";
                    txt.Text = m.Baslik1;
                    txt.MaxLength = 50;
                    CustomizeControl1.AddControl("Başlık 1", txt, "Manşet alanında üst başlık.");

                    txt = new TextBox();
                    txt.ID = "Baslik2";
                    txt.CssClass = "form-control";
                    txt.Text = m.Baslik2;
                    txt.MaxLength = 50;
                    CustomizeControl1.AddControl("Başlık 2", txt, "Manşet alanında alt başlık.");

                    txt = new TextBox();
                    txt.ID = "Aciklama";
                    txt.CssClass = "form-control";
                    txt.Text = m.Aciklama;
                    txt.TextMode = TextBoxMode.MultiLine;
                    txt.MaxLength = 150;
                    CustomizeControl1.AddControl("Açıklama", txt, "Açıklama girmezseniz Manşet gösterim kısımında sadece resim görünecektir.");

                    txt = new TextBox();
                    txt.ID = "Baglanti";
                    txt.CssClass = "form-control";
                    txt.Text = baglanti;
                    txt.MaxLength = 150;
                    CustomizeControl1.AddControl("Bağlantısı", txt);

                    txt = new TextBox();
                    txt.ID = "Dugme";
                    txt.CssClass = "form-control";
                    txt.Text = m.Dugme;
                    txt.MaxLength = 20;
                    CustomizeControl1.AddControl("Link Başlık", txt, "Tıklama yapılacak buton adı.");

                    CheckBox chk = new CheckBox();
                    chk.ID = "Aktif";
                    chk.Checked = m.Aktif;
                    CustomizeControl1.AddControl("Yayımla", chk);

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

        void CustomizeControl1_SubmitClick(SortedDictionary<string, Control> controls)
        {
            try
            {
                if (!string.IsNullOrEmpty(modulID)
                    & !string.IsNullOrEmpty(((TextBox)controls["Baglanti"]).Text))
                    using (Manset m = MansetMethods.GetManset(BAYMYO.UI.Converts.NullToString(Request.QueryString["mid"]).Trim()))
                    {
                        m.ModulID = modulID;
                        m.Baslik1 = ((TextBox)controls["Baslik1"]).Text;
                        m.Baslik2 = ((TextBox)controls["Baslik2"]).Text;
                        m.Aciklama = ((TextBox)controls["Aciklama"]).Text;
                        m.Baglanti = ((TextBox)controls["Baglanti"]).Text;
                        m.Dugme = BAYMYO.UI.Commons.SubStringText(((TextBox)controls["Dugme"]).Text, 20);
                        m.Aktif = ((CheckBox)controls["Aktif"]).Checked;
                        m.Yerlesim = 1;
                        if (!string.IsNullOrEmpty(m.ID))
                        {
                            m.GuncellemeTarihi = DateTime.Now;
                            if ((controls["ResimBuyuk"] as FileUpload).HasFile)
                                if (BAYMYO.UI.FileIO.Remove(Server.MapPath(modulPath + m.ResimBuyuk)))
                                    m.ResimBuyuk = BAYMYO.UI.FileIO.UploadImage(controls["ResimBuyuk"] as FileUpload, m.Baslik1 + "-buyuk", Server.MapPath(modulPath), 1084, true);
                            if (MansetMethods.Update(m) > 0)
                            {
                                jSonData.CreateData("mansetler");
                                if (!string.IsNullOrEmpty(m.ResimBuyuk))
                                    ((Image)controls["BuyukResim"]).ImageUrl = modulPath + m.ResimBuyuk;
                                CustomizeControl1.MessageText = MessageBox.Show(ProccesType.Update, DialogResult.Succes);
                            }
                        }
                        else
                        {
                            m.KayitTarihi = DateTime.Now;
                            m.GuncellemeTarihi = m.KayitTarihi;
                            m.ResimBuyuk = BAYMYO.UI.FileIO.UploadImage(controls["ResimBuyuk"] as FileUpload, m.Baslik1 + "-buyuk", Server.MapPath(modulPath), 1084, true);
                            if (MansetMethods.Insert(m) > 0)
                            {
                                jSonData.CreateData("mansetler");
                                if (!string.IsNullOrEmpty(m.ResimBuyuk))
                                    ((Image)controls["BuyukResim"]).ImageUrl = modulPath + m.ResimBuyuk;
                                CustomizeControl1.MessageText = MessageBox.Show(ProccesType.Insert, DialogResult.Succes);
                                ((TextBox)controls["Baslik1"]).Focus();
                            }
                        }
                        CreateXml();
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
                using (Manset m = MansetMethods.GetManset(BAYMYO.UI.Converts.NullToString(Request.QueryString["mid"]).Trim()))
                {
                    bool sil = BAYMYO.UI.FileIO.Remove(Server.MapPath(Settings.ImagesPath + "manset/" + m.ModulID + "/" + m.ResimBuyuk));
                    if (sil)
                        if (MansetMethods.Delete(m) > 0)
                        {
                            CreateXml();
                            jSonData.CreateData("mansetler");
                            CustomizeControl1.MessageText = MessageBox.Show(ProccesType.Delete, DialogResult.Succes);
                            Core.ClearControls(controls);
                            ((TextBox)controls["Baslik1"]).Focus();
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