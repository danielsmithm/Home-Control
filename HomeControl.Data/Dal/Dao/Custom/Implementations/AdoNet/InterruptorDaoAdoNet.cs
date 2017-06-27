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
        public Interruptor Add(Interruptor interruptor)
        {
            SqlConnection conection = null;

            try
            {
                conection = ConnectionFactory.getConnection();
                conection.Open();

                SqlCommand comand = createCommand(conection, "INSERT INTO DISPOSITIVOES( Ativo, Porta, Estado, Discriminator, Comodo_Id, ValorAtual, ValorMaximo, ValorMinimo, EstadoAtual ) VALUES( @Ativo, @Porta, @Estado, @Discriminator, @Comodo_Id, @ValorAtual, @ValorMaximo, @ValorMinimo, @EstadoAtual ); SELECT CAST(scope_identity() AS int)");

                // Define as informações do parâmetro criado
                SqlParameter param = new SqlParameter("@Ativo", interruptor.Ativo);
                comand.Parameters.Add(param);
                SqlParameter param1 = new SqlParameter("@Porta", interruptor.Porta);
                comand.Parameters.Add(param1);
                SqlParameter param2 = new SqlParameter("@Estado", interruptor.Estado);
                comand.Parameters.Add(param2);
                SqlParameter param3 = new SqlParameter("@Discriminator", null); // interruptor.Discriminator ??
                comand.Parameters.Add(param3);
                SqlParameter param4 = new SqlParameter("@Comodo_Id", interruptor.ComodoId);
                comand.Parameters.Add(param4);
                SqlParameter param5 = new SqlParameter("@ValorAtual", null); // ??
                comand.Parameters.Add(param5);
                SqlParameter param6 = new SqlParameter("@ValorMaximo", null); // ??
                comand.Parameters.Add(param6);
                SqlParameter param7 = new SqlParameter("@ValorMinimo", null); // ??
                comand.Parameters.Add(param7);
                SqlParameter param8 = new SqlParameter("@EstadoAtual", null); // ??
                comand.Parameters.Add(param8);

                // TODO: Verificar se o resultado retornado não é nulo para poder converter.
                interruptor.Id = Convert.ToInt32(comand.ExecuteNonQuery());

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

                SqlCommand comand = createCommand(conection, "SELECT (  Id, Ativo, Porta, Estado ) from DISPOSITIVOES where Id = @Id");

                // Define as informações de parâmetro
                SqlParameter param = new SqlParameter("@Id", id);
                comand.Parameters.Add(param);

                // Executando o commando e obtendo o resultado
                reader = comand.ExecuteReader();

                return readInterruptor(reader);

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

                SqlCommand comand = createCommand(conection, "SELECT (  Id, Ativo, Porta, Estado ) from DISPOSITIVOES");

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

                SqlCommand comand = createCommand(conection, "DELETE from DISPOSITIVOES where Id = @Id");

                // Define as informações do parâmetro criado
                SqlParameter param = new SqlParameter("@Id", interruptor.Id);

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

                SqlCommand comand = createCommand(conection, "UPDATE DISPOSITIVOES Set Ativo = @Ativo, Porta = @Porta, Estado = @Estado, Discriminator = @Discriminator, Comodo_Id = @Comodo_Id, ValorAtual = @ValorAtual, ValorMaximo = @ValorMaximo, ValorMinimo = @ValorMinimo, EstadoAtual = @EstadoAtual WHERE Id = @Id");

                // Define as informações do parâmetro criado
                SqlParameter param = new SqlParameter("@Id", interruptor.Id);
                comand.Parameters.Add(param);
                SqlParameter param1 = new SqlParameter("@Ativo", interruptor.Ativo);
                comand.Parameters.Add(param1);
                SqlParameter param2 = new SqlParameter("@Porta", interruptor.Porta);
                comand.Parameters.Add(param2);
                SqlParameter param3 = new SqlParameter("@Estado", interruptor.Estado);
                comand.Parameters.Add(param3);
                SqlParameter param4 = new SqlParameter("@Discriminator", null); // interruptor.Discriminator ??
                comand.Parameters.Add(param4);
                SqlParameter param5 = new SqlParameter("@Comodo_Id", interruptor.ComodoId);
                comand.Parameters.Add(param5);
                SqlParameter param6 = new SqlParameter("@ValorAtual", null ); // ??
                comand.Parameters.Add(param6);
                SqlParameter param7 = new SqlParameter("@ValorMaximo", null); // ??
                comand.Parameters.Add(param7);
                SqlParameter param8 = new SqlParameter("@ValorMinimo", null); // ??
                comand.Parameters.Add(param8);
                SqlParameter param9 = new SqlParameter("@EstadoAtual", null); // ??
                comand.Parameters.Add(param9);

                comand.ExecuteNonQuery();

                return interruptor;

            }
            finally
            {
                closeResources(conection, null);
            }

        }

        private Interruptor readInterruptor(SqlDataReader reader)
        {
            Interruptor interruptor = null;

            if (reader != null)
            {

                if (reader.HasRows)
                {
                    interruptor = new Interruptor();

                    interruptor.Id = Convert.ToInt32(reader["Id"]);
                    interruptor.Ativo = (Boolean)reader["Nome"];
                    interruptor.Porta = Convert.ToInt32(reader["Porta"]);
                    interruptor.Estado = Convert.ToInt32(reader["Estado"]);
                    //interruptor.Discriminator = (String)reader["Discriminator"]; // ???
                    interruptor.ComodoId = Convert.ToInt32(reader["Comodo_Id"]);
                }

            }

            return interruptor;
        }

        private List<Interruptor> readAllInterruptor(SqlDataReader reader)
        {

            List<Interruptor> interruptors = new List<Interruptor>();

            if (reader != null)
            {

                while (reader.NextResult())
                {
                    interruptors.Add(readInterruptor(reader));
                }

            }

            return interruptors;

        }
    }
}
