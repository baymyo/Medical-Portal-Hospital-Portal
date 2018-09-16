using System;
using System.Web.UI.WebControls;

namespace baymyoStatic.panel.ascx
{
    public partial class yorumliste : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.Page.IsPostBack)
            {
                Core.GetProccesList("yorum", ddlIslemler);
                GetDataPaging();
            }
        }

        private void GetDataPaging()
        {
            using (BAYMYO.UI.Web.DataPagers data = new BAYMYO.UI.Web.DataPagers(dataGrid1, "yorum", "kayittarihi desc", "1=1"))
            {
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
                data.ViewDataCount = 25;
                data.Binding();
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
                                Core.Update("yorum", "yoneticionay", BAYMYO.UI.Converts.NullToGuidString(dataGrid1.DataKeys[item.RowIndex][0]), chkState);
                        GetDataPaging();
                    }
                    else if (ddlIslemler.SelectedIndex == 3 || ddlIslemler.SelectedIndex == 4)
                    {
                        foreach (GridViewRow item in dataGrid1.Rows)
                            if (((CheckBox)item.Cells[0].FindControl("chkSelected")).Checked)
                                Core.Update("yorum", "aktif", BAYMYO.UI.Converts.NullToGuidString(dataGrid1.DataKeys[item.RowIndex][0]), chkState);
                        GetDataPaging();
                    }
                    else if (ddlIslemler.SelectedIndex == 5)
                    {
                        foreach (GridViewRow item in dataGrid1.Rows)
                            if (((CheckBox)item.Cells[0].FindControl("chkSelected")).Checked)
                                YorumMethods.Publish(BAYMYO.UI.Converts.NullToGuidString(dataGrid1.DataKeys[item.RowIndex][0]));
                        GetDataPaging();
                    }
                    else if (ddlIslemler.SelectedIndex == 6)
                    {
                        foreach (GridViewRow item in dataGrid1.Rows)
                            if (((CheckBox)item.Cells[0].FindControl("chkSelected")).Checked)
                                YorumMethods.Delete(BAYMYO.UI.Converts.NullToGuidString(dataGrid1.DataKeys[item.RowIndex][0]));
                        GetDataPaging();
                    }
                }
            }
            catch (Exception ex)
            {
                pageNumberLiteral.Text = ex.Message;
            }
        }
    }
}