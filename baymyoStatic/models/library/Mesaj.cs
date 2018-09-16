using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Data;
using BAYMYO.MultiSQLClient;

namespace baymyoStatic
{
    public partial class MesajCollection : CollectionBase, IDisposable
    {
        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }

        public Mesaj this[int index]
        {
            get { return (Mesaj)this.List[index]; }
            set { this.List[index] = value; }
        }

        public object SyncRoot { get { return this.List.SyncRoot; } }

        public int Add(Mesaj obj)
        {
            return this.List.Add(obj);
        }

        public void Insert(int index, Mesaj obj)
        {
            this.List.Insert(index, obj);
        }

        public bool Contains(Mesaj obj)
        {
            return this.List.Contains(obj);
        }

        public int IndexOf(Mesaj obj)
        {
            return this.List.IndexOf(obj);
        }

        public void Remove(Mesaj obj)
        {
            this.List.Remove(obj);
        }
    }

    public partial class Mesaj : IDisposable
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
        private string m_IP;
        public string IP
        {
            get { return m_IP; }
            set { m_IP = value; }
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
        private string m_Telefon;
        public string Telefon
        {
            get { return m_Telefon; }
            set { m_Telefon = value; }
        }
        private string m_Konu;
        public string Konu
        {
            get { return m_Konu; }
            set { m_Konu = value; }
        }
        private string m_Icerik;
        public string Icerik
        {
            get { return m_Icerik; }
            set { m_Icerik = value; }
        }
        private string m_Yanit;
        public string Yanit
        {
            get { return m_Yanit; }
            set { m_Yanit = value; }
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
        private byte m_Durum;
        public byte Durum
        {
            get { return m_Durum; }
            set { m_Durum = value; }
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

        public Mesaj()
        {
        }

        /// <summary>
        /// Mesaj Nesnesi Oluþtur
        /// </summary>
        public Mesaj(Int64 pid, System.String phesapid, string pip, string padi, string pmail, string ptelefon, string pkonu, string picerik, string pyanit, DateTime pkayittarihi, DateTime pguncellemetarihi, byte pdurum, bool pyoneticionay, bool paktif)
        {
            this.m_ID = pid;
            this.m_HesapID = phesapid;
            this.m_IP = pip;
            this.m_Adi = padi;
            this.m_Mail = pmail;
            this.m_Telefon = ptelefon;
            this.m_Konu = pkonu;
            this.m_Icerik = picerik;
            this.m_Yanit = pyanit;
            this.m_KayitTarihi = pkayittarihi;
            this.m_GuncellemeTarihi = pguncellemetarihi;
            this.m_Durum = pdurum;
            this.m_YoneticiOnay = pyoneticionay;
            this.m_Aktif = paktif;
        }
    }

    public partial class MesajMethods
    {
        ///<summary>
        /// Mesaj Data PrimaryKey
        ///</summary>
        public static Mesaj GetMesaj(Int64 pid)
        {
            Mesaj rvMesaj = new Mesaj();
            using (MConnection conneciton = new MConnection(MClientProvider.MySQL))
            {
                switch (conneciton.State)
                {
                    case System.Data.ConnectionState.Closed:
                        conneciton.Open();
                        break;
                }
                using (MCommand cmd = new MCommand(CommandType.Text, "select * from mesaj where id=?id limit 1", conneciton))
                {
                    cmd.Parameters.Add("id", pid, MSqlDbType.BigInt);
                    using (IDataReader IDR = cmd.ExecuteReader())
                    {
                        while (IDR.Read())
                            rvMesaj = new Mesaj(MConvert.NullToInt64(IDR["id"]), MConvert.NullToGuidString(IDR["hesapid"]), MConvert.NullToString(IDR["ip"]), MConvert.NullToString(IDR["adi"]), MConvert.NullToString(IDR["mail"]), MConvert.NullToString(IDR["telefon"]), MConvert.NullToString(IDR["konu"]), MConvert.NullToString(IDR["icerik"]), MConvert.NullToString(IDR["yanit"]), MConvert.NullToDateTime(IDR["kayittarihi"]), MConvert.NullToDateTime(IDR["guncellemetarihi"]), MConvert.NullToByte(IDR["durum"]), MConvert.NullToBool(IDR["yoneticionay"]), MConvert.NullToBool(IDR["aktif"]));
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
            return rvMesaj;
        }

        /// <summary>
        /// Mesaj Getir
        /// </summary>
        public static Mesaj GetMesaj(CommandType cmdType, string sqlQuery, MParameterCollection parameters)
        {
            Mesaj rvMesaj = new Mesaj();
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
                            rvMesaj = new Mesaj(MConvert.NullToInt64(IDR["id"]), MConvert.NullToGuidString(IDR["hesapid"]), MConvert.NullToString(IDR["ip"]), MConvert.NullToString(IDR["adi"]), MConvert.NullToString(IDR["mail"]), MConvert.NullToString(IDR["telefon"]), MConvert.NullToString(IDR["konu"]), MConvert.NullToString(IDR["icerik"]), MConvert.NullToString(IDR["yanit"]), MConvert.NullToDateTime(IDR["kayittarihi"]), MConvert.NullToDateTime(IDR["guncellemetarihi"]), MConvert.NullToByte(IDR["durum"]), MConvert.NullToBool(IDR["yoneticionay"]), MConvert.NullToBool(IDR["aktif"]));
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
            return rvMesaj;
        }

        /// <summary>
        /// Mesaj Liste Getir
        /// </summary>
        public static MesajCollection GetList(CommandType cmdType, string sqlQuery, MParameterCollection parameters)
        {
            MesajCollection rvMesaj = new MesajCollection();
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
                            rvMesaj.Add(new Mesaj(MConvert.NullToInt64(IDR["id"]), MConvert.NullToGuidString(IDR["hesapid"]), MConvert.NullToString(IDR["ip"]), MConvert.NullToString(IDR["adi"]), MConvert.NullToString(IDR["mail"]), MConvert.NullToString(IDR["telefon"]), MConvert.NullToString(IDR["konu"]), MConvert.NullToString(IDR["icerik"]), MConvert.NullToString(IDR["yanit"]), MConvert.NullToDateTime(IDR["kayittarihi"]), MConvert.NullToDateTime(IDR["guncellemetarihi"]), MConvert.NullToByte(IDR["durum"]), MConvert.NullToBool(IDR["yoneticionay"]), MConvert.NullToBool(IDR["aktif"])));
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
            return rvMesaj;
        }

        ///<summary>
        /// Mesaj Data Select
        ///</summary>
        public static MesajCollection GetSelect()
        {
            MesajCollection rvMesaj = new MesajCollection();
            using (MConnection conneciton = new MConnection(MClientProvider.MySQL))
            {
                switch (conneciton.State)
                {
                    case System.Data.ConnectionState.Closed:
                        conneciton.Open();
                        break;
                }
                using (MCommand cmd = new MCommand(CommandType.Text, "select * from mesaj", conneciton))
                {

                    using (IDataReader IDR = cmd.ExecuteReader())
                    {
                        while (IDR.Read())
                            rvMesaj.Add(new Mesaj(MConvert.NullToInt64(IDR["id"]), MConvert.NullToGuidString(IDR["hesapid"]), MConvert.NullToString(IDR["ip"]), MConvert.NullToString(IDR["adi"]), MConvert.NullToString(IDR["mail"]), MConvert.NullToString(IDR["telefon"]), MConvert.NullToString(IDR["konu"]), MConvert.NullToString(IDR["icerik"]), MConvert.NullToString(IDR["yanit"]), MConvert.NullToDateTime(IDR["kayittarihi"]), MConvert.NullToDateTime(IDR["guncellemetarihi"]), MConvert.NullToByte(IDR["durum"]), MConvert.NullToBool(IDR["yoneticionay"]), MConvert.NullToBool(IDR["aktif"])));
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
            return rvMesaj;
        }

        ///<summary>
        /// Mesaj Data Count
        ///</summary>
        public static int Count(byte pdurum, bool pyoneticionay)
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
                using (MCommand cmd = new MCommand(CommandType.Text, "select count(id) as totalcount from mesaj where durum=?durum and yoneticionay=?yoneticionay", conneciton))
                {
                    cmd.Parameters.Add("durum", pdurum, MSqlDbType.Byte);
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
        public static int Count(string phesapid, byte pdurum, bool paktif)
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
                using (MCommand cmd = new MCommand(CommandType.Text, "select count(id) as totalcount from mesaj where yoneticionay=1 and hesapid=?hesapid and durum=?durum and aktif=?aktif", conneciton))
                {
                    cmd.Parameters.Add("hesapid", phesapid, MSqlDbType.VarChar);
                    cmd.Parameters.Add("durum", pdurum, MSqlDbType.Byte);
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
        public static int Count(byte pdurum, string pmail, bool paktif)
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
                using (MCommand cmd = new MCommand(CommandType.Text, "select count(id) as totalcount from mesaj where mail=?mail and durum=?durum and aktif=?aktif", conneciton))
                {
                    cmd.Parameters.Add("mail", pmail, MSqlDbType.VarChar);
                    cmd.Parameters.Add("durum", pdurum, MSqlDbType.Byte);
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
        /// Mesaj Data Insert
        ///</summary>
        public static Int64 Insert(Mesaj p)
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
                using (MCommand cmd = new MCommand(CommandType.Text, "insert into mesaj (hesapid,ip,adi,mail,telefon,konu,icerik,yanit,kayittarihi,guncellemetarihi,durum,yoneticionay,aktif) values(?hesapid,?ip,?adi,?mail,?telefon,?konu,?icerik,?yanit,?kayittarihi,?guncellemetarihi,?durum,?yoneticionay,?aktif); select last_insert_id();", conneciton))
                {
                    cmd.Parameters.Add("hesapid", p.HesapID, MSqlDbType.VarChar);
                    cmd.Parameters.Add("ip", p.IP, MSqlDbType.VarChar);
                    cmd.Parameters.Add("adi", p.Adi, MSqlDbType.VarChar);
                    cmd.Parameters.Add("mail", p.Mail, MSqlDbType.VarChar);
                    cmd.Parameters.Add("telefon", p.Telefon, MSqlDbType.VarChar);
                    cmd.Parameters.Add("konu", p.Konu, MSqlDbType.VarChar);
                    cmd.Parameters.Add("icerik", p.Icerik, MSqlDbType.VarChar);
                    cmd.Parameters.Add("yanit", p.Yanit, MSqlDbType.VarChar);
                    cmd.Parameters.Add("kayittarihi", p.KayitTarihi, MSqlDbType.DateTime);
                    cmd.Parameters.Add("guncellemetarihi", p.GuncellemeTarihi, MSqlDbType.DateTime);
                    cmd.Parameters.Add("durum", p.Durum, MSqlDbType.SmallInt);
                    cmd.Parameters.Add("yoneticionay", p.YoneticiOnay, MSqlDbType.Boolean);
                    cmd.Parameters.Add("aktif", p.Aktif, MSqlDbType.Boolean);
                    rowsAffected = MConvert.NullToInt(cmd.ExecuteScalar());
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
        /// Mesaj Data Update
        ///</summary>
        public static int Update(Mesaj p)
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
                using (MCommand cmd = new MCommand(CommandType.Text, "update mesaj set hesapid=?hesapid,ip=?ip,adi=?adi,mail=?mail,telefon=?telefon,konu=?konu,icerik=?icerik,yanit=?yanit,kayittarihi=?kayittarihi,guncellemetarihi=?guncellemetarihi,durum=?durum,yoneticionay=?yoneticionay,aktif=?aktif where id=?id", conneciton))
                {
                    cmd.Parameters.Add("id", p.ID, MSqlDbType.BigInt);
                    cmd.Parameters.Add("hesapid", p.HesapID, MSqlDbType.VarChar);
                    cmd.Parameters.Add("ip", p.IP, MSqlDbType.VarChar);
                    cmd.Parameters.Add("adi", p.Adi, MSqlDbType.VarChar);
                    cmd.Parameters.Add("mail", p.Mail, MSqlDbType.VarChar);
                    cmd.Parameters.Add("telefon", p.Telefon, MSqlDbType.VarChar);
                    cmd.Parameters.Add("konu", p.Konu, MSqlDbType.VarChar);
                    cmd.Parameters.Add("icerik", p.Icerik, MSqlDbType.VarChar);
                    cmd.Parameters.Add("yanit", p.Yanit, MSqlDbType.VarChar);
                    cmd.Parameters.Add("kayittarihi", p.KayitTarihi, MSqlDbType.DateTime);
                    cmd.Parameters.Add("guncellemetarihi", p.GuncellemeTarihi, MSqlDbType.DateTime);
                    cmd.Parameters.Add("durum", p.Durum, MSqlDbType.SmallInt);
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
        /// Mesaj Data Delete
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
                using (MCommand cmd = new MCommand(CommandType.Text, "delete from mesaj where id=?id", conneciton))
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
        /// Mesaj Data Delete
        ///</summary>
        public static int Delete(Mesaj p)
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
                using (MCommand cmd = new MCommand(CommandType.Text, "delete from mesaj where id=?id", conneciton))
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
