using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Data;
using BAYMYO.MultiSQLClient;

namespace baymyoStatic
{
    public partial class VideoCollection : CollectionBase, IDisposable
    {
        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }

        public Video this[int index]
        {
            get { return (Video)this.List[index]; }
            set { this.List[index] = value; }
        }

        public object SyncRoot { get { return this.List.SyncRoot; } }

        public int Add(Video obj)
        {
            return this.List.Add(obj);
        }

        public void Insert(int index, Video obj)
        {
            this.List.Insert(index, obj);
        }

        public bool Contains(Video obj)
        {
            return this.List.Contains(obj);
        }

        public int IndexOf(Video obj)
        {
            return this.List.IndexOf(obj);
        }

        public void Remove(Video obj)
        {
            this.List.Remove(obj);
        }
    }

    public partial class Video : IDisposable
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
        private string m_Embed;
        public string Embed
        {
            get { return m_Embed; }
            set { m_Embed = value; }
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

        public Video()
        {
        }

        /// <summary>
        /// Video Nesnesi Oluþtur
        /// </summary>
        public Video(Int64 pid, System.String phesapid, string pkategoriid, string presimurl, string pbaslik, string pembed, string petiket, DateTime pkayittarihi, DateTime pguncellemetarihi, bool pgosterimsayi, bool puye, bool pyorum, bool pyoneticionay, bool paktif)
        {
            this.m_ID = pid;
            this.m_HesapID = phesapid;
            this.m_KategoriID = pkategoriid;
            this.m_ResimUrl = presimurl;
            this.m_Baslik = pbaslik;
            this.m_Embed = pembed;
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

    public partial class VideoMethods
    {
        ///<summary>
        /// Video Data PrimaryKey
        ///</summary>
        public static Video GetVideo(Int64 pid)
        {
            Video rvVideo = new Video();
            using (MConnection conneciton = new MConnection(MClientProvider.MySQL))
            {
                switch (conneciton.State)
                {
                    case System.Data.ConnectionState.Closed:
                        conneciton.Open();
                        break;
                }
                using (MCommand cmd = new MCommand(CommandType.Text, "select * from video where id=?id limit 1", conneciton))
                {
                    cmd.Parameters.Add("id", pid, MSqlDbType.BigInt);
                    using (IDataReader IDR = cmd.ExecuteReader())
                    {
                        while (IDR.Read())
                            rvVideo = new Video(MConvert.NullToInt64(IDR["id"]), MConvert.NullToGuidString(IDR["hesapid"]), MConvert.NullToString(IDR["kategoriid"]), MConvert.NullToString(IDR["resimurl"]), MConvert.NullToString(IDR["baslik"]), MConvert.NullToString(IDR["embed"]), MConvert.NullToString(IDR["etiket"]), MConvert.NullToDateTime(IDR["kayittarihi"]), MConvert.NullToDateTime(IDR["guncellemetarihi"]), MConvert.NullToBool(IDR["gosterimsayi"]), MConvert.NullToBool(IDR["uye"]), MConvert.NullToBool(IDR["yorum"]), MConvert.NullToBool(IDR["yoneticionay"]), MConvert.NullToBool(IDR["aktif"]));
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
            return rvVideo;
        }

        /// <summary>
        /// Video Getir
        /// </summary>
        public static Video GetVideo(CommandType cmdType, string sqlQuery, MParameterCollection parameters)
        {
            Video rvVideo = new Video();
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
                            rvVideo = new Video(MConvert.NullToInt64(IDR["id"]), MConvert.NullToGuidString(IDR["hesapid"]), MConvert.NullToString(IDR["kategoriid"]), MConvert.NullToString(IDR["resimurl"]), MConvert.NullToString(IDR["baslik"]), MConvert.NullToString(IDR["embed"]), MConvert.NullToString(IDR["etiket"]), MConvert.NullToDateTime(IDR["kayittarihi"]), MConvert.NullToDateTime(IDR["guncellemetarihi"]), MConvert.NullToBool(IDR["gosterimsayi"]), MConvert.NullToBool(IDR["uye"]), MConvert.NullToBool(IDR["yorum"]), MConvert.NullToBool(IDR["yoneticionay"]), MConvert.NullToBool(IDR["aktif"]));
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
            return rvVideo;
        }

        /// <summary>
        /// Video Liste Getir
        /// </summary>
        public static VideoCollection GetList(CommandType cmdType, string sqlQuery, MParameterCollection parameters)
        {
            VideoCollection rvVideo = new VideoCollection();
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
                            rvVideo.Add(new Video(MConvert.NullToInt64(IDR["id"]), MConvert.NullToGuidString(IDR["hesapid"]), MConvert.NullToString(IDR["kategoriid"]), MConvert.NullToString(IDR["resimurl"]), MConvert.NullToString(IDR["baslik"]), MConvert.NullToString(IDR["embed"]), MConvert.NullToString(IDR["etiket"]), MConvert.NullToDateTime(IDR["kayittarihi"]), MConvert.NullToDateTime(IDR["guncellemetarihi"]), MConvert.NullToBool(IDR["gosterimsayi"]), MConvert.NullToBool(IDR["uye"]), MConvert.NullToBool(IDR["yorum"]), MConvert.NullToBool(IDR["yoneticionay"]), MConvert.NullToBool(IDR["aktif"])));
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
            return rvVideo;
        }

        ///<summary>
        /// Video Data Select
        ///</summary>
        public static VideoCollection GetSelect(Int64 id,byte limit)
        {
            VideoCollection rvVideo = new VideoCollection();
            using (MConnection conneciton = new MConnection(MClientProvider.MySQL))
            {
                switch (conneciton.State)
                {
                    case System.Data.ConnectionState.Closed:
                        conneciton.Open();
                        break;
                }
                using (MCommand cmd = new MCommand(CommandType.Text, "select * from video where id<>?id order by guncellemetarihi desc limit ?limit", conneciton))
                {
                    cmd.Parameters.Add("id", id, MSqlDbType.BigInt);
                    cmd.Parameters.Add("limit", limit, MSqlDbType.TinyInt);
                    using (IDataReader IDR = cmd.ExecuteReader())
                    {
                        while (IDR.Read())
                            rvVideo.Add(new Video(MConvert.NullToInt64(IDR["id"]), MConvert.NullToGuidString(IDR["hesapid"]), MConvert.NullToString(IDR["kategoriid"]), MConvert.NullToString(IDR["resimurl"]), MConvert.NullToString(IDR["baslik"]), MConvert.NullToString(IDR["embed"]), MConvert.NullToString(IDR["etiket"]), MConvert.NullToDateTime(IDR["kayittarihi"]), MConvert.NullToDateTime(IDR["guncellemetarihi"]), MConvert.NullToBool(IDR["gosterimsayi"]), MConvert.NullToBool(IDR["uye"]), MConvert.NullToBool(IDR["yorum"]), MConvert.NullToBool(IDR["yoneticionay"]), MConvert.NullToBool(IDR["aktif"])));
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
            return rvVideo;
        }

        ///<summary>
        /// Video Data Insert
        ///</summary>
        public static Int64 Insert(Video p)
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
                using (MCommand cmd = new MCommand(CommandType.Text, "insert into video (hesapid,kategoriid,resimurl,baslik,embed,etiket,kayittarihi,guncellemetarihi,gosterimsayi,uye,yorum,yoneticionay,aktif) values(?hesapid,?kategoriid,?resimurl,?baslik,?embed,?etiket,?kayittarihi,?guncellemetarihi,?gosterimsayi,?uye,?yorum,?yoneticionay,?aktif); select last_insert_id();", conneciton))
                {
                    cmd.Parameters.Add("hesapid", p.HesapID, MSqlDbType.VarChar);
                    cmd.Parameters.Add("kategoriid", p.KategoriID, MSqlDbType.VarChar);
                    cmd.Parameters.Add("resimurl", p.ResimUrl, MSqlDbType.VarChar);
                    cmd.Parameters.Add("baslik", p.Baslik, MSqlDbType.VarChar);
                    cmd.Parameters.Add("embed", p.Embed, MSqlDbType.VarChar);
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
        /// Video Data Update
        ///</summary>
        public static int Update(Video p)
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
                using (MCommand cmd = new MCommand(CommandType.Text, "update video set hesapid=?hesapid,kategoriid=?kategoriid,resimurl=?resimurl,baslik=?baslik,embed=?embed,etiket=?etiket,kayittarihi=?kayittarihi,guncellemetarihi=?guncellemetarihi,gosterimsayi=?gosterimsayi,uye=?uye,yorum=?yorum,yoneticionay=?yoneticionay,aktif=?aktif where id=?id", conneciton))
                {
                    cmd.Parameters.Add("id", p.ID, MSqlDbType.BigInt);
                    cmd.Parameters.Add("hesapid", p.HesapID, MSqlDbType.VarChar);
                    cmd.Parameters.Add("kategoriid", p.KategoriID, MSqlDbType.VarChar);
                    cmd.Parameters.Add("resimurl", p.ResimUrl, MSqlDbType.VarChar);
                    cmd.Parameters.Add("baslik", p.Baslik, MSqlDbType.VarChar);
                    cmd.Parameters.Add("embed", p.Embed, MSqlDbType.VarChar);
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
        /// Video Data Delete
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
                using (MCommand cmd = new MCommand(CommandType.Text, "delete from video where id=?id", conneciton))
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
        /// Video Data Delete
        ///</summary>
        public static int Delete(Video p)
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
                using (MCommand cmd = new MCommand(CommandType.Text, "delete from video where id=?id", conneciton))
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
