using System;
using System.Collections.Generic;
using System.Web;
using System.Linq;

namespace baymyoStatic
{
    public class jSonObject
    {
        string
            m_ID = string.Empty,
            m_KategoriID = string.Empty,
            m_KategoriAdi = string.Empty,
            m_Resim = string.Empty,
            m_KucukResim = string.Empty,
            m_Link = string.Empty,
            m_Baslik = string.Empty,
            m_Ozet = string.Empty,
            m_Tarih = string.Empty,
            m_Saat = string.Empty;

        public string ID
        {
            get { return m_ID; }
            set { m_ID = value; }
        }
        public string KategoriID
        {
            get { return m_KategoriID; }
            set { m_KategoriID = value; }
        }
        public string KategoriAdi
        {
            get { return m_KategoriAdi; }
            set { m_KategoriAdi = value; }
        }
        public string Resim
        {
            get { return m_Resim; }
            set { m_Resim = value; }
        }
        public string KucukResim
        {
            get { return m_KucukResim; }
            set { m_KucukResim = value; }
        }
        public string Link
        {
            get { return m_Link; }
            set { m_Link = value; }
        }
        public string Baslik
        {
            get { return m_Baslik; }
            set { m_Baslik = value; }
        }
        public string Ozet
        {
            get { return m_Ozet; }
            set { m_Ozet = value; }
        }
        public string Tarih
        {
            get { return m_Tarih; }
            set { m_Tarih = value; }
        }
        public string Saat
        {
            get { return m_Saat; }
            set { m_Saat = value; }
        }

        string
            m_Deger = "0",
            m_Degisim = "0";

        public string Deger
        {
            get { return m_Deger; }
            set { m_Deger = value; }
        }
        public string Degisim
        {
            get { return m_Degisim; }
            set { m_Degisim = value; }
        }

        int
            m_GosterimSayi = 0,
            m_YorumSayi = 0;

        public int GosterimSayi
        {
            get { return m_GosterimSayi; }
            set { m_GosterimSayi = value; }
        }
        public int YorumSayi
        {
            get { return m_YorumSayi; }
            set { m_YorumSayi = value; }
        }

        bool
            m_BaslikGoster,
            m_Video,
            m_Galeri,
            m_Yeni;

        public bool BaslikGoster
        {
            get { return m_BaslikGoster; }
            set { m_BaslikGoster = value; }
        }
        public bool Video
        {
            get { return m_Video; }
            set { m_Video = value; }
        }
        public bool Galeri
        {
            get { return m_Galeri; }
            set { m_Galeri = value; }
        }
        public bool Yeni
        {
            get { return m_Yeni; }
            set { m_Yeni = value; }
        }

        DateTime m_OrderDate;
        public DateTime OrderDate
        {
            get { return m_OrderDate; }
            set { m_OrderDate = value; }
        }

        public jSonObject()
        {
        }
    }

    public class jSonData
    {
        private static DateTime m_ContentDate;

        public static List<jSonObject> GetKategori(string modulName)
        {
            List<jSonObject> rv = new List<jSonObject>();
            List<Kategori> items = KategoriMethods.GetMenu(modulName);
            foreach (Kategori item in items)
                rv.Add(new jSonObject { ID = item.ID, Link = Core.CreateLink(modulName + "kategori", item.ID, item.Adi), Baslik = item.Adi, Resim = item.Renk });
            items.Clear();
            return rv;
        }
        public static List<jSonObject> GetKategori(string modulName, byte menuType)
        {
            List<jSonObject> rv = new List<jSonObject>();
            List<Kategori> items = KategoriMethods.GetMenu(modulName, menuType);
            foreach (Kategori item in items)
                rv.Add(new jSonObject { ID = item.ID, Link = Core.CreateLink(modulName + "kategori", item.ID, item.Adi), Baslik = item.Adi, Resim = item.Renk });
            items.Clear();
            return rv;
        }

