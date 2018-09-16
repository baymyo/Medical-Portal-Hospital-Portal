using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Data;
using BAYMYO.MultiSQLClient;

namespace baymyoStatic
{
    public partial class AnketCollection : CollectionBase, IDisposable
    {
        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }

        public Anket this[int index]
        {
            get { return (Anket)this.List[index]; }
            set { this.List[index] = value; }
        }

        public object SyncRoot { get { return this.List.SyncRoot; } }

        public int Add(Anket obj)
        {
            return this.List.Add(obj);
        }

        public void Insert(int index, Anket obj)
        {
            this.List.Insert(index, obj);
        }

        public bool Contains(Anket obj)
        {
            return this.List.Contains(obj);
        }

        public int IndexOf(Anket obj)
        {
            return this.List.IndexOf(obj);
        }

        public void Remove(Anket obj)
        {
            this.List.Remove(obj);
        }
    }

    public partial class Anket : IDisposable
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
        private Int64 m_AnketID;
        public Int64 AnketID
        {
            get { return m_AnketID; }
            set { m_AnketID = value; }
        }
        private string m_Adi;
        public string Adi
        {
            get { return m_Adi; }
            set { m_Adi = value; }
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
        private string m_ResimUrl;
        public string ResimUrl
        {
            get { return m_ResimUrl; }
            set { m_ResimUrl = value; }
        }
        private string m_Grup;
        public string Grup
        {
            get { return m_Grup; }
            set { m_Grup = value; }
        }
        private bool m_Yatay;
        public bool Yatay
        {
            get { return m_Yatay; }
            set { m_Yatay = value; }
        }

        #endregion

        public Anket()
        {
        }

        /// <summary>
        /// Anket Nesnesi Oluþtur
        /// </summary>
        public Anket(Int64 pid, Int64 panketid, string padi, DateTime pkayittarihi, bool paktif, string presim, string pgrup, bool pyatay)
        {
            this.m_ID = pid;
            this.m_AnketID = panketid;
            this.m_Adi = padi;
            this.m_KayitTarihi = pkayittarihi;
            this.m_Aktif = paktif;
            this.m_ResimUrl = presim;
            this.m_Grup = pgrup;
            this.m_Yatay = pyatay;
        }
    }

    public partial class AnketMethods
    {
        ///<summary>
        /// Anket Data PrimaryKey
        ///</summary>
        public static Anket GetAnket(Int64 pid)
        {
            Anket rvAnket = new Anket();
            using (MConnection conneciton = new MConnection(MClientProvider.MySQL))
            {
                switch (conneciton.State)
                {
                    case System.Data.ConnectionState.Closed:
                        conneciton.Open();
                        break;
                }
                using (MCommand cmd = new MCommand(CommandType.Text, "select * from anket where id=?id limit 1", conneciton))
                {
                    cmd.Parameters.Add("id", pid, MSqlDbType.BigInt);
                    using (IDataReader IDR = cmd.ExecuteReader())
                    {
                        while (IDR.Read())
                            rvAnket = new Anket(MConvert.NullToInt64(IDR["id"]), MConvert.NullToInt64(IDR["anketid"]), MConvert.NullToString(IDR["adi"]), MConvert.NullToDateTime(IDR["kayittarihi"]), MConvert.NullToBool(IDR["aktif"]), MConvert.NullToString(IDR["resimurl"]), MConvert.NullToString(IDR["grup"]), MConvert.NullToBool(IDR["yatay"]));
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
            return rvAnket;
        }

        /// <summary>
        /// Anket Getir
        /// </summary>
        public static Anket GetAnket(CommandType cmdType, string sqlQuery, MParameterCollection parameters)
        {
            Anket rvAnket = new Anket();
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
                            rvAnket = new Anket(MConvert.NullToInt64(IDR["id"]), MConvert.NullToInt64(IDR["anketid"]), MConvert.NullToString(IDR["adi"]), MConvert.NullToDateTime(IDR["kayittarihi"]), MConvert.NullToBool(IDR["aktif"]), MConvert.NullToString(IDR["resimurl"]), MConvert.NullToString(IDR["grup"]), MConvert.NullToBool(IDR["yatay"]));
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
            return rvAnket;
        }

        /// <summary>
        /// Anket Liste Getir
        /// </summary>
        public static AnketCollection GetList(CommandType cmdType, string sqlQuery, MParameterCollection parameters)
        {
            AnketCollection rvAnket = new AnketCollection();
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
                            rvAnket.Add(new Anket(MConvert.NullToInt64(IDR["id"]), MConvert.NullToInt64(IDR["anketid"]), MConvert.NullToString(IDR["adi"]), MConvert.NullToDateTime(IDR["kayittarihi"]), MConvert.NullToBool(IDR["aktif"]), MConvert.NullToString(IDR["resimurl"]), MConvert.NullToString(IDR["grup"]), MConvert.NullToBool(IDR["yatay"])));
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
            return rvAnket;
        }

        ///<summary>
        /// Anket Data Select
        ///</summary>
        public static AnketCollection GetSelect(Int64 panketid)
        {
            AnketCollection rvAnket = new AnketCollection();
            using (MConnection conneciton = new MConnection(MClientProvider.MySQL))
            {
                switch (conneciton.State)
                {
                    case System.Data.ConnectionState.Closed:
                        conneciton.Open();
                        break;
                }
                using (MCommand cmd = new MCommand(CommandType.Text, "select * from anket where anketid=?anketid order by grup asc, id asc", conneciton))
                {
                    cmd.Parameters.Add("anketid", panketid, MSqlDbType.BigInt);
                    using (IDataReader IDR = cmd.ExecuteReader())
                    {
                        while (IDR.Read())
                            rvAnket.Add(new Anket(MConvert.NullToInt64(IDR["id"]), MConvert.NullToInt64(IDR["anketid"]), MConvert.NullToString(IDR["adi"]), MConvert.NullToDateTime(IDR["kayittarihi"]), MConvert.NullToBool(IDR["aktif"]), MConvert.NullToString(IDR["resimurl"]), MConvert.NullToString(IDR["grup"]), MConvert.NullToBool(IDR["yatay"])));
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
            return rvAnket;
        }
        public static AnketCollection GetSelect(Anket p)
        {
            AnketCollection rvAnket = new AnketCollection();
            using (MConnection conneciton = new MConnection(MClientProvider.MySQL))
            {
                switch (conneciton.State)
                {
                    case System.Data.ConnectionState.Closed:
                        conneciton.Open();
                        break;
                }
                using (MCommand cmd = new MCommand(CommandType.Text, "select * from anket where anketid=?anketid order by " + (BAYMYO.UI.Converts.NullToBool(p.Grup) ? "grup asc," : "") + " id asc", conneciton))
                {
                    cmd.Parameters.Add("anketid", p.ID, MSqlDbType.BigInt);
                    using (IDataReader IDR = cmd.ExecuteReader())
                    {
                        while (IDR.Read())
                            rvAnket.Add(new Anket(MConvert.NullToInt64(IDR["id"]), MConvert.NullToInt64(IDR["anketid"]), MConvert.NullToString(IDR["adi"]), MConvert.NullToDateTime(IDR["kayittarihi"]), MConvert.NullToBool(IDR["aktif"]), MConvert.NullToString(IDR["resimurl"]), MConvert.NullToString(IDR["grup"]), MConvert.NullToBool(IDR["yatay"])));
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
            return rvAnket;
        }

        ///<summary>
        /// Anket Data Insert
        ///</summary>
        public static Int64 Insert(Anket p)
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
                using (MCommand cmd = new MCommand(CommandType.Text, "insert into anket (anketid,adi,kayittarihi,aktif,resimurl,grup,yatay) values(?anketid,?adi,?kayittarihi,?aktif,?resimurl,?grup,?yatay); select last_insert_id();", conneciton))
                {
                    cmd.Parameters.Add("anketid", p.AnketID, MSqlDbType.BigInt);
                    cmd.Parameters.Add("adi", p.Adi, MSqlDbType.VarChar);
                    cmd.Parameters.Add("kayittarihi", p.KayitTarihi, MSqlDbType.DateTime);
                    cmd.Parameters.Add("aktif", p.Aktif, MSqlDbType.Boolean);
                    cmd.Parameters.Add("resimurl", p.ResimUrl, MSqlDbType.VarChar);
                    cmd.Parameters.Add("grup", p.Grup, MSqlDbType.VarChar);
                    cmd.Parameters.Add("yatay", p.Yatay, MSqlDbType.Boolean);
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
        /// Anket Data Update
        ///</summary>
        public static int Update(Anket p)
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
                using (MCommand cmd = new MCommand(CommandType.Text, "update anket set anketid=?anketid,adi=?adi,kayittarihi=?kayittarihi,aktif=?aktif,resimurl=?resimurl,grup=?grup,yatay=?yatay where id=?id", conneciton))
                {
                    cmd.Parameters.Add("id", p.ID, MSqlDbType.BigInt);
                    cmd.Parameters.Add("anketid", p.AnketID, MSqlDbType.BigInt);
                    cmd.Parameters.Add("adi", p.Adi, MSqlDbType.VarChar);
                    cmd.Parameters.Add("kayittarihi", p.KayitTarihi, MSqlDbType.DateTime);
                    cmd.Parameters.Add("aktif", p.Aktif, MSqlDbType.Boolean);
                    cmd.Parameters.Add("resimurl", p.ResimUrl, MSqlDbType.VarChar);
                    cmd.Parameters.Add("grup", p.Grup, MSqlDbType.VarChar);
                    cmd.Parameters.Add("yatay", p.Yatay, MSqlDbType.Boolean);
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
        /// Anket Data Delete
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
                using (MCommand cmd = new MCommand(CommandType.Text, "delete from anket where id=?id", conneciton))
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
        /// Anket Data Delete
        ///</summary>
        public static int Delete(Anket p)
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
                using (MCommand cmd = new MCommand(CommandType.Text, "delete from anket where id=?id", conneciton))
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

        ///<summary>
        /// Anket Data Remove
        ///</summary>
        public static int Remove(Int64 anketid)
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
                using (MCommand cmd = new MCommand(CommandType.Text, "delete from anket where anketid=?anketid", conneciton))
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
    }
}
