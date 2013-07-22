using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using TelerikAcademyDB;
class Program
{
    static void Main()
    {
        TelerikAcademyEntities db = new TelerikAcademyEntities();
        Stopwatch sw = new Stopwatch();
        using (db)
        {
            sw.Start();
            IEnumerable query = db.Employees.ToList()
                 .Select(x => x.Address).ToList()
                 .Select(t => t.Town).ToList()
                 .Where(t => t.Name == "Sofia");
            sw.Stop();
            Console.WriteLine("Slow: {0}", sw.Elapsed);
            // made 644 queries
            sw.Restart();
            
            IEnumerable querySmart = db.Employees
              .Select(x => x.Address)
              .Select(t => t.Town)
              .Where(t => t.Name == "Sofia").ToList();
            sw.Stop();
            Console.WriteLine("Fast: {0}", sw.Elapsed);
           // made 2 queries
        }
    }
}

