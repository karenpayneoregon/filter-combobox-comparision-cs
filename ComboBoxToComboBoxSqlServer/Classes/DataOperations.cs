using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
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

                    var list = CategoriesWithoutProducts();
                    if (list.Count >0)
                    {
                        foreach (var id in list)
                        {
                            
                            var row = dt.AsEnumerable().FirstOrDefault(dr => 
                                dr.Field<int>("CategoryId") == id);

                            if (row != null) dt.Rows.Remove(row);

                        }
                        
                    }
                }
            }

            return dt;
        }

        /// <summary>
        /// Get categories without products
        /// </summary>
        /// <returns></returns>
        private List<int> CategoriesWithoutProducts()
        {
            List<int> list = new List<int>();
            using (var cn = new SqlConnection(ConnectionString))
            {
                using (var cmd = new SqlCommand() { Connection = cn })
                {
                    cmd.CommandText = 
                        @"SELECT CategoryID 
                          FROM dbo.Categories 
                          WHERE CategoryID NOT IN(SELECT CategoryID FROM  dbo.Products) ";

                    cn.Open();
                    var reader = cmd.ExecuteReader();
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            list.Add(reader.GetInt32(0));
                        }
                    }
                }
                
            }

            return list;
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