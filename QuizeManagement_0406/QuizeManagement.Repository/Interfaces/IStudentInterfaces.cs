using QuizeManagement.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuizeManagement.Repository.Interfaces
{
    public interface IStudentInterfaces
    {
         bool AddUser(RegisterModel _registerModel);
        RegisterModel CheckUser(LoginModel _loginModel);
        AdminModel CheckAdmin(LoginModel _loginModel);

    }
}
