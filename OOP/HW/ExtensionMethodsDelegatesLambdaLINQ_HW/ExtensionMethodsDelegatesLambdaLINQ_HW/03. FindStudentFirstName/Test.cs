using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _03.FindStudentFirstName
{
    class Test
    {
        static void Main()
        {
            Student[] students =
            {new Student("Pesho","Ivanov",28),
            new Student("Gosho","Ivanov",18),
            new Student("Pesho","Angelov",22)};

            var findFirstName =
                from student in students
                where student.FName.CompareTo(student.LName)==-1
                select student;

            Console.WriteLine("Finds all students that first name is before the secound:");
            foreach (var item in findFirstName)
            {
                Console.WriteLine(item);
            }

            var findStudentByAge =
               from student in students
               where student.Age >= 18 && student.Age <= 25
               select student;

            Console.WriteLine("Finds all students betwean age 18 and 25:");
            foreach (var item in findStudentByAge)
            {
                Console.WriteLine(item);
            }

            var sortStudentsByName = students.OrderByDescending(x => x.FName).ThenByDescending(x => x.LName);

            Console.WriteLine("Students ordered by first name and then last name in DESCENDING:");
            foreach (var item in sortStudentsByName)
            {
                Console.WriteLine(item);
            }

            var sortStudentsByNameLINQ =
                from student in students
                orderby student.FName descending, student.LName descending
                select student;

            Console.WriteLine("The same like the above but using LINQ:");
            foreach (var item in sortStudentsByNameLINQ)
            {
                Console.WriteLine(item);
            }
        }

    }
}

