using LearningManagement.DBConfig;
using LearningManagement.Model;

namespace LearningManagement.IService;

public interface IForumService
{
    void CreateForum(Forum newForum);
    List<Forum> GetForum(int forumId);
    int AddContentToForumDtl(int forumId, string forumDtlContent);
    List<ForumDtl> GetForumDetailsByForumId(int forumId);
}
