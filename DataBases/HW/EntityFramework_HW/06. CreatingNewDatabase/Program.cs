using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using Northwind.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
namespace _06.CreatingNewDatabase
{
    class Program
    {
        static void Main()
        {




            StringBuilder dbScript = new StringBuilder();
            dbScript.Append("USE NorthwindTwin ");
            NorthwindEntities objectContext = new NorthwindEntities();
            string generatedScript =
                ((IObjectContextAdapter)objectContext).ObjectContext.CreateDatabaseScript();
            dbScript.Append(generatedScript);
            objectContext.Database.ExecuteSqlCommand("CREATE DATABASE NorthwindTwin");
            objectContext.Database.ExecuteSqlCommand(dbScript.ToString());



        }
    }
}
