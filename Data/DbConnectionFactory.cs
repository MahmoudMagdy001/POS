using System.Configuration;
using System.Data.SqlClient;

namespace POS.Data
{
    public static class DbConnectionFactory
    {
        private static readonly string DefaultMasterConnectionString =
            @"Server=(localdb)\MSSQLLocalDB;Database=master;Integrated Security=True;TrustServerCertificate=True;";

        private static readonly string DefaultAppConnectionString =
            @"Server=(localdb)\MSSQLLocalDB;Database=POS_DB;Integrated Security=True;TrustServerCertificate=True;";

        public static string GetConnectionString()
        {
            var configConn = ConfigurationManager.ConnectionStrings["POS_DB"];
            if (configConn != null && !string.IsNullOrWhiteSpace(configConn.ConnectionString))
            {
                return configConn.ConnectionString;
            }
            return DefaultAppConnectionString;
        }

        public static string GetMasterConnectionString()
        {
            return DefaultMasterConnectionString;
        }

        public static SqlConnection CreateConnection()
        {
            return new SqlConnection(GetConnectionString());
        }

        public static SqlConnection CreateMasterConnection()
        {
            return new SqlConnection(GetMasterConnectionString());
        }
    }
}
