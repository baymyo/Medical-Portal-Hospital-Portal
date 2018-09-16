using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Data;
using BAYMYO.MultiSQLClient;

namespace baymyoStatic
{
    public partial class MansetCollection : CollectionBase, IDisposable
    {
        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }

        public Manset this[int index]
        {
            get { return (Manset)this.List[index]; }
            set { this.List[index] = value; }
        }

        public object SyncRoot { get { return this.List.SyncRoot; } }

        public int Add(Manset obj)
        {
            return this.List.Add(obj);
        }

        public void Insert(int index, Manset obj)
        {
            this.List.Insert(index, obj);
        }

        public bool Contains(Manset obj)
        {
            return this.List.Contains(obj);
        }

        public int IndexOf(Manset obj)
        {
            return this.List.IndexOf(obj);
        }

        public void Remove(Manset obj)
        {
            this.List.Remove(obj);
        }
    }

    public partial class Manset : IDisposable
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
        private string m_ModulID;
        public string ModulID
        {
            get { return m_ModulID; }
            set { m_ModulID = value; }
        }
        private string m_ResimBuyuk;
        public string ResimBuyuk
        {
            get { return m_ResimBuyuk; }
            set { m_ResimBuyuk = value; }
        }
        private string m_Baslik1;
        public string Baslik1
        {
            get { return m_Baslik1; }
            set { m_Baslik1 = value; }
        }
        private string m_Baslik2;
        public string Baslik2
        {
            get { return m_Baslik2; }
            set { m_Baslik2 = value; }
        }
        private string m_Aciklama;
        public string Aciklama
        {
            get { return m_Aciklama; }
            set { m_Aciklama = value; }
        }
        private string m_Baglanti;
        public string Baglanti
        {
            get { return m_Baglanti; }
            set { m_Baglanti = value; }
        }
        private string m_Dugme;
        public string Dugme
        {
            get { return m_Dugme; }
            set { m_Dugme = value; }
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
        private DateTime m_GuncellemeTarihi;
        public DateTime GuncellemeTarihi
        {
            get { return m_GuncellemeTarihi; }
            set { m_GuncellemeTarihi = value; }
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

        public Manset()
        {
            this.m_ModulID = this.m_ResimBuyuk = this.m_Baslik1 = this.m_Baslik2 = this.m_Aciklama = this.m_Baglanti = this.m_Dugme = this.m_Dil = "";
            this.m_Yerlesim = 0;
            this.m_Aktif = false;
        }

        /// <summary>
        /// Manset Nesnesi Oluþtur
        /// </summary>
        public Manset(System.String pid, string pmodulid, string presimbuyuk, string pbaslik1, string pbaslik2, string paciklama, string pbaglanti, string pdugme, string pdil, DateTime pkayittarihi, DateTime pguncellemetarihi, byte pyerlesim, bool paktif)
        {
            this.m_ID = pid;
            this.m_ModulID = pmodulid;
            this.m_ResimBuyuk = presimbuyuk;
            this.m_Baslik1 = pbaslik1;
            this.m_Baslik2 = pbaslik2;
            this.m_Aciklama = paciklama;
            this.m_Baglanti = pbaglanti;
            this.m_Dugme = pdugme;
            this.m_Dil = pdil;
            this.m_KayitTarihi = pkayittarihi;
            this.m_GuncellemeTarihi = pguncellemetarihi;
            this.m_Yerlesim = pyerlesim;
            this.m_Aktif = paktif;
        }
    }

    public partial class MansetMethods
    {
        ///<summary>
        /// Manset Data PrimaryKey
        ///</summary>
        public static Manset GetManset(System.String pid)
        {
            Manset rvManset = new Manset();
            using (MConnection conneciton = new MConnection(MClientProvider.MySQL))
            {
                switch (conneciton.State)
                {
                    case System.Data.ConnectionState.Closed:
                        conneciton.Open();
                        break;
                }
                using (MCommand cmd = new MCommand(CommandType.Text, "select * from manset where id=?id order by guncellemetarihi desc limit 1", conneciton))
                {
                    cmd.Parameters.Add("id", pid, MSqlDbType.VarChar);
                    using (IDataReader IDR = cmd.ExecuteReader())
                    {
                        while (IDR.Read())
                            rvManset = new Manset(MConvert.NullToGuidString(IDR["id"]), MConvert.NullToString(IDR["modulid"]), MConvert.NullToString(IDR["resimbuyuk"]), MConvert.NullToString(IDR["baslik1"]), MConvert.NullToString(IDR["baslik2"]), MConvert.NullToString(IDR["aciklama"]), MConvert.NullToString(IDR["baglanti"]), MConvert.NullToString(IDR["dugme"]), MConvert.NullToString(IDR["dil"]), MConvert.NullToDateTime(IDR["kayittarihi"]), MConvert.NullToDateTime(IDR["guncellemetarihi"]), MConvert.NullToByte(IDR["yerlesim"]), MConvert.NullToBool(IDR["aktif"]));
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
            return rvManset;
        }
        ///<summary>
        /// Manset Data Select
        ///</summary>
        public static MansetCollection GetSelect(byte yerlesim, byte limit)
        {
            MansetCollection rvManset = new MansetCollection();
            using (MConnection conneciton = new MConnection(MClientProvider.MySQL))
            {
                switch (conneciton.State)
                {
                    case System.Data.ConnectionState.Closed:
                        conneciton.Open();
                        break;
                }
                using (MCommand cmd = new MCommand(CommandType.Text, "select * from manset where yerlesim=" + yerlesim + " and aktif=1 and resimbuyuk<>'' limit " + limit, conneciton))
                {

                    using (IDataReader IDR = cmd.ExecuteReader())
                    {
                        while (IDR.Read())
                            rvManset.Add(new Manset(MConvert.NullToGuidString(IDR["id"]), MConvert.NullToString(IDR["modulid"]), MConvert.NullToString(IDR["resimbuyuk"]), MConvert.NullToString(IDR["baslik1"]), MConvert.NullToString(IDR["baslik2"]), MConvert.NullToString(IDR["aciklama"]), MConvert.NullToString(IDR["baglanti"]), MConvert.NullToString(IDR["dugme"]), MConvert.NullToString(IDR["dil"]), MConvert.NullToDateTime(IDR["kayittarihi"]), MConvert.NullToDateTime(IDR["guncellemetarihi"]), MConvert.NullToByte(IDR["yerlesim"]), MConvert.NullToBool(IDR["aktif"])));
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
            return rvManset;
        }

        ///<summary>
        /// Manset Data Insert
        ///</summary>
        public static int Insert(Manset p)
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
                using (MCommand cmd = new MCommand(CommandType.Text, "insert into manset (id,modulid,resimbuyuk,baslik1,baslik2,aciklama,baglanti,dugme,dil,kayittarihi,guncellemetarihi,yerlesim,aktif) values(?id,?modulid,?resimbuyuk,?baslik1,?baslik2,?aciklama,?baglanti,?dugme,?dil,?kayittarihi,?guncellemetarihi,?yerlesim,?aktif)", conneciton))
                {
                    cmd.Parameters.Add("id", Guid.NewGuid(), MSqlDbType.VarChar);
                    cmd.Parameters.Add("modulid", p.ModulID, MSqlDbType.VarChar);
                    cmd.Parameters.Add("resimbuyuk", p.ResimBuyuk, MSqlDbType.VarChar);
                    cmd.Parameters.Add("baslik1", p.Baslik1, MSqlDbType.VarChar);
                    cmd.Parameters.Add("baslik2", p.Baslik2, MSqlDbType.VarChar);
                    cmd.Parameters.Add("aciklama", p.Aciklama, MSqlDbType.VarChar);
                    cmd.Parameters.Add("baglanti", p.Baglanti, MSqlDbType.VarChar);
                    cmd.Parameters.Add("dugme", p.Dugme, MSqlDbType.VarChar);
                    cmd.Parameters.Add("dil", p.Dil, MSqlDbType.VarChar);
                    cmd.Parameters.Add("kayittarihi", p.KayitTarihi, MSqlDbType.DateTime);
                    cmd.Parameters.Add("guncellemetarihi", p.GuncellemeTarihi, MSqlDbType.DateTime);
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
        /// Manset Data Update
        ///</summary>
        public static int Update(Manset p)
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
                using (MCommand cmd = new MCommand(CommandType.Text, "update manset set modulid=?modulid,resimbuyuk=?resimbuyuk,baslik1=?baslik1,baslik2=?baslik2,aciklama=?aciklama,baglanti=?baglanti,dugme=?dugme,dil=?dil,kayittarihi=?kayittarihi,guncellemetarihi=?guncellemetarihi,yerlesim=?yerlesim,aktif=?aktif where id=?id", conneciton))
                {
                    cmd.Parameters.Add("id", p.ID, MSqlDbType.VarChar);
                    cmd.Parameters.Add("modulid", p.ModulID, MSqlDbType.VarChar);
                    cmd.Parameters.Add("resimbuyuk", p.ResimBuyuk, MSqlDbType.VarChar);
                    cmd.Parameters.Add("baslik1", p.Baslik1, MSqlDbType.VarChar);
                    cmd.Parameters.Add("baslik2", p.Baslik2, MSqlDbType.VarChar);
                    cmd.Parameters.Add("aciklama", p.Aciklama, MSqlDbType.VarChar);
                    cmd.Parameters.Add("baglanti", p.Baglanti, MSqlDbType.VarChar);
                    cmd.Parameters.Add("dugme", p.Dugme, MSqlDbType.VarChar);
                    cmd.Parameters.Add("dil", p.Dil, MSqlDbType.VarChar);
                    cmd.Parameters.Add("kayittarihi", p.KayitTarihi, MSqlDbType.DateTime);
                    cmd.Parameters.Add("guncellemetarihi", p.GuncellemeTarihi, MSqlDbType.DateTime);
                    cmd.Parameters.Add("yerlesim", p.Yerlesim, MSqlDbType.Byte);
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
        /// Manset Data Delete
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
                using (MCommand cmd = new MCommand(CommandType.Text, "delete from manset where id=?id", conneciton))
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
        /// Manset Data Delete
        ///</summary>
        public static int Delete(Manset p)
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
                using (MCommand cmd = new MCommand(CommandType.Text, "delete from manset where id=?id", conneciton))
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
