using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using QuizeManagement.Helpers.SpHelper;
using QuizeManagement.Models.DbContext;
using QuizeManagement.Models.ViewModels;

namespace QuizeManagement_0406.Controllers
{
    public class User_TableController : Controller
    {
        private QuizeManagement_0406Entities db = new QuizeManagement_0406Entities();

        // GET: User_Table
        public ActionResult Index()
        {
            int id = Convert.ToInt32(Session["userId"]);
            SqlParameter parameter = new SqlParameter("@id", id);
            var students = db.Database.SqlQuery<RegisterModel>(SpHelper.StudenDataShow, parameter).ToList();
            return View(students);
        }

        // GET: User_Table/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            User_Table user_Table = db.User_Table.Find(id);
            if (user_Table == null)
            {
                return HttpNotFound();
            }
            return View(user_Table);
        }

        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            User_Table user_Table = db.User_Table.Find(id);   
            user_Table.Updated_at = null;
            if (user_Table == null)
            {
                return HttpNotFound();
            }
            return View(user_Table);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "User_id,Username,Email,Password_hash,Created_at,Updated_at")] User_Table user_Table)
        {
            if (ModelState.IsValid)
            {
                user_Table.Updated_at = DateTime.Now;
                db.Entry(user_Table).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(user_Table);
        }


        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
