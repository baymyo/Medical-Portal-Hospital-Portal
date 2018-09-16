
namespace baymyoStatic
{
    public enum DialogResult
    {
        Info,
        Help,
        Stop,
        Succes,
        Warning,
        Error
    }

    public enum ProccesType
    {
        None,
        Insert,
        Update,
        Delete,
        Select
    }

    public static class MessageBox
    {
        static string m_Info = "<div class=\"alert alert-info alert-dismissible\" role=\"alert\">{0}</div>";
        static string m_Success = "<div class=\"alert alert-success alert-dismissible\" role=\"alert\">{0}</div>";
        static string m_Warning = "<div class=\"alert alert-warning alert-dismissible\" role=\"alert\">{0}</div>";
        static string m_Help = "<div class=\"alert alert-warning alert-dismissible\" role=\"alert\">{0}</div>";
        static string m_Stop = "<div class=\"alert alert-danger alert-dismissible\" role=\"alert\">{0}</div>";
        static string m_Error = "<div class=\"alert alert-danger alert-dismissible\" role=\"alert\">{0}</div>";

        public static string Show(DialogResult dialogResult, string message)
        {
            switch (dialogResult)
            {
                case DialogResult.Info:
                    return string.Format(m_Info, message);
                case DialogResult.Help:
                    return string.Format(m_Help, message);
                case DialogResult.Stop:
                    return string.Format(m_Stop, message);
                case DialogResult.Succes:
                    return string.Format(m_Success, message);
                case DialogResult.Warning:
                    return string.Format(m_Warning, message);
                case DialogResult.Error:
                    return string.Format(m_Error, message);
                default:
                    return string.Empty;
            }
        }
        public static string Show(ProccesType procType, DialogResult dialogResult)
        {
            string message = string.Empty;
            switch (procType)
            {
                case ProccesType.Insert:
                    message = "Yeni kayıt ekleme";
                    break;
                case ProccesType.Update:
                    message = "Güncelleme";
                    break;
                case ProccesType.Delete:
                    message = "Silme";
                    break;
                case ProccesType.Select:
                    message = "Kayıt(ları) getirme";
                    break;
                default:
                    message = "Genel";
                    break;
            }

            switch (dialogResult)
            {
                case DialogResult.Info:
                    return string.Format(m_Info, message + " işleminiz hakkında bilgilendirme!");
                case DialogResult.Help:
                    return string.Format(m_Help, message += " işleminiz hakkında yardım!");
                case DialogResult.Stop:
                    return string.Format(m_Stop, message += " işleminiz durdurudu!");
                case DialogResult.Succes:
                    return string.Format(m_Success, message += " işleminiz başarılı bir şekilde gerçekleştirildi.");
                case DialogResult.Warning:
                    return string.Format(m_Warning, message += " işlemi gerçekleştirmek istediğinize emin misiniz?");
                case DialogResult.Error:
                    return string.Format(m_Error, message += " işleminiz sırasında hata oluştu lütfen tekrar deneyiniz!");
                default:
                    return string.Empty;
            }
        }

        public static void Show(System.Web.UI.Page viewPage, string message)
        {
            if (!string.IsNullOrEmpty(message) & viewPage != null)
                System.Web.UI.ScriptManager.RegisterStartupScript(viewPage, typeof(System.Web.UI.Page), "x", "alert('" + message + "');", true);
        }
        public static void Show(System.Web.UI.Page viewPage, ProccesType procType, DialogResult dialogResult)
        {
            string message = string.Empty;
            switch (procType)
            {
                case ProccesType.Insert:
                    message = "Yeni kayıt ekleme";
                    break;
                case ProccesType.Update:
                    message = "Güncelleme";
                    break;
                case ProccesType.Delete:
                    message = "Silme";
                    break;
                case ProccesType.Select:
                    message = "Kayıt(ları) getirme";
                    break;
                default:
                    message = "Genel";
                    break;
            }

            switch (dialogResult)
            {
                case DialogResult.Info:
                    message += " işleminiz hakkında bilgilendirme!";
                    break;
                case DialogResult.Help:
                    message += " işleminiz hakkında yardım!";
                    break;
                case DialogResult.Stop:
                    message += " işleminiz durdurudu!";
                    break;
                case DialogResult.Succes:
                    message += " işleminiz başarılı bir şekilde gerçekleştirildi.";
                    break;
                case DialogResult.Warning:
                    message += " işlemi gerçekleştirmek istediğinize emin misiniz?";
                    break;
                case DialogResult.Error:
                    message += " işleminiz sırasında hata oluştu lütfen tekrar deneyiniz!";
                    break;
            }

            Show(viewPage, message);
        }

        public static string IsNotViews()
        {
            return Show(DialogResult.Stop, "Gösterilecek içerik bulunamadı.");
        }

        public static string IsNotNull()
        {
            return Show(DialogResult.Warning, "Tüm alanların seçili olduğundan emin olunuz ve tekrar deneyiniz! Bu hata genellikle seçim alanlarının unutulmasından kaynaklanmaktadır!");
        }

        public static string AccessDenied()
        {
            return string.Format(m_Warning, "Erişmek istediğiniz içerik için yetkiniz bulunmamaktadır!");
        }

        public static string UnSuccessful()
        {
            return string.Format(m_Stop,
                "<strong>İşleminiz gerçekleştirilemiyor, bunun sebebleri aşağıdakilerden biri olabilir!</strong>"
                + "<ul>"
                + "<li>Lütfen sayfaya erişim yolunuzun doğru olduğundan emin olunuz.</li>"
                + "<li>Sunucu yoğunluğundan dolayı bu işlemi gerçekleştiremiyor olabilirsiniz.</li>"
                + "<li>Bilgilerinizin doğru olduğundan emin olunuz.</li>"
                + "<li>Bu işlemi daha önce gerçekleştirmiş olabilirsiniz.</li>"
                + "<li>Bu işlemi gerçekleştirmeye yetkiniz olduğundan emin olunuz.</li>"
                + "</ul>");
        }
    }
}