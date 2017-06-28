using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeControl.Data.Dal.Dao.Custom.Implementations.AdoNet
{
    public class ConnectionFactory
    {

        public static string getConnectionString()
        {
            //TODO: Colocar em arquivo de configuração.
            string ConnectionString = "Data Source=DIOGO-PC\\SQLEXPRESS2014; ";
            ConnectionString += "User ID=homecontrol; ";
            ConnectionString += "Password=123456; ";
            ConnectionString += "Initial Catalog=HomeControl";
            return ConnectionString;
        }

        public static SqlConnection getConnection()
        {
            return new SqlConnection(getConnectionString());
        }

    }
}
