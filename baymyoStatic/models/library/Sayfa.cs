using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Data;
using BAYMYO.MultiSQLClient;

namespace baymyoStatic
{
    public partial class SayfaCollection : CollectionBase, IDisposable
    {
        public void Dispose()
        {
            if (this.Count > 0)
                this.Clear();
            GC.SuppressFinalize(this);
        }

        public Sayfa this[int index]
        {
            get { return (Sayfa)this.List[index]; }
            set { this.List[index] = value; }
        }

        public object SyncRoot { get { return this.List.SyncRoot; } }

        public int Add(Sayfa obj)
        {
            return this.List.Add(obj);
        }

        public void Insert(int index, Sayfa obj)
        {
            this.List.Insert(index, obj);
        }

        public bool Contains(Sayfa obj)
        {
            return this.List.Contains(obj);
        }

        public int IndexOf(Sayfa obj)
        {
            return this.List.IndexOf(obj);
        }

        public void Remove(Sayfa obj)
        {
            this.List.Remove(obj);
        }
    }

    public partial class Sayfa : IDisposable
    {
        #region ---IDisposable Members---
        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }
        #endregion

        #region ---Properties/Field - Özellikler Alanlar---
        private Int16 m_ID;
        public Int16 ID
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
        private string m_Dil;
        public string Dil
        {
            get { return m_Dil; }
            set { m_Dil = value; }
        }
        private DateTime m_KayitTarihi;
        public DateTime KayitTarihi
        {
            get { return m_KayitTarihi; }
            set { m_KayitTarihi = value; }
        }
        private byte m_Yerlesim;
        public byte Yerlesim
        {
            get { return m_Yerlesim; }
            set { m_Yerlesim = value; }
        }
        private bool m_Aktif;
        public bool Aktif
        {
            get { return m_Aktif; }
            set { m_Aktif = value; }
        }

        #endregion

        public Sayfa()
        {
        }

