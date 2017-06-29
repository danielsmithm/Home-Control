using HomeControl.Data.Dal.Dao.Custom.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HomeControl.Domain.Dispositivos;
using System.Data.SqlClient;

namespace HomeControl.Data.Dal.Dao.Custom.Implementations.AdoNet
{
    class EmbarcadoDatoAdoNet : AbstractAdoNetDao, IEmbarcadoDao
    {
        public Embarcado Add(Embarcado embarcado)
        {
            SqlConnection conection = null;

            try
            {
                conection = ConnectionFactory.getConnection();
                conection.Open();

                SqlCommand comand = createCommand(conection, "INSERT INTO CONTROLADOR( Nome, Ip_Address, Mac_Address, IdComodo ) VALUES( @Nome, @Ip_Address, @Mac_Address, @IdComodo ); SELECT CAST(scope_identity() AS int)");

                // Define as informações do parâmetro criado
                SqlParameter param = new SqlParameter("@Nome", embarcado.Nome);
                comand.Parameters.Add(param);
                SqlParameter param1 = new SqlParameter("@Ip_Address", embarcado.Socket);
                comand.Parameters.Add(param1);
                SqlParameter param2 = new SqlParameter("@Mac_Address", embarcado.MacAddress);
                comand.Parameters.Add(param2);
                SqlParameter param3 = new SqlParameter("@IdComodo", embarcado.ComodoId);
                comand.Parameters.Add(param3);

                // TODO: Verificar se o resultado retornado não é nulo para poder converter.
                embarcado.Id = Convert.ToInt32( comand.ExecuteScalar() );

                return embarcado;
            }
            finally
            {
                closeResources(conection, null);
            }
        }

        public void Dispose()
        {

        }

        public Embarcado Find(int id)
        {
            SqlConnection conection = null;
            SqlDataReader reader = null;

            try
            {
                // instantiate and open connection
                conection = ConnectionFactory.getConnection();
                conection.Open();

                SqlCommand comand = createCommand(conection, "SELECT IdControlador, Nome, Ip_Address, Mac_Address, IdComodo from CONTROLADOR where IdControlador = @IdControlador");

                // Define as informações de parâmetro
                SqlParameter param = new SqlParameter("@IdControlador", id);
                comand.Parameters.Add(param);

                // Executando o commando e obtendo o resultado
                reader = comand.ExecuteReader();

                return readSingleEmbarcado(reader);

            }
            finally
            {
                // Fecha o datareader
                closeResources(conection, reader);
            }
        }

        public List<Embarcado> FindAll()
        {
            SqlConnection conection = null;
            SqlDataReader reader = null;

            try
            {
                // instantiate and open connection
                conection = ConnectionFactory.getConnection();
                conection.Open();

                SqlCommand comand = createCommand(conection, "SELECT IdControlador, Nome, Ip_Address, Mac_Address, IdComodo from CONTROLADOR");

                // Executando o commando e obtendo o resultado
               reader = comand.ExecuteReader();

                return readAllEmbarcado(reader);

            }
            finally
            {
                closeResources(conection, reader);
            }
        }


        public Embarcado Remove(Embarcado embarcado)
        {

            SqlConnection conection = null;

            try
            {
                conection = ConnectionFactory.getConnection();
                conection.Open();

                SqlCommand comand = createCommand(conection, "DELETE from CONTROLADOR where IdControlador = @IdControlador");

                // Define as informações do parâmetro criado
                SqlParameter param = new SqlParameter("@IdControlador", embarcado.Id);

                comand.Parameters.Add(param);

                comand.ExecuteNonQuery();

                return embarcado;
            }
            finally
            {
                closeResources(conection, null);
            }
        }

        public void SaveChanges()
        {
        }

        public Embarcado Update(Embarcado embarcado)
        {
            SqlConnection conection = null;

            try
            {
                conection = ConnectionFactory.getConnection();
                conection.Open();
               
                SqlCommand comand = createCommand(conection, "UPDATE CONTROLADOR Set Nome = @Nome, Ip_Address = @Ip_Address, Mac_Address = @Mac_Address, IdComodo = @IdComodo WHERE IdControlador = @IdControlador");

                // Define as informações do parâmetro criado
                SqlParameter param = new SqlParameter("@IdControlador", embarcado.Id);
                comand.Parameters.Add(param);
                SqlParameter param1 = new SqlParameter("@Nome", embarcado.Nome);
                comand.Parameters.Add(param1);
                SqlParameter param2 = new SqlParameter("@Ip_Address", embarcado.Socket);
                comand.Parameters.Add(param2);
                SqlParameter param3 = new SqlParameter("@Mac_Address", embarcado.MacAddress);
                comand.Parameters.Add(param3);
                SqlParameter param4 = new SqlParameter("@IdComodo", embarcado.ComodoId);
                comand.Parameters.Add(param4);

                comand.ExecuteNonQuery();

                return embarcado;

            }
            finally
            {
                closeResources(conection, null);
            }

        }

        private Embarcado readSingleEmbarcado(SqlDataReader reader)
        {
            Embarcado embarcado = null;

            if (reader.Read())
            {
                embarcado = readEmbarcado(reader);
            }

            return embarcado;
        }

        private Embarcado readEmbarcado(SqlDataReader reader)
        {
            Embarcado embarcado = null;

            if (reader != null)
            {

                if (reader.HasRows)
                {
                    embarcado = new Embarcado();

                    embarcado.Id = Convert.ToInt32(reader["IdControlador"]);
                    embarcado.Nome = (String)reader["Nome"];
                    embarcado.Socket = (String)reader["Ip_Address"];
                    embarcado.MacAddress = (String)reader["Mac_Address"];
                    embarcado.ComodoId = Convert.ToInt32(reader["IdComodo"]);
                }

            }

            return embarcado;
        }

        private List<Embarcado> readAllEmbarcado(SqlDataReader reader)
        {

            List<Embarcado> embarcados = new List<Embarcado>();

            if (reader != null)
            {

                while (reader.Read())
                {
                    embarcados.Add(readEmbarcado(reader));
                }

            }

            return embarcados;

        }
    }
}
