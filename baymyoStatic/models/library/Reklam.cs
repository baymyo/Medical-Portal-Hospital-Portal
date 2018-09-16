using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Data;
using BAYMYO.MultiSQLClient;

namespace baymyoStatic
{
    public partial class ReklamCollection : CollectionBase, IDisposable
    {
        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }

        public Reklam this[int index]
        {
            get { return (Reklam)this.List[index]; }
            set { this.List[index] = value; }
        }

        public object SyncRoot { get { return this.List.SyncRoot; } }

        public int Add(Reklam obj)
        {
            return this.List.Add(obj);
        }

        public void Insert(int index, Reklam obj)
        {
            this.List.Insert(index, obj);
        }

        public bool Contains(Reklam obj)
        {
            return this.List.Contains(obj);
        }

        public int IndexOf(Reklam obj)
        {
            return this.List.IndexOf(obj);
        }

        public void Remove(Reklam obj)
        {
            this.List.Remove(obj);
        }
    }

    public partial class Reklam : IDisposable
    {
        #region ---IDisposable Members---
        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }
        #endregion

        #region ---Properties/Field - Özellikler Alanlar---
        private int m_ID;
        public int ID
        {
            get { return m_ID; }
            set { m_ID = value; }
        }
        private string m_BannerName;
        public string BannerName
        {
            get { return m_BannerName; }
            set { m_BannerName = value; }
        }
        private string m_ImageUrl;
        public string ImageUrl
        {
            get { return m_ImageUrl; }
            set { m_ImageUrl = value; }
        }
        private string m_NavigateUrl;
        public string NavigateUrl
        {
            get { return m_NavigateUrl; }
            set { m_NavigateUrl = value; }
        }
        private string m_AlternateText;
        public string AlternateText
        {
            get { return m_AlternateText; }
            set { m_AlternateText = value; }
        }
        private string m_Keyword;
        public string Keyword
        {
            get { return m_Keyword; }
            set { m_Keyword = value; }
        }
        private string m_AdSenseClient;
        public string AdSenseClient
        {
            get { return m_AdSenseClient; }
            set { m_AdSenseClient = value; }
        }
        private string m_AdSenseSlot;
        public string AdSenseSlot
        {
            get { return m_AdSenseSlot; }
            set { m_AdSenseSlot = value; }
        }
        private int m_Impressions;
        public int Impressions
        {
            get { return m_Impressions; }
            set { m_Impressions = value; }
        }
        private int m_Width;
        public int Width
        {
            get { return m_Width; }
            set { m_Width = value; }
        }
        private int m_Height;
        public int Height
        {
            get { return m_Height; }
            set { m_Height = value; }
        }
        private byte m_Orders;
        public byte Orders
        {
            get { return m_Orders; }
            set { m_Orders = value; }
        }
        private bool m_IsActive;
        public bool IsActive
        {
            get { return m_IsActive; }
            set { m_IsActive = value; }
        }
        #endregion

        public Reklam()
        {
        }

        /// <summary>
        /// Reklam Nesnesi Oluþtur
        /// </summary>
        public Reklam(int pid, string pbannername, string pimageurl, string pnavigateurl, string palternatetext, string pkeyword, string padsenseclient, string padsenseslot, int pimpressions, int pwidth, int pheight, byte porders, bool pisactive)
        {
            this.m_ID = pid;
            this.m_BannerName = pbannername;
            this.m_ImageUrl = pimageurl;
            this.m_NavigateUrl = pnavigateurl;
            this.m_AlternateText = palternatetext;
            this.m_Keyword = pkeyword;
            this.m_AdSenseClient = padsenseclient;
            this.m_AdSenseSlot = padsenseslot;
            this.m_Impressions = pimpressions;
            this.m_Width = pwidth;
            this.m_Height = pheight;
            this.m_Orders = porders;
            this.m_IsActive = pisactive;
        }
    }

    public partial class ReklamMethods
    {
        ///<summary>
        /// Reklam Data PrimaryKey
        ///</summary>
        public static Reklam GetReklam(int pid)
        {
            Reklam rvReklam = new Reklam();
            using (MConnection conneciton = new MConnection(MClientProvider.MySQL))
            {
                switch (conneciton.State)
                {
                    case System.Data.ConnectionState.Closed:
                        conneciton.Open();
                        break;
                }
                using (MCommand cmd = new MCommand(CommandType.Text, "select * from reklam where id=?id", conneciton))
                {
                    cmd.Parameters.Add("id", pid, MSqlDbType.Int);
                    using (IDataReader IDR = cmd.ExecuteReader())
                    {
                        while (IDR.Read())
                            rvReklam = new Reklam(MConvert.NullToInt(IDR["id"]), MConvert.NullToString(IDR["bannername"]), MConvert.NullToString(IDR["imageurl"]), MConvert.NullToString(IDR["navigateurl"]), MConvert.NullToString(IDR["alternatetext"]), MConvert.NullToString(IDR["keyword"]), MConvert.NullToString(IDR["adsenseclient"]), MConvert.NullToString(IDR["adsenseslot"]), MConvert.NullToInt(IDR["impressions"]), MConvert.NullToInt(IDR["width"]), MConvert.NullToInt(IDR["height"]), IDR.GetByte(11), MConvert.NullToBool(IDR["isactive"]));
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
            return rvReklam;
        }
        public static ReklamCollection GetSelect(int width, int height, byte orders)
        {
            ReklamCollection rvReklam = new ReklamCollection();
            using (MConnection conneciton = new MConnection(MClientProvider.MySQL))
            {
                switch (conneciton.State)
                {
                    case System.Data.ConnectionState.Closed:
                        conneciton.Open();
                        break;
                }
                using (MCommand cmd = new MCommand(CommandType.Text, "select * from reklam r where r.width=?width and r.height=?height and r.orders=?orders and r.isactive=1 limit 99", conneciton))
                {
                    cmd.Parameters.Add("width", width, MSqlDbType.Int);
                    cmd.Parameters.Add("height", height, MSqlDbType.Int);
                    cmd.Parameters.Add("orders", orders, MSqlDbType.TinyInt);
                    using (IDataReader IDR = cmd.ExecuteReader())
                    {
                        while (IDR.Read())
                            rvReklam.Add(new Reklam(MConvert.NullToInt(IDR["id"]), MConvert.NullToString(IDR["bannername"]), MConvert.NullToString(IDR["imageurl"]), MConvert.NullToString(IDR["navigateurl"]), MConvert.NullToString(IDR["alternatetext"]), MConvert.NullToString(IDR["keyword"]), MConvert.NullToString(IDR["adsenseclient"]), MConvert.NullToString(IDR["adsenseslot"]), MConvert.NullToInt(IDR["impressions"]), MConvert.NullToInt(IDR["width"]), MConvert.NullToInt(IDR["height"]), IDR.GetByte(11), MConvert.NullToBool(IDR["isactive"])));
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
            return rvReklam;
        }
        ///<summary>
        /// Reklam Data Insert
        ///</summary>
        public static int Insert(Reklam p)
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
                using (MCommand cmd = new MCommand(CommandType.Text, "insert into reklam (bannername,imageurl,navigateurl,alternatetext,keyword,adsenseclient,adsenseslot,impressions,width,height,orders,isactive) values(?bannername,?imageurl,?navigateurl,?alternatetext,?keyword,?adsenseclient,?adsenseslot,?impressions,?width,?height,?orders,?isactive)", conneciton))
                {
                    cmd.Parameters.Add("bannername", p.BannerName, MSqlDbType.VarChar);
                    cmd.Parameters.Add("imageurl", p.ImageUrl, MSqlDbType.LongText);
                    cmd.Parameters.Add("navigateurl", p.NavigateUrl, MSqlDbType.VarChar);
                    cmd.Parameters.Add("alternatetext", p.AlternateText, MSqlDbType.VarChar);
                    cmd.Parameters.Add("keyword", p.Keyword, MSqlDbType.VarChar);
                    cmd.Parameters.Add("adsenseclient", p.AdSenseClient, MSqlDbType.VarChar);
                    cmd.Parameters.Add("adsenseslot", p.AdSenseSlot, MSqlDbType.VarChar);
                    cmd.Parameters.Add("impressions", p.Impressions, MSqlDbType.Int);
                    cmd.Parameters.Add("width", p.Width, MSqlDbType.Int);
                    cmd.Parameters.Add("height", p.Height, MSqlDbType.Int);
                    cmd.Parameters.Add("orders", p.Orders, MSqlDbType.TinyInt);
                    cmd.Parameters.Add("isactive", p.IsActive, MSqlDbType.Boolean);
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
        /// Reklam Data Update
        ///</summary>
        public static int Update(Reklam p)
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
                using (MCommand cmd = new MCommand(CommandType.Text, "update reklam set bannername=?bannername,imageurl=?imageurl,navigateurl=?navigateurl,alternatetext=?alternatetext,keyword=?keyword,adsenseclient=?adsenseclient,adsenseslot=?adsenseslot,impressions=?impressions,width=?width,height=?height,orders=?orders,isactive=?isactive where id=?id", conneciton))
                {
                    cmd.Parameters.Add("id", p.ID, MSqlDbType.Int);
                    cmd.Parameters.Add("bannername", p.BannerName, MSqlDbType.VarChar);
                    cmd.Parameters.Add("imageurl", p.ImageUrl, MSqlDbType.LongText);
                    cmd.Parameters.Add("navigateurl", p.NavigateUrl, MSqlDbType.VarChar);
                    cmd.Parameters.Add("alternatetext", p.AlternateText, MSqlDbType.VarChar);
                    cmd.Parameters.Add("keyword", p.Keyword, MSqlDbType.VarChar);
                    cmd.Parameters.Add("adsenseclient", p.AdSenseClient, MSqlDbType.VarChar);
                    cmd.Parameters.Add("adsenseslot", p.AdSenseSlot, MSqlDbType.VarChar);
                    cmd.Parameters.Add("impressions", p.Impressions, MSqlDbType.Int);
                    cmd.Parameters.Add("width", p.Width, MSqlDbType.Int);
                    cmd.Parameters.Add("height", p.Height, MSqlDbType.Int);
                    cmd.Parameters.Add("orders", p.Orders, MSqlDbType.TinyInt);
                    cmd.Parameters.Add("isactive", p.IsActive, MSqlDbType.Boolean);
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
        /// Reklam Data Delete
        ///</summary>
        public static int Delete(int pid)
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
                using (MCommand cmd = new MCommand(CommandType.Text, "delete from reklam where id=?id", conneciton))
                {
                    cmd.Parameters.Add("id", pid, MSqlDbType.Int);
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
        public static int Delete(Reklam p)
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
                using (MCommand cmd = new MCommand(CommandType.Text, "delete from reklam where id=?id", conneciton))
                {
                    cmd.Parameters.Add("id", p.ID, MSqlDbType.Int);
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