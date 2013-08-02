using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using Supermarket.Data;
using System.Data.SqlClient;
using System.Data;

namespace Supermarket.ConsoleClient
{
    public class Program
    {
        static void Main()
        {
            CopyMysqlToSql();
        }

        private static void CopyMysqlToSql()
        {
            DeleteData();
            CopyVendors();
            CopyMesures();
            CopyProducts();
        }
        private static void CopyVendors()
        {
            string query = "SELECT * FROM vendors";
            MySqlProvider.ExecuteSqlQueryReturnValue(query, null, delegate(MySqlDataReader reader)
            {
                while (reader.Read())
                {
                    string sqlQuery = "INSERT INTO Vendors";

                    Dictionary<string, object> parametters = new Dictionary<string, object>();
                    parametters.Add("Id", reader.GetValue(0));
                    parametters.Add("VendorName", reader.GetValue(1));

                    SqlProvider.ExecuteSqlQueryInsert(sqlQuery, parametters, null);
                }
            });
        }

        private static void CopyMesures()
        {
            string query = "SELECT * FROM measures";
            MySqlProvider.ExecuteSqlQueryReturnValue(query, null, delegate(MySqlDataReader reader)
            {
                while (reader.Read())
                {
                    string sqlQuery = "INSERT INTO Measures";

                    Dictionary<string, object> parametters = new Dictionary<string, object>();
                    parametters.Add("Id", reader.GetValue(0));
                    parametters.Add("MeasureName", reader.GetValue(1));

                    SqlProvider.ExecuteSqlQueryInsert(sqlQuery, parametters, null);
                }
            });
        }

        private static void CopyProducts()
        {
            string query = "SELECT * FROM products";
            MySqlProvider.ExecuteSqlQueryReturnValue(query, null, delegate(MySqlDataReader reader)
            {
                while (reader.Read())
                {
                    string sqlQuery = "INSERT INTO Products";

                    Dictionary<string, object> parametters = new Dictionary<string, object>();
                    parametters.Add("Id", reader.GetValue(0));
                    parametters.Add("ProductName", reader.GetValue(1));
                    parametters.Add("VendorId", reader.GetValue(3));
                    parametters.Add("MeasureId", reader.GetValue(2));
                    parametters.Add("BasePrise", reader.GetValue(4));

                    SqlProvider.ExecuteSqlQueryInsert(sqlQuery, parametters, null);
                }
            });
        }
        private static void DeleteData()
        {
            string query = "DELETE FROM Products";
            SqlProvider.ExecuteSqlQueryDelete(query);
            query = "DELETE FROM Measures";
            SqlProvider.ExecuteSqlQueryDelete(query); 
            query = "DELETE FROM Vendors";
            SqlProvider.ExecuteSqlQueryDelete(query);
        }
    }
}
