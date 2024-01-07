using Domain.Entities;
using Domain.Interfaces;
using Infrastructure.Database;
using Infrastructure.Repositories.Base;

namespace Infrastructure.Repositories
{
    public class UserRepository : RepositoryBase<User>, IUserRepository
    {
        public UserRepository(ApplicationDbContext applicationDbContext) : base(applicationDbContext)
        {
        }

        public User GetById(Guid id)
        {
            throw new NotImplementedException();
        }
    }
}
