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

        public const String DISCRIMINATOR_DISPOSITIVO = "DEFAULT";
        public const String DISCRIMINATOR_INTERRUPTOR = "INTERRUPTOR";
        public const String DISCRIMINATOR_SENSOR = "SENSOR";
        public const String DISCRIMINATOR_POTENCIOMETRO = "POTENCIOMETRO";
                
        private Dispositivo readDispositivo(SqlDataReader reader)
        {
            DefaultPotenciometro dispositivo = null;

            if (reader != null)
            {

                if (reader.HasRows)
                {
                    dispositivo = new DefaultPotenciometro();

                    dispositivo.Id = Convert.ToInt32(reader["IdDispositivo"]);
                    dispositivo.Ativo = Convert.ToBoolean(reader["Ativo"]);
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

                while (reader.Read())
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

                SqlCommand comand = createCommand(conection, "SELECT IdDispositivo, Ativo, Porta, Nome, ValorMaximo, ValorMinimo, Estado, IdControlador from DISPOSITIVO WHERE Porta = @Porta");

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

        public List<Dispositivo> FindAll()
        {
            throw new NotImplementedException();
        }

        public Dispositivo Find(int id)
        {
            throw new NotImplementedException();
        }

        public Dispositivo Update(Dispositivo entity)
        {
            throw new NotImplementedException();
        }

        public void SaveChanges()
        {
            throw new NotImplementedException();
        }

        public Dispositivo Add(Dispositivo entity)
        {
            throw new NotImplementedException();
        }

        public Dispositivo Remove(Dispositivo entity)
        {
            throw new NotImplementedException();
        }

        public void Dispose()
        {
        
        }

    }
}
