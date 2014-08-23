using System.Collections.Generic;
using UniversityManagementSystemApp.Models;

namespace UniversityManagementSystemApp.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<UniversityManagementSystemApp.Models.UniversityDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
        }

        protected override void Seed(UniversityManagementSystemApp.Models.UniversityDbContext context)
        {
            //new List<Semester>
            //{
            //    new Semester{SemesterName = "1st Semester"},
            //    new Semester{SemesterName = "2nd Semester"},
            //    new Semester{SemesterName = "3rd Semester"},
            //    new Semester{SemesterName = "4th Semester"},
            //    new Semester{SemesterName = "5th Semester"},
            //    new Semester{SemesterName = "6th Semester"},
            //    new Semester{SemesterName = "7th Semester"},
            //    new Semester{SemesterName = "8th Semester"}
            //}.ForEach(semesters => context.Semesters.AddOrUpdate(semesters)); ;

            //new List<Designation>
            //{
            //    new Designation {Name = "Lecturer"},
            //    new Designation {Name = "Assistant Professor"},
            //    new Designation {Name = "Associate Professor"},
            //    new Designation {Name = "Professor"}
            //}.ForEach(designations => context.Designations.AddOrUpdate(designations));

            //new List<Room>
            //{
            //    new Room {RoomNo = "101"},
            //    new Room {RoomNo = "102"},
            //    new Room {RoomNo = "103"},
            //    new Room {RoomNo = "201"},
            //    new Room {RoomNo = "202"},
            //    new Room {RoomNo = "203"}
            //}.ForEach(rooms => context.Rooms.AddOrUpdate(rooms));

            //new List<GradeLetter>
            //{
            //    new GradeLetter {Grade = "A+"},
            //    new GradeLetter {Grade = "A"},
            //    new GradeLetter {Grade = "A-"},
            //    new GradeLetter {Grade = "B+"},
            //    new GradeLetter {Grade = "B"},
            //    new GradeLetter {Grade = "B-"},
            //    new GradeLetter {Grade = "C"},
            //    new GradeLetter {Grade = "D"},
            //    new GradeLetter {Grade = "F"}
            //}.ForEach(grades => context.GradeLetters.AddOrUpdate(grades));

            //new List<Department>
            //{
            //    new Department {Code = "CSE", Name = "Computer Science & Engineering"},
            //    new Department {Code = "EEE", Name = "Electrical & Electronics Engineering"},
            //    new Department {Code = "BBA", Name = "Bachelor of Business Administration"},
            //    new Department {Code = "PHY", Name = "Physics"},
            //    new Department {Code = "CHY", Name = "Chemistry"},
            //    new Department {Code = "MATH", Name = "Mathmetics"},
            //    new Department {Code = "BAN", Name = "Bangla"},
            //    new Department {Code = "ENG", Name = "English"},
            //    new Department {Code = "GEN", Name = "Genetics"},
            //    new Department {Code = "FAR", Name = "Farmacy"}

            //}.ForEach(departments => context.Departments.AddOrUpdate(departments));


            //new List<Day>
            //{
            //    new Day {TimeDay = "Saturday"},
            //    new Day {TimeDay = "Sunday"},
            //    new Day {TimeDay = "Monday"},
            //    new Day {TimeDay = "Tuesday"},
            //    new Day {TimeDay = "Wednesday"},
            //    new Day {TimeDay = "Thursday"},
            //    new Day {TimeDay = "Friday"}
            //}.ForEach(day => context.Days.AddOrUpdate(day));

            //new List<Course>
            //{
            //    new Course {Code = "CSE-111", Name = "Programming In C", Credit = 3, Description = "Basic Programming", DepartmentId = 1, SemesterId = 1},
            //    new Course {Code = "CSE-112", Name = "Programming In C ++", Credit = 4, Description = "OOP", DepartmentId = 1, SemesterId = 2},
            //    new Course {Code = "CSE-211", Name = "Compiler Design", Credit = 3, Description = "Compiler and Liberary Design", DepartmentId = 1, SemesterId = 3},
            //    new Course {Code = "CSE-212", Name = "Artificial Intellegence", Credit = 3, Description = "AI", DepartmentId = 1, SemesterId = 4},
            //    new Course {Code = "EEE-111", Name = "Basic Electricity", Credit = 3, Description = "Basic Knowledge", DepartmentId = 2, SemesterId = 1},
            //    new Course {Code = "EEE-112", Name = "Energy Transmission", Credit = 4, Description = "Energy Conversion", DepartmentId = 2, SemesterId = 2},
            //    new Course {Code = "EEE-211", Name = "Electrical Instrumentation", Credit = 4, Description = "Basic Tools Learning", DepartmentId = 2, SemesterId = 3},
            //    new Course {Code = "ACC-115", Name = "Basic Accounting & Costing", Credit = 5, Description = "Accounting Basic", DepartmentId = 3, SemesterId = 1},

            //}.ForEach(crs => context.Courses.AddOrUpdate(crs));

            //new List<Teacher>
            //{
            //    new Teacher {Name = "Md. Abul Kashem", Address = "DUET, Gazipur", Email = "mak@mail.com", ContactNo = "01884292449", DesignationId= 4, DepartmentId = 1, Credit = 7},
            //    new Teacher {Name = "Md. Nasim Akter", Address = "DUET, Gazipur", Email = "mna@cse.com", ContactNo = "01778892251", DesignationId= 3, DepartmentId = 1, Credit = 8},
            //    new Teacher {Name = "Md. Obaydur Rahman", Address = "DUET, Gazipur", Email = "mor@engr.com", ContactNo = "01922369874", DesignationId= 2, DepartmentId = 1, Credit = 9},
            //    new Teacher {Name = "Md. Atikur Rahman", Address = "DUET, Gazipur", Email = "mar@eee.com", ContactNo = "01814569874", DesignationId= 1, DepartmentId = 2, Credit = 5},
            //    new Teacher {Name = "Sirazul Haq Mollah", Address = "DUET, Gazipur", Email = "shm@eee.com", ContactNo = "01836974125", DesignationId= 3, DepartmentId = 2, Credit = 4},
            //    new Teacher {Name = "Tarekuzzaman", Address = "DUET, Gazipur", Email = "tz@bba.com", ContactNo = "01966974125", DesignationId= 3, DepartmentId = 3, Credit = 3},
                
            //}.ForEach(tcr => context.Teachers.AddOrUpdate(tcr));
        }
    }
}