        public static List<jSonObject> GetHaberler(string q, byte type, byte limit)
        {
            List<jSonObject> rv = new List<jSonObject>();
            if (limit > 20)
                return rv;
            string query = "select h.id,h.resimurl,h.baslik,h.ozet,h.kayittarihi,h.video,h.galeri,h.kategoriid,k.adi,k.renk from haber h inner join kategori k on k.modulid='haber' and k.id=h.kategoriid where h.resimurl<>'' and h.kayittarihi < ?yayintarihi and h.yoneticionay=1 and h.aktif=1 ";
            switch (type)
            {
                case 1:
                    query += " and h.anasayfa=1 ";
                    break;
            }
            if (string.IsNullOrWhiteSpace(q))
                query += " and h.kategoriid NOT LIKE '" + Settings.Site.Category1.ID + "%' and h.kategoriid NOT LIKE '" + Settings.Site.Category2.ID + "%' and h.kategoriid NOT LIKE '" + Settings.Site.Category3.ID + "%' and h.kategoriid NOT LIKE '" + Settings.Site.Category4.ID + "%' ";
            else if (type == 9 & q.Length >= 4 & q.Length <= 10)
                query += " and h.baslik like '%" + q + "%' ";
            else if (q.Length >= 3 & type != 9)
                query += " and h.kategoriid like '" + q + "%' ";
            query += " order by h.kayittarihi desc limit " + limit;

            using (System.Data.DataTable table = new System.Data.DataTable("haber"))
            {
                using (BAYMYO.UI.Web.CustomSqlQuery data = new BAYMYO.UI.Web.CustomSqlQuery(table, query))
                {
                    data.Parameters.Add("yayintarihi", DateTime.Now, BAYMYO.MultiSQLClient.MSqlDbType.DateTime);
                    data.Execute();
                    foreach (System.Data.DataRow item in table.Rows)
                    {
                        m_ContentDate = BAYMYO.UI.Converts.NullToDateTime(item[4]);
                        rv.Add(new jSonObject { ID = item[0].ToString(), KategoriID = item[7].ToString(), KategoriAdi = item[8].ToString().ToUpper(), Deger = item[9].ToString(), Link = Core.CreateLink("haber", item[0], item[2]), Resim = item[1].ToString(), Baslik = item[2].ToString(), Ozet = item[3].ToString(), Tarih = m_ContentDate.ToLongDateString(), Saat = m_ContentDate.ToShortTimeString(), Video = !(item[5].ToString().Equals("0")), Galeri = !(item[6].ToString().Equals("0")), Yeni = (m_ContentDate >= DateTime.Now.AddHours(-5)) });
                    }
                }
            }
            query = null;
            return rv;
        }
        public static List<jSonObject> GetHaberler(DateTime tarih1, DateTime tarih2, byte limit)
        {
            //GOSTERİM SAYISINA GORE GETİRİR
            List<jSonObject> rv = new List<jSonObject>();
            if (limit > 20)
                return rv;
            using (System.Data.DataTable table = new System.Data.DataTable("haber"))
            {
                using (BAYMYO.UI.Web.CustomSqlQuery data = new BAYMYO.UI.Web.CustomSqlQuery(table, "select h.id,h.resimurl,h.baslik,h.ozet,h.kayittarihi,h.video,h.galeri,COUNT(g.icerikid) cnt from haber h inner join gosterim g on g.modulid='haber' and g.icerikid=h.id where h.yoneticionay=1 and h.aktif=1 and h.resimurl<>'' and h.kayittarihi < ?yayintarihi and h.kayittarihi between ?tarih1 and ?tarih2 group by h.id,h.resimurl,h.baslik,h.ozet,h.kayittarihi,h.video,h.galeri order by cnt desc limit " + limit))
                {
                    data.Parameters.Add("yayintarihi", DateTime.Now, BAYMYO.MultiSQLClient.MSqlDbType.DateTime);
                    data.Parameters.Add("tarih1", tarih1, BAYMYO.MultiSQLClient.MSqlDbType.DateTime);
                    data.Parameters.Add("tarih2", tarih2, BAYMYO.MultiSQLClient.MSqlDbType.DateTime);
                    data.Execute();
                    foreach (System.Data.DataRow item in table.Rows)
                    {
                        m_ContentDate = BAYMYO.UI.Converts.NullToDateTime(item[4]);
                        rv.Add(new jSonObject { ID = item[0].ToString(), Link = Core.CreateLink("haber", item[0], item[2]), Resim = item[1].ToString(), Baslik = item[2].ToString(), Ozet = item[3].ToString(), Tarih = m_ContentDate.ToLongDateString(), Saat = m_ContentDate.ToShortTimeString(), Video = !(item[5].ToString().Equals("0")), Galeri = !(item[6].ToString().Equals("0")), Yeni = (m_ContentDate >= DateTime.Now.AddHours(-5)) });
                    }
                }
            }
            return rv;
        }

