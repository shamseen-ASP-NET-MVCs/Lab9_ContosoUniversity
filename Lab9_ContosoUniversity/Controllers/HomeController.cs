using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Lab9_ContosoUniversity.DAL;
using Lab9_ContosoUniversity.ViewModels;

namespace Lab9_ContosoUniversity.Controllers
{
    public class HomeController : Controller
    {
        private SchoolContext db = new SchoolContext();
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            //for every unique enrollment date, create a new collection dategroup
            //then create a new instance of EnrollmentDateGroup and pass in the date and number of students 
            IQueryable<EnrollmentDateGroup> data = from student in db.Students
                                                    group student by student.EnrollmentDate into dategroup
                                                    select new EnrollmentDateGroup()
                                                    {
                                                        EnrollmentDate = dategroup.Key, //the actual date
                                                        StudentCount = dategroup.Count() //num of students
                                                    };
            return View(data.ToList());
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}