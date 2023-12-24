using LearningManagement.DBConfig;
using LearningManagement.Model;

namespace LearningManagement.IService;

public interface IForumService
{
    void CreateForum(Forum newForum);
    bool IsForumAvailableForSession(int sessionId);
    List<Forum> GetForum(int forumId);
    int AddContentToForumDtl(int forumId, string forumDtlContent);
    List<ForumDtl> GetForumDetailsByForumId(int forumId);
}
