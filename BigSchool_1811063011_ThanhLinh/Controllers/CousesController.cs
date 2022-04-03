using BigSchool_1811063011_ThanhLinh.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BigSchool_1811063011_ThanhLinh.Controllers
{
    public class CousesController : Controller
    {
        // GET: Couses
       
            public class CousersController : Controller
        {
            // GET: Cousers
            public ActionResult Create()
            {
                BigSchoolContext context = new BigSchoolContext();
                Course objCourse = new Course();
                objCourse.ListCategory = context.Categories.ToList();
                return View(objCourse);
            }
            [Authorize]
            [HttpPost]
            [ValidateAntiForgeryToken]

            public ActionResult Create(Course objCourse)
            {
                BigSchoolContext context = new BigSchoolContext();
                ModelState.Remove("LecturerId");
                if (!ModelState.IsValid)
                {
                    objCourse.ListCategory = context.Categories.ToList();
                    return View("Create", objCourse);
                }
                ApplicationUser user = System.Web.HttpContext.Current.GetOwinContext().GetUserManager<ApplicationUserManager>().FindById(System.Web.HttpContext.Current.User.Identity.GetUserId());
                objCourse.LecturerId = user.Id;

                context.Courses.Add(objCourse);
                context.SaveChanges();

                return RedirectToAction("Index", "Home");
            }
            public ActionResult Attending()
            {
                BigSchoolContext context = new BigSchoolContext();
                ApplicationUser currentUser = System.Web.HttpContext.Current.GetOwinContext().GetUserManager<ApplicationUserManager>().FindById(System.Web.HttpContext.Current.User.Identity.GetUserId());
                var listAttendances = context.Attendances.Where(p => p.Attendee == currentUser.Id).ToList();
                var courses = new List<Course>();
                foreach (Attendance temp in listAttendances)
                {
                    Course objCourse = temp.Course;
                    objCourse.LecturerName = System.Web.HttpContext.Current.GetOwinContext().GetUserManager<ApplicationUserManager>().FindById(objCourse.LecturerId).Name;
                    courses.Add(objCourse);
                }
                return View(courses);
            }
            public ActionResult Mine()
            {
                ApplicationUser currentUser = System.Web.HttpContext.Current.GetOwinContext().GetUserManager<ApplicationUserManager>().FindById(System.Web.HttpContext.Current.User.Identity.GetUserId());
                BigSchoolContext context = new BigSchoolContext();
                var courses = context.Courses.Where(c => c.LecturerId == currentUser.Id && c.Datetime > DateTime.Now).ToList();
                foreach (Course i in courses)
                {
                    i.LecturerName = currentUser.UserName;
                }
                return View(courses);
            }

            public ActionResult LectureIamGoing()
            {
                ApplicationUser currentUser = System.Web.HttpContext.Current.GetOwinContext().GetUserManager<ApplicationUserManager>().FindById(System.Web.HttpContext.Current.User.Identity.GetUserId());
                BigSchoolContext context = new BigSchoolContext();
                var listFollwee = context.Followings.Where(p => p.FollwerId ==
                currentUser.Id).ToList();
                var listAttendances = context.Attendances.Where(p => p.Attendee ==
                currentUser.Id).ToList();
                var courses = new List<Course>();
                foreach (var course in listAttendances)
                {
                    foreach (var item in listFollwee)
                    {
                        if (item.FolloweeId == course.Course.LecturerId)
                        {
                            Course objCourse = course.Course;
                            objCourse.LecturerName = System.Web.HttpContext.Current.GetOwinContext().GetUserManager<ApplicationUserManager>().FindById(objCourse.LecturerId).Name;
                            courses.Add(objCourse);
                        }
                    }

                }
                return View(courses);
            }


        }

    }
}
