using HomeControl.Data.Dal.Dao.Custom.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HomeControl.Domain.Interruptores;
using System.Data.SqlClient;

namespace HomeControl.Data.Dal.Dao.Custom.Implementations.AdoNet
{
    public class InterruptorDaoAdoNet : AbstractAdoNetDao, IInterruptorDao
    {

        public const String DISCRIMINATOR_INTERRUPTOR = "INTERRUPTOR";

        public Interruptor Add(Interruptor interruptor)
        {
            SqlConnection conection = null;

            try
            {
                conection = ConnectionFactory.getConnection();
                conection.Open();

                SqlCommand comand = createCommand(conection, "INSERT INTO DISPOSITIVO( Ativo, Porta, Nome, ValorMaximo, ValorMinimo, Discriminator, Estado, IdControlador ) VALUES( @Ativo, @Porta, @Nome, @ValorMaximo, @ValorMinimo, @Discriminator, @Estado, @IdControlador ); SELECT CAST(scope_identity() AS int)");

                // Define as informações do parâmetro criado
                SqlParameter param = new SqlParameter("@Ativo", interruptor.Ativo);
                comand.Parameters.Add(param);
                SqlParameter param1 = new SqlParameter("@Porta", interruptor.Porta);
                comand.Parameters.Add(param1);
                SqlParameter param2 = new SqlParameter("@Nome", ""); // ??;
                comand.Parameters.Add(param2);
                SqlParameter param3 = new SqlParameter("@ValorMaximo", ""); // ??
                comand.Parameters.Add(param3);
                SqlParameter param4 = new SqlParameter("@ValorMinimo", ""); // ??
                comand.Parameters.Add(param4);
                SqlParameter param5 = new SqlParameter("@Discriminator", DISCRIMINATOR_INTERRUPTOR);
                comand.Parameters.Add(param5);
                SqlParameter param6 = new SqlParameter("@Estado", interruptor.Estado);
                comand.Parameters.Add(param6);
                SqlParameter param7 = new SqlParameter("@IdControlador", interruptor.Embarcadoid);
                comand.Parameters.Add(param7);

                // TODO: Verificar se o resultado retornado não é nulo para poder converter.
                interruptor.Id = Convert.ToInt32( comand.ExecuteScalar() );

                return interruptor;
            }
            finally
            {
                closeResources(conection, null);
            }
        }

        public void Dispose()
        {

        }

        public Interruptor Find(int id)
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
                SqlParameter param1 = new SqlParameter("@Discriminator", DISCRIMINATOR_INTERRUPTOR);
                comand.Parameters.Add(param1);

                // Executando o commando e obtendo o resultado
                reader = comand.ExecuteReader();

                return readSingleInterruptor(reader);

            }
            finally
            {
                // Fecha o datareader
                closeResources(conection, reader);
            }
        }

        public List<Interruptor> FindAll()
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
                SqlParameter param = new SqlParameter("@Discriminator", DISCRIMINATOR_INTERRUPTOR);
                comand.Parameters.Add(param); ;

                // Executando o commando e obtendo o resultado
                reader = comand.ExecuteReader();

                return readAllInterruptor(reader);

            }
            finally
            {
                closeResources(conection, reader);
            }
        }


        public Interruptor Remove(Interruptor interruptor)
        {

            SqlConnection conection = null;

            try
            {
                conection = ConnectionFactory.getConnection();
                conection.Open();

                SqlCommand comand = createCommand(conection, "DELETE from DISPOSITIVO where IdDispositivo = @IdDispositivo");

                // Define as informações do parâmetro criado
                SqlParameter param = new SqlParameter("@IdDispositivo", interruptor.Id);

                comand.Parameters.Add(param);

                comand.ExecuteNonQuery();

                return interruptor;
            }
            finally
            {
                closeResources(conection, null);
            }
        }

        public void SaveChanges()
        {
        }

        public Interruptor Update(Interruptor interruptor)
        {
            SqlConnection conection = null;

            try
            {
                conection = ConnectionFactory.getConnection();
                conection.Open();

                SqlCommand comand = createCommand(conection, "UPDATE DISPOSITIVO Set Ativo = @Ativo, Porta = @Porta, Nome = @Nome, ValorMaximo = @ValorMaximo, ValorMinimo = @ValorMinimo, Discriminator = @Discriminator, Estado = @Estado, IdControlador = @IdControlador WHERE IdDispositivo = @IdDispositivo");

                // Define as informações do parâmetro criado
                SqlParameter param = new SqlParameter("@IdDispositivo", interruptor.Id);
                comand.Parameters.Add(param);
                SqlParameter param1 = new SqlParameter("@Ativo", interruptor.Ativo);
                comand.Parameters.Add(param1);
                SqlParameter param2 = new SqlParameter("@Porta", interruptor.Porta);
                comand.Parameters.Add(param2);
                SqlParameter param3 = new SqlParameter("@Nome", ""); // ??;
                comand.Parameters.Add(param3);
                SqlParameter param4 = new SqlParameter("@ValorMaximo", ""); // ??
                comand.Parameters.Add(param4);
                SqlParameter param5 = new SqlParameter("@ValorMinimo", ""); // ??
                comand.Parameters.Add(param5);
                SqlParameter param6 = new SqlParameter("@Discriminator", DISCRIMINATOR_INTERRUPTOR);
                comand.Parameters.Add(param6);
                SqlParameter param7 = new SqlParameter("@Estado", interruptor.Estado); // ??
                comand.Parameters.Add(param7);
                SqlParameter param8 = new SqlParameter("@IdControlador", interruptor.Embarcadoid);
                comand.Parameters.Add(param8);

                comand.ExecuteNonQuery();

                return interruptor;

            }
            finally
            {
                closeResources(conection, null);
            }

        }

        private Interruptor readSingleInterruptor(SqlDataReader reader)
        {
            Interruptor interruptor = null;

            if (reader.Read())
            {
                interruptor = readInterruptor(reader);
            }

            return interruptor;
        }

        private Interruptor readInterruptor(SqlDataReader reader)
        {
            Interruptor interruptor = null;

            if (reader != null)
            {

                if (reader.HasRows)
                {
                    interruptor = new Interruptor();

                    interruptor.Id = Convert.ToInt32(reader["IdDispositivo"]);
                    interruptor.Ativo = Convert.ToBoolean(reader["Ativo"]);
                    interruptor.Porta = Convert.ToInt32(reader["Porta"]);
                    interruptor.Estado = Convert.ToInt32(reader["Estado"]);
                    interruptor.ComodoId = Convert.ToInt32(reader["IdControlador"]); // Deveria atribuir a IdControlador e não ComodoId
                }

            }

            return interruptor;
        }

        private List<Interruptor> readAllInterruptor(SqlDataReader reader)
        {

            List<Interruptor> interruptors = new List<Interruptor>();

            if (reader != null)
            {

                while (reader.Read())
                {
                    interruptors.Add(readInterruptor(reader));
                }

            }

            return interruptors;

        }
    }
}
