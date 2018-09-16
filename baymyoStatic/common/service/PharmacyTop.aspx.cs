using System;

namespace baymyoStatic.common.service
{
    public partial class PharmacyTop : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            ltrAdv790_Top.Text = BAYMYO.UI.FileIO.ReadText(Server.MapPath(Settings.ViewPath) + "Adventure728x90.view");
            adv790_top.Visible = string.IsNullOrWhiteSpace(ltrAdv790_Top.Text);
            if (adv790_top.Visible)
                if (System.IO.File.Exists(Server.MapPath(Settings.XmlPath + "adv_728x90_1.xml")))
                    adv790_top.AdvertisementFile = Settings.XmlPath + "adv_728x90_1.xml";
            ltrGoogleAnalytics.Text = BAYMYO.UI.FileIO.ReadText(Server.MapPath(Settings.ViewPath) + "GoogleAnalytics.view");
        }
    }
}