        /// <summary>
        /// Sayfa Nesnesi Oluþtur
        /// </summary>
        public Sayfa(Int16 pid, System.String phesapid, string pbaslik, string picerik, string pdil, DateTime pkayittarihi, byte pyerlesim, bool paktif)
        {
            this.m_ID = pid;
            this.m_HesapID = phesapid;
            this.m_Baslik = pbaslik;
            this.m_Icerik = picerik;
            this.m_Dil = pdil;
            this.m_KayitTarihi = pkayittarihi;
            this.m_Yerlesim = pyerlesim;
            this.m_Aktif = paktif;
        }
    }

    public partial class SayfaMethods
    {
        ///<summary>
        /// Sayfa Data PrimaryKey
        ///</summary>
        public static Sayfa GetSayfa(Int16 pid)
        {
            Sayfa rvSayfa = new Sayfa();
            using (MConnection conneciton = new MConnection(MClientProvider.MySQL))
            {
                switch (conneciton.State)
                {
                    case System.Data.ConnectionState.Closed:
                        conneciton.Open();
                        break;
                }
                using (MCommand cmd = new MCommand(CommandType.Text, "select * from sayfa where id=?id limit 1", conneciton))
                {
                    cmd.Parameters.Add("id", pid, MSqlDbType.SmallInt);
                    using (IDataReader IDR = cmd.ExecuteReader())
                    {
                        while (IDR.Read())
                            rvSayfa = new Sayfa(MConvert.NullToInt16(IDR["id"]), MConvert.NullToGuidString(IDR["hesapid"]), MConvert.NullToString(IDR["baslik"]), MConvert.NullToString(IDR["icerik"]), MConvert.NullToString(IDR["dil"]), MConvert.NullToDateTime(IDR["kayittarihi"]), MConvert.NullToByte(IDR["yerlesim"]), MConvert.NullToBool(IDR["aktif"]));
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
            return rvSayfa;
        }

        /// <summary>
        /// Sayfa Getir
        /// </summary>
        public static Sayfa GetSayfa(CommandType cmdType, string sqlQuery, MParameterCollection parameters)
        {
            Sayfa rvSayfa = new Sayfa();
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
                            rvSayfa = new Sayfa(MConvert.NullToInt16(IDR["id"]), MConvert.NullToGuidString(IDR["hesapid"]), MConvert.NullToString(IDR["baslik"]), MConvert.NullToString(IDR["icerik"]), MConvert.NullToString(IDR["dil"]), MConvert.NullToDateTime(IDR["kayittarihi"]), MConvert.NullToByte(IDR["yerlesim"]), MConvert.NullToBool(IDR["aktif"]));
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
            return rvSayfa;
        }

        /// <summary>
        /// Sayfa Liste Getir
        /// </summary>
        public static SayfaCollection GetList(CommandType cmdType, string sqlQuery, MParameterCollection parameters)
        {
            SayfaCollection rvSayfa = new SayfaCollection();
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
                            rvSayfa.Add(new Sayfa(MConvert.NullToInt16(IDR["id"]), MConvert.NullToGuidString(IDR["hesapid"]), MConvert.NullToString(IDR["baslik"]), MConvert.NullToString(IDR["icerik"]), MConvert.NullToString(IDR["dil"]), MConvert.NullToDateTime(IDR["kayittarihi"]), MConvert.NullToByte(IDR["yerlesim"]), MConvert.NullToBool(IDR["aktif"])));
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
            return rvSayfa;
        }

        ///<summary>
        /// Sayfa Data Select
        ///</summary>
        public static SayfaCollection GetSelect(byte yerlesim, byte limit)
        {
            SayfaCollection rvSayfa = new SayfaCollection();
            using (MConnection conneciton = new MConnection(MClientProvider.MySQL))
            {
                switch (conneciton.State)
                {
                    case System.Data.ConnectionState.Closed:
                        conneciton.Open();
                        break;
                }
                using (MCommand cmd = new MCommand(CommandType.Text, "select * from sayfa where aktif=1 and yerlesim=5 or aktif=1 and yerlesim=?yerlesim limit " + limit, conneciton))
                {
                    cmd.Parameters.Add("yerlesim", yerlesim, MSqlDbType.SmallInt);
                    using (IDataReader IDR = cmd.ExecuteReader())
                    {
                        while (IDR.Read())
                            rvSayfa.Add(new Sayfa(MConvert.NullToInt16(IDR["id"]), MConvert.NullToGuidString(IDR["hesapid"]), MConvert.NullToString(IDR["baslik"]), MConvert.NullToString(IDR["icerik"]), MConvert.NullToString(IDR["dil"]), MConvert.NullToDateTime(IDR["kayittarihi"]), MConvert.NullToByte(IDR["yerlesim"]), MConvert.NullToBool(IDR["aktif"])));
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
            return rvSayfa;
        }

        ///<summary>
        /// Sayfa Data Insert
        ///</summary>
        public static int Insert(Sayfa p)
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
                using (MCommand cmd = new MCommand(CommandType.Text, "insert into sayfa (hesapid,baslik,icerik,dil,kayittarihi,yerlesim,aktif) values(?hesapid,?baslik,?icerik,?dil,?kayittarihi,?yerlesim,?aktif)", conneciton))
                {
                    cmd.Parameters.Add("hesapid", p.HesapID, MSqlDbType.VarChar);
                    cmd.Parameters.Add("baslik", p.Baslik, MSqlDbType.VarChar);
                    cmd.Parameters.Add("icerik", p.Icerik, MSqlDbType.LongText);
                    cmd.Parameters.Add("dil", p.Dil, MSqlDbType.VarChar);
                    cmd.Parameters.Add("kayittarihi", p.KayitTarihi, MSqlDbType.DateTime);
                    cmd.Parameters.Add("yerlesim", p.Yerlesim, MSqlDbType.SmallInt);
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
        /// Sayfa Data Update
        ///</summary>
        public static int Update(Sayfa p)
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
                using (MCommand cmd = new MCommand(CommandType.Text, "update sayfa set hesapid=?hesapid,baslik=?baslik,icerik=?icerik,dil=?dil,kayittarihi=?kayittarihi,yerlesim=?yerlesim,aktif=?aktif where id=?id", conneciton))
                {
                    cmd.Parameters.Add("id", p.ID, MSqlDbType.SmallInt);
                    cmd.Parameters.Add("hesapid", p.HesapID, MSqlDbType.VarChar);
                    cmd.Parameters.Add("baslik", p.Baslik, MSqlDbType.VarChar);
                    cmd.Parameters.Add("icerik", p.Icerik, MSqlDbType.LongText);
                    cmd.Parameters.Add("dil", p.Dil, MSqlDbType.VarChar);
                    cmd.Parameters.Add("kayittarihi", p.KayitTarihi, MSqlDbType.DateTime);
                    cmd.Parameters.Add("yerlesim", p.Yerlesim, MSqlDbType.SmallInt);
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
        /// Sayfa Data Delete
        ///</summary>
        public static int Delete(Int16 pid)
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
                using (MCommand cmd = new MCommand(CommandType.Text, "delete from sayfa where id=?id", conneciton))
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
        /// Sayfa Data Delete
        ///</summary>
        public static int Delete(Sayfa p)
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
                using (MCommand cmd = new MCommand(CommandType.Text, "delete from sayfa where id=?id", conneciton))
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
