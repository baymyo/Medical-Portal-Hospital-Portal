using System;

namespace baymyoStatic.panel.ascx
{
    public partial class bakimliste : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.Page.IsPostBack)
                Core.GetProccesList("bakim", ddlIslemler);
        }

        protected void btnSaveChanges_Click(object sender, EventArgs e)
        {
            try
            {
                DatabaseProccesType tipi;
                switch (ddlIslemler.SelectedIndex)
                {
                    case 2: tipi = DatabaseProccesType.Analyze; break;
                    case 3: tipi = DatabaseProccesType.Check; break;
                    case 4: tipi = DatabaseProccesType.Repair; break;
                    default: tipi = DatabaseProccesType.Optimize; break;
                }
                dataGrid1.DataSource = Database.ProcRun(tipi);
                dataGrid1.DataBind();
                infoLiteral.Text = MessageBox.Show(DialogResult.Succes, string.Format("<b>'{0}'</b> işleminiz başarılı bir şekilde gerçekleştirildi!", tipi));
            }
            catch (Exception ex)
            {
                infoLiteral.Text = MessageBox.Show(DialogResult.Error, ex.Message);
            }
        }
    }
}