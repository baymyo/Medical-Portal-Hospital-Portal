using System;
using System.Web.UI.WebControls;

namespace baymyoStatic.panel.ascx
{
    public partial class sayfaliste : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.Page.IsPostBack)
            {
                Core.GetProccesList("sayfa", ddlIslemler);
                GetDataPaging();
            }
        }

        private void GetDataPaging()
        {
            using (BAYMYO.UI.Web.DataPagers data = new BAYMYO.UI.Web.DataPagers(dataGrid1, "sayfa", "id desc"))
            {
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
                    string columnName = "tipi"; object value = ddlIslemler.SelectedIndex;
                    switch (ddlIslemler.SelectedIndex)
                    {
                        case 5:
                            columnName = "aktif";
                            value = true;
                            break;
                        case 6:
                            columnName = "aktif";
                            value = false;
                            break;
                    }
                    foreach (GridViewRow item in dataGrid1.Rows)
                        if (((CheckBox)item.Cells[0].FindControl("chkSelected")).Checked)
                            Core.Update("sayfa", columnName, BAYMYO.UI.Converts.NullToInt16(dataGrid1.DataKeys[item.RowIndex][0]), value);
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