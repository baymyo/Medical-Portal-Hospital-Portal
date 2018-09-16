using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Data;
using BAYMYO.MultiSQLClient;

namespace baymyoStatic
{
    public partial class FirmaCollection : CollectionBase, IDisposable
    {
        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }

        public Firma this[int index]
        {
            get { return (Firma)this.List[index]; }
            set { this.List[index] = value; }
        }

        public object SyncRoot { get { return this.List.SyncRoot; } }

        public int Add(Firma obj)
        {
            return this.List.Add(obj);
        }

        public void Insert(int index, Firma obj)
        {
            this.List.Insert(index, obj);
        }

        public bool Contains(Firma obj)
        {
            return this.List.Contains(obj);
        }

        public int IndexOf(Firma obj)
        {
            return this.List.IndexOf(obj);
        }

        public void Remove(Firma obj)
        {
            this.List.Remove(obj);
        }
    }

    public partial class Firma : IDisposable
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
        private string m_KategoriID;
        public string KategoriID
        {
            get { return m_KategoriID; }
            set { m_KategoriID = value; }
        }
        private string m_ResimUrl;
        public string ResimUrl
        {
            get { return m_ResimUrl; }
            set { m_ResimUrl = value; }
        }
        private string m_Adi;
        public string Adi
        {
            get { return m_Adi; }
            set { m_Adi = value; }
        }
        private string m_Yetkili;
        public string Yetkili
        {
            get { return m_Yetkili; }
            set { m_Yetkili = value; }
        }
        private string m_Adres;
        public string Adres
        {
            get { return m_Adres; }
            set { m_Adres = value; }
        }
        private string m_Mail;
        public string Mail
        {
            get { return m_Mail; }
            set { m_Mail = value; }
        }
        private string m_Web;
        public string Web
        {
            get { return m_Web; }
            set { m_Web = value; }
        }
        private string m_Telefon1;
        public string Telefon1
        {
            get { return m_Telefon1; }
            set { m_Telefon1 = value; }
        }
        private string m_Telefon2;
        public string Telefon2
        {
            get { return m_Telefon2; }
            set { m_Telefon2 = value; }
        }
        private string m_GSM;
        public string GSM
        {
            get { return m_GSM; }
            set { m_GSM = value; }
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
        private DateTime m_GuncellemeTarihi;
        public DateTime GuncellemeTarihi
        {
            get { return m_GuncellemeTarihi; }
            set { m_GuncellemeTarihi = value; }
        }
        private bool m_GosterimSayi;
        public bool GosterimSayi
        {
            get { return m_GosterimSayi; }
            set { m_GosterimSayi = value; }
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

        public Firma()
        {
        }

        /// <summary>
        /// Firma Nesnesi Oluþtur
        /// </summary>
        public Firma(Int64 pid, System.String phesapid, string pkategoriid, string presimurl, string padi, string pyetkili, string padres, string pmail, string pweb, string ptelefon1, string ptelefon2, string pgsm, string psehir, DateTime pkayittarihi, DateTime pguncellemetarihi, bool pgosterimsayi, bool pyoneticionay, bool paktif)
        {
            this.m_ID = pid;
            this.m_HesapID = phesapid;
            this.m_KategoriID = pkategoriid;
            this.m_ResimUrl = presimurl;
            this.m_Adi = padi;
            this.m_Yetkili = pyetkili;
            this.m_Adres = padres;
            this.m_Mail = pmail;
            this.m_Web = pweb;
            this.m_Telefon1 = ptelefon1;
            this.m_Telefon2 = ptelefon2;
            this.m_GSM = pgsm;
            this.m_Sehir = psehir;
            this.m_KayitTarihi = pkayittarihi;
            this.m_GuncellemeTarihi = pguncellemetarihi;
            this.m_GosterimSayi = pgosterimsayi;
            this.m_YoneticiOnay = pyoneticionay;
            this.m_Aktif = paktif;
        }
    }

    public partial class FirmaMethods
    {
        ///<summary>
        /// Firma Data PrimaryKey
        ///</summary>
        public static Firma GetFirma(Int64 pid)
        {
            Firma rvFirma = new Firma();
            using (MConnection conneciton = new MConnection(MClientProvider.MySQL))
            {
                switch (conneciton.State)
                {
                    case System.Data.ConnectionState.Closed:
                        conneciton.Open();
                        break;
                }
                using (MCommand cmd = new MCommand(CommandType.Text, "select * from firma where id=?id limit 1", conneciton))
                {
                    cmd.Parameters.Add("id", pid, MSqlDbType.BigInt);
                    using (IDataReader IDR = cmd.ExecuteReader())
                    {
                        while (IDR.Read())
                            rvFirma = new Firma(MConvert.NullToInt64(IDR["id"]), MConvert.NullToGuidString(IDR["hesapid"]), MConvert.NullToString(IDR["kategoriid"]), MConvert.NullToString(IDR["resimurl"]), MConvert.NullToString(IDR["adi"]), MConvert.NullToString(IDR["yetkili"]), MConvert.NullToString(IDR["adres"]), MConvert.NullToString(IDR["mail"]), MConvert.NullToString(IDR["web"]), MConvert.NullToString(IDR["telefon1"]), MConvert.NullToString(IDR["telefon2"]), MConvert.NullToString(IDR["gsm"]), MConvert.NullToString(IDR["sehir"]), MConvert.NullToDateTime(IDR["kayittarihi"]), MConvert.NullToDateTime(IDR["guncellemetarihi"]), MConvert.NullToBool(IDR["gosterimsayi"]), MConvert.NullToBool(IDR["yoneticionay"]), MConvert.NullToBool(IDR["aktif"]));
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
            return rvFirma;
        }

        /// <summary>
        /// Firma Getir
        /// </summary>
        public static Firma GetFirma(CommandType cmdType, string sqlQuery, MParameterCollection parameters)
        {
            Firma rvFirma = new Firma();
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
                            rvFirma = new Firma(MConvert.NullToInt64(IDR["id"]), MConvert.NullToGuidString(IDR["hesapid"]), MConvert.NullToString(IDR["kategoriid"]), MConvert.NullToString(IDR["resimurl"]), MConvert.NullToString(IDR["adi"]), MConvert.NullToString(IDR["yetkili"]), MConvert.NullToString(IDR["adres"]), MConvert.NullToString(IDR["mail"]), MConvert.NullToString(IDR["web"]), MConvert.NullToString(IDR["telefon1"]), MConvert.NullToString(IDR["telefon2"]), MConvert.NullToString(IDR["gsm"]), MConvert.NullToString(IDR["sehir"]), MConvert.NullToDateTime(IDR["kayittarihi"]), MConvert.NullToDateTime(IDR["guncellemetarihi"]), MConvert.NullToBool(IDR["gosterimsayi"]), MConvert.NullToBool(IDR["yoneticionay"]), MConvert.NullToBool(IDR["aktif"]));
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
            return rvFirma;
        }

        /// <summary>
        /// Firma Liste Getir
        /// </summary>
        public static FirmaCollection GetList(CommandType cmdType, string sqlQuery, MParameterCollection parameters)
        {
            FirmaCollection rvFirma = new FirmaCollection();
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
                            rvFirma.Add(new Firma(MConvert.NullToInt64(IDR["id"]), MConvert.NullToGuidString(IDR["hesapid"]), MConvert.NullToString(IDR["kategoriid"]), MConvert.NullToString(IDR["resimurl"]), MConvert.NullToString(IDR["adi"]), MConvert.NullToString(IDR["yetkili"]), MConvert.NullToString(IDR["adres"]), MConvert.NullToString(IDR["mail"]), MConvert.NullToString(IDR["web"]), MConvert.NullToString(IDR["telefon1"]), MConvert.NullToString(IDR["telefon2"]), MConvert.NullToString(IDR["gsm"]), MConvert.NullToString(IDR["sehir"]), MConvert.NullToDateTime(IDR["kayittarihi"]), MConvert.NullToDateTime(IDR["guncellemetarihi"]), MConvert.NullToBool(IDR["gosterimsayi"]), MConvert.NullToBool(IDR["yoneticionay"]), MConvert.NullToBool(IDR["aktif"])));
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
            return rvFirma;
        }

        ///<summary>
        /// Firma Data Select
        ///</summary>
        public static FirmaCollection GetSelect(System.String phesapid)
        {
            FirmaCollection rvFirma = new FirmaCollection();
            using (MConnection conneciton = new MConnection(MClientProvider.MySQL))
            {
                switch (conneciton.State)
                {
                    case System.Data.ConnectionState.Closed:
                        conneciton.Open();
                        break;
                }
                using (MCommand cmd = new MCommand(CommandType.Text, "select * from firma where hesapid=?hesapid", conneciton))
                {
                    cmd.Parameters.Add("hesapid", phesapid, MSqlDbType.VarChar);
                    using (IDataReader IDR = cmd.ExecuteReader())
                    {
                        while (IDR.Read())
                            rvFirma.Add(new Firma(MConvert.NullToInt64(IDR["id"]), MConvert.NullToGuidString(IDR["hesapid"]), MConvert.NullToString(IDR["kategoriid"]), MConvert.NullToString(IDR["resimurl"]), MConvert.NullToString(IDR["adi"]), MConvert.NullToString(IDR["yetkili"]), MConvert.NullToString(IDR["adres"]), MConvert.NullToString(IDR["mail"]), MConvert.NullToString(IDR["web"]), MConvert.NullToString(IDR["telefon1"]), MConvert.NullToString(IDR["telefon2"]), MConvert.NullToString(IDR["gsm"]), MConvert.NullToString(IDR["sehir"]), MConvert.NullToDateTime(IDR["kayittarihi"]), MConvert.NullToDateTime(IDR["guncellemetarihi"]), MConvert.NullToBool(IDR["gosterimsayi"]), MConvert.NullToBool(IDR["yoneticionay"]), MConvert.NullToBool(IDR["aktif"])));
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
            return rvFirma;
        }

        ///<summary>
        /// Firma Data Count
        ///</summary>
        public static int Count(bool pyoneticionay)
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
                using (MCommand cmd = new MCommand(CommandType.Text, "select count(*) as totalcount from firma where yoneticionay=?yoneticionay", conneciton))
                {
                    cmd.Parameters.Add("yoneticionay", pyoneticionay, MSqlDbType.Boolean);
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
        /// Firma Data Insert
        ///</summary>
        public static Int64 Insert(Firma p)
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
                using (MCommand cmd = new MCommand(CommandType.Text, "insert into firma (hesapid,kategoriid,resimurl,adi,yetkili,adres,mail,web,telefon1,telefon2,gsm,sehir,kayittarihi,guncellemetarihi,gosterimsayi,yoneticionay,aktif) values(?hesapid,?kategoriid,?resimurl,?adi,?yetkili,?adres,?mail,?web,?telefon1,?telefon2,?gsm,?sehir,?kayittarihi,?guncellemetarihi,?gosterimsayi,?yoneticionay,?aktif); select last_insert_id();", conneciton))
                {
                    cmd.Parameters.Add("hesapid", p.HesapID, MSqlDbType.VarChar);
                    cmd.Parameters.Add("kategoriid", p.KategoriID, MSqlDbType.VarChar);
                    cmd.Parameters.Add("resimurl", p.ResimUrl, MSqlDbType.VarChar);
                    cmd.Parameters.Add("adi", p.Adi, MSqlDbType.VarChar);
                    cmd.Parameters.Add("yetkili", p.Yetkili, MSqlDbType.VarChar);
                    cmd.Parameters.Add("adres", p.Adres, MSqlDbType.VarChar);
                    cmd.Parameters.Add("mail", p.Mail, MSqlDbType.VarChar);
                    cmd.Parameters.Add("web", p.Web, MSqlDbType.VarChar);
                    cmd.Parameters.Add("telefon1", p.Telefon1, MSqlDbType.VarChar);
                    cmd.Parameters.Add("telefon2", p.Telefon2, MSqlDbType.VarChar);
                    cmd.Parameters.Add("gsm", p.GSM, MSqlDbType.VarChar);
                    cmd.Parameters.Add("sehir", p.Sehir, MSqlDbType.VarChar);
                    cmd.Parameters.Add("kayittarihi", p.KayitTarihi, MSqlDbType.DateTime);
                    cmd.Parameters.Add("guncellemetarihi", p.GuncellemeTarihi, MSqlDbType.DateTime);
                    cmd.Parameters.Add("gosterimsayi", p.GosterimSayi, MSqlDbType.Boolean);
                    cmd.Parameters.Add("yoneticionay", p.YoneticiOnay, MSqlDbType.Boolean);
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
        /// Firma Data Update
        ///</summary>
        public static int Update(Firma p)
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
                using (MCommand cmd = new MCommand(CommandType.Text, "update firma set hesapid=?hesapid,kategoriid=?kategoriid,resimurl=?resimurl,adi=?adi,yetkili=?yetkili,adres=?adres,mail=?mail,web=?web,telefon1=?telefon1,telefon2=?telefon2,gsm=?gsm,sehir=?sehir,kayittarihi=?kayittarihi,guncellemetarihi=?guncellemetarihi,gosterimsayi=?gosterimsayi,yoneticionay=?yoneticionay,aktif=?aktif where id=?id", conneciton))
                {
                    cmd.Parameters.Add("id", p.ID, MSqlDbType.BigInt);
                    cmd.Parameters.Add("hesapid", p.HesapID, MSqlDbType.VarChar);
                    cmd.Parameters.Add("kategoriid", p.KategoriID, MSqlDbType.VarChar);
                    cmd.Parameters.Add("resimurl", p.ResimUrl, MSqlDbType.VarChar);
                    cmd.Parameters.Add("adi", p.Adi, MSqlDbType.VarChar);
                    cmd.Parameters.Add("yetkili", p.Yetkili, MSqlDbType.VarChar);
                    cmd.Parameters.Add("adres", p.Adres, MSqlDbType.VarChar);
                    cmd.Parameters.Add("mail", p.Mail, MSqlDbType.VarChar);
                    cmd.Parameters.Add("web", p.Web, MSqlDbType.VarChar);
                    cmd.Parameters.Add("telefon1", p.Telefon1, MSqlDbType.VarChar);
                    cmd.Parameters.Add("telefon2", p.Telefon2, MSqlDbType.VarChar);
                    cmd.Parameters.Add("gsm", p.GSM, MSqlDbType.VarChar);
                    cmd.Parameters.Add("sehir", p.Sehir, MSqlDbType.VarChar);
                    cmd.Parameters.Add("kayittarihi", p.KayitTarihi, MSqlDbType.DateTime);
                    cmd.Parameters.Add("guncellemetarihi", p.GuncellemeTarihi, MSqlDbType.DateTime);
                    cmd.Parameters.Add("gosterimsayi", p.GosterimSayi, MSqlDbType.Boolean);
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
        /// Firma Data Delete
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
                using (MCommand cmd = new MCommand(CommandType.Text, "delete from firma where id=?id", conneciton))
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
        /// Firma Data Delete
        ///</summary>
        public static int Delete(Firma p)
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
                using (MCommand cmd = new MCommand(CommandType.Text, "delete from firma where id=?id", conneciton))
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
