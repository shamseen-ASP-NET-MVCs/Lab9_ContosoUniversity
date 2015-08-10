using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Lab9_ContosoUniversity.Models
{
    public class Course
    {
        //how to generate properties for a database
        // the .None means I can input my own values instead of autogenerating
        //good because course id's usually have a certain code
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int CourseID { get; set; }
        public string Title { get; set; }
        public int Credits { get; set; }

        //one-to-many
        public virtual ICollection<Enrollment> Enrollments { get; set; }
    }
}
