using System;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace baymyoStatic
{
    public partial class Default : System.Web.UI.Page
    {
        protected override void OnPreInit(EventArgs e)
        {
            string goName = Request.QueryString["go"], folder = string.Empty;
            #region --- Yetki Kontrol ---
            switch (goName)
            {
                case "myaccount":
                    if (!Core.IsUserActive | !Core.CurrentUser.Roller.Contains("U"))
                        goName = "login";
                    else if (Core.IsUserActive & Core.CurrentUser.Roller.Contains("U"))
                        folder = "account/";
                    break;
            }
            #endregion
            #region --- Master Change ---
            switch (goName)
            {
                case "account":
                case "login":
                case "logout":
                case "register":
                case "remember":
                case "myaccount":
                    this.MasterPageFile = "~/common/master/Account.Master";
                    break;
                case "index":
                    this.MasterPageFile = "~/common/master/Default.Master";
                    break;
                default:
                    if (!string.IsNullOrEmpty(goName))
                        this.MasterPageFile = "~/common/master/Contents.Master";
                    else
                        this.MasterPageFile = "~/common/master/Default.Master";
                    break;
            }
            #endregion
            string modulPath = null;
            if (string.IsNullOrEmpty(goName))
                goName = "index";
            try
            {
                modulPath = Settings.AscxPath + folder + goName + ".ascx";
                UserControl cnt = (UserControl)this.Page.LoadControl(modulPath);
                ((ContentPlaceHolder)this.Page.Master.FindControl("plcModul")).Controls.Add(cnt);
            }
            catch (Exception ex)
            {
            }
            goName = modulPath = null;
            base.OnPreInit(e);
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            //Response.Buffer = true;
            //Response.Cache.SetCacheability(System.Web.HttpCacheability.Private);
            //Response.Cache.SetExpires(DateTime.Now.AddMinutes(60));
            //Response.Cache.SetMaxAge(new TimeSpan(1, 0, 0));
        }
    }
}