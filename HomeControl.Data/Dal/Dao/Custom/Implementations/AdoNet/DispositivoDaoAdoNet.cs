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

                SqlCommand comand = createCommand(conection, "INSERT INTO DISPOSITIVO( Ativo, Porta, Nome, ValorMaximo, ValorMinimo, Estado, IdControlador ) VALUES( @Ativo, @Porta, @Nome, @ValorMaximo, @ValorMinimo, @Estado, @IdControlador ); SELECT CAST(scope_identity() AS int)");

                // Define as informações do parâmetro criado
                SqlParameter param = new SqlParameter("@Ativo", dispositivo.Ativo);
                comand.Parameters.Add(param);
                SqlParameter param1 = new SqlParameter("@Porta", dispositivo.Porta);
                comand.Parameters.Add(param1);
                SqlParameter param2 = new SqlParameter("@Nome", null);
                comand.Parameters.Add(param2);
                SqlParameter param3 = new SqlParameter("@ValorMaximo", ((DefaultPotenciometro)dispositivo).ValorMaximo );
                comand.Parameters.Add(param3);
                SqlParameter param4 = new SqlParameter("@ValorMinimo", ((DefaultPotenciometro)dispositivo).ValorMinimo );
                comand.Parameters.Add(param4);
                SqlParameter param5 = new SqlParameter("@Estado", dispositivo.Estado );
                comand.Parameters.Add(param5);
                SqlParameter param6 = new SqlParameter("@IdControlador", null );
                comand.Parameters.Add(param6);

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

                SqlCommand comand = createCommand(conection, "SELECT (  IdDispositivo, Ativo, Porta, Nome, ValorMaximo, ValorMinimo, Estado, IdControlador ) from DISPOSITIVO where IdDispositivo = @Id");

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

                SqlCommand comand = createCommand(conection, "SELECT (  IdDispositivo, Ativo, Porta, Nome, ValorMaximo, ValorMinimo, Estado, IdControlador ) from DISPOSITIVO");

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

                SqlCommand comand = createCommand(conection, "DELETE from DISPOSITIVO where IdDispositivo = @Id");

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
                
                SqlCommand comand = createCommand(conection, "UPDATE DISPOSITIVO Set Ativo = @Ativo, Porta = @Porta, Nome = @Nome, ValorMaximo = @ValorMaximo, ValorMinimo = @ValorMinimo, Estado = @Estado, IdControlador = @IdControlador WHERE IdDispositivo = @IdDispositivo");

                // Define as informações do parâmetro criado
                SqlParameter param = new SqlParameter("@IdDispositivo", dispositivo.Id);
                comand.Parameters.Add(param);
                SqlParameter param1 = new SqlParameter("@Ativo", dispositivo.Ativo);
                comand.Parameters.Add(param1);
                SqlParameter param2 = new SqlParameter("@Porta", dispositivo.Porta);
                comand.Parameters.Add(param2);
                SqlParameter param3 = new SqlParameter("@Nome", null);
                comand.Parameters.Add(param3);
                SqlParameter param4 = new SqlParameter("@ValorMaximo", ((DefaultPotenciometro)dispositivo).ValorMaximo);
                comand.Parameters.Add(param4);
                SqlParameter param5 = new SqlParameter("@ValorMinimo", ((DefaultPotenciometro)dispositivo).ValorMinimo);
                comand.Parameters.Add(param5);
                SqlParameter param6 = new SqlParameter("@Estado", dispositivo.Estado);
                comand.Parameters.Add(param6);
                SqlParameter param7 = new SqlParameter("@IdControlador", null);
                comand.Parameters.Add(param7);

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

                    dispositivo.Id = Convert.ToInt32(reader["IdDispositivo"]);
                    dispositivo.Ativo = (Boolean)reader["Ativo"];
                    dispositivo.Porta = Convert.ToInt32(reader["Porta"]);
                    //dispositivo.Nome = Convert.ToInt32(reader["Nome"]);       // Incluir Nome
                    dispositivo.ValorMaximo = (float)Convert.ToDouble(reader["ValorMaximo"]);
                    dispositivo.ValorMinimo = (float)Convert.ToDouble(reader["ValorMinimo"]);
                    dispositivo.Estado = Convert.ToInt32(reader["Estado"]);
                    //dispositivo.IdControlador = Convert.ToInt32(reader["IdControlador"]);     // Incluir IdControlador
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

                SqlCommand comand = createCommand(conection, "SELECT (  IdDispositivo, Ativo, Porta, Nome, ValorMaximo, ValorMinimo, Estado, IdControlador ) from DISPOSITIVO WHERE Porta = @Porta");

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
