using System;
using System.Collections.Generic;
using System.Web;

namespace baymyoStatic
{
    public class PortalStyle : IDisposable
    {
        #region IDisposable Members
        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }
        #endregion

        public string CssCategory { get; set; }
        public string CssListTitle { get; set; }
        public string CssForm { get; set; }
        public string CssFormOther { get; set; }
        public string CssBand { get; set; }
        public string CssBandOther { get; set; }
        public string CssFlashNews { get; set; }
        public string CssFlashNewsOther { get; set; }
        public string CssArticleNews { get; set; }
        public string CssArticleNewsOther { get; set; }
        public string CssLastNews { get; set; }
        public string CssLastNewsOther { get; set; }
        public string CssHitNews { get; set; }
        public string CssHitNewsOther { get; set; }
        public string CssPaper { get; set; }
        public string CssPaperOther { get; set; }
        public string CssListViewName { get; set; }

        public PortalStyle()
        {
            CssCategory = "#cf0a0a";
            CssListTitle = "default";
            CssForm = "default";
            CssFormOther = "redDark";
            CssBand = "default";
            CssBandOther = "defaultDark";
            CssFlashNews = "grayDark";
            CssFlashNewsOther = "yellow";
            CssArticleNews = "goldDark";
            CssArticleNewsOther = "redDark";
            CssLastNews = "default";
            CssLastNewsOther = "redDark";
            CssHitNews = "indigo";
            CssHitNewsOther = "indigoDark";
            CssPaper = "yellow";
            CssPaperOther = "yellowDark";
            CssListViewName = "single-list";
        }

        public override string ToString()
        {
            return "www.baymyo.com";
        }
    }

    public class PortalStyleMethods
    {
        public static string FilePath { get { return HttpContext.Current.Server.MapPath(Settings.ViewPath) + "PortalStyle"; } }

        public static System.Web.UI.WebControls.ListItem[] GetCssStyles()
        {
            System.Web.UI.WebControls.ListItem[] lst = {
                                 new System.Web.UI.WebControls.ListItem("Default Style", "default"),
                                 new System.Web.UI.WebControls.ListItem("Default Dark Style", "defaultDark"),
                                 new System.Web.UI.WebControls.ListItem("Blue Style", "blue"),
                                 new System.Web.UI.WebControls.ListItem("Blue Dark Style", "blueDark"),
                                 new System.Web.UI.WebControls.ListItem("BlueGrey Style", "blueGrey"),
                                 new System.Web.UI.WebControls.ListItem("BlueGrey Dark Style", "blueGreyDark"),
                                 new System.Web.UI.WebControls.ListItem("Red Style", "red"),
                                 new System.Web.UI.WebControls.ListItem("Red Dark Style", "redDark"),
                                 new System.Web.UI.WebControls.ListItem("Grey Style", "gray"),
                                 new System.Web.UI.WebControls.ListItem("Grey Dark Style", "grayDark"),
                                 new System.Web.UI.WebControls.ListItem("Gold Style", "gold"),
                                 new System.Web.UI.WebControls.ListItem("Gold Dark Style", "goldDark"),
                                 new System.Web.UI.WebControls.ListItem("Green Style", "green"),
                                 new System.Web.UI.WebControls.ListItem("Green Dark Style", "greenDark"),
                                 new System.Web.UI.WebControls.ListItem("Pink Style", "pink"),
                                 new System.Web.UI.WebControls.ListItem("Pink Dark Style", "pinkDark"),
                                 new System.Web.UI.WebControls.ListItem("Orange Style", "orange"),
                                 new System.Web.UI.WebControls.ListItem("Orange Dark Style", "orangeDark"),
                                 new System.Web.UI.WebControls.ListItem("Yellow Style", "yellow"),
                                 new System.Web.UI.WebControls.ListItem("Yellow Dark Style", "yellowDark"),
                                 new System.Web.UI.WebControls.ListItem("Brown Style", "brown"),
                                 new System.Web.UI.WebControls.ListItem("Brown Dark Style", "brownDark"),
                                 new System.Web.UI.WebControls.ListItem("Indigo Style", "indigo"),
                                 new System.Web.UI.WebControls.ListItem("Indigo Dark Style", "indigoDark")
                             };
            return lst;
        }

        public static PortalStyle Read()
        {
            PortalStyle rv = null;
            try
            {
                //if (HttpContext.Current.Cache["portalStyle"] == null)
                using (System.IO.StreamReader sr = new System.IO.StreamReader(FilePath))
                {
                    System.Web.Script.Serialization.JavaScriptSerializer javaScriptSerializer = new System.Web.Script.Serialization.JavaScriptSerializer();
                    rv = javaScriptSerializer.Deserialize<PortalStyle>(sr.ReadToEnd());
                    //HttpContext.Current.Cache["PortalStyles"] = rv;
                    sr.Close();
                }
                //else
                //    rv = HttpContext.Current.Cache["portalStyle"] as Portal;
            }
            catch (Exception)
            {
                return new PortalStyle();
            }
            return rv;
        }

        public static bool Save(PortalStyle data)
        {
            try
            {
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
    }
}