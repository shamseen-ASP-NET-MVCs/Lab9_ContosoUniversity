using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Lab9_ContosoUniversity.Models
{
    public class Course
    {
        //how to generate properties for a database
        //the .None means I can input my own values instead of autogenerating
        //good because course id's usually have a certain code
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Display(Name ="Number")]
        public int CourseID { get; set; }

        [StringLength(50, MinimumLength = 3)]
        public string Title { get; set; }

        [Range(0, 5)]
        public int Credits { get; set; }

        public int DepartmentID { get; set; }

        //nav properties
        public virtual Department Department { get; set; } //many-to-one
        public virtual ICollection<Instructor> Instructors { get; set; } //one-to-many
        public virtual ICollection<Enrollment> Enrollments { get; set; }
    }
}
