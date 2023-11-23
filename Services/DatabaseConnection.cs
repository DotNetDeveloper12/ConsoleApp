using System.Data;
using System.Data.SqlClient;

namespace CrudeOp.Services
{
    public class DatabaseConnection
    {
        private static string ConnectionString = "Server=DESKTOP-P6A6A1S\\SQLEXPRESS;Database=MyDatabase;Integrated Security=True;";

        public static IDbConnection GetConnection()
        {
            return new SqlConnection(ConnectionString);
        }
    }
}
