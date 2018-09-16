using System;
using System.Collections.Generic;
using System.Web.UI.WebControls;

namespace baymyoStatic.panel.ascx
{
    public partial class haberliste : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.Page.IsPostBack)
            {
                #region --- Kategoriler ---
                List<Kategori> kategoriler = KategoriMethods.GetMenu("haber", false);
                ddlKategoriler.DataMember = "kategori";
                ddlKategoriler.DataValueField = "id";
                ddlKategoriler.DataTextField = "adi";
                ddlKategoriler.Items.Add(new ListItem("Kategoriye göre süz!", "0"));
                ListItem item = null;
                foreach (Kategori kategori in kategoriler)
                {
                    switch (kategori.ParentID)
                    {
                        case "":
                            item = new ListItem(kategori.Adi, kategori.ID);
                            item.Attributes.CssStyle.Value = "padding-left: 5px; background: #f5f5f5; color: #454545; font-weight:bold;";
                            break;
                        case "0":
                            item = new ListItem(kategori.Adi, kategori.ID);
                            item.Attributes.CssStyle.Value = "padding-left: 25px; background: #f5f5f5; color: #fe760c; font-weight: bold;";
                            break;
                        default:
                            item = new ListItem(kategori.Adi, kategori.ID);
                            item.Attributes.CssStyle.Value = string.Format("padding-left: {0}px; background: #f5f5f5; color: #454545;", (BAYMYO.UI.Converts.NullToInt(kategori.ParentID.Split(',').Length + 1) * 25));
                            break;
                    }
                    ddlKategoriler.Items.Add(item);
                }
                kategoriler.Clear();
                #endregion
                Core.GetProccesList("haber", ddlIslemler);
                GetDataPaging();
            }
        }

        private void GetDataPaging()
        {
            using (BAYMYO.UI.Web.DataPagers data = new BAYMYO.UI.Web.DataPagers(dataGrid1, "haber", "kayittarihi desc", "1=1", 25))
            {
                if (!string.IsNullOrEmpty(Request.QueryString["kid"]))
                {
                    ddlKategoriler.SelectedValue = Request.QueryString["kid"];
                    data.Where += " and kategoriid=?kategoriid";
                    data.Parameters.Add("kategoriid", ddlKategoriler.SelectedValue, BAYMYO.MultiSQLClient.MSqlDbType.VarChar);
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
                Response.Redirect(Settings.PanelPath + "?go=" + Request.QueryString["go"] + "&kid=" + ddlKategoriler.SelectedValue, false);
            else
                Response.Redirect(Settings.PanelPath + "?go=" + Request.QueryString["go"], false);
        }
    }
}