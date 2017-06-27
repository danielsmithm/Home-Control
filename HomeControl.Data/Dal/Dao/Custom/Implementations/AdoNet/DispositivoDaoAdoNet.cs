using HomeControl.Data.Dal.Dao.Custom.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HomeControl.Domain.Dispositivos;
using System.Data.SqlClient;
using HomeControl.Domain.Domain.Potenciometro;

namespace HomeControl.Data.Dal.Dao.Custom.Implementations.AdoNet
{
    public class DispositivoDaoAdoNet : AbstractAdoNetDao, IDispositivoDao
    {
        public Dispositivo Add(Dispositivo dispositivo)
        {
            SqlConnection conection = null;

            try
            {
                conection = ConnectionFactory.getConnection();
                conection.Open();

                SqlCommand comand = createCommand(conection, "INSERT INTO DISPOSITIVOES( Ativo, Porta, Estado, Discriminator, Comodo_Id, ValorAtual, ValorMaximo, ValorMinimo, EstadoAtual ) VALUES( @Ativo, @Porta, @Estado, @Discriminator, @Comodo_Id, @ValorAtual, @ValorMaximo, @ValorMinimo, @EstadoAtual ); SELECT CAST(scope_identity() AS int)");

                // Define as informações do parâmetro criado
                SqlParameter param = new SqlParameter("@Ativo", dispositivo.Ativo);
                comand.Parameters.Add(param);
                SqlParameter param1 = new SqlParameter("@Porta", dispositivo.Porta);
                comand.Parameters.Add(param1);
                SqlParameter param2 = new SqlParameter("@Estado", dispositivo.Estado);
                comand.Parameters.Add(param2);
                SqlParameter param3 = new SqlParameter("@Discriminator", null ); // dispositivo.Discriminator ??
                comand.Parameters.Add(param3);
                SqlParameter param4 = new SqlParameter("@Comodo_Id", dispositivo.ComodoId);
                comand.Parameters.Add(param4);
                SqlParameter param5 = new SqlParameter("@ValorAtual", ((DefaultPotenciometro) dispositivo).ValorAtual ); // ??
                comand.Parameters.Add(param5);
                SqlParameter param6 = new SqlParameter("@ValorMaximo", ((DefaultPotenciometro)dispositivo).ValorMaximo ); // ??
                comand.Parameters.Add(param6);
                SqlParameter param7 = new SqlParameter("@ValorMinimo", ((DefaultPotenciometro)dispositivo).ValorMinimo ); // ??
                comand.Parameters.Add(param7);
                SqlParameter param8 = new SqlParameter("@EstadoAtual", ((DefaultPotenciometro)dispositivo).EstadoAtual ); // ??
                comand.Parameters.Add(param8);

                // TODO: Verificar se o resultado retornado não é nulo para poder converter.
                dispositivo.Id = Convert.ToInt32(comand.ExecuteNonQuery());

                return dispositivo;
            }
            finally
            {
                closeResources(conection, null);
            }
        }

        public void Dispose()
        {

        }

        public Dispositivo Find(int id)
        {
            SqlConnection conection = null;
            SqlDataReader reader = null;

            try
            {
                // instantiate and open connection
                conection = ConnectionFactory.getConnection();
                conection.Open();

                SqlCommand comand = createCommand(conection, "SELECT (  Id, Ativo, Porta, Estado, Discriminator, Comodo_Id, ValorAtual, ValorMaximo, ValorMinimo, EstadoAtual ) from DISPOSITIVOES where Id = @Id");

                // Define as informações de parâmetro
                SqlParameter param = new SqlParameter("@Id", id);
                comand.Parameters.Add(param);

                // Executando o commando e obtendo o resultado
                reader = comand.ExecuteReader();

                return readDispositivo(reader);

            }
            finally
            {
                // Fecha o datareader
                closeResources(conection, reader);
            }
        }

        public List<Dispositivo> FindAll()
        {
            SqlConnection conection = null;
            SqlDataReader reader = null;

            try
            {
                // instantiate and open connection
                conection = ConnectionFactory.getConnection();
                conection.Open();

                SqlCommand comand = createCommand(conection, "SELECT (  Id, Ativo, Porta, Estado, Discriminator, Comodo_Id, ValorAtual, ValorMaximo, ValorMinimo, EstadoAtual ) from DISPOSITIVOES");

                // Executando o commando e obtendo o resultado
                reader = comand.ExecuteReader();

                return readAllDispositivo(reader);

            }
            finally
            {
                closeResources(conection, reader);
            }
        }


        public Dispositivo Remove(Dispositivo dispositivo)
        {

            SqlConnection conection = null;

            try
            {
                conection = ConnectionFactory.getConnection();
                conection.Open();

                SqlCommand comand = createCommand(conection, "DELETE from DISPOSITIVOES where Id = @Id");

                // Define as informações do parâmetro criado
                SqlParameter param = new SqlParameter("@Id", dispositivo.Id);

                comand.Parameters.Add(param);

                comand.ExecuteNonQuery();

                return dispositivo;
            }
            finally
            {
                closeResources(conection, null);
            }
        }

