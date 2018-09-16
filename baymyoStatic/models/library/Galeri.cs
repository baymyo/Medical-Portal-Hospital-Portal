using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Data;
using BAYMYO.MultiSQLClient;

namespace baymyoStatic
{
    public partial class GaleriCollection : CollectionBase, IDisposable
    {
        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }

        public Galeri this[int index]
        {
            get { return (Galeri)this.List[index]; }
            set { this.List[index] = value; }
        }

        public object SyncRoot { get { return this.List.SyncRoot; } }

        public int Add(Galeri obj)
        {
            return this.List.Add(obj);
        }

        public void Insert(int index, Galeri obj)
        {
            this.List.Insert(index, obj);
        }

        public bool Contains(Galeri obj)
        {
            return this.List.Contains(obj);
        }

        public int IndexOf(Galeri obj)
        {
            return this.List.IndexOf(obj);
        }

        public void Remove(Galeri obj)
        {
            this.List.Remove(obj);
        }
    }

    public partial class Galeri : IDisposable
    {
        #region ---IDisposable Members---
        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }
        #endregion

        #region ---Properties/Field - Özellikler Alanlar---
        private System.String m_ID;
        public System.String ID
        {
            get { return m_ID; }
            set { m_ID = value; }
        }
        private Int64 m_AlbumID;
        public Int64 AlbumID
        {
            get { return m_AlbumID; }
            set { m_AlbumID = value; }
        }
        private string m_ResimUrl;
        public string ResimUrl
        {
            get { return m_ResimUrl; }
            set { m_ResimUrl = value; }
        }
        private string m_Aciklama;
        public string Aciklama
        {
            get { return m_Aciklama; }
            set { m_Aciklama = value; }
        }
        private DateTime m_KayitTarihi;
        public DateTime KayitTarihi
        {
            get { return m_KayitTarihi; }
            set { m_KayitTarihi = value; }
        }
        private bool m_Kapak;
        public bool Kapak
        {
            get { return m_Kapak; }
            set { m_Kapak = value; }
        }

        #endregion

        public Galeri()
        {
        }

        /// <summary>
        /// Galeri Nesnesi Oluþtur
        /// </summary>
        public Galeri(System.String pid, Int64 palbumid, string presimurl, string paciklama, DateTime pkayittarihi, bool pkapak)
        {
            this.m_ID = pid;
            this.m_AlbumID = palbumid;
            this.m_ResimUrl = presimurl;
            this.m_Aciklama = paciklama;
            this.m_KayitTarihi = pkayittarihi;
            this.m_Kapak = pkapak;
        }
    }

    public partial class GaleriMethods
    {
        ///<summary>
        /// Galeri Data PrimaryKey
        ///</summary>
        public static Galeri GetGaleri(System.String pid)
        {
            Galeri rvGaleri = new Galeri();
            using (MConnection conneciton = new MConnection(MClientProvider.MySQL))
            {
                switch (conneciton.State)
                {
                    case System.Data.ConnectionState.Closed:
                        conneciton.Open();
                        break;
                }
                using (MCommand cmd = new MCommand(CommandType.Text, "select * from galeri where id=?id limit 1", conneciton))
                {
                    cmd.Parameters.Add("id", pid, MSqlDbType.VarChar);
                    using (IDataReader IDR = cmd.ExecuteReader())
                    {
                        while (IDR.Read())
                            rvGaleri = new Galeri(MConvert.NullToGuidString(IDR["id"]), MConvert.NullToInt64(IDR["albumid"]), MConvert.NullToString(IDR["resimurl"]), MConvert.NullToString(IDR["aciklama"]), MConvert.NullToDateTime(IDR["kayittarihi"]), MConvert.NullToBool(IDR["kapak"]));
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
            return rvGaleri;
        }


        /// <summary>
        /// Galeri Getir
        /// </summary>
        public static Galeri GetGaleri(CommandType cmdType, string sqlQuery, MParameterCollection parameters)
        {
            Galeri rvGaleri = new Galeri();
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
                            rvGaleri = new Galeri(MConvert.NullToGuidString(IDR["id"]), MConvert.NullToInt64(IDR["albumid"]), MConvert.NullToString(IDR["resimurl"]), MConvert.NullToString(IDR["aciklama"]), MConvert.NullToDateTime(IDR["kayittarihi"]), MConvert.NullToBool(IDR["kapak"]));
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
            return rvGaleri;
        }

        /// <summary>
        /// Galeri Liste Getir
        /// </summary>
        public static GaleriCollection GetList(CommandType cmdType, string sqlQuery, MParameterCollection parameters)
        {
            GaleriCollection rvGaleri = new GaleriCollection();
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
                            rvGaleri.Add(new Galeri(MConvert.NullToGuidString(IDR["id"]), MConvert.NullToInt64(IDR["albumid"]), MConvert.NullToString(IDR["resimurl"]), MConvert.NullToString(IDR["aciklama"]), MConvert.NullToDateTime(IDR["kayittarihi"]), MConvert.NullToBool(IDR["kapak"])));
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
            return rvGaleri;
        }

        ///<summary>
        /// Galeri Data Select
        ///</summary>
        public static GaleriCollection GetSelect(Int64 palbumid)
        {
            GaleriCollection rvGaleri = new GaleriCollection();
            using (MConnection conneciton = new MConnection(MClientProvider.MySQL))
            {
                switch (conneciton.State)
                {
                    case System.Data.ConnectionState.Closed:
                        conneciton.Open();
                        break;
                }
                using (MCommand cmd = new MCommand(CommandType.Text, "select * from galeri where albumid=?albumid", conneciton))
                {
                    cmd.Parameters.Add("albumid", palbumid, MSqlDbType.BigInt);
                    using (IDataReader IDR = cmd.ExecuteReader())
                    {
                        while (IDR.Read())
                            rvGaleri.Add(new Galeri(MConvert.NullToGuidString(IDR["id"]), MConvert.NullToInt64(IDR["albumid"]), MConvert.NullToString(IDR["resimurl"]), MConvert.NullToString(IDR["aciklama"]), MConvert.NullToDateTime(IDR["kayittarihi"]), MConvert.NullToBool(IDR["kapak"])));
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
            return rvGaleri;
        }

        ///<summary>
        /// Galeri Data Insert
        ///</summary>
        public static int Insert(Galeri p)
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
                using (MCommand cmd = new MCommand(CommandType.Text, "insert into galeri (id,albumid,resimurl,aciklama,kayittarihi,kapak) values(?id,?albumid,?resimurl,?aciklama,?kayittarihi,?kapak)", conneciton))
                {
                    cmd.Parameters.Add("id", Guid.NewGuid(), MSqlDbType.VarChar);
                    cmd.Parameters.Add("albumid", p.AlbumID, MSqlDbType.BigInt);
                    cmd.Parameters.Add("resimurl", p.ResimUrl, MSqlDbType.VarChar);
                    cmd.Parameters.Add("aciklama", p.Aciklama, MSqlDbType.VarChar);
                    cmd.Parameters.Add("kayittarihi", p.KayitTarihi, MSqlDbType.DateTime);
                    cmd.Parameters.Add("kapak", p.Kapak, MSqlDbType.Boolean);
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
        /// Galeri Data Update
        ///</summary>
        public static int Update(Galeri p)
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
                using (MCommand cmd = new MCommand(CommandType.Text, "update galeri set albumid=?albumid,resimurl=?resimurl,aciklama=?aciklama,kayittarihi=?kayittarihi,kapak=?kapak where id=?id", conneciton))
                {
                    cmd.Parameters.Add("id", p.ID, MSqlDbType.VarChar);
                    cmd.Parameters.Add("albumid", p.AlbumID, MSqlDbType.BigInt);
                    cmd.Parameters.Add("resimurl", p.ResimUrl, MSqlDbType.VarChar);
                    cmd.Parameters.Add("aciklama", p.Aciklama, MSqlDbType.VarChar);
                    cmd.Parameters.Add("kayittarihi", p.KayitTarihi, MSqlDbType.DateTime);
                    cmd.Parameters.Add("kapak", p.Kapak, MSqlDbType.Boolean);
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

        public static int Update(Int64 albumid)
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
                using (MCommand cmd = new MCommand(CommandType.Text, "update galeri set kapak=0 where albumid=?albumid", conneciton))
                {
                    cmd.Parameters.Add("albumid", albumid, MSqlDbType.BigInt);
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
        /// Galeri Data Delete
        ///</summary>
        public static int Delete(System.String pid)
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
                using (MCommand cmd = new MCommand(CommandType.Text, "delete from galeri where id=?id", conneciton))
                {
                    cmd.Parameters.Add("id", pid, MSqlDbType.VarChar);
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
        /// Galeri Data Delete
        ///</summary>
        public static int Delete(Galeri p)
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
                using (MCommand cmd = new MCommand(CommandType.Text, "delete from galeri where id=?id", conneciton))
                {
                    cmd.Parameters.Add("id", p.ID, MSqlDbType.VarChar);
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
        /// Galeri Data Delete
        ///</summary>
        public static int Delete(Int64 albumid)
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
                using (MCommand cmd = new MCommand(CommandType.Text, "delete from galeri where albumid=?albumid", conneciton))
                {
                    cmd.Parameters.Add("albumid", albumid, MSqlDbType.VarChar);
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
