using LearningManagement.DBConfig;
using LearningManagement.DBConnection;
using LearningManagement.IRepo;
using LearningManagement.Model;
using Microsoft.EntityFrameworkCore;
using Npgsql;
using System;
using System.Collections.Generic;

namespace LearningManagement.Repo;

public class ForumRepo : IForumRepo
{
    public void CreateForum(Forum newForum)
    {
        const string query = "INSERT INTO t_forum (forum_title, forum_content, session_id, lecturer_id) " +
                             "VALUES (@ForumTitle, @ForumContent, @SessionId, @LecturerId)";

        using (NpgsqlConnection connection = new NpgsqlConnection(ConnectionDB.ConnectionString))
        {
            connection.Open();
            using (NpgsqlCommand command = new NpgsqlCommand(query, connection))
            {
                command.Parameters.AddWithValue("@ForumTitle", newForum.ForumTitle);
                command.Parameters.AddWithValue("@ForumContent", newForum.ForumContent);
                command.Parameters.AddWithValue("@SessionId", newForum.Session.Id);
                command.Parameters.AddWithValue("@LecturerId", newForum.Lecturer.Id);

                command.ExecuteNonQuery();
            }
        }
    }

    public int AddContentToForumDtl(ForumDtl newContent, DBContextConfig context)
    {
        context.ForumDtls.Add(newContent);
        context.SaveChanges();

        return newContent.Id;
    }

    public List<ForumDtl> GetForumDetailsByForumId(int forumId, DBContextConfig context)
    {

        var forumDetails = context.ForumDtls
            .Include(f => f.User)
            .Where(f => f.ForumId == forumId)
            .OrderBy(f => f.CreatedAt)
            .ToList();
        return forumDetails;
    }

    public List<Forum> GetForum(int sessionId, DBContextConfig context)
    {
        var forums = context.Forums
            .Include(f => f.Lecturer)
            .Where(f => f.SessionId == sessionId)
            .ToList();

        return forums;
    }


    //List<ForumDtl> forumDetails = new List<ForumDtl>();
    //    const string query = "SELECT forum_dtl.id, forum_dtl.user_id, forum_dtl.forum_dtl_content, " +
    //                         "t_user.fullname AS user_fullname " +
    //                         "FROM t_forum_dtl AS forum_dtl " +
    //                         "JOIN t_user ON forum_dtl.user_id = t_user.id " +
    //                         "WHERE forum_dtl.forum_id = @ForumId";

    //    using (NpgsqlConnection connection = new NpgsqlConnection(ConnectionDB.ConnectionString))
    //    {
    //        connection.Open();
    //        using (NpgsqlCommand command = new NpgsqlCommand(query, connection))
    //        {
    //            command.Parameters.AddWithValue("@ForumId", forumId);

    //            using (NpgsqlDataReader reader = command.ExecuteReader())
    //            {
    //                while (reader.Read())
    //                {
    //                    ForumDtl forumDetail = new ForumDtl
    //                    {
    //                        Id = (int)reader["id"],
    //                        User = new User
    //                        {
    //                            Id = (int)reader["user_id"],
    //                            Fullname = (string)reader["user_fullname"]
    //                        },
    //                        ForumDtlContent = (string)reader["forum_dtl_content"]
    //                    };

    //                    forumDetails.Add(forumDetail);
    //                }
    //            }
    //        }
    //    }

    //    return forumDetails;
    //}

}



//public List<ForumDtl> GetForumDetails(int sessionId)
//{
//    List<ForumDtl> forumDetails = new List<ForumDtl>();
//    const string query = "SELECT forum_dtl.id, forum_dtl.user_id, forum_dtl.forum_dtl_content, " +
//                         "t_user.fullname AS user_fullname " +
//                         "FROM t_forum_dtl AS forum_dtl " +
//                         "JOIN t_user ON forum_dtl.user_id = t_user.id " +
//                         "WHERE forum_dtl.session_id = @SessionId";

//    using (NpgsqlConnection connection = new NpgsqlConnection(ConnectionDB.ConnectionString))
//    {
//        connection.Open();
//        using (NpgsqlCommand command = new NpgsqlCommand(query, connection))
//        {
//            command.Parameters.AddWithValue("@SessionId", sessionId);

//            using (NpgsqlDataReader reader = command.ExecuteReader())
//            {
//                while (reader.Read())
//                {
//                    ForumDtl forumDetail = new ForumDtl
//                    {
//                        Id = (int)reader["id"],
//                        UserId = new User
//                        {
//                            Id = (int)reader["user_id"],
//                            Fullname = (string)reader["user_fullname"]
//                        },
//                        ForumContent = (string)reader["forum_dtl_content"]
//                    };

//                    forumDetails.Add(forumDetail);
//                }
//            }
//        }
//    }

//    return forumDetails;
//}




