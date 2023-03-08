using Dapper;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace SampleAPIs.Helper
{

    public class DapperContext
    {
        private readonly IConfiguration configuration;
        private readonly string ConnectionString;
        public DapperContext(IConfiguration _configuration)
        {
            configuration = _configuration;
            ConnectionString = configuration.GetConnectionString("DefaultConnections");
        }

        public SqlConnection connection => new SqlConnection(ConnectionString);

    }
}
