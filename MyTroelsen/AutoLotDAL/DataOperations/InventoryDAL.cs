using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using System.Data.Common;
using AutoLotDAL.Models;

namespace AutoLotDAL.DataOperations
{
    public class InventoryDAL
    {
        private readonly string _connectionString;

        private DbProviderFactory dbProviderFactory;

        private IDbConnection dbConnection = null;

        public InventoryDAL() : this(@"Data Source=DESKTOP-A4S5T9U\SQLEXPRESS;Initial Catalog=AutoLot;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False", "System.Data.SqlClient")
        {
        }

        public InventoryDAL(string connectionString,string provider)
        {
            _connectionString = connectionString;
            dbProviderFactory = DbProviderFactories.GetFactory(provider);
        }
 
        private void OpenConnection()
        {
            dbConnection = dbProviderFactory.CreateConnection();
            dbConnection.ConnectionString = _connectionString;
            dbConnection.Open();
        }

        private void CloseConnection()
        {
             if(dbConnection?.State != ConnectionState.Closed)
            {
                dbConnection?.Close();
            }
        }

        public List<Car> GetAllInventory()
        {
            List<Car> cars = new List<Car>();

            string sqlQuery = "Select * FROM Inventory";
            OpenConnection();

            using (IDbCommand selectCommand = dbProviderFactory.CreateCommand())
            {
                selectCommand.CommandText = sqlQuery;
                selectCommand.Connection = dbConnection;
                selectCommand.CommandType = CommandType.Text;

                IDataReader dataReader = selectCommand.ExecuteReader(CommandBehavior.CloseConnection);

                while (dataReader.Read())
                {
                    cars.Add(new Car { CarId = (int)dataReader["CarId"], Color = (string)dataReader["Color"], Make = dataReader["Make"].ToString(), PetName = dataReader["PetName"].ToString() });
                }

                dataReader.Close();
            }

            return cars;
        }

        public Car GetCarById(int id)
        {
            OpenConnection();

            Car car = null;

            string sqlQuery = $"SELECT * FROM Inventory WHERE CarId = {id}";

            using (IDbCommand selectCommand = dbProviderFactory.CreateCommand())
            {
                selectCommand.CommandText = sqlQuery;
                selectCommand.Connection = dbConnection;
                selectCommand.CommandType = CommandType.Text;

                IDataReader dataReader = selectCommand.ExecuteReader(CommandBehavior.CloseConnection);

                while (dataReader.Read())
                {
                    car = new Car { CarId = (int)dataReader["CarId"], Color = (string)dataReader["Color"], Make = dataReader["Make"].ToString(), PetName = dataReader["PetName"].ToString() };
                }

                dataReader.Close();
            }

            return car;

        }


        public void InsertAuto(Car car)
        {
            OpenConnection();

            string query = $"INSERT INTO Inventory (Make,Color,PetName) Values(@Make,@Color,@PetName)";

            using (IDbCommand dbCommand = dbProviderFactory.CreateCommand())
            {
                dbCommand.CommandText = query;
                dbCommand.Connection = dbConnection;
                dbCommand.CommandType = CommandType.Text;


                IDbDataParameter makeParameter = dbProviderFactory.CreateParameter();

                makeParameter.ParameterName = "@Make";
                makeParameter.DbType = DbType.String;
                makeParameter.Size = 10;
                makeParameter.Value = car.Make;

                dbCommand.Parameters.Add(makeParameter);



                IDbDataParameter colorParameter = dbProviderFactory.CreateParameter();

                colorParameter.ParameterName = "@Color";
                colorParameter.DbType = DbType.String;
                colorParameter.Size = 10;
                colorParameter.Value = car.Color;

                dbCommand.Parameters.Add(colorParameter);



                IDbDataParameter petnameParameter = dbProviderFactory.CreateParameter();

                petnameParameter.ParameterName = "@PetName";
                petnameParameter.DbType = DbType.String;
                petnameParameter.Size = 10;
                petnameParameter.Value = car.PetName;

                dbCommand.Parameters.Add(petnameParameter);



                dbCommand.ExecuteNonQuery();
            }

            CloseConnection();
        }

