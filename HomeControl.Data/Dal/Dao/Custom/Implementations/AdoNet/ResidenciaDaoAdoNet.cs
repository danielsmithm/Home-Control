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

                SqlCommand comand = createCommand(conection, "INSERT INTO RESIDENCIA(nome) VALUES(@nome); SELECT CAST(scope_identity() AS int)");

                // Define as informações do parâmetro criado
                SqlParameter param = new SqlParameter("@nome", residencia.Nome);
                comand.Parameters.Add(param);

                // TODO: Verificar se o resultado retornado não é nulo para poder converter.
                residencia.Id = Convert.ToInt32( comand.ExecuteNonQuery() );

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

        public Residencia Find(int id)
        {
            SqlConnection conection = null;
            SqlDataReader reader = null;            

            try
            {
                // instantiate and open connection
                conection = ConnectionFactory.getConnection();
                conection.Open();

                SqlCommand comand = createCommand(conection, "SELECT id,nome from RESIDENCIAS where id = @id");
                
                // Define as informações de parâmetro
                SqlParameter param = new SqlParameter("@id", id);
                comand.Parameters.Add(param);

                // Executando o commando e obtendo o resultado
                reader = comand.ExecuteReader();

                return readResidencia(reader);
                
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

                SqlCommand comand = createCommand(conection,"SELECT id,nome from RESIDENCIAS");                

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

                SqlCommand comand = createCommand(conection, "DELETE from RESIDENCIAS where id = @id");

                // Define as informações do parâmetro criado
                SqlParameter param = new SqlParameter("@id", residencia.Id);

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

                SqlCommand comand = createCommand(conection, "UPDATE RESIDENCIAS Set nome = @nome WHERE id = @id");

                // Define as informações do parâmetro criado
                SqlParameter param = new SqlParameter("@id", residencia.Id);
                comand.Parameters.Add(param);
                SqlParameter param1 = new SqlParameter("@nome", residencia.Nome);
                comand.Parameters.Add(param1);

                comand.ExecuteNonQuery();
                
                return residencia;

            }
            finally
            {
                closeResources(conection, null);
            }

        }


        private Residencia readResidencia(SqlDataReader reader)
        {
            Residencia residencia = null;

            if (reader != null)
            {

                if (reader.HasRows)
                {
                    residencia = new Residencia();

                    residencia.Id = Convert.ToInt32(reader["id"]);
                    residencia.Nome = (String)reader["nome"];
                }

            }

            return residencia;
        }

        private List<Residencia> readAllResidencia(SqlDataReader reader)
        {

            List<Residencia> residencias = new List<Residencia>();

            if (reader != null)
            {

                while (reader.NextResult())
                {
                    residencias.Add(readResidencia(reader));
                }

            }

            return residencias;

        }

    }
}
