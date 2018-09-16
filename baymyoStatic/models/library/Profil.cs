using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Data;
using BAYMYO.MultiSQLClient;

namespace baymyoStatic
{
    public partial class ProfilCollection : CollectionBase, IDisposable
    {
        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }

        public Profil this[int index]
        {
            get { return (Profil)this.List[index]; }
            set { this.List[index] = value; }
        }

        public object SyncRoot { get { return this.List.SyncRoot; } }

        public int Add(Profil obj)
        {
            return this.List.Add(obj);
        }

        public void Insert(int index, Profil obj)
        {
            this.List.Insert(index, obj);
        }

        public bool Contains(Profil obj)
        {
            return this.List.Contains(obj);
        }

        public int IndexOf(Profil obj)
        {
            return this.List.IndexOf(obj);
        }

        public void Remove(Profil obj)
        {
            this.List.Remove(obj);
        }
    }

    public partial class Profil : IDisposable
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
        private string m_Url;
        public string Url
        {
            get { return m_Url; }
            set { m_Url = value; }
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
        private string m_Telefon;
        public string Telefon
        {
            get { return m_Telefon; }
            set { m_Telefon = value; }
        }
        private string m_GSM;
        public string GSM
        {
            get { return m_GSM; }
            set { m_GSM = value; }
        }
        private string m_Meslek;
        public string Meslek
        {
            get { return m_Meslek; }
            set { m_Meslek = value; }
        }
        private string m_Egitim;
        public string Egitim
        {
            get { return m_Egitim; }
            set { m_Egitim = value; }
        }
        private string m_Sehir;
        public string Sehir
        {
            get { return m_Sehir; }
            set { m_Sehir = value; }
        }
        private string m_Hakkimda;
        public string Hakkimda
        {
            get { return m_Hakkimda; }
            set { m_Hakkimda = value; }
        }

        #endregion

        public Profil()
        {
            this.m_ID = this.m_Url = this.m_ResimUrl = this.m_Adi = this.m_Mail = this.m_Web = this.m_Telefon = this.m_GSM = this.m_Meslek = this.m_Egitim = this.m_Sehir = this.m_Hakkimda = string.Empty;
        }

        /// <summary>
        /// Profil Nesnesi Oluþtur
        /// </summary>
        public Profil(System.String pid, string purl, string presimurl, string padi, string pmail, string pweb, string ptelefon, string pgsm, string pmeslek, string pegitim, string psehir, string phakkimda)
        {
            this.m_ID = pid;
            this.m_Url = purl;
            this.m_ResimUrl = presimurl;
            this.m_Adi = padi;
            this.m_Mail = pmail;
            this.m_Web = pweb;
            this.m_Telefon = ptelefon;
            this.m_GSM = pgsm;
            this.m_Meslek = pmeslek;
            this.m_Egitim = pegitim;
            this.m_Sehir = psehir;
            this.m_Hakkimda = phakkimda;
        }
    }

    public partial class ProfilMethods
    {
        ///<summary>
        /// Profil Data PrimaryKey
        ///</summary>
        public static Profil GetProfil(System.String pid)
        {
            Profil rvProfil = new Profil();
            using (MConnection conneciton = new MConnection(MClientProvider.MySQL))
            {
                switch (conneciton.State)
                {
                    case System.Data.ConnectionState.Closed:
                        conneciton.Open();
                        break;
                }
                using (MCommand cmd = new MCommand(CommandType.Text, "select * from profil where id=?id limit 1", conneciton))
                {
                    cmd.Parameters.Add("id", pid, MSqlDbType.VarChar);
                    using (IDataReader IDR = cmd.ExecuteReader())
                    {
                        while (IDR.Read())
                            rvProfil = new Profil(MConvert.NullToGuidString(IDR["id"]), MConvert.NullToString(IDR["url"]), MConvert.NullToString(IDR["resimurl"]), MConvert.NullToString(IDR["adi"]), MConvert.NullToString(IDR["mail"]), MConvert.NullToString(IDR["web"]), MConvert.NullToString(IDR["telefon"]), MConvert.NullToString(IDR["gsm"]), MConvert.NullToString(IDR["meslek"]), MConvert.NullToString(IDR["egitim"]), MConvert.NullToString(IDR["sehir"]), MConvert.NullToString(IDR["hakkimda"]));
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
            return rvProfil;
        }

        public static Profil GetProfilUrl(string purl)
        {
            Profil rvProfil = new Profil();
            using (MConnection conneciton = new MConnection(MClientProvider.MySQL))
            {
                switch (conneciton.State)
                {
                    case System.Data.ConnectionState.Closed:
                        conneciton.Open();
                        break;
                }
                using (MCommand cmd = new MCommand(CommandType.Text, "select * from profil where url=?url limit 1", conneciton))
                {
                    cmd.Parameters.Add("url", purl, MSqlDbType.VarChar);
                    using (IDataReader IDR = cmd.ExecuteReader())
                    {
                        while (IDR.Read())
                            rvProfil = new Profil(MConvert.NullToGuidString(IDR["id"]), MConvert.NullToString(IDR["url"]), MConvert.NullToString(IDR["resimurl"]), MConvert.NullToString(IDR["adi"]), MConvert.NullToString(IDR["mail"]), MConvert.NullToString(IDR["web"]), MConvert.NullToString(IDR["telefon"]), MConvert.NullToString(IDR["gsm"]), MConvert.NullToString(IDR["meslek"]), MConvert.NullToString(IDR["egitim"]), MConvert.NullToString(IDR["sehir"]), MConvert.NullToString(IDR["hakkimda"]));
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
            return rvProfil;
        }

        /// <summary>
        /// Profil Getir
        /// </summary>
        public static Profil GetProfil(CommandType cmdType, string sqlQuery, MParameterCollection parameters)
        {
            Profil rvProfil = new Profil();
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
                            rvProfil = new Profil(MConvert.NullToGuidString(IDR["id"]), MConvert.NullToString(IDR["url"]), MConvert.NullToString(IDR["resimurl"]), MConvert.NullToString(IDR["adi"]), MConvert.NullToString(IDR["mail"]), MConvert.NullToString(IDR["web"]), MConvert.NullToString(IDR["telefon"]), MConvert.NullToString(IDR["gsm"]), MConvert.NullToString(IDR["meslek"]), MConvert.NullToString(IDR["egitim"]), MConvert.NullToString(IDR["sehir"]), MConvert.NullToString(IDR["hakkimda"]));
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
            return rvProfil;
        }

        /// <summary>
        /// Profil Liste Getir
        /// </summary>
        public static ProfilCollection GetList(CommandType cmdType, string sqlQuery, MParameterCollection parameters)
        {
            ProfilCollection rvProfil = new ProfilCollection();
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
                            rvProfil.Add(new Profil(MConvert.NullToGuidString(IDR["id"]), MConvert.NullToString(IDR["url"]), MConvert.NullToString(IDR["resimurl"]), MConvert.NullToString(IDR["adi"]), MConvert.NullToString(IDR["mail"]), MConvert.NullToString(IDR["web"]), MConvert.NullToString(IDR["telefon"]), MConvert.NullToString(IDR["gsm"]), MConvert.NullToString(IDR["meslek"]), MConvert.NullToString(IDR["egitim"]), MConvert.NullToString(IDR["sehir"]), MConvert.NullToString(IDR["hakkimda"])));
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
            return rvProfil;
        }

        ///<summary>
        /// Profil Data Select
        ///</summary>
        public static ProfilCollection GetSelect()
        {
            ProfilCollection rvProfil = new ProfilCollection();
            using (MConnection conneciton = new MConnection(MClientProvider.MySQL))
            {
                switch (conneciton.State)
                {
                    case System.Data.ConnectionState.Closed:
                        conneciton.Open();
                        break;
                }
                using (MCommand cmd = new MCommand(CommandType.Text, "select * from profil", conneciton))
                {

                    using (IDataReader IDR = cmd.ExecuteReader())
                    {
                        while (IDR.Read())
                            rvProfil.Add(new Profil(MConvert.NullToGuidString(IDR["id"]), MConvert.NullToString(IDR["url"]), MConvert.NullToString(IDR["resimurl"]), MConvert.NullToString(IDR["adi"]), MConvert.NullToString(IDR["mail"]), MConvert.NullToString(IDR["web"]), MConvert.NullToString(IDR["telefon"]), MConvert.NullToString(IDR["gsm"]), MConvert.NullToString(IDR["meslek"]), MConvert.NullToString(IDR["egitim"]), MConvert.NullToString(IDR["sehir"]), MConvert.NullToString(IDR["hakkimda"])));
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
            return rvProfil;
        }

        ///<summary>
        /// Profil Data Insert
        ///</summary>
        public static string Insert(Profil p)
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
                using (MCommand cmd = new MCommand(CommandType.Text, "select count(*) as totalcount from profil where url=?url", conneciton))
                {
                    cmd.Parameters.Add("url", p.Url);
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
                        return "URL";
                    }
                }
                using (MCommand cmd = new MCommand(CommandType.Text, "insert into profil (id,url,resimurl,adi,mail,web,telefon,gsm,meslek,egitim,sehir,hakkimda) values(?id,?url,?resimurl,?adi,?mail,?web,?telefon,?gsm,?meslek,?egitim,?sehir,?hakkimda)", conneciton))
                {
                    cmd.Parameters.Add("id", p.ID, MSqlDbType.VarChar);
                    cmd.Parameters.Add("url", p.Url, MSqlDbType.VarChar);
                    cmd.Parameters.Add("resimurl", p.ResimUrl, MSqlDbType.VarChar);
                    cmd.Parameters.Add("adi", p.Adi, MSqlDbType.VarChar);
                    cmd.Parameters.Add("mail", p.Mail, MSqlDbType.VarChar);
                    cmd.Parameters.Add("web", p.Web, MSqlDbType.VarChar);
                    cmd.Parameters.Add("telefon", p.Telefon, MSqlDbType.VarChar);
                    cmd.Parameters.Add("gsm", p.GSM, MSqlDbType.VarChar);
                    cmd.Parameters.Add("meslek", p.Meslek, MSqlDbType.VarChar);
                    cmd.Parameters.Add("egitim", p.Egitim, MSqlDbType.VarChar);
                    cmd.Parameters.Add("sehir", p.Sehir, MSqlDbType.VarChar);
                    cmd.Parameters.Add("hakkimda", p.Hakkimda, MSqlDbType.VarChar);
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
        /// Profil Data Update
        ///</summary>
        public static string Update(Profil p)
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
                using (MCommand cmd = new MCommand(CommandType.Text, "select count(*) as totalcount from profil where id<>?id and url=?url", conneciton))
                {
                    cmd.Parameters.Add("id", p.ID);
                    cmd.Parameters.Add("url", p.Url);
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
                        return "URL";
                    }
                }
                using (MCommand cmd = new MCommand(CommandType.Text, "update profil set url=?url,resimurl=?resimurl,adi=?adi,mail=?mail,web=?web,telefon=?telefon,gsm=?gsm,meslek=?meslek,egitim=?egitim,sehir=?sehir,hakkimda=?hakkimda where id=?id", conneciton))
                {
                    cmd.Parameters.Add("id", p.ID, MSqlDbType.VarChar);
                    cmd.Parameters.Add("url", p.Url, MSqlDbType.VarChar);
                    cmd.Parameters.Add("resimurl", p.ResimUrl, MSqlDbType.VarChar);
                    cmd.Parameters.Add("adi", p.Adi, MSqlDbType.VarChar);
                    cmd.Parameters.Add("mail", p.Mail, MSqlDbType.VarChar);
                    cmd.Parameters.Add("web", p.Web, MSqlDbType.VarChar);
                    cmd.Parameters.Add("telefon", p.Telefon, MSqlDbType.VarChar);
                    cmd.Parameters.Add("gsm", p.GSM, MSqlDbType.VarChar);
                    cmd.Parameters.Add("meslek", p.Meslek, MSqlDbType.VarChar);
                    cmd.Parameters.Add("egitim", p.Egitim, MSqlDbType.VarChar);
                    cmd.Parameters.Add("sehir", p.Sehir, MSqlDbType.VarChar);
                    cmd.Parameters.Add("hakkimda", p.Hakkimda, MSqlDbType.VarChar);
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
        /// Profil Data Delete
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
                using (MCommand cmd = new MCommand(CommandType.Text, "delete from profil where id=?id", conneciton))
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
        /// Profil Data Delete
        ///</summary>
        public static int Delete(Profil p)
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
                using (MCommand cmd = new MCommand(CommandType.Text, "delete from profil where id=?id", conneciton))
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
