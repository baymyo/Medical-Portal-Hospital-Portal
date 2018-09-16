using System;

namespace baymyoStatic.common.ascx
{
    public partial class baglantiliste : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.Page.IsPostBack)
                GetDataPaging();
        }

        private void GetDataPaging()
        {
            using (BAYMYO.UI.Web.DataPagers data = new BAYMYO.UI.Web.DataPagers(rptListe, "firma", "kayittarihi desc", "yoneticionay=1 and aktif=1"))
            {
                if (!string.IsNullOrEmpty(Request.QueryString["kid"]))
                {
                    data.Where += " and kategoriid=?kategoriid";
                    data.Parameters.Add("kategoriid", Request.QueryString["kid"], BAYMYO.MultiSQLClient.MSqlDbType.VarChar);
                }
                if (!string.IsNullOrEmpty(Request.QueryString["q"]))
                {
                    data.Where += " and baslik like ?baslik";
                    data.Parameters.Add("baslik", "%" + Request.QueryString["q"] + "%", BAYMYO.MultiSQLClient.MSqlDbType.VarChar);
                }
                if (!string.IsNullOrEmpty(Request.QueryString["city"]))
                {
                    data.Where += " and sehir like ?city";
                    data.Parameters.Add("city", "%" + Request.QueryString["city"] + "%", BAYMYO.MultiSQLClient.MSqlDbType.VarChar);
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