using Blog.API.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Blog.API.Repositories.TypeConfigurations
{
    public class AuthorConfiguration : IEntityTypeConfiguration<Author>
    {
        public void Configure(EntityTypeBuilder<Author> builder)
        {
            builder.ToTable(nameof(Author));

            builder.Property(post => post.Id).UseIdentityColumn();
            builder.Property(post => post.Name).HasColumnType("varchar(50)");
            builder.Property(post => post.Salt).HasColumnType("varchar(256)");
            builder.Property(post => post.Role).HasColumnType("bit(1)");
        }
    }
}
