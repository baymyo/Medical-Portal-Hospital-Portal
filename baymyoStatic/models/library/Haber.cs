using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Data;
using BAYMYO.MultiSQLClient;

namespace baymyoStatic
{
    public partial class HaberCollection : CollectionBase, IDisposable
    {
        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }

        public Haber this[int index]
        {
            get { return (Haber)this.List[index]; }
            set { this.List[index] = value; }
        }

        public object SyncRoot { get { return this.List.SyncRoot; } }

        public int Add(Haber obj)
        {
            return this.List.Add(obj);
        }

        public void Insert(int index, Haber obj)
        {
            this.List.Insert(index, obj);
        }

        public bool Contains(Haber obj)
        {
            return this.List.Contains(obj);
        }

        public int IndexOf(Haber obj)
        {
            return this.List.IndexOf(obj);
        }

        public void Remove(Haber obj)
        {
            this.List.Remove(obj);
        }
    }

    public partial class Haber : IDisposable
    {
        #region ---IDisposable Members---
        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }
        #endregion

        #region ---Properties/Field - Özellikler Alanlar---
        private Int64 m_ID;
        public Int64 ID
        {
            get { return m_ID; }
            set { m_ID = value; }
        }
        private System.String m_HesapID;
        public System.String HesapID
        {
            get { return m_HesapID; }
            set { m_HesapID = value; }
        }
        private string m_KategoriID;
        public string KategoriID
        {
            get { return m_KategoriID; }
            set { m_KategoriID = value; }
        }
        private string m_ResimUrl;
        public string ResimUrl
        {
            get { return m_ResimUrl; }
            set { m_ResimUrl = value; }
        }
        private string m_Baslik;
        public string Baslik
        {
            get { return m_Baslik; }
            set { m_Baslik = value; }
        }
        private string m_Ozet;
        public string Ozet
        {
            get { return m_Ozet; }
            set { m_Ozet = value; }
        }
        private string m_Icerik;
        public string Icerik
        {
            get { return m_Icerik; }
            set { m_Icerik = value; }
        }
        private string m_Etiket;
        public string Etiket
        {
            get { return m_Etiket; }
            set { m_Etiket = value; }
        }
        private string m_Sehir;
        public string Sehir
        {
            get { return m_Sehir; }
            set { m_Sehir = value; }
        }
        private DateTime m_KayitTarihi;
        public DateTime KayitTarihi
        {
            get { return m_KayitTarihi; }
            set { m_KayitTarihi = value; }
        }
        private DateTime m_GuncellemeTarihi;
        public DateTime GuncellemeTarihi
        {
            get { return m_GuncellemeTarihi; }
            set { m_GuncellemeTarihi = value; }
        }
        private bool m_GosterimSayi;
        public bool GosterimSayi
        {
            get { return m_GosterimSayi; }
            set { m_GosterimSayi = value; }
        }
        private Int64 m_Video;
        public Int64 Video
        {
            get { return m_Video; }
            set { m_Video = value; }
        }
        private Int64 m_Galeri;
        public Int64 Galeri
        {
            get { return m_Galeri; }
            set { m_Galeri = value; }
        }
        private bool m_Uye;
        public bool Uye
        {
            get { return m_Uye; }
            set { m_Uye = value; }
        }
        private bool m_Yorum;
        public bool Yorum
        {
            get { return m_Yorum; }
            set { m_Yorum = value; }
        }
        private bool m_YoneticiOnay;
        public bool YoneticiOnay
        {
            get { return m_YoneticiOnay; }
            set { m_YoneticiOnay = value; }
        }
        private bool m_Anasayfa;
        public bool Anasayfa
        {
            get { return m_Anasayfa; }
            set { m_Anasayfa = value; }
        }
        private bool m_Aktif;
        public bool Aktif
        {
            get { return m_Aktif; }
            set { m_Aktif = value; }
        }
        #endregion

        public Haber()
        {
        }

        /// <summary>
        /// Haber Nesnesi Oluþtur
        /// </summary>
        public Haber(Int64 pid, System.String phesapid, string pkategoriid, string presimurl, string pbaslik, string pozet, string picerik, string petiket, string psehir, DateTime pkayittarihi, DateTime pguncellemetarihi, bool pgosterimsayi, Int64 pvideo, Int64 pgaleri, bool puye, bool pyorum, bool pyoneticionay, bool panasayfa, bool paktif)
        {
            this.m_ID = pid;
            this.m_HesapID = phesapid;
            this.m_KategoriID = pkategoriid;
            this.m_ResimUrl = presimurl;
            this.m_Baslik = pbaslik;
            this.m_Ozet = pozet;
            this.m_Icerik = picerik;
            this.m_Etiket = petiket;
            this.m_Sehir = psehir;
            this.m_KayitTarihi = pkayittarihi;
            this.m_GuncellemeTarihi = pguncellemetarihi;
            this.m_GosterimSayi = pgosterimsayi;
            this.m_Video = pvideo;
            this.m_Galeri = pgaleri;
            this.m_Uye = puye;
            this.m_Yorum = pyorum;
            this.m_YoneticiOnay = pyoneticionay;
            this.m_Anasayfa = panasayfa;
            this.m_Aktif = paktif;
        }
    }

    public partial class HaberMethods
    {
        ///<summary>
        /// Haber Data PrimaryKey
        ///</summary>
        public static Haber GetHaber(Int64 pid)
        {
            Haber rvHaber = new Haber();
            using (MConnection conneciton = new MConnection(MClientProvider.MySQL))
            {
                switch (conneciton.State)
                {
                    case System.Data.ConnectionState.Closed:
                        conneciton.Open();
                        break;
                }
                using (MCommand cmd = new MCommand(CommandType.Text, "select * from haber where id=?id limit 1", conneciton))
                {
                    cmd.Parameters.Add("id", pid, MSqlDbType.BigInt);
                    using (IDataReader IDR = cmd.ExecuteReader())
                    {
                        while (IDR.Read())
                            rvHaber = new Haber(MConvert.NullToInt64(IDR["id"]), MConvert.NullToGuidString(IDR["hesapid"]), MConvert.NullToString(IDR["kategoriid"]), MConvert.NullToString(IDR["resimurl"]), MConvert.NullToString(IDR["baslik"]), MConvert.NullToString(IDR["ozet"]), MConvert.NullToString(IDR["icerik"]), MConvert.NullToString(IDR["etiket"]), MConvert.NullToString(IDR["sehir"]), MConvert.NullToDateTime(IDR["kayittarihi"]), MConvert.NullToDateTime(IDR["guncellemetarihi"]), MConvert.NullToBool(IDR["gosterimsayi"]), MConvert.NullToInt64(IDR["video"]), MConvert.NullToInt64(IDR["galeri"]), MConvert.NullToBool(IDR["uye"]), MConvert.NullToBool(IDR["yorum"]), MConvert.NullToBool(IDR["yoneticionay"]), MConvert.NullToBool(IDR["anasayfa"]), MConvert.NullToBool(IDR["aktif"]));
                        IDR.Close();
                    }
                }
                switch (conneciton.State)
                {
                    case System.Data.ConnectionState.Open:
                        conneciton.Close();
                        break;
                }
            }
            return rvHaber;
        }

        public static Haber GetHaber(string pbaslik)
        {
            Haber rvHaber = new Haber();
            using (MConnection conneciton = new MConnection(MClientProvider.MySQL))
            {
                switch (conneciton.State)
                {
                    case System.Data.ConnectionState.Closed:
                        conneciton.Open();
                        break;
                }
                using (MCommand cmd = new MCommand(CommandType.Text, "select * from haber h where h.baslik=?baslik limit 1", conneciton))
                {
                    cmd.Parameters.Add("baslik", pbaslik, MSqlDbType.VarChar);
                    using (IDataReader IDR = cmd.ExecuteReader())
                    {
                        while (IDR.Read())
                            rvHaber = new Haber(MConvert.NullToInt64(IDR["id"]), MConvert.NullToGuidString(IDR["hesapid"]), MConvert.NullToString(IDR["kategoriid"]), MConvert.NullToString(IDR["resimurl"]), MConvert.NullToString(IDR["baslik"]), MConvert.NullToString(IDR["ozet"]), MConvert.NullToString(IDR["icerik"]), MConvert.NullToString(IDR["etiket"]), MConvert.NullToString(IDR["sehir"]), MConvert.NullToDateTime(IDR["kayittarihi"]), MConvert.NullToDateTime(IDR["guncellemetarihi"]), MConvert.NullToBool(IDR["gosterimsayi"]), MConvert.NullToInt64(IDR["video"]), MConvert.NullToInt64(IDR["galeri"]), MConvert.NullToBool(IDR["uye"]), MConvert.NullToBool(IDR["yorum"]), MConvert.NullToBool(IDR["yoneticionay"]), MConvert.NullToBool(IDR["anasayfa"]), MConvert.NullToBool(IDR["aktif"]));
                        IDR.Close();
                    }
                }
                switch (conneciton.State)
                {
                    case System.Data.ConnectionState.Open:
                        conneciton.Close();
                        break;
                }
            }
            return rvHaber;
        }

        /// <summary>
        /// Haber Getir
        /// </summary>
        public static Haber GetHaber(CommandType cmdType, string sqlQuery, MParameterCollection parameters)
        {
            Haber rvHaber = new Haber();
            using (MConnection conneciton = new MConnection(MClientProvider.MySQL))
            {
                switch (conneciton.State)
                {
                    case System.Data.ConnectionState.Closed:
                        conneciton.Open();
                        break;
                }
                using (MCommand cmd = new MCommand(cmdType, sqlQuery, conneciton))
                {
                    if (parameters != null)
                        foreach (MParameter item in parameters)
                            cmd.Parameters.Add(item);
                    using (IDataReader IDR = cmd.ExecuteReader())
                    {
                        while (IDR.Read())
                            rvHaber = new Haber(MConvert.NullToInt64(IDR["id"]), MConvert.NullToGuidString(IDR["hesapid"]), MConvert.NullToString(IDR["kategoriid"]), MConvert.NullToString(IDR["resimurl"]), MConvert.NullToString(IDR["baslik"]), MConvert.NullToString(IDR["ozet"]), MConvert.NullToString(IDR["icerik"]), MConvert.NullToString(IDR["etiket"]), MConvert.NullToString(IDR["sehir"]), MConvert.NullToDateTime(IDR["kayittarihi"]), MConvert.NullToDateTime(IDR["guncellemetarihi"]), MConvert.NullToBool(IDR["gosterimsayi"]), MConvert.NullToInt64(IDR["video"]), MConvert.NullToInt64(IDR["galeri"]), MConvert.NullToBool(IDR["uye"]), MConvert.NullToBool(IDR["yorum"]), MConvert.NullToBool(IDR["yoneticionay"]), MConvert.NullToBool(IDR["anasayfa"]), MConvert.NullToBool(IDR["aktif"]));
                        IDR.Close();
                    }
                }
                switch (conneciton.State)
                {
                    case System.Data.ConnectionState.Open:
                        conneciton.Close();
                        break;
                }
            }
            return rvHaber;
        }

        /// <summary>
        /// Haber Liste Getir
        /// </summary>
        public static HaberCollection GetList(CommandType cmdType, string sqlQuery, MParameterCollection parameters)
        {
            HaberCollection rvHaber = new HaberCollection();
            using (MConnection conneciton = new MConnection(MClientProvider.MySQL))
            {
                switch (conneciton.State)
                {
                    case System.Data.ConnectionState.Closed:
                        conneciton.Open();
                        break;
                }
                using (MCommand cmd = new MCommand(cmdType, sqlQuery, conneciton))
                {
                    if (parameters != null)
                        foreach (MParameter item in parameters)
                            cmd.Parameters.Add(item);
                    using (IDataReader IDR = cmd.ExecuteReader())
                    {
                        while (IDR.Read())
                            rvHaber.Add(new Haber(MConvert.NullToInt64(IDR["id"]), MConvert.NullToGuidString(IDR["hesapid"]), MConvert.NullToString(IDR["kategoriid"]), MConvert.NullToString(IDR["resimurl"]), MConvert.NullToString(IDR["baslik"]), MConvert.NullToString(IDR["ozet"]), MConvert.NullToString(IDR["icerik"]), MConvert.NullToString(IDR["etiket"]), MConvert.NullToString(IDR["sehir"]), MConvert.NullToDateTime(IDR["kayittarihi"]), MConvert.NullToDateTime(IDR["guncellemetarihi"]), MConvert.NullToBool(IDR["gosterimsayi"]), MConvert.NullToInt64(IDR["video"]), MConvert.NullToInt64(IDR["galeri"]), MConvert.NullToBool(IDR["uye"]), MConvert.NullToBool(IDR["yorum"]), MConvert.NullToBool(IDR["yoneticionay"]), MConvert.NullToBool(IDR["anasayfa"]), MConvert.NullToBool(IDR["aktif"])));
                        IDR.Close();
                    }
                }
                switch (conneciton.State)
                {
                    case System.Data.ConnectionState.Open:
                        conneciton.Close();
                        break;
                }
            }
            return rvHaber;
        }
        public static HaberCollection GetSelect(string pkategoriid)
        {
            HaberCollection rvHaber = new HaberCollection();
            using (MConnection conneciton = new MConnection(MClientProvider.MySQL))
            {
                switch (conneciton.State)
                {
                    case System.Data.ConnectionState.Closed:
                        conneciton.Open();
                        break;
                }
                using (MCommand cmd = new MCommand(CommandType.Text, "select * from haber where kategoriid=?kategoriid", conneciton))
                {
                    cmd.Parameters.Add("kategoriid", pkategoriid, MSqlDbType.VarChar);
                    using (IDataReader IDR = cmd.ExecuteReader())
                    {
                        while (IDR.Read())
                            rvHaber.Add(new Haber(MConvert.NullToInt64(IDR["id"]), MConvert.NullToGuidString(IDR["hesapid"]), MConvert.NullToString(IDR["kategoriid"]), MConvert.NullToString(IDR["resimurl"]), MConvert.NullToString(IDR["baslik"]), MConvert.NullToString(IDR["ozet"]), MConvert.NullToString(IDR["icerik"]), MConvert.NullToString(IDR["etiket"]), MConvert.NullToString(IDR["sehir"]), MConvert.NullToDateTime(IDR["kayittarihi"]), MConvert.NullToDateTime(IDR["guncellemetarihi"]), MConvert.NullToBool(IDR["gosterimsayi"]), MConvert.NullToInt64(IDR["video"]), MConvert.NullToInt64(IDR["galeri"]), MConvert.NullToBool(IDR["uye"]), MConvert.NullToBool(IDR["yorum"]), MConvert.NullToBool(IDR["yoneticionay"]), MConvert.NullToBool(IDR["anasayfa"]), MConvert.NullToBool(IDR["aktif"])));
                        IDR.Close();
                    }
                }
                switch (conneciton.State)
                {
                    case System.Data.ConnectionState.Open:
                        conneciton.Close();
                        break;
                }
            }
            return rvHaber;
        }

        ///<summary>
        /// Haber Data Count
        ///</summary>
        public static int Count(bool pyoneticionay)
        {
            int rowsAffected = 0;
            using (MConnection conneciton = new MConnection(MClientProvider.MySQL))
            {
                switch (conneciton.State)
                {
                    case System.Data.ConnectionState.Closed:
                        conneciton.Open();
                        break;
                }
                using (MCommand cmd = new MCommand(CommandType.Text, "select count(*) as totalcount from haber where yoneticionay=?yoneticionay", conneciton))
                {
                    cmd.Parameters.Add("yoneticionay", pyoneticionay, MSqlDbType.Boolean);
                    rowsAffected = MConvert.NullToInt(cmd.ExecuteScalar());
                }
                switch (conneciton.State)
                {
                    case System.Data.ConnectionState.Open:
                        conneciton.Close();
                        break;
                }
            }
            return rowsAffected;
        }

        ///<summary>
        /// Haber Data Insert
        ///</summary>
        public static Int64 Insert(Haber p)
        {
            Int64 rowsAffected = 0;
            using (MConnection conneciton = new MConnection(MClientProvider.MySQL))
            {
                switch (conneciton.State)
                {
                    case System.Data.ConnectionState.Closed:
                        conneciton.Open();
                        break;
                }
                using (MCommand cmd = new MCommand(CommandType.Text, "insert into haber (hesapid,kategoriid,resimurl,baslik,ozet,icerik,etiket,sehir,kayittarihi,guncellemetarihi,gosterimsayi,video,galeri,uye,yorum,yoneticionay,anasayfa,aktif) values(?hesapid,?kategoriid,?resimurl,?baslik,?ozet,?icerik,?etiket,?sehir,?kayittarihi,?guncellemetarihi,?gosterimsayi,?video,?galeri,?uye,?yorum,?yoneticionay,?anasayfa,?aktif); select last_insert_id();", conneciton))
                {
                    cmd.Parameters.Add("hesapid", p.HesapID, MSqlDbType.VarChar);
                    cmd.Parameters.Add("kategoriid", p.KategoriID, MSqlDbType.VarChar);
                    cmd.Parameters.Add("resimurl", p.ResimUrl, MSqlDbType.VarChar);
                    cmd.Parameters.Add("baslik", p.Baslik, MSqlDbType.VarChar);
                    cmd.Parameters.Add("ozet", p.Ozet, MSqlDbType.VarChar);
                    cmd.Parameters.Add("icerik", p.Icerik, MSqlDbType.LongText);
                    cmd.Parameters.Add("etiket", p.Etiket, MSqlDbType.VarChar);
                    cmd.Parameters.Add("sehir", p.Sehir, MSqlDbType.VarChar);
                    cmd.Parameters.Add("kayittarihi", p.KayitTarihi, MSqlDbType.DateTime);
                    cmd.Parameters.Add("guncellemetarihi", p.GuncellemeTarihi, MSqlDbType.DateTime);
                    cmd.Parameters.Add("gosterimsayi", p.GosterimSayi, MSqlDbType.Boolean);
                    cmd.Parameters.Add("video", p.Video, MSqlDbType.BigInt);
                    cmd.Parameters.Add("galeri", p.Galeri, MSqlDbType.BigInt);
                    cmd.Parameters.Add("uye", p.Uye, MSqlDbType.Boolean);
                    cmd.Parameters.Add("yorum", p.Yorum, MSqlDbType.Boolean);
                    cmd.Parameters.Add("yoneticionay", p.YoneticiOnay, MSqlDbType.Boolean);
                    cmd.Parameters.Add("anasayfa", p.Anasayfa, MSqlDbType.Boolean);
                    cmd.Parameters.Add("aktif", p.Aktif, MSqlDbType.Boolean);
                    rowsAffected = MConvert.NullToInt64(cmd.ExecuteScalar());
                }
                switch (conneciton.State)
                {
                    case System.Data.ConnectionState.Open:
                        conneciton.Close();
                        break;
                }
            }
            return rowsAffected;
        }

        ///<summary>
        /// Haber Data Update
        ///</summary>
        public static int Update(Haber p)
        {
            int rowsAffected = 0;
            using (MConnection conneciton = new MConnection(MClientProvider.MySQL))
            {
                switch (conneciton.State)
                {
                    case System.Data.ConnectionState.Closed:
                        conneciton.Open();
                        break;
                }
                using (MCommand cmd = new MCommand(CommandType.Text, "update haber set hesapid=?hesapid,kategoriid=?kategoriid,resimurl=?resimurl,baslik=?baslik,ozet=?ozet,icerik=?icerik,etiket=?etiket,sehir=?sehir,kayittarihi=?kayittarihi,guncellemetarihi=?guncellemetarihi,gosterimsayi=?gosterimsayi,video=?video,galeri=?galeri,uye=?uye,yorum=?yorum,yoneticionay=?yoneticionay,anasayfa=?anasayfa,aktif=?aktif where id=?id", conneciton))
                {
                    cmd.Parameters.Add("id", p.ID, MSqlDbType.BigInt);
                    cmd.Parameters.Add("hesapid", p.HesapID, MSqlDbType.VarChar);
                    cmd.Parameters.Add("kategoriid", p.KategoriID, MSqlDbType.VarChar);
                    cmd.Parameters.Add("resimurl", p.ResimUrl, MSqlDbType.VarChar);
                    cmd.Parameters.Add("baslik", p.Baslik, MSqlDbType.VarChar);
                    cmd.Parameters.Add("ozet", p.Ozet, MSqlDbType.VarChar);
                    cmd.Parameters.Add("icerik", p.Icerik, MSqlDbType.LongText);
                    cmd.Parameters.Add("etiket", p.Etiket, MSqlDbType.VarChar);
                    cmd.Parameters.Add("sehir", p.Sehir, MSqlDbType.VarChar);
                    cmd.Parameters.Add("kayittarihi", p.KayitTarihi, MSqlDbType.DateTime);
                    cmd.Parameters.Add("guncellemetarihi", p.GuncellemeTarihi, MSqlDbType.DateTime);
                    cmd.Parameters.Add("gosterimsayi", p.GosterimSayi, MSqlDbType.Boolean);
                    cmd.Parameters.Add("video", p.Video, MSqlDbType.BigInt);
                    cmd.Parameters.Add("galeri", p.Galeri, MSqlDbType.BigInt);
                    cmd.Parameters.Add("uye", p.Uye, MSqlDbType.Boolean);
                    cmd.Parameters.Add("yorum", p.Yorum, MSqlDbType.Boolean);
                    cmd.Parameters.Add("yoneticionay", p.YoneticiOnay, MSqlDbType.Boolean);
                    cmd.Parameters.Add("anasayfa", p.Anasayfa, MSqlDbType.Boolean);
                    cmd.Parameters.Add("aktif", p.Aktif, MSqlDbType.Boolean);
                    rowsAffected = cmd.ExecuteNonQuery();
                }
                switch (conneciton.State)
                {
                    case System.Data.ConnectionState.Open:
                        conneciton.Close();
                        break;
                }
            }
            return rowsAffected;
        }

        ///<summary>
        /// Haber Data Delete
        ///</summary>
        public static int Delete(Int64 pid)
        {
            int rowsAffected = 0;
            using (MConnection conneciton = new MConnection(MClientProvider.MySQL))
            {
                switch (conneciton.State)
                {
                    case System.Data.ConnectionState.Closed:
                        conneciton.Open();
                        break;
                }
                using (MCommand cmd = new MCommand(CommandType.Text, "delete from haber where id=?id", conneciton))
                {
                    cmd.Parameters.Add("id", pid, MSqlDbType.BigInt);
                    rowsAffected = cmd.ExecuteNonQuery();
                }
                switch (conneciton.State)
                {
                    case System.Data.ConnectionState.Open:
                        conneciton.Close();
                        break;
                }
            }
            return rowsAffected;
        }

        ///<summary>
        /// Haber Data Delete
        ///</summary>
        public static int Delete(Haber p)
        {
            int rowsAffected = 0;
            using (MConnection conneciton = new MConnection(MClientProvider.MySQL))
            {
                switch (conneciton.State)
                {
                    case System.Data.ConnectionState.Closed:
                        conneciton.Open();
                        break;
                }
                using (MCommand cmd = new MCommand(CommandType.Text, "delete from haber where id=?id", conneciton))
                {
                    cmd.Parameters.Add("id", p.ID, MSqlDbType.BigInt);
                    rowsAffected = cmd.ExecuteNonQuery();
                }
                switch (conneciton.State)
                {
                    case System.Data.ConnectionState.Open:
                        conneciton.Close();
                        break;
                }
            }
            return rowsAffected;
        }
    }
}
