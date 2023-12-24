using LearningManagement.DBConnection;
using LearningManagement.Model;
using LearningManagement.IRepo;
using Npgsql;

namespace LearningManagement.Repo;



public class MaterialRepo : IMaterialRepo
{
    public int AddMaterial(Material newMaterial, int CreatedBy)
    {
        const string query = @"
                INSERT INTO t_material (material_title, material_content, session_id, created_by, created_at, ver, is_active)
                VALUES (@MaterialTitle, @MaterialContent, @SessionId, @CreatedBy, NOW(), 1, TRUE) RETURNING id";

        using (NpgsqlConnection connection = new NpgsqlConnection(ConnectionDB.ConnectionString))
        {
            connection.Open();
            using (NpgsqlCommand command = new NpgsqlCommand(query, connection))
            {
                command.Parameters.AddWithValue("@MaterialTitle", newMaterial.MaterialTitle);
                command.Parameters.AddWithValue("@MaterialContent", newMaterial.MaterialContent);
                command.Parameters.AddWithValue("@SessionId", newMaterial.Session.Id);
                command.Parameters.AddWithValue("@CreatedBy", CreatedBy);

                int newMaterialId = Convert.ToInt32(command.ExecuteScalar());
                return newMaterialId;
            }
        }
    }

    public bool IsMaterialAvailableForSession(int sessionId)
    {
        const string query = "SELECT COUNT(*) FROM t_material WHERE session_id = @SessionId";

        using (NpgsqlConnection connection = new NpgsqlConnection(ConnectionDB.ConnectionString))
        {
            connection.Open();
            using (NpgsqlCommand command = new NpgsqlCommand(query, connection))
            {
                command.Parameters.AddWithValue("@SessionId", sessionId);

                int materialCount = Convert.ToInt32(command.ExecuteScalar());

                // Return true if material is available, false otherwise
                return materialCount > 0;
            }
        }


    }
    public List<Material> GetMaterialBySession(int sessionId)
    {
        List<Material> materials = new List<Material>();
        const string query = "SELECT id, material_title, material_content, session_id FROM t_material WHERE session_id = @SessionId";

        using (NpgsqlConnection connection = new NpgsqlConnection(ConnectionDB.ConnectionString))
        {
            connection.Open();
            using (NpgsqlCommand command = new NpgsqlCommand(query, connection))
            {
                command.Parameters.AddWithValue("@SessionId", sessionId);

                using (NpgsqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Material material = new Material
                        {
                            Id = (int)reader["id"],
                            MaterialTitle = (string)reader["material_title"],
                            MaterialContent = (string)reader["material_content"],
                            Session = new Session() { Id = (int)reader["session_id"] }
                        };

                        materials.Add(material);
                    }
                }
            }
        }

        return materials;
    }
}
