using Blog.API.Models;

namespace Blog.API.Repositories
{
    public interface IPostRepository
    {
        IList<Post> GetAll(int page, int size);
        void Insert(Post post);
    }

    public class PostRepository : IPostRepository
    {
        private readonly BlogContext context;

        public PostRepository(BlogContext context)
        {
            this.context = context;
        }

        public IList<Post> GetAll(int page, int size)
        {
            var posts = context.Posts.Skip((page - 1) * size).Take(size).ToList();
            
            return posts;
        }

        public void Insert(Post post)
        {
            context.Posts.Add(post);
            context.SaveChanges();
        }
    }
}
