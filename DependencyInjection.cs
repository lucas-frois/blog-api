using Blog.API.Repositories;
using Blog.API.Services;

namespace Blog.API
{
    public static class DependencyInjection
    {
        public static void RegisterServices(this IServiceCollection services)
        {
            services.AddScoped<IPostRepository, PostRepository>();
            services.AddScoped<IPostService, PostService>();
        }

        public static void ConfigureDatabase()
        {

        }
    }
}
