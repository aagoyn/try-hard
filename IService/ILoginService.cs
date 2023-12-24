using LearningManagement.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LearningManagement.IService
{
    public interface ILoginService
    {
        User? Login(string email, string password);
        void SetLoggedInUser(User user);
        User GetLoggedInUser();
    }
}
