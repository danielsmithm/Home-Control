using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeControl.Data.Dal.Dao.Custom.Implementations.AdoNet
{
    public class AbstractAdoNetDao
    {

        public static SqlCommand createCommand(SqlConnection conection, String sqlCommand)
        {
            SqlCommand comand = new SqlCommand();
            
            comand.CommandText = sqlCommand;
            comand.CommandType = CommandType.Text;
            comand.Connection = conection;

            return comand;
        }

        public static void closeResources(SqlConnection conection, SqlDataReader reader)
        {
            if (reader != null)
            {
                reader.Close();
            }
            
            if (conection != null)
            {
                conection.Close();
            }
        }


    }
}
