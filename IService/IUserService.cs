using LearningManagement.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LearningManagement.IService;

public interface IUserService
{
    int RegisterStudent(string fullName, string email, string password, int? photoProfileId);
    void RegisterLecturer(string fullName, string email, int createdBy);
    List<User> GetAllLecturers();

}
