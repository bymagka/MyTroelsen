using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace AutoLotDAL.BulkImport
{
    public static class ProcessBulkImport
    {
        private static SqlConnection sqlConn= null;
        private static string connectionString = @"Data Source=DESKTOP-A4S5T9U\SQLEXPRESS;Initial Catalog=AutoLot;Integrated Security=True;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";

        private static void OpenConnection()
        {
            sqlConn = new SqlConnection(connectionString);
            sqlConn.Open();
        }

        private static void CloseConnection()
        {
            if (sqlConn?.State != ConnectionState.Closed)
            {
                sqlConn?.Close();
            }
        }

        public static void ExecuteBulkImport<T>(IEnumerable<T> records,string TableName)
        {
            OpenConnection();

            using (SqlConnection conn = sqlConn)
            {
                SqlBulkCopy sqlBulk = new SqlBulkCopy(conn) { DestinationTableName = TableName };

                var dataReader =new MyDataReader<T>(records.ToList());

                try
                {
                    sqlBulk.WriteToServer(dataReader);
                }
                catch (Exception)
                {

                    throw;
                }

                finally
                {
                    CloseConnection();
                }
            }
        }
    }
}
