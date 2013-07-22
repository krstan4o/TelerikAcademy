using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using StudentSystem.Data;
using System.Data.Entity;

namespace StudentSystem.Client
{
    public static class StudentSystem
    {

        public static void AddStudent(string name, string number) 
        {
            StudentSystemDB db = new StudentSystemDB();
            using (db)
            {
                Student newStudent = new Student();
                newStudent.Name = name;
                newStudent.Number = number;
               
                db.Students.Add(newStudent);
                db.SaveChanges();
            }
        }

        public static void UpdateStudentByNumber(string oldStudentNumber, string newName, string newNumber)
        {
            StudentSystemDB db = new StudentSystemDB();
            using (db)
            {
                var studentToUpdate = db.Students.First(x => x.Number == oldStudentNumber);
                studentToUpdate.Number = newNumber;
                studentToUpdate.Name = newName;
                db.SaveChanges();
            }
        }

        public static void UpdateStudentByStudentId(int studentId, string newName, string newNumber)
        {
            StudentSystemDB db = new StudentSystemDB();
            using (db)
            {
                var studentToUpdate = db.Students.First(x => x.StudentId == studentId);
                studentToUpdate.Number = newNumber;
                studentToUpdate.Name = newName;
                db.SaveChanges();
            }
        }

        public static void DeleteStudentByNumber(string studentNumber)
        {
            StudentSystemDB db = new StudentSystemDB();
            using (db)
            {
                var student = db.Students.First(x => x.Number == studentNumber);
                db.Students.Remove(student);
                db.SaveChanges();
            }
        }

        public static void DeleteStudentById(int Id)
        {
            StudentSystemDB db = new StudentSystemDB();
            using (db)
            {
                var student = db.Students.First(x => x.StudentId == Id);
                db.Students.Remove(student);
                db.SaveChanges();
            }
        }

        public static void AddCourse(string name, string materials)
        {
            StudentSystemDB db = new StudentSystemDB();
            using (db)
            {

                var course = new Course();
                course.Name = name;
                course.Materials = materials;
                db.Courses.Add(course);
                db.SaveChanges();
            }
        }

        public static void UpdateCourse(int id, string newName, string newMaterials)
        {
            StudentSystemDB db = new StudentSystemDB();
            using (db)
            {
                var course = db.Courses.First(x => x.CoursesId == id);
                course.Materials = newMaterials;
                course.Name = newName;
                db.SaveChanges();
            }
        }

        public static void DeleteCourseById(int id)
        {
            StudentSystemDB db = new StudentSystemDB();
            using (db)
            {
                var course = db.Courses.First(x => x.CoursesId == id);
                db.Courses.Remove(course);
                db.SaveChanges();
            }
        }

        public static void DeleteCourseByName(string name)
        {
            StudentSystemDB db = new StudentSystemDB();
            using (db)
            {
                var course = db.Courses.First(x => x.Name == name);
                db.Courses.Remove(course);
                db.SaveChanges();
            }
        }

        public static void AddStudentToCourse(int courseId, int studentId) 
        {
            StudentSystemDB db = new StudentSystemDB();
            using (db)
            {
               db.Students.First(x => x.StudentId == studentId).
                    Courses.Add(db.Courses.First(x => x.CoursesId == courseId));
             
                db.SaveChanges();
            }
        }

        public static void DeleteStudentFromCourse(int courseId, int studentId)
        {
            StudentSystemDB db = new StudentSystemDB();
            using (db)
            {
                db.Students.First(x => x.StudentId == studentId).
                     Courses.Remove(db.Courses.First(x => x.CoursesId == courseId));

                db.SaveChanges();
            }
        }

        public static void AddHomeWork(int courseId, int studentId, string content)
        {
            StudentSystemDB db = new StudentSystemDB();
            using (db)
            {
              
                db.Homeworks.Add(new Homework() { 
                    Content = content,
                    Student = db.Students.First(x => x.StudentId == studentId),
                    Course = db.Courses.First(x=> x.CoursesId == courseId),
                    TimeSend = DateTime.Now
                });

                db.SaveChanges();
            }
        }

        public static void UpdateHomeWork(int homeworkId, string newContent)
        {
            StudentSystemDB db = new StudentSystemDB();
            using (db)
            {

                var homework = db.Homeworks.First(x => x.HomeworkId == homeworkId);
                homework.Content = newContent;
                db.SaveChanges();
            }
        }

        public static void DeleteHomeWork(int homeworkId)
        {
            StudentSystemDB db = new StudentSystemDB();
            using (db)
            {

                db.Homeworks.Remove(db.Homeworks.First(x => x.HomeworkId == homeworkId));
                db.SaveChanges();
            }
        }
    }
}
