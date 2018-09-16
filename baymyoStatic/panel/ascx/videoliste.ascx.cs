using System;
using System.Collections.Generic;
using System.Web.UI.WebControls;

namespace baymyoStatic.panel.ascx
{
    public partial class videoliste : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.Page.IsPostBack)
            {
                #region --- Kategoriler ---
                List<Kategori> kategoriler = KategoriMethods.GetMenu("video", false);
                kategoriler.Insert(0, new Kategori() { ID = "", Adi = "Kategoriye göre süz!" });
                ddlKategoriler.DataValueField = "id";
                ddlKategoriler.DataTextField = "adi";
                ddlKategoriler.DataSource = kategoriler;
                ddlKategoriler.DataBind();
                #endregion
                Core.GetProccesList("video", ddlIslemler);
                GetDataPaging();
            }
        }

        private void GetDataPaging()
        {
            using (BAYMYO.UI.Web.DataPagers data = new BAYMYO.UI.Web.DataPagers(dataGrid1, "video", "id desc", "1=1", 25))
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
                totalCount = string.Format("Toplam <b>{0}</b> video.", data.TotalDataCount);
            }
        }

        protected void btnSaveChanges_Click(object sender, EventArgs e)
        {
            try
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
                                Core.Update("video", "yoneticionay", BAYMYO.UI.Converts.NullToInt64(dataGrid1.DataKeys[item.RowIndex][0]), chkState);
                        jSonData.CreateData("videolar");
                        GetDataPaging();
                    }
                    else if (ddlIslemler.SelectedIndex == 3 || ddlIslemler.SelectedIndex == 4)
                    {
                        foreach (GridViewRow item in dataGrid1.Rows)
                            if (((CheckBox)item.Cells[0].FindControl("chkSelected")).Checked)
                                Core.Update("video", "aktif", BAYMYO.UI.Converts.NullToInt64(dataGrid1.DataKeys[item.RowIndex][0]), chkState);
                        jSonData.CreateData("videolar");
                        GetDataPaging();
                    }
                }
            }
            catch (Exception ex)
            {
                pageNumberLiteral.Text = ex.Message;
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