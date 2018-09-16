using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Data;
using BAYMYO.MultiSQLClient;

namespace baymyoStatic
{
    public partial class KategoriCollection : CollectionBase, IDisposable
    {
        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }

        public Kategori this[int index]
        {
            get { return (Kategori)this.List[index]; }
            set { this.List[index] = value; }
        }

        public object SyncRoot { get { return this.List.SyncRoot; } }

        public int Add(Kategori obj)
        {
            return this.List.Add(obj);
        }

        public void Insert(int index, Kategori obj)
        {
            this.List.Insert(index, obj);
        }

        public bool Contains(Kategori obj)
        {
            return this.List.Contains(obj);
        }

        public int IndexOf(Kategori obj)
        {
            return this.List.IndexOf(obj);
        }

        public void Remove(Kategori obj)
        {
            this.List.Remove(obj);
        }
    }

    [System.ComponentModel.DataObject(true)]
    public partial class Kategori : IDisposable
    {
        #region ---IDisposable Members---
        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }
        #endregion

        #region ---Properties/Field - Özellikler Alanlar---
        private string m_ID;
        public string ID
        {
            get { return m_ID; }
            set { m_ID = value; }
        }
        private string m_ParentID;
        public string ParentID
        {
            get { return m_ParentID; }
            set { m_ParentID = value; }
        }
        private string m_ModulID;
        public string ModulID
        {
            get { return m_ModulID; }
            set { m_ModulID = value; }
        }
        private string m_Adi;
        public string Adi
        {
            get { return m_Adi; }
            set { m_Adi = value; }
        }
        private string m_Dil;
        public string Dil
        {
            get { return m_Dil; }
            set { m_Dil = value; }
        }
        private byte m_Sira;
        public byte Sira
        {
            get { return m_Sira; }
            set { m_Sira = value; }
        }
        private byte m_Menu;
        public byte Menu
        {
            get { return m_Menu; }
            set { m_Menu = value; }
        }
        private bool m_Aktif;
        public bool Aktif
        {
            get { return m_Aktif; }
            set { m_Aktif = value; }
        }
        private string m_Renk;
        public string Renk
        {
            get { return m_Renk; }
            set { m_Renk = value; }
        }
        private string m_Aciklama;
        public string Aciklama
        {
            get { return m_Aciklama; }
            set { m_Aciklama = value; }
        }
        private string m_Etiket;
        public string Etiket
        {
            get { return m_Etiket; }
            set { m_Etiket = value; }
        }
        #endregion

        public Kategori()
        {
            //this.m_ID = this.m_ModulID = this.m_ParentID = this.m_Adi = string.Empty;
        }

        /// <summary>
        /// Kategori Nesnesi Oluþtur
        /// </summary>
        public Kategori(string pid, string pparentid, string pmodulid, string padi, string pdil, byte psira, byte pmenu, bool paktif, string prenk, string paciklama, string petiket)
        {
            this.m_ID = pid;
            this.m_ParentID = pparentid;
            this.m_ModulID = pmodulid;
            this.m_Adi = padi;
            this.m_Dil = pdil;
            this.m_Sira = psira;
            this.m_Menu = pmenu;
            this.m_Aktif = paktif;
            this.m_Renk = prenk;
            this.m_Aciklama = paciklama;
            this.m_Etiket = petiket;
        }
    }

    public partial class KategoriMethods
    {

        public static string FilePath { get { return System.Web.HttpContext.Current.Server.MapPath(Settings.ViewPath) + "Categories"; } }
        public static Dictionary<string, object> GetCustomList()
        {
            Dictionary<string, object> rvKategori = new Dictionary<string, object>();
            using (MConnection conneciton = new MConnection(MClientProvider.MySQL))
            {
                switch (conneciton.State)
                {
                    case System.Data.ConnectionState.Closed:
                        conneciton.Open();
                        break;
                }
                using (MCommand cmd = new MCommand(CommandType.Text, "select id,modulid,adi from kategori", conneciton))
                {
                    using (IDataReader IDR = cmd.ExecuteReader())
                    {
                        while (IDR.Read())
                            rvKategori.Add(
                                MConvert.NullToString(IDR["modulid"]) + "-" + BAYMYO.UI.Commons.ClearInvalidCharacter(Core.ReplaceToLover(BAYMYO.UI.Converts.NullToString(IDR["adi"]))).Replace("---", "-").Replace("--", "-"),
                                MConvert.NullToString(IDR["id"]));
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
            return rvKategori;
        }
        public static Dictionary<string, object> Read()
        {
            Dictionary<string, object> rv = null;
            try
            {
                //if (HttpContext.Current.Cache["siteSettings"] == null)
                using (System.IO.StreamReader sr = new System.IO.StreamReader(FilePath))
                {
                    System.Web.Script.Serialization.JavaScriptSerializer javaScriptSerializer = new System.Web.Script.Serialization.JavaScriptSerializer();
                    rv = (Dictionary<string, object>)javaScriptSerializer.Deserialize(sr.ReadToEnd(), typeof(object));
                    //HttpContext.Current.Cache["siteSettings"] = rv;
                    sr.Close();
                }
                //else
                //    rv = HttpContext.Current.Cache["siteSettings"] as Portal;
            }
            catch (Exception)
            {
                return new Dictionary<string, object>();
            }
            return rv;
        }
        public static bool Save()
        {
            try
            {
                Dictionary<string, object> data = GetCustomList();
                if (data != null)
                {
                    System.Web.Script.Serialization.JavaScriptSerializer javaScriptSerializer = new System.Web.Script.Serialization.JavaScriptSerializer();
                    string jsondata = javaScriptSerializer.Serialize(data);
                    if (!string.IsNullOrEmpty(jsondata))
                    {
                        using (System.IO.StreamWriter sw = new System.IO.StreamWriter(FilePath))
                        {
                            sw.Write(jsondata);
                            sw.Close();
                        }
                        System.Web.HttpContext.Current.Application["kategoriler"] = KategoriMethods.Read();
                        return true;
                    }
                }
                return false;
            }
            catch (Exception)
            {
                return false;
            }
        }

        ///<summary>
        /// Kategori Data PrimaryKey
        ///</summary>
        [System.ComponentModel.DataObjectMethod(System.ComponentModel.DataObjectMethodType.Select)]
        public static Kategori GetKategori(string modulid, string id)
        {
            Kategori rvKategori = new Kategori();
            using (MConnection conneciton = new MConnection(MClientProvider.MySQL))
            {
                switch (conneciton.State)
                {
                    case System.Data.ConnectionState.Closed:
                        conneciton.Open();
                        break;
                }
                using (MCommand cmd = new MCommand(CommandType.Text, "select * from kategori where modulid=?modulid and id=?id limit 1", conneciton))
                {
                    cmd.Parameters.Add("modulid", modulid, MSqlDbType.VarChar);
                    cmd.Parameters.Add("id", id, MSqlDbType.VarChar);
                    using (IDataReader IDR = cmd.ExecuteReader())
                    {
                        while (IDR.Read())
                            rvKategori = new Kategori(MConvert.NullToString(IDR["id"]), MConvert.NullToString(IDR["parentid"]), MConvert.NullToString(IDR["modulid"]), MConvert.NullToString(IDR["adi"]), MConvert.NullToString(IDR["dil"]), MConvert.NullToByte(IDR["sira"]), MConvert.NullToByte(IDR["menu"]), MConvert.NullToBool(IDR["aktif"]), MConvert.NullToString(IDR["renk"]), MConvert.NullToString(IDR["aciklama"]), MConvert.NullToString(IDR["etiket"]));
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
            return rvKategori;
        }

        [System.ComponentModel.DataObjectMethod(System.ComponentModel.DataObjectMethodType.Select)]
        public static List<Kategori> GetMenu(string modulid)
        {
            List<Kategori> rvKategori = new List<Kategori>();
            using (MConnection conneciton = new MConnection(MClientProvider.MySQL))
            {
                switch (conneciton.State)
                {
                    case System.Data.ConnectionState.Closed:
                        conneciton.Open();
                        break;
                }
                using (MCommand cmd = new MCommand(CommandType.Text, "select * from kategori where modulid=?modulid and aktif=1 order by sira asc, adi asc", conneciton))
                {
                    cmd.Parameters.Add("modulid", modulid, MSqlDbType.VarChar);
                    using (IDataReader IDR = cmd.ExecuteReader())
                    {
                        while (IDR.Read())
                            rvKategori.Add(new Kategori(MConvert.NullToString(IDR["id"]), MConvert.NullToString(IDR["parentid"]), MConvert.NullToString(IDR["modulid"]), MConvert.NullToString(IDR["adi"]), MConvert.NullToString(IDR["dil"]), MConvert.NullToByte(IDR["sira"]), MConvert.NullToByte(IDR["menu"]), MConvert.NullToBool(IDR["aktif"]), MConvert.NullToString(IDR["renk"]), MConvert.NullToString(IDR["aciklama"]), MConvert.NullToString(IDR["etiket"])));
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
            return rvKategori;
        }

        [System.ComponentModel.DataObjectMethod(System.ComponentModel.DataObjectMethodType.Select)]
        public static List<Kategori> GetMenu(string modulid, bool rootNode)
        {
            List<Kategori> rvKategori = new List<Kategori>();
            using (MConnection conneciton = new MConnection(MClientProvider.MySQL))
            {
                switch (conneciton.State)
                {
                    case System.Data.ConnectionState.Closed:
                        conneciton.Open();
                        break;
                }
                switch (rootNode)
                {
                    case true:
                        rvKategori.Add(new Kategori("0", "", modulid, "<Seçiniz>", "", 0, 1, false, "", "", ""));
                        break;
                }
                using (MCommand cmd = new MCommand(CommandType.Text, "select * from kategori where modulid=?modulid and aktif=1 order by adi asc", conneciton))
                {
                    cmd.Parameters.Add("modulid", modulid, MSqlDbType.VarChar);
                    using (IDataReader IDR = cmd.ExecuteReader())
                    {
                        while (IDR.Read())
                            rvKategori.Add(new Kategori(MConvert.NullToString(IDR["id"]), MConvert.NullToString(IDR["parentid"]), MConvert.NullToString(IDR["modulid"]), MConvert.NullToString(IDR["adi"]), MConvert.NullToString(IDR["dil"]), MConvert.NullToByte(IDR["sira"]), MConvert.NullToByte(IDR["menu"]), MConvert.NullToBool(IDR["aktif"]), MConvert.NullToString(IDR["renk"]), MConvert.NullToString(IDR["aciklama"]), MConvert.NullToString(IDR["etiket"])));
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
            return rvKategori;
        }

        [System.ComponentModel.DataObjectMethod(System.ComponentModel.DataObjectMethodType.Select)]
        public static List<Kategori> GetMenu(string modulid, byte menuType)
        {
            List<Kategori> rvKategori = new List<Kategori>();
            using (MConnection conneciton = new MConnection(MClientProvider.MySQL))
            {
                switch (conneciton.State)
                {
                    case System.Data.ConnectionState.Closed:
                        conneciton.Open();
                        break;
                }
                string query = "select * from kategori where modulid=?modulid and menu=?menu and aktif=1 order by sira asc, adi asc limit 20";
                switch (menuType)
                {
                    case 1:
                        query = "select * from kategori where modulid=?modulid and menu=?menu and aktif=1 order by sira asc, parentid asc limit 12";
                        break;
                }
                using (MCommand cmd = new MCommand(CommandType.Text, query, conneciton))
                {
                    cmd.Parameters.Add("modulid", modulid, MSqlDbType.VarChar);
                    cmd.Parameters.Add("menu", (byte)menuType, MSqlDbType.Byte);
                    using (IDataReader IDR = cmd.ExecuteReader())
                    {
                        while (IDR.Read())
                        {
                            rvKategori.Add(new Kategori(MConvert.NullToString(IDR["id"]), MConvert.NullToString(IDR["parentid"]), MConvert.NullToString(IDR["modulid"]), MConvert.NullToString(IDR["adi"]), MConvert.NullToString(IDR["dil"]), MConvert.NullToByte(IDR["sira"]), MConvert.NullToByte(IDR["menu"]), MConvert.NullToBool(IDR["aktif"]), MConvert.NullToString(IDR["renk"]), MConvert.NullToString(IDR["aciklama"]), MConvert.NullToString(IDR["etiket"])));
                        }
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
            return rvKategori;
        }

        [System.ComponentModel.DataObjectMethod(System.ComponentModel.DataObjectMethodType.Select)]
        public static BAYMYO.UI.Data.HierarchicalCollection GetHierarchical(string modulid, bool rootNode)
        {
            BAYMYO.UI.Data.HierarchicalCollection rvKategori = new BAYMYO.UI.Data.HierarchicalCollection();
            using (MConnection conneciton = new MConnection(MClientProvider.MySQL))
            {
                switch (conneciton.State)
                {
                    case System.Data.ConnectionState.Closed:
                        conneciton.Open();
                        break;
                }
                using (MCommand cmd = new MCommand(CommandType.Text, "select * from kategori where modulid=?modulid order by id asc", conneciton))
                {
                    cmd.Parameters.Add("modulid", modulid, MSqlDbType.VarChar);
                    using (IDataReader IDR = cmd.ExecuteReader())
                    {
                        if (rootNode)
                            rvKategori.Add(new BAYMYO.UI.Data.Hierarchical("0", "", "Yeni Kategori"));
                        while (IDR.Read())
                            rvKategori.Add(new BAYMYO.UI.Data.Hierarchical(MConvert.NullToString(IDR["id"]), MConvert.NullToString(IDR["parentid"]), MConvert.NullToString(IDR["adi"])));
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
            return rvKategori;
        }

        ///<summary>
        /// Kategori Data Select
        ///</summary>
        [System.ComponentModel.DataObjectMethod(System.ComponentModel.DataObjectMethodType.Select)]
        public static KategoriCollection GetSelect()
        {
            KategoriCollection rvKategori = new KategoriCollection();
            using (MConnection conneciton = new MConnection(MClientProvider.MySQL))
            {
                switch (conneciton.State)
                {
                    case System.Data.ConnectionState.Closed:
                        conneciton.Open();
                        break;
                }
                using (MCommand cmd = new MCommand(CommandType.Text, "select * from kategori", conneciton))
                {

                    using (IDataReader IDR = cmd.ExecuteReader())
                    {
                        while (IDR.Read())
                            rvKategori.Add(new Kategori(MConvert.NullToString(IDR["id"]), MConvert.NullToString(IDR["parentid"]), MConvert.NullToString(IDR["modulid"]), MConvert.NullToString(IDR["adi"]), MConvert.NullToString(IDR["dil"]), MConvert.NullToByte(IDR["sira"]), MConvert.NullToByte(IDR["menu"]), MConvert.NullToBool(IDR["aktif"]), MConvert.NullToString(IDR["renk"]), MConvert.NullToString(IDR["aciklama"]), MConvert.NullToString(IDR["etiket"])));
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
            return rvKategori;
        }

        ///<summary>
        /// Kategori Data Insert
        ///</summary>
        [System.ComponentModel.DataObjectMethod(System.ComponentModel.DataObjectMethodType.Insert)]
        public static string Insert(Kategori p)
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
                using (MCommand cmd = new MCommand(CommandType.Text, "select count(*) as totalcount from kategori where modulid=?modulid and adi=?adi", conneciton))
                {
                    cmd.Parameters.Add("modulid", p.ModulID, MSqlDbType.VarChar);
                    cmd.Parameters.Add("adi", p.Adi, MSqlDbType.VarChar);
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
                        return "ADI";
                    }
                }
                using (MCommand cmd = new MCommand(CommandType.Text, "insert into kategori (id,parentid,modulid,adi,dil,sira,menu,aktif,renk,aciklama,etiket) values(?id,?parentid,?modulid,?adi,?dil,?sira,?menu,?aktif,?renk,?aciklama,?etiket)", conneciton))
                {
                    cmd.Parameters.Add("id", p.ID, MSqlDbType.VarChar);
                    cmd.Parameters.Add("parentid", p.ParentID, MSqlDbType.VarChar);
                    cmd.Parameters.Add("modulid", p.ModulID, MSqlDbType.VarChar);
                    cmd.Parameters.Add("adi", p.Adi, MSqlDbType.VarChar);
                    cmd.Parameters.Add("dil", p.Dil, MSqlDbType.VarChar);
                    cmd.Parameters.Add("sira", p.Sira, MSqlDbType.SmallInt);
                    cmd.Parameters.Add("menu", p.Menu, MSqlDbType.Byte);
                    cmd.Parameters.Add("aktif", p.Aktif, MSqlDbType.Boolean);
                    cmd.Parameters.Add("renk", p.Renk, MSqlDbType.VarChar);
                    cmd.Parameters.Add("aciklama", p.Aciklama, MSqlDbType.VarChar);
                    cmd.Parameters.Add("etiket", p.Etiket, MSqlDbType.VarChar);
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
        /// Kategori Data Update
        ///</summary>
        [System.ComponentModel.DataObjectMethod(System.ComponentModel.DataObjectMethodType.Update)]
        public static string Update(Kategori p)
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
                using (MCommand cmd = new MCommand(CommandType.Text, "select count(*) as totalcount from kategori where id<>?id and modulid=?modulid and adi=?adi", conneciton))
                {
                    cmd.Parameters.Add("id", p.ID, MSqlDbType.VarChar);
                    cmd.Parameters.Add("modulid", p.ModulID, MSqlDbType.VarChar);
                    cmd.Parameters.Add("adi", p.Adi, MSqlDbType.VarChar);
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
                        return "ADI";
                    }
                }
                using (MCommand cmd = new MCommand(CommandType.Text, "update kategori set id=?id,parentid=?parentid,modulid=?modulid,adi=?adi,dil=?dil,sira=?sira,menu=?menu,aktif=?aktif,renk=?renk,aciklama=?aciklama,etiket=?etiket where modulid=?modulid and id=?id", conneciton))
                {
                    cmd.Parameters.Add("id", p.ID, MSqlDbType.VarChar);
                    cmd.Parameters.Add("parentid", p.ParentID, MSqlDbType.VarChar);
                    cmd.Parameters.Add("modulid", p.ModulID, MSqlDbType.VarChar);
                    cmd.Parameters.Add("adi", p.Adi, MSqlDbType.VarChar);
                    cmd.Parameters.Add("dil", p.Dil, MSqlDbType.VarChar);
                    cmd.Parameters.Add("sira", p.Sira, MSqlDbType.SmallInt);
                    cmd.Parameters.Add("menu", p.Menu, MSqlDbType.Byte);
                    cmd.Parameters.Add("aktif", p.Aktif, MSqlDbType.Boolean);
                    cmd.Parameters.Add("renk", p.Renk, MSqlDbType.VarChar);
                    cmd.Parameters.Add("aciklama", p.Aciklama, MSqlDbType.VarChar);
                    cmd.Parameters.Add("etiket", p.Etiket, MSqlDbType.VarChar);
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
        /// Kategori Data Delete
        ///</summary>
        [System.ComponentModel.DataObjectMethod(System.ComponentModel.DataObjectMethodType.Delete)]
        public static int Delete(string modulid, string id)
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
                using (MCommand cmd = new MCommand(CommandType.Text, "delete from kategori where modulid=?modulid and id like ?id", conneciton))
                {
                    cmd.Parameters.Add("modulid", modulid, MSqlDbType.VarChar);
                    cmd.Parameters.Add("id", id + "%", MSqlDbType.VarChar);
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
