using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication2
{
    class DataBaseConnector : IDataBaseConnect
    {
        public string connectionString { get; private set; }

        public DataBaseConnector (string connectionString)
        {
            this.connectionString = connectionString;
            
        }
        public DataBaseConnector() { }     

        public bool DeleteFileByName(string name)
        {
            string sqlExpression = "sp_DeleteFile";

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


                return command.ExecuteNonQuery() > 0;
            }

        }

        public List<FileEntry> GetAllFilesInformation()
        {          
            string sqlExpression = "sp_GetAllFiles";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                List<FileEntry> resultList = new List<FileEntry>();

                connection.Open();
                SqlCommand command = new SqlCommand(sqlExpression, connection);             
                command.CommandType = System.Data.CommandType.StoredProcedure;
                var reader = command.ExecuteReader();

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        int id = reader.GetInt32(0);
                        string name = reader.GetString(1);
                        string content = reader.GetString(2);
                        resultList.Add(new FileEntry(id, name, content));                        
                    }
                }
                else
                {
                    resultList = null;
                }

                reader.Close();

                return resultList;
            }
        }


        public List<string> GetFileNamesList()
        {
            string sqlExpression = "sp_GetNamesList";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                List<string> resultList = new List<string>();

                connection.Open();
                SqlCommand command = new SqlCommand(sqlExpression, connection);
                command.CommandType = System.Data.CommandType.StoredProcedure;
                var reader = command.ExecuteReader();

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        resultList.Add(reader.GetString(0));
                    }
                }
                else
                {
                    resultList = null;
                }

                reader.Close();

                return resultList;
            }
        }

        public int InsertNewFile(string name, string content)
        {
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

                var result = command.ExecuteScalar();

                return Int32.Parse(result.ToString()); // new file id
            }

        }

        public string ReadFileContent(string name)
        {
            string sqlExpression = "sp_GetFileContent";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string result = null;

                connection.Open();
                SqlCommand command = new SqlCommand(sqlExpression, connection);
                command.CommandType = System.Data.CommandType.StoredProcedure;
                SqlParameter nameParam = new SqlParameter
                {
                    ParameterName = "@fileName",
                    Value = name
                };
                command.Parameters.Add(nameParam);
                var reader = command.ExecuteReader();

                if (reader.HasRows)
                {
                    reader.Read();
                    result = reader.GetString(0);
                }
                else
                {
                    result = null;
                }

                reader.Close();

                return result;
            }

        }

        public bool UpdateFileContent(string name, string newContent)
        {
            string sqlExpression = "sp_UpdateFile";

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
                    Value = newContent
                };
                command.Parameters.Add(contentParam);

                return command.ExecuteNonQuery() > 0;
            }
        }
    }
}
