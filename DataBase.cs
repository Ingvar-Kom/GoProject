using System.Data.SqlClient;

namespace my_project
{
    internal class DataBase
    {
        SqlConnection sglConnection = new(@"Data Source=DESKTOP-NTMKSG2\SQLEXPRESS;Initial Catalog=test;Integrated Security=True");
        public void OpenConnection()
        {
            if (sglConnection.State == System.Data.ConnectionState.Closed)
            {
                sglConnection.Open();
            }
        }
        public void CloseConnection()
        {
            if (sglConnection.State == System.Data.ConnectionState.Open)
            {
                sglConnection.Close();
            }
        }
        public SqlConnection GetConnection()
        {
            return sglConnection;
        }
    }
}
