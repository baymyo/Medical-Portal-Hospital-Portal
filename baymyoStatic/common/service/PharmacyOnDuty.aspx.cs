using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace baymyoStatic.common.service
{
    public partial class PharmacyOnDuty : System.Web.UI.Page
    {
        public int heigth = 70;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (System.IO.File.Exists(Server.MapPath(Settings.XmlPath + "adv_728x90_1.xml"))
                || !string.IsNullOrWhiteSpace(BAYMYO.UI.FileIO.ReadText(Server.MapPath(Settings.ViewPath) + "Adventure728x90.view")))
                heigth = 165;
        }
    }
}