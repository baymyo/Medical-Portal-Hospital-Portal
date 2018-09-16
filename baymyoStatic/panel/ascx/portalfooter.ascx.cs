using System;
using System.Collections.Generic;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace baymyoStatic.panel.ascx
{
    public partial class portalfooter : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            CustomizeControl1.FormTitle = string.Format(Settings.FormTitleFormat, "Portal", "Footer SEO");
            CustomizeControl1.RemoveVisible = false;

            TextBox txt = new TextBox();
            txt.ID = "FooterBox";
            txt.CssClass = "form-control";
            txt.Height = 500;
            txt.TextMode = TextBoxMode.MultiLine;
            txt.Text = BAYMYO.UI.FileIO.ReadText(Server.MapPath(Settings.ViewPath + "FooterBox.view"));
            CustomizeControl1.AddControl("Footer", txt, "Bu alanda HTML içerik girilerek SEO optimizasyonu yapılabilir.");

            CustomizeControl1.SubmitClick += new CustomizeControl.ButtonEvent(CustomizeControl1_SubmitClick);
        }

        void CustomizeControl1_SubmitClick(SortedDictionary<string, Control> controls)
        {
            BAYMYO.UI.FileIO.WriteText(Server.MapPath(Settings.ViewPath + "FooterBox.view"), ((TextBox)controls["FooterBox"]).Text, System.Text.Encoding.UTF8);
        }
    }
}