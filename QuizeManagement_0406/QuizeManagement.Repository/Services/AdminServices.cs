using QuizeManagement.Helpers.Helper;
using QuizeManagement.Models.DbContext;
using QuizeManagement.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;

using System.Data.Entity;
using QuizeManagement.Repository.Interfaces;
using System.Threading.Tasks;

namespace QuizeManagement.Repository.Services

{
    public class AdminServices : IAdminInterfaces
    {
        public void AddQuestion(List<QuestionOrOpetionAddingModel> _QustionAddingModel)
        {
            string AddQuestionsAndOptions = "AddQuestionsAndOptions";
            foreach (var item in _QustionAddingModel)
            {
                QuestionOrOpetionAddingModel _QustionAddingModels = new QuestionOrOpetionAddingModel();
                string Answers = item.Answers;
                string options1 = item.options1;
                string options2 = item.options2;
                string options3 = item.options3;
                string options4 = item.options4;
                string question = item.question;
                int quizId_ = item.quizId;
                Dictionary<string, object> Parameters = new Dictionary<string, object>
                {
                    {"@quiz_id" ,quizId_},
                    {"@question_text ",question},
                    {"@options1 ", options1},
                    {"@options2 ",options2},
                    {"@options3", options3},
                    {"@options4", options4},
                    { "@Answers",Answers}
                };
                GenericRepository.AddQuestion(AddQuestionsAndOptions, Parameters);
            }

        }

        public List<AllQuiezModel> GetIndex()
        {
            QuizeManagement_0406Entities db = new QuizeManagement_0406Entities();
            List<AllQuiezModel> quizzes_Table =  db.Database.SqlQuery<AllQuiezModel>("Exec showQuiez").ToList();

            return  quizzes_Table;

        }

        public void CreateQuiz(QuizzeModel quizzesTable)
        {

            SqlParameter[] spParameter = new SqlParameter[]
            {
                new SqlParameter("@title",quizzesTable.Title),
                new SqlParameter("@description",quizzesTable.Description)
            };
            QuizeManagement_0406Entities db = new QuizeManagement_0406Entities();
            quizzesTable.Created_at = DateTime.Now;
            quizzesTable.Updated_at = null;

            db.Database.SqlQuery<QuizzeModel>("Exec CreateQuiz @title , @description ",spParameter).ToList();
            db.SaveChanges();
        }

        public void UpdateQuiz(Quizzes_Table quizzesTable)
        {
            QuizeManagement_0406Entities db = new QuizeManagement_0406Entities();
            quizzesTable.Updated_at = DateTime.Now;
            db.Entry(quizzesTable).State = EntityState.Modified;
            db.SaveChanges();
        }


        public void DeleteQuiz(int id)
        {
            QuizeManagement_0406Entities db = new QuizeManagement_0406Entities();
            SqlParameter[] deleteParameter = new SqlParameter[]
           {
                new SqlParameter("@QuizId",id),
             
           };
            //var quiz = db.Quizzes_Table.Find(id);
            //if (quiz != null)
            //{
            db.Database.ExecuteSqlCommand("Exec DeleteQuiz @QuizId", deleteParameter);
             
            //}
        }
        public Quizzes_Table GetQuizById(int id)
        {
            QuizeManagement_0406Entities db = new QuizeManagement_0406Entities();
            return db.Quizzes_Table.Find(id);
        }

        public List<QuestionOrOpetionAddingModel> GetQuestions(int id)
        {
            QuizeManagement_0406Entities db = new QuizeManagement_0406Entities();
            var questionDataList = new List<QuestionOrOpetionAddingModel>();
            var questions = db.Question_Table.Where(q => q.Quiz_id == id).Take(5).ToList();
            foreach (var question in questions)
            {
                var options = db.Options_Table.Where(o => o.Question_id == question.Question_id).OrderBy(o => o.Option_id).ToList();

                var questionData = new QuestionOrOpetionAddingModel
                {
                    quizId = id,
                    questionId = question.Question_id,
                    question = question.Question_txt,
                    options1 = options.ElementAtOrDefault(0)?.Option_text,
                    options2 = options.ElementAtOrDefault(1)?.Option_text,
                    options3 = options.ElementAtOrDefault(2)?.Option_text,
                    options4 = options.ElementAtOrDefault(3)?.Option_text,
                    Answers = options.FirstOrDefault(o => (bool)o.Is_correct)?.Option_text
                };

                questionDataList.Add(questionData);
            }

            return questionDataList;
        }

        public void UpdateQuestions(List<QuestionOrOpetionAddingModel> questions)
        {
            QuizeManagement_0406Entities db = new QuizeManagement_0406Entities();
            foreach (var question in questions)
            {
                var existingQuestion = db.Question_Table.Find(question.questionId);
                if (existingQuestion != null)
                {
                    existingQuestion.Question_txt = question.question;
                    existingQuestion.Updated_at = DateTime.Now;

                    var options = db.Options_Table.Where(o => o.Question_id == question.questionId).ToList();
                    if (options.Count >= 4)
                    {
                        options[0].Option_text = question.options1;
                        options[1].Option_text = question.options2;
                        options[2].Option_text = question.options3;
                        options[3].Option_text = question.options4;

                        foreach (var option in options)
                        {
                            option.Is_correct = option.Option_text == question.Answers;
                            db.Entry(option).State = EntityState.Modified;
                        }
                    }

                    db.Entry(existingQuestion).State = EntityState.Modified;
                }
            }
            db.SaveChanges();
        }     

    }
}
