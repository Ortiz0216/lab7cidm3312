using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace lab7cidm3312
{
    class Program
    {
        static void List()
        {
            using (var db = new AppDbContext())
            {
                var allStuff = db.Courses.Include(p => p.StudentCourses).ThenInclude(ep => ep.Student);

                foreach (var course in allStuff)
                {
                    Console.WriteLine($"{course.CourseName} -");
                    foreach (var student in course.StudentCourses)
                    {
                        Console.WriteLine($"\t{student.Student.FirstName} {student.Student.LastName} {student.GPA}");
                    }
                    Console.WriteLine();
                }
            }
        }
        static void Main(string[] args)
        {
            using (var db = new AppDbContext())
            {
                db.Database.EnsureDeleted();
                db.Database.EnsureCreated();
                
                List<Course> courses = new List<Course>() 
                {
                    new Course {CourseName = "Not enrolled in course"},
                    new Course {CourseName = "Managment 101"},
                    new Course {CourseName = "Statistics 101"},
                };


                List<Student> students = new List<Student>() {
                    new Student {FirstName = "Daniel", LastName = "Ortiz"},
                    new Student {FirstName = "John", LastName = "Katumo"},
                    new Student {FirstName = "Austin", LastName = "Anderson"},
                    new Student {FirstName = "Nathan", LastName = "Clayton"},
                    new Student {FirstName = "Transer Student: Jose ", LastName = "Morales"},
                };

                
                List<StudentCourse> joinTable = new List<StudentCourse>() {
                    new StudentCourse {Student = students[0], Course = courses[1], GPA = 97.2},
                    new StudentCourse {Student = students[1], Course = courses[2], GPA = 90.6},
                    new StudentCourse {Student = students[2], Course = courses[2], GPA = 75.01},
                    new StudentCourse {Student = students[3], Course = courses[2], GPA = 86.3},
                    new StudentCourse {Student = students[1], Course = courses[1], GPA = 68.61},
                    new StudentCourse {Student = students[3], Course = courses[1], GPA = 99.81},
                };

                db.AddRange(courses);
                db.AddRange(students);
                db.AddRange(joinTable);
                db.SaveChanges();
            }
            List();

            using (var db = new AppDbContext())
            {
                // Move Employee "C" from Project 1 to Project 2
                // Here's what you need
                // Employee id of employee moving
                // Project id of OLD project
                // Project id of NEW project
                int studentId = 5;
                int courseId = 1;


                StudentCourse scToRemove = db.StudentCourses.Find(studentId, courseId);
                Student s = db.Students.Find(studentId);
                Course c = db.Courses.Find(courseId);

                db.Remove(scToRemove);
                db.SaveChanges();
            }
            using (var db = new AppDbContext())
            {
                StudentCourse scToAdd = new StudentCourse {
                    Student = db.Students.Find(4), // This is Employee 4
                    Course = db.Courses.Find(2), // This is Project 2
                    GPA = 85.06,
                };
                db.Add(scToAdd);
                db.SaveChanges();
            }
            List();
        }
    }
}
