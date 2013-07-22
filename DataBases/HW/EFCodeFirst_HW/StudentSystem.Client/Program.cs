
using System;
using System.Data.Entity.Migrations;
using System.Data.Entity;
using StudentSystem.Data;
using System.Linq;
using StudentSystem.Data.Migrations;
namespace StudentSystem.Client
{
    class Program
    {
        static void Main()
        {
            Database.SetInitializer(new MigrateDatabaseToLatestVersion<StudentSystemDB, Configuration>());
            string name = "Kycsii";
            string number = "222";
           StudentSystem.AddStudent(name, number);

           

           StudentSystem.AddHomeWork(1, 2, "Nemam domashno nema vreme");
           StudentSystem.UpdateHomeWork(1, "alelelele");

        }
    }
}
