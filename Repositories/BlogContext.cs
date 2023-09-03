using Blog.API.Models;
using Microsoft.EntityFrameworkCore;

namespace Blog.API.Repositories
{
    public class BlogContext : DbContext
    {
        public BlogContext(DbContextOptions<BlogContext> options) : base(options) { }
        public DbSet<Post> Posts { get; set; }
        public DbSet<User> Authors { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfigurationsFromAssembly(typeof(BlogContext).Assembly);
        }
    }
}
