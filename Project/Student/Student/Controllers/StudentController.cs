using Student.DataAccess;
using Student.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Student.Controllers
{
    public class StudentController : Controller
    {
        public StudentRepository _studentRepository = null;
        public StudentModel SM = null;
        public StudentController()
        {
            _studentRepository = new StudentRepository();
            SM = new StudentModel();
        }
        // GET: Student
        public ActionResult Index()
        {
            ViewBag.CityList = _studentRepository.GetCityList();
            ViewBag.SubjectList = _studentRepository.GetSubjectList();
            SM.Students = _studentRepository.GetAllStudent();
            return View(SM);
        }
        [HttpPost]
        public ActionResult Index(StudentModel SM)
        {
            bool Result = _studentRepository.SaveStudent(SM);
            if (Result == true)
            {
                TempData["status"] = "ok";
                TempData["msg"] = "Student details saved successfully";
            }
            else
            {
                TempData["status"] = "Failed";
                TempData["msg"] = "Failed to save student details";
            }
            return RedirectToAction("Index");
        }

        public JsonResult GetStudentByID(int studentid)
        {
            StudentModel Result = _studentRepository.GetStudent(studentid);
            return Json(Result, JsonRequestBehavior.AllowGet);
        }
        [HttpDelete]
        public ActionResult StudentRemove(int id)
        {
            bool Result = _studentRepository.RemoveStudentDetails(id);
            if (Result == true)
            {
                TempData["status"] = "ok";
                TempData["msg"] = "Student details deleted successfully";
            }
            else
            {
                TempData["status"] = "Failed";
                TempData["msg"] = "Failed to remove student details";
            }
            return Json(Result, JsonRequestBehavior.AllowGet);
        }
    }
}