        public static List<jSonObject> GetDoktorlar(byte limit)
        {
            List<jSonObject> rv = new List<jSonObject>();
            if (limit > 40)
                return rv;
            using (HesapCollection hesaplar = HesapMethods.GetSelect("select * from hesap h where h.tipi=2 and h.aktivasyon=1 and h.aktif=1 order by h.tipi asc limit " + limit, true))
            {
                foreach (Hesap h in hesaplar)
                    rv.Add(new jSonObject { ID = h.ID.ToString(), Resim = h.ProfilObject.ResimUrl, Link = h.ProfilObject.Url, Baslik = KategoriMethods.GetKategori("meslek", h.ProfilObject.Meslek).Adi + " " + h.Adi + " " + h.Soyadi, Tarih = h.KayitTarihi.ToShortDateString() });
                hesaplar.Clear();
            }
            return rv;
        }
        public static List<jSonObject> GetYazarlar(byte limit)
        {
            List<jSonObject> rv = new List<jSonObject>();
            if (limit > 40)
                return rv;
            using (HesapCollection hesaplar = HesapMethods.GetSelect("select * from hesap h where h.tipi in(1,2) and h.aktivasyon=1 and h.aktif=1 limit " + limit, true))
            {
                foreach (Hesap h in hesaplar)
                    using (Makale m = MakaleMethods.GetMakale(h.ID))
                    {
                        if (m.ID > 0)
                            rv.Add(new jSonObject { ID = m.ID.ToString(), Resim = h.ProfilObject.ResimUrl, Link = Core.CreateLink("makale", m.ID, m.Baslik), Baslik = h.Adi + " " + h.Soyadi, Ozet = m.Baslik, Tarih = m.GuncellemeTarihi.ToLongDateString(), OrderDate = m.GuncellemeTarihi });
                    }
                hesaplar.Clear();
            }
            rv = rv.OrderByDescending(x => x.OrderDate).ToList<jSonObject>();
            return rv;
        }
        public static List<jSonObject> GetYazarlar(byte type, byte limit)
        {
            List<jSonObject> rv = new List<jSonObject>();
            if (limit > 20)
                return rv;
            using (HesapCollection hesaplar = HesapMethods.GetSelect("select * from hesap h where h.tipi=" + type + " and h.aktivasyon=1 and h.aktif=1 limit " + limit, true))
            {
                foreach (Hesap h in hesaplar)
                    using (Makale m = MakaleMethods.GetMakale(h.ID))
                    {
                        if (m.ID > 0)
                            rv.Add(new jSonObject { ID = m.ID.ToString(), Resim = h.ProfilObject.ResimUrl, Link = Core.CreateLink("makale", m.ID, m.Baslik), Baslik = h.Adi + " " + h.Soyadi, Ozet = m.Baslik, Tarih = m.GuncellemeTarihi.ToLongDateString(), OrderDate = m.GuncellemeTarihi });
                    }
                hesaplar.Clear();
            }
            rv = rv.OrderByDescending(x => x.OrderDate).ToList<jSonObject>();
            return rv;
        }

        public static List<jSonObject> GetBaglantilar(byte limit)
        {
            List<jSonObject> rv = new List<jSonObject>();
            if (limit > 10)
                return rv;
            using (System.Data.DataTable data = new System.Data.DataTable("firma"))
            {
                using (BAYMYO.UI.Web.CustomSqlQuery query = new BAYMYO.UI.Web.CustomSqlQuery(data, "select f.id,f.resimurl,f.adi,f.adres,f.telefon1,f.guncellemetarihi from firma f where f.aktif=1 order by f.guncellemetarihi desc limit " + limit))
                {
                    query.Execute();
                    foreach (System.Data.DataRow item in data.Rows)
                        rv.Add(new jSonObject { ID = item[0].ToString(), Resim = item[1].ToString(), Link = Core.CreateLink("firma", item[0].ToString(), item[2].ToString()), Baslik = item[2].ToString(), Ozet = item[3].ToString() + " Tel: <b class=\"phone\">" + item[4].ToString() + "</b>", Tarih = BAYMYO.UI.Converts.NullToDateTime(item[5]).ToLongDateString() });
                }
            }
            return rv;
        }

