using LearningManagement.DBConfig;
using LearningManagement.Helper;
using LearningManagement.IRepo;
using LearningManagement.IService;
using LearningManagement.Model;
using LearningManagement.Repo;


namespace LearningManagement.Service;

public class ForumService : IForumService
{
    private readonly IForumRepo forumRepo;
    private readonly DBContextConfig context;
    private readonly SessionHelper sessionHelper;

    public ForumService(IForumRepo forumRepo, SessionHelper sessionHelper)
    {
        this.forumRepo = forumRepo;
        this.sessionHelper = sessionHelper;
        context = new DBContextConfig();
    }
    public void CreateForum(Forum newForum)
    {
        forumRepo.CreateForum(newForum);
    }
    public List<Forum> GetForum(int sessionId)
    {
        return forumRepo.GetForum(sessionId, context);
    }

    public List<ForumDtl> GetForumDetailsByForumId(int forumId)
    {
        return forumRepo.GetForumDetailsByForumId(forumId, context);
    }

    public int AddContentToForumDtl(int forumId, string forumDtlContent)
    {
        var newContent = new ForumDtl
        {
            ForumId = forumId,
            ForumDtlContent = forumDtlContent,
            UserId = sessionHelper.UserId,
            CreatedBy = sessionHelper.UserId,
            CreatedAt = DateTime.Now,
            IsActive = true,
        };

        return forumRepo.AddContentToForumDtl(newContent, context);
    }
}
