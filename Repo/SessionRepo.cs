using LearningManagement.DBConfig;
using LearningManagement.DBConnection;
using LearningManagement.IRepo;
using LearningManagement.Model;
using Microsoft.EntityFrameworkCore;
using Npgsql;
using System;

namespace LearningManagement.Repo
{
    public class SessionRepo : ISessionRepo
    {

        public Attendance? GetSessionAttendanceStatus(int sessionId, int studentId, DBContextConfig context)
        {
            var attendance = context.Attendances
                                    .Where(sa => sa.StudentId == studentId && sa.SessionId == sessionId)
                                    .FirstOrDefault();
            return attendance;
        }

        public Attendance RequestAttendance(Attendance attendance, DBContextConfig context)
        {
            context.Attendances.Add(attendance);
            context.SaveChanges();

            return attendance;
        }

        public int AddSession(Session newSession, int CreatedBy)
        {
            const string query = "INSERT INTO t_session (learning_id, session_title, session_start, session_end, created_by, created_at, ver, is_active) " +
                                 "VALUES (@LearningId, @SessionTitle, @SessionStart, @SessionEnd, @CreatedBy, NOW(), 1, TRUE) RETURNING id";

            using (NpgsqlConnection connection = new NpgsqlConnection(ConnectionDB.ConnectionString))
            {
                connection.Open();
                using (NpgsqlCommand command = new NpgsqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@LearningId", newSession.Learning.Id);
                    command.Parameters.AddWithValue("@SessionTitle", newSession.SessionTitle);
                    command.Parameters.AddWithValue("@SessionStart", newSession.SessionStart);
                    command.Parameters.AddWithValue("@SessionEnd", newSession.SessionEnd);
                    command.Parameters.AddWithValue("@CreatedBy", CreatedBy);


                    int newSessionId = Convert.ToInt32(command.ExecuteScalar());
                    return newSessionId;
                }
            }
        }
        public List<Session> GetSessionsByLecturer(int lecturerId)
        {
            const string query = @"SELECT s.id AS session_id, 
                                  s.session_title, 
                                  s.session_start, 
                                  s.session_end, 
                                  l.id AS learning_id, 
                                  l.day_name, 
                                  l.learning_date, 
                                  c.class_name,
                                  c.id AS class_id
                           FROM t_session s
                           JOIN t_learning l ON s.learning_id = l.id
                           JOIN t_class c ON l.class_id = c.id
                           WHERE c.lecturer_id = @LecturerId";

            using (NpgsqlConnection connection = new NpgsqlConnection(ConnectionDB.ConnectionString))
            {
                connection.Open();
                using (NpgsqlCommand command = new NpgsqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@LecturerId", lecturerId);

                    using (NpgsqlDataReader reader = command.ExecuteReader())
                    {
                        List<Session> sessions = new List<Session>();
                        while (reader.Read())
                        {
                            Session session = new Session
                            {
                                Id = reader.GetInt32(reader.GetOrdinal("session_id")),
                                Learning = new Learning
                                {
                                    Id = reader.GetInt32(reader.GetOrdinal("learning_id")),
                                    DayName = reader.GetString(reader.GetOrdinal("day_name")),
                                    LearningDate = reader.GetFieldValue<DateOnly>(reader.GetOrdinal("learning_date")),
                                    Class = new Class
                                    {
                                        Id = reader.GetInt32(reader.GetOrdinal("class_id")),
                                        ClassName = reader.GetString(reader.GetOrdinal("class_name")),
                                    }
                                },
                                SessionTitle = reader.GetString(reader.GetOrdinal("session_title")),
                                SessionStart = reader.GetTimeSpan(reader.GetOrdinal("session_start")),
                                SessionEnd = reader.GetTimeSpan(reader.GetOrdinal("session_end"))
                            };

                            sessions.Add(session);
                        }

                        return sessions;
                    }
                }
            }
        }

        public List<Session> GetSessionsByLearning(int learningId)
        {
            const string query = @"SELECT id AS session_id, 
                                  session_title, 
                                  session_start, 
                                  session_end
                           FROM t_session
                           WHERE learning_id = @LearningId";

            using (NpgsqlConnection connection = new NpgsqlConnection(ConnectionDB.ConnectionString))
            {
                connection.Open();
                using (NpgsqlCommand command = new NpgsqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@LearningId", learningId);

                    using (NpgsqlDataReader reader = command.ExecuteReader())
                    {
                        List<Session> sessions = new List<Session>();
                        while (reader.Read())
                        {
                            Session session = new Session
                            {
                                Id = reader.GetInt32(reader.GetOrdinal("session_id")),
                                SessionTitle = reader.GetString(reader.GetOrdinal("session_title")),
                                SessionStart = reader.GetTimeSpan(reader.GetOrdinal("session_start")),
                                SessionEnd = reader.GetTimeSpan(reader.GetOrdinal("session_end"))
                            };

                            sessions.Add(session);
                        }

                        return sessions;
                    }
                }
            }
        }


    }
}





