using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace baymyoStatic.panel.ascx
{
    public partial class hesapliste : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.Page.IsPostBack)
            {
                Core.GetProccesList("hesap", ddlIslemler);
                GetDataPaging();
            }
        }

        private void GetDataPaging()
        {
            using (BAYMYO.UI.Web.DataPagers data = new BAYMYO.UI.Web.DataPagers(dataGrid1, "hesap", "aktif asc, kayittarihi desc", "1=1"))
            {
                if (!string.IsNullOrEmpty(Request.QueryString["tipi"]))
                {
                    data.Where += " and tipi=?tipi";
                    data.Parameters.Add("tipi", Request.QueryString["tipi"], BAYMYO.MultiSQLClient.MSqlDbType.TinyInt);
                }
                if (!string.IsNullOrEmpty(Request.QueryString["aktivasyon"]))
                {
                    data.Where += " or aktivasyon=?aktivasyon";
                    data.Parameters.Add("aktivasyon", Request.QueryString["aktivasyon"], BAYMYO.MultiSQLClient.MSqlDbType.Boolean);
                }
                if (!string.IsNullOrEmpty(Request.QueryString["aktif"]))
                {
                    data.Where += " or aktif=?aktif";
                    data.Parameters.Add("aktif", Request.QueryString["aktif"], BAYMYO.MultiSQLClient.MSqlDbType.Boolean);
                }
                if (!string.IsNullOrEmpty(Request.QueryString["adi"]))
                {
                    data.Where += " or adi like ?adi";
                    data.Parameters.Add("adi", Request.QueryString["adi"] + "%", BAYMYO.MultiSQLClient.MSqlDbType.VarChar);
                }
                data.ViewDataCount = 26;
                data.PageNumberTargetControl = pageNumberLiteral;
                data.Binding();
            }
        }

        protected void btnSaveChanges_Click(object sender, EventArgs e)
        {
            try
            {
                if (ddlIslemler.SelectedIndex > 0 & Core.IsUserAdmin)
                {
                    string columnName = "aktif"; bool chkState = false;
                    switch (ddlIslemler.SelectedIndex)
                    {
                        case 1:
                            columnName = "yorum";
                            chkState = true;
                            break;
                        case 2:
                            columnName = "yorum";
                            chkState = false;
                            break;
                        case 3:
                            columnName = "abonelik";
                            chkState = true;
                            break;
                        case 4:
                            columnName = "abonelik";
                            chkState = false;
                            break;
                        case 5:
                            columnName = "aktivasyon";
                            chkState = true;
                            break;
                        case 6:
                            columnName = "aktivasyon";
                            chkState = false;
                            break;
                        case 7:
                            chkState = true;
                            break;
                        case 8:
                            chkState = false;
                            break;
                    }
                    foreach (GridViewRow item in dataGrid1.Rows)
                        if (((CheckBox)item.Cells[0].FindControl("chkSelected")).Checked)
                            Core.Update("hesap", columnName, BAYMYO.UI.Converts.NullToGuid(dataGrid1.DataKeys[item.RowIndex][0]), chkState);
                    GetDataPaging();
                }
            }
            catch (Exception ex)
            {
                pageNumberLiteral.Text = ex.Message;
            }
        }
    }
}