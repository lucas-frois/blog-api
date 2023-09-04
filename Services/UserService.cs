using Blog.API.Models;
using Blog.API.Repositories;

namespace Blog.API.Services
{
    public interface IUserService
    {
        Task Authenticate(string email, string password);
        Task Create(string name, string email, string password, string role);
    }

    public class UserService : IUserService
    {
        private readonly IUserRepository userRepository;

        public UserService(IUserRepository userRepository)
        {
            this.userRepository = userRepository;
        }

        public async Task Authenticate(string email, string password)
        {
            var user = userRepository.GetByEmail(email);

            if (user == null)
            {
                throw new Exception();
            }

            
            var isCorrectPassword = EncryptionService.VerifyPassword(password, user.PasswordHash, user.Salt);

            if (!isCorrectPassword)
            {
                throw new Exception();
            }

        }

        public async Task Create(string name, string email, string password, string role)
        {
            var hash = EncryptionService.HashPasword(password, out var salt);

            if (!Enum.TryParse(role, out UserRole roleEnum))
            {
                throw new Exception();
            }

            var user = new User
            {
                Name = name,
                Email = email,
                Salt = salt, 
                Role = role
            };

            userRepository.Insert(user);
        }
    }
}
