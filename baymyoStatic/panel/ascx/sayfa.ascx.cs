using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace baymyoStatic.panel.ascx
{
    public partial class sayfa : System.Web.UI.UserControl
    {
        protected override void OnInit(EventArgs e)
        {
            try
            {
                CustomizeControl1.FormTitle = string.Format(Settings.FormTitleFormat, "Sayfa", "Tanımlama");
                using (Sayfa m = SayfaMethods.GetSayfa(BAYMYO.UI.Converts.NullToInt16(Request.QueryString["sid"])))
                {
                    CustomizeControl1.RemoveVisible = (m.ID > 0);

                    TextBox txt = new TextBox();
                    txt.ID = "Adi";
                    txt.CssClass = "form-control";
                    txt.Text = m.Baslik;
                    txt.MaxLength = 50;
                    CustomizeControl1.AddControl("Başlık", txt);

                    CKEditor.NET.CKEditorControl fck = new CKEditor.NET.CKEditorControl();
                    fck.ID = "Icerik";
                    fck.Height = 400;
                    fck.Text = m.Icerik;
                    CustomizeControl1.AddControl("Editör", fck);

                    DropDownList ddl = new DropDownList();
                    ddl.ID = "Yerlesim";
                    ddl.CssClass = "form-control";
                    ddl.Width = 300;
                    ddl.DataValueField = "Key";
                    ddl.DataTextField = "Value";
                    ddl.DataSource = Core.GetMenuTypes();
                    ddl.DataBind();
                    ddl.SelectedValue = m.Yerlesim.ToString();
                    CustomizeControl1.AddControl("Yerleşim", ddl);

                    //ddl = new DropDownList();
                    //ddl.ID = "Dil";
                    //ddl.Width = 300;
                    //ddl.DataValueField = "Key";
                    //ddl.DataTextField = "Value";
                    //ddl.DataSource = Core.GetLanguages();
                    //ddl.DataBind();
                    //ddl.SelectedValue = m.Dil;
                    //CustomizeControl1.AddControl("Dil", ddl);

                    CheckBox chk = new CheckBox();
                    chk.ID = "Aktif";
                    chk.Checked = (m.ID > 0) ? m.Aktif : true;
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
                if (!string.IsNullOrEmpty(((TextBox)controls["Adi"]).Text)
                    & !string.IsNullOrEmpty(((CKEditor.NET.CKEditorControl)controls["Icerik"]).Text))
                    using (Sayfa m = SayfaMethods.GetSayfa(BAYMYO.UI.Converts.NullToInt16(Request.QueryString["sid"])))
                    {
                        m.Baslik = ((TextBox)controls["Adi"]).Text;
                        m.Icerik = ((CKEditor.NET.CKEditorControl)controls["Icerik"]).Text;
                        m.Yerlesim = 0;
                        m.Yerlesim = BAYMYO.UI.Converts.NullToByte(((DropDownList)controls["Yerlesim"]).SelectedValue);
                        m.Dil = "tr-TR";
                        //m.Dil = ((DropDownList)controls["Dil"]).SelectedValue;
                        if (Core.IsUserAdmin)
                            m.Aktif = ((CheckBox)controls["Aktif"]).Checked;
                        else
                            m.Aktif = false;
                        if (m.ID > 0)
                        {
                            if (SayfaMethods.Update(m) > 0)
                                CustomizeControl1.MessageText = MessageBox.Show(ProccesType.Update, DialogResult.Succes);
                        }
                        else
                        {
                            m.HesapID = Core.CurrentUser.ID;
                            m.KayitTarihi = DateTime.Now;
                            if (SayfaMethods.Insert(m) > 0)
                            {
                                CustomizeControl1.MessageText = MessageBox.Show(ProccesType.Insert, DialogResult.Succes);
                                Core.ClearControls(controls);
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
                if (!string.IsNullOrEmpty(Request.QueryString["sid"]))
                    if (SayfaMethods.Delete(BAYMYO.UI.Converts.NullToInt16(Request["sid"])) > 0)
                    {
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