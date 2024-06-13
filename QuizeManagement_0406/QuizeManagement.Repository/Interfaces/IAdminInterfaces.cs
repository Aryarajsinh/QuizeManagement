using QuizeManagement.Models.DbContext;
using QuizeManagement.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuizeManagement.Repository.Interfaces
{
    public interface IAdminInterfaces
    {
        void AddQuestion(List<QuestionOrOpetionAddingModel> _QustionAddingModel);
         List<AllQuiezModel> GetIndex();
        void CreateQuiz(QuizzeModel quizzesTable);
        void UpdateQuiz(Quizzes_Table quizzesTable);
        void DeleteQuiz(int id);
        Quizzes_Table GetQuizById(int id);
        List<QuestionOrOpetionAddingModel> GetQuestions(int id);
        void UpdateQuestions(List<QuestionOrOpetionAddingModel> questions);

    }
}
