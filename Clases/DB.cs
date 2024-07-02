using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;

namespace WindowsFormsApp1.Clases
{
    internal class DB
    {
        private string connectionString;

        public DB()
        {
            // Configurar la cadena de conexión
            connectionString = "Server=DESKTOP-8B8IO0R\\SQLEXPRESS;Database=BASEDATOS;User Id=IamMAu;Password=123456789;";
        }

        // Método para abrir una conexión
        public SqlConnection OpenConnection()
        {
            SqlConnection connection = new SqlConnection(connectionString);
            try
            {
                connection.Open();
                Console.WriteLine("Conexión abierta exitosamente.");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error al abrir la conexión: " + ex.Message);
            }
            return connection;
        }

        // Método para cerrar una conexión
        public void CloseConnection(SqlConnection connection)
        {
            try
            {
                if (connection.State == ConnectionState.Open)
                {
                    connection.Close();
                    Console.WriteLine("Conexión cerrada exitosamente.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error al cerrar la conexión: " + ex.Message);
            }
        }

        // Método de ejemplo para ejecutar una consulta
        public DataTable ExecuteQuery(string query)
        {
            DataTable dataTable = new DataTable();
            using (SqlConnection connection = OpenConnection())
            {
                try
                {
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        using (SqlDataAdapter adapter = new SqlDataAdapter(command))
                        {
                            adapter.Fill(dataTable);
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error al ejecutar la consulta: " + ex.Message);
                }
                finally
                {
                    CloseConnection(connection);
                }
            }
            return dataTable;
        }

        // Método de ejemplo para ejecutar un comando que no devuelve resultados (INSERT, UPDATE, DELETE)
        public void ExecuteNonQuery(string query)
        {
            using (SqlConnection connection = OpenConnection())
            {
                try
                {
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.ExecuteNonQuery();
                        Console.WriteLine("Comando ejecutado exitosamente.");
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error al ejecutar el comando: " + ex.Message);
                }
                finally
                {
                    CloseConnection(connection);
                }
            }
        }

        // Método para guardar datos en una tabla
        public bool Save(string tablename, string campos, string valores)
        {
            bool resp = false;
            string query = $"INSERT INTO {tablename}({campos})VALUES({valores})";

            try
            {
                using (SqlCommand com = new SqlCommand(query, OpenConnection()))
                {
                    Int64 ri = com.ExecuteNonQuery();
                    if (ri > 0)
                    {
                        resp = true;
                    }
                }
            }
            catch (SqlException error)
            {
                Console.WriteLine("Error al ejecutar el comando: " + error.Message);
            }

            return resp;
        }
    }
}
