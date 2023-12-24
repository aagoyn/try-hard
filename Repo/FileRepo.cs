using LearningManagement.Model;
using LearningManagement.DBConnection;
using Npgsql;
using LearningManagement.IRepo;
using LearningManagement.DBConfig;

namespace LearningManagement.Repo;

public class FileRepo : IFileRepo
{

    public int CreateFile(FileLms file, DBContextConfig context)
    {
        context.LmsFiles.Add(file);
        context.SaveChanges();

        return file.Id;
    }

    //public int CreateFile(FileLms file)
    //{
    //    const string query = "INSERT INTO t_file (file_title, file_extension, created_by, created_at, ver, is_active) " +
    //                         "VALUES (@FileContent, @FileExtension, @CreatedBy, @CreatedAt, 1, TRUE) RETURNING id";

    //    using (NpgsqlConnection connection = new NpgsqlConnection(ConnectionDB.ConnectionString))
    //    {
    //        using (NpgsqlCommand command = new NpgsqlCommand(query, connection))
    //        {
    //            command.Parameters.AddWithValue("@FileContent", file.FileTitle);
    //            command.Parameters.AddWithValue("@FileExtension", file.FileExtension);
    //            command.Parameters.AddWithValue("@CreatedBy", file.CreatedBy);
    //            command.Parameters.AddWithValue("@CreatedAt", file.CreatedAt);

    //            connection.Open();
    //            int newFileId = (int)command.ExecuteScalar();
    //            return newFileId;
    //        }
    //    }
    //}

}