        public static List<jSonObject> GetSeriIlanlar(byte limit)
        {
            List<jSonObject> rv = new List<jSonObject>();
            if (limit > 10)
                return rv;
            using (System.Data.DataTable data = new System.Data.DataTable("seriilan"))
            {
                using (BAYMYO.UI.Web.CustomSqlQuery query = new BAYMYO.UI.Web.CustomSqlQuery(data, "select s.id,s.resimurl,s.baslik,s.fiyat,s.guncellemetarihi from seriilan s where s.aktif=1 order by s.guncellemetarihi desc limit " + limit))
                {
                    query.Execute();
                    foreach (System.Data.DataRow item in data.Rows)
                        rv.Add(new jSonObject { ID = item[0].ToString(), Resim = item[1].ToString(), Link = Core.CreateLink("seriilan", item[0].ToString(), item[2].ToString()), Baslik = item[2].ToString(), Deger = string.Format("{0:#0.00}", item[3]), Tarih = BAYMYO.UI.Converts.NullToDateTime(item[4]).ToLongDateString() });
                }
            }
            return rv;
        }

        public static List<jSonObject> GetSayfalar(byte yerlesim, byte limit)
        {
            List<jSonObject> rv = new List<jSonObject>();
            if (limit > 10)
                return rv;
            using (SayfaCollection m = SayfaMethods.GetSelect(yerlesim, limit))
            {
                foreach (Sayfa item in m)
                    rv.Add(new jSonObject { Link = Core.CreateLink("sayfa", item.ID, item.Baslik), Baslik = item.Baslik });
            }
            return rv;
        }

        public static List<jSonObject> GetVideolar(string q, byte type, byte limit)
        {
            List<jSonObject> rv = new List<jSonObject>();
            if (limit > 20)
                return rv;
            using (System.Data.DataTable data = new System.Data.DataTable("video"))
            {
                string mq = "select COUNT(m.icerikid) from manset m where m.icerikid=v.id and m.kayittarihi < ?yayintarihi and m.resimbuyuk<>'' and m.modulid='video' and m.aktif=1 order by m.sira asc, m.kayittarihi desc limit 27";
                using (BAYMYO.UI.Web.CustomSqlQuery query = new BAYMYO.UI.Web.CustomSqlQuery())
                {
                    query.TargetControl = data;
                    query.CommandText = "select v.id,v.resimurl,v.baslik,v.guncellemetarihi from video v where (" + mq + ")<=0 and v.yoneticionay=1 and v.aktif=1 ";

                    if (type == 9 & q.Length >= 3 & q.Length <= 10)
                        query.CommandText += " and v.baslik like '%" + q + "%' ";
                    else if (q.Length >= 3 & type != 9)
                        query.CommandText += " and v.kategoriid like '" + q + "%' ";
                    query.CommandText += "order by v.kayittarihi desc limit " + limit;

                    query.Parameters.Add("yayintarihi", DateTime.Now, BAYMYO.MultiSQLClient.MSqlDbType.DateTime);
                    query.Execute();
                    foreach (System.Data.DataRow item in data.Rows)
                    {
                        m_ContentDate = BAYMYO.UI.Converts.NullToDateTime(item[3]);
                        rv.Add(new jSonObject { ID = item[0].ToString(), Resim = Settings.ImagesPath + "video/" + item[1].ToString(), Link = Core.CreateLink("video", item[0].ToString(), item[2].ToString()), Baslik = item[2].ToString(), Tarih = m_ContentDate.ToLongDateString(), Saat = m_ContentDate.ToShortTimeString(), Yeni = (m_ContentDate >= DateTime.Now.AddHours(-5)) });
                    }
                }
            }
            return rv;
        }

