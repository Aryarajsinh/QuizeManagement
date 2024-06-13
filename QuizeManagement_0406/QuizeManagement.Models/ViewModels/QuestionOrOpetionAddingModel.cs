using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuizeManagement.Models.ViewModels
{
    public class QuestionOrOpetionAddingModel
    {
        [Key]
        public int quizId { get; set; }
        public int questionId { get; set; }

        [Required(ErrorMessage = "Enter the Value")]
        public string question { get; set; }

        [Required(ErrorMessage ="Select")]
        public string options1 { get; set; }

        [Required(ErrorMessage = "Select")]
        public string options2 { get; set; }

        [Required(ErrorMessage = "Select")]
        public string options3 { get; set; }

        [Required(ErrorMessage = "Select")]
        public string options4 { get; set; }

        [Required(ErrorMessage = "Select")]
        public string Answers { get; set; }
        public string UserAnswer { get; set; }
        public bool IsCorrect { get; set; }

        [Required]
        public List<Option> Options { get; set; }

        public class Option
        {
            public int Option_id { get; set; }
            public string Option_text { get; set; }
            public bool Is_correct { get; set; }
        }

    }
}
