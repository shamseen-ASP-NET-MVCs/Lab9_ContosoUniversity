using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Lab9_ContosoUniversity.Models;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.Validation;

namespace Lab9_ContosoUniversity.DAL
{
    public class SchoolContext : DbContext
    {

        //specifying name of connection string, but entity framework would have mapped it to SchoolContext anyway
        public SchoolContext() : base("SchoolContext") { } 
        public DbSet<Student> Students { get; set; }
        public DbSet<Enrollment> Enrollments { get; set; }
        public DbSet<Course> Courses { get; set; }
        public DbSet<Department> Departments {get;set;}
        public DbSet<Instructor> Instructors {get;set;}
        public DbSet<OfficeAssignment> OfficeAssignments { get; set; }


        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            //prevents table names from being pluralized so Student instead of Students
            //really it's a matter of preference but whatever. I don't wanna look out for name changes throughout tutorial
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();

            //making a join table bc instructor and course are many-to-many
            //mapping table to have a courseID column on the left, and instructorID column on the right.
            //then naming table CourseInstructor
            modelBuilder.Entity<Course>()
                .HasMany(c => c.Instructors).WithMany(i => i.Courses)
                .Map(t => t.MapLeftKey("CourseID")
                            .MapRightKey("InstructorID")
                            .ToTable("CourseInstructor"));
        }
    }
}
