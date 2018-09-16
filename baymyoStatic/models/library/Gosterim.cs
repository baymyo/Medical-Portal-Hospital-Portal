using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Data;
using BAYMYO.MultiSQLClient;

namespace baymyoStatic
{
    public partial class GosterimCollection : CollectionBase, IDisposable
    {
        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }

        public Gosterim this[int index]
        {
            get { return (Gosterim)this.List[index]; }
            set { this.List[index] = value; }
        }

        public object SyncRoot { get { return this.List.SyncRoot; } }

        public int Add(Gosterim obj)
        {
            return this.List.Add(obj);
        }

        public void Insert(int index, Gosterim obj)
        {
            this.List.Insert(index, obj);
        }

        public bool Contains(Gosterim obj)
        {
            return this.List.Contains(obj);
        }

        public int IndexOf(Gosterim obj)
        {
            return this.List.IndexOf(obj);
        }

        public void Remove(Gosterim obj)
        {
            this.List.Remove(obj);
        }
    }

    public partial class Gosterim : IDisposable
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
        private System.String m_HesapID;
        public System.String HesapID
        {
            get { return m_HesapID; }
            set { m_HesapID = value; }
        }
        private string m_IP;
        public string IP
        {
            get { return m_IP; }
            set { m_IP = value; }
        }
        private string m_ModulID;
        public string ModulID
        {
            get { return m_ModulID; }
            set { m_ModulID = value; }
        }
        private string m_IcerikID;
        public string IcerikID
        {
            get { return m_IcerikID; }
            set { m_IcerikID = value; }
        }
        private DateTime m_KayitTarihi;
        public DateTime KayitTarihi
        {
            get { return m_KayitTarihi; }
            set { m_KayitTarihi = value; }
        }

        #endregion

        public Gosterim()
        {
        }

        /// <summary>
        /// Gosterim Nesnesi Oluþtur
        /// </summary>
        public Gosterim(System.String pid, System.String phesapid, string pip, string pmodulid, string picerikid, DateTime pkayittarihi)
        {
            this.m_ID = pid;
            this.m_HesapID = phesapid;
            this.m_IP = pip;
            this.m_ModulID = pmodulid;
            this.m_IcerikID = picerikid;
            this.m_KayitTarihi = pkayittarihi;
        }
    }

    public partial class GosterimMethods
    {
        ///<summary>
        /// Gosterim Data PrimaryKey
        ///</summary>
        public static Gosterim GetGosterim(System.String pid)
        {
            Gosterim rvGosterim = new Gosterim();
            using (MConnection conneciton = new MConnection(MClientProvider.MySQL))
            {
                switch (conneciton.State)
                {
                    case System.Data.ConnectionState.Closed:
                        conneciton.Open();
                        break;
                }
                using (MCommand cmd = new MCommand(CommandType.Text, "select * from gosterim where id=?id limit 1", conneciton))
                {
                    cmd.Parameters.Add("id", pid, MSqlDbType.VarChar);
                    using (IDataReader IDR = cmd.ExecuteReader())
                    {
                        while (IDR.Read())
                            rvGosterim = new Gosterim(MConvert.NullToGuidString(IDR["id"]), MConvert.NullToGuidString(IDR["hesapid"]), MConvert.NullToString(IDR["ip"]), MConvert.NullToString(IDR["modulid"]), MConvert.NullToString(IDR["icerikid"]), MConvert.NullToDateTime(IDR["kayittarihi"]));
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
            return rvGosterim;
        }

        /// <summary>
        /// Gosterim Getir
        /// </summary>
        public static Gosterim GetGosterim(CommandType cmdType, string sqlQuery, MParameterCollection parameters)
        {
            Gosterim rvGosterim = new Gosterim();
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
                            rvGosterim = new Gosterim(MConvert.NullToGuidString(IDR["id"]), MConvert.NullToGuidString(IDR["hesapid"]), MConvert.NullToString(IDR["ip"]), MConvert.NullToString(IDR["modulid"]), MConvert.NullToString(IDR["icerikid"]), MConvert.NullToDateTime(IDR["kayittarihi"]));
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
            return rvGosterim;
        }

        /// <summary>
        /// Gosterim Liste Getir
        /// </summary>
        public static GosterimCollection GetList(CommandType cmdType, string sqlQuery, MParameterCollection parameters)
        {
            GosterimCollection rvGosterim = new GosterimCollection();
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
                            rvGosterim.Add(new Gosterim(MConvert.NullToGuidString(IDR["id"]), MConvert.NullToGuidString(IDR["hesapid"]), MConvert.NullToString(IDR["ip"]), MConvert.NullToString(IDR["modulid"]), MConvert.NullToString(IDR["icerikid"]), MConvert.NullToDateTime(IDR["kayittarihi"])));
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
            return rvGosterim;
        }

        ///<summary>
        /// Gosterim Data Select
        ///</summary>
        public static GosterimCollection GetSelect()
        {
            GosterimCollection rvGosterim = new GosterimCollection();
            using (MConnection conneciton = new MConnection(MClientProvider.MySQL))
            {
                switch (conneciton.State)
                {
                    case System.Data.ConnectionState.Closed:
                        conneciton.Open();
                        break;
                }
                using (MCommand cmd = new MCommand(CommandType.Text, "select * from gosterim", conneciton))
                {

                    using (IDataReader IDR = cmd.ExecuteReader())
                    {
                        while (IDR.Read())
                            rvGosterim.Add(new Gosterim(MConvert.NullToGuidString(IDR["id"]), MConvert.NullToGuidString(IDR["hesapid"]), MConvert.NullToString(IDR["ip"]), MConvert.NullToString(IDR["modulid"]), MConvert.NullToString(IDR["icerikid"]), MConvert.NullToDateTime(IDR["kayittarihi"])));
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
            return rvGosterim;
        }

        ///<summary>
        /// Gosterim Data Count
        ///</summary>
        public static int Count(string modulid, object id)
        {
            int rvCount = 0;
            using (MConnection conneciton = new MConnection(MClientProvider.MySQL))
            {
                switch (conneciton.State)
                {
                    case System.Data.ConnectionState.Closed:
                        conneciton.Open();
                        break;
                }
                string query = string.Empty;
                switch (Settings.Site.CounterView)
                {
                    case CounterViewType.Multiple:
                        query = "select count(ip) as totalview from gosterim where modulid=?modulid and icerikid=?icerikid";
                        break;
                    default:
                        query = "select count(distinct ip) as totalview from gosterim where modulid=?modulid and icerikid=?icerikid";
                        break;
                }
                using (MCommand cmd = new MCommand(CommandType.Text, query, conneciton))
                {
                    cmd.Parameters.Add("modulid", modulid, MSqlDbType.VarChar);
                    cmd.Parameters.Add("icerikid", id, MSqlDbType.VarChar);
                    rvCount = MConvert.NullToInt(cmd.ExecuteScalar());
                }
                query = null;
                switch (conneciton.State)
                {
                    case System.Data.ConnectionState.Open:
                        conneciton.Close();
                        break;
                }
            }
            return rvCount;
        }

        ///<summary>
        /// Gosterim Data Insert
        ///</summary>
        public static int Insert(Gosterim p)
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
                using (MCommand cmd = new MCommand(CommandType.Text, "insert into gosterim (id,hesapid,ip,modulid,icerikid,kayittarihi) values(?id,?hesapid,?ip,?modulid,?icerikid,?kayittarihi)", conneciton))
                {
                    cmd.Parameters.Add("id", Guid.NewGuid(), MSqlDbType.VarChar);
                    cmd.Parameters.Add("hesapid", p.HesapID, MSqlDbType.VarChar);
                    cmd.Parameters.Add("ip", p.IP, MSqlDbType.VarChar);
                    cmd.Parameters.Add("modulid", p.ModulID, MSqlDbType.VarChar);
                    cmd.Parameters.Add("icerikid", p.IcerikID, MSqlDbType.VarChar);
                    cmd.Parameters.Add("kayittarihi", p.KayitTarihi, MSqlDbType.DateTime);
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
        /// Gosterim Data Update
        ///</summary>
        public static int Update(Gosterim p)
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
                using (MCommand cmd = new MCommand(CommandType.Text, "update gosterim set hesapid=?hesapid,ip=?ip,modulid=?modulid,icerikid=?icerikid,kayittarihi=?kayittarihi where id=?id", conneciton))
                {
                    cmd.Parameters.Add("id", p.ID, MSqlDbType.VarChar);
                    cmd.Parameters.Add("hesapid", p.HesapID, MSqlDbType.VarChar);
                    cmd.Parameters.Add("ip", p.IP, MSqlDbType.VarChar);
                    cmd.Parameters.Add("modulid", p.ModulID, MSqlDbType.VarChar);
                    cmd.Parameters.Add("icerikid", p.IcerikID, MSqlDbType.VarChar);
                    cmd.Parameters.Add("kayittarihi", p.KayitTarihi, MSqlDbType.DateTime);
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
        /// Gosterim Data Delete
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
                using (MCommand cmd = new MCommand(CommandType.Text, "delete from gosterim where id=?id", conneciton))
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
        /// Gosterim Data Delete
        ///</summary>
        public static int Delete(Gosterim p)
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
                using (MCommand cmd = new MCommand(CommandType.Text, "delete from gosterim where id=?id", conneciton))
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
    }
}
