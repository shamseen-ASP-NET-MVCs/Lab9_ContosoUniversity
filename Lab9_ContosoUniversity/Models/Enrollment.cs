using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab9_ContosoUniversity.Models
{
    public enum Grade
    {
        A, B, C, D, F
    }
    public class Enrollment
    {
        public int EnrollmentID { get; set; }
        public Grade? Grade { get; set; } //? means Grade can be nullable

        //foreign keys
        public int CourseID { get; set; }
        public int StudentID { get; set; }

        //nav properties ==> virtual so as to use lazy loading
        //one-to-one
        public virtual Course Course { get; set; }
        public virtual Student Student { get; set; }


    }
}
