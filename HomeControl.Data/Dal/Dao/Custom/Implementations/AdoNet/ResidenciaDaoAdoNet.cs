using HomeControl.Data.Dal.Dao.Custom.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HomeControl.Domain.Residencia;
using System.Data.SqlClient;
using System.Data;

namespace HomeControl.Data.Dal.Dao.Custom.Implementations.AdoNet
{
    public class ResidenciaDaoAdoNet : AbstractAdoNetDao, IResidenciaDao
    {
        public Residencia Add(Residencia residencia)
        {
            
            SqlConnection conection = null;

            try
            {
                conection = ConnectionFactory.getConnection();
                conection.Open();

                SqlCommand comand = createCommand(conection, "INSERT INTO RESIDENCIA(Nome, Cidade, Pais, Bairro, Rua, Numero) VALUES(@Nome, @Cidade, @Pais, @Bairro, @Rua, @Numero); SELECT CAST(scope_identity() AS int)");

                // Define as informações do parâmetro criado
                SqlParameter param = new SqlParameter("@Nome", residencia.Nome);
                comand.Parameters.Add(param);
                SqlParameter param1 = new SqlParameter("@Cidade", ""); // Campo faltando no Modelo
                comand.Parameters.Add(param1);
                SqlParameter param2 = new SqlParameter("@Pais", ""); // Campo faltando no Modelo
                comand.Parameters.Add(param2);
                SqlParameter param3 = new SqlParameter("@Bairro", ""); // Campo faltando no Modelo
                comand.Parameters.Add(param3);
                SqlParameter param4 = new SqlParameter("@Rua", ""); // Campo faltando no Modelo
                comand.Parameters.Add(param4);
                SqlParameter param5 = new SqlParameter("@Numero", ""); // Campo faltando no Modelo
                comand.Parameters.Add(param5);

                // TODO: Verificar se o resultado retornado não é nulo para poder converter.
                residencia.Id = Convert.ToInt32( comand.ExecuteScalar() );

                return residencia;
            }
            finally
            {
                closeResources(conection, null);
            }
        }

        public void Dispose()
        {
            
        }

        public Residencia Find(int idResidencia)
        {
            SqlConnection conection = null;
            SqlDataReader reader = null;            

            try
            {
                // instantiate and open connection
                conection = ConnectionFactory.getConnection();
                conection.Open();

                SqlCommand comand = createCommand(conection, "SELECT idResidencia, Nome, Pais, Cidade, Bairro, Rua, Numero from RESIDENCIA where idResidencia = @idResidencia");
                
                // Define as informações de parâmetro
                SqlParameter param = new SqlParameter("@idResidencia", idResidencia);
                comand.Parameters.Add(param);

                // Executando o commando e obtendo o resultado
                reader = comand.ExecuteReader();

                return readSingleResidencia(reader);
                
            }
            finally
            {
                // Fecha o datareader
                closeResources(conection, reader);
            }
        }

        public List<Residencia> FindAll()
        {
            SqlConnection conection = null;
            SqlDataReader reader = null;

            try
            {
                // instantiate and open connection
                conection = ConnectionFactory.getConnection();
                conection.Open();

                SqlCommand comand = createCommand(conection, "SELECT idResidencia, Nome, Pais, Cidade, Bairro, Rua, Numero from RESIDENCIA");                

                // Executando o commando e obtendo o resultado
                reader = comand.ExecuteReader();

                return readAllResidencia(reader);

            }
            finally
            {                
                closeResources(conection, reader);
            }
        }


        public Residencia Remove(Residencia residencia)
        {

            SqlConnection conection = null;

            try
            {
                conection = ConnectionFactory.getConnection();
                conection.Open();

                SqlCommand comand = createCommand(conection, "DELETE from RESIDENCIA where idResidencia = @idResidencia");

                // Define as informações do parâmetro criado
                SqlParameter param = new SqlParameter("@idResidencia", residencia.Id);

                comand.Parameters.Add(param);

                comand.ExecuteNonQuery();

                return residencia;
            }
            finally
            {
                closeResources(conection, null);
            }
        }

        public void SaveChanges()
        {
        }

        public Residencia Update(Residencia residencia)
        {
            SqlConnection conection = null;

            try
            {
                conection = ConnectionFactory.getConnection();
                conection.Open();

                SqlCommand comand = createCommand(conection, "UPDATE RESIDENCIA Set Nome = @Nome, Cidade = @Cidade, Pais = @Pais, Bairro = @Bairro, Rua = @Rua, Numero = @Numero WHERE idResidencia = @idResidencia");

                // Define as informações do parâmetro criado
                SqlParameter param = new SqlParameter("@idResidencia", residencia.Id);
                comand.Parameters.Add(param);
                SqlParameter param1 = new SqlParameter("@Nome", residencia.Nome);
                comand.Parameters.Add(param1);
                SqlParameter param2 = new SqlParameter("@Cidade", ""); // Campo faltando no Modelo
                comand.Parameters.Add(param2);
                SqlParameter param3 = new SqlParameter("@Pais", ""); // Campo faltando no Modelo
                comand.Parameters.Add(param3);
                SqlParameter param4 = new SqlParameter("@Bairro", ""); // Campo faltando no Modelo
                comand.Parameters.Add(param4);
                SqlParameter param5 = new SqlParameter("@Rua", ""); // Campo faltando no Modelo
                comand.Parameters.Add(param5);
                SqlParameter param6 = new SqlParameter("@Numero", ""); // Campo faltando no Modelo
                comand.Parameters.Add(param6);

                comand.ExecuteNonQuery();
                
                return residencia;

            }
            finally
            {
                closeResources(conection, null);
            }

        }

        private Residencia readSingleResidencia(SqlDataReader reader)
        {
            Residencia residencia = null;

            if ( reader.Read() )
            {
                residencia = readResidencia(reader);
            }

            return residencia;
        }


        private Residencia readResidencia(SqlDataReader reader)
        {
            Residencia residencia = null;


            if (reader != null)
            {
                if (reader.HasRows)
                {
                    residencia = new Residencia();

                    residencia.Id = Convert.ToInt32(reader["idResidencia"]);
                    residencia.Nome = (String)reader["Nome"];
                }
            }

            return residencia;
        }

        private List<Residencia> readAllResidencia(SqlDataReader reader)
        {

            List<Residencia> residencias = new List<Residencia>();

            if (reader != null)
            {
                while (reader.Read())
                {
                    residencias.Add(readResidencia(reader));
                } 
            }

            return residencias;

        }
    }
}
