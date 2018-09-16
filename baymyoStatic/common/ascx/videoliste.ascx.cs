using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace baymyoStatic.common.ascx
{
    public partial class videoliste : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.Page.IsPostBack)
                GetDataPaging();
        }

        private void GetDataPaging()
        {
            using (BAYMYO.UI.Web.DataPagers data = new BAYMYO.UI.Web.DataPagers(rptListe, "video", "guncellemetarihi desc", "1=1"))
            {
                if (!string.IsNullOrEmpty(Request.QueryString["kid"]))
                {
                    data.Where += " and kategoriid like ?kategoriid";
                    data.Parameters.Add("kategoriid", Request.QueryString["kid"] + "%", BAYMYO.MultiSQLClient.MSqlDbType.VarChar);
                }
                if (!string.IsNullOrEmpty(Request.QueryString["q"]))
                {
                    data.Where += " and baslik like ?baslik ";
                    data.Parameters.Add("baslik", "%" + Request.QueryString["q"] + "%", BAYMYO.MultiSQLClient.MSqlDbType.NVarChar);
                }
                if (!string.IsNullOrEmpty(Request.QueryString["t"]))
                {
                    data.Where += " and etiket like ?etiket";
                    data.Parameters.Add("etiket", "%" + Request.QueryString["t"].Replace('-', '_').Replace('ğ', '_').Replace('ş', '_').Replace('ç', '_').Replace('ü', '_').Replace('ö', '_').Replace('ı', '_') + "%", BAYMYO.MultiSQLClient.MSqlDbType.VarChar);
                }
                data.ViewDataCount = 39;
                data.PageNumberTargetControl = pageNumberLiteral;
                data.Binding();
                if (data.TotalDataCount < 1)
                {
                    pageNumberLiteral.Visible = true;
                    pageNumberLiteral.Text = MessageBox.IsNotViews();
                }
                else
                    jobSet.Visible = (data.TotalPageCount > 1);
            }
        }
    }
}