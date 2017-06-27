using HomeControl.Data.Dal.Dao.Custom.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HomeControl.Domain.Residencia;
using System.Data.SqlClient;

namespace HomeControl.Data.Dal.Dao.Custom.Implementations.AdoNet
{
    public class ComodoDaoAdoNet : AbstractAdoNetDao, IComodoDao
    {
        public Comodo Add(Comodo comodo)
        {

            SqlConnection conection = null;

            try
            {
                conection = ConnectionFactory.getConnection();
                conection.Open();

                SqlCommand comand = createCommand(conection, "INSERT INTO COMODOES(Nome, Residencia_Id) VALUES(@Nome, @Residencias_Id); SELECT CAST(scope_identity() AS int)");

                // Define as informações do parâmetro criado
                SqlParameter param = new SqlParameter("@Nome", comodo.Nome);
                comand.Parameters.Add(param);
                SqlParameter param1 = new SqlParameter("@Residencia_Id", comodo.ResidenciaId);
                comand.Parameters.Add(param1);

                // TODO: Verificar se o resultado retornado não é nulo para poder converter.
                comodo.Id = Convert.ToInt32(comand.ExecuteNonQuery());

                return comodo;
            }
            finally
            {
                closeResources(conection, null);
            }
        }

        public void Dispose()
        {

        }

        public Comodo Find(int id)
        {
            SqlConnection conection = null;
            SqlDataReader reader = null;

            try
            {
                // instantiate and open connection
                conection = ConnectionFactory.getConnection();
                conection.Open();

                SqlCommand comand = createCommand(conection, "SELECT Id,Nome,Residencia_Id from COMODOES where Id = @Id");

                // Define as informações de parâmetro
                SqlParameter param = new SqlParameter("@Id", id);
                comand.Parameters.Add(param);

                // Executando o commando e obtendo o resultado
                reader = comand.ExecuteReader();

                return readComodo(reader);

            }
            finally
            {
                // Fecha o datareader
                closeResources(conection, reader);
            }
        }

        public List<Comodo> FindAll()
        {
            SqlConnection conection = null;
            SqlDataReader reader = null;

            try
            {
                // instantiate and open connection
                conection = ConnectionFactory.getConnection();
                conection.Open();

                SqlCommand comand = createCommand(conection, "SELECT Id,Nome,Residencia_Id from COMODOES");

                // Executando o commando e obtendo o resultado
                reader = comand.ExecuteReader();

                return readAllComodo(reader);

            }
            finally
            {
                closeResources(conection, reader);
            }
        }


        public Comodo Remove(Comodo comodo)
        {

            SqlConnection conection = null;

            try
            {
                conection = ConnectionFactory.getConnection();
                conection.Open();

                SqlCommand comand = createCommand(conection, "DELETE from COMODOES where Id = @Id");

                // Define as informações do parâmetro criado
                SqlParameter param = new SqlParameter("@Id", comodo.Id);

                comand.Parameters.Add(param);

                comand.ExecuteNonQuery();

                return comodo;
            }
            finally
            {
                closeResources(conection, null);
            }
        }

        public void SaveChanges()
        {
        }

        public Comodo Update(Comodo comodo)
        {
            SqlConnection conection = null;

            try
            {
                conection = ConnectionFactory.getConnection();
                conection.Open();

                SqlCommand comand = createCommand(conection, "UPDATE COMODOES Set Nome = @Nome, Residencia_Id = @Residencia_Id WHERE Id = @Id");

                // Define as informações do parâmetro criado
                SqlParameter param = new SqlParameter("@Id", comodo.Id);
                comand.Parameters.Add(param);
                SqlParameter param1 = new SqlParameter("@Nome", comodo.Nome);
                comand.Parameters.Add(param1);
                SqlParameter param2 = new SqlParameter("@Residencia_Id", comodo.ResidenciaId);
                comand.Parameters.Add(param2);

                comand.ExecuteNonQuery();

                return comodo;

            }
            finally
            {
                closeResources(conection, null);
            }

        }

        private Comodo readComodo(SqlDataReader reader)
        {
            Comodo comodo = null;

            if (reader != null)
            {

                if (reader.HasRows)
                {
                    comodo = new Comodo();

                    comodo.Id = Convert.ToInt32(reader["Id"]);
                    comodo.Nome = (String)reader["Nome"];
                    comodo.ResidenciaId = Convert.ToInt32(reader["Residencia_Id"]);
                }

            }

            return comodo;
        }

        private List<Comodo> readAllComodo(SqlDataReader reader)
        {

            List<Comodo> comodos = new List<Comodo>();

            if (reader != null)
            {

                while (reader.NextResult())
                {
                    comodos.Add(readComodo(reader));
                }

            }

            return comodos;

        }
    }
}
