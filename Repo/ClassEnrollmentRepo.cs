using LearningManagement.DBConfig;
using LearningManagement.DBConnection;
using LearningManagement.IRepo;
using LearningManagement.Model;
using Microsoft.EntityFrameworkCore;
using Npgsql;
using System;

namespace LearningManagement.Repo
{
    public class ClassEnrollmentRepo : IClassEnrollmentRepo
    {
        public int EnrollStudent(ClassEnrollment enrollment, DBContextConfig context)
        {
            context.ClassEnrollments.Add(enrollment);

            context.SaveChanges();

            return enrollment.Id;
        }

        public List<Class> GetUnenrolledClasses(int studentId, DBContextConfig context)
        {
            var unenrolledClasses = context.Classes
                .Where(c => !context.ClassEnrollments.Any(ce => ce.StudentId == studentId && ce.ClassId == c.Id))
                .ToList();

            return unenrolledClasses;
        }


        public List<Class> GetEnrolledClasses(int studentId, DBContextConfig context)
        {
            var enrolledClasses = context.ClassEnrollments
                .Where(ce => ce.StudentId == studentId)
                .Join(
                    context.Classes,
                    ce => ce.ClassId,
                    c => c.Id,
                    (ce, c) => c
                )
                .ToList();

            return enrolledClasses;
        }


        public List<Class> GetAllClasses(DBContextConfig context)
        {
            var allClasses = context.Classes
                    .ToList();

            //    List<Class> availableClasses = new List<Class>();
            //    const string query = "SELECT * FROM t_class";

            //    using (NpgsqlConnection connection = new NpgsqlConnection(ConnectionDB.ConnectionString))
            //    {
            //        connection.Open();
            //        using (NpgsqlCommand command = new NpgsqlCommand(query, connection))
            //        {
            //            using (NpgsqlDataReader reader = command.ExecuteReader())
            //            {
            //                while (reader.Read())
            //                {
            //                    Class classItem = new Class()
            //                    {
            //                        Id = (int)reader["id"],
            //                        ClassCode = (string)reader["class_code"],
            //                        ClassName = (string)reader["class_name"],
            //                        ClassDesc = (string)reader["class_desc"],
            //                    };

            //                    availableClasses.Add(classItem);
            //                }
            //            }
            //        }
            //    }

            return allClasses;
        }
    }
}

//List<Class> enrolledClasses = new List<Class>();

//const string query = "SELECT c.* FROM t_class c " +
//                     "JOIN t_class_enrollment ce ON c.id = ce.class_id " +
//                     "WHERE ce.student_id = @studentId";

//using (NpgsqlConnection connection = new NpgsqlConnection(ConnectionDB.ConnectionString))
//{
//    connection.Open();
//    using (NpgsqlCommand command = new NpgsqlCommand(query, connection))
//    {
//        command.Parameters.AddWithValue("@studentId", studentId);

//        using (NpgsqlDataReader reader = command.ExecuteReader())
//        {
//            while (reader.Read())
//            {
//                Class classItem = new Class()
//                {
//                    Id = (int)reader["id"],
//                    ClassCode = (string)reader["class_code"],
//                    ClassName = (string)reader["class_name"],
//                    ClassDesc = (string)reader["class_desc"],
//                };

//                enrolledClasses.Add(classItem);
//            }
//        }
//    }
//}