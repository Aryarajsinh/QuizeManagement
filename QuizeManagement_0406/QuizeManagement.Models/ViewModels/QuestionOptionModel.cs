using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuizeManagement.Models.ViewModels
{
    public class QuestionOptionModel
    {
        
       public int QuestionId { get; set; }
        public string QuestionText { get; set; }
        public string OptionText { get; set; }
        public bool IsCorrect { get; set; }
      
        public int QuizId { get; set; }

        public List<OpetionModel> Options { get; set; }

    }
}
