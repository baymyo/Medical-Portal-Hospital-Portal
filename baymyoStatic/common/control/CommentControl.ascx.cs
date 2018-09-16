using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace baymyoStatic.common.control
{
    public partial class CommentControl : System.Web.UI.UserControl
    {
        public string FormTitle { get { return (string)ViewState["FormTitle"]; } set { ViewState["FormTitle"] = value; } }
        public string ModulID { get { return (string)ViewState["ModulID"]; } set { ViewState["ModulID"] = value; } }
        public string IcerikID { get { return (string)ViewState["IcerikID"]; } set { ViewState["IcerikID"] = value; } }
        public bool IsCommandActive { get { return BAYMYO.UI.Converts.NullToBool(ViewState["IsCommandActive"]); } set { ViewState["IsCommandActive"] = value; } }

        private void GetDataPaging()
        {
            string query = "select p.resimurl,p.url,h.tipi,y.* from yorum y left join hesap h on h.mail=y.mail left join profil p on p.id=h.id where ",
                queryCount = "select count(y.id) from yorum y left join hesap h on h.mail=y.mail left join profil p on p.id=h.id where ";
            switch (IsCommandActive)
            {
                case true:
                    query += "y.yoneticionay=1 and y.modulid=?modulid and y.icerikid=?icerikid order by y.kayittarihi desc";
                    queryCount += "y.yoneticionay=1 and y.modulid=?modulid and y.icerikid=?icerikid";
                    break;
                default:
                    query += "y.yoneticionay=1 and y.aktif=1 and y.modulid=?modulid and y.icerikid=?icerikid order by y.kayittarihi desc";
                    queryCount += "y.yoneticionay=1 and y.aktif=1 and y.modulid=?modulid and y.icerikid=?icerikid";
                    break;
            }
            using (BAYMYO.UI.Web.DataPagers data = new BAYMYO.UI.Web.DataPagers(query, queryCount))
            {
                data.ViewDataCount = 5;
                data.DataTargetControl = rptComments;
                data.PageNumberTargetControl = pageNumberLiteral;
                data.Parameters.Add("modulid", ModulID, BAYMYO.MultiSQLClient.MSqlDbType.VarChar);
                data.Parameters.Add("icerikid", IcerikID, BAYMYO.MultiSQLClient.MSqlDbType.VarChar);
                data.Binding();
                //ltrOpenComment.Text = "<div class=\"modul-main-box\"><div id=\"comment-top\" class=\"job-set\"><span href=\"#\" class=\"writeOpen left\">" + string.Format("<div class=\"icon left\"></div><strong class=\"left\">Yorum Gönder</strong></span>&nbsp;<span class=\"count right\">{0}</span></div></div>", ((data.TotalDataCount > 0) ? "Toplam (" + data.TotalDataCount + ") adet yorum var." : "Bu içeriğe henüz yorum yapılmamış!"));
                CustomizeControl1.StatusText = ((data.TotalDataCount > 0) ? "Toplam (<b>" + data.TotalDataCount + "</b>) adet yorum var." : "<b>Bu içeriğe ilk siz yorum yapın!</b>");
            }
        }

        protected override void OnInit(EventArgs e)
        {
            if (this.Visible)
            {
                if (!Page.IsPostBack & Settings.Site.FaceBookComment)
                {
                    System.Web.UI.HtmlControls.HtmlMeta meta = new System.Web.UI.HtmlControls.HtmlMeta();
                    meta.Attributes.Add("property", "fb:admins");
                    meta.Attributes.Add("content", Settings.Site.FaceBookAdminUrl);
                    this.Page.Header.Controls.Add(meta);

                    meta = new System.Web.UI.HtmlControls.HtmlMeta();
                    meta.Attributes.Add("property", "fb:app_id");
                    meta.Attributes.Add("content", Settings.Site.FaceBookApi);
                    this.Page.Header.Controls.Add(meta);

                    facebookComment.Text = "<div class=\"clear\"></div>" + BAYMYO.UI.FileIO.ReadText(Server.MapPath(Settings.ViewPath) + "FaceBook.view").Replace("%SITE_FACEBOOK_API_KEY%", Settings.Site.FaceBookApi).Replace("%SITE_COMMENT_URL%", Request.Url.AbsoluteUri) + "<div class=\"clear\"></div>";
                }
                else
                {
                    facebookComment.Text = "BU İÇERİK FACEBOOK YORUMUNA AÇIK DEĞİL!";
                }
                TextBox txt = new TextBox();
                txt.ID = "Adi";
                switch (Core.CurrentUser.Tipi)
                {
                    case AccountType.Admin:
                    case AccountType.Private:
                    case AccountType.Doctor:
                    case AccountType.Editor:
                        txt.Text = !string.IsNullOrEmpty(Core.CurrentUser.ProfilObject.Adi) ? Core.CurrentUser.ProfilObject.Adi : Core.CurrentUser.Adi;
                        break;
                    case AccountType.Standart:
                        txt.Text = Core.CurrentUser.Adi;
                        break;
                }
                txt.CssClass = "form-control";
                txt.MaxLength = 100;
                txt.Enabled = !Core.CurrentUser.Aktif;
                txt.Visible = !Core.CurrentUser.Aktif;
                CustomizeControl1.AddControl("Adınız", txt);

                txt = new TextBox();
                txt.ID = "Mail";
                txt.Text = Core.CurrentUser.Mail;
                txt.Enabled = !Core.CurrentUser.Aktif;
                txt.Visible = !Core.CurrentUser.Aktif;
                txt.CssClass = "form-control";
                txt.TextMode = TextBoxMode.Email;
                txt.MaxLength = 60;
                CustomizeControl1.AddControl("Mail", txt);

                txt = new TextBox();
                txt.ID = "Icerik";
                txt.CssClass = "form-control";
                txt.MaxLength = 500;
                txt.TextMode = TextBoxMode.MultiLine;
                CustomizeControl1.AddControl("Mesajınız", txt);

                CustomizeControl1.SubmitClick += new CustomizeControl.ButtonEvent(CustomizeControl1_SubmitClick);
            }
            base.OnInit(e);
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            siteComments.Visible = this.Visible;
            if (!Page.IsPostBack & this.Visible)
            {
                //writeBox.Style.Value = "display: none";
                GetDataPaging();
            }
            //else
            //    writeBox.Style.Value = "display: block";
            CustomizeControl1.IsValidated = true;
        }

        void CustomizeControl1_SubmitClick(SortedDictionary<string, Control> controls)
        {
            if (((TextBox)controls["Icerik"]).Text.Length > 500)
            {
                CustomizeControl1.MessageText = MessageBox.Show(DialogResult.Warning, string.Format("Yazmış olduğunuz yorum uzunluğu <b>{0}</b> karakterdir. Yorum alanına en fazla <b>500</b> karakter girebilirsiniz lütfen yazınızı kontrol ederek tekrar deneyiniz.", ((TextBox)controls["Icerik"]).Text.Length));
                return;
            }
            if (!string.IsNullOrEmpty(((TextBox)controls["Adi"]).Text)
                & !string.IsNullOrEmpty(((TextBox)controls["Mail"]).Text)
                & !string.IsNullOrEmpty(((TextBox)controls["Icerik"]).Text)
                & !string.IsNullOrEmpty(ModulID)
                & !string.IsNullOrEmpty(IcerikID)
                & CustomizeControl1.PanelVisible
                & CustomizeControl1.SubmitEnabled)
                using (Yorum m = new Yorum())
                {
                    m.IP = Context.Request.ServerVariables["REMOTE_ADDR"].ToString();
                    m.Adi = BAYMYO.UI.Commons.SubStringText(((TextBox)controls["Adi"]).Text, 100);
                    m.Mail = ((TextBox)controls["Mail"]).Text;
                    m.ModulID = ModulID;
                    m.IcerikID = IcerikID;
                    m.Icerik = BAYMYO.UI.Commons.SubStringText(((TextBox)controls["Icerik"]).Text, 500);
                    m.KayitTarihi = DateTime.Now;
                    m.YoneticiOnay = IsCommandActive;
                    m.Aktif = IsCommandActive;
                    if (YorumMethods.Insert(m) > 0)
                    {
                        CustomizeControl1.MessageText = MessageBox.Show(DialogResult.Succes, "Yorumunuz başarılı bir şekilde tarafımıza gönderilmiştir, kontrol edildikten sonra yayımlanacaktır.");
                        CustomizeControl1.PanelVisible = false;
                        CustomizeControl1.SubmitEnabled = false;
                        ((TextBox)controls["Adi"]).Text = string.Empty;
                        ((TextBox)controls["Mail"]).Text = string.Empty;
                        ((TextBox)controls["Icerik"]).Text = string.Empty;
                    }
                }
            else
                CustomizeControl1.MessageText = MessageBox.Show(DialogResult.Stop, "Lütfen aşağıdaki ilgili kutucukların dolu olduğundan yada geçerli değerlere sahip olduğundan emin olunuz.");
        }

        protected void rptComments_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            if (this.IsCommandActive)
            {
                switch (e.CommandName)
                {
                    case "aktif":
                        using (HiddenField hv = rptComments.Items[e.Item.ItemIndex].FindControl("hfID") as HiddenField)
                        {
                            if (hv != null)
                                using (Yorum m = YorumMethods.GetYorum(BAYMYO.UI.Converts.NullToGuidString(hv.Value)))
                                {
                                    if (!string.IsNullOrEmpty(m.ID))
                                    {
                                        m.Aktif = true;
                                        YorumMethods.Update(m);
                                        GetDataPaging();
                                        CustomizeControl1.MessageText = MessageBox.Show(DialogResult.Succes, "Yorum başarılı bir şekilde aktif edildi!");
                                    }
                                }
                        }
                        break;
                    case "pasif":
                        using (HiddenField hv = rptComments.Items[e.Item.ItemIndex].FindControl("hfID") as HiddenField)
                        {
                            if (hv != null)
                                using (Yorum m = YorumMethods.GetYorum(BAYMYO.UI.Converts.NullToGuidString(hv.Value)))
                                {
                                    if (!string.IsNullOrEmpty(m.ID))
                                    {
                                        m.Aktif = false;
                                        YorumMethods.Update(m);
                                        GetDataPaging();
                                        CustomizeControl1.MessageText = MessageBox.Show(DialogResult.Succes, "Yorum başarılı bir şekilde pasif edildi!");
                                    }
                                }
                        }
                        break;
                    case "remove":
                        using (HiddenField hv = rptComments.Items[e.Item.ItemIndex].FindControl("hfID") as HiddenField)
                        {
                            if (hv != null)
                                using (Yorum m = YorumMethods.GetYorum(BAYMYO.UI.Converts.NullToGuidString(hv.Value)))
                                {
                                    if (!string.IsNullOrEmpty(m.ID))
                                    {
                                        YorumMethods.Delete(m);
                                        GetDataPaging();
                                        CustomizeControl1.MessageText = MessageBox.Show(DialogResult.Stop, "Yorum başarılı bir şekilde pasif edildi!");
                                    }
                                }
                        }
                        break;
                }
            }
        }
    }
}