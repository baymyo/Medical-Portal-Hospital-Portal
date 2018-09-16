using System;
using System.Web;

namespace baymyoStatic.master
{
    public partial class Default : System.Web.UI.MasterPage
    {
        protected override void OnInit(EventArgs e)
        {
            try
            {
                if (!this.Page.IsPostBack)
                {
                    ltrFooter.Text = BAYMYO.UI.FileIO.ReadText(Server.MapPath(Settings.ViewPath) + "FooterBox.view");
                    ltrGoogleAnalytics.Text = BAYMYO.UI.FileIO.ReadText(Server.MapPath(Settings.ViewPath) + "GoogleAnalytics.view");
                    ltrCopyright.Text = BAYMYO.UI.FileIO.ReadText(Server.MapPath(Settings.ViewPath) + "Copyright.view");
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            base.OnInit(e);
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Page.IsPostBack)
            {
                rptTop.DataSource = SayfaMethods.GetSelect(1, 10);
                rptTop.DataBind();
            }
        }
    }
}