using Blog.API.Models;

namespace Blog.API.Repositories
{
    public interface IUserRepository
    {
        User? GetById(long userId);
        User? GetByEmail(string email);
        void Insert(User user);
    }

    public class UserRepository : IUserRepository
    {
        private readonly BlogContext context;

        public UserRepository(BlogContext context)
        {
            this.context = context;
        }

        public User? GetByEmail(string email)
        {
            return context.Users.FirstOrDefault(u => u.Email == email);
        }

        public User? GetById(long userId)
        {
            return context.Users.FirstOrDefault(user => user.Id == userId);
        }

        public void Insert(User user)
        {
            context.Add(user);
            context.SaveChanges();
        }
    }
}
