using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Northwind.Data;
    static class TestingCustomers
    {
        static void Main()
        {
           NorthwindDB.InsertCustomer("XXXXX","FU OOD", "Ivan Ivanov", "Owner", "Sofiq Mladost 1", "Sofia", "BG", "1700", "Bulgaria", "030-0076545", "030-0076545");
           //  NorthwindDB.ModifyCustomer("XXXXX", "FU asdasda", "blaAAA blaAA", "ASAS", "Sofiq Mladost 1", "Sofia", "BG", "1700", "Bulgaria", "030-0076545", "030-0076545");
           // NorthwindDB.DeleteCustomer("XXXXX");

        }
    }

