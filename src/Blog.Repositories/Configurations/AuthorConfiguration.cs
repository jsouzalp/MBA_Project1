using Blog.Entities.Authors;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Blog.Repositories.Configurations
{
    public class AuthorConfiguration : IEntityTypeConfiguration<Author>
    {
        public void Configure(EntityTypeBuilder<Author> builder)
        {
            #region Mapping columns
            builder.ToTable("TB_AUTHOR");
            builder.HasKey(x => x.Id)
                .HasName("PK_TB_AUTHOR");
            builder.Property(x => x.Id)
                .HasColumnName("AUTHOR_ID")
                .HasColumnType(DatabaseTypeConstant.UniqueIdentifier)
                .IsRequired();
            builder.Property(x => x.Name)
                .HasColumnName("NAME")
                .HasColumnType(DatabaseTypeConstant.Varchar)
                .HasMaxLength(1024)
                .IsRequired();
            #endregion

            #region Indexes
            #endregion

            #region Relationships
            builder.HasMany(x => x.Posts)
                .WithOne(x => x.Author)
                .HasForeignKey(x => x.AuthorId)
                .HasConstraintName("FK_TB_AUTHOR_001")
                .OnDelete(DeleteBehavior.Cascade);
            #endregion
        }
    }
}
