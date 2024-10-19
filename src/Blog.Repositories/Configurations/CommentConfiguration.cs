using Blog.Entities.Comments;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Blog.Repositories.Configurations
{
    public class CommentConfiguration : IEntityTypeConfiguration<Comment>
    {
        public void Configure(EntityTypeBuilder<Comment> builder)
        {
            #region Mapping columns
            builder.ToTable("TB_COMMENT");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id)
                .HasColumnName("COMMENT_ID")
                .HasColumnType(DatabaseTypeConstant.UniqueIdentifier)
                .IsRequired();
            builder.Property(x=> x.PostId)
                .HasColumnName("POST_ID")
                .HasColumnType(DatabaseTypeConstant.UniqueIdentifier)
                .IsRequired();
            builder.Property(x=> x.CommentAuthorId)
                .HasColumnName("COMENT_AUTHOR_ID")
                .HasColumnType(DatabaseTypeConstant.UniqueIdentifier)
                .IsRequired();
            builder.Property(x => x.Date)
                .HasColumnName("DATE")
                .HasColumnType(DatabaseTypeConstant.DateTime)
                .IsRequired();
            builder.Property(x=> x.Message)
                .HasColumnName("MESSAGE")
                .HasColumnType(DatabaseTypeConstant.Varchar)
                .HasMaxLength(1024)
                .IsRequired();
            #endregion

            #region Indexes
            builder.HasIndex(x => x.PostId).HasDatabaseName("IDX_TB_COMMENT_01");
            builder.HasIndex(x => x.CommentAuthorId).HasDatabaseName("IDX_TB_COMMENT_02");
            builder.HasIndex(x => x.Date).HasDatabaseName("IDX_TB_COMMENT_03");
            #endregion

            #region Relationships
            builder.HasOne(c => c.Post)
               .WithMany(p => p.Comments)
               .HasForeignKey(c => c.PostId)
               .HasConstraintName("FK_TB_COMMENT_001")
               .OnDelete(DeleteBehavior.NoAction);

            builder.HasOne(x => x.CommentAuthor)
                .WithMany(x => x.Comments)
                .HasForeignKey(x => x.CommentAuthorId)
                .HasConstraintName("FK_TB_COMMENT_002")
                .OnDelete(DeleteBehavior.NoAction);
            #endregion

            #region Ignores
            builder.Ignore(x => x.CommentAuthorName);
            #endregion
        }
    }
}
