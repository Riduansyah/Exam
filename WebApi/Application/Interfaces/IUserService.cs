using Domain.Entities;

namespace Application.Interfaces
{
    public interface IUserService
    {
        List<User> GetAllUsers();
        User Add(User model);
    }
}
