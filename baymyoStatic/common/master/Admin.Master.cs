using System;

namespace baymyoStatic.master
{
    public partial class Admin : System.Web.UI.MasterPage
    {
        public int toplamSayi = 0, hesaplar = 0, haberler = 0, makaleler = 0, sorular = 0, yorumlar = 0;

        protected void Page_Load(object sender, EventArgs e)
        {
            switch (Core.CurrentUser.Tipi)
            {
                case AccountType.Admin:
                case AccountType.Doctor:
                case AccountType.Editor:
                case AccountType.Private:
                    #region --- Menu ---
                    hesaplar = HesapMethods.Count(false);
                    haberler = HaberMethods.Count(false);
                    makaleler = MakaleMethods.Count(false);
                    //ilanlar = SeriIlanMethods.Count(false);
                    //firmalar = FirmaMethods.Count(false);
                    sorular = MesajMethods.Count(1, false);
                    yorumlar = YorumMethods.Count(false);
                    toplamSayi = (hesaplar + haberler + makaleler + sorular + yorumlar);
                    #endregion
                    break;
            }
        }
    }
}