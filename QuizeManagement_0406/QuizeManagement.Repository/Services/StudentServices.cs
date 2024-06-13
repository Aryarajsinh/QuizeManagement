using QuizeManagement.Helpers.Helper;
using QuizeManagement.Helpers.SpHelper;
using QuizeManagement.Models.DbContext;
using QuizeManagement.Models.ViewModels;
using QuizeManagement.Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace QuizeManagement.Repository.Services
{
   public class StudentServices :IStudentInterfaces
    {
        QuizeManagement_0406Entities _context = new QuizeManagement_0406Entities();
       
        public bool AddUser(RegisterModel _registerModel)
        {

            string Username = _registerModel.Username;
            string password = HashPassword(_registerModel.Password);
            string Email = _registerModel.Email;


            Dictionary<string, object> _addUser = new Dictionary<string, object>
                {
                            { "@Username", Username},
                            { "@password", password },
                            { "@Email", Email }
                };
            Dictionary<string, object> _checkUser = new Dictionary<string, object>
                {
                            { "@Email", Email }
                };
            bool IsEmailNotExist = GenericRepository.IsEmailExist(SpHelper.CheckingEmailExistsOrNot, _checkUser);
            if (IsEmailNotExist)
            {
                GenericRepository.GetSingleDataTable(SpHelper.AddUser, _addUser);
                return true;
            }
            else
            {
                return false;
            }

        }

        public RegisterModel CheckUser(LoginModel _loginModel)
        {

            string password = HashPassword(_loginModel.Password);
            Dictionary<string, object> parameter = new Dictionary<string, object>
                {
                    {"@Email",_loginModel.Email },
                     {"@Password_hash",password }

                };

            RegisterModel _register = GenericRepository.CheckingUserIsValidOrNotLogin(SpHelper.CheckRegisterLogin, parameter);
            return _register; 
 
        } 
        
        public AdminModel CheckAdmin(LoginModel _loginModel)
        {

            //string password = HashPassword(_loginModel.Password);
            Dictionary<string, object> parameter = new Dictionary<string, object>
                {
                     {"@Email",_loginModel.Email },
                     {"@Password",_loginModel.Password }

                };

            AdminModel _admin = GenericRepository.CheckingAdminLogin(SpHelper.CheckAdminLogin, parameter);

            return _admin;
        }
           
        


        private string HashPassword(string password)
        {
            using (var sha256 = SHA256.Create())
            {

                var hashedBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
                return Convert.ToBase64String(hashedBytes);
            }
        }
    }
}
