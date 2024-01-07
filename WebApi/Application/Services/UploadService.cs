using Application.Interfaces;
using Domain.Entities;
using Domain.Interfaces;

namespace Application.Services
{
    public class UploadService : IUploadService
    {
        private readonly IUploadRepository _fileUploadRepository;

        public UploadService(IUploadRepository fileUploadRepository)
        {
            _fileUploadRepository = fileUploadRepository;
        }

        public List<FileBlob> GetAllDocument()
        {
            return _fileUploadRepository.GetAll().ToList();
        }

        public bool UploadFiles(FileBlob fileBlob)
        {
            _fileUploadRepository.Add(fileBlob);
            return true;
        }
    }
}
