using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace baymyoStatic.panel.ascx
{
    public partial class galeriliste : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.Page.IsPostBack)
            {
                #region --- Kategoriler ---
                List<Kategori> kategoriler = KategoriMethods.GetMenu("galeri", false);
                kategoriler.Insert(0, new Kategori() { ID = "", Adi = "Kategoriye göre süz!" });
                ddlKategoriler.DataValueField = "id";
                ddlKategoriler.DataTextField = "adi";
                ddlKategoriler.DataSource = kategoriler;
                ddlKategoriler.DataBind();
                #endregion
                Core.GetProccesList("album", ddlIslemler);
                GetDataPaging();
            }
        }

        private void GetDataPaging()
        {
            using (BAYMYO.UI.Web.DataPagers data = new BAYMYO.UI.Web.DataPagers(
                "select g.id as resimid, g.resimurl,a.id,a.adi,a.kayittarihi,a.aktif from album a left join galeri g on g.albumid=a.id and g.kapak=1"
                , "select count(a.id) from album a left join galeri g on g.albumid=a.id and g.kapak=1"))
            {
                if (!string.IsNullOrEmpty(Request.QueryString["kid"]))
                {
                    ddlKategoriler.SelectedValue = Request.QueryString["kid"];
                    data.CustomDataQuery += " where kategoriid=?kategoriid";
                    data.CustomDataCountQuery += " where kategoriid=?kategoriid";
                    data.Parameters.Add("kategoriid", ddlKategoriler.SelectedValue, BAYMYO.MultiSQLClient.MSqlDbType.VarChar);
                }
                data.CustomDataQuery += " order by a.guncellemetarihi desc";
                data.ViewDataCount = 25;
                data.DataTargetControl = dataGrid1;
                data.PageNumberTargetControl = pageNumberLiteral;
                data.Binding();
                totalCount = string.Format("Toplam <b>{0}</b> albüm.", data.TotalDataCount);
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
                            chkState = true;
                            break;
                    }
                    if (ddlIslemler.SelectedIndex == 1 || ddlIslemler.SelectedIndex == 2)
                    {
                        foreach (GridViewRow item in dataGrid1.Rows)
                            if (((CheckBox)item.Cells[0].FindControl("chkSelected")).Checked)
                            {
                                Core.Update("album", "aktif", BAYMYO.UI.Converts.NullToGuidString(dataGrid1.DataKeys[item.RowIndex][0]), chkState);
                                break;
                            }
                        jSonData.CreateData("galeriler");
                        GetDataPaging();
                    }
                    else if (ddlIslemler.SelectedIndex == 3)
                    {
                        foreach (GridViewRow item in dataGrid1.Rows)
                            if (((CheckBox)item.Cells[0].FindControl("chkSelected")).Checked)
                                if (BAYMYO.UI.FileIO.RemoveDirectory(Server.MapPath(Settings.ImagesPath + "album/" + dataGrid1.DataKeys[item.RowIndex][0] + "/")))
                                {
                                    Core.RemoveForeignKey("galeri", dataGrid1.DataKeys[item.RowIndex][0].ToString());
                                    GaleriMethods.Delete(BAYMYO.UI.Converts.NullToInt64(dataGrid1.DataKeys[item.RowIndex][0]));
                                    AlbumMethods.Delete(BAYMYO.UI.Converts.NullToInt64(dataGrid1.DataKeys[item.RowIndex][0]));
                                }
                        jSonData.CreateData("galeriler");
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