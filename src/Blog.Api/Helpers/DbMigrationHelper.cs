using Blog.Repositories.Contexts;
using Microsoft.EntityFrameworkCore;

namespace Blog.Api.Helpers
{
    public static class DbMigrationHelper
    {
        public static async Task SeedDataAsync(WebApplication serviceScope)
        {
            var services = serviceScope.Services.CreateScope().ServiceProvider;
            await SeedDataAsync(services);
        }

        public static async Task SeedDataAsync(IServiceProvider serviceProvider)
        {
            using var scope = serviceProvider.GetRequiredService<IServiceScopeFactory>().CreateScope();
            var env = scope.ServiceProvider.GetRequiredService<IWebHostEnvironment>();

            var context = scope.ServiceProvider.GetRequiredService<BlogDbContext>();

            if (env.IsDevelopment())
            {
                await context.Database.MigrateAsync();
                await SeedDatabaseAsync(context);
            }
        }

        private static async Task SeedDatabaseAsync(BlogDbContext context)
        {
            if (!context.Authors.Any())
            {
                Guid authorId1 = Guid.NewGuid();
                await context.Authors.AddAsync(new Entities.Authors.Author()
                {
                    Id = authorId1,
                    Name = "Cath Oliveira"
                });

                Guid authorId2 = Guid.NewGuid();
                Guid postId1 = Guid.NewGuid();
                Guid postId2 = Guid.NewGuid();
                await context.Authors.AddAsync(new Entities.Authors.Author()
                {
                    Id = authorId2,
                    Name = "Jairo Azevedo", 
                    Posts = new List<Entities.Posts.Post>()
                    {
                        new Entities.Posts.Post() 
                        {
                            Id = postId1,
                            AuthorId = authorId2,
                            Date = DateTime.Now,
                            Title = "Primeiro post", 
                            Message = "Este é um exemplo de postagem - Postagem 001", 
                            Comments = new List<Entities.Comments.Comment>()
                            {
                                new Entities.Comments.Comment()
                                {
                                    Id = Guid.NewGuid(),
                                    PostId = postId1,
                                    Date = DateTime.Now,
                                    CommentAuthorId = authorId1,
                                    Message = "Primeiro comentário na minha postagem 001"
                                },
                                new Entities.Comments.Comment()
                                {
                                    Id = Guid.NewGuid(),
                                    PostId = postId1,
                                    Date = DateTime.Now,
                                    CommentAuthorId = authorId1,
                                    Message = "Segundo comentário na minha postagem 001"
                                }
                            }
                        },
                        new Entities.Posts.Post()
                        {
                            Id = postId2,
                            AuthorId = authorId2,
                            Date = DateTime.Now,
                            Title = "Segundo post",
                            Message = "Este é um exemplo de postagem - Postagem 002",
                            Comments = new List<Entities.Comments.Comment>()
                            {
                                new Entities.Comments.Comment()
                                {
                                    Id = Guid.NewGuid(),
                                    PostId = postId1,
                                    Date = DateTime.Now,
                                    CommentAuthorId = authorId1,
                                    Message = "Primeiro comentário na minha postagem 002"
                                },
                                new Entities.Comments.Comment()
                                {
                                    Id = Guid.NewGuid(),
                                    PostId = postId1,
                                    Date = DateTime.Now,
                                    CommentAuthorId = authorId1,
                                    Message = "Segundo comentário na minha postagem 002"
                                },
                                new Entities.Comments.Comment()
                                {
                                    Id = Guid.NewGuid(),
                                    PostId = postId1,
                                    Date = DateTime.Now,
                                    CommentAuthorId = authorId1,
                                    Message = "Terceiro comentário na minha postagem 002"
                                }
                            }
                        }
                    }
                });

                await context.SaveChangesAsync();
            }
        }
    }
}
