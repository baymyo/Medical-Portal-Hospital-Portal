using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Data;
using BAYMYO.MultiSQLClient;

namespace baymyoStatic
{
    public partial class YorumCollection : CollectionBase, IDisposable
    {
        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }

        public Yorum this[int index]
        {
            get { return (Yorum)this.List[index]; }
            set { this.List[index] = value; }
        }

        public object SyncRoot { get { return this.List.SyncRoot; } }

        public int Add(Yorum obj)
        {
            return this.List.Add(obj);
        }

        public void Insert(int index, Yorum obj)
        {
            this.List.Insert(index, obj);
        }

        public bool Contains(Yorum obj)
        {
            return this.List.Contains(obj);
        }

        public int IndexOf(Yorum obj)
        {
            return this.List.IndexOf(obj);
        }

        public void Remove(Yorum obj)
        {
            this.List.Remove(obj);
        }
    }

    public partial class Yorum : IDisposable
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
        private string m_Adi;
        public string Adi
        {
            get { return m_Adi; }
            set { m_Adi = value; }
        }
        private string m_Mail;
        public string Mail
        {
            get { return m_Mail; }
            set { m_Mail = value; }
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

        public Yorum()
        {
        }

        /// <summary>
        /// Yorum Nesnesi Oluþtur
        /// </summary>
        public Yorum(System.String pid, string pip, string pmodulid, string picerikid, string padi, string pmail, string picerik, DateTime pkayittarihi, bool pyoneticionay, bool paktif)
        {
            this.m_ID = pid;
            this.m_IP = pip;
            this.m_ModulID = pmodulid;
            this.m_IcerikID = picerikid;
            this.m_Adi = padi;
            this.m_Mail = pmail;
            this.m_Icerik = picerik;
            this.m_KayitTarihi = pkayittarihi;
            this.m_YoneticiOnay = pyoneticionay;
            this.m_Aktif = paktif;
        }
    }

    public partial class YorumMethods
    {
        ///<summary>
        /// Yorum Data PrimaryKey
        ///</summary>
        public static Yorum GetYorum(System.String pid)
        {
            Yorum rvYorum = new Yorum();
            using (MConnection conneciton = new MConnection(MClientProvider.MySQL))
            {
                switch (conneciton.State)
                {
                    case System.Data.ConnectionState.Closed:
                        conneciton.Open();
                        break;
                }
                using (MCommand cmd = new MCommand(CommandType.Text, "select * from yorum where id=?id limit 1", conneciton))
                {
                    cmd.Parameters.Add("id", pid, MSqlDbType.VarChar);
                    using (IDataReader IDR = cmd.ExecuteReader())
                    {
                        while (IDR.Read())
                            rvYorum = new Yorum(MConvert.NullToGuidString(IDR["id"]), MConvert.NullToString(IDR["ip"]), MConvert.NullToString(IDR["modulid"]), MConvert.NullToString(IDR["icerikid"]), MConvert.NullToString(IDR["adi"]), MConvert.NullToString(IDR["mail"]), MConvert.NullToString(IDR["icerik"]), MConvert.NullToDateTime(IDR["kayittarihi"]), MConvert.NullToBool(IDR["yoneticionay"]), MConvert.NullToBool(IDR["aktif"]));
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
            return rvYorum;
        }


        /// <summary>
        /// Yorum Getir
        /// </summary>
        public static Yorum GetYorum(CommandType cmdType, string sqlQuery, MParameterCollection parameters)
        {
            Yorum rvYorum = new Yorum();
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
                            rvYorum = new Yorum(MConvert.NullToGuidString(IDR["id"]), MConvert.NullToString(IDR["ip"]), MConvert.NullToString(IDR["modulid"]), MConvert.NullToString(IDR["icerikid"]), MConvert.NullToString(IDR["adi"]), MConvert.NullToString(IDR["mail"]), MConvert.NullToString(IDR["icerik"]), MConvert.NullToDateTime(IDR["kayittarihi"]), MConvert.NullToBool(IDR["yoneticionay"]), MConvert.NullToBool(IDR["aktif"]));
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
            return rvYorum;
        }

        /// <summary>
        /// Yorum Liste Getir
        /// </summary>
        public static YorumCollection GetList(CommandType cmdType, string sqlQuery, MParameterCollection parameters)
        {
            YorumCollection rvYorum = new YorumCollection();
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
                            rvYorum.Add(new Yorum(MConvert.NullToGuidString(IDR["id"]), MConvert.NullToString(IDR["ip"]), MConvert.NullToString(IDR["modulid"]), MConvert.NullToString(IDR["icerikid"]), MConvert.NullToString(IDR["adi"]), MConvert.NullToString(IDR["mail"]), MConvert.NullToString(IDR["icerik"]), MConvert.NullToDateTime(IDR["kayittarihi"]), MConvert.NullToBool(IDR["yoneticionay"]), MConvert.NullToBool(IDR["aktif"])));
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
            return rvYorum;
        }

        ///<summary>
        /// Yorum Data Select
        ///</summary>
        public static YorumCollection GetSelect()
        {
            YorumCollection rvYorum = new YorumCollection();
            using (MConnection conneciton = new MConnection(MClientProvider.MySQL))
            {
                switch (conneciton.State)
                {
                    case System.Data.ConnectionState.Closed:
                        conneciton.Open();
                        break;
                }
                using (MCommand cmd = new MCommand(CommandType.Text, "select * from yorum", conneciton))
                {

                    using (IDataReader IDR = cmd.ExecuteReader())
                    {
                        while (IDR.Read())
                            rvYorum.Add(new Yorum(MConvert.NullToGuidString(IDR["id"]), MConvert.NullToString(IDR["ip"]), MConvert.NullToString(IDR["modulid"]), MConvert.NullToString(IDR["icerikid"]), MConvert.NullToString(IDR["adi"]), MConvert.NullToString(IDR["mail"]), MConvert.NullToString(IDR["icerik"]), MConvert.NullToDateTime(IDR["kayittarihi"]), MConvert.NullToBool(IDR["yoneticionay"]), MConvert.NullToBool(IDR["aktif"])));
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
            return rvYorum;
        }

        ///<summary>
        /// Yorum Data Count
        ///</summary>
        public static int Count(bool pyoneticionay)
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
                using (MCommand cmd = new MCommand(CommandType.Text, "select count(id) as totalcount from yorum where yoneticionay=?yoneticionay", conneciton))
                {
                    cmd.Parameters.Add("yoneticionay", pyoneticionay, MSqlDbType.Boolean);
                    rvCount = MConvert.NullToInt(cmd.ExecuteScalar());
                }
                switch (conneciton.State)
                {
                    case System.Data.ConnectionState.Open:
                        conneciton.Close();
                        break;
                }
            }
            return rvCount;
        }
        public static int Count(string pmail, bool paktif)
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
                using (MCommand cmd = new MCommand(CommandType.Text, "select count(id) as totalcount from yorum where mail=?mail and aktif=?aktif", conneciton))
                {
                    cmd.Parameters.Add("mail", pmail, MSqlDbType.VarChar);
                    cmd.Parameters.Add("aktif", paktif, MSqlDbType.Boolean);
                    rvCount = MConvert.NullToInt(cmd.ExecuteScalar());
                }
                switch (conneciton.State)
                {
                    case System.Data.ConnectionState.Open:
                        conneciton.Close();
                        break;
                }
            }
            return rvCount;
        }
        public static int Count(string picerikid)
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
                using (MCommand cmd = new MCommand(CommandType.Text, "select count(y.id) AS totalcount from yorum y where y.yoneticionay=1 and y.aktif=1 and y.icerikid=?icerikid", conneciton))
                {
                    cmd.Parameters.Add("icerikid", picerikid, BAYMYO.MultiSQLClient.MSqlDbType.VarChar);
                    rvCount = MConvert.NullToInt(cmd.ExecuteScalar());
                }
                switch (conneciton.State)
                {
                    case System.Data.ConnectionState.Open:
                        conneciton.Close();
                        break;
                }
            }
            return rvCount;
        }
        public static int Count(string phesapid, string purl, bool pyoneticionay, bool paktif)
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
                using (MCommand cmd = new MCommand(CommandType.Text, "select count(y.id) AS totalcount from yorum y where (y.yoneticionay=?yoneticionay and y.aktif=?aktif) and (exists(select id from makale where makale.hesapid=?hesapid) or exists(select id from mesaj where mesaj.hesapid=?hesapid) or exists(select id from video where video.hesapid=?hesapid) or y.icerikid=?url)", conneciton))
                {
                    cmd.Parameters.Add("hesapid", phesapid, BAYMYO.MultiSQLClient.MSqlDbType.VarChar);
                    cmd.Parameters.Add("url", purl, BAYMYO.MultiSQLClient.MSqlDbType.VarChar);
                    cmd.Parameters.Add("yoneticionay", pyoneticionay, MSqlDbType.Boolean);
                    cmd.Parameters.Add("aktif", paktif, MSqlDbType.Boolean);
                    rvCount = MConvert.NullToInt(cmd.ExecuteScalar());
                }
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
        /// Yorum Data Insert
        ///</summary>
        public static int Insert(Yorum p)
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
                using (MCommand cmd = new MCommand(CommandType.Text, "insert into yorum (id,ip,modulid,icerikid,adi,mail,icerik,kayittarihi,yoneticionay,aktif) values(?id,?ip,?modulid,?icerikid,?adi,?mail,?icerik,?kayittarihi,?yoneticionay,?aktif)", conneciton))
                {
                    cmd.Parameters.Add("id", Guid.NewGuid(), MSqlDbType.VarChar);
                    cmd.Parameters.Add("ip", p.IP, MSqlDbType.VarChar);
                    cmd.Parameters.Add("modulid", p.ModulID, MSqlDbType.VarChar);
                    cmd.Parameters.Add("icerikid", p.IcerikID, MSqlDbType.VarChar);
                    cmd.Parameters.Add("adi", p.Adi, MSqlDbType.VarChar);
                    cmd.Parameters.Add("mail", p.Mail, MSqlDbType.VarChar);
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

        public static int Publish(string pID)
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
                using (MCommand cmd = new MCommand(CommandType.Text, "update yorum set yoneticionay=?yoneticionay,aktif=?aktif where id=?id", conneciton))
                {
                    cmd.Parameters.Add("id", pID, MSqlDbType.VarChar);
                    cmd.Parameters.Add("yoneticionay", true, MSqlDbType.Boolean);
                    cmd.Parameters.Add("aktif", true, MSqlDbType.Boolean);
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
        public static int Update(Yorum p)
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
                using (MCommand cmd = new MCommand(CommandType.Text, "update yorum set ip=?ip,modulid=?modulid,icerikid=?icerikid,adi=?adi,mail=?mail,icerik=?icerik,kayittarihi=?kayittarihi,yoneticionay=?yoneticionay,aktif=?aktif where id=?id", conneciton))
                {
                    cmd.Parameters.Add("id", p.ID, MSqlDbType.VarChar);
                    cmd.Parameters.Add("ip", p.IP, MSqlDbType.VarChar);
                    cmd.Parameters.Add("modulid", p.ModulID, MSqlDbType.VarChar);
                    cmd.Parameters.Add("icerikid", p.IcerikID, MSqlDbType.VarChar);
                    cmd.Parameters.Add("adi", p.Adi, MSqlDbType.VarChar);
                    cmd.Parameters.Add("mail", p.Mail, MSqlDbType.VarChar);
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
        /// Yorum Data Delete
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
                using (MCommand cmd = new MCommand(CommandType.Text, "delete from yorum where id=?id", conneciton))
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
        /// Yorum Data Delete
        ///</summary>
        public static int Delete(Yorum p)
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
                using (MCommand cmd = new MCommand(CommandType.Text, "delete from yorum where id=?id", conneciton))
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
