using System;
using System.Web;

namespace baymyoStatic.common.service
{
    public partial class Default : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        [System.Web.Services.WebMethod]
        public static bool MapsSaved(string point, string zoom, string title, string description)
        {
            if (Core.IsUserActive & !string.IsNullOrEmpty(point))
            {
                System.Threading.Thread.Sleep(3000);
                Hesap hsp = Core.CurrentUser;
                if (hsp != null)
                {
                    if (!string.IsNullOrEmpty(hsp.ID) & hsp.Aktif)
                    {
                        switch (hsp.Tipi)
                        {
                            case AccountType.None:
                            case AccountType.Standart:
                                return false;
                            default:
                                string[] kordinatlar = point.Replace("(", "").Replace(")", "").Replace(" ", "").Split(',');
                                jSonData.CreateFile("maps", hsp.ID.ToString(),
                                   new Maps
                                   {
                                       Lat = kordinatlar[0],
                                       Lng = kordinatlar[1],
                                       Zoom = zoom,
                                       Title = title,
                                       Description = description
                                   });
                                return true;
                        }
                    }
                }
            }
            return false;
        }
        [System.Web.Services.WebMethod]
        public static bool MapsSaved(string point, string zoom, string title, string description, string subPath, string fileName)
        {
            if (Core.IsUserActive & !string.IsNullOrEmpty(point) & !string.IsNullOrEmpty(subPath) & !string.IsNullOrEmpty(fileName))
            {
                System.Threading.Thread.Sleep(3000);
                string[] kordinatlar = point.Replace("(", "").Replace(")", "").Replace(" ", "").Split(',');
                jSonData.CreateFile("maps/" + subPath + "/", fileName,
                   new Maps
                   {
                       Lat = kordinatlar[0],
                       Lng = kordinatlar[1],
                       Zoom = zoom,
                       Title = title,
                       Description = description
                   });
                return true;
            }
            return false;
        }
        [System.Web.Services.WebMethod]
        public static bool MapsRemove()
        {
            if (Core.IsUserActive)
            {
                System.Threading.Thread.Sleep(3000);
                Hesap hsp = Core.CurrentUser;
                if (hsp != null)
                    if (!string.IsNullOrEmpty(hsp.ID) & hsp.Aktif)
                    {
                        BAYMYO.UI.FileIO.Remove(HttpContext.Current.Server.MapPath(Settings.JSonPath + "maps/" + hsp.ID + ".js"));
                        return true;
                    }
            }
            return false;
        }
        [System.Web.Services.WebMethod]
        public static bool MapsRemove(string subPath, string fileName)
        {
            if (Core.IsUserActive)
            {
                System.Threading.Thread.Sleep(3000);
                Hesap hsp = Core.CurrentUser;
                if (!string.IsNullOrEmpty(subPath) & !string.IsNullOrEmpty(fileName))
                    return BAYMYO.UI.FileIO.Remove(HttpContext.Current.Server.MapPath(Settings.JSonPath + "maps/" + subPath + "/" + fileName + ".js"));
            }
            return false;
        }
    }
}