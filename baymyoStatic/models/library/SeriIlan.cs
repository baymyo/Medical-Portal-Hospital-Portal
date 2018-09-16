using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Data;
using BAYMYO.MultiSQLClient;

namespace baymyoStatic
{
    public partial class SeriIlanCollection : CollectionBase, IDisposable
    {
        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }

        public SeriIlan this[int index]
        {
            get { return (SeriIlan)this.List[index]; }
            set { this.List[index] = value; }
        }

        public object SyncRoot { get { return this.List.SyncRoot; } }

        public int Add(SeriIlan obj)
        {
            return this.List.Add(obj);
        }

        public void Insert(int index, SeriIlan obj)
        {
            this.List.Insert(index, obj);
        }

        public bool Contains(SeriIlan obj)
        {
            return this.List.Contains(obj);
        }

        public int IndexOf(SeriIlan obj)
        {
            return this.List.IndexOf(obj);
        }

        public void Remove(SeriIlan obj)
        {
            this.List.Remove(obj);
        }
    }

    public partial class SeriIlan : IDisposable
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
        private string m_Detay;
        public string Detay
        {
            get { return m_Detay; }
            set { m_Detay = value; }
        }
        private string m_Semt;
        public string Semt
        {
            get { return m_Semt; }
            set { m_Semt = value; }
        }
        private string m_Sehir;
        public string Sehir
        {
            get { return m_Sehir; }
            set { m_Sehir = value; }
        }
        private string m_Telefon;
        public string Telefon
        {
            get { return m_Telefon; }
            set { m_Telefon = value; }
        }
        private float m_Fiyat;
        public float Fiyat
        {
            get { return m_Fiyat; }
            set { m_Fiyat = value; }
        }
        private byte m_Kimden;
        public byte Kimden
        {
            get { return m_Kimden; }
            set { m_Kimden = value; }
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
        private bool m_YoneticiOnay;
        public bool YoneticiOnay
        {
            get { return m_YoneticiOnay; }
            set { m_YoneticiOnay = value; }
        }
        private bool m_Aktif;
        public bool Aktif
        {
            get { return m_Aktif; }
            set { m_Aktif = value; }
        }

        #endregion

        public SeriIlan()
        {
        }

        /// <summary>
        /// SeriIlan Nesnesi Oluþtur
        /// </summary>
        public SeriIlan(Int64 pid, System.String phesapid, string pkategoriid, string presimurl, string pbaslik, string pdetay, string psemt, string psehir, string ptelefon, float pfiyat, byte pkimden, DateTime pkayittarihi, DateTime pguncellemetarihi, bool pgosterimsayi, bool pyoneticionay, bool paktif)
        {
            this.m_ID = pid;
            this.m_HesapID = phesapid;
            this.m_KategoriID = pkategoriid;
            this.m_ResimUrl = presimurl;
            this.m_Baslik = pbaslik;
            this.m_Detay = pdetay;
            this.m_Semt = psemt;
            this.m_Sehir = psehir;
            this.m_Telefon = ptelefon;
            this.m_Fiyat = pfiyat;
            this.m_Kimden = pkimden;
            this.m_KayitTarihi = pkayittarihi;
            this.m_GuncellemeTarihi = pguncellemetarihi;
            this.m_GosterimSayi = pgosterimsayi;
            this.m_YoneticiOnay = pyoneticionay;
            this.m_Aktif = paktif;
        }
    }

    public partial class SeriIlanMethods
    {
        ///<summary>
        /// SeriIlan Data PrimaryKey
        ///</summary>
        public static SeriIlan GetSeriIlan(Int64 pid)
        {
            SeriIlan rvSeriIlan = new SeriIlan();
            using (MConnection conneciton = new MConnection(MClientProvider.MySQL))
            {
                switch (conneciton.State)
                {
                    case System.Data.ConnectionState.Closed:
                        conneciton.Open();
                        break;
                }
                using (MCommand cmd = new MCommand(CommandType.Text, "select * from seriilan where id=?id limit 1", conneciton))
                {
                    cmd.Parameters.Add("id", pid, MSqlDbType.BigInt);
                    using (IDataReader IDR = cmd.ExecuteReader())
                    {
                        while (IDR.Read())
                            rvSeriIlan = new SeriIlan(MConvert.NullToInt64(IDR["id"]), MConvert.NullToGuidString(IDR["hesapid"]), MConvert.NullToString(IDR["kategoriid"]), MConvert.NullToString(IDR["resimurl"]), MConvert.NullToString(IDR["baslik"]), MConvert.NullToString(IDR["detay"]), MConvert.NullToString(IDR["semt"]), MConvert.NullToString(IDR["sehir"]), MConvert.NullToString(IDR["telefon"]), MConvert.NullToFloat(IDR["fiyat"]), MConvert.NullToByte(IDR["kimden"]), MConvert.NullToDateTime(IDR["kayittarihi"]), MConvert.NullToDateTime(IDR["guncellemetarihi"]), MConvert.NullToBool(IDR["gosterimsayi"]), MConvert.NullToBool(IDR["yoneticionay"]), MConvert.NullToBool(IDR["aktif"]));
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
            return rvSeriIlan;
        }

        /// <summary>
        /// SeriIlan Getir
        /// </summary>
        public static SeriIlan GetSeriIlan(CommandType cmdType, string sqlQuery, MParameterCollection parameters)
        {
            SeriIlan rvSeriIlan = new SeriIlan();
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
                            rvSeriIlan = new SeriIlan(MConvert.NullToInt64(IDR["id"]), MConvert.NullToGuidString(IDR["hesapid"]), MConvert.NullToString(IDR["kategoriid"]), MConvert.NullToString(IDR["resimurl"]), MConvert.NullToString(IDR["baslik"]), MConvert.NullToString(IDR["detay"]), MConvert.NullToString(IDR["semt"]), MConvert.NullToString(IDR["sehir"]), MConvert.NullToString(IDR["telefon"]), MConvert.NullToFloat(IDR["fiyat"]), MConvert.NullToByte(IDR["kimden"]), MConvert.NullToDateTime(IDR["kayittarihi"]), MConvert.NullToDateTime(IDR["guncellemetarihi"]), MConvert.NullToBool(IDR["gosterimsayi"]), MConvert.NullToBool(IDR["yoneticionay"]), MConvert.NullToBool(IDR["aktif"]));
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
            return rvSeriIlan;
        }

        /// <summary>
        /// SeriIlan Liste Getir
        /// </summary>
        public static SeriIlanCollection GetList(CommandType cmdType, string sqlQuery, MParameterCollection parameters)
        {
            SeriIlanCollection rvSeriIlan = new SeriIlanCollection();
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
                            rvSeriIlan.Add(new SeriIlan(MConvert.NullToInt64(IDR["id"]), MConvert.NullToGuidString(IDR["hesapid"]), MConvert.NullToString(IDR["kategoriid"]), MConvert.NullToString(IDR["resimurl"]), MConvert.NullToString(IDR["baslik"]), MConvert.NullToString(IDR["detay"]), MConvert.NullToString(IDR["semt"]), MConvert.NullToString(IDR["sehir"]), MConvert.NullToString(IDR["telefon"]), MConvert.NullToFloat(IDR["fiyat"]), MConvert.NullToByte(IDR["kimden"]), MConvert.NullToDateTime(IDR["kayittarihi"]), MConvert.NullToDateTime(IDR["guncellemetarihi"]), MConvert.NullToBool(IDR["gosterimsayi"]), MConvert.NullToBool(IDR["yoneticionay"]), MConvert.NullToBool(IDR["aktif"])));
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
            return rvSeriIlan;
        }

        ///<summary>
        /// SeriIlan Data Select
        ///</summary>
        public static SeriIlanCollection GetSelect(System.String phesapid)
        {
            SeriIlanCollection rvSeriIlan = new SeriIlanCollection();
            using (MConnection conneciton = new MConnection(MClientProvider.MySQL))
            {
                switch (conneciton.State)
                {
                    case System.Data.ConnectionState.Closed:
                        conneciton.Open();
                        break;
                }
                using (MCommand cmd = new MCommand(CommandType.Text, "select * from seriilan where hesapid=?hesapid", conneciton))
                {
                    cmd.Parameters.Add("hesapid", phesapid, MSqlDbType.VarChar);
                    using (IDataReader IDR = cmd.ExecuteReader())
                    {
                        while (IDR.Read())
                            rvSeriIlan.Add(new SeriIlan(MConvert.NullToInt64(IDR["id"]), MConvert.NullToGuidString(IDR["hesapid"]), MConvert.NullToString(IDR["kategoriid"]), MConvert.NullToString(IDR["resimurl"]), MConvert.NullToString(IDR["baslik"]), MConvert.NullToString(IDR["detay"]), MConvert.NullToString(IDR["semt"]), MConvert.NullToString(IDR["sehir"]), MConvert.NullToString(IDR["telefon"]), MConvert.NullToFloat(IDR["fiyat"]), MConvert.NullToByte(IDR["kimden"]), MConvert.NullToDateTime(IDR["kayittarihi"]), MConvert.NullToDateTime(IDR["guncellemetarihi"]), MConvert.NullToBool(IDR["gosterimsayi"]), MConvert.NullToBool(IDR["yoneticionay"]), MConvert.NullToBool(IDR["aktif"])));
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
            return rvSeriIlan;
        }

        ///<summary>
        /// Ýlan Data Count
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
                using (MCommand cmd = new MCommand(CommandType.Text, "select count(*) as totalcount from seriilan where yoneticionay=?yoneticionay", conneciton))
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
        /// SeriIlan Data Insert
        ///</summary>
        public static Int64 Insert(SeriIlan p)
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
                using (MCommand cmd = new MCommand(CommandType.Text, "insert into seriilan (hesapid,kategoriid,resimurl,baslik,detay,semt,sehir,telefon,fiyat,kimden,kayittarihi,guncellemetarihi,gosterimsayi,yoneticionay,aktif) values(?hesapid,?kategoriid,?resimurl,?baslik,?detay,?semt,?sehir,?telefon,?fiyat,?kimden,?kayittarihi,?guncellemetarihi,?gosterimsayi,?yoneticionay,?aktif); select last_insert_id();", conneciton))
                {
                    cmd.Parameters.Add("hesapid", p.HesapID, MSqlDbType.VarChar);
                    cmd.Parameters.Add("kategoriid", p.KategoriID, MSqlDbType.VarChar);
                    cmd.Parameters.Add("resimurl", p.ResimUrl, MSqlDbType.VarChar);
                    cmd.Parameters.Add("baslik", p.Baslik, MSqlDbType.VarChar);
                    cmd.Parameters.Add("detay", p.Detay, MSqlDbType.VarChar);
                    cmd.Parameters.Add("semt", p.Semt, MSqlDbType.VarChar);
                    cmd.Parameters.Add("sehir", p.Sehir, MSqlDbType.VarChar);
                    cmd.Parameters.Add("telefon", p.Telefon, MSqlDbType.VarChar);
                    cmd.Parameters.Add("fiyat", p.Fiyat, MSqlDbType.Float);
                    cmd.Parameters.Add("kimden", p.Kimden, MSqlDbType.SmallInt);
                    cmd.Parameters.Add("kayittarihi", p.KayitTarihi, MSqlDbType.DateTime);
                    cmd.Parameters.Add("guncellemetarihi", p.GuncellemeTarihi, MSqlDbType.DateTime);
                    cmd.Parameters.Add("gosterimsayi", p.GosterimSayi, MSqlDbType.Boolean);
                    cmd.Parameters.Add("yoneticionay", p.YoneticiOnay, MSqlDbType.Boolean);
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
        /// SeriIlan Data Update
        ///</summary>
        public static int Update(SeriIlan p)
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
                using (MCommand cmd = new MCommand(CommandType.Text, "update seriilan set hesapid=?hesapid,kategoriid=?kategoriid,resimurl=?resimurl,baslik=?baslik,detay=?detay,semt=?semt,sehir=?sehir,telefon=?telefon,fiyat=?fiyat,kimden=?kimden,kayittarihi=?kayittarihi,guncellemetarihi=?guncellemetarihi,gosterimsayi=?gosterimsayi,yoneticionay=?yoneticionay,aktif=?aktif where id=?id", conneciton))
                {
                    cmd.Parameters.Add("id", p.ID, MSqlDbType.BigInt);
                    cmd.Parameters.Add("hesapid", p.HesapID, MSqlDbType.VarChar);
                    cmd.Parameters.Add("kategoriid", p.KategoriID, MSqlDbType.VarChar);
                    cmd.Parameters.Add("resimurl", p.ResimUrl, MSqlDbType.VarChar);
                    cmd.Parameters.Add("baslik", p.Baslik, MSqlDbType.VarChar);
                    cmd.Parameters.Add("detay", p.Detay, MSqlDbType.VarChar);
                    cmd.Parameters.Add("semt", p.Semt, MSqlDbType.VarChar);
                    cmd.Parameters.Add("sehir", p.Sehir, MSqlDbType.VarChar);
                    cmd.Parameters.Add("telefon", p.Telefon, MSqlDbType.VarChar);
                    cmd.Parameters.Add("fiyat", p.Fiyat, MSqlDbType.Float);
                    cmd.Parameters.Add("kimden", p.Kimden, MSqlDbType.SmallInt);
                    cmd.Parameters.Add("kayittarihi", p.KayitTarihi, MSqlDbType.DateTime);
                    cmd.Parameters.Add("guncellemetarihi", p.GuncellemeTarihi, MSqlDbType.DateTime);
                    cmd.Parameters.Add("gosterimsayi", p.GosterimSayi, MSqlDbType.Boolean);
                    cmd.Parameters.Add("yoneticionay", p.YoneticiOnay, MSqlDbType.Boolean);
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
        /// SeriIlan Data Delete
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
                using (MCommand cmd = new MCommand(CommandType.Text, "delete from seriilan where id=?id", conneciton))
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
        /// SeriIlan Data Delete
        ///</summary>
        public static int Delete(SeriIlan p)
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
                using (MCommand cmd = new MCommand(CommandType.Text, "delete from seriilan where id=?id", conneciton))
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
