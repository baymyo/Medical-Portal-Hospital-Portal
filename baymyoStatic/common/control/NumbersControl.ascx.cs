using System;

namespace baymyoStatic
{
    public partial class NumbersControl : System.Web.UI.UserControl
    {
        public byte Number1
        {
            get { return BAYMYO.UI.Converts.NullToByte(N1.Text); }
            set { N1.Text = BAYMYO.UI.Converts.NullToString(value); }
        }
        public byte Number2
        {
            get { return BAYMYO.UI.Converts.NullToByte(N2.Text); }
            set { N2.Text = BAYMYO.UI.Converts.NullToString(value); }
        }
        public byte Number3
        {
            get { return BAYMYO.UI.Converts.NullToByte(N3.Text); }
            set { N3.Text = BAYMYO.UI.Converts.NullToString(value); }
        }
        public byte Number4
        {
            get { return BAYMYO.UI.Converts.NullToByte(N4.Text); }
            set { N4.Text = BAYMYO.UI.Converts.NullToString(value); }
        }
        public byte Number5
        {
            get { return BAYMYO.UI.Converts.NullToByte(N5.Text); }
            set { N5.Text = BAYMYO.UI.Converts.NullToString(value); }
        }
        protected void Page_Load(object sender, EventArgs e)
        {

        }
    }
}