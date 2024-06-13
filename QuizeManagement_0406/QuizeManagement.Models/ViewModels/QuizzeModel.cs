using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuizeManagement.Models.ViewModels
{
  public class QuizzeModel
    {

        public int Quiz_id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public Nullable<int> Created_by { get; set; }
        public Nullable<System.DateTime> Created_at { get; set; }
        public Nullable<System.DateTime> Updated_at { get; set; }
    }
}
