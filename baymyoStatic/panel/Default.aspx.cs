using System;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace baymyoStatic.panel
{
    public partial class Default : System.Web.UI.Page
    {
        protected override void OnPreInit(EventArgs e)
        {
            string goName = Request.QueryString["go"];
            #region --- Yetki Kontrol ---
            if (Core.IsUserActive)
                switch (goName)
                {
                    case "index"://"Panel", "P"
                        //if (!Core.CurrentUser.Roller.Contains("P"))
                        //    goName = "secure";
                        break;
                    case "hesap"://"hesap", "H"
                    case "hesapliste":
                        if (!Core.CurrentUser.Roller.Contains("H"))
                            goName = "secure";
                        switch (Core.CurrentUser.Tipi)
                        {
                            case AccountType.None:
                            case AccountType.Standart:
                            case AccountType.Private:
                                goName = "secure";
                                break;
                        }
                        break;
                    case "portal"://"portal", "A"
                        if (!Core.CurrentUser.Roller.Contains("A"))
                            goName = "secure";
                        break;
                    case "bakiye"://"bakiye", "B"
                    case "bakiyeliste":
                        if (!Core.CurrentUser.Roller.Contains("B"))
                            goName = "secure";
                        break;
                    case "paracekme":
                    case "paracekmeliste":
                        if (!Core.CurrentUser.Roller.Contains("C"))
                            goName = "secure";
                        break;
                    case "parayatirma":
                    case "parayatirmaliste":
                        if (!Core.CurrentUser.Roller.Contains("Y"))
                            goName = "secure";
                        break;
                    case "seansliste"://"seans", "S"
                    case "satilanliste":
                    case "kazananliste":
                    case "cekilenliste":
                        if (!Core.CurrentUser.Roller.Contains("S"))
                            goName = "secure";
                        break;
                    case "oda"://"oda", "O"
                    case "odaliste":
                        if (!Core.CurrentUser.Roller.Contains("R"))
                            goName = "secure";
                        break;
                    case "kart"://"kart", "K"
                    case "kartliste":
                        if (!Core.CurrentUser.Roller.Contains("K"))
                            goName = "secure";
                        break;
                    case "sayi"://"sayi", "T"
                    case "sayiliste":
                        if (!Core.CurrentUser.Roller.Contains("T"))
                            goName = "secure";
                        break;
                    case "operators"://"operators", "O"
                        if (!Core.CurrentUser.Roller.Contains("O"))
                            goName = "secure";
                        break;
                }
            else
                goName = "index";
            #endregion
            string modulPath = null;
            if (string.IsNullOrEmpty(goName))
                goName = "index";
            try
            {
                modulPath = Settings.PanelAscxPath + goName + ".ascx";
                UserControl cnt = (UserControl)this.Page.LoadControl(modulPath);
                ((ContentPlaceHolder)this.Page.Master.FindControl("plcModul")).Controls.Add(cnt);
            }
            catch (Exception ex)
            {
                LiteralControl ltr = new LiteralControl();
                ltr.Text = MessageBox.Show(DialogResult.Error, ex.Message);
                ((ContentPlaceHolder)this.Page.Master.FindControl("plcModul")).Controls.Add(ltr);
            }
            goName = modulPath = null;
            base.OnPreInit(e);
        }
        protected void Page_Load(object sender, EventArgs e)
        {

        }
    }
}