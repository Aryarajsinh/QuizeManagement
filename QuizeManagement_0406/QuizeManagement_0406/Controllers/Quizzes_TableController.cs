using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using QuizeManagement.Helpers.Helper;
using QuizeManagement.Models.DbContext;
using QuizeManagement.Models.ViewModels;
using QuizeManagement.Repository.Services;
using QuizeManagement_0406.Common;

namespace QuizeManagement_0406.Controllers
{
    [AdminAuthorize]
    public class Quizzes_TableController : Controller
    {
        private QuizeManagement_0406Entities db = new QuizeManagement_0406Entities();
        AdminServices _AdminServices = new AdminServices();
        AdminApiHelper _adminapi = new AdminApiHelper();

        public async Task<ActionResult> Index()
        {

            //return View(_AdminServices.GetIndex());

            return View(await _adminapi.GetAllQuizzesAsync());
        }

        //public ActionResult Details(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    Quizzes_Table quizzes_Table = db.Quizzes_Table.Find(id);
        //    if (quizzes_Table == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(quizzes_Table);
        //}


        public ActionResult Create()
        {
            ViewBag.Created_by = new SelectList(db.User_Table, "User_id", "Username");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "Quiz_id,Title,Description,Created_by,Created_at,Updated_at")] QuizzeModel quizzes_Table)
        {
            if (ModelState.IsValid)
            {
                //quizzes_Table.Created_at = DateTime.Now;
                //quizzes_Table.Updated_at = null;

                //db.Quizzes_Table.Add(quizzes_Table);
                //db.SaveChanges();
                //_AdminServices.CreateQuiz(quizzes_Table);
                await _adminapi.PostAddQuiezAsync(quizzes_Table);
                return RedirectToAction("Index", "Quizzes_Table");
            }

            ViewBag.Created_by = new SelectList(db.User_Table, "User_id", "Username", quizzes_Table.Created_by);
            return View(quizzes_Table);
        }

        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            //Quizzes_Table quizzes_Table = db.Quizzes_Table.Find(id);
            Quizzes_Table quizzes_Table = _AdminServices.GetQuizById(id.Value);
            if (quizzes_Table == null)
            {
                return HttpNotFound();
            }
            ViewBag.Created_by = new SelectList(db.User_Table, "User_id", "Username", quizzes_Table.Created_by);
            return View(quizzes_Table);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Quiz_id,Title,Description,Created_by,Created_at,Updated_at")] Quizzes_Table quizzes_Table)
        {
            if (ModelState.IsValid)
            {
                //quizzes_Table.Updated_at = DateTime.Now;
                //db.Entry(quizzes_Table).State = EntityState.Modified;
                //db.SaveChanges();
                _AdminServices.UpdateQuiz(quizzes_Table);
                return RedirectToAction("Index", "Quizzes_Table");
            }
            ViewBag.Created_by = new SelectList(db.User_Table, "User_id", "Username", quizzes_Table.Created_by);
            return View(quizzes_Table);
        }


        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            //Quizzes_Table quizzes_Table = db.Quizzes_Table.Find(id);
            Quizzes_Table quizzes_Table = _AdminServices.GetQuizById(id.Value);
            if (quizzes_Table == null)
            {
                return HttpNotFound();
            }
            return View(quizzes_Table);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            //var quiz = db.Quizzes_Table.Find(id);
            //if (quiz == null)
            //{
            //    return HttpNotFound();
            //}
            //SqlParameter[] delteRecord = new SqlParameter[]{
            //    new SqlParameter("@QuizId",id)
            //};
            //var questions = db.Question_Table.Where(q => q.Quiz_id == id).ToList();
            //foreach (var question in questions)
            //{
            //List<DeleteModel> options = db.Database.SqlQuery<DeleteModel>("Exec DeleteQuiz @QuizId", delteRecord).ToList();
            //db.Options_Table.RemoveRange(options);
            //}
            //db.Question_Table.RemoveRange(questions); // Delete the questions related to the quiz

            /// Finally, delete the quiz itself
            //db.Quizzes_Table.Remove(quiz);

            // Save changes to the database
            //db.SaveChanges();

            _adminapi.DeleteQuizzesAsync(id);

            return RedirectToAction("Index");
        }


        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        public ActionResult CreateQuestion(int? id)
        {
            Session["Quizid"] = id.ToString();
            //Quizzes_Table quizzes_Table = db.Quizzes_Table.Find(id);
            Quizzes_Table quizzes_Table = _AdminServices.GetQuizById(id.Value);
            Session["Description"] = quizzes_Table.Description;
            Session["Title"] = quizzes_Table.Title;
            return View();
        }


        [HttpPost]
        public ActionResult CreateQuestion(List<QuestionOrOpetionAddingModel> _QustionAddingModel)

        {
            _AdminServices.AddQuestion(_QustionAddingModel);
            ModelState.Clear();
            return RedirectToAction("Index");

        }

        [HttpGet]
        public ActionResult UpdateQuestions(int id)
        {
            var questions = _AdminServices.GetQuestions(id);
            if (questions == null || questions.Count == 0)
            {
                return HttpNotFound();
            }

            return View(questions);
        }

        [HttpPost]
        public ActionResult UpdateQuestions(List<QuestionOrOpetionAddingModel> questions)
        {
            if (questions == null || !ModelState.IsValid)
            {
                return View(questions);
            }

            _AdminServices.UpdateQuestions(questions);
            return RedirectToAction("index");
        }

    }
}
