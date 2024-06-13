using QuizeManagement.Models.DbContext;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuizeManagement.Models.ViewModels
{
    public class QuizViewModel
    {
        public Quizzes_Table Quiz { get; set; }
        public List<Question_Table> Questions { get; set; }
        public List<Options_Table> Options { get; set; }
    }
}
