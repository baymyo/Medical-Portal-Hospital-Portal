using System;

namespace baymyoStatic.common.ascx
{
    public partial class makaleliste : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.Page.IsPostBack)
                GetDataPaging();
        }

        public string totalCount = string.Empty;
        private void GetDataPaging()
        {
            //new BAYMYO.UI.Web.DataPagers(rptListe, "Makale", "kayittarihi desc", "aktif=1"))
            using (BAYMYO.UI.Web.DataPagers data = new BAYMYO.UI.Web.DataPagers(
                "select m.id, m.hesapid,p.url, m.kategoriid, m.resimurl, m.baslik, m.ozet, m.kayittarihi, m.aktif, h.adi, h.soyadi from makale m inner join hesap h on h.id=m.hesapid inner join profil p on p.id=h.id ",
                "select count(m.id) from makale m inner join hesap h on h.id=m.hesapid inner join profil p on p.id=h.id"))
            {
                data.CustomDataQuery += " where m.yoneticionay=1 and m.aktif=1";
                data.CustomDataCountQuery += " where m.yoneticionay=1 and m.aktif=1";
                if (!string.IsNullOrEmpty(Request.QueryString["hspid"]))
                {
                    data.CustomDataQuery += " and m.hesapid=?hesapid";
                    data.CustomDataCountQuery += " and m.hesapid=?hesapid";
                    data.Parameters.Add("hesapid", BAYMYO.UI.Converts.NullToGuidString(Request.QueryString["hspid"]), BAYMYO.MultiSQLClient.MSqlDbType.VarChar);
                }
                if (!string.IsNullOrEmpty(Request.QueryString["url"]))
                {
                    data.CustomDataQuery += " and p.url=?url";
                    data.CustomDataCountQuery += " and p.url=?url";
                    data.Parameters.Add("url", Request.QueryString["url"], BAYMYO.MultiSQLClient.MSqlDbType.VarChar);
                }
                if (!string.IsNullOrEmpty(Request.QueryString["kid"]))
                {
                    data.CustomDataQuery += " and m.kategoriid Like ?kategoriid";
                    data.CustomDataCountQuery += " and m.kategoriid Like ?kategoriid";
                    data.Parameters.Add("kategoriid", Request.QueryString["kid"] + "%", BAYMYO.MultiSQLClient.MSqlDbType.VarChar);
                }
                if (!string.IsNullOrEmpty(Request.QueryString["q"]))
                {
                    data.CustomDataQuery += " and m.baslik like ?baslik";
                    data.CustomDataCountQuery += " and m.baslik like ?baslik";
                    data.Parameters.Add("baslik", "%" + Request.QueryString["q"] + "%", BAYMYO.MultiSQLClient.MSqlDbType.VarChar);
                }
                if (!string.IsNullOrEmpty(Request.QueryString["t"]))
                {
                    data.CustomDataQuery += " and m.etiket Like ?etiket";
                    data.CustomDataCountQuery += " and m.etiket Like ?etiket";
                    data.Parameters.Add("etiket", "%" + Request.QueryString["t"].Replace('-', '_').Replace('ğ', '_').Replace('ş', '_').Replace('ç', '_').Replace('ü', '_').Replace('ö', '_').Replace('ı', '_') + "%", BAYMYO.MultiSQLClient.MSqlDbType.VarChar);
                }
                //<%#TOP%> Limitler arası süzme yapılmasını sağlar...
                data.CustomDataQuery += " order by m.guncellemetarihi desc";
                data.ViewDataCount = 16;
                data.DataTargetControl = dataGrid1;
                data.PageNumberTargetControl = pageNumberLiteral;
                data.Binding();
                totalCount = string.Format("Toplam <b>{0}</b> makale.", data.TotalDataCount);
                if (data.TotalDataCount < 1)
                {
                    pageNumberLiteral.Visible = true;
                    pageNumberLiteral.Text = MessageBox.IsNotViews();
                }
            }
        }
    }
}