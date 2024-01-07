using Domain.Entities;

namespace Application.Interfaces
{
    public interface IUploadService
    {
        List<FileBlob> GetAllDocument();
        bool UploadFiles(FileBlob files);
    }
}
