using Blog.API.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Blog.API.Repositories.TypeConfigurations
{
    public class CommentConfigurations : IEntityTypeConfiguration<Comment>
    {
        public void Configure(EntityTypeBuilder<Comment> builder)
        {
            builder.ToTable(nameof(Comment));

            builder.Property(comment => comment.Id).UseIdentityColumn();
            builder.Property(comment => comment.Content).HasColumnType("nvarchar(max)");
            builder.Property(comment => comment.CreatedAt).HasColumnType("datetime");

            builder.HasOne(comment => comment.Post);
            builder.Ignore(comment => comment.IsVisibleOnlyToWriter);
        }
    }
}
