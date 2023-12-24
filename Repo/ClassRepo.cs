using LearningManagement.DBConfig;
using LearningManagement.DBConnection;
using LearningManagement.IRepo;
using LearningManagement.Model;
using Microsoft.EntityFrameworkCore;
using Npgsql;
using System;

namespace LearningManagement.Repo
{
    public class ClassRepo : IClassRepo
    {
        public int AddClass(Class newClass, int lecturerId, DBContextConfig context)
        {
            context.Classes.Add(newClass);
            context.SaveChanges();

            return newClass.Id;

            //const string query = "INSERT INTO t_class (class_name, class_code, class_desc, class_photo, lecturer_id, created_by, created_at, ver, is_active) " +
            //                     "VALUES (@ClassName, @ClassCode, @ClassDesc, @ClassPhoto, @LecturerId, @CreatedBy, NOW(), 1, TRUE) RETURNING id";

            //using (NpgsqlConnection connection = new NpgsqlConnection(ConnectionDB.ConnectionString))
            //{
            //    connection.Open();
            //    using (NpgsqlCommand command = new NpgsqlCommand(query, connection))
            //    {
            //        command.Parameters.AddWithValue("@ClassName", newClass.ClassName);
            //        command.Parameters.AddWithValue("@ClassCode", newClass.ClassCode);
            //        command.Parameters.AddWithValue("@ClassDesc", newClass.ClassDesc);
            //        command.Parameters.AddWithValue("@ClassPhoto", newClass.ClassPhoto.Id);
            //        command.Parameters.AddWithValue("@LecturerId", selectedLecturerId);
            //        command.Parameters.AddWithValue("@CreatedBy", CreatedBy);

            //        int newClassId = Convert.ToInt32(command.ExecuteScalar());
            //        return newClassId;
            //    }
            //}
        }

        public List<Class> GetAllClasses(DBContextConfig context)
        {
            var availableClasses = context.Classes.ToList();
            return availableClasses;


            //List<Class> availableClasses = new List<Class>();
            //const string query = "SELECT * FROM t_class";

            //using (NpgsqlConnection connection = new NpgsqlConnection(ConnectionDB.ConnectionString))
            //{
            //    connection.Open();
            //    using (NpgsqlCommand command = new NpgsqlCommand(query, connection))
            //    {
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

            //                availableClasses.Add(classItem);
            //            }
            //        }
            //    }
            //}

            //return availableClasses;
        }

        public Class GetClassById(int classId, DBContextConfig context)
        {
            var classesById = context.Classes.FirstOrDefault(c => c.Id == classId);
            return classesById;

            //Class classes = null;
            //const string query = "SELECT * FROM t_class WHERE id = @ClassId";

            //using (NpgsqlConnection connection = new NpgsqlConnection(ConnectionDB.ConnectionString))
            //{
            //    connection.Open();
            //    using (NpgsqlCommand command = new NpgsqlCommand(query, connection))
            //    {
            //        command.Parameters.AddWithValue("@ClassId", classId);

            //        using (NpgsqlDataReader reader = command.ExecuteReader())
            //        {
            //            if (reader.Read())
            //            {
            //                classes = new Class()
            //                {
            //                    Id = (int)reader["id"],
            //                    ClassName = (string)reader["class_name"],
            //                    ClassCode = (string)reader["class_code"],
            //                };
            //            }
            //        }
            //    }
            //}

            //return classes;
        }

        public List<Class> GetClassesAssignedToLecturer(int lecturerId, DBContextConfig context)
        {

            var lecturerClasses = context.Classes
                                       .Where(c => c.LecturerId == lecturerId)
                                       .ToList();
            return lecturerClasses;

            //List<Class> lecturerClasses = new List<Class>();
            //const string query = "SELECT * FROM t_class WHERE lecturer_id = @LecturerId";

            //using (NpgsqlConnection connection = new NpgsqlConnection(ConnectionDB.ConnectionString))
            //{
            //    connection.Open();
            //    using (NpgsqlCommand command = new NpgsqlCommand(query, connection))
            //    {
            //        command.Parameters.AddWithValue("@LecturerId", lecturerId);

            //        using (NpgsqlDataReader reader = command.ExecuteReader())
            //        {
            //            while (reader.Read())
            //            {
            //                Class classLecturer = new Class()
            //                {
            //                    Id = (int)reader["id"],
            //                    ClassCode = (string)reader["class_code"],
            //                    ClassName = (string)reader["class_name"],
            //                    ClassDesc = (string)reader["class_desc"],
            //                    Lecturer = new User() { Id = (int)reader["lecturer_id"] },
            //                };

            //                lecturerClasses.Add(classLecturer);
            //            }
            //        }
            //    }
            //}

            //return lecturerClasses;
        }

        public void AssignLecturerToClass(int lecturerId, int classId, DBContextConfig context)
        {
            var assignClass = context.Classes
                .FirstOrDefault(c => c.Id == classId);

            if (assignClass != null)
            {
                assignClass.LecturerId = lecturerId;
                context.SaveChanges();
            }

            //const string query = "UPDATE t_class SET lecturer_id = @LecturerId WHERE id = @ClassId";

            //using (NpgsqlConnection connection = new NpgsqlConnection(ConnectionDB.ConnectionString))
            //{
            //    connection.Open();
            //    using (NpgsqlCommand command = new NpgsqlCommand(query, connection))
            //    {
            //        command.Parameters.AddWithValue("@LecturerId", lecturerId);
            //        command.Parameters.AddWithValue("@ClassId", classId);

            //        command.ExecuteNonQuery();
            //    }
            //}
        }
    }
}
