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
            string ConnectionString = "Data Source= cnat167741; ";
            ConnectionString += "User ID= sa; ";
            ConnectionString += "Password= senha; ";
            ConnectionString += "Initial Catalog=NorthWind";
            return ConnectionString;
        }

        public static SqlConnection getConnection()
        {
            return new SqlConnection(getConnectionString());
        }

    }
}
