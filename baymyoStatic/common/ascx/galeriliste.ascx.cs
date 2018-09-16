using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace baymyoStatic.common.ascx
{
    public partial class galeriliste : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.Page.IsPostBack)
                GetDataPaging();
        }

        private void GetDataPaging()
        {
            using (BAYMYO.UI.Web.DataPagers data = new BAYMYO.UI.Web.DataPagers(
                "select g.id as resimid,g.resimurl,a.id,a.adi,a.kayittarihi from album a inner join galeri g on g.albumid=a.id and g.kapak=1 where a.yoneticionay=1 and a.aktif=1"
                , "select count(a.id) from album a inner join galeri g on g.albumid=a.id and g.kapak=1 where a.yoneticionay=1 and a.aktif=1"))
            {
                if (!string.IsNullOrEmpty(Request.QueryString["kid"]))
                {
                    data.CustomDataQuery += " and a.kategoriid like ?kategoriid";
                    data.CustomDataCountQuery += " and a.kategoriid like ?kategoriid";
                    data.Parameters.Add("kategoriid", Request.QueryString["kid"] + "%", BAYMYO.MultiSQLClient.MSqlDbType.VarChar);
                }
                if (!string.IsNullOrEmpty(Request.QueryString["q"]))
                {
                    data.CustomDataQuery += " and a.adi like ?adi ";
                    data.CustomDataCountQuery += " and a.adi like ?adi ";
                    data.Parameters.Add("adi", "%" + Request.QueryString["q"] + "%", BAYMYO.MultiSQLClient.MSqlDbType.NVarChar);
                }
                if (!string.IsNullOrEmpty(Request.QueryString["t"]))
                {
                    data.CustomDataQuery += " and a.etiket like ?etiket";
                    data.CustomDataCountQuery += " and a.etiket like ?etiket";
                    data.Parameters.Add("etiket", "%" + Request.QueryString["t"].Replace('-', '_').Replace('ğ', '_').Replace('ş', '_').Replace('ç', '_').Replace('ü', '_').Replace('ö', '_').Replace('ı', '_') + "%", BAYMYO.MultiSQLClient.MSqlDbType.VarChar);
                }

                data.CustomDataQuery += " order by a.guncellemetarihi desc";
                data.ViewDataCount = 39;
                data.DataTargetControl = rptListe;
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