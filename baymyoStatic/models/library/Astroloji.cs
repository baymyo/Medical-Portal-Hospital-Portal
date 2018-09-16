using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Data;
using BAYMYO.MultiSQLClient;

namespace baymyoStatic
{
    public partial class AstrolojiCollection : CollectionBase, IDisposable
    {
        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }

        public Astroloji this[int index]
        {
            get { return (Astroloji)this.List[index]; }
            set { this.List[index] = value; }
        }

        public object SyncRoot { get { return this.List.SyncRoot; } }

        public int Add(Astroloji obj)
        {
            return this.List.Add(obj);
        }

        public void Insert(int index, Astroloji obj)
        {
            this.List.Insert(index, obj);
        }

        public bool Contains(Astroloji obj)
        {
            return this.List.Contains(obj);
        }

        public int IndexOf(Astroloji obj)
        {
            return this.List.IndexOf(obj);
        }

        public void Remove(Astroloji obj)
        {
            this.List.Remove(obj);
        }
    }

    public partial class Astroloji : IDisposable
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
        private byte m_BurcID;
        public byte BurcID
        {
            get { return m_BurcID; }
            set { m_BurcID = value; }
        }
        private string m_Icerik;
        public string Icerik
        {
            get { return m_Icerik; }
            set { m_Icerik = value; }
        }
        private DateTime m_KayitTarihi;
        public DateTime KayitTarihi
        {
            get { return m_KayitTarihi; }
            set { m_KayitTarihi = value; }
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

        public Astroloji()
        {
        }

        /// <summary>
        /// Astroloji Nesnesi Oluþtur
        /// </summary>
        public Astroloji(Int64 pid, System.String phesapid, byte pburcid, string picerik, DateTime pkayittarihi, bool pyoneticionay, bool paktif)
        {
            this.m_ID = pid;
            this.m_HesapID = phesapid;
            this.m_BurcID = pburcid;
            this.m_Icerik = picerik;
            this.m_KayitTarihi = pkayittarihi;
            this.m_YoneticiOnay = pyoneticionay;
            this.m_Aktif = paktif;
        }
    }

    public partial class AstrolojiMethods
    {
        ///<summary>
        /// Astroloji Data PrimaryKey
        ///</summary>
        public static Astroloji GetAstroloji(Int64 pid)
        {
            Astroloji rvAstroloji = new Astroloji();
            using (MConnection conneciton = new MConnection(MClientProvider.MySQL))
            {
                switch (conneciton.State)
                {
                    case System.Data.ConnectionState.Closed:
                        conneciton.Open();
                        break;
                }
                using (MCommand cmd = new MCommand(CommandType.Text, "select * from astroloji where id=?id", conneciton))
                {
                    cmd.Parameters.Add("id", pid, MSqlDbType.BigInt);
                    using (IDataReader IDR = cmd.ExecuteReader())
                    {
                        while (IDR.Read())
                            rvAstroloji = new Astroloji(MConvert.NullToInt64(IDR["id"]), MConvert.NullToGuidString(IDR["hesapid"]), MConvert.NullToByte(IDR["burcid"]), MConvert.NullToString(IDR["icerik"]), MConvert.NullToDateTime(IDR["kayittarihi"]), MConvert.NullToBool(IDR["yoneticionay"]), MConvert.NullToBool(IDR["aktif"]));
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
            return rvAstroloji;
        }


        /// <summary>
        /// Astroloji Getir
        /// </summary>
        public static Astroloji GetAstroloji(CommandType cmdType, string sqlQuery, MParameterCollection parameters)
        {
            Astroloji rvAstroloji = new Astroloji();
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
                            rvAstroloji = new Astroloji(MConvert.NullToInt64(IDR["id"]), MConvert.NullToGuidString(IDR["hesapid"]), MConvert.NullToByte(IDR["burcid"]), MConvert.NullToString(IDR["icerik"]), MConvert.NullToDateTime(IDR["kayittarihi"]), MConvert.NullToBool(IDR["yoneticionay"]), MConvert.NullToBool(IDR["aktif"]));
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
            return rvAstroloji;
        }

        /// <summary>
        /// Astroloji Liste Getir
        /// </summary>
        public static AstrolojiCollection GetList(CommandType cmdType, string sqlQuery, MParameterCollection parameters)
        {
            AstrolojiCollection rvAstroloji = new AstrolojiCollection();
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
                            rvAstroloji.Add(new Astroloji(MConvert.NullToInt64(IDR["id"]), MConvert.NullToGuidString(IDR["hesapid"]), MConvert.NullToByte(IDR["burcid"]), MConvert.NullToString(IDR["icerik"]), MConvert.NullToDateTime(IDR["kayittarihi"]), MConvert.NullToBool(IDR["yoneticionay"]), MConvert.NullToBool(IDR["aktif"])));
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
            return rvAstroloji;
        }

        ///<summary>
        /// Astroloji Data Select
        ///</summary>
        public static AstrolojiCollection GetSelect()
        {
            AstrolojiCollection rvAstroloji = new AstrolojiCollection();
            using (MConnection conneciton = new MConnection(MClientProvider.MySQL))
            {
                switch (conneciton.State)
                {
                    case System.Data.ConnectionState.Closed:
                        conneciton.Open();
                        break;
                }
                using (MCommand cmd = new MCommand(CommandType.Text, "select * from astroloji", conneciton))
                {

                    using (IDataReader IDR = cmd.ExecuteReader())
                    {
                        while (IDR.Read())
                            rvAstroloji.Add(new Astroloji(MConvert.NullToInt64(IDR["id"]), MConvert.NullToGuidString(IDR["hesapid"]), MConvert.NullToByte(IDR["burcid"]), MConvert.NullToString(IDR["icerik"]), MConvert.NullToDateTime(IDR["kayittarihi"]), MConvert.NullToBool(IDR["yoneticionay"]), MConvert.NullToBool(IDR["aktif"])));
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
            return rvAstroloji;
        }


        ///<summary>
        /// Astroloji Data Insert
        ///</summary>
        public static int Insert(System.String phesapid, byte pburcid, string picerik, DateTime pkayittarihi, bool pyoneticionay, bool paktif)
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
                using (MCommand cmd = new MCommand(CommandType.Text, "insert into astroloji (hesapid,burcid,icerik,kayittarihi,yoneticionay,aktif) values(?hesapid,?burcid,?icerik,?kayittarihi,?yoneticionay,?aktif)", conneciton))
                {
                    cmd.Parameters.Add("hesapid", phesapid, MSqlDbType.VarChar);
                    cmd.Parameters.Add("burcid", pburcid, MSqlDbType.SmallInt);
                    cmd.Parameters.Add("icerik", picerik, MSqlDbType.VarChar);
                    cmd.Parameters.Add("kayittarihi", pkayittarihi, MSqlDbType.DateTime);
                    cmd.Parameters.Add("yoneticionay", pyoneticionay, MSqlDbType.Boolean);
                    cmd.Parameters.Add("aktif", paktif, MSqlDbType.Boolean);
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
        /// Astroloji Data Insert
        ///</summary>
        public static int Insert(Astroloji p)
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
                using (MCommand cmd = new MCommand(CommandType.Text, "insert into astroloji (hesapid,burcid,icerik,kayittarihi,yoneticionay,aktif) values(?hesapid,?burcid,?icerik,?kayittarihi,?yoneticionay,?aktif)", conneciton))
                {
                    cmd.Parameters.Add("hesapid", p.HesapID, MSqlDbType.VarChar);
                    cmd.Parameters.Add("burcid", p.BurcID, MSqlDbType.SmallInt);
                    cmd.Parameters.Add("icerik", p.Icerik, MSqlDbType.VarChar);
                    cmd.Parameters.Add("kayittarihi", p.KayitTarihi, MSqlDbType.DateTime);
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
        /// Astroloji Data Update
        ///</summary>
        public static int Update(Int64 pid, System.String phesapid, byte pburcid, string picerik, DateTime pkayittarihi, bool pyoneticionay, bool paktif)
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
                using (MCommand cmd = new MCommand(CommandType.Text, "update astroloji set hesapid=?hesapid,burcid=?burcid,icerik=?icerik,kayittarihi=?kayittarihi,yoneticionay=?yoneticionay,aktif=?aktif where id=?id", conneciton))
                {
                    cmd.Parameters.Add("id", pid, MSqlDbType.BigInt);
                    cmd.Parameters.Add("hesapid", phesapid, MSqlDbType.VarChar);
                    cmd.Parameters.Add("burcid", pburcid, MSqlDbType.SmallInt);
                    cmd.Parameters.Add("icerik", picerik, MSqlDbType.VarChar);
                    cmd.Parameters.Add("kayittarihi", pkayittarihi, MSqlDbType.DateTime);
                    cmd.Parameters.Add("yoneticionay", pyoneticionay, MSqlDbType.Boolean);
                    cmd.Parameters.Add("aktif", paktif, MSqlDbType.Boolean);
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
        /// Astroloji Data Update
        ///</summary>
        public static int Update(Astroloji p)
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
                using (MCommand cmd = new MCommand(CommandType.Text, "update astroloji set hesapid=?hesapid,burcid=?burcid,icerik=?icerik,kayittarihi=?kayittarihi,yoneticionay=?yoneticionay,aktif=?aktif where id=?id", conneciton))
                {
                    cmd.Parameters.Add("id", p.ID, MSqlDbType.BigInt);
                    cmd.Parameters.Add("hesapid", p.HesapID, MSqlDbType.VarChar);
                    cmd.Parameters.Add("burcid", p.BurcID, MSqlDbType.SmallInt);
                    cmd.Parameters.Add("icerik", p.Icerik, MSqlDbType.VarChar);
                    cmd.Parameters.Add("kayittarihi", p.KayitTarihi, MSqlDbType.DateTime);
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
        /// Astroloji Data Delete
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
                using (MCommand cmd = new MCommand(CommandType.Text, "delete from astroloji where id=?id", conneciton))
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
        /// Astroloji Data Delete
        ///</summary>
        public static int Delete(Astroloji p)
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
                using (MCommand cmd = new MCommand(CommandType.Text, "delete from astroloji where id=?id", conneciton))
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
