using System;

namespace baymyoStatic.common.ascx
{
    public partial class doktor : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            this.Page.Title = "Doktorlar - " + Settings.Site.Title;
            //BAYMYO.UI.Web.Pages.AddMetaTag(this.Page, "Medicana,Arsuz," + Settings.Site.Keywords, "Arsuz şehrindeki tüm doktorları görüntülemek için tıkla.");
            if (!this.Page.IsPostBack)
                GetDataPaging();
        }

        void GetDataPaging()
        {
            using (BAYMYO.UI.Web.DataPagers data = new BAYMYO.UI.Web.DataPagers(
                "select p.url as url,p.resimurl as resimurl,p.sehir as sehir,mk.adi as meslek,h.id,h.adi as adi,h.soyadi as soyadi,h.kayittarihi as kayittarihi from hesap h inner join profil p on h.id=p.id inner join kategori mk on mk.modulid='meslek' and mk.id=p.meslek where h.tipi=2 and h.aktif=1 and h.aktivasyon=1 and p.resimurl<>'' "
                , "select count(h.id) As totalcount from hesap h inner join profil p on h.id=p.id inner join kategori mk on mk.modulid='meslek' and mk.id=p.meslek where h.aktif=1 and h.aktivasyon=1 and p.resimurl<>'' "))
            {
                if (!string.IsNullOrEmpty(Request.QueryString["q"]))
                {
                    data.CustomDataQuery += " and h.adi like ?adi";
                    data.CustomDataCountQuery += " and h.adi like ?adi";
                    data.Parameters.Add("adi", "%" + Request.QueryString["q"] + "%", BAYMYO.MultiSQLClient.MSqlDbType.VarChar);
                }
                if (!string.IsNullOrEmpty(Request.QueryString["city"]))
                {
                    data.CustomDataQuery += " and p.sehir=?city";
                    data.CustomDataCountQuery += " and p.sehir=?city";
                    data.Parameters.Add("city", Request.QueryString["city"].ToUpper().Trim(), BAYMYO.MultiSQLClient.MSqlDbType.VarChar);
                }
                if (!string.IsNullOrEmpty(Request.QueryString["job"]))
                {
                    data.CustomDataQuery += " and p.meslek=?job";
                    data.CustomDataCountQuery += " and p.meslek=?job";
                    data.Parameters.Add("job", Request.QueryString["job"].ToUpper().Trim(), BAYMYO.MultiSQLClient.MSqlDbType.VarChar);
                }

                //<%#TOP%> Limitler arası süzme yapılmasını sağlar...
                data.CustomDataQuery += " order by h.kayittarihi desc";
                data.DataTargetControl = rptListe;
                data.PageNumberTargetControl = pageNumberLiteral;
                data.ViewDataCount = 40;
                data.Binding();

                if (data.TotalDataCount < 1)
                {
                    pageNumberLiteral.Visible = true;
                    pageNumberLiteral.Text = MessageBox.IsNotViews();
                }
            }
        }
    }
}