using System;
using System.Web.UI.WebControls;

namespace baymyoStatic.panel.ascx
{
    public partial class habereditorliste : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.Page.IsPostBack)
            {
                #region --- Kategoriler ---
                HesapCollection kategoriler = HesapMethods.GetSelect();
                kategoriler.Insert(0, new Hesap { ID = string.Empty, Adi = "Editöre göre süz!" });
                ddlKategoriler.DataMember = "Hesap";
                ddlKategoriler.DataValueField = "ID";
                ddlKategoriler.DataTextField = "Adi";
                ddlKategoriler.DataSource = kategoriler;
                ddlKategoriler.DataBind();
                #endregion
                Core.GetProccesList("haber", ddlIslemler);
                GetDataPaging();
            }
        }

        private void GetDataPaging()
        {
            using (BAYMYO.UI.Web.DataPagers data = new BAYMYO.UI.Web.DataPagers(dataGrid1, "haber", "kayittarihi desc", "1=1", 25))
            {
                if (!string.IsNullOrEmpty(Request.QueryString["hid"]))
                {
                    ddlKategoriler.SelectedValue = Request.QueryString["hid"];
                    data.Where += " and hesapid=?hesapid";
                    data.Parameters.Add("hesapid", ddlKategoriler.SelectedValue, BAYMYO.MultiSQLClient.MSqlDbType.VarChar);
                }
                if (!string.IsNullOrEmpty(Request.QueryString["onay"]))
                {
                    data.Where += " and yoneticionay=?yoneticionay";
                    data.Parameters.Add("yoneticionay", Request.QueryString["onay"], BAYMYO.MultiSQLClient.MSqlDbType.Boolean);
                }
                if (!string.IsNullOrEmpty(Request.QueryString["aktif"]))
                {
                    data.Where += " or aktif=?aktif";
                    data.Parameters.Add("aktif", Request.QueryString["aktif"], BAYMYO.MultiSQLClient.MSqlDbType.Boolean);
                }
                data.PageNumberTargetControl = pageNumberLiteral;
                data.Binding();
                totalCount = string.Format("Toplam <b>{0}</b> haber.", data.TotalDataCount);
            }
        }

        protected void btnSaveChanges_Click(object sender, EventArgs e)
        {
            if (ddlIslemler.SelectedIndex > 0 & Core.IsUserAdmin)
            {
                bool chkState = false;
                switch (ddlIslemler.SelectedIndex)
                {
                    case 1:
                    case 3:
                        chkState = true;
                        break;
                }
                if (ddlIslemler.SelectedIndex == 1 || ddlIslemler.SelectedIndex == 2)
                {
                    foreach (GridViewRow item in dataGrid1.Rows)
                        if (((CheckBox)item.Cells[0].FindControl("chkSelected")).Checked)
                            Core.Update("haber", "yoneticionay", BAYMYO.UI.Converts.NullToInt64(dataGrid1.DataKeys[item.RowIndex][0]), chkState);
                    jSonData.CreateData("haberler");
                    GetDataPaging();
                }
                else if (ddlIslemler.SelectedIndex == 3 || ddlIslemler.SelectedIndex == 4)
                {
                    foreach (GridViewRow item in dataGrid1.Rows)
                        if (((CheckBox)item.Cells[0].FindControl("chkSelected")).Checked)
                            Core.Update("haber", "aktif", BAYMYO.UI.Converts.NullToInt64(dataGrid1.DataKeys[item.RowIndex][0]), chkState);
                    jSonData.CreateData("haberler");
                    GetDataPaging();
                }
            }
        }

        public string totalCount = string.Empty;
        protected void ddlKategoriler_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlKategoriler.SelectedIndex > 0)
                Response.Redirect(Settings.PanelPath + "?go=" + Request.QueryString["go"] + "&hid=" + ddlKategoriler.SelectedValue, false);
            else
                Response.Redirect(Settings.PanelPath + "?go=" + Request.QueryString["go"], false);
        }
    }
}