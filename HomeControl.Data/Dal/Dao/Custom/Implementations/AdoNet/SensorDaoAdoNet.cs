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
        public const String DISCRIMINATOR_SENSOR = "SENSOR";

        public Sensor Add(Sensor sensor)
        {
            SqlConnection conection = null;

            try
            {
                conection = ConnectionFactory.getConnection();
                conection.Open();

                SqlCommand comand = createCommand(conection, "INSERT INTO DISPOSITIVO( Ativo, Porta, Nome, ValorMaximo, ValorMinimo, Discriminator, Estado, IdControlador ) VALUES( @Ativo, @Porta, @Nome, @ValorMaximo, @ValorMinimo, @Discriminator, @Estado, @IdControlador ); SELECT CAST(scope_identity() AS int)");

                // Define as informações do parâmetro criado
                SqlParameter param = new SqlParameter("@Ativo", sensor.Ativo);
                comand.Parameters.Add(param);
                SqlParameter param1 = new SqlParameter("@Porta", sensor.Porta);
                comand.Parameters.Add(param1);
                SqlParameter param2 = new SqlParameter("@Nome", ""); // ??;
                comand.Parameters.Add(param2);
                SqlParameter param3 = new SqlParameter("@ValorMaximo", ""); // ??
                comand.Parameters.Add(param3);
                SqlParameter param4 = new SqlParameter("@ValorMinimo", ""); // ??
                comand.Parameters.Add(param4);
                SqlParameter param5 = new SqlParameter("@Discriminator", DISCRIMINATOR_SENSOR);
                comand.Parameters.Add(param5);
                SqlParameter param6 = new SqlParameter("@Estado", sensor.Estado); // ??
                comand.Parameters.Add(param6);
                SqlParameter param7 = new SqlParameter("@IdControlador", sensor.Embarcadoid); // Deveria ser ControladorId
                comand.Parameters.Add(param7);

                // TODO: Verificar se o resultado retornado não é nulo para poder converter.
                sensor.Id = Convert.ToInt32(comand.ExecuteScalar());

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

                SqlCommand comand = createCommand(conection, "SELECT IdDispositivo, Ativo, Porta, Discriminator, Estado, IdControlador from DISPOSITIVO where Discriminator = @Discriminator and IdDispositivo = @IdDispositivo");

                // Define as informações de parâmetro
                SqlParameter param = new SqlParameter("@IdDispositivo", id);
                comand.Parameters.Add(param);
                SqlParameter param1 = new SqlParameter("@Discriminator", DISCRIMINATOR_SENSOR);
                comand.Parameters.Add(param1);

                // Executando o commando e obtendo o resultado
                reader = comand.ExecuteReader();

                return readSingleSensor(reader);

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
                SqlCommand comand = createCommand(conection, "SELECT IdDispositivo, Ativo, Porta, Discriminator, Estado, IdControlador from DISPOSITIVO where Discriminator = @Discriminator");

                // Define as informações de parâmetro
                SqlParameter param = new SqlParameter("@Discriminator", DISCRIMINATOR_SENSOR);
                comand.Parameters.Add(param); ;

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

                SqlCommand comand = createCommand(conection, "DELETE from DISPOSITIVO where IdDispositivo = @IdDispositivo");

                // Define as informações do parâmetro criado
                SqlParameter param = new SqlParameter("@IdDispositivo", sensor.Id);

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

                SqlCommand comand = createCommand(conection, "UPDATE DISPOSITIVO Set Ativo = @Ativo, Porta = @Porta, Nome = @Nome, ValorMaximo = @ValorMaximo, ValorMinimo = @ValorMinimo, Discriminator = @Discriminator, Estado = @Estado, IdControlador = @IdControlador WHERE IdDispositivo = @IdDispositivo");

                // Define as informações do parâmetro criado
                SqlParameter param = new SqlParameter("@IdDispositivo", sensor.Id);
                comand.Parameters.Add(param);
                SqlParameter param1 = new SqlParameter("@Ativo", sensor.Ativo);
                comand.Parameters.Add(param1);
                SqlParameter param2 = new SqlParameter("@Porta", sensor.Porta);
                comand.Parameters.Add(param2);
                SqlParameter param3 = new SqlParameter("@Nome", ""); // ??;
                comand.Parameters.Add(param3);
                SqlParameter param4 = new SqlParameter("@ValorMaximo", ""); // ??
                comand.Parameters.Add(param4);
                SqlParameter param5 = new SqlParameter("@ValorMinimo", ""); // ??
                comand.Parameters.Add(param5);
                SqlParameter param6 = new SqlParameter("@Discriminator", DISCRIMINATOR_SENSOR);
                comand.Parameters.Add(param6);
                SqlParameter param7 = new SqlParameter("@Estado", sensor.Estado); // ??
                comand.Parameters.Add(param7);
                SqlParameter param8 = new SqlParameter("@IdControlador", sensor.Embarcadoid); // Deveria ser ControladorId
                comand.Parameters.Add(param8);

                comand.ExecuteNonQuery();

                return sensor;

            }
            finally
            {
                closeResources(conection, null);
            }

        }

        private Sensor readSingleSensor(SqlDataReader reader)
        {
            Sensor sensor = null;

            if (reader.Read())
            {
                sensor = readSensor(reader);
            }

            return sensor;
        }

        private Sensor readSensor(SqlDataReader reader)
        {
            Sensor sensor = null;

            if (reader != null)
            {

                if (reader.HasRows)
                {
                    sensor = new Sensor();

                    sensor.Id = Convert.ToInt32(reader["IdDispositivo"]);
                    sensor.Ativo = Convert.ToBoolean(reader["Ativo"]);
                    sensor.Porta = Convert.ToInt32(reader["Porta"]);
                    sensor.Estado = Convert.ToInt32(reader["Estado"]);
                    sensor.Embarcadoid = Convert.ToInt32(reader["IdControlador"]);
                }

            }

            return sensor;
        }

        private List<Sensor> readAllSensor(SqlDataReader reader)
        {

            List<Sensor> sensors = new List<Sensor>();

            if (reader != null)
            {

                while (reader.Read())
                {
                    sensors.Add(readSensor(reader));
                }

            }

            return sensors;

        }
    }
}
