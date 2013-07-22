using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using TelerikAcademyDB;
class Program
{
    static void Main()
    {
        Database db = new Database();
        using (db)
        {
            var employees = db.Employees;
            Stopwatch sw = new Stopwatch();
            Console.WriteLine("EmployeeName | EmployeeDepartment | EmployeeTown");
            sw.Start();
            foreach (var item in employees)
            {
                Console.WriteLine("{0} | {1} | {2}", item.FirstName, item.Department.Name, item.Address.Town.Name);
            }
            sw.Stop();
            Console.WriteLine();
            Console.WriteLine("Time with problem N+1: {0}", sw.Elapsed);
            Console.WriteLine("338 queryes with proffiler");
            Console.WriteLine("------------------------------------------------------------");
            sw.Reset();
            sw.Start();
            foreach (var item in employees.Include("Department").Include("Address.Town"))
            {
                Console.WriteLine("{0} | {1} | {2}", item.FirstName, item.Department.Name, item.Address.Town.Name);
            }
            sw.Stop();
            Console.WriteLine("Time withouth problem N+1: {0}", sw.Elapsed);
            Console.WriteLine("1 queryes with proffiler");
        }
        
    }
}

