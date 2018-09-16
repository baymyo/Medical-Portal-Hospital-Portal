using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Data;
using BAYMYO.MultiSQLClient;

namespace baymyoStatic
{
    public partial class ResmiIlanCollection : CollectionBase, IDisposable
    {
        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }

        public ResmiIlan this[int index]
        {
            get { return (ResmiIlan)this.List[index]; }
            set { this.List[index] = value; }
        }

        public object SyncRoot { get { return this.List.SyncRoot; } }

        public int Add(ResmiIlan obj)
        {
            return this.List.Add(obj);
        }

        public void Insert(int index, ResmiIlan obj)
        {
            this.List.Insert(index, obj);
        }

        public bool Contains(ResmiIlan obj)
        {
            return this.List.Contains(obj);
        }

        public int IndexOf(ResmiIlan obj)
        {
            return this.List.IndexOf(obj);
        }

        public void Remove(ResmiIlan obj)
        {
            this.List.Remove(obj);
        }
    }

    public partial class ResmiIlan : IDisposable
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
        private string m_Baslik;
        public string Baslik
        {
            get { return m_Baslik; }
            set { m_Baslik = value; }
        }
        private string m_Icerik;
        public string Icerik
        {
            get { return m_Icerik; }
            set { m_Icerik = value; }
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
        private bool m_Aktif;
        public bool Aktif
        {
            get { return m_Aktif; }
            set { m_Aktif = value; }
        }

        #endregion

        public ResmiIlan()
        {
        }

        /// <summary>
        /// ResmiIlan Nesnesi Oluþtur
        /// </summary>
        public ResmiIlan(Int64 pid, string pbaslik, string picerik, string psehir, DateTime pkayittarihi, bool paktif)
        {
            this.m_ID = pid;
            this.m_Baslik = pbaslik;
            this.m_Icerik = picerik;
            this.m_Sehir = psehir;
            this.m_KayitTarihi = pkayittarihi;
            this.m_Aktif = paktif;
        }
    }

    public partial class ResmiIlanMethods
    {
        ///<summary>
        /// ResmiIlan Data PrimaryKey
        ///</summary>
        public static ResmiIlan GetResmiIlan(Int64 pid)
        {
            ResmiIlan rvResmiIlan = new ResmiIlan();
            using (MConnection conneciton = new MConnection(MClientProvider.MySQL))
            {
                switch (conneciton.State)
                {
                    case System.Data.ConnectionState.Closed:
                        conneciton.Open();
                        break;
                }
                using (MCommand cmd = new MCommand(CommandType.Text, "select * from resmiilan where id=?id limit 1", conneciton))
                {
                    cmd.Parameters.Add("id", pid, MSqlDbType.BigInt);
                    using (IDataReader IDR = cmd.ExecuteReader())
                    {
                        while (IDR.Read())
                            rvResmiIlan = new ResmiIlan(MConvert.NullToInt64(IDR["id"]), MConvert.NullToString(IDR["baslik"]), MConvert.NullToString(IDR["icerik"]), MConvert.NullToString(IDR["sehir"]), MConvert.NullToDateTime(IDR["kayittarihi"]), MConvert.NullToBool(IDR["aktif"]));
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
            return rvResmiIlan;
        }

        /// <summary>
        /// ResmiIlan Getir
        /// </summary>
        public static ResmiIlan GetResmiIlan(CommandType cmdType, string sqlQuery, MParameterCollection parameters)
        {
            ResmiIlan rvResmiIlan = new ResmiIlan();
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
                            rvResmiIlan = new ResmiIlan(MConvert.NullToInt64(IDR["id"]), MConvert.NullToString(IDR["baslik"]), MConvert.NullToString(IDR["icerik"]), MConvert.NullToString(IDR["sehir"]), MConvert.NullToDateTime(IDR["kayittarihi"]), MConvert.NullToBool(IDR["aktif"]));
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
            return rvResmiIlan;
        }

        /// <summary>
        /// ResmiIlan Liste Getir
        /// </summary>
        public static ResmiIlanCollection GetList(CommandType cmdType, string sqlQuery, MParameterCollection parameters)
        {
            ResmiIlanCollection rvResmiIlan = new ResmiIlanCollection();
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
                            rvResmiIlan.Add(new ResmiIlan(MConvert.NullToInt64(IDR["id"]), MConvert.NullToString(IDR["baslik"]), MConvert.NullToString(IDR["icerik"]), MConvert.NullToString(IDR["sehir"]), MConvert.NullToDateTime(IDR["kayittarihi"]), MConvert.NullToBool(IDR["aktif"])));
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
            return rvResmiIlan;
        }

        ///<summary>
        /// ResmiIlan Data Select
        ///</summary>
        public static ResmiIlanCollection GetSelect()
        {
            ResmiIlanCollection rvResmiIlan = new ResmiIlanCollection();
            using (MConnection conneciton = new MConnection(MClientProvider.MySQL))
            {
                switch (conneciton.State)
                {
                    case System.Data.ConnectionState.Closed:
                        conneciton.Open();
                        break;
                }
                using (MCommand cmd = new MCommand(CommandType.Text, "select * from resmiilan", conneciton))
                {

                    using (IDataReader IDR = cmd.ExecuteReader())
                    {
                        while (IDR.Read())
                            rvResmiIlan.Add(new ResmiIlan(MConvert.NullToInt64(IDR["id"]), MConvert.NullToString(IDR["baslik"]), MConvert.NullToString(IDR["icerik"]), MConvert.NullToString(IDR["sehir"]), MConvert.NullToDateTime(IDR["kayittarihi"]), MConvert.NullToBool(IDR["aktif"])));
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
            return rvResmiIlan;
        }

        ///<summary>
        /// ResmiIlan Data Insert
        ///</summary>
        public static Int64 Insert(ResmiIlan p)
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
                using (MCommand cmd = new MCommand(CommandType.Text, "insert into resmiilan (baslik,icerik,sehir,kayittarihi,aktif) values(?baslik,?icerik,?sehir,?kayittarihi,?aktif); select last_insert_id();", conneciton))
                {
                    cmd.Parameters.Add("baslik", p.Baslik, MSqlDbType.VarChar);
                    cmd.Parameters.Add("icerik", p.Icerik, MSqlDbType.LongText);
                    cmd.Parameters.Add("sehir", p.Sehir, MSqlDbType.VarChar);
                    cmd.Parameters.Add("kayittarihi", p.KayitTarihi, MSqlDbType.DateTime);
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
        /// ResmiIlan Data Update
        ///</summary>
        public static int Update(ResmiIlan p)
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
                using (MCommand cmd = new MCommand(CommandType.Text, "update resmiilan set baslik=?baslik,icerik=?icerik,sehir=?sehir,kayittarihi=?kayittarihi,aktif=?aktif where id=?id", conneciton))
                {
                    cmd.Parameters.Add("id", p.ID, MSqlDbType.BigInt);
                    cmd.Parameters.Add("baslik", p.Baslik, MSqlDbType.VarChar);
                    cmd.Parameters.Add("icerik", p.Icerik, MSqlDbType.LongText);
                    cmd.Parameters.Add("sehir", p.Sehir, MSqlDbType.VarChar);
                    cmd.Parameters.Add("kayittarihi", p.KayitTarihi, MSqlDbType.DateTime);
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
        /// ResmiIlan Data Delete
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
                using (MCommand cmd = new MCommand(CommandType.Text, "delete from resmiilan where id=?id", conneciton))
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
        /// ResmiIlan Data Delete
        ///</summary>
        public static int Delete(ResmiIlan p)
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
                using (MCommand cmd = new MCommand(CommandType.Text, "delete from resmiilan where id=?id", conneciton))
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
