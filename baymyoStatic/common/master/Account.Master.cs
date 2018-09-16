using System;

namespace baymyoStatic.master
{
    public partial class Account : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Core.IsUserActive)
                ltrMenu.Text = BAYMYO.UI.FileIO.ReadText(Server.MapPath(Settings.ViewPath) + "Menu.view")
                    .Replace("%VirtualPath%", Settings.VirtualPath)
                    .Replace("%AdSoyad%", Core.CurrentUser.Adi + " " + Core.CurrentUser.Soyadi);
            ltrCopyright.Text = BAYMYO.UI.FileIO.ReadText(Server.MapPath(Settings.ViewPath) + "Copyright.view");
        }
    }
}