        public static List<jSonObject> GetGeleriler(string q, byte type, byte limit)
        {
            List<jSonObject> rv = new List<jSonObject>();
            if (limit > 20)
                return rv;
            using (System.Data.DataTable data = new System.Data.DataTable("album"))
            {
                string mq = "select COUNT(m.icerikid) from manset m where m.icerikid=a.id and m.kayittarihi < ?yayintarihi and m.resimbuyuk<>'' and m.modulid='album' and m.aktif=1 order by m.sira asc, m.kayittarihi desc limit 27";
                using (BAYMYO.UI.Web.CustomSqlQuery query = new BAYMYO.UI.Web.CustomSqlQuery())
                {
                    query.TargetControl = data;
                    query.CommandText = "select a.id,g.resimurl,a.adi,a.kayittarihi from album a inner join galeri g on g.albumid=a.id and g.kapak=1 where (" + mq + ") <= 0 and a.yoneticionay=1 and a.aktif=1 ";

                    if (type == 9 & q.Length >= 3 & q.Length <= 10)
                        query.CommandText += " and a.adi like '%" + q + "%' ";
                    else if (q.Length >= 3 & type != 9)
                        query.CommandText += " and a.kategoriid like '" + q + "%' ";
                    query.CommandText += "order by a.kayittarihi desc limit " + limit;

                    query.Parameters.Add("yayintarihi", DateTime.Now, BAYMYO.MultiSQLClient.MSqlDbType.DateTime);
                    query.Execute();
                    foreach (System.Data.DataRow item in data.Rows)
                    {
                        m_ContentDate = BAYMYO.UI.Converts.NullToDateTime(item[3]);
                        rv.Add(new jSonObject { ID = item[0].ToString(), Resim = Settings.ImagesPath + "album/" + item[0].ToString() + "/" + item[1].ToString(), Link = Core.CreateLink("galeri", item[0].ToString(), item[2].ToString()), Baslik = item[2].ToString(), Tarih = m_ContentDate.ToLongDateString(), Saat = m_ContentDate.ToShortTimeString(), Yeni = (m_ContentDate >= DateTime.Now.AddHours(-5)) });
                    }
                }
            }
            return rv;
        }

        public static List<jSonObject> GetPiyasalar()
        {
            List<jSonObject> rv = new List<jSonObject>();
            using (System.Data.DataTable data = Core.GetPiyasaBilgileri())
            {
                foreach (System.Data.DataRow item in data.Rows)
                    rv.Add(new jSonObject { Resim = Core.StateArrow(item["status"], 10), Baslik = item["code"].ToString(), Deger = item["lastValue"].ToString(), Degisim = item["lastChange"].ToString(), Tarih = item["date"].ToString() });
                rv.Insert(1, rv[5]);
                rv.RemoveAt(6);
            }
            return rv;
        }

