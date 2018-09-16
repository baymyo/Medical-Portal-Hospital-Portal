using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace baymyoStatic.common.ascx
{
    public partial class sayfagoster : System.Web.UI.UserControl
    {

        public Sayfa m;
        protected void Page_Load(object sender, EventArgs e)
        {
            m = SayfaMethods.GetSayfa(BAYMYO.UI.Converts.NullToInt16(Request.QueryString["sid"]));
            if (m != null)
                if (!m.Aktif & !Core.CurrentUser.Tipi.Equals(AccountType.Admin))
                {
                    this.Page.Title = "Aradığınız içerik bulunamadı!";
                    m.Icerik = MessageBox.Show(DialogResult.Warning, "Bu içerik gösterime kapatılmıştır. Kimler yayından kaldırabilir yazarı yada yöneticilerimiz tarafından yayından kaldırılabilir.");
                }
                else
                {
                    this.Page.Title = BAYMYO.UI.Web.Pages.ClearHtml(m.Baslik);
                    BAYMYO.UI.Web.Pages.AddMetaTag(this.Page, m.Baslik, BAYMYO.UI.Web.Pages.ClearHtml(m.Icerik));
                }
        }
    }
}