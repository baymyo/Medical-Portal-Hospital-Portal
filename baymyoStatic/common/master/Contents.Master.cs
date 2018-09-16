using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace baymyoStatic.common.master
{
    public partial class Contents : System.Web.UI.MasterPage
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

                rptLeft.DataSource = SayfaMethods.GetSelect(1, 15);
                rptLeft.DataBind();
            }
        }
    }
}