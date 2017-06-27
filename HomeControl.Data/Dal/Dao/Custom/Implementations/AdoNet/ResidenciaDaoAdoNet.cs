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

                SqlCommand comand = createCommand(conection,"INSERT INFO RESIDENCIA(nome) VALUES(@nome)");

                // Define as informações do parâmetro criado
                SqlParameter param = new SqlParameter("@nome", residencia.Nome);
                comand.Parameters.Add(param);

                comand.ExecuteNonQuery();

                // Pegar ultimo valor de id inserido
                //entity.Id = cmd.;
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

                SqlCommand comand = createCommand(conection, "SELECT id,nome from RESIDENCIA where id = @id");
                
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

                SqlCommand comand = createCommand(conection,"SELECT id,nome from RESIDENCIA");                

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

                SqlCommand comand = new SqlCommand();

                //TODO: SQL
                comand.CommandText = "INSERT INFO RESIDENCIA(nome) VALUES(@id)";
                comand.CommandType = CommandType.Text;
                comand.Connection = conection;

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

                SqlCommand comand = new SqlCommand();

                //TODO: SQL
                comand.CommandText = "UPDATE Residencias SET Nome = @Nome WHERE ID = @ID;";
                comand.CommandType = CommandType.Text;
                comand.Connection = conection;

                // Define as informações do parâmetro criado
                SqlParameter param = new SqlParameter("@ID", residencia.Id);
           
                comand.Parameters.Add(param);

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
