using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using System.Data;
using BAYMYO.UI;

namespace baymyoStatic
{
    public static class Block
    {
        #region ---Properties/Field - Özellikler Alanlar---
        public static string ThemePath
        {
            get { return Settings.CommonPath + "master/"; }
        }
        public static string AscxPath
        {
            get { return Settings.AscxPath; }
        }
        public static string HtmlPath
        {
            get { return Settings.HtmlPath; }
        }
        public static string XmlPath
        {
            get { return Settings.XmlPath + "bloklar.xml"; }
        }
        public static string XmlGaleriPath
        {
            get { return Settings.XmlPath + "blokgaleri.xml"; }
        }
        public static string XmlMobilePath
        {
            get { return Settings.XmlPath + "blokmobile.xml"; }
        }
        #endregion

        #region ---Methot/Fonksiyonlar---
        public static DataTable GetXmlData(string mdl)
        {
            DataTable XMLData = new DataTable("Bloklar");
            try
            {
                XMLData.Columns.Add("ID", typeof(int));
                XMLData.Columns.Add("Adi", typeof(string));
                XMLData.Columns.Add("Baslik", typeof(string));
                XMLData.Columns.Add("Yer", typeof(string));
                XMLData.Columns.Add("SablonTipi", typeof(string));
                XMLData.Columns.Add("Sira", typeof(Int16));
                XMLData.Columns.Add("Dil", typeof(string));
                XMLData.Columns.Add("TumDil", typeof(bool));
                XMLData.Columns.Add("Sayfa", typeof(string));
                XMLData.Columns.Add("TumSayfa", typeof(bool));
                XMLData.Columns.Add("Tipi", typeof(string));
                XMLData.Columns.Add("Aktif", typeof(bool));

                XMLData.Columns["ID"].AllowDBNull = false;
                XMLData.Columns["ID"].AutoIncrement = true;
                XMLData.Columns["ID"].AutoIncrementStep = 1;
                XMLData.Columns["ID"].AutoIncrementSeed = 1;

                string xmlPath = null;
                switch (mdl)
                {
                    case "mobile":
                        xmlPath = HttpContext.Current.Server.MapPath(Block.XmlMobilePath);
                        break;
                    case "galeri":
                        xmlPath = HttpContext.Current.Server.MapPath(Block.XmlGaleriPath);
                        break;
                    default:
                        xmlPath = HttpContext.Current.Server.MapPath(Block.XmlPath);
                        break;
                }
                if (!System.IO.File.Exists(xmlPath))
                    XMLData.WriteXml(xmlPath);
                XMLData.ReadXml(xmlPath);
                xmlPath = null;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return XMLData;
        }
        public static DataTable GetFiles(BlockType blokType)
        {
            switch (blokType)
            {
                case BlockType.Ascx:
                    return BAYMYO.UI.FileIO.ReadDirectory(HttpContext.Current.Server.MapPath(Block.AscxPath), "*.ascx");
                default:
                    return BAYMYO.UI.FileIO.ReadDirectory(HttpContext.Current.Server.MapPath(Block.HtmlPath), "*.bhtml");
            }
        }
        public static DataTable GetLocations()
        {
            DataTable dt = new DataTable("Yerlesimler");
            dt.Columns.Add("Key", typeof(string));
            dt.Columns.Add("Value", typeof(string));
            dt.Columns.Add("Display", typeof(string));

            DataRow dr;

            #region ---Tüm Blok Seçenekleri---
            string title = "Tum";
            dr = dt.NewRow();
            dr[0] = title + "_EnUst";
            dr[1] = "<a class=\"toolTip\" title=\"<img src=" + Settings.ImagesPath + "yerlesim/enust.png>\">" + title + " - En Üst Blok</a>";
            dr[2] = title + " - En Üst Blok";
            dt.Rows.Add(dr);

            dr = dt.NewRow();
            dr[0] = title + "_Ust";
            dr[1] = "<a class=\"toolTip\" title=\"<img src=" + Settings.ImagesPath + "yerlesim/ust.png>\">" + title + " - Üst Blok</a>";
            dr[2] = title + " - Üst Blok";
            dt.Rows.Add(dr);

            dr = dt.NewRow();
            dr[0] = title + "_UstKemer";
            dr[1] = "<a class=\"toolTip\" title=\"<img src=" + Settings.ImagesPath + "yerlesim/ustkemerblok.png>\">" + title + " - Üst Kemer Blok</a>";
            dr[2] = title + " - Üst Kemer Blok";
            dt.Rows.Add(dr);

            dr = dt.NewRow();
            dr[0] = title + "_Sol";
            dr[1] = "<a class=\"toolTip\" title=\"<img src=" + Settings.ImagesPath + "yerlesim/sol.png>\">" + title + " - Sol Blok</a>";
            dr[2] = title + " - Sol Blok";
            dt.Rows.Add(dr);

            dr = dt.NewRow();
            dr[0] = title + "_Orta";
            dr[1] = "<a class=\"toolTip\" title=\"<img src=" + Settings.ImagesPath + "yerlesim/orta.png>\">" + title + " - Orta Blok</a>";
            dr[2] = title + " - Orta Blok";
            dt.Rows.Add(dr);

            dr = dt.NewRow();
            dr[0] = title + "_Sag";
            dr[1] = "<a class=\"toolTip\" title=\"<img src=" + Settings.ImagesPath + "yerlesim/sag.png>\">" + title + " - Sag Blok</a>";
            dr[2] = title + " - Sag Blok";
            dt.Rows.Add(dr);

            dr = dt.NewRow();
            dr[0] = title + "_Alt";
            dr[1] = "<a class=\"toolTip\" title=\"<img src=" + Settings.ImagesPath + "yerlesim/alt.png>\">" + title + " - Alt Blok</a>";
            dr[2] = title + " - Alt Blok";
            dt.Rows.Add(dr);
            #endregion

            #region ---Anasayfa Blok Seçenekleri---
            title = "Anasayfa";
            dr = dt.NewRow();
            dr[0] = title + "_EnUst";
            dr[1] = "<a class=\"toolTip\" title=\"<img src=" + Settings.ImagesPath + "yerlesim/enust.png>\">" + title + " - En Üst Blok</a>";
            dr[2] = title + " - En Üst Blok";
            dt.Rows.Add(dr);

            dr = dt.NewRow();
            dr[0] = title + "_Ust";
            dr[1] = "<a class=\"toolTip\" title=\"<img src=" + Settings.ImagesPath + "yerlesim/ust.png>\">" + title + " - Üst Blok</a>";
            dr[2] = title + " - Üst Blok";
            dt.Rows.Add(dr);

            dr = dt.NewRow();
            dr[0] = title + "_UstKemer";
            dr[1] = "<a class=\"toolTip\" title=\"<img src=" + Settings.ImagesPath + "yerlesim/ustkemerblok.png>\">" + title + " - Üst Kemer Blok</a>";
            dr[2] = title + " - Üst Kemer Blok";
            dt.Rows.Add(dr);

            dr = dt.NewRow();
            dr[0] = title + "_Sol";
            dr[1] = "<a class=\"toolTip\" title=\"<img src=" + Settings.ImagesPath + "yerlesim/sol.png>\">" + title + " - Sol Blok</a>";
            dr[2] = title + " - Sol Blok";
            dt.Rows.Add(dr);

            dr = dt.NewRow();
            dr[0] = title + "_Orta";
            dr[1] = "<a class=\"toolTip\" title=\"<img src=" + Settings.ImagesPath + "yerlesim/orta.png>\">" + title + " - Orta Blok</a>";
            dr[2] = title + " - Orta Blok";
            dt.Rows.Add(dr);

            dr = dt.NewRow();
            dr[0] = title + "_Sag";
            dr[1] = "<a class=\"toolTip\" title=\"<img src=" + Settings.ImagesPath + "yerlesim/sag.png>\">" + title + " - Sag Blok</a>";
            dr[2] = title + " - Sag Blok";
            dt.Rows.Add(dr);

            dr = dt.NewRow();
            dr[0] = title + "_Alt";
            dr[1] = "<a class=\"toolTip\" title=\"<img src=" + Settings.ImagesPath + "yerlesim/alt.png>\">" + title + " - Alt Blok</a>";
            dr[2] = title + " - Alt Blok";
            dt.Rows.Add(dr);
            #endregion

            #region ---Icerik Blok Seçenekleri---
            title = "Icerik";
            dr = dt.NewRow();
            dr[0] = title + "_EnUst";
            dr[1] = "<a class=\"toolTip\" title=\"<img src=" + Settings.ImagesPath + "yerlesim/ic_enust.png>\">" + title + " - En Üst Blok</a>";
            dr[2] = title + " - En Üst Blok";
            dt.Rows.Add(dr);

            dr = dt.NewRow();
            dr[0] = title + "_Ust";
            dr[1] = "<a class=\"toolTip\" title=\"<img src=" + Settings.ImagesPath + "yerlesim/ic_ust.png>\">" + title + " - Üst Blok</a>";
            dr[2] = title + " - Üst Blok";
            dt.Rows.Add(dr);

            dr = dt.NewRow();
            dr[0] = title + "_UstKemer";
            dr[1] = "<a class=\"toolTip\" title=\"<img src=" + Settings.ImagesPath + "yerlesim/ic_ustkemerblok.png>\">" + title + " - Üst Kemer Blok</a>";
            dr[2] = title + " - Üst Kemer Blok";
            dt.Rows.Add(dr);

            dr = dt.NewRow();
            dr[0] = title + "_Sol";
            dr[1] = "<a class=\"toolTip\" title=\"<img src=" + Settings.ImagesPath + "yerlesim/ic_sol.png>\">" + title + " - Sol Blok</a>";
            dr[2] = title + " - Sol Blok";
            dt.Rows.Add(dr);

            dr = dt.NewRow();
            dr[0] = title + "_Orta";
            dr[1] = "<a class=\"toolTip\" title=\"<img src=" + Settings.ImagesPath + "yerlesim/ic_orta.png>\">" + title + " - Orta Blok</a>";
            dr[2] = title + " - Orta Blok";
            dt.Rows.Add(dr);

            dr = dt.NewRow();
            dr[0] = title + "_Sag";
            dr[1] = "<a class=\"toolTip\" title=\"<img src=" + Settings.ImagesPath + "yerlesim/ic_sag.png>\">" + title + " - Sag Blok</a>";
            dr[2] = title + " - Sag Blok";
            dt.Rows.Add(dr);

            dr = dt.NewRow();
            dr[0] = title + "_Alt";
            dr[1] = "<a class=\"toolTip\" title=\"<img src=" + Settings.ImagesPath + "yerlesim/ic_alt.png>\">" + title + " - Alt Blok</a>";
            dr[2] = title + " - Alt Blok";
            dt.Rows.Add(dr);
            #endregion

            return dt;
        }
        public static DataTable GetThemeTypes(bool isHtmlBlock)
        {
            DataTable dt = new DataTable("ThemeTypes");
            dt.Columns.Add("Key", typeof(string));
            dt.Columns.Add("Value", typeof(string));

            DataRow dr;

            #region ---Blok Seçenekleri---
            switch (isHtmlBlock)
            {
                case true:
                    dr = dt.NewRow();
                    dr["Key"] = "HtmlNoTheme";
                    dr["Value"] = "Html NoTheme";
                    dt.Rows.Add(dr);

                    dr = dt.NewRow();
                    dr["Key"] = "HtmlAndTheme";
                    dr["Value"] = "Html And Theme";
                    dt.Rows.Add(dr);
                    break;
                case false:
                    dr = dt.NewRow();
                    dr["Key"] = "AscxNoTheme";
                    dr["Value"] = "Ascx NoTheme";
                    dt.Rows.Add(dr);

                    dr = dt.NewRow();
                    dr["Key"] = "AscxAndTheme";
                    dr["Value"] = "Ascx And Theme";
                    dt.Rows.Add(dr);
                    break;
            }
            #endregion

            return dt;
        }
        #endregion

        static LiteralControl AddLiteral(string path)
        {
            LiteralControl getLiteral = new LiteralControl();
            try
            {
                getLiteral.Text = FileIO.ReadText(path);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return getLiteral;
        }
        public static void Add(ContentPlaceHolder contentPlace, string themeType, string name, string title)
        {
            try
            {
                //UserControl getTheme = null; Label getTitle = null;
                if (contentPlace != null)
                    switch (themeType)
                    {
                        case "HtmlNoTheme":
                            string fullPath = HttpContext.Current.Server.MapPath(Block.HtmlPath + name);
                            if (File.Exists(fullPath))
                                contentPlace.Controls.Add(AddLiteral(fullPath));
                            fullPath = null;
                            return;
                        case "HtmlAndTheme":
                            //string fullPath = HttpContext.Current.Server.MapPath(Blok.HtmlPath + name);
                            //if (File.Exists(fullPath))
                            //getTheme = (UserControl)contentPlace.Page.LoadControl(ThemePath);
                            //getTitle = ((Label)getTheme.FindControl("Adi"));
                            //getTitle.Text = getTitle.Text.Replace("%Adi%", title);
                            //((ContentPlaceHolder)getTheme.FindControl("Content")).Controls.Add(AddLiteral(fullPath));
                            //contentPlace.Controls.Add(getTheme);
                            //fullPath = null;
                            return;
                        case "AscxNoTheme":
                            if (File.Exists(HttpContext.Current.Server.MapPath(Block.AscxPath + name)))
                            {
                                UserControl cnt = (UserControl)contentPlace.Page.LoadControl(Block.AscxPath + name);
                                contentPlace.Controls.Add(cnt);
                            }
                            return;
                        case "AscxAndTheme":
                            //if (File.Exists(HttpContext.Current.Server.MapPath(Blok.AscxPath + name)))
                            //getTheme = (UserControl)contentPlace.Page.LoadControl(ThemePath);
                            //getTitle = ((Label)getTheme.FindControl("Adi"));
                            //getTitle.Text = getTitle.Text.Replace("%Adi%", title);
                            //((ContentPlaceHolder)getTheme.FindControl("Content")).Controls.Add((UserControl)contentPlace.Page.LoadControl(Blok.AscxPath + name));
                            //contentPlace.Controls.Add(getTheme);
                            return;
                    }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}