        public void SaveChanges()
        {
        }

        public Dispositivo Update(Dispositivo dispositivo)
        {
            SqlConnection conection = null;

            try
            {
                conection = ConnectionFactory.getConnection();
                conection.Open();
                
                SqlCommand comand = createCommand(conection, "UPDATE DISPOSITIVOES Set Ativo = @Ativo, Porta = @Porta, Estado = @Estado, Discriminator = @Discriminator, Comodo_Id = @Comodo_Id, ValorAtual = @ValorAtual, ValorMaximo = @ValorMaximo, ValorMinimo = @ValorMinimo, EstadoAtual = @EstadoAtual WHERE Id = @Id");

                // Define as informações do parâmetro criado
                SqlParameter param = new SqlParameter("@Id", dispositivo.Id);
                comand.Parameters.Add(param);
                SqlParameter param1 = new SqlParameter("@Ativo", dispositivo.Ativo);
                comand.Parameters.Add(param1);
                SqlParameter param2 = new SqlParameter("@Porta", dispositivo.Porta);
                comand.Parameters.Add(param2);
                SqlParameter param3 = new SqlParameter("@Estado", dispositivo.Estado);
                comand.Parameters.Add(param3);
                SqlParameter param4 = new SqlParameter("@Discriminator", null); // dispositivo.Discriminator ??
                comand.Parameters.Add(param4);
                SqlParameter param5 = new SqlParameter("@Comodo_Id", dispositivo.ComodoId);
                comand.Parameters.Add(param5);
                SqlParameter param6 = new SqlParameter("@ValorAtual", ((DefaultPotenciometro)dispositivo).ValorAtual); // ??
                comand.Parameters.Add(param6);
                SqlParameter param7 = new SqlParameter("@ValorMaximo", ((DefaultPotenciometro)dispositivo).ValorMaximo); // ??
                comand.Parameters.Add(param7);
                SqlParameter param8 = new SqlParameter("@ValorMinimo", ((DefaultPotenciometro)dispositivo).ValorMinimo); // ??
                comand.Parameters.Add(param8);
                SqlParameter param9 = new SqlParameter("@EstadoAtual", ((DefaultPotenciometro)dispositivo).EstadoAtual); // ??
                comand.Parameters.Add(param9);

                comand.ExecuteNonQuery();

                return dispositivo;

            }
            finally
            {
                closeResources(conection, null);
            }

        }

        private Dispositivo readDispositivo(SqlDataReader reader)
        {
            DefaultPotenciometro dispositivo = null;

            if (reader != null)
            {

                if (reader.HasRows)
                {
                    dispositivo = new DefaultPotenciometro();

                    dispositivo.Id = Convert.ToInt32(reader["Id"]);
                    dispositivo.Ativo = (Boolean)reader["Nome"];
                    dispositivo.Porta = Convert.ToInt32(reader["Porta"]);
                    dispositivo.Estado = Convert.ToInt32(reader["Estado"]);
                    //dispositivo.Discriminator = (String)reader["Discriminator"]; // ???
                    dispositivo.ComodoId = Convert.ToInt32(reader["Comodo_Id"]);
                    dispositivo.ValorAtual  = (float) Convert.ToDouble(reader["ValorAtual"]); // Funciona ??
                    dispositivo.ValorMaximo = (float) Convert.ToDouble(reader["ValorMaximo"]);
                    dispositivo.ValorMinimo = (float) Convert.ToDouble(reader["ValorMinimo"]);
                    dispositivo.EstadoAtual = (float) Convert.ToDouble(reader["EstadoAtual"]);
                }

            }

            return dispositivo;
        }

        private List<Dispositivo> readAllDispositivo(SqlDataReader reader)
        {

            List<Dispositivo> dispositivos = new List<Dispositivo>();

            if (reader != null)
            {

                while (reader.NextResult())
                {
                    dispositivos.Add(readDispositivo(reader));
                }

            }

            return dispositivos;

        }

        public List<Dispositivo> FindByPorta(int porta)
        {
            SqlConnection conection = null;
            SqlDataReader reader = null;

            try
            {
                // instantiate and open connection
                conection = ConnectionFactory.getConnection();
                conection.Open();

                SqlCommand comand = createCommand(conection, "SELECT (  Id, Ativo, Porta, Estado, Discriminator, Comodo_Id, ValorAtual, ValorMaximo, ValorMinimo, EstadoAtual ) from DISPOSITIVOES WHERE Porta = @Porta");

                SqlParameter param = new SqlParameter("@Porta", porta);
                comand.Parameters.Add(param);

                // Executando o commando e obtendo o resultado
                reader = comand.ExecuteReader();

                return readAllDispositivo(reader);

            }
            finally
            {
                closeResources(conection, reader);
            }
        }
    }
}
