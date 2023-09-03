using Blog.API.Models;
using System.Data.Entity;

namespace Blog.API.Repositories
{
    public interface IPostRepository
    {
        IList<Post> GetAll(int page, int size);
        Post? GetById(long postId);
        void Insert(Post post);
        void Update(Post post);
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
            var posts = context.Posts
                .Skip((page - 1) * size)
                .Take(size)
                .AsNoTracking()
                .ToList();
            
            return posts;
        }

        public Post? GetById(long postId)
        {
            return context.Posts.FirstOrDefault(post => post.Id == postId);
        }

        public void Insert(Post post)
        {
            context.Posts.Add(post);
            context.SaveChanges();
        }

        public void Update(Post post)
        {
            context.SaveChanges();
        }
    }
}
