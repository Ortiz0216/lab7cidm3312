using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace lab7cidm3312
{
    public class AppDbContext: DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Data Source=database.db");
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<StudentCourse>()
                .HasKey(e => new {e.StudentId, e.CourseId});
        }
        public DbSet<Student> Students {get; set;}
        public DbSet<Course> Courses {get; set;}
        public DbSet<StudentCourse> StudentCourses {get; set;}
    }
    public class Student
    {
        public int StudentId {get; set;}
        public string FirstName {get; set;}
        public string LastName {get; set;}
        public List<StudentCourse> StudentCourses {get; set;} // Navigation Property. Student can have MANY StudentCourses
    }

    public class Course
    {
        public int CourseId {get; set;}
        public string CourseName {get; set;}
        public List<StudentCourse> StudentCourses {get; set;} // Navigation Property. Course can have MANY StudentCourses
    }

    public class StudentCourse
    {
        public int StudentId {get; set;} // Composite Primary Key, Foreign Key 1
        public int CourseId {get; set;} // Composite Primary Key, Foreign Key 2
        public Student Student {get; set;} // Navigation Property. One student per StudentCourse
        public Course Course {get; set;} // Navigation Property. One course per StudentCourse
        public double GPA {get; set;}
    }
}