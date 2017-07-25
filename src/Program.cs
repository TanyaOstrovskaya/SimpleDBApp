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
            DataBaseConnector dbConnector = new DataBaseConnector(connectionString);
            bool res = dbConnector.DeleteFileByName("fddsf");
            Console.WriteLine(res);
            Console.Read();

        }
    }    

}
