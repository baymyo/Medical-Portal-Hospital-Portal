using System;

namespace baymyoStatic.panel.ascx
{
    public partial class secure : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            this.Page.Title = "Güvenlik Duvarı - Firewall";
        }
    }
}