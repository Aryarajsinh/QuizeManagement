using QuizeManagement.Helpers.Helper;
using QuizeManagement.Models.DbContext;
using QuizeManagement.Models.ViewModels;
using QuizeManagement.Repository.Interfaces;
using QuizeManagement.Repository.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.Mvc;
using static QuizeManagement.Models.ViewModels.QuestionOrOpetionAddingModel;

namespace QuizeManagement_0406.Controllers
{
    public class LoginController : Controller
    {
        IStudentInterfaces _student;
        QuizeManagement_0406Entities _opetion = new QuizeManagement_0406Entities();

        public LoginController()
        {           
            _student = new StudentServices();
        }
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(LoginModel db)
         {
            if (ModelState.IsValid && !SessionHelper.IsUserLoggedIn)
            {
                RegisterModel _studentModel = _student.CheckUser(db);
                AdminModel _adminModel = _student.CheckAdmin(db);



                if (_studentModel.User_id > 0 && _adminModel.Username == null)
                {
                    SessionHelper.LoggedInUser = _studentModel.Username;
                    Session["UserName"] = _studentModel.Username;
                    Session["userId"] = _studentModel.User_id.ToString();
                    Session["Studentid"] =_studentModel.User_id.ToString();
                    return RedirectToAction("Index", "Student");
                }
                else if (_studentModel.Username == null && _adminModel.Admin_id > 0)
                {

                    //Admin_Table _RegistersAdmin = _opetion.Admin_Table.FirstOrDefault(u => u.Email == db.Email);
                    //string userName = _RegistersAdmin.Username;
                    SessionHelper.LoggedInUser = _adminModel.Username;
                    Session["AdminId"] = _adminModel.Admin_id;
                    Session["UserName"] = _adminModel.Username;
                    return RedirectToAction("Index", "Quizzes_Table");
                }
                else
                {
                    return View("Login");

                }
            }
            return View();

        }

        public ActionResult SignUp()
        {
            return View();
        }

        [HttpPost]
        public ActionResult SignUp(RegisterModel _registerModel)
        {
            if (ModelState.IsValid)
            {
                if (_student.AddUser(_registerModel))
                {
                    return RedirectToAction("Login");
                }
                else
                {
                    ModelState.AddModelError("Email", "Email already exist");
                    return View();
                }
            }
            else
            {
                return View();
            }
        }

        public ActionResult Logout()
        {
            SessionHelper.LogOutUser();
            return RedirectToAction("Login", "Login");
        }


    }
}