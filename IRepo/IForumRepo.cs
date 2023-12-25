using LearningManagement.DBConfig;
using LearningManagement.Model;

namespace LearningManagement.IRepo;

public interface IForumRepo
{
    void CreateForum(Forum newForum);
    List<Forum> GetForum(int sessionId, DBContextConfig context);
    int AddContentToForumDtl(ForumDtl newContent, DBContextConfig context);
    List<ForumDtl> GetForumDetailsByForumId(int forumId, DBContextConfig context);
}
