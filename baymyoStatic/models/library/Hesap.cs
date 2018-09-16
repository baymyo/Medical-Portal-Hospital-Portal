using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Data;
using BAYMYO.MultiSQLClient;

namespace baymyoStatic
{
    public partial class HesapCollection : CollectionBase, IDisposable
    {
        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }

        public Hesap this[int index]
        {
            get { return (Hesap)this.List[index]; }
            set { this.List[index] = value; }
        }

        public object SyncRoot { get { return this.List.SyncRoot; } }

        public int Add(Hesap obj)
        {
            return this.List.Add(obj);
        }

        public void Insert(int index, Hesap obj)
        {
            this.List.Insert(index, obj);
        }

        public bool Contains(Hesap obj)
        {
            return this.List.Contains(obj);
        }

        public int IndexOf(Hesap obj)
        {
            return this.List.IndexOf(obj);
        }

        public void Remove(Hesap obj)
        {
            this.List.Remove(obj);
        }
    }

    public partial class Hesap : IDisposable
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
        private string m_Adi;
        public string Adi
        {
            get { return m_Adi; }
            set { m_Adi = value; }
        }
        private string m_Soyadi;
        public string Soyadi
        {
            get { return m_Soyadi; }
            set { m_Soyadi = value; }
        }
        private string m_Mail;
        public string Mail
        {
            get { return m_Mail; }
            set { m_Mail = value; }
        }
        private string m_Sifre;
        public string Sifre
        {
            get { return m_Sifre; }
            set { m_Sifre = value; }
        }
        private string m_Roller;
        public string Roller
        {
            get { return m_Roller; }
            set { m_Roller = value; }
        }
        private string m_OnayKodu;
        public string OnayKodu
        {
            get { return m_OnayKodu; }
            set { m_OnayKodu = value; }
        }
        private DateTime m_DogumTarihi;
        public DateTime DogumTarihi
        {
            get { return m_DogumTarihi; }
            set { m_DogumTarihi = value; }
        }
        private DateTime m_KayitTarihi;
        public DateTime KayitTarihi
        {
            get { return m_KayitTarihi; }
            set { m_KayitTarihi = value; }
        }
        private SexType m_Cinsiyet;
        public SexType Cinsiyet
        {
            get { return m_Cinsiyet; }
            set { m_Cinsiyet = value; }
        }
        private AccountType m_Tipi;
        public AccountType Tipi
        {
            get { return m_Tipi; }
            set { m_Tipi = value; }
        }
        private bool m_Yorum;
        public bool Yorum
        {
            get { return m_Yorum; }
            set { m_Yorum = value; }
        }
        private bool m_Abonelik;
        public bool Abonelik
        {
            get { return m_Abonelik; }
            set { m_Abonelik = value; }
        }
        private bool m_Aktivasyon;
        public bool Aktivasyon
        {
            get { return m_Aktivasyon; }
            set { m_Aktivasyon = value; }
        }
        private bool m_Aktif;
        public bool Aktif
        {
            get { return m_Aktif; }
            set { m_Aktif = value; }
        }
        private Profil m_ProfilObject;
        public Profil ProfilObject
        {
            get { return m_ProfilObject; }
            set { m_ProfilObject = value; }
        }
        #endregion

        public Hesap()
        {
            this.m_ID = this.m_IP = this.m_Adi = this.m_Soyadi = this.m_Mail = this.m_Sifre = this.m_Roller = string.Empty;
            this.m_ProfilObject = new Profil();
        }

        /// <summary>
        /// Hesap Nesnesi Oluþtur
        /// </summary>
        public Hesap(System.String pid, string pip, string padi, string psoyadi, string pmail, string psifre, string proller, string ponaykodu, DateTime pdogumtarihi, DateTime pkayittarihi, byte pcinsiyet, byte ptipi, bool pyorum, bool pabonelik, bool paktivasyon, bool paktif)
        {
            this.m_ID = pid;
            this.m_IP = pip;
            this.m_Adi = padi;
            this.m_Soyadi = psoyadi;
            this.m_Mail = pmail;
            this.m_Sifre = psifre;
            this.m_Roller = proller;
            this.m_OnayKodu = ponaykodu;
            this.m_DogumTarihi = pdogumtarihi;
            this.m_KayitTarihi = pkayittarihi;
            this.m_Cinsiyet = Core.GetSexType(pcinsiyet);
            this.m_Tipi = Core.GetAccountType(ptipi);
            this.m_Yorum = pyorum;
            this.m_Abonelik = pabonelik;
            this.m_Aktivasyon = paktivasyon;
            this.m_Aktif = paktif;
            this.m_ProfilObject = new Profil();
        }
        public Hesap(System.String pid, string pip, string padi, string psoyadi, string pmail, string psifre, string proller, string ponaykodu, DateTime pdogumtarihi, DateTime pkayittarihi, byte pcinsiyet, byte ptipi, bool pyorum, bool pabonelik, bool paktivasyon, bool paktif, bool isprofilobject)
        {
            this.m_ID = pid;
            this.m_IP = pip;
            this.m_Adi = padi;
            this.m_Soyadi = psoyadi;
            this.m_Mail = pmail;
            this.m_Sifre = psifre;
            this.m_Roller = proller;
            this.m_OnayKodu = ponaykodu;
            this.m_DogumTarihi = pdogumtarihi;
            this.m_KayitTarihi = pkayittarihi;
            this.m_Cinsiyet = Core.GetSexType(pcinsiyet);
            this.m_Tipi = Core.GetAccountType(ptipi);
            this.m_Yorum = pyorum;
            this.m_Abonelik = pabonelik;
            this.m_Aktivasyon = paktivasyon;
            this.m_Aktif = paktif;
            switch (isprofilobject)
            {
                case true:
                    this.m_ProfilObject = ProfilMethods.GetProfil(this.m_ID);
                    break;
            }
        }
    }

    public partial class HesapMethods
    {
        ///<summary>
        /// Hesap Data PrimaryKey
        ///</summary>
        public static Hesap GetHesap(System.String pid)
        {
            Hesap rvHesap = new Hesap();
            using (MConnection conneciton = new MConnection(MClientProvider.MySQL))
            {
                switch (conneciton.State)
                {
                    case System.Data.ConnectionState.Closed:
                        conneciton.Open();
                        break;
                }
                using (MCommand cmd = new MCommand(CommandType.Text, "select * from hesap where id=?id limit 1", conneciton))
                {
                    cmd.Parameters.Add("id", pid, MSqlDbType.VarChar);
                    using (IDataReader IDR = cmd.ExecuteReader())
                    {
                        while (IDR.Read())
                            rvHesap = new Hesap(MConvert.NullToGuidString(IDR["id"]), MConvert.NullToString(IDR["ip"]), MConvert.NullToString(IDR["adi"]), MConvert.NullToString(IDR["soyadi"]), MConvert.NullToString(IDR["mail"]), MConvert.NullToString(IDR["sifre"]), MConvert.NullToString(IDR["roller"]), MConvert.NullToString(IDR["onaykodu"]), MConvert.NullToDateTime(IDR["dogumtarihi"]), MConvert.NullToDateTime(IDR["kayittarihi"]), MConvert.NullToByte(IDR["cinsiyet"]), MConvert.NullToByte(IDR["tipi"]), MConvert.NullToBool(IDR["yorum"]), MConvert.NullToBool(IDR["abonelik"]), MConvert.NullToBool(IDR["aktivasyon"]), MConvert.NullToBool(IDR["aktif"]));
                        IDR.Close();
                    }
                }
                switch (conneciton.State)
                {
                    case System.Data.ConnectionState.Open:
                        conneciton.Close();
                        break;
                }
                rvHesap.ProfilObject = ProfilMethods.GetProfil(rvHesap.ID);
            }
            return rvHesap;
        }

        ///<summary>
        /// Hesap Data URL
        ///</summary>
        public static Hesap GetHesapUrl(string purl)
        {
            Hesap rvHesap = new Hesap();
            Profil tempProfil = ProfilMethods.GetProfilUrl(purl);
            if (tempProfil != null)
                using (MConnection conneciton = new MConnection(MClientProvider.MySQL))
                {
                    switch (conneciton.State)
                    {
                        case System.Data.ConnectionState.Closed:
                            conneciton.Open();
                            break;
                    }
                    using (MCommand cmd = new MCommand(CommandType.Text, "select * from hesap where id=?id limit 1", conneciton))
                    {
                        cmd.Parameters.Add("id", tempProfil.ID, MSqlDbType.VarChar);
                        using (IDataReader IDR = cmd.ExecuteReader())
                        {
                            while (IDR.Read())
                                rvHesap = new Hesap(MConvert.NullToGuidString(IDR["id"]), MConvert.NullToString(IDR["ip"]), MConvert.NullToString(IDR["adi"]), MConvert.NullToString(IDR["soyadi"]), MConvert.NullToString(IDR["mail"]), MConvert.NullToString(IDR["sifre"]), MConvert.NullToString(IDR["roller"]), MConvert.NullToString(IDR["onaykodu"]), MConvert.NullToDateTime(IDR["dogumtarihi"]), MConvert.NullToDateTime(IDR["kayittarihi"]), MConvert.NullToByte(IDR["cinsiyet"]), MConvert.NullToByte(IDR["tipi"]), MConvert.NullToBool(IDR["yorum"]), MConvert.NullToBool(IDR["abonelik"]), MConvert.NullToBool(IDR["aktivasyon"]), MConvert.NullToBool(IDR["aktif"]));
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
            rvHesap.ProfilObject = tempProfil;
            return rvHesap;
        }

        /// <summary>
        /// Hesap Getir
        /// </summary>
        public static Hesap GetHesap(CommandType cmdType, string sqlQuery, MParameterCollection parameters)
        {
            Hesap rvHesap = new Hesap();
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
                            rvHesap = new Hesap(MConvert.NullToGuidString(IDR["id"]), MConvert.NullToString(IDR["ip"]), MConvert.NullToString(IDR["adi"]), MConvert.NullToString(IDR["soyadi"]), MConvert.NullToString(IDR["mail"]), MConvert.NullToString(IDR["sifre"]), MConvert.NullToString(IDR["roller"]), MConvert.NullToString(IDR["onaykodu"]), MConvert.NullToDateTime(IDR["dogumtarihi"]), MConvert.NullToDateTime(IDR["kayittarihi"]), MConvert.NullToByte(IDR["cinsiyet"]), MConvert.NullToByte(IDR["tipi"]), MConvert.NullToBool(IDR["yorum"]), MConvert.NullToBool(IDR["abonelik"]), MConvert.NullToBool(IDR["aktivasyon"]), MConvert.NullToBool(IDR["aktif"]));
                        IDR.Close();
                    }
                }
                switch (conneciton.State)
                {
                    case System.Data.ConnectionState.Open:
                        conneciton.Close();
                        break;
                }
                rvHesap.ProfilObject = ProfilMethods.GetProfil(rvHesap.ID);
            }
            return rvHesap;
        }

        /// <summary>
        /// Hesap Liste Getir
        /// </summary>
        public static HesapCollection GetList(CommandType cmdType, string sqlQuery, MParameterCollection parameters)
        {
            HesapCollection rvHesap = new HesapCollection();
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
                            rvHesap.Add(new Hesap(MConvert.NullToGuidString(IDR["id"]), MConvert.NullToString(IDR["ip"]), MConvert.NullToString(IDR["adi"]), MConvert.NullToString(IDR["soyadi"]), MConvert.NullToString(IDR["mail"]), MConvert.NullToString(IDR["sifre"]), MConvert.NullToString(IDR["roller"]), MConvert.NullToString(IDR["onaykodu"]), MConvert.NullToDateTime(IDR["dogumtarihi"]), MConvert.NullToDateTime(IDR["kayittarihi"]), MConvert.NullToByte(IDR["cinsiyet"]), MConvert.NullToByte(IDR["tipi"]), MConvert.NullToBool(IDR["yorum"]), MConvert.NullToBool(IDR["abonelik"]), MConvert.NullToBool(IDR["aktivasyon"]), MConvert.NullToBool(IDR["aktif"])));
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
            return rvHesap;
        }

        ///<summary>
        /// Hesap Data Select
        ///</summary>
        public static HesapCollection GetSelect()
        {
            HesapCollection rvHesap = new HesapCollection();
            using (MConnection conneciton = new MConnection(MClientProvider.MySQL))
            {
                switch (conneciton.State)
                {
                    case System.Data.ConnectionState.Closed:
                        conneciton.Open();
                        break;
                }
                using (MCommand cmd = new MCommand(CommandType.Text, "select id,concat(adi,' ',soyadi) adi from hesap where aktivasyon=1 and aktif=1 and tipi in(1,2,3,5)", conneciton))
                {
                    using (IDataReader IDR = cmd.ExecuteReader())
                    {
                        while (IDR.Read())
                            rvHesap.Add(new Hesap { ID = MConvert.NullToGuidString(IDR["id"]), Adi = MConvert.NullToString(IDR["adi"]) });
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
            return rvHesap;
        }
        public static HesapCollection GetSelect(string query, bool isProfilObjects)
        {
            HesapCollection rvHesap = new HesapCollection();
            using (MConnection conneciton = new MConnection(MClientProvider.MySQL))
            {
                switch (conneciton.State)
                {
                    case System.Data.ConnectionState.Closed:
                        conneciton.Open();
                        break;
                }
                using (MCommand cmd = new MCommand(CommandType.Text, query, conneciton))
                {
                    using (IDataReader IDR = cmd.ExecuteReader())
                    {
                        while (IDR.Read())
                            rvHesap.Add(new Hesap(MConvert.NullToGuidString(IDR["id"]), MConvert.NullToString(IDR["ip"]), MConvert.NullToString(IDR["adi"]), MConvert.NullToString(IDR["soyadi"]), MConvert.NullToString(IDR["mail"]), MConvert.NullToString(IDR["sifre"]), MConvert.NullToString(IDR["roller"]), MConvert.NullToString(IDR["onaykodu"]), MConvert.NullToDateTime(IDR["dogumtarihi"]), MConvert.NullToDateTime(IDR["kayittarihi"]), MConvert.NullToByte(IDR["cinsiyet"]), MConvert.NullToByte(IDR["tipi"]), MConvert.NullToBool(IDR["yorum"]), MConvert.NullToBool(IDR["abonelik"]), MConvert.NullToBool(IDR["aktivasyon"]), MConvert.NullToBool(IDR["aktif"]), isProfilObjects));
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
            return rvHesap;
        }

        ///<summary>
        /// Hesap Data Count
        ///</summary>
        public static int Count(bool paktif)
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
                using (MCommand cmd = new MCommand(CommandType.Text, "select count(*) as totalcount from hesap where aktivasyon=1 and aktif=?aktif", conneciton))
                {
                    cmd.Parameters.Add("aktif", paktif, MSqlDbType.Boolean);
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
        /// Hesap Data Insert
        ///</summary>
        public static string Insert(Hesap p)
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
                using (MCommand cmd = new MCommand(CommandType.Text, "select count(*) as totalcount from hesap where mail=?mail", conneciton))
                {
                    cmd.Parameters.Add("mail", p.Mail, MSqlDbType.VarChar);
                    rowsAffected = MConvert.NullToInt(cmd.ExecuteScalar());
                    if (rowsAffected > 0)
                    {
                        switch (conneciton.State)
                        {
                            case System.Data.ConnectionState.Open:
                                conneciton.Close();
                                break;
                        }
                        conneciton.Dispose();
                        return "EMAIL";
                    }
                }
                using (MCommand cmd = new MCommand(CommandType.Text, "insert into hesap (id,ip,adi,soyadi,mail,sifre,roller,onaykodu,dogumtarihi,kayittarihi,cinsiyet,tipi,yorum,abonelik,aktivasyon,aktif) values(?id,?ip,?adi,?soyadi,?mail,?sifre,?roller,?onaykodu,?dogumtarihi,?kayittarihi,?cinsiyet,?tipi,?yorum,?abonelik,?aktivasyon,?aktif)", conneciton))
                {
                    p.ID = Guid.NewGuid().ToString();
                    cmd.Parameters.Add("id", p.ID, MSqlDbType.VarChar);
                    cmd.Parameters.Add("ip", p.IP, MSqlDbType.VarChar);
                    cmd.Parameters.Add("adi", p.Adi, MSqlDbType.VarChar);
                    cmd.Parameters.Add("soyadi", p.Soyadi, MSqlDbType.VarChar);
                    cmd.Parameters.Add("mail", p.Mail, MSqlDbType.VarChar);
                    cmd.Parameters.Add("sifre", p.Sifre, MSqlDbType.VarChar);
                    cmd.Parameters.Add("roller", p.Roller, MSqlDbType.VarChar);
                    cmd.Parameters.Add("onaykodu", p.OnayKodu, MSqlDbType.VarChar);
                    cmd.Parameters.Add("dogumtarihi", p.DogumTarihi, MSqlDbType.DateTime);
                    cmd.Parameters.Add("kayittarihi", p.KayitTarihi, MSqlDbType.DateTime);
                    cmd.Parameters.Add("cinsiyet", p.Cinsiyet, MSqlDbType.SmallInt);
                    cmd.Parameters.Add("tipi", p.Tipi, MSqlDbType.SmallInt);
                    cmd.Parameters.Add("yorum", p.Yorum, MSqlDbType.Boolean);
                    cmd.Parameters.Add("abonelik", p.Abonelik, MSqlDbType.Boolean);
                    cmd.Parameters.Add("aktivasyon", p.Aktivasyon, MSqlDbType.Boolean);
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
            return (rowsAffected <= 0) ? string.Empty : p.ID;
        }

        ///<summary>
        /// Hesap Data Update
        ///</summary>
        public static string Update(Hesap p)
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
                using (MCommand cmd = new MCommand(CommandType.Text, "select count(*) as totalcount from hesap where id<>?id and mail=?mail", conneciton))
                {
                    cmd.Parameters.Add("id", p.ID, MSqlDbType.VarChar);
                    cmd.Parameters.Add("mail", p.Mail, MSqlDbType.VarChar);
                    rowsAffected = MConvert.NullToInt(cmd.ExecuteScalar());
                    if (rowsAffected > 0)
                    {
                        switch (conneciton.State)
                        {
                            case System.Data.ConnectionState.Open:
                                conneciton.Close();
                                break;
                        }
                        conneciton.Dispose();
                        return "EMAIL";
                    }
                }
                using (MCommand cmd = new MCommand(CommandType.Text, "update hesap set ip=?ip,adi=?adi,soyadi=?soyadi,mail=?mail,sifre=?sifre,roller=?roller,onaykodu=?onaykodu,dogumtarihi=?dogumtarihi,kayittarihi=?kayittarihi,cinsiyet=?cinsiyet,tipi=?tipi,yorum=?yorum,abonelik=?abonelik,aktivasyon=?aktivasyon,aktif=?aktif where id=?id", conneciton))
                {
                    cmd.Parameters.Add("id", p.ID, MSqlDbType.VarChar);
                    cmd.Parameters.Add("ip", p.IP, MSqlDbType.VarChar);
                    cmd.Parameters.Add("adi", p.Adi, MSqlDbType.VarChar);
                    cmd.Parameters.Add("soyadi", p.Soyadi, MSqlDbType.VarChar);
                    cmd.Parameters.Add("mail", p.Mail, MSqlDbType.VarChar);
                    cmd.Parameters.Add("sifre", p.Sifre, MSqlDbType.VarChar);
                    cmd.Parameters.Add("roller", p.Roller, MSqlDbType.VarChar);
                    cmd.Parameters.Add("onaykodu", p.OnayKodu, MSqlDbType.VarChar);
                    cmd.Parameters.Add("dogumtarihi", p.DogumTarihi, MSqlDbType.DateTime);
                    cmd.Parameters.Add("kayittarihi", p.KayitTarihi, MSqlDbType.DateTime);
                    cmd.Parameters.Add("cinsiyet", p.Cinsiyet, MSqlDbType.SmallInt);
                    cmd.Parameters.Add("tipi", p.Tipi, MSqlDbType.SmallInt);
                    cmd.Parameters.Add("yorum", p.Yorum, MSqlDbType.Boolean);
                    cmd.Parameters.Add("abonelik", p.Abonelik, MSqlDbType.Boolean);
                    cmd.Parameters.Add("aktivasyon", p.Aktivasyon, MSqlDbType.Boolean);
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
            return (rowsAffected <= 0) ? string.Empty : p.ID;
        }

        ///<summary>
        /// Hesap Data Delete
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
                using (MCommand cmd = new MCommand(CommandType.Text, "delete from hesap where id=?id", conneciton))
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
        /// Hesap Data Delete
        ///</summary>
        public static int Delete(Hesap p)
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
                using (MCommand cmd = new MCommand(CommandType.Text, "delete from hesap where id=?id", conneciton))
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
