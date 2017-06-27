using HomeControl.Data.Dal.Dao.Custom.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HomeControl.Domain.Sensores;
using System.Data.SqlClient;

namespace HomeControl.Data.Dal.Dao.Custom.Implementations.AdoNet
{
    public class SensorDaoAdoNet : AbstractAdoNetDao, ISensorDao
    {
        public Sensor Add(Sensor sensor)
        {
            SqlConnection conection = null;

            try
            {
                conection = ConnectionFactory.getConnection();
                conection.Open();

                SqlCommand comand = createCommand(conection, "INSERT INTO DISPOSITIVOES( Ativo, Porta, Estado, Discriminator, Comodo_Id, ValorAtual, ValorMaximo, ValorMinimo, EstadoAtual ) VALUES( @Ativo, @Porta, @Estado, @Discriminator, @Comodo_Id, @ValorAtual, @ValorMaximo, @ValorMinimo, @EstadoAtual ); SELECT CAST(scope_identity() AS int)");

                // Define as informações do parâmetro criado
                SqlParameter param = new SqlParameter("@Ativo", sensor.Ativo);
                comand.Parameters.Add(param);
                SqlParameter param1 = new SqlParameter("@Porta", sensor.Porta);
                comand.Parameters.Add(param1);
                SqlParameter param2 = new SqlParameter("@Estado", sensor.Estado);
                comand.Parameters.Add(param2);
                SqlParameter param3 = new SqlParameter("@Discriminator", null); // sensor.Discriminator ??
                comand.Parameters.Add(param3);
                SqlParameter param4 = new SqlParameter("@Comodo_Id", sensor.ComodoId);
                comand.Parameters.Add(param4);
                SqlParameter param5 = new SqlParameter("@ValorAtual", sensor.valorAtual); // ??
                comand.Parameters.Add(param5);
                SqlParameter param6 = new SqlParameter("@ValorMaximo", null); // ??
                comand.Parameters.Add(param6);
                SqlParameter param7 = new SqlParameter("@ValorMinimo", null); // ??
                comand.Parameters.Add(param7);
                SqlParameter param8 = new SqlParameter("@EstadoAtual", null); // ??
                comand.Parameters.Add(param8);

                // TODO: Verificar se o resultado retornado não é nulo para poder converter.
                sensor.Id = Convert.ToInt32(comand.ExecuteNonQuery());

                return sensor;
            }
            finally
            {
                closeResources(conection, null);
            }
        }

        public void Dispose()
        {

        }

        public Sensor Find(int id)
        {
            SqlConnection conection = null;
            SqlDataReader reader = null;

            try
            {
                // instantiate and open connection
                conection = ConnectionFactory.getConnection();
                conection.Open();

                SqlCommand comand = createCommand(conection, "SELECT (  Id, Ativo, Porta, Estado, Comodo_Id, ValorAtual ) from DISPOSITIVOES where Id = @Id");

                // Define as informações de parâmetro
                SqlParameter param = new SqlParameter("@Id", id);
                comand.Parameters.Add(param);

                // Executando o commando e obtendo o resultado
                reader = comand.ExecuteReader();

                return readSensor(reader);

            }
            finally
            {
                // Fecha o datareader
                closeResources(conection, reader);
            }
        }

        public List<Sensor> FindAll()
        {
            SqlConnection conection = null;
            SqlDataReader reader = null;

            try
            {
                // instantiate and open connection
                conection = ConnectionFactory.getConnection();
                conection.Open();

                SqlCommand comand = createCommand(conection, "SELECT (  Id, Ativo, Porta, Estado, Comodo_Id, ValorAtual ) from DISPOSITIVOES");

                // Executando o commando e obtendo o resultado
                reader = comand.ExecuteReader();

                return readAllSensor(reader);

            }
            finally
            {
                closeResources(conection, reader);
            }
        }


        public Sensor Remove(Sensor sensor)
        {

            SqlConnection conection = null;

            try
            {
                conection = ConnectionFactory.getConnection();
                conection.Open();

                SqlCommand comand = createCommand(conection, "DELETE from DISPOSITIVOES where Id = @Id");

                // Define as informações do parâmetro criado
                SqlParameter param = new SqlParameter("@Id", sensor.Id);

                comand.Parameters.Add(param);

                comand.ExecuteNonQuery();

                return sensor;
            }
            finally
            {
                closeResources(conection, null);
            }
        }

        public void SaveChanges()
        {
        }

        public Sensor Update(Sensor sensor)
        {
            SqlConnection conection = null;

            try
            {
                conection = ConnectionFactory.getConnection();
                conection.Open();

                SqlCommand comand = createCommand(conection, "UPDATE DISPOSITIVOES Set Ativo = @Ativo, Porta = @Porta, Estado = @Estado, Discriminator = @Discriminator, Comodo_Id = @Comodo_Id, ValorAtual = @ValorAtual, ValorMaximo = @ValorMaximo, ValorMinimo = @ValorMinimo, EstadoAtual = @EstadoAtual WHERE Id = @Id");

                // Define as informações do parâmetro criado
                SqlParameter param = new SqlParameter("@Id", sensor.Id);
                comand.Parameters.Add(param);
                SqlParameter param1 = new SqlParameter("@Ativo", sensor.Ativo);
                comand.Parameters.Add(param1);
                SqlParameter param2 = new SqlParameter("@Porta", sensor.Porta);
                comand.Parameters.Add(param2);
                SqlParameter param3 = new SqlParameter("@Estado", sensor.Estado);
                comand.Parameters.Add(param3);
                SqlParameter param4 = new SqlParameter("@Discriminator", null); // sensor.Discriminator ??
                comand.Parameters.Add(param4);
                SqlParameter param5 = new SqlParameter("@Comodo_Id", sensor.ComodoId);
                comand.Parameters.Add(param5);
                SqlParameter param6 = new SqlParameter("@ValorAtual", sensor.valorAtual); // ??
                comand.Parameters.Add(param6);
                SqlParameter param7 = new SqlParameter("@ValorMaximo", null); // ??
                comand.Parameters.Add(param7);
                SqlParameter param8 = new SqlParameter("@ValorMinimo", null); // ??
                comand.Parameters.Add(param8);
                SqlParameter param9 = new SqlParameter("@EstadoAtual", null); // ??
                comand.Parameters.Add(param9);

                comand.ExecuteNonQuery();

                return sensor;

            }
            finally
            {
                closeResources(conection, null);
            }

        }

        private Sensor readSensor(SqlDataReader reader)
        {
            Sensor sensor = null;

            if (reader != null)
            {

                if (reader.HasRows)
                {
                    sensor = new Sensor();

                    sensor.Id = Convert.ToInt32(reader["Id"]);
                    sensor.Ativo = (Boolean)reader["Nome"];
                    sensor.Porta = Convert.ToInt32(reader["Porta"]);
                    sensor.Estado = Convert.ToInt32(reader["Estado"]);
                    //sensor.Discriminator = (String)reader["Discriminator"]; // ???
                    sensor.ComodoId = Convert.ToInt32(reader["Comodo_Id"]);
                }

            }

            return sensor;
        }

        private List<Sensor> readAllSensor(SqlDataReader reader)
        {

            List<Sensor> sensors = new List<Sensor>();

            if (reader != null)
            {

                while (reader.NextResult())
                {
                    sensors.Add(readSensor(reader));
                }

            }

            return sensors;

        }
    }
}
