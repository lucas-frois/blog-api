using Blog.API.Models;
using Blog.API.Repositories;

namespace Blog.API.Services
{
    public interface IUserService
    {
        Task Create(string name, string email, string password, string role);
        Task<string> Authenticate(string email, string password);
        Task<User> GetUser(long userId);
        Task<User> GetUserByEmail(string email);
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

            role = role.ToUpper();

            if (!Enum.TryParse(role, out UserRoleEnum roleEnum))
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
            var user = userRepository.GetByCondition(user => user.Email == email);

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

        public async Task<User> GetUserByEmail(string email)
        {
            return userRepository.GetByCondition(user => user.Email == email);
        }
    }
}
