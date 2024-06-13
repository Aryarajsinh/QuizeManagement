using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuizeManagement.Models.ViewModels
{
    public class StudentModel
    {
        [Key]
        public int User_id { get; set; }

        [Required(ErrorMessage = "enter Username")]
        [MaxLength(15, ErrorMessage = "Username must be 15 character")]
        [MinLength(4, ErrorMessage = "Username must be 4 character")]
        [RegularExpression(@"^[a-z]+[0-9]+$", ErrorMessage = "Invalid Username.")]
        public string Username { get; set; }

        [Required(ErrorMessage = "enter Email")]
        [RegularExpression(@"^[a-zA-Z0-9_.%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$", ErrorMessage = "Invalid email id.")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Enter Password")]
        [MinLength(8, ErrorMessage = "Password must be 8 character")]
        public string Password { get; set; }

        public Nullable<System.DateTime> Created_at { get; set; }
        public Nullable<System.DateTime> Updated_at { get; set; }
    }
}
