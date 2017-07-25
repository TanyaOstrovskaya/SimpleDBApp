using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication2
{
    class Program
    {
        static string connectionString = @"Data Source=ASUS\MSSQL;Initial Catalog=FilesDB;Integrated Security=True";

        static void Main(string[] args)
        {         

            Console.Write("Введите имя файла:");
            string name = Console.ReadLine();

            Console.Write("Введите содержимое:");
            string content = Console.ReadLine();

            AddFile(10, name, content);
            Console.WriteLine();
            GetAllFiles();

            Console.Read();

        }

     

        private static void GetAllFiles()
        {
            // [sp_GetAllFiles]
            // название процедуры
            string sqlExpression = "sp_GetAllFiles";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand(sqlExpression, connection);
                // указываем, что команда представляет хранимую процедуру
                command.CommandType = System.Data.CommandType.StoredProcedure;
                var reader = command.ExecuteReader();

                if (reader.HasRows)
                {
                    Console.WriteLine("{0}\t{1}\t{2}", reader.GetName(0), reader.GetName(1), reader.GetName(2));

                    while (reader.Read())
                    {
                        int id = reader.GetInt32(0);
                        string name = reader.GetString(1);
                        string content = reader.GetString(2);
                        Console.WriteLine("{0} \t{1} \t\t{2}", id, name, content);
                    }
                }
                reader.Close();
            }           
        }

        private static void AddFile(int id, string name, string content)
        {
            // [sp_InsertFile]
            string sqlExpression = "sp_InsertFile";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand(sqlExpression, connection);
                command.CommandType = System.Data.CommandType.StoredProcedure;

               
              
                SqlParameter nameParam = new SqlParameter
                {
                    ParameterName = "@fileName",
                    Value = name
                };
                command.Parameters.Add(nameParam);

                SqlParameter contentParam = new SqlParameter
                {
                    ParameterName = "@fileContent",
                    Value = content
                };  
                command.Parameters.Add(contentParam);

                var res = command.ExecuteNonQuery();

            }
            
        }
    }
}
