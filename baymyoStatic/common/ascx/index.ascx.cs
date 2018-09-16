using System;

namespace baymyoStatic.common.ascx
{
    public partial class index : System.Web.UI.UserControl
    {
        protected override void OnInit(EventArgs e)
        {
            try
            {
                //if (!this.Page.IsPostBack)
                //    ltrSlider.Text = BAYMYO.UI.FileIO.ReadText(Server.MapPath(Settings.ViewPath) + "SliderBox.view");
            }
            catch (Exception ex)
            {
                throw ex;
            }
            base.OnInit(e);
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            this.Page.Title = baymyoStatic.Settings.Site.Title;
        }
    }
}