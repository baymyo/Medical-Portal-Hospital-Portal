using System;
using System.Collections.Generic;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace baymyoStatic.panel.ascx
{
    public partial class portalseo : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            CustomizeControl1.FormTitle = string.Format(Settings.FormTitleFormat, "Portal", "SEO");
            CustomizeControl1.RemoveVisible = false;

            TextBox txt = new TextBox();
            txt.ID = "JsonLd";
            txt.CssClass = "form-control";
            txt.Height = 500;
            txt.TextMode = TextBoxMode.MultiLine;
            txt.Text = BAYMYO.UI.FileIO.ReadText(Server.MapPath(Settings.ViewPath + "JsonLd.view"));
            CustomizeControl1.AddControl("SEO", txt, "Bu alanda JSON+LD içerik girilerek SEO optimizasyonu yapılabilir.");

            CustomizeControl1.SubmitClick += new CustomizeControl.ButtonEvent(CustomizeControl1_SubmitClick);
        }

        void CustomizeControl1_SubmitClick(SortedDictionary<string, Control> controls)
        {
            BAYMYO.UI.FileIO.WriteText(Server.MapPath(Settings.ViewPath + "JsonLd.view"), ((TextBox)controls["JsonLd"]).Text, System.Text.Encoding.UTF8);
        }
    }
}