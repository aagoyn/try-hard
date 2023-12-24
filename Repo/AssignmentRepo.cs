using LearningManagement.DBConnection;
using LearningManagement.Model;
using LearningManagement.IRepo;
using Npgsql;
using Microsoft.EntityFrameworkCore;
using LearningManagement.DBConfig;


namespace LearningManagement.Repo;

public class AssignmentRepo : IAssignmentRepo
{
    public int AddAssignment(Assignment newAssignment, DBContextConfig context)
    {

        context.Assignments.Add(newAssignment);
        context.SaveChanges();

        return newAssignment.Id;

        //const string query = @"
        //        INSERT INTO t_assignment (assignment_title, assignmnet_duration, session_id, created_by, created_at, ver, is_active)
        //        VALUES (@AssignmentTitle, @AssignmentDuration, @SessionId, @CreatedBy, NOW(), 1, TRUE) RETURNING id";

        //using (NpgsqlConnection connection = new NpgsqlConnection(ConnectionDB.ConnectionString))
        //{
        //    connection.Open();
        //    using (NpgsqlCommand command = new NpgsqlCommand(query, connection))
        //    {
        //        command.Parameters.AddWithValue("@AssignmentTitle", newAssignment.AssignmentTitle);
        //        command.Parameters.AddWithValue("@AssignmentDuration", newAssignment.AssignmentDuration);
        //        command.Parameters.AddWithValue("@SessionId", newAssignment.Session.Id);
        //        command.Parameters.AddWithValue("@CreatedBy", newAssignment.CreatedBy);

        //        int newMaterialId = Convert.ToInt32(command.ExecuteScalar());
        //        return newMaterialId;
        //    }
        //}
    }

    public Assignment GetAssignmentById(int assignmentId, DBContextConfig context)
    {
        return context.Assignments
            .FirstOrDefault(a => a.Id == assignmentId);
    }

    public List<Assignment> GetAssignmentsBySession(int sessionId, DBContextConfig context)
    {

        return context.Assignments
            .Where(a => a.SessionId == sessionId)
            .ToList();

        //List<Assignment> assignments = new List<Assignment>();
        //const string query = "SELECT id, assignment_title, assignment_duration " +
        //                     "FROM t_assignment " +
        //                     "WHERE session_id = @SessionId";

        //using (NpgsqlConnection connection = new NpgsqlConnection(ConnectionDB.ConnectionString))
        //{
        //    connection.Open();
        //    using (NpgsqlCommand command = new NpgsqlCommand(query, connection))
        //    {
        //        command.Parameters.AddWithValue("@SessionId", sessionId);

        //        using (NpgsqlDataReader reader = command.ExecuteReader())
        //        {
        //            while (reader.Read())
        //            {
        //                Assignment assignment = new Assignment
        //                {
        //                    Id = (int)reader["id"],
        //                    AssignmentTitle = (string)reader["assignment_title"],
        //                    AssignmentDuration = (int)reader["assignment_duration"],
        //                    Session = new Session { Id = sessionId }
        //                };

        //                assignments.Add(assignment);
        //            }
        //        }
        //    }
        //}
        //return assignments;
    }
}
