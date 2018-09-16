using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Data;
using BAYMYO.MultiSQLClient;

namespace baymyoStatic
{
    public partial class SehirCollection : CollectionBase, IDisposable
    {
        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }

        public Sehir this[int index]
        {
            get { return (Sehir)this.List[index]; }
            set { this.List[index] = value; }
        }

        public object SyncRoot { get { return this.List.SyncRoot; } }

        public int Add(Sehir obj)
        {
            return this.List.Add(obj);
        }

        public void Insert(int index, Sehir obj)
        {
            this.List.Insert(index, obj);
        }

        public bool Contains(Sehir obj)
        {
            return this.List.Contains(obj);
        }

        public int IndexOf(Sehir obj)
        {
            return this.List.IndexOf(obj);
        }

        public void Remove(Sehir obj)
        {
            this.List.Remove(obj);
        }
    }

    public partial class Sehir : IDisposable
    {
        #region ---IDisposable Members---
        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }
        #endregion

        #region ---Properties/Field - Özellikler Alanlar---
        private byte m_ID;
        public byte ID
        {
            get { return m_ID; }
            set { m_ID = value; }
        }
        private string m_Adi;
        public string Adi
        {
            get { return m_Adi; }
            set { m_Adi = value; }
        }

        #endregion

        public Sehir()
        {
        }

        /// <summary>
        /// Sehir Nesnesi Oluþtur
        /// </summary>
        public Sehir(byte pid, string padi)
        {
            this.m_ID = pid;
            this.m_Adi = padi.ToUpper();
        }
    }

    public partial class SehirMethods
    {
        ///<summary>
        /// Sehir Data PrimaryKey
        ///</summary>
        public static Sehir GetSehir(byte pid)
        {
            Sehir rvSehir = new Sehir();
            using (MConnection conneciton = new MConnection(MClientProvider.MySQL))
            {
                switch (conneciton.State)
                {
                    case System.Data.ConnectionState.Closed:
                        conneciton.Open();
                        break;
                }
                using (MCommand cmd = new MCommand(CommandType.Text, "select * from sehir where id=?id limit 1", conneciton))
                {
                    cmd.Parameters.Add("id", pid, MSqlDbType.SmallInt);
                    using (IDataReader IDR = cmd.ExecuteReader())
                    {
                        while (IDR.Read())
                            rvSehir = new Sehir(MConvert.NullToByte(IDR["id"]), MConvert.NullToString(IDR["adi"]));
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
            return rvSehir;
        }

        /// <summary>
        /// Sehir Getir
        /// </summary>
        public static Sehir GetSehir(CommandType cmdType, string sqlQuery, MParameterCollection parameters)
        {
            Sehir rvSehir = new Sehir();
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
                            rvSehir = new Sehir(MConvert.NullToByte(IDR["id"]), MConvert.NullToString(IDR["adi"]));
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
            return rvSehir;
        }

        /// <summary>
        /// Sehir Liste Getir
        /// </summary>
        public static SehirCollection GetList(CommandType cmdType, string sqlQuery, MParameterCollection parameters)
        {
            SehirCollection rvSehir = new SehirCollection();
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
                            rvSehir.Add(new Sehir(MConvert.NullToByte(IDR["id"]), MConvert.NullToString(IDR["adi"])));
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
            return rvSehir;
        }

        ///<summary>
        /// Sehir Data Select
        ///</summary>
        public static SehirCollection GetSelect()
        {
            SehirCollection rvSehir = new SehirCollection();
            using (MConnection conneciton = new MConnection(MClientProvider.MySQL))
            {
                switch (conneciton.State)
                {
                    case System.Data.ConnectionState.Closed:
                        conneciton.Open();
                        break;
                }
                using (MCommand cmd = new MCommand(CommandType.Text, "select * from sehir order by adi asc", conneciton))
                {

                    using (IDataReader IDR = cmd.ExecuteReader())
                    {
                        while (IDR.Read())
                            rvSehir.Add(new Sehir(MConvert.NullToByte(IDR["id"]), MConvert.NullToString(IDR["adi"])));
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
            return rvSehir;
        }

        ///<summary>
        /// Sehir Data Insert
        ///</summary>
        public static int Insert(string padi)
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
                using (MCommand cmd = new MCommand(CommandType.Text, "insert into sehir (adi) values(?adi)", conneciton))
                {
                    cmd.Parameters.Add("adi", padi, MSqlDbType.VarChar);
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
        /// Sehir Data Insert
        ///</summary>
        public static int Insert(Sehir p)
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
                using (MCommand cmd = new MCommand(CommandType.Text, "insert into sehir (adi) values(?adi)", conneciton))
                {
                    cmd.Parameters.Add("adi", p.Adi, MSqlDbType.VarChar);
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
        /// Sehir Data Update
        ///</summary>
        public static int Update(byte pid, string padi)
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
                using (MCommand cmd = new MCommand(CommandType.Text, "update sehir set adi=?adi where id=?id", conneciton))
                {
                    cmd.Parameters.Add("id", pid, MSqlDbType.SmallInt);
                    cmd.Parameters.Add("adi", padi, MSqlDbType.VarChar);
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
        /// Sehir Data Update
        ///</summary>
        public static int Update(Sehir p)
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
                using (MCommand cmd = new MCommand(CommandType.Text, "update sehir set adi=?adi where id=?id", conneciton))
                {
                    cmd.Parameters.Add("id", p.ID, MSqlDbType.SmallInt);
                    cmd.Parameters.Add("adi", p.Adi, MSqlDbType.VarChar);
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
        /// Sehir Data Delete
        ///</summary>
        public static int Delete(byte pid)
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
                using (MCommand cmd = new MCommand(CommandType.Text, "delete from sehir where id=?id", conneciton))
                {
                    cmd.Parameters.Add("id", pid, MSqlDbType.SmallInt);
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
        /// Sehir Data Delete
        ///</summary>
        public static int Delete(Sehir p)
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
                using (MCommand cmd = new MCommand(CommandType.Text, "delete from sehir where id=?id", conneciton))
                {
                    cmd.Parameters.Add("id", p.ID, MSqlDbType.SmallInt);
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
