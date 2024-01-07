
using Domain.Entities;
using System;

namespace Domain.Interfaces
{
    public interface IUserRepository : IRepositoryBase<User>
    {
        User GetById(Guid id);
    }
}
