using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace SqlDemo.Data.Repositories
{
    public class BaseRepository
    {
        protected SqlConnection SqlConnection;

        public BaseRepository()
        {
            var connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ToString();
            SqlConnection = new SqlConnection(connectionString);
        }

        protected void OpenSqlConnection()
        {
            if (SqlConnection.State == ConnectionState.Closed)
            {
                SqlConnection.Open();
            }
        }
    }
}