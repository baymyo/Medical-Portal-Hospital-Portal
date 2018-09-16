using System;
using System.Collections.Generic;
using System.Web;
using BAYMYO.MultiSQLClient;

namespace baymyoStatic
{
    /// <summary>
    /// Summary description for Database
    /// </summary>
    public class Database
    {
        string m_Table, m_Op, m_Msg_Type, m_Msg_Text;

        public string Table
        {
            get { return m_Table; }
            set { m_Table = value; }
        }

        public string Op
        {
            get { return m_Op; }
            set { m_Op = value; }
        }

        public string Msg_Type
        {
            get { return m_Msg_Type; }
            set { m_Msg_Type = value; }
        }

        public string Msg_Text
        {
            get { return m_Msg_Text; }
            set { m_Msg_Text = value; }
        }

        public Database(string table, string op, string msg_type, string msg_text)
        {
            this.m_Table = table;
            this.m_Op = op;
            this.m_Msg_Type = msg_type;
            this.m_Msg_Text = msg_text;
        }

        public static List<Database> ProcRun(DatabaseProccesType procType)
        {
            List<Database> rv = new List<Database>();
            string commandText = null;
            switch (procType)
            {
                case DatabaseProccesType.Analyze:
                    commandText = "analyze tables ";
                    break;
                case DatabaseProccesType.Check:
                    commandText = "check tables ";
                    break;
                case DatabaseProccesType.Repair:
                    commandText = "repair tables ";
                    break;
                default:
                    commandText = "optimize tables ";
                    break;
            }
            using (System.Data.DataTable dt = new System.Data.DataTable("tables"))
            {
                using (BAYMYO.UI.Web.CustomSqlQuery q = new BAYMYO.UI.Web.CustomSqlQuery(dt, "show tables"))
                {
                    q.Execute();
                    int count = dt.Rows.Count - 2;
                    for (int i = 0; i < count; i++)
                        commandText += MConvert.NullToString(dt.Rows[i][0]) + ',';
                    if (dt.Rows.Count > 0)
                        commandText += MConvert.NullToString(dt.Rows[count + 1][0]);
                    count = 0;
                }
            }

            using (MConnection conneciton = new MConnection(MClientProvider.MySQL))
            {
                switch (conneciton.State)
                {
                    case System.Data.ConnectionState.Closed:
                        conneciton.Open();
                        break;
                }
                using (MCommand cmd = new MCommand(System.Data.CommandType.Text, commandText, conneciton))
                {
                    using (System.Data.IDataReader IDR = cmd.ExecuteReader())
                    {
                        while (IDR.Read())
                            rv.Add(new Database(MConvert.NullToString(IDR[0]), MConvert.NullToString(IDR[1]), MConvert.NullToString(IDR[2]), MConvert.NullToString(IDR[3])));
                        IDR.Close();
                    }
                }
                switch (conneciton.State)
                {
                    case System.Data.ConnectionState.Open:
                        conneciton.Close();
                        break;
                }
            }
            return rv;
        }
    }
}