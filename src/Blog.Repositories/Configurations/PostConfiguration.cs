using Blog.Entities.Posts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Blog.Repositories.Configurations
{
    public class PostConfiguration : IEntityTypeConfiguration<Post>
    {
        public void Configure(EntityTypeBuilder<Post> builder)
        {
            #region Mapping columns
            builder.ToTable("TB_POST");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id)
                .HasColumnName("POST_ID")
                .HasColumnType(DatabaseTypeConstant.UniqueIdentifier)
                .IsRequired();
            builder.Property(x => x.AuthorId)
                .HasColumnName("AUTHOR_ID")
                .HasColumnType(DatabaseTypeConstant.UniqueIdentifier)
                .IsRequired();
            builder.Property(x => x.Date)
                .HasColumnName("DATE")
                .HasColumnType(DatabaseTypeConstant.DateTime)
                .IsRequired();
            builder.Property(x => x.Title)
                .HasColumnName("TITLE")
                .HasColumnType(DatabaseTypeConstant.Varchar)
                .HasMaxLength(100)
                .IsRequired();
            builder.Property(x => x.Message)
                .HasColumnName("MESSAGE")
                .HasColumnType(DatabaseTypeConstant.Varchar)
                .HasMaxLength(1024)
                .IsRequired();
            #endregion

            #region Indexes
            builder.HasIndex(x => x.AuthorId).HasDatabaseName("IDX_TB_POST_01");
            builder.HasIndex(x => x.Date).HasDatabaseName("IDX_TB_POST_02");
            #endregion

            #region Relationships
            builder.HasMany(x => x.Comments)
                .WithOne(x => x.Post)
                .HasForeignKey(x => x.PostId)
                .HasConstraintName("FK_TB_POST_001")
            .OnDelete(DeleteBehavior.NoAction);

            builder.HasOne(x => x.Author)
                .WithMany(x => x.Posts)
                .HasForeignKey(x => x.AuthorId)
                .OnDelete(DeleteBehavior.NoAction);
            #endregion
        }
    }
}
