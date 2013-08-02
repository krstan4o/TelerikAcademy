using System;
using System.Collections.Generic;
using System.Data.OleDb;
using System.Data;
using System.Linq;

namespace Supermarket.Data
{
    public class ExcelProvider
    {
        public static void ExecuteSqlQueryReturnValue(string query, Dictionary<string, object> parametters, Action<OleDbDataReader> action, string connectionString)
        {
            RunSqlQueryReturnValue(query, parametters, CommandType.Text, action, connectionString);
        }

        private static void RunSqlQueryReturnValue(string queryText, Dictionary<string, object> parametters, CommandType commandType, Action<OleDbDataReader> action, string connectionString)
        {
            using (OleDbConnection oConn = new OleDbConnection(connectionString))
            {
                using (OleDbCommand oRS = oConn.CreateCommand())
                {
                    oConn.Open();
                    oRS.CommandType = commandType;
                    oRS.CommandText = queryText;
                    if (parametters != null && parametters.Count > 0)
                    {
                        foreach (var parametter in parametters)
                        {
                            oRS.Parameters.AddWithValue(parametter.Key, parametter.Value);
                        }
                    }

                    OleDbDataReader rdr = oRS.ExecuteReader(CommandBehavior.CloseConnection);
                    if (action != null)
                    {
                        action(rdr);
                    }
                }
            }
        }
    }
}
