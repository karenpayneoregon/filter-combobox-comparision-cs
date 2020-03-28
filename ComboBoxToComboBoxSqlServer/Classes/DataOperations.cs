using System;
using System.Data;
using System.Data.SqlClient;
using BaseConnectionLibrary.ConnectionClasses;

namespace ComboBoxToComboBoxSqlServer.Classes
{
    public class DataOperations : SqlServerConnection
    {
        /// <summary>
        /// Create database connection string. If your server name
        /// is not .\SQLEXPRESS then the first line of code
        /// below needs to change to your SQL-Server name.
        ///
        /// This also needs to be done in the class NorthWindEntityCore
        /// in OnConfiguring method of the class NorthWindContext
        /// </summary>
        public DataOperations()
        {
            DatabaseServer = ".\\SQLEXPRESS";

            if (Environment.UserName.ToLower().Contains("karen"))
            {
                DatabaseServer = "KARENS-PC";
            }

            DefaultCatalog = "NorthWindAzure";
        }

        public DataTable CategoryDataTable()
        {
            var dt = new DataTable();

            using (var cn = new SqlConnection(ConnectionString))
            {
                using (var cmd = new SqlCommand() {Connection = cn})
                {
                    var selectStatement = 
                        "SELECT CategoryId, CategoryName " + 
                        "FROM Categories ORDER BY CategoryName";

                    cmd.CommandText = selectStatement;
                    cn.Open();
                    dt.Load(cmd.ExecuteReader());
                }
            }

            return dt;
        }
        public DataTable ProductDataTable()
        {
            var dt = new DataTable();

            using (var cn = new SqlConnection(ConnectionString))
            {
                using (var cmd = new SqlCommand() { Connection = cn })
                {
                    var selectStatement = 
                        "SELECT ProductId,CategoryId, ProductName " + 
                        "FROM Products ORDER BY ProductName";

                    cmd.CommandText = selectStatement;
                    cn.Open();
                    dt.Load(cmd.ExecuteReader());
                }
            }

            return dt;
        }
    }
}