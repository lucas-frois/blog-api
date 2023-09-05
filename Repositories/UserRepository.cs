using Blog.API.Models;

namespace Blog.API.Repositories
{
    public interface IUserRepository : IGenericRepository<User> { }

    public class UserRepository : GenericRepository<User>, IUserRepository
    {
        public UserRepository(BlogContext context) : base(context) { }
    }
}
