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

                SqlCommand comand = createCommand(conection, "INSERT INTO COMODO(Nome, idResidencia) VALUES(@Nome, @idResidencia); SELECT CAST(scope_identity() AS int)");

                // Define as informações do parâmetro criado
                SqlParameter param = new SqlParameter("@Nome", comodo.Nome);
                comand.Parameters.Add(param);
                SqlParameter param1 = new SqlParameter("@idResidencia", comodo.ResidenciaId);
                comand.Parameters.Add(param1);

                // TODO: Verificar se o resultado retornado não é nulo para poder converter.
                comodo.Id = Convert.ToInt32(comand.ExecuteScalar());

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

        public Comodo Find(int idComodo)
        {
            SqlConnection conection = null;
            SqlDataReader reader = null;

            try
            {
                // instantiate and open connection
                conection = ConnectionFactory.getConnection();
                conection.Open();

                SqlCommand comand = createCommand(conection, "SELECT idComodo, Nome, idResidencia from COMODO where idComodo = @idComodo");

                // Define as informações de parâmetro
                SqlParameter param = new SqlParameter("@idComodo", idComodo);
                comand.Parameters.Add(param);

                // Executando o commando e obtendo o resultado
                reader = comand.ExecuteReader();

                return readSingleComodo(reader);
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

                SqlCommand comand = createCommand(conection, "SELECT idComodo, Nome, idResidencia from COMODO");

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

                SqlCommand comand = createCommand(conection, "DELETE from COMODO where idComodo = @idComodo");

                // Define as informações do parâmetro criado
                SqlParameter param = new SqlParameter("@idComodo", comodo.Id);

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

                SqlCommand comand = createCommand(conection, "UPDATE COMODO Set Nome = @Nome, idResidencia = @idResidencia WHERE idComodo = @idComodo");

                // Define as informações do parâmetro criado
                SqlParameter param = new SqlParameter("@idComodo", comodo.Id);
                comand.Parameters.Add(param);
                SqlParameter param1 = new SqlParameter("@Nome", comodo.Nome);
                comand.Parameters.Add(param1);
                SqlParameter param2 = new SqlParameter("@idResidencia", comodo.ResidenciaId);
                comand.Parameters.Add(param2);

                comand.ExecuteNonQuery();

                return comodo;

            }
            finally
            {
                closeResources(conection, null);
            }

        }

        private Comodo readSingleComodo(SqlDataReader reader)
        {
            Comodo comodo = null;

            if (reader.Read())
            {
                comodo = readComodo(reader);
            }

            return comodo;
        }


        private Comodo readComodo(SqlDataReader reader)
        {
            Comodo comodo = null;


            if (reader != null)
            {
                if (reader.HasRows)
                {
                    comodo = new Comodo();

                    comodo.Id = Convert.ToInt32(reader["idComodo"]);
                    comodo.Nome = (String)reader["Nome"];
                    comodo.ResidenciaId = Convert.ToInt32(reader["idResidencia"]);
                }
            }

            return comodo;
        }

        private List<Comodo> readAllComodo(SqlDataReader reader)
        {

            List<Comodo> comodos = new List<Comodo>();

            if (reader != null)
            {
                while (reader.Read())
                {
                    comodos.Add(readComodo(reader));
                }
            }

            return comodos;

        }
    }
}
