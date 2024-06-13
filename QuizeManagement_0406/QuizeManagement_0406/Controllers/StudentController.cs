using QuizeManagement.Models.DbContext;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using System.Data.Entity;
using QuizeManagement.Models.ViewModels;
using QuizeManagement.Helpers.Helper;

namespace QuizeManagement_0406.Controllers
{
    [CustomAutherize]
    public class StudentController : Controller
    {
        private QuizeManagement_0406Entities db = new QuizeManagement_0406Entities();

        public ActionResult Index()
        {
            var quizzes_Table = db.Quizzes_Table.Include(q => q.User_Table);

            return View(quizzes_Table.ToList());
        }

        public ActionResult Startquiz(int quizid)
        {
            var questions = GetQuestions(quizid);
            if (questions == null || questions.Count == 0)
            {
                return HttpNotFound();
    }

    Session["quizquestions"] = questions;
            Session["currentquestionindex"] = 0;
            Session["score"] = 0;

            return RedirectToAction("Showquestion");
}
//public ActionResult StartQuiz(int quizId)
//{
//    var questions = GetQuestions(quizId);
//    if (questions == null || questions.Count == 0)
//    {
//        return HttpNotFound();
//    }

//    Session["QuizQuestions"] = questions;
//    Session["CurrentQuestionIndex"] = 0;
//    Session["Score"] = 0;

//    return RedirectToAction("ShowQuestion");
//}

public List<QuestionOrOpetionAddingModel> GetQuestions(int quizid)
        {
            var questionDataList = new List<QuestionOrOpetionAddingModel>();

            var questions = db.Question_Table.Where(q => q.Quiz_id == quizid).ToList();
            foreach (var question in questions)
            {
                var options = db.Options_Table.Where(o => o.Question_id == question.Question_id).OrderBy(o => o.Option_id).ToList();

                var questionData = new QuestionOrOpetionAddingModel
                {
                    quizId = quizid,
                    questionId = question.Question_id,
                    question = question.Question_txt,
                    Options = options.Select(o => new QuestionOrOpetionAddingModel.Option
                    {
                        Option_id = o.Option_id,
                        Option_text = o.Option_text,
                        Is_correct = (bool)o.Is_correct
                    }).ToList()
                };

                questionDataList.Add(questionData);
            }

            return questionDataList;
        }


        public ActionResult ShowQuestion()
        {
            var questions = Session["QuizQuestions"] as List<QuestionOrOpetionAddingModel>;
            int currentIndex = (int)Session["CurrentQuestionIndex"];

            if (questions == null || currentIndex >= questions.Count)
            {
                return RedirectToAction("EndQuiz");
            }

            var currentQuestion = questions[currentIndex];
            return View(currentQuestion);
        }
        [HttpPost]

        public ActionResult SubmitAnswer(int? selectedOptionId, int questionId)
        {

            if (ModelState.IsValid)
            {
                // Retrieve the session data
                var questions = Session["QuizQuestions"] as List<QuestionOrOpetionAddingModel>;
                int currentIndex = (int)Session["CurrentQuestionIndex"];

                if (questions == null || currentIndex >= questions.Count)
                {
                    return RedirectToAction("EndQuiz");
                }

                var currentQuestion = questions[currentIndex];
                var selectedOptionText = db.Options_Table.FirstOrDefault(o => o.Option_id == selectedOptionId)?.Option_text;
                currentQuestion.UserAnswer = selectedOptionText; // Store the user's answer as a string

                // Retrieve the correct answer from the database
                var correctOption = db.Options_Table.FirstOrDefault(o => o.Question_id == questionId && o.Is_correct == true);

                if (correctOption == null)
                {
                    // Log or handle error - no correct answer found
                    currentQuestion.IsCorrect = false;
                }
                else
                {
                    currentQuestion.IsCorrect = (correctOption.Option_text == selectedOptionText);
                }

                if (currentQuestion.IsCorrect)
                {
                    Session["Score"] = (int)Session["Score"] + 10;
                }

                // Save user's answer to the database
                var userAnswerEntry = new User_Answer_Table
                {
                    User_id = 1, // Assuming user ID is 1 for now, replace with actual user ID
                    Quiz_id = currentQuestion.quizId,
                    Question_id = questionId,
                    Selected_Option_id = selectedOptionId,
                    Created_at = DateTime.Now
                };
                db.User_Answer_Table.Add(userAnswerEntry);
                db.SaveChanges();

                // Move to the next question
                Session["CurrentQuestionIndex"] = currentIndex + 1;

                // Redirect to show the next question
                return RedirectToAction("ShowQuestion");
            }
        else{
                return View("ShowQuestion");
        }
        }
        //[HttpPost]
        //public ActionResult SubmitAnswer(int? selectedOptionId, int questionId)
        //{
        //    var questions = Session["QuizQuestions"] as List<QuestionOrOpetionAddingModel>;
        //    int currentIndex = (int)Session["CurrentQuestionIndex"];

