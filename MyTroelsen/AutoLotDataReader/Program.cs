using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using static System.Console;

namespace AutoLotDataReader
{
    class Program
    {
        static void Main(string[] args)
        {

            WriteLine("SQL Data Reader");


            SqlConnectionStringBuilder sqlStringBuilder = new SqlConnectionStringBuilder
            {
                DataSource = @"DESKTOP-A4S5T9U\SQLEXPRESS",
                InitialCatalog = "AutoLot",
                ConnectTimeout = 30,
                IntegratedSecurity = true
            };

            using (SqlConnection conn = new SqlConnection())
            {
                string connectionString = 

                conn.ConnectionString = sqlStringBuilder.ConnectionString;
               
                conn.Open();

                ShowConnectionInfo(conn);

                SqlCommand commandReader = new SqlCommand();
                commandReader.Connection = conn;

                commandReader.CommandText = "Select * From Inventory; SELECT * FROM Customers";



                using(SqlDataReader sqlReader = commandReader.ExecuteReader())
                {
                    do
                    {
                        while (sqlReader.Read())
                        {
                            WriteLine("*****Record******");

                            for (int i = 0; i < sqlReader.FieldCount; i++)
                            {
                                WriteLine($"{sqlReader.GetName(i)}: {sqlReader[i]}");
                            }


                        }
                    } while (sqlReader.NextResult());
                }
            }
            ReadLine();
        }

        private static void ShowConnectionInfo(SqlConnection conn)
        {
            WriteLine($"Data source: {conn.DataSource}");
            WriteLine($"Database: {conn.Database}");
            WriteLine($"Connection timeout: {conn.ConnectionTimeout}");
            WriteLine($"State: {conn.State}");
        }
    }
}
