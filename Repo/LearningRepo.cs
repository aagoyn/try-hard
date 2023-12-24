using LearningManagement.DBConfig;
using LearningManagement.DBConnection;
using LearningManagement.IRepo;
using LearningManagement.Model;
using Microsoft.EntityFrameworkCore;
using Npgsql;
using System.Data;

namespace LearningManagement.Repo;

public class LearningRepo : ILearningRepo
{
    //public void AddLearning(int classId, string day, DateTime learningDate, int CreatedBy)
    //{
    //    const string query = "INSERT INTO t_learning (class_id, day_name, learning_date, created_by, created_at, ver, is_active) " +
    //                         "VALUES (@ClassId, @LearningDay, @LearningDate, @CreatedBy, NOW(), 1, TRUE)";

    //    using (NpgsqlConnection connection = new NpgsqlConnection(ConnectionDB.ConnectionString))
    //    {
    //        connection.Open();
    //        using (NpgsqlCommand command = new NpgsqlCommand(query, connection))
    //        {
    //            command.Parameters.AddWithValue("@ClassId", classId);
    //            command.Parameters.AddWithValue("@LearningDay", day);
    //            command.Parameters.AddWithValue("@LearningDate", learningDate);
    //            command.Parameters.AddWithValue("@CreatedBy", CreatedBy);


    //            command.ExecuteNonQuery();
    //        }
    //    }
    //}

    public void AddLearning(int classId, string day, DateOnly learningDate, int createdBy, DBContextConfig context)
    {
        var learning = new Learning()
        {
            ClassId = classId,
            DayName = day,
            LearningDate = learningDate,
            CreatedBy = createdBy,
            CreatedAt = DateTime.Now,
            Ver = 1,
            IsActive = true
        };

        context.Learnings.Add(learning);
        context.SaveChanges();
    }


    public List<Learning> GetLearningByLecturer(int lecturerId, DBContextConfig context)
    {
        var learnings = context.Learnings
                                 .Include(l => l.Class)
                                 .ThenInclude(c => c.Lecturer)
                                 .Where(l => l.Class.LecturerId == lecturerId)
                                 .Select(l => new Learning
                                 {
                                     Id = l.Id,
                                     DayName = l.DayName,
                                     LearningDate = l.LearningDate,
                                     Class = new Class
                                     {
                                         Id = l.Class.Id,
                                         ClassName = l.Class.ClassName,
                                         Lecturer = new User
                                         {
                                             Id = l.Class.Lecturer.Id,
                                             Fullname = l.Class.Lecturer.Fullname
                                         }
                                     }
                                 })
                                 .ToList();
        return learnings;

        //List<Learning> learnings = new List<Learning>();
        //const string query = "SELECT learning.id AS learning_id, learning.day_name, learning.learning_date, " +
        //             "t_class.id AS class_id, t_class.class_name, t_class.lecturer_id, " +
        //             "t_user.fullname AS lecturer_name " +
        //             "FROM t_learning AS learning " +
        //             "JOIN t_class AS t_class ON learning.class_id = t_class.id " +
        //             "JOIN t_user AS t_user ON t_class.lecturer_id = t_user.id " +
        //             "WHERE t_class.lecturer_id = @LecturerId";

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
        //                Learning learning = new Learning
        //                {
        //                    Id = (int)reader["learning_id"],
        //                    DayName = (string)reader["day_name"],
        //                    LearningDate = reader.GetFieldValue<DateOnly>("learning_date"),
        //                    Class = new Class
        //                    {
        //                        Id = (int)reader["class_id"],
        //                        ClassName = (string)reader["class_name"],
        //                        Lecturer = new User
        //                        {
        //                            Id = (int)reader["lecturer_id"],
        //                            Fullname = (string)reader["lecturer_name"]
        //                        }

        //                    }

        //                };

        //                learnings.Add(learning);
        //            }
        //        }
        //    }
        //}

        //return learnings;
    }

    public List<Learning> GetLearningByClass(int classId, DBContextConfig context)
    {

        var learnings = context.Learnings
                                 .Include(l => l.Class)
                                 .ThenInclude(c => c.Lecturer)
                                 .Where(l => l.ClassId == classId)
                                 .Select(l => new Learning
                                 {
                                     Id = l.Id,
                                     DayName = l.DayName,
                                     LearningDate = l.LearningDate,
                                     Class = new Class
                                     {
                                         Id = l.Class.Id,
                                         ClassName = l.Class.ClassName,
                                         Lecturer = new User
                                         {
                                             Id = l.Class.Lecturer.Id,
                                             Fullname = l.Class.Lecturer.Fullname
                                         }
                                     }
                                 })
                                 .ToList();
        return learnings;

        //List<Learning> learnings = new List<Learning>();
        //const string query = "SELECT learning.id AS learning_id, learning.day_name, learning.learning_date, " +
        //                     "t_class.id AS class_id, t_class.class_name, t_class.lecturer_id, " +
        //                     "t_user.fullname AS lecturer_name " +
        //                     "FROM t_learning AS learning " +
        //                     "JOIN t_class AS t_class ON learning.class_id = t_class.id " +
        //                     "JOIN t_user AS t_user ON t_class.lecturer_id = t_user.id " +
        //                     "WHERE t_class.id = @ClassId";

        //using (NpgsqlConnection connection = new NpgsqlConnection(ConnectionDB.ConnectionString))
        //{
        //    connection.Open();
        //    using (NpgsqlCommand command = new NpgsqlCommand(query, connection))
        //    {
        //        command.Parameters.AddWithValue("@ClassId", classId);

        //        using (NpgsqlDataReader reader = command.ExecuteReader())
        //        {
        //            while (reader.Read())
        //            {
        //                Learning learning = new Learning
        //                {
        //                    Id = (int)reader["learning_id"],
        //                    DayName = (string)reader["day_name"],
        //                    LearningDate = reader.GetFieldValue<DateOnly>("learning_date"),
        //                    Class = new Class
        //                    {
        //                        Id = (int)reader["class_id"],
        //                        ClassName = (string)reader["class_name"],
        //                        Lecturer = new User
        //                        {
        //                            Id = (int)reader["lecturer_id"],
        //                            Fullname = (string)reader["lecturer_name"]
        //                        }
        //                    }
        //                };

        //                learnings.Add(learning);
        //            }
        //        }
        //    }
        //}

        //return learnings;
    }

}