        public static List<jSonObject> GetHavaDurumlari()
        {
            List<jSonObject> rv = new List<jSonObject>();
            using (System.Data.DataTable data = Core.GetHavaDurumlari())
            {
                foreach (System.Data.DataRow item in data.Rows)
                    rv.Add(new jSonObject { Resim = item["resim"].ToString(), Baslik = item["sehir"].ToString(), Ozet = item["bilgi"].ToString(), Deger = item["endusuk"].ToString(), Degisim = item["enyuksek"].ToString(), Tarih = BAYMYO.UI.Converts.NullToDateTime(item["tarih"]).ToString("dd.MM.yyyy ddddd").Replace('.', '/') });
            }
            return rv;
        }
        public static List<jSonObject> GetHavaDurumlariLive(string sehir)
        {
            List<jSonObject> rv = new List<jSonObject>();
            using (System.Data.DataTable dt = Core.CreateHavaDurumuTable())
            {
                System.Data.DataRow dr = null;
                int index = 0, count = 0, orjinalIndex = 0;
                string tempContent = BAYMYO.UI.Web.Pages.HtmlRead(string.Format("http://www.mgm.gov.tr/tahmin/il-ve-ilceler.aspx?m={0}", sehir.ToUpperInvariant().ToLowerInvariant().Replace("İçel", "Mersin")).Replace("Kahramanmaraş", "K.MARAS").Replace("Afyon", "Afyonkarahisar").ToUpperInvariant(), "text/*", "client", System.Text.Encoding.UTF8);
                string tempDeyim = "(.*?)Trh\">(?<tarih>.+?)</th>(.*?)";
                System.Text.RegularExpressions.Match matchTemp = System.Text.RegularExpressions.Regex.Match(tempContent, tempDeyim, System.Text.RegularExpressions.RegexOptions.Multiline);
                DateTime date = DateTime.Now;
                while (matchTemp.Success)
                {
                    dr = dt.NewRow();
                    dr["tarih"] = date.AddDays(index).ToString("dd/MM/yyyy");
                    dr["tarihgelen"] = matchTemp.Groups["tarih"].Value;
                    dr["sehir"] = sehir.Replace("İçel", "Mersin");
                    dt.Rows.Add(dr);
                    matchTemp = matchTemp.NextMatch();
                    index++;
                }
                count = dt.Rows.Count;
                tempDeyim = "(.*?)minS\">(?<endusuk>[0-9\\-]{1,3})</td>(.*?)";
                matchTemp = System.Text.RegularExpressions.Regex.Match(tempContent, tempDeyim, System.Text.RegularExpressions.RegexOptions.Multiline);
                if (count > 0)
                    orjinalIndex = count - 5;
                index = orjinalIndex;
                while (matchTemp.Success)
                {
                    if (index < count)
                    {
                        dt.Rows[index]["endusuk"] = BAYMYO.UI.Converts.NullToInt16(matchTemp.Groups["endusuk"].Value);
                        matchTemp = matchTemp.NextMatch();
                    }
                    else
                        break;
                    index++;
                }
                tempDeyim = "(.*?)maxS\">(?<enyuksek>[0-9\\-]{1,3})</td>(.*?)";
                matchTemp = System.Text.RegularExpressions.Regex.Match(tempContent, tempDeyim, System.Text.RegularExpressions.RegexOptions.Multiline);
                index = orjinalIndex;
                while (matchTemp.Success)
                {
                    if (index < count)
                    {
                        dt.Rows[index]["enyuksek"] = BAYMYO.UI.Converts.NullToInt16(matchTemp.Groups["enyuksek"].Value);
                        matchTemp = matchTemp.NextMatch();
                    }
                    else
                        break;
                    index++;
                }
                tempDeyim = "(.*?)title=\"(?<bilgi>.+?)\"(.*?)src=\"(?<resim>.+?)\"(.*?)</td>";
                matchTemp = System.Text.RegularExpressions.Regex.Match(tempContent, tempDeyim, System.Text.RegularExpressions.RegexOptions.Multiline);
                index = orjinalIndex;
                int lastIndex = 0, textLenght = 0;
                string resimUrl = string.Empty;
                while (matchTemp.Success)
                {
                    if (index < count)
                    {
                        dt.Rows[index]["bilgi"] = matchTemp.Groups["bilgi"].Value;
                        resimUrl = matchTemp.Groups["resim"].Value.Replace(".gif", "").Replace(".jpg", "").Replace(".png", "");
                        lastIndex = resimUrl.LastIndexOf('/') + 1;
                        textLenght = resimUrl.Length - lastIndex;
                        dt.Rows[index]["resim"] = string.Format("{0}{1}", Settings.ImagesPath + "havadurumu/", resimUrl.Substring(lastIndex, textLenght) + ".png");
                        matchTemp = matchTemp.NextMatch();
                        lastIndex = 0;
                        textLenght = 0;
                    }
                    else
                        break;
                    index++;
                }
                DateTime currentDate;
                foreach (System.Data.DataRow item in dt.Rows)
                {
                    currentDate = BAYMYO.UI.Converts.NullToDateTime(item["tarih"]);
                    rv.Add(new jSonObject { Resim = item["resim"].ToString(), Baslik = currentDate.ToString("ddddd"), Ozet = item["bilgi"].ToString(), Deger = item["endusuk"].ToString(), Degisim = item["enyuksek"].ToString(), Tarih = currentDate.ToString("dd MMMMM yyyy"), Saat = currentDate.ToShortTimeString(), Yeni = ((currentDate - DateTime.Now).Days == 0) });
                }
            }
            return rv;
        }

