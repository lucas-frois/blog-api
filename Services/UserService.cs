using Blog.API.Models;
using Blog.API.Repositories;

namespace Blog.API.Services
{
    public interface IUserService
    {
        Task Create(string name, string email, string password, string role);
        Task<string> Authenticate(string email, string password);
        Task<User> GetUser(long userId);
    }

    public class UserService : IUserService
    {
        private readonly IUserRepository userRepository;

        public UserService(IUserRepository userRepository)
        {
            this.userRepository = userRepository;
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
                PasswordHash = hash,
                Role = role
            };

            userRepository.Insert(user);
        }

        public async Task<string> Authenticate(string email, string password)
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

            var token = EncryptionService.GenerateJwtToken(user);

            return token;
        }

        public async Task<User> GetUser(long userId)
        {
            return userRepository.GetById(userId);
        }
    }
}
