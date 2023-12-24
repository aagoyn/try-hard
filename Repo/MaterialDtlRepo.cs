using LearningManagement.DBConnection;
using LearningManagement.Model;
using LearningManagement.IRepo;
using Npgsql;
using Microsoft.EntityFrameworkCore;
using LearningManagement.DBConfig;

namespace LearningManagement.Repo;

public class MaterialDtlRepo : IMaterialDtlRepo
{
    public void AddMaterialDetails(MaterialDtl materialDtl, DBContextConfig context)
    {

        context.MaterialDtls.Add(materialDtl);
        context.SaveChanges();

        //const string query = @"
        //    INSERT INTO t_material_dtl (material_id, material_file, created_by, created_at, ver, is_active)
        //    VALUES (@MaterialId, @MaterialFile, @CreatedBy, NOW(), 1, TRUE)";

        //using (NpgsqlConnection connection = new NpgsqlConnection(ConnectionDB.ConnectionString))
        //{
        //    connection.Open();
        //    using (NpgsqlCommand command = new NpgsqlCommand(query, connection))
        //    {
        //        command.Parameters.AddWithValue("@MaterialId", materialDtl.Material.Id);
        //        command.Parameters.AddWithValue("@MaterialFile", materialDtl.MaterialFile.Id);
        //        command.Parameters.AddWithValue("@CreatedBy", createdBy);

        //        command.ExecuteNonQuery();
        //    }
        //}
    }
}