        public void DeleteeAuto(Car car)
        {
            OpenConnection();

            string query = $"DELETE FROM Inventory WHERE CarId = '{car.CarId}'";

            using (IDbCommand dbCommand = dbProviderFactory.CreateCommand())
            {
                 try
                {
                    dbCommand.CommandText = query;
                    dbCommand.Connection = dbConnection;
                    dbCommand.CommandType = CommandType.Text;
                    dbCommand.ExecuteNonQuery();
                }
                catch (SqlException ex)
                {
                    Exception error = new Exception("Sorry! That car is in the order!", ex);
                    throw error;
                }             
            }
            CloseConnection();
        }

        public void UpdateAutoPetName(Car car)
        {
            OpenConnection();

            string query = $"UPDATE Inventory SET PetName = '{car.PetName}' WHERE CarId = '{car.CarId}'";

            using (IDbCommand dbCommand = dbProviderFactory.CreateCommand())
            {

                    dbCommand.CommandText = query;
                    dbCommand.Connection = dbConnection;
                    dbCommand.CommandType = CommandType.Text;
                    dbCommand.ExecuteNonQuery();
              
            }
            CloseConnection();
        }

        public void LookUpPetName(Car car)
        {
            OpenConnection();

            string carPetName;


            using (IDbCommand dbCommand = dbProviderFactory.CreateCommand())
            {
                dbCommand.CommandText = "GetPetName";
                dbCommand.Connection = dbConnection;
                dbCommand.CommandType = CommandType.StoredProcedure;


                IDbDataParameter caridParameter = dbProviderFactory.CreateParameter();

                caridParameter.ParameterName = "@CarId";
                caridParameter.DbType = DbType.Int32;
                caridParameter.Size = 10;
                caridParameter.Value = car.CarId;
                caridParameter.Direction = ParameterDirection.Input;

                dbCommand.Parameters.Add(caridParameter);

                IDbDataParameter petnameParameter = dbProviderFactory.CreateParameter();

                petnameParameter.ParameterName = "@petName";
                petnameParameter.DbType = DbType.String;
                petnameParameter.Size = 10;
                petnameParameter.Direction = ParameterDirection.Output;

                dbCommand.Parameters.Add(petnameParameter);

                dbCommand.ExecuteNonQuery();

                carPetName = ((IDbDataParameter)dbCommand.Parameters["@petName"]).Value.ToString();
            }

            CloseConnection();
        }
   
        public void ProcessCreditRisk(bool throwEx,int custId)
        {
            OpenConnection();

            string fName;
            string lName;

            IDbCommand cmdSelect = dbProviderFactory.CreateCommand();

            cmdSelect.CommandText = $"SELECT * FROM Customers WHERE CustID = '{custId}'";
            cmdSelect.Connection = dbConnection;

            using(IDataReader dataReader = cmdSelect.ExecuteReader())
            {
                if (dataReader.Read())
                {
                    fName = (string)dataReader["FirstName"];
                    lName = (string)dataReader["LastName"];
                    
                }
                else
                {
                    CloseConnection();
                    return;
                }
            }


            IDbCommand cmdRemove = dbProviderFactory.CreateCommand();
            cmdRemove.CommandText = $"Delete FROM Customers WHERE CustID = '{custId}'";
            cmdRemove.Connection = dbConnection;

            IDbCommand cmdInsert = dbProviderFactory.CreateCommand();
            cmdInsert.CommandText = $"INSERT Into CreditRisks(FirstName,LastName) Values('{fName}','{lName}')";
            cmdInsert.Connection = dbConnection;

            IDbTransaction dbTransaction = null;


            try
            {
                dbTransaction = dbConnection.BeginTransaction();

                cmdRemove.Transaction = dbTransaction;
                cmdInsert.Transaction = dbTransaction;

                cmdInsert.ExecuteNonQuery();
                cmdRemove.ExecuteNonQuery();

                if (throwEx)
                {
                    throw new Exception("Sorry! Database error! Tx failed");
                }

                dbTransaction.Commit();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                dbTransaction?.Rollback();
            }
            finally
            {
                CloseConnection();
            }

        }
    }
}
