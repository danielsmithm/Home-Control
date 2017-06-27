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

                SqlCommand comand = createCommand(conection, "INSERT INTO EMBARCADOES( nome, ipAddress, macAddress ) VALUES( @nome, @ipAddress, @macAddress ); SELECT CAST(scope_identity() AS int)");

                // Define as informações do parâmetro criado
                SqlParameter param = new SqlParameter("@nome", embarcado.Nome);
                comand.Parameters.Add(param);
                SqlParameter param1 = new SqlParameter("@ipAddress", embarcado.Socket);
                comand.Parameters.Add(param1);
                SqlParameter param2 = new SqlParameter("@macAddress", embarcado.MacAddress);
                comand.Parameters.Add(param2);

                // TODO: Verificar se o resultado retornado não é nulo para poder converter.
                embarcado.Id = Convert.ToInt32(comand.ExecuteNonQuery());

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

                SqlCommand comand = createCommand(conection, "SELECT (  id,nome, ipAddress, macAddress ) from EMBARCADOES where id = @id");

                // Define as informações de parâmetro
                SqlParameter param = new SqlParameter("@id", id);
                comand.Parameters.Add(param);

                // Executando o commando e obtendo o resultado
                reader = comand.ExecuteReader();

                return readEmbarcado(reader);

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
                
                SqlCommand comand = createCommand(conection, "SELECT (  id, nome, ipAddress, macAddress ) from EMBARCADOES");

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

                SqlCommand comand = createCommand(conection, "DELETE from EMBARCADOES where id = @id");

                // Define as informações do parâmetro criado
                SqlParameter param = new SqlParameter("@id", embarcado.Id);

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

                SqlCommand comand = createCommand(conection, "UPDATE EMBARCADOES Set nome = @nome, ipAddress = @ipAddress, macAddress = @macAddress WHERE id = @id");

                // Define as informações do parâmetro criado
                SqlParameter param = new SqlParameter("@id", embarcado.Id);
                comand.Parameters.Add(param);
                SqlParameter param1 = new SqlParameter("@nome", embarcado.Nome);
                comand.Parameters.Add(param1);
                SqlParameter param2 = new SqlParameter("@ipAddress", embarcado.Socket);
                comand.Parameters.Add(param2);
                SqlParameter param3 = new SqlParameter("@macAddress", embarcado.MacAddress);
                comand.Parameters.Add(param3);

                comand.ExecuteNonQuery();

                return embarcado;

            }
            finally
            {
                closeResources(conection, null);
            }

        }

        private Embarcado readEmbarcado(SqlDataReader reader)
        {
            Embarcado embarcado = null;

            if (reader != null)
            {

                if (reader.HasRows)
                {
                    embarcado = new Embarcado();

                    embarcado.Id = Convert.ToInt32(reader["Id"]);
                    embarcado.Nome = (String)reader["nome"];
                    embarcado.Socket = (String)reader["ipAddress"];
                    embarcado.MacAddress = (String)reader["macAddress"];
                }

            }

            return embarcado;
        }

        private List<Embarcado> readAllEmbarcado(SqlDataReader reader)
        {

            List<Embarcado> embarcados = new List<Embarcado>();

            if (reader != null)
            {

                while (reader.NextResult())
                {
                    embarcados.Add(readEmbarcado(reader));
                }

            }

            return embarcados;

        }
    }
}
