using Blog.API.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Blog.API.Repositories.TypeConfigurations
{
    public class PostConfiguration : IEntityTypeConfiguration<Post>
    {
        public void Configure(EntityTypeBuilder<Post> builder)
        {
            builder.ToTable(nameof(Post));

            builder.Property(post => post.Id).UseIdentityColumn();
            builder.Property(post => post.Title).HasColumnType("varchar(50)");
            builder.Property(post => post.Content).HasColumnType("nvarchar(max)");
            builder.Property(post => post.Status).HasColumnType("varchar(10)");
            builder.Property(post => post.CreatedAt).HasColumnType("datetime");

            builder.HasMany(post => post.Comments);
            builder.Ignore(post => post.PostStatusEnum);
            builder.Navigation(post => post.Comments).AutoInclude();

        }
    }
}
