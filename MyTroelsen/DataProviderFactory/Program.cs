using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using System.Data;
using System.Data.Common;
using static System.Console;

namespace DataProviderFactory
{
    class Program
    {
        static void Main(string[] args)
        {
            WriteLine("Fun with data provider factories");

            string dataProvider = ConfigurationManager.AppSettings["provider"];

            string connectionString = ConfigurationManager.ConnectionStrings["AutoLotSqlDataProvider"].ConnectionString;

            DbProviderFactory dbFactory = DbProviderFactories.GetFactory(dataProvider);

            using(DbConnection connection = dbFactory.CreateConnection())
            {
                if (connection == null)
                {
                    ShowError("Connection");
                    return;
                }

                WriteLine($"Your connection object is {connection.GetType().Name}");
                connection.ConnectionString = connectionString;
                connection.Open();


                DbCommand dbCommand = dbFactory.CreateCommand();

                if(dbCommand == null)
                {
                    ShowError("Command");
                    return;
                }

                WriteLine($"Your command object is {dbCommand.GetType().Name}");

                dbCommand.Connection = connection;
                dbCommand.CommandText = "SELECT * FROM Inventory";

                using (DbDataReader dbReader = dbCommand.ExecuteReader())
                {
                    WriteLine($"Your data reader object is {dbReader.GetType().Name}");

                    while (dbReader.Read())
                    {
                        WriteLine($"-> Car #{dbReader["CarId"]} is a {dbReader["Make"]}.");
                    }
                }
               
                
            }

            ReadLine();
        }

        private static void ShowError(string objectName)
        {
            WriteLine($"There was an issue creating the {objectName}");

            ReadLine();
        }
    }
}
