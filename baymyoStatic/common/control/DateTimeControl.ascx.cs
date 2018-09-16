using System;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace baymyoStatic
{
    public enum FormatTypes
    {
        None,
        Date,
        DateTime,
        BirthDate
    }

    public partial class DateTimeControl : System.Web.UI.UserControl
    {

        public FormatTypes FormatType { get { return (FormatTypes)ViewState["FormatTypes"]; } set { ViewState["FormatTypes"] = value; } }

        DateTime m_Date;
        public DateTime Date
        {
            get
            {
                switch (FormatType)
                {
                    case FormatTypes.DateTime:
                        return Convert.ToDateTime(string.Format("{0}/{1}/{2} {3}:{4}:00", ddlGun.SelectedValue, ddlAy.SelectedValue, ddlYil.SelectedValue, ddlSaat.SelectedValue, ddlDakika.SelectedValue));
                    case FormatTypes.Date:
                    case FormatTypes.BirthDate:
                        return Convert.ToDateTime(string.Format("{0}/{1}/{2} 00:00:00", ddlGun.SelectedValue, ddlAy.SelectedValue, ddlYil.SelectedValue));
                    default:
                        return DateTime.Now;
                }
            }
            set
            {
                m_Date = value;
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                switch (FormatType)
                {
                    case FormatTypes.DateTime:
                        int years = DateTime.Now.Year + 2;
                        for (int i = 1; i <= 31; i++)
                            ddlGun.Items.Add(new ListItem(i.ToString("0#"), i.ToString("0#")));
                        for (int i = 1; i <= 12; i++)
                            ddlAy.Items.Add(new ListItem(i.ToString("0#"), i.ToString("0#")));
                        for (int i = 2005; i <= years; i++)
                            ddlYil.Items.Add(new ListItem(i.ToString("0000"), i.ToString("0000")));
                        for (int i = 1; i <= 23; i++)
                            ddlSaat.Items.Add(new ListItem(i.ToString("0#"), i.ToString("0#")));
                        ddlSaat.Items.Add(new ListItem("24", "00"));
                        for (int i = 0; i <= 59; i++)
                            ddlDakika.Items.Add(new ListItem(i.ToString("0#"), i.ToString("0#")));
                        pnlTime.Visible = true;
                        SetDateTime(2005);
                        break;
                    case FormatTypes.Date:
                        for (int i = 1; i <= 31; i++)
                            ddlGun.Items.Add(new ListItem(i.ToString("0#"), i.ToString("0#")));
                        for (int i = 1; i <= 12; i++)
                            ddlAy.Items.Add(new ListItem(i.ToString("0#"), i.ToString("0#")));
                        for (int i = DateTime.Now.Year; i >= 2000; i--)
                            ddlYil.Items.Add(new ListItem(i.ToString("0000"), i.ToString("0000")));
                        pnlTime.Visible = false;
                        SetDate(DateTime.Now.Year);
                        break;
                    case FormatTypes.BirthDate:
                        for (int i = 1; i <= 31; i++)
                            ddlGun.Items.Add(new ListItem(i.ToString("0#"), i.ToString("0#")));
                        for (int i = 1; i <= 12; i++)
                            ddlAy.Items.Add(new ListItem(i.ToString("0#"), i.ToString("0#")));
                        for (int i = 1923; i <= DateTime.Now.Year; i++)
                            ddlYil.Items.Add(new ListItem(i.ToString("0000"), i.ToString("0000")));
                        pnlTime.Visible = false;
                        SetDate(1923);
                        break;
                }
            }
        }

        void SetDate(int year)
        {
            if (m_Date.Year > year)
            {
                ddlGun.SelectedValue = m_Date.Day.ToString("00");
                ddlAy.SelectedValue = m_Date.Month.ToString("00");
                ddlYil.SelectedValue = m_Date.Year.ToString("0000");
            }
            else
            {
                ddlGun.Items.Insert(0, new ListItem("Gün", "00"));
                ddlAy.Items.Insert(0, new ListItem("Ay", "00"));
                ddlYil.Items.Insert(0, new ListItem("Yıl", "0000"));
            }
        }
        void SetDateTime(int year)
        {
            if (m_Date.Year > year)
            {
                ddlGun.SelectedValue = m_Date.Day.ToString("00");
                ddlAy.SelectedValue = m_Date.Month.ToString("00");
                ddlYil.SelectedValue = m_Date.Year.ToString("0000");

                ddlSaat.SelectedValue = m_Date.Hour.ToString("00");
                ddlDakika.SelectedValue = m_Date.Minute.ToString("00");
            }
        }
    }
}