        public static List<jSonObject> GetGazeteler()
        {
            List<jSonObject> rv = new List<jSonObject>();
            rv.Add(new jSonObject { Resim = "aksam", Baslik = "Akşam" });
            rv.Add(new jSonObject { Resim = "birgun", Baslik = "Birgün" });
            rv.Add(new jSonObject { Resim = "bugun", Baslik = "Bugün" });
            rv.Add(new jSonObject { Resim = "cumhuriyet", Baslik = "Cumhuriyet" });
            rv.Add(new jSonObject { Resim = "dunya", Baslik = "Dünya" });
            rv.Add(new jSonObject { Resim = "fanatik", Baslik = "Fanatik" });
            rv.Add(new jSonObject { Resim = "fotomac", Baslik = "Fotomaç" });
            rv.Add(new jSonObject { Resim = "gunes", Baslik = "Güneş" });
            rv.Add(new jSonObject { Resim = "haberturk", Baslik = "Habertürk" });
            rv.Add(new jSonObject { Resim = "hurriyet", Baslik = "Hürriyet" });
            rv.Add(new jSonObject { Resim = "milliyet", Baslik = "Milliyet" });
            rv.Add(new jSonObject { Resim = "milligazete", Baslik = "Milli Gazete" });
            rv.Add(new jSonObject { Resim = "posta", Baslik = "Posta" });
            rv.Add(new jSonObject { Resim = "sabah", Baslik = "Sabah" });
            rv.Add(new jSonObject { Resim = "sozcu", Baslik = "Sözcü" });
            rv.Add(new jSonObject { Resim = "star", Baslik = "Star" });
            rv.Add(new jSonObject { Resim = "takvim", Baslik = "Takvim" });
            rv.Add(new jSonObject { Resim = "taraf", Baslik = "Taraf" });
            rv.Add(new jSonObject { Resim = "turkiye", Baslik = "Türkiye" });
            rv.Add(new jSonObject { Resim = "vakit", Baslik = "Vakit" });
            rv.Add(new jSonObject { Resim = "vatan", Baslik = "Vatan" });
            rv.Add(new jSonObject { Resim = "yeniasya", Baslik = "Yeni Asya" });
            rv.Add(new jSonObject { Resim = "yenisafak", Baslik = "Yeni Şafak" });
            rv.Add(new jSonObject { Resim = "zaman", Baslik = "Zaman" });
            return rv;
        }

