using System.Data.OleDb;
using System.Data;

namespace AccessC
{
    class ConectThisAccessWorker
    {
        public static string Connectic = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=Words.mdb;";
        OleDbConnection myConnection;
        public void OpenConnectic()
        {
            myConnection = new OleDbConnection(Connectic);
            myConnection.Open();
        }
        public string Select(string query)
        {
            OleDbDataReader result;
            string resultstring = "";
                OleDbCommand command = new OleDbCommand(query, myConnection);
                result = command.ExecuteReader();
                while (result.Read())
                { 
                    for(int i = 0; i < result.FieldCount; i++)
                        resultstring += (result[i].ToString() + " ");
                }
                resultstring = resultstring.Trim();
            return resultstring;
        }
        public void InsertAndDeleteAndUpdate(string query)
        {
            OleDbCommand command = new OleDbCommand(query, myConnection);
            command.ExecuteNonQuery();
        }
        public DataTable FillDGV(string query)
        {
            using (OleDbConnection conn = new OleDbConnection(Connectic))
            {
                OleDbCommand comm = new OleDbCommand(query, conn);
                DataTable table = new DataTable();
                OleDbDataAdapter adapter = new OleDbDataAdapter(comm);
                adapter.Fill(table);
                return table;
            }
        }
        public void CloseConnect()
        {
            myConnection.Close();
        }
    }
}
