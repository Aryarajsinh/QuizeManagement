using QuizeManagement.Models.DbContext;
using QuizeManagement.Models.ViewModels;
using QuizeManagement.Repository.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;

namespace QuizeManagement.Api.Controllers
{
    [System.Web.Http.AllowAnonymous]
    public class AdminController : ApiController
    {

        AdminServices _AdminServices = new AdminServices();

        [System.Web.Http.HttpGet]
        [System.Web.Http.Route("api/Admin/GetAllQuiez")]
        public List<AllQuiezModel> GetAllQuiez()
        {
            return  _AdminServices.GetIndex();
        }

        [System.Web.Http.HttpPost]
        [System.Web.Http.Route("api/Admin/PostCreate")]
        public void PostCreate([Bind(Include = "Quiz_id,Title,Description,Created_by,Created_at,Updated_at")] QuizzeModel quizzes_Table)
        {
            _AdminServices.CreateQuiz(quizzes_Table);
        }

        //[System.Web.Http.HttpGet]
        //[System.Web.Http.Route("api/Admin/GetEdit")]
        //public Quizzes_Table GetEdit(int? id)
        //{
        //    return _AdminServices.GetQuizById(id.Value);
        //}


        //[System.Web.Http.HttpPost]
        //[System.Web.Http.Route("api/Admin/PostEdit")]
        //public void PostEdit([Bind(Include = "Quiz_id,Title,Description,Created_by,Created_at,Updated_at")] Quizzes_Table quizzes_Table)
        //{
        //    _AdminServices.UpdateQuiz(quizzes_Table);
        //}

        //[System.Web.Http.HttpGet]
        //[System.Web.Http.Route("api/Admin/GetDelete")]
        //public Quizzes_Table GetDelete(int? id)
        //{

        //    return _AdminServices.GetQuizById(id.Value);

        //}

        [System.Web.Http.HttpPost]
        [System.Web.Http.Route("api/Admin/PostDelete")]
        public void PostDelete(int id)
        {
            _AdminServices.DeleteQuiz(id);
        }


        //[System.Web.Http.HttpGet]
        //[System.Web.Http.Route("api/Admin/GetCreateQuestion")]
        //public Quizzes_Table GetCreateQuestion(int? id)
        //{
        //    return _AdminServices.GetQuizById(id.Value);
        //}

        //[System.Web.Http.HttpPost]
        //[System.Web.Http.Route("api/Admin/PostCreateQuestion")]
        //public void PostCreateQuestion(List<QuestionOrOpetionAddingModel> _QustionAddingModel)
        //{
        //    _AdminServices.AddQuestion(_QustionAddingModel);

        //}

        //[System.Web.Http.HttpGet]
        //[System.Web.Http.Route("api/Admin/GetUpdateQuestions")]
        //public List<QuestionOrOpetionAddingModel> GetUpdateQuestions(int id)
        //{
        //    return _AdminServices.GetQuestions(id);

        //}

        //[System.Web.Http.HttpPost]
        //[System.Web.Http.Route("api/Admin/PostUpdateQuestions")]
        //public void PostUpdateQuestions(List<QuestionOrOpetionAddingModel> questions)
        //{
        //    _AdminServices.UpdateQuestions(questions);
        //}
    }
}