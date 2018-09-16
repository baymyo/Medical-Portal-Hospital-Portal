using System;

namespace baymyoStatic.common.ascx
{
    public partial class logout : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            this.Page.Title = "Güvenli Çıkılş - " + Settings.Site.Title;
            if (Request.QueryString["go"].Equals("logout"))
            {
                Session.Remove("UserInfo");
                System.Web.Security.FormsAuthentication.SignOut();
                Response.Redirect(Settings.VirtualPath, false);
            }
        }
    }
}