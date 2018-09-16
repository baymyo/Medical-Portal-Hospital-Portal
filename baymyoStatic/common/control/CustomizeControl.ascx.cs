using System;
using System.Collections.Generic;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace baymyoStatic
{
    public partial class CustomizeControl : System.Web.UI.UserControl
    {
        #region --- Properties ---
        public delegate void ButtonEvent(SortedDictionary<string, Control> controls);
        private const string FormControlPath = "\\common\\control\\";

        public bool IsValidated { get { return BAYMYO.UI.Converts.NullToBool(ViewState["IsValidated"]); } set { ViewState["IsValidated"] = value; } }

        public string FormTitle { get; set; } = string.Empty;
        public bool FormTitleVisible { get; set; } = false;
        private bool formScriptEnable = true;
        public bool FormScriptEnable
        {
            get { return formScriptEnable; }
            set { formScriptEnable = value; }
        }
        #endregion

        #region --- Panel Set ---
        public bool PanelVisible
        {
            get { return CustomControls.Visible; }
            set { CustomControls.Visible = value; }
        }
        public bool PanelEnabled
        {
            get { return CustomControls.Enabled; }
            set { CustomControls.Enabled = value; }
        }
        #endregion

        #region --- Message Button Set ---
        public string MessageText
        {
            get { return CustomMessage.Text; }
            set { CustomMessage.Text = value; }
        }
        public bool MessageVisible
        {
            get { return CustomMessage.Visible; }
            set { CustomMessage.Visible = value; }
        }
        #endregion

        #region --- Status Button Set ---
        public string StatusText
        {
            get { return CustomStatus.Text; }
            set { CustomStatus.Text = value; }
        }
        public string StatusToolTip
        {
            get { return CustomStatus.ToolTip; }
            set { CustomStatus.ToolTip = value; }
        }
        public bool StatusEnabled
        {
            get { return CustomStatus.Enabled; }
            set { CustomStatus.Enabled = value; }
        }
        public bool StatusVisible
        {
            get { return CustomStatus.Visible; }
            set { CustomStatus.Visible = value; }
        }
        #endregion

        #region --- Submit Button Set ---
        public string SubmitToolTip
        {
            get { return CustomSubmit.ToolTip; }
            set { CustomSubmit.ToolTip = value; }
        }
        public string SubmitOnClientClick
        {
            get { return CustomSubmit.OnClientClick; }
            set { CustomSubmit.OnClientClick = value; }
        }
        public string SubmitCssClass
        {
            get { return CustomSubmit.CssClass; }
            set { CustomSubmit.CssClass = value; }
        }
        public string SubmitText
        {
            get { return CustomSubmit.Text; }
            set { CustomSubmit.Text = value; }
        }
        public bool SubmitEnabled
        {
            get { return CustomSubmit.Enabled; }
            set { CustomSubmit.Enabled = value; }
        }
        public bool SubmitVisible
        {
            get { return CustomSubmit.Visible; }
            set { CustomSubmit.Visible = value; }
        }
        public Unit SubmitWidth
        {
            get { return CustomSubmit.Width; }
            set { CustomSubmit.Width = value; }
        }
        public Unit SubmitHeight
        {
            get { return CustomSubmit.Height; }
            set { CustomSubmit.Height = value; }
        }

        public event ButtonEvent SubmitClick;
        #endregion

        #region --- Update Button Set ---
        public string UpdateToolTip
        {
            get { return CustomUpdate.ToolTip; }
            set { CustomUpdate.ToolTip = value; }
        }
        public string UpdateOnClientClick
        {
            get { return CustomUpdate.OnClientClick; }
            set { CustomUpdate.OnClientClick = value; }
        }
        public string UpdateCssClass
        {
            get { return CustomUpdate.CssClass; }
            set { CustomUpdate.CssClass = value; }
        }
        public string UpdateText
        {
            get { return CustomUpdate.Text; }
            set { CustomUpdate.Text = value; }
        }
        public bool UpdateEnabled
        {
            get { return CustomUpdate.Enabled; }
            set { CustomUpdate.Enabled = value; }
        }
        public bool UpdateVisible
        {
            get { return CustomUpdate.Visible; }
            set { CustomUpdate.Visible = value; }
        }
        public Unit UpdateWidth
        {
            get { return CustomUpdate.Width; }
            set { CustomUpdate.Width = value; }
        }
        public Unit UpdateHeight
        {
            get { return CustomUpdate.Height; }
            set { CustomUpdate.Height = value; }
        }

        public event ButtonEvent UpdateClick;
        #endregion

        #region --- Remove Button Set ---
        public string RemoveToolTip
        {
            get { return CustomRemove.ToolTip; }
            set { CustomRemove.ToolTip = value; }
        }
        public string RemoveOnClientClick
        {
            get { return CustomRemove.OnClientClick; }
            set { CustomRemove.OnClientClick = value; }
        }
        public string RemoveCssClass
        {
            get { return CustomRemove.CssClass; }
            set { CustomRemove.CssClass = value; }
        }
        public string RemoveText
        {
            get { return CustomRemove.Text; }
            set { CustomRemove.Text = value; }
        }
        public bool RemoveEnabled
        {
            get { return CustomRemove.Enabled; }
            set { CustomRemove.Enabled = value; }
        }
        public bool RemoveVisible
        {
            get { return CustomRemove.Visible; }
            set { CustomRemove.Visible = value; }
        }
        public Unit RemoveWidth
        {
            get { return CustomRemove.Width; }
            set { CustomRemove.Width = value; }
        }
        public Unit RemoveHeight
        {
            get { return CustomRemove.Height; }
            set { CustomRemove.Height = value; }
        }

        public event ButtonEvent RemoveClick;
        #endregion

        #region --- Custom Control Add ---
        SortedDictionary<string, Control> m_CustomControls = new SortedDictionary<string, Control>();
        /// <summary>
        /// İçerisindeki tüm kontrollerin listesini dönderir.
        /// </summary>
        public SortedDictionary<string, Control> ControlList
        {
            get { return this.m_CustomControls; }
        }
        /// <summary>
        /// H5 türünde ve "navbar-default, titles" cssleri eklenen bir tag oluşturur.
        /// </summary>
        /// <param name="title">Türündeki başlık içeriğini alır.</param>
        public void AddTitle(string title)
        {
            Literal ltr = new Literal();
            ltr.Text = "<div class=\"page-header\"><h4>" + title + "</h4></div>";
            this.CustomPanel.Controls.Add(ltr);
        }
        /// <summary>
        /// Literal kontrol eklemek için hazırlanmıştır.
        /// </summary>
        /// <param name="control">System.Web.UI.WebControls.Literal nesnesi alır.</param>
        public void AddTitle(Literal control)
        {
            this.CustomPanel.Controls.Add(control);
        }
        /// <summary>
        /// Sayfada gösterilecek ve içerisinden değer alınacak kontroller için hazırlanmıştır.
        /// </summary>
        /// <param name="title">Kullanıcı tarafından görüntülenen başlık bilgisi.</param>
        /// <param name="control">Control türünde herhangi bir nesne alır.</param>
        public void AddControl(string title, Control control)
        {
            this.m_CustomControls.Add(control.ID, control);
            switch (control.Visible)
            {
                case true:
                    UserControl pnlControl = (UserControl)this.Page.LoadControl(FormControlPath + "CustomizeControlShema.ascx");
                    ((Literal)pnlControl.FindControl("ltrTitle")).Text = title;
                    ((Panel)pnlControl.FindControl("pnlControl")).Controls.Add(control);
                    this.CustomPanel.Controls.Add(pnlControl);
                    break;
            }
        }
        /// <summary>
        /// Sayfada gösterilecek ve içerisinden değer alınacak kontroller için hazırlanmıştır.
        /// </summary>
        /// <param name="title">Kullanıcı tarafından görüntülenen başlık bilgisi.</param>
        /// <param name="control">Control türünde herhangi bir nesne alır.</param>
        /// <param name="extender">Extender için gerekli kontrolü alır.</param>
        public void AddControl(string title, Control control, Control extender)
        {
            this.m_CustomControls.Add(control.ID, control);
            switch (control.Visible)
            {
                case true:
                    UserControl pnlControl = (UserControl)this.Page.LoadControl(FormControlPath + "CustomizeControlShema.ascx");
                    ((Literal)pnlControl.FindControl("ltrTitle")).Text = title;
                    Panel pnl = ((Panel)pnlControl.FindControl("pnlControl"));
                    pnl.Controls.Add(control);
                    pnl.Controls.Add(extender);
                    this.CustomPanel.Controls.Add(pnlControl);
                    break;
            }
        }
        /// <summary>
        /// Sayfada gösterilecek ve içerisinden değer alınacak kontroller için hazırlanmıştır.
        /// </summary>
        /// <param name="title">Kullanıcı tarafından görüntülenen başlık bilgisi.</param>
        /// <param name="control">Control türünde herhangi bir nesne alır.</param>
        /// <param name="extenders">Control[] Extender için gerekli kontrol listesini alır.</param>
        public void AddControl(string title, Control control, Control[] extenders)
        {
            this.m_CustomControls.Add(control.ID, control);
            switch (control.Visible)
            {
                case true:
                    UserControl pnlControl = (UserControl)this.Page.LoadControl(FormControlPath + "CustomizeControlShema.ascx");
                    ((Literal)pnlControl.FindControl("ltrTitle")).Text = title;
                    Panel pnl = ((Panel)pnlControl.FindControl("pnlControl"));
                    pnl.Controls.Add(control);
                    foreach (Control item in extenders)
                        pnl.Controls.Add(item);
                    this.CustomPanel.Controls.Add(pnlControl);
                    break;
            }
        }
        /// <summary>
        /// Sayfada gösterilecek ve içerisinden değer alınacak kontroller için hazırlanmıştır.
        /// </summary>
        /// <param name="title">Kullanıcı tarafından görüntülenen başlık bilgisi.</param>
        /// <param name="control">Control türünde herhangi bir nesne alır.</param>
        /// <param name="description">Control için gerekli açıklamalar.</param>
        public void AddControl(string title, Control control, string description)
        {
            this.m_CustomControls.Add(control.ID, control);
            switch (control.Visible)
            {
                case true:
                    UserControl pnlControl = (UserControl)this.Page.LoadControl(FormControlPath + "CustomizeControlShema.ascx");
                    ((Literal)pnlControl.FindControl("ltrTitle")).Text = title;
                    ((Panel)pnlControl.FindControl("pnlControl")).Controls.Add(control);
                    if (!string.IsNullOrEmpty(description))
                        ((Literal)pnlControl.FindControl("ltrExample")).Text = "<var class=\"description\">" + description + "</var>";
                    this.CustomPanel.Controls.Add(pnlControl);
                    break;
            }
        }
        /// <summary>
        /// Sayfada gösterilecek ve içerisinden değer alınacak kontroller için hazırlanmıştır.
        /// </summary>
        /// <param name="title">Kullanıcı tarafından görüntülenen başlık bilgisi.</param>
        /// <param name="control">Control türünde herhangi bir nesne alır.</param>
        /// <param name="extender">Extender için gerekli kontrolü alır.</param>
        /// <param name="description">Control için gerekli açıklamalar.</param>
        public void AddControl(string title, Control control, Control extender, string description)
        {
            m_CustomControls.Add(control.ID, control);
            switch (control.Visible)
            {
                case true:
                    UserControl pnlControl = (UserControl)this.Page.LoadControl(FormControlPath + "CustomizeControlShema.ascx");
                    ((Literal)pnlControl.FindControl("ltrTitle")).Text = title;
                    Panel pnl = ((Panel)pnlControl.FindControl("pnlControl"));
                    pnl.Controls.Add(control);
                    pnl.Controls.Add(extender);
                    if (!string.IsNullOrEmpty(description))
                        ((Literal)pnlControl.FindControl("ltrExample")).Text = "<var class=\"description\">" + description + "</var>";
                    this.CustomPanel.Controls.Add(pnlControl);
                    break;
            }
        }
        /// <summary>
        /// Sayfada gösterilecek ve içerisinden değer alınacak kontroller için hazırlanmıştır.
        /// </summary>
        /// <param name="title">Kullanıcı tarafından görüntülenen başlık bilgisi.</param>
        /// <param name="control">Control türünde herhangi bir nesne alır.</param>
        /// <param name="extenders">Control[] Extender için gerekli kontrol listesini alır.</param>
        /// <param name="description">Control için gerekli açıklamalar.</param>
        public void AddControl(string title, Control control, Control[] extenders, string description)
        {
            this.m_CustomControls.Add(control.ID, control);
            switch (control.Visible)
            {
                case true:
                    UserControl pnlControl = (UserControl)this.Page.LoadControl(FormControlPath + "CustomizeControlShema.ascx");
                    ((Literal)pnlControl.FindControl("ltrTitle")).Text = title;
                    Panel pnl = ((Panel)pnlControl.FindControl("pnlControl"));
                    pnl.Controls.Add(control);
                    foreach (Control item in extenders)
                        pnl.Controls.Add(item);
                    if (!string.IsNullOrEmpty(description))
                        ((Literal)pnlControl.FindControl("ltrExample")).Text = "<var class=\"description\">" + description + "</var>";
                    this.CustomPanel.Controls.Add(pnlControl);
                    break;
            }
        }
        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            if (FormScriptEnable)
            {
                CustomSubmit.CssClass = "btn btn-default submitButton";
                CustomUpdate.CssClass = "btn btn-success submitButton";
                CustomRemove.CssClass = "btn btn-danger submitButton";
            }
            if (!this.Page.IsPostBack & IsValidated)
            {
                Session["securityCode"] = true;
                ValidateImage.ImageUrl = "/common/service/SecurityImage.ashx?g=" + Guid.NewGuid();
                ValidateCode.Attributes.Add("placeholder", "Kod");
            }
            ValidateImage.Visible = IsValidated;
            ValidateCode.Visible = IsValidated;
        }

        bool Validated()
        {
            if (IsValidated)
            {
                try
                {
                    return Session["securityCode"].Equals(Core.Compute(ValidateCode.Text.ToUpper()));
                }
                catch (Exception)
                {
                    return false;
                }
                finally
                {
                    Session.Remove("securityCode");
                    Session["securityCode"] = true;
                    ValidateImage.ImageUrl = "/common/service/SecurityImage.ashx?g=" + Guid.NewGuid();
                    ValidateCode.Text = string.Empty;
                }
            }
            else
                return true;
        }

        string securityMessage = "Resim içerisinde gösterilen <b>5</b> karakterlik yazı ile <b>KOD</b> girdiğiniz karakterler uyuşmamaktadır.";
        /// <summary>
        /// Kayıt ekleme işlemlerinizde kullanabileceğiniz event.
        /// </summary>
        /// <param name="sender">object verir.</param>
        /// <param name="e">event verir.</param>
        protected void CustomSubmit_Click(object sender, EventArgs e)
        {
            if (SubmitClick != null & Validated())
                SubmitClick(this.m_CustomControls);
            else if (IsValidated)
                MessageText = MessageBox.Show(DialogResult.Warning, securityMessage);
        }
        /// <summary>
        /// Güncelleme işlemlerinizde kullanabileceğiniz event.
        /// </summary>
        /// <param name="sender">object verir.</param>
        /// <param name="e">event verir.</param>
        protected void CustomUpdate_Click(object sender, EventArgs e)
        {
            if (UpdateClick != null & Validated())
                UpdateClick(this.m_CustomControls);
            else if (IsValidated)
                MessageText = MessageBox.Show(DialogResult.Warning, securityMessage);
        }
        /// <summary>
        /// Silme işlemlerinizde kullanabileceğiniz event.
        /// </summary>
        /// <param name="sender">object verir.</param>
        /// <param name="e">event verir.</param>
        protected void CustomRemove_Click(object sender, EventArgs e)
        {
            if (RemoveClick != null & Validated())
                RemoveClick(this.m_CustomControls);
            else if (IsValidated)
                MessageText = MessageBox.Show(DialogResult.Warning, securityMessage);
        }
    }
}