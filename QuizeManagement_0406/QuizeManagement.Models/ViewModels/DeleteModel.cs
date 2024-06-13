using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuizeManagement.Models.ViewModels
{
    public class DeleteModel
    {
        public int Answer_id { get; set; }
        public Nullable<int> User_id { get; set; }
        public Nullable<int> Quiz_id { get; set; }
        public Nullable<int> Question_id { get; set; }
        public Nullable<int> Selected_Option_id { get; set; }
        public Nullable<System.DateTime> Created_at { get; set; }

        public int Result_id { get; set; }

        public Nullable<int> Score { get; set; }
   

     
        public string Question_txt { get; set; }

        public Nullable<System.DateTime> Updated_at { get; set; }

        public int Option_id { get; set; }
      
        public string Option_text { get; set; }
        public Nullable<bool> Is_correct { get; set; }


        public string Title { get; set; }
        public string Description { get; set; }
        public Nullable<int> Created_by { get; set; }


    }
}
