using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Lab9_ContosoUniversity.Models
{
    public class Department
    {
        public int DepartmentID { get; set; }

        [StringLength(50, MinimumLength =3)]
        public string Name { get; set; }

        [DataType(DataType.Currency)]
        [Column(TypeName ="money")]
        public decimal Budget { get; set; }

        //foreign key
        public int? InstructorID { get; set; }

        //nav properties
        public virtual Instructor Administrator { get; set; } //one to one
        public virtual ICollection<Course> Courses { get; set; } //one to many
    }
}
