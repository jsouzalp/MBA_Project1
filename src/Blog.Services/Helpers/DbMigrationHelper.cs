using Blog.Entities.Authors;
using Blog.Entities.Comments;
using Blog.Entities.Posts;
using Blog.Repositories.Contexts;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Blog.Services.Helpers
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
            var contextIdentity = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
            var userManager = scope.ServiceProvider.GetRequiredService<UserManager<IdentityUser>>();

            if (env.IsDevelopment())
            {
                await context.Database.MigrateAsync();
                await contextIdentity.Database.MigrateAsync();
                await SeedDatabaseAsync(context, userManager);
            }
        }

        private static async Task SeedDatabaseAsync(BlogDbContext context, UserManager<IdentityUser> userManager)
        {
            if (!context.Authors.Any())
            {
                #region Criação de usuários no Identity
                Guid authorId1 = Guid.NewGuid();
                Guid authorId2 = Guid.NewGuid();

                //var user1 = new IdentityUser { Id = Guid.NewGuid().ToString(), UserName = "cath.lp@gmail.com", Email = "cath.lp@gmail.com", EmailConfirmed = true };
                var user1 = new IdentityUser { UserName = "cath.lp@gmail.com", Email = "cath.lp@gmail.com", EmailConfirmed = true };
                var result1 = await userManager.CreateAsync(user1, "123");

                if (result1.Succeeded)
                {
                    authorId1 = Guid.Parse(user1.Id);
                }

                //var user2 = new IdentityUser { Id = Guid.NewGuid().ToString(), UserName = "jsouza.lp@gmail.com", Email = "jsouza.lp@gmail.com", EmailConfirmed = true };
                var user2 = new IdentityUser { UserName = "jsouza.lp@gmail.com", Email = "jsouza.lp@gmail.com", EmailConfirmed = true };
                var result2 = await userManager.CreateAsync(user2, "123");

                if (result2.Succeeded)
                {
                    authorId2 = Guid.Parse(user2.Id);
                }
                #endregion

                #region Criação de massa de dados
                await context.Authors.AddAsync(new Author()
                {
                    Id = authorId1,
                    IdentityUser = authorId1,
                    Name = "Cath Oliveira"
                });

                Guid postId1 = Guid.NewGuid();
                Guid postId2 = Guid.NewGuid();
                await context.Authors.AddAsync(new Author()
                {
                    Id = authorId2,
                    IdentityUser = authorId2,
                    Name = "Jairo Azevedo",
                    Posts = new List<Post>()
                    {
                        new Post()
                        {
                            Id = postId1,
                            AuthorId = authorId2,
                            Date = DateTime.Now,
                            Title = "Primeiro post",
                            Message = "Este é um exemplo de postagem - Postagem 001",
                            Comments = new List<Comment>()
                            {
                                new Comment()
                                {
                                    Id = Guid.NewGuid(),
                                    PostId = postId1,
                                    Date = DateTime.Now,
                                    CommentAuthorId = authorId1,
                                    Message = "Primeiro comentário na minha postagem 001"
                                },
                                new Comment()
                                {
                                    Id = Guid.NewGuid(),
                                    PostId = postId1,
                                    Date = DateTime.Now,
                                    CommentAuthorId = authorId1,
                                    Message = "Segundo comentário na minha postagem 001"
                                }
                            }
                        },
                        new Post()
                        {
                            Id = postId2,
                            AuthorId = authorId2,
                            Date = DateTime.Now,
                            Title = "Segundo post",
                            Message = "Este é um exemplo de postagem - Postagem 002",
                            Comments = new List<Comment>()
                            {
                                new Comment()
                                {
                                    Id = Guid.NewGuid(),
                                    PostId = postId1,
                                    Date = DateTime.Now,
                                    CommentAuthorId = authorId1,
                                    Message = "Primeiro comentário na minha postagem 002"
                                },
                                new Comment()
                                {
                                    Id = Guid.NewGuid(),
                                    PostId = postId1,
                                    Date = DateTime.Now,
                                    CommentAuthorId = authorId1,
                                    Message = "Segundo comentário na minha postagem 002"
                                },
                                new Comment()
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
                #endregion
            }
        }
    }
}
