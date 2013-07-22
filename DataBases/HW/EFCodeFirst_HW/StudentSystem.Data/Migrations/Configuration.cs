namespace StudentSystem.Data.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    public sealed class Configuration : DbMigrationsConfiguration<StudentSystem.Data.StudentSystemDB>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
            this.AutomaticMigrationDataLossAllowed = true;
        }

        protected override void Seed(StudentSystem.Data.StudentSystemDB context)
        {
            context.Students.AddOrUpdate(
                     s => s.Name,
                    new Student { Name = "Kiro", Number = "11111111"},
                    new Student { Name = "Mitio", Number = "22222222"},
                    new Student { Name = "asdasdadaddddddddd", Number = "33333333" }
                );
            context.Courses.AddOrUpdate(
                c=> c.Name,
                new Course { Name = "C# Part 1", Materials = "http://wtfC#.com" },
                 new Course { Name = "C# Part 2", Materials = "http://wtfC#2.com" },
                  new Course { Name = "PHP", Materials = "http://tutorialPhp.com" }
                );
            context.SaveChanges();

      context.Students
                .Where(x => x.Number == "11111111")
                .First()
                .Courses.Add(context.Courses.Where(c => c.Name == "PHP").First());
     
            context.Students
                .Where(x => x.Number == "11111111")
                .First()
                .Courses.Add(context.Courses.Where(c => c.Name == "PHP").First());

            context.Students
                .Where(x => x.Number == "22222222")
                .First()
                .Courses.Add(context.Courses.Where(c => c.Name == "C# Part 2").First());

            context.Students
                .Where(x => x.Number == "33333333")
                .First()
                .Courses.Add(context.Courses.Where(c => c.Name == "C# Part 1").First());
            context.SaveChanges();
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data. E.g.
            //
            //    context.People.AddOrUpdate(
            //      p => p.FullName,
            //      new Person { FullName = "Andrew Peters" },
            //      new Person { FullName = "Brice Lambson" },
            //      new Person { FullName = "Rowan Miller" }
            //    );
            //
        }
    }
}
