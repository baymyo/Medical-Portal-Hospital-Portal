using System;
using System.Web.UI.WebControls;

namespace baymyoStatic.panel.ascx
{
    public partial class mansetliste : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.Page.IsPostBack)
            {
                //string[] moduller = "anket,haber,makale,firma,seriilan,video,galeri,resmiilan".Split(',');
                string[] moduller = "haber,makale,video,galeri,diger".Split(',');
                foreach (string mdl in moduller)
                    ddlKategoriler.Items.Add(new ListItem(mdl.ToUpper() + " MANŞET", mdl));
                ddlKategoriler.Items.Insert(0, new ListItem("<Seçiniz>", ""));
                Core.GetProccesList("manset", ddlIslemler);
                dataGrid1.Columns[1].Visible = Settings.Site.IsFlashOrder;
                GetDataPaging();
            }
        }

        void CreateXml()
        {
            try
            {
                using (System.Data.DataTable dt = new System.Data.DataTable("manset"))
                {
                    using (BAYMYO.UI.Web.CustomSqlQuery query = new BAYMYO.UI.Web.CustomSqlQuery(dt, "manset", "kayittarihi desc", "aktif=1"))
                    {
                        query.Top = 10;
                        query.Execute();
                        dt.WriteXml(Server.MapPath(Settings.XmlPath + "manset.xml"), System.Data.XmlWriteMode.WriteSchema);
                    }
                }
            }
            catch (Exception ex)
            {
                pageNumberLiteral.Text = ex.Message;
            }
        }

        void GetDataPaging()
        {
            using (BAYMYO.UI.Web.DataPagers data = new BAYMYO.UI.Web.DataPagers(dataGrid1, "manset", "guncellemetarihi desc", "1=1", 20))
            {
                if (!string.IsNullOrEmpty(Request.QueryString["mdl"]))
                {
                    ddlKategoriler.SelectedValue = Request.QueryString["mdl"];
                    data.Where += " and modulid=?modulid";
                    data.Parameters.Add("modulid", Request.QueryString["mdl"], BAYMYO.MultiSQLClient.MSqlDbType.VarChar);
                }
                if (!string.IsNullOrEmpty(Request.QueryString["y"]))
                {
                    data.Where += " and yerlesim=?yerlesim";
                    data.Parameters.Add("yerlesim", Request.QueryString["y"], BAYMYO.MultiSQLClient.MSqlDbType.Byte);
                }
                data.PageNumberTargetControl = pageNumberLiteral;
                data.Binding();
                totalCount = string.Format("Toplam <b>{0}</b> haber.", data.TotalDataCount);
            }
        }

        protected void btnSaveChanges_Click(object sender, EventArgs e)
        {
            try
            {
                if (ddlIslemler.SelectedIndex > 0 & Core.IsUserAdmin)
                {
                    bool chkState = false;
                    string columnName = "";
                    switch (ddlIslemler.SelectedIndex)
                    {
                        case 1:
                            columnName = "aktif";
                            chkState = true;
                            break;
                        case 2:
                            columnName = "aktif";
                            break;
                    }
                    switch (ddlIslemler.SelectedIndex)
                    {
                        case 1:
                        case 2:
                            foreach (GridViewRow item in dataGrid1.Rows)
                                if (((CheckBox)item.Cells[0].FindControl("chkSelected")).Checked)
                                    Core.Update("manset", columnName, BAYMYO.UI.Converts.NullToString(dataGrid1.DataKeys[item.RowIndex][0]), chkState);
                            break;
                    }
                    jSonData.CreateData("mansetler");
                    GetDataPaging();
                    CreateXml();
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
                Response.Redirect(Settings.PanelPath + "?go=" + Request.QueryString["go"] + "&mdl=" + ddlKategoriler.SelectedValue + "&y=" + Request.QueryString["y"], false);
            else
                Response.Redirect(Settings.PanelPath + "?go=" + Request.QueryString["go"], false);
        }
    }
}