        public static void CreateData(string jsonName)
        {
            try
            {
                switch (jsonName)
                {
                    case "mansetler":
                        jSonData.CreateFile("manset1", MansetMethods.GetSelect(1, 10));
                        break;
                    case "kategoriler":
                        jSonData.CreateFile("haberkategoriust", GetKategori("haber", 1));
                        jSonData.CreateFile("haberkategorialt", GetKategori("haber", 2));
                        jSonData.CreateFile("galerikategori", GetKategori("galeri"));
                        jSonData.CreateFile("videokategori", GetKategori("video"));
                        jSonData.CreateFile("makalekategori", GetKategori("makale"));
                        jSonData.CreateFile("meslekkategori", GetKategori("meslek"));
                        jSonData.CreateFile("seriilankategori", GetKategori("seriilan"));
                        jSonData.CreateFile("firmakategori", GetKategori("firma"));
                        KategoriMethods.Save();
                        break;
                    case "haberler":
                        // Sondakika Haberleri
                        List<jSonObject> last = GetHaberler("", 1, 8);
                        if (last.Count > 9)
                            last.RemoveAt(last.Count - 1);
                        // Orta Blok Haberler
                        jSonData.CreateFile("haberler", last);
                        // Orta Blok Renkli Haber Kategorileri
                        if (Settings.Site.Category1.IsActive)
                            jSonData.CreateFile("haberler1", GetHaberler(Settings.Site.Category1.ID, 1, 8));
                        if (Settings.Site.Category2.IsActive)
                            jSonData.CreateFile("haberler2", GetHaberler(Settings.Site.Category2.ID, 1, 8));
                        if (Settings.Site.Category3.IsActive)
                            jSonData.CreateFile("haberler3", GetHaberler(Settings.Site.Category3.ID, 1, 8));
                        if (Settings.Site.Category4.IsActive)
                            jSonData.CreateFile("haberler4", GetHaberler(Settings.Site.Category4.ID, 1, 8));
                        break;
                    case "haber":
                        jSonData.CreateFile("haberkategoriust", GetKategori("haber", 1));
                        jSonData.CreateFile("haberkategorialt", GetKategori("haber", 2));
                        KategoriMethods.Save();
                        break;
                    case "doktorlar":
                        jSonData.CreateFile(jsonName, GetDoktorlar(40));
                        break;
                    case "makale":
                        jSonData.CreateFile(jsonName + "kategori", GetKategori(jsonName));
                        break;
                    case "makaleler":
                        jSonData.CreateFile(jsonName, GetYazarlar(40));
                        break;
                    case "meslek":
                        jSonData.CreateFile(jsonName + "kategori", GetKategori(jsonName));
                        break;
                    case "baglantilar":
                        jSonData.CreateFile(jsonName, GetBaglantilar(8));
                        break;
                    case "seriilanlar":
                        jSonData.CreateFile(jsonName, GetSeriIlanlar(8));
                        break;
                    case "video":
                        jSonData.CreateFile(jsonName + "kategori", GetKategori(jsonName));
                        break;
                    case "videolar":
                        jSonData.CreateFile("video", GetVideolar("", 1, 7));
                        jSonData.CreateFile("videocontent", GetVideolar("", 1, 20));
                        break;
                    case "galeri":
                        jSonData.CreateFile(jsonName + "kategori", GetKategori(jsonName));
                        break;
                    case "galeriler":
                        jSonData.CreateFile("galeri", GetGeleriler("", 1, 7));
                        jSonData.CreateFile("galericontent", GetGeleriler("", 1, 20));
                        break;
                    case "firma":
                        jSonData.CreateFile(jsonName + "kategori", GetKategori(jsonName));
                        break;
                    case "seriilan":
                        jSonData.CreateFile(jsonName + "kategori", GetKategori(jsonName));
                        break;
                    case "sayfalar":
                        jSonData.CreateFile(jsonName, GetSayfalar(1, 4));
                        break;
                    case "piyasabilgileri":
                        jSonData.CreateFile(jsonName, GetPiyasalar());
                        break;
                    case "havadurumlari":
                        jSonData.CreateFile(jsonName, GetHavaDurumlari());
                        break;
                    case "gazeteler":
                        jSonData.CreateFile(jsonName, GetGazeteler());
                        break;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static bool CreateFile(string fileName, object data)
        {
            try
            {
                if (data != null)
                {
                    System.Web.Script.Serialization.JavaScriptSerializer javaScriptSerializer = new System.Web.Script.Serialization.JavaScriptSerializer();
                    string jsondata = javaScriptSerializer.Serialize(data);
                    //string jsondata = Newtonsoft.Json.JsonConvert.SerializeObject(data);
                    if (!string.IsNullOrEmpty(jsondata))
                        using (System.IO.StreamWriter sw = new System.IO.StreamWriter(HttpContext.Current.Server.MapPath(Settings.JSonPath + fileName + ".js")))
                        {
                            sw.Write(jsondata);
                            sw.Close();
                            return true;
                        }
                }
                return false;
            }
            catch (Exception)
            {
                return false;
            }
        }
        public static bool CreateFile(string subPathName, string fileName, object data)
        {
            try
            {
                if (data != null)
                {
                    System.Web.Script.Serialization.JavaScriptSerializer javaScriptSerializer = new System.Web.Script.Serialization.JavaScriptSerializer();
                    string jsondata = javaScriptSerializer.Serialize(data);
                    //string jsondata = Newtonsoft.Json.JsonConvert.SerializeObject(data);
                    if (!string.IsNullOrEmpty(jsondata))
                        using (System.IO.StreamWriter sw = new System.IO.StreamWriter(HttpContext.Current.Server.MapPath(Settings.JSonPath + subPathName + "/" + fileName + ".js")))
                        {
                            sw.Write(jsondata);
                            sw.Close();
                            return true;
                        }
                }
                return false;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}