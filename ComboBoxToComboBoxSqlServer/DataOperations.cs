using System;
using System.Data;
using System.Data.SqlClient;
using BaseConnectionLibrary.ConnectionClasses;

namespace ComboBoxToComboBoxSqlServer
{
    public class DataOperations : SqlServerConnection
    {
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
            DataTable dt = new DataTable();

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
            DataTable dt = new DataTable();

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