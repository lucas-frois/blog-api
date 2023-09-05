using Blog.API.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Blog.API.Repositories.TypeConfigurations
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.ToTable(nameof(User));

            builder.Property(post => post.Id).UseIdentityColumn();
            builder.Property(post => post.Name).HasColumnType("varchar(50)");
            builder.Property(post => post.Salt).HasColumnType("varchar(256)");
            builder.Property(post => post.Role).HasColumnType("varchar(10)");

            builder.HasMany(user => user.Posts);
            builder.Ignore(user => user.UserRoleEnum);
        }
    }
}
