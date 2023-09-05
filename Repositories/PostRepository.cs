using Blog.API.Models;

namespace Blog.API.Repositories
{
    public interface IPostRepository : IGenericRepository<Post> { }

    public class PostRepository : GenericRepository<Post>, IPostRepository
    {
        public PostRepository(BlogContext context) : base(context) { }
    }
}
