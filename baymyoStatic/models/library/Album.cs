using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Data;
using BAYMYO.MultiSQLClient;

namespace baymyoStatic
{
    public partial class AlbumCollection : CollectionBase, IDisposable
    {
        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }

        public Album this[int index]
        {
            get { return (Album)this.List[index]; }
            set { this.List[index] = value; }
        }

        public object SyncRoot { get { return this.List.SyncRoot; } }

        public int Add(Album obj)
        {
            return this.List.Add(obj);
        }

        public void Insert(int index, Album obj)
        {
            this.List.Insert(index, obj);
        }

        public bool Contains(Album obj)
        {
            return this.List.Contains(obj);
        }

        public int IndexOf(Album obj)
        {
            return this.List.IndexOf(obj);
        }

        public void Remove(Album obj)
        {
            this.List.Remove(obj);
        }
    }

    public partial class Album : IDisposable
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
        private string m_Adi;
        public string Adi
        {
            get { return m_Adi; }
            set { m_Adi = value; }
        }
        private string m_Etiket;
        public string Etiket
        {
            get { return m_Etiket; }
            set { m_Etiket = value; }
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
        private bool m_Aktif;
        public bool Aktif
        {
            get { return m_Aktif; }
            set { m_Aktif = value; }
        }

        #endregion

        public Album()
        {
        }

        /// <summary>
        /// Album Nesnesi Oluþtur
        /// </summary>
        public Album(Int64 pid, System.String phesapid, string pkategoriid, string padi, string petiket, DateTime pkayittarihi, DateTime pguncellemetarihi, bool pgosterimsayi, bool puye, bool pyorum, bool pyoneticionay, bool paktif)
        {
            this.m_ID = pid;
            this.m_HesapID = phesapid;
            this.m_KategoriID = pkategoriid;
            this.m_Adi = padi;
            this.m_Etiket = petiket;
            this.m_KayitTarihi = pkayittarihi;
            this.m_GuncellemeTarihi = pguncellemetarihi;
            this.m_GosterimSayi = pgosterimsayi;
            this.m_Uye = puye;
            this.m_Yorum = pyorum;
            this.m_YoneticiOnay = pyoneticionay;
            this.m_Aktif = paktif;
        }
    }

    public partial class AlbumMethods
    {
        ///<summary>
        /// Album Data PrimaryKey
        ///</summary>
        public static Album GetAlbum(Int64 pid)
        {
            Album rvAlbum = new Album();
            using (MConnection conneciton = new MConnection(MClientProvider.MySQL))
            {
                switch (conneciton.State)
                {
                    case System.Data.ConnectionState.Closed:
                        conneciton.Open();
                        break;
                }
                using (MCommand cmd = new MCommand(CommandType.Text, "select * from album where id=?id limit 1", conneciton))
                {
                    cmd.Parameters.Add("id", pid, MSqlDbType.BigInt);
                    using (IDataReader IDR = cmd.ExecuteReader())
                    {
                        while (IDR.Read())
                            rvAlbum = new Album(MConvert.NullToInt64(IDR["id"]), MConvert.NullToGuidString(IDR["hesapid"]), MConvert.NullToString(IDR["kategoriid"]), MConvert.NullToString(IDR["adi"]), MConvert.NullToString(IDR["etiket"]), MConvert.NullToDateTime(IDR["kayittarihi"]), MConvert.NullToDateTime(IDR["guncellemetarihi"]), MConvert.NullToBool(IDR["gosterimsayi"]), MConvert.NullToBool(IDR["uye"]), MConvert.NullToBool(IDR["yorum"]), MConvert.NullToBool(IDR["yoneticionay"]), MConvert.NullToBool(IDR["aktif"]));
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
            return rvAlbum;
        }
        public static Album GetAlbum(string padi)
        {
            Album rvAlbum = new Album();
            using (MConnection conneciton = new MConnection(MClientProvider.MySQL))
            {
                switch (conneciton.State)
                {
                    case System.Data.ConnectionState.Closed:
                        conneciton.Open();
                        break;
                }
                using (MCommand cmd = new MCommand(CommandType.Text, "select * from album where adi=?adi limit 1", conneciton))
                {
                    cmd.Parameters.Add("adi", padi, MSqlDbType.VarChar);
                    using (IDataReader IDR = cmd.ExecuteReader())
                    {
                        while (IDR.Read())
                            rvAlbum = new Album(MConvert.NullToInt64(IDR["id"]), MConvert.NullToGuidString(IDR["hesapid"]), MConvert.NullToString(IDR["kategoriid"]), MConvert.NullToString(IDR["adi"]), MConvert.NullToString(IDR["etiket"]), MConvert.NullToDateTime(IDR["kayittarihi"]), MConvert.NullToDateTime(IDR["guncellemetarihi"]), MConvert.NullToBool(IDR["gosterimsayi"]), MConvert.NullToBool(IDR["uye"]), MConvert.NullToBool(IDR["yorum"]), MConvert.NullToBool(IDR["yoneticionay"]), MConvert.NullToBool(IDR["aktif"]));
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
            return rvAlbum;
        }

        /// <summary>
        /// Album Getir
        /// </summary>
        public static Album GetAlbum(CommandType cmdType, string sqlQuery, MParameterCollection parameters)
        {
            Album rvAlbum = new Album();
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
                            rvAlbum = new Album(MConvert.NullToInt64(IDR["id"]), MConvert.NullToGuidString(IDR["hesapid"]), MConvert.NullToString(IDR["kategoriid"]), MConvert.NullToString(IDR["adi"]), MConvert.NullToString(IDR["etiket"]), MConvert.NullToDateTime(IDR["kayittarihi"]), MConvert.NullToDateTime(IDR["guncellemetarihi"]), MConvert.NullToBool(IDR["gosterimsayi"]), MConvert.NullToBool(IDR["uye"]), MConvert.NullToBool(IDR["yorum"]), MConvert.NullToBool(IDR["yoneticionay"]), MConvert.NullToBool(IDR["aktif"]));
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
            return rvAlbum;
        }

        /// <summary>
        /// Album Liste Getir
        /// </summary>
        public static AlbumCollection GetList(CommandType cmdType, string sqlQuery, MParameterCollection parameters)
        {
            AlbumCollection rvAlbum = new AlbumCollection();
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
                            rvAlbum.Add(new Album(MConvert.NullToInt64(IDR["id"]), MConvert.NullToGuidString(IDR["hesapid"]), MConvert.NullToString(IDR["kategoriid"]), MConvert.NullToString(IDR["adi"]), MConvert.NullToString(IDR["etiket"]), MConvert.NullToDateTime(IDR["kayittarihi"]), MConvert.NullToDateTime(IDR["guncellemetarihi"]), MConvert.NullToBool(IDR["gosterimsayi"]), MConvert.NullToBool(IDR["uye"]), MConvert.NullToBool(IDR["yorum"]), MConvert.NullToBool(IDR["yoneticionay"]), MConvert.NullToBool(IDR["aktif"])));
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
            return rvAlbum;
        }

        ///<summary>
        /// Album Data Select
        ///</summary>
        public static AlbumCollection GetSelect(byte limit)
        {
            AlbumCollection rvAlbum = new AlbumCollection();
            using (MConnection conneciton = new MConnection(MClientProvider.MySQL))
            {
                switch (conneciton.State)
                {
                    case System.Data.ConnectionState.Closed:
                        conneciton.Open();
                        break;
                }
                using (MCommand cmd = new MCommand(CommandType.Text, "select * from album order by guncellemetarihi desc limit ?limit", conneciton))
                {
                    cmd.Parameters.Add("limit", limit, MSqlDbType.TinyInt);
                    using (IDataReader IDR = cmd.ExecuteReader())
                    {
                        while (IDR.Read())
                            rvAlbum.Add(new Album(MConvert.NullToInt64(IDR["id"]), MConvert.NullToGuidString(IDR["hesapid"]), MConvert.NullToString(IDR["kategoriid"]), MConvert.NullToString(IDR["adi"]), MConvert.NullToString(IDR["etiket"]), MConvert.NullToDateTime(IDR["kayittarihi"]), MConvert.NullToDateTime(IDR["guncellemetarihi"]), MConvert.NullToBool(IDR["gosterimsayi"]), MConvert.NullToBool(IDR["uye"]), MConvert.NullToBool(IDR["yorum"]), MConvert.NullToBool(IDR["yoneticionay"]), MConvert.NullToBool(IDR["aktif"])));
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
            return rvAlbum;
        }

        ///<summary>
        /// Album Data Insert
        ///</summary>
        public static Int64 Insert(Album p)
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
                using (MCommand cmd = new MCommand(CommandType.Text, "insert into album (hesapid,kategoriid,adi,etiket,kayittarihi,guncellemetarihi,gosterimsayi,uye,yorum,yoneticionay,aktif) values(?hesapid,?kategoriid,?adi,?etiket,?kayittarihi,?guncellemetarihi,?gosterimsayi,?uye,?yorum,?yoneticionay,?aktif); select last_insert_id();", conneciton))
                {
                    cmd.Parameters.Add("hesapid", p.HesapID, MSqlDbType.VarChar);
                    cmd.Parameters.Add("kategoriid", p.KategoriID, MSqlDbType.VarChar);
                    cmd.Parameters.Add("adi", p.Adi, MSqlDbType.VarChar);
                    cmd.Parameters.Add("etiket", p.Etiket, MSqlDbType.VarChar);
                    cmd.Parameters.Add("kayittarihi", p.KayitTarihi, MSqlDbType.DateTime);
                    cmd.Parameters.Add("guncellemetarihi", p.GuncellemeTarihi, MSqlDbType.DateTime);
                    cmd.Parameters.Add("gosterimsayi", p.GosterimSayi, MSqlDbType.Boolean);
                    cmd.Parameters.Add("uye", p.Uye, MSqlDbType.Boolean);
                    cmd.Parameters.Add("yorum", p.Yorum, MSqlDbType.Boolean);
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
        /// Album Data Update
        ///</summary>
        public static int Update(Album p)
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
                using (MCommand cmd = new MCommand(CommandType.Text, "update album set hesapid=?hesapid,kategoriid=?kategoriid,adi=?adi,etiket=?etiket,kayittarihi=?kayittarihi,guncellemetarihi=?guncellemetarihi,gosterimsayi=?gosterimsayi,uye=?uye,yorum=?yorum,yoneticionay=?yoneticionay,aktif=?aktif where id=?id", conneciton))
                {
                    cmd.Parameters.Add("id", p.ID, MSqlDbType.BigInt);
                    cmd.Parameters.Add("hesapid", p.HesapID, MSqlDbType.VarChar);
                    cmd.Parameters.Add("kategoriid", p.KategoriID, MSqlDbType.VarChar);
                    cmd.Parameters.Add("adi", p.Adi, MSqlDbType.VarChar);
                    cmd.Parameters.Add("etiket", p.Etiket, MSqlDbType.VarChar);
                    cmd.Parameters.Add("kayittarihi", p.KayitTarihi, MSqlDbType.DateTime);
                    cmd.Parameters.Add("guncellemetarihi", p.GuncellemeTarihi, MSqlDbType.DateTime);
                    cmd.Parameters.Add("gosterimsayi", p.GosterimSayi, MSqlDbType.Boolean);
                    cmd.Parameters.Add("uye", p.Uye, MSqlDbType.Boolean);
                    cmd.Parameters.Add("yorum", p.Yorum, MSqlDbType.Boolean);
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
        /// Album Data Delete
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
                using (MCommand cmd = new MCommand(CommandType.Text, "delete from album where id=?id", conneciton))
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
        /// Album Data Delete
        ///</summary>
        public static int Delete(Album p)
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
                using (MCommand cmd = new MCommand(CommandType.Text, "delete from album where id=?id", conneciton))
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
