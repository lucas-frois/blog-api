using Blog.API.Repositories;
using Blog.API.Services;
using Microsoft.EntityFrameworkCore;

namespace Blog.API
{
    public static class DependencyInjection
    {
        public static void RegisterServices(this IServiceCollection services)
        {
            services.AddScoped<IPostRepository, PostRepository>();
            services.AddScoped<IPostService, PostService>();
        }

        public static void ConfigureDatabase(this IServiceCollection services, IConfiguration configuration)
        {
            var environment = configuration.GetSection("env").Value;
            var isProductionEnv = environment?.ToLower() == "prod";

            services.AddDbContextPool<BlogContext>(options =>
            {

                if (isProductionEnv)
                {
                    var connectionString = configuration.GetConnectionString("connstr");
                    options.UseSqlServer(connectionString);
                }

                options.UseInMemoryDatabase("inmemoryconnstr");
            });
        }
    }
}
