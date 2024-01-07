using Application.Interfaces;
using Domain.Entities;
using Domain.Interfaces;

namespace Application.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;

        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public User Add(User model)
        {
            _userRepository.Add(model);
            return model;
        }

        public List<User> GetAllUsers()
        {
            return _userRepository.GetAll().ToList();
        }
    }
}
