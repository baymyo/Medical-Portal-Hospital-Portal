using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace baymyoStatic.common.ascx
{
    public partial class galerigoster : System.Web.UI.UserControl
    {
        public string SayfaURL = "", Etiketler = "";
        public Album AlbumBilgi
        {
            get { return (Cache["AlbumBilgi"] == null) ? new Album() : Cache["AlbumBilgi"] as Album; }
            set { Cache["AlbumBilgi"] = value; }
        }
        public Kategori KategoriBilgi
        {
            get { return (Cache["KategoriBilgi"] == null) ? new Kategori() : Cache["KategoriBilgi"] as Kategori; }
            set { Cache["KategoriBilgi"] = value; }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                //Album Bilgisi
                if (!BAYMYO.UI.Converts.NullToString(AlbumBilgi.ID).Equals(BAYMYO.UI.Converts.NullToInt64(Request.QueryString["raid"])))
                {
                    AlbumBilgi = AlbumMethods.GetAlbum(BAYMYO.UI.Converts.NullToInt64(Request.QueryString["raid"]));
                    Core.ViewCounter("galeri", AlbumBilgi.ID);
                }
                if (AlbumBilgi != null)
                {
                    this.Page.Title = AlbumBilgi.Adi;// +" | " + Settings.Site.Title;
                    this.Page.MetaDescription = AlbumBilgi.Adi + " fotoğraf galerisi için tıklayın.";
                    this.Page.MetaKeywords = AlbumBilgi.Etiket;
                    Etiketler = AlbumBilgi.Etiket;
                    if (string.IsNullOrEmpty(Etiketler))
                        Etiketler = AlbumBilgi.Adi;
                    GetOtherData(AlbumBilgi);
                    switch (Core.CurrentUser.Tipi)
                    {
                        case AccountType.Admin:
                            View(AlbumBilgi);
                            break;
                        case AccountType.Private:
                        case AccountType.Doctor:
                        case AccountType.Editor:
                            if (!AlbumBilgi.Aktif & !BAYMYO.UI.Converts.NullToGuidString(Core.CurrentUser.ID).Equals(AlbumBilgi.HesapID))
                            {
                                pnlGaleri.Visible = false;
                                CommentControl1.Visible = false;
                                pageNumberLiteral.Text = MessageBox.Show(DialogResult.Warning, "Bu içerik gösterime kapatılmıştır. Kimler yayından kaldırabilir yazarı yada yöneticilerimiz tarafından yayından kaldırılabilir.");
                                return;
                            }
                            else
                                View(AlbumBilgi);
                            break;
                        case AccountType.None:
                        case AccountType.Standart:
                            if (!AlbumBilgi.Aktif)
                            {
                                pnlGaleri.Visible = false;
                                CommentControl1.Visible = false;
                                pageNumberLiteral.Text = MessageBox.Show(DialogResult.Warning, "Bu içerik gösterime kapatılmıştır. Kimler yayından kaldırabilir yazarı yada yöneticilerimiz tarafından yayından kaldırılabilir.");
                                return;
                            }
                            else
                                View(AlbumBilgi);
                            break;
                    }
                }
            }
            base.OnInit(e);
        }

        private void View(Album m)
        {
            CommentControl1.IsCommandActive = BAYMYO.UI.Converts.NullToGuidString(Core.CurrentUser.ID).Equals(m.HesapID);
            CommentControl1.Visible = m.Yorum;
            CommentControl1.ModulID = "galeri";
            CommentControl1.IcerikID = m.ID.ToString();
            //Icerik Bilgisi
            if (!BAYMYO.UI.Converts.NullToString(KategoriBilgi.ModulID).Equals("galeri") || !BAYMYO.UI.Converts.NullToString(KategoriBilgi.ID).Equals(m.KategoriID))
                KategoriBilgi = KategoriMethods.GetKategori("galeri", m.KategoriID);
            if (KategoriBilgi.Aktif)
                this.Page.Title += " | " + KategoriBilgi.Adi.ToUpper(); //+ " | " + Settings.Site.Title + " | " + m.Etiket;
            Etiketler = "";
            string etiketQuery = string.Empty;
            foreach (string item in m.Etiket.Split(','))
            {
                Etiketler += string.Format("<a href=\"{0}{1}\" target=\"_blank\"><strong>{2}</strong></a>, ", Settings.SiteUrl.Replace("http:", ""), Core.CreateLink("galerietiket", "", item), item.Trim());
                etiketQuery += " or i.yoneticionay=1 and i.aktif=1 and i.etiket like <%USTENTIRNAK%>%" + item.Trim() + ",%<%USTENTIRNAK%>";
            }
            if (!BAYMYO.UI.Converts.NullToString(Session["etiketSession"]).Equals(etiketQuery))
                Session["etiketSession"] = etiketQuery;

            using (BAYMYO.UI.Web.DataPagers data = new BAYMYO.UI.Web.DataPagers(rptListe, "galeri", "kayittarihi asc", "albumid=?albumid", 1))
            {
                data.Parameters.Add("albumid", m.ID, BAYMYO.MultiSQLClient.MSqlDbType.BigInt);
                data.PageNumberTargetControl = pageNumberLiteral;
                data.Binding();
                SayfaURL = (data.CurrentPage >= data.LastPage) ? "/galeriler" : "/?go=galerigoster&mdl=galeri&raid=" + m.ID + "&pno=" + data.NextPage + "#titleBox";
                if (data.TotalDataCount < 1)
                {
                    pageNumberLiteral.Visible = true;
                    pageNumberLiteral.Text = MessageBox.IsNotViews();
                }
                else
                    jobSet.Visible = (data.TotalPageCount > 1);
            }
        }

        public int Counter()
        {
            return GosterimMethods.Count("galeri", AlbumBilgi.ID.ToString());
        }

        private void GetOtherData(Album m)
        {
            using (BAYMYO.UI.Web.CustomSqlQuery data = new BAYMYO.UI.Web.CustomSqlQuery(rptOther,
                "select g.id as resimid,g.resimurl,a.id,a.adi,a.kayittarihi from album a inner join galeri g on g.albumid=a.id and g.kapak=1 where a.id<>?id and a.yoneticionay=1 and a.aktif=1 and a.kategoriid like ?kategoriid order by a.guncellemetarihi desc limit 10"))
            {
                data.Parameters.Add("id", m.ID + "%", BAYMYO.MultiSQLClient.MSqlDbType.BigInt);
                data.Parameters.Add("kategoriid", m.KategoriID + "%", BAYMYO.MultiSQLClient.MSqlDbType.VarChar);
                data.Execute();
            }
        }
    }
}