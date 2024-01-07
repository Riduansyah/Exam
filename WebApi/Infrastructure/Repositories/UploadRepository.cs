using Domain.Entities;
using Domain.Interfaces;
using Infrastructure.Database;
using Infrastructure.Repositories.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repositories
{
    public class UploadRepository : RepositoryBase<FileBlob>, IUploadRepository
    {
        public UploadRepository(ApplicationDbContext applicationDbContext) : base(applicationDbContext)
        {
        }
    }
}
