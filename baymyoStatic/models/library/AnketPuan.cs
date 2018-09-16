using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Data;
using BAYMYO.MultiSQLClient;

namespace baymyoStatic
{
    public partial class AnketPuanCollection : CollectionBase, IDisposable
    {
        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }

        public AnketPuan this[int index]
        {
            get { return (AnketPuan)this.List[index]; }
            set { this.List[index] = value; }
        }

        public object SyncRoot { get { return this.List.SyncRoot; } }

        public int Add(AnketPuan obj)
        {
            return this.List.Add(obj);
        }

        public void Insert(int index, AnketPuan obj)
        {
            this.List.Insert(index, obj);
        }

        public bool Contains(AnketPuan obj)
        {
            return this.List.Contains(obj);
        }

        public int IndexOf(AnketPuan obj)
        {
            return this.List.IndexOf(obj);
        }

        public void Remove(AnketPuan obj)
        {
            this.List.Remove(obj);
        }
    }

    public partial class AnketPuan : IDisposable
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
        private Int64 m_AnketID;
        public Int64 AnketID
        {
            get { return m_AnketID; }
            set { m_AnketID = value; }
        }
        private Int64 m_SoruID;
        public Int64 SoruID
        {
            get { return m_SoruID; }
            set { m_SoruID = value; }
        }
        private string m_IP;
        public string IP
        {
            get { return m_IP; }
            set { m_IP = value; }
        }

        #endregion

        public AnketPuan()
        {
        }

        /// <summary>
        /// AnketPuan Nesnesi Oluþtur
        /// </summary>
        public AnketPuan(System.String pid, System.String phesapid, Int64 panketid, Int64 psoruid, string pip)
        {
            this.m_ID = pid;
            this.m_HesapID = phesapid;
            this.m_AnketID = panketid;
            this.m_SoruID = psoruid;
            this.m_IP = pip;
        }
    }

    public partial class AnketPuanMethods
    {
        public static AnketPuan GetAnketPuan(System.String pid)
        {
            AnketPuan rvAnketPuan = new AnketPuan();
            using (MConnection conneciton = new MConnection(MClientProvider.MySQL))
            {
                switch (conneciton.State)
                {
                    case System.Data.ConnectionState.Closed:
                        conneciton.Open();
                        break;
                }
                using (MCommand cmd = new MCommand(CommandType.Text, "select * from anketpuan where id=?id limit 1", conneciton))
                {
                    cmd.Parameters.Add("id", pid, MSqlDbType.VarChar);
                    using (IDataReader IDR = cmd.ExecuteReader())
                    {
                        while (IDR.Read())
                            rvAnketPuan = new AnketPuan(MConvert.NullToGuidString(IDR["id"]), MConvert.NullToGuidString(IDR["hesapid"]), MConvert.NullToInt64(IDR["anketid"]), MConvert.NullToInt64(IDR["soruid"]), MConvert.NullToString(IDR["ip"]));
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
            return rvAnketPuan;
        }
        public static AnketPuan GetAnketPuan(CommandType cmdType, string sqlQuery, MParameterCollection parameters)
        {
            AnketPuan rvAnketPuan = new AnketPuan();
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
                            rvAnketPuan = new AnketPuan(MConvert.NullToGuidString(IDR["id"]), MConvert.NullToGuidString(IDR["hesapid"]), MConvert.NullToInt64(IDR["anketid"]), MConvert.NullToInt64(IDR["soruid"]), MConvert.NullToString(IDR["ip"]));
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
            return rvAnketPuan;
        }

        public static AnketPuanCollection GetList(CommandType cmdType, string sqlQuery, MParameterCollection parameters)
        {
            AnketPuanCollection rvAnketPuan = new AnketPuanCollection();
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
                            rvAnketPuan.Add(new AnketPuan(MConvert.NullToGuidString(IDR["id"]), MConvert.NullToGuidString(IDR["hesapid"]), MConvert.NullToInt64(IDR["anketid"]), MConvert.NullToInt64(IDR["soruid"]), MConvert.NullToString(IDR["ip"])));
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
            return rvAnketPuan;
        }
        public static AnketPuanCollection GetSelect(Int64 psoruid)
        {
            AnketPuanCollection rvAnketPuan = new AnketPuanCollection();
            using (MConnection conneciton = new MConnection(MClientProvider.MySQL))
            {
                switch (conneciton.State)
                {
                    case System.Data.ConnectionState.Closed:
                        conneciton.Open();
                        break;
                }
                using (MCommand cmd = new MCommand(CommandType.Text, "select * from anketpuan where soruid=?soruid", conneciton))
                {
                    cmd.Parameters.Add("soruid", psoruid, MSqlDbType.BigInt);
                    using (IDataReader IDR = cmd.ExecuteReader())
                    {
                        while (IDR.Read())
                            rvAnketPuan.Add(new AnketPuan(MConvert.NullToGuidString(IDR["id"]), MConvert.NullToGuidString(IDR["hesapid"]), MConvert.NullToInt64(IDR["anketid"]), MConvert.NullToInt64(IDR["soruid"]), MConvert.NullToString(IDR["ip"])));
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
            return rvAnketPuan;
        }

        public static Int64 GetCount(Int64 panketid)
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
                using (MCommand cmd = new MCommand(CommandType.Text, "select count(ip) from anketpuan where anketid=?anketid", conneciton))
                {
                    cmd.Parameters.Add("anketid", panketid, MSqlDbType.BigInt);
                    rowsAffected = BAYMYO.UI.Converts.NullToInt64(cmd.ExecuteScalar());
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
        public static Int64 GetCount(Int64 panketid, string pip)
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
                using (MCommand cmd = new MCommand(CommandType.Text, "select count(ip) from anketpuan where anketid=?anketid and ip=?ip", conneciton))
                {
                    cmd.Parameters.Add("anketid", panketid, MSqlDbType.BigInt);
                    cmd.Parameters.Add("ip", pip, MSqlDbType.VarChar);
                    rowsAffected = BAYMYO.UI.Converts.NullToInt64(cmd.ExecuteScalar());
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

        public static int Insert(AnketPuan p)
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
                using (MCommand cmd = new MCommand(CommandType.Text, "insert into anketpuan (id,hesapid,anketid,soruid,ip) values(?id,?hesapid,?anketid,?soruid,?ip)", conneciton))
                {
                    cmd.Parameters.Add("id", Guid.NewGuid(), MSqlDbType.VarChar);
                    cmd.Parameters.Add("hesapid", p.HesapID, MSqlDbType.VarChar);
                    cmd.Parameters.Add("anketid", p.AnketID, MSqlDbType.BigInt);
                    cmd.Parameters.Add("soruid", p.SoruID, MSqlDbType.BigInt);
                    cmd.Parameters.Add("ip", p.IP, MSqlDbType.VarChar);
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
        public static int Update(AnketPuan p)
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
                using (MCommand cmd = new MCommand(CommandType.Text, "update anketpuan set hesapid=?hesapid,anketid=?anketid,soruid=?soruid,ip=?ip where id=?id", conneciton))
                {
                    cmd.Parameters.Add("id", p.ID, MSqlDbType.VarChar);
                    cmd.Parameters.Add("hesapid", p.HesapID, MSqlDbType.VarChar);
                    cmd.Parameters.Add("anketid", p.AnketID, MSqlDbType.BigInt);
                    cmd.Parameters.Add("soruid", p.SoruID, MSqlDbType.BigInt);
                    cmd.Parameters.Add("ip", p.IP, MSqlDbType.VarChar);
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
                using (MCommand cmd = new MCommand(CommandType.Text, "delete from anketpuan where id=?id", conneciton))
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
        public static int Delete(AnketPuan p)
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
                using (MCommand cmd = new MCommand(CommandType.Text, "delete from anketpuan where id=?id", conneciton))
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
        public static int Delete(Int64 anketid)
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
                using (MCommand cmd = new MCommand(CommandType.Text, "delete from anketpuan where anketid=?anketid", conneciton))
                {
                    cmd.Parameters.Add("anketid", anketid, MSqlDbType.BigInt);
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
        public static int Remove(Int64 soruid)
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
                using (MCommand cmd = new MCommand(CommandType.Text, "delete from anketpuan where soruid=?soruid", conneciton))
                {
                    cmd.Parameters.Add("soruid", soruid, MSqlDbType.BigInt);
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
