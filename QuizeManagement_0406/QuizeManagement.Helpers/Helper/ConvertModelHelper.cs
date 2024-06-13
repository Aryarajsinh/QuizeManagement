using QuizeManagement.Models.DbContext;
using QuizeManagement.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuizeManagement.Helpers.Helper
{
    public class ConvertModelHelper
    {
        public static User_Table convertRegisterDemoModelToRegisterDemo(RegisterModel user)
        {
            User_Table _registerDemoModel = new User_Table();

            _registerDemoModel.User_id = user.User_id;
            _registerDemoModel.Username = user.Username;
            _registerDemoModel.Email = user.Email;
            _registerDemoModel.Password_hash = user.Password;

            return _registerDemoModel;
        }    
        public static Admin_Table convertRegisterModelToRegister(RegisterModel user)
        {
            Admin_Table _registerDemoModel = new Admin_Table();

            _registerDemoModel.Admin_id = user.User_id;
            _registerDemoModel.Username = user.Username;
            _registerDemoModel.Email = user.Email;
            _registerDemoModel.Password = user.Password;

            return _registerDemoModel;
        }
    }
}
