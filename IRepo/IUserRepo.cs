﻿using LearningManagement.DBConfig;
using LearningManagement.Model;


namespace LearningManagement.IRepo;

public interface IUserRepo
{
    User? GetUserByEmailPass(string email, string password, DBContextConfig context);
    int GetRoleIdByRoleCode(string roleCode, DBContextConfig context);
    List<User> GetAllUsers();
    List<User> GetAllLecturers();
    int RegisterStudent(User newStudent, DBContextConfig context);
    void RegisterLecturer(string fullName, string email, string autogeneratedPassword, int createdBy);
}