        //    if (questions == null || currentIndex >= questions.Count)
        //    {
        //        return RedirectToAction("EndQuiz");
        //    }

        //    var currentQuestion = questions[currentIndex];

        //    if (selectedOptionId.HasValue)
        //    {
        //        var selectedOptionText = db.Options_Table.FirstOrDefault(o => o.Option_id == selectedOptionId.Value)?.Option_text;
        //        currentQuestion.UserAnswer = selectedOptionText;

        //        var correctOption = db.Options_Table.FirstOrDefault(o => o.Question_id == questionId && o.Is_correct == true);

        //        if (correctOption == null)
        //        {
        //            currentQuestion.IsCorrect = false;
        //        }
        //        else
        //        {
        //            currentQuestion.IsCorrect = (correctOption.Option_text == selectedOptionText);
        //        }

        //        // Debugging logs
        //        System.Diagnostics.Debug.WriteLine("Question ID: " + questionId);
        //        System.Diagnostics.Debug.WriteLine("Selected Option Text: " + selectedOptionText);
        //        System.Diagnostics.Debug.WriteLine("Correct Option Text: " + correctOption?.Option_text);
        //        System.Diagnostics.Debug.WriteLine("Is Correct: " + currentQuestion.IsCorrect);

        //        if (currentQuestion.IsCorrect)
        //        {
        //            Session["Score"] = (int)Session["Score"] + 10;
        //        }

        //        var userAnswerEntry = new User_Answer_Table
        //        {
        //            User_id = 1, // Replace with actual user ID
        //            Quiz_id = currentQuestion.quizId,
        //            Question_id = questionId,
        //            Selected_Option_id = selectedOptionId.Value,
        //            Created_at = DateTime.Now
        //        };
        //        db.User_Answer_Table.Add(userAnswerEntry);
        //        db.SaveChanges();
        //    }

        //    currentIndex++;
        //    if (currentIndex >= questions.Count)
        //    {
        //        return RedirectToAction("EndQuiz");
        //    }

        //    Session["CurrentQuestionIndex"] = currentIndex;

        //    return RedirectToAction("ShowQuestion");
        //}


        public ActionResult EndQuiz()
        {
            int score = (int)Session["Score"];
            int userId = 1; // Assuming user ID is 1 for now, replace with actual user ID
            int quizId = ((List<QuestionOrOpetionAddingModel>)Session["QuizQuestions"]).FirstOrDefault()?.quizId ?? 0;

            // Save the result to the database
            var result = new Result_Table
            {
                User_id = userId,
                Quiz_id = quizId,
                Score = score,
                Created_at = DateTime.Now
            };

            db.Result_Table.Add(result);
            db.SaveChanges();

            ViewBag.Score = score;
            return View();
        }

        //public ActionResult EndQuiz()
        //{
        //    int score = (int)Session["Score"];
        //    int userId = 1;
        //    int quizId = ((List<QuestionOrOpetionAddingModel>)Session["QuizQuestions"]).FirstOrDefault()?.quizId ?? 0;

        //    var result = new Result_Table
        //    {
        //        User_id = userId,
        //        Quiz_id = quizId,
        //        Score = score,
        //        Created_at = DateTime.Now
        //    };

        //    db.Result_Table.Add(result);
        //    db.SaveChanges();

        //    ViewBag.Score = score;
        //    return View();
        //}

    }
}