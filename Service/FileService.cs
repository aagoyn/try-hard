using LearningManagement.IRepo;
using LearningManagement.Model;
using LearningManagement.IService;
using LearningManagement.DBConfig;
using LearningManagement.Helper;


namespace LearningManagement.Service;

public class FileService : IFileService
{
    private readonly IFileRepo fileRepo;
    private readonly SessionHelper sessionHelper;

    public FileService(IFileRepo fileRepo, SessionHelper sessionHelper)
    {
        this.fileRepo = fileRepo;
        this.sessionHelper = sessionHelper;
    }

    public int CreateFile(string fileTitle, string fileExtension)
    {
        using (var context = new DBContextConfig())
        {
            var file = new FileLms
            {
                FileTitle = fileTitle,
                FileExtension = fileExtension,
                CreatedBy = sessionHelper.UserId,
                CreatedAt = DateTime.Now,
                IsActive = true
            };

            return fileRepo.CreateFile(file, context);
        }
    }
}
