using Blog.Bases.Services;
using Blog.Entities.Comments;
using Blog.Services.Entities;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.VisualStudio.TestPlatform.TestHost;
using System.Net.Http.Json;

namespace Blog.Tests.CommentApi
{
    public class CommentIntegrationTests : IClassFixture<WebApplicationFactory<Program>>
    {
        private readonly HttpClient _client;
        private readonly Guid _postId;
        private readonly Guid _authorId;

        public CommentIntegrationTests(WebApplicationFactory<Program> factory)
        {
            _client = factory.CreateClient();

            _postId = Guid.Parse("e504112c-ba95-4e58-a60b-2e63c630ab38");
            _authorId = Guid.Parse("4e7a3c4b-16ce-485f-bc30-0da09a6dfb3f");
        }

        [Fact]
        public async Task CreateCommentAsync_ReturnsCreatedComment()
        {
            // Arrange
            var newComment = new CommentInput
            {
                Id = Guid.NewGuid(),
                PostId = _postId,
                CommentAuthorId = _authorId,
                Message = "This is a test comment."
            };

            var serviceInput = new ServiceInput<CommentInput>
            {
                Input = newComment
            };

            // Act
            var response = await _client.PostAsJsonAsync("/api/v1/comment", serviceInput);

            // Assert
            response.EnsureSuccessStatusCode(); // Verifica se o status é 2xx
            var createdComment = await response.Content.ReadFromJsonAsync<ServiceOutput<CommentOutput>>();
            Assert.NotNull(createdComment);
            Assert.Equal(newComment.Message, createdComment.Output.Message);
        }

        [Fact]
        public async Task UpdateCommentAsync_ReturnsUpdatedComment()
        {
            // Arrange
            var updateComment = new CommentInput
            {
                Id = Guid.NewGuid(),
                PostId = _postId,
                CommentAuthorId = _authorId,
                Message = "Updated comment message."
            };

            var serviceInput = new ServiceInput<CommentInput>
            {
                Input = updateComment
            };

            // Primeiro criamos o comentário para depois atualizar
            await _client.PostAsJsonAsync("/api/v1/comment", new ServiceInput<CommentInput>
            {
                Input = new CommentInput
                {
                    Id = updateComment.Id,
                    PostId = updateComment.PostId,
                    CommentAuthorId = updateComment.CommentAuthorId,
                    Message = "Initial comment message."
                }
            });

            // Act - realiza o update
            var response = await _client.PutAsJsonAsync("/api/v1/comment", serviceInput);

            // Assert
            response.EnsureSuccessStatusCode();
            var updatedComment = await response.Content.ReadFromJsonAsync<ServiceOutput<CommentOutput>>();
            Assert.NotNull(updatedComment);
            Assert.Equal("Updated comment message.", updatedComment.Output.Message);
        }

        [Fact]
        public async Task RemoveCommentAsync_DeletesComment_ReturnsTrue()
        {
            // Arrange
            var commentId = Guid.NewGuid();

            // Create comment to be deleted
            await _client.PostAsJsonAsync("/api/v1/comment", new ServiceInput<CommentInput>
            {
                Input = new CommentInput
                {
                    Id = commentId,
                    PostId = _postId,
                    CommentAuthorId = _authorId,
                    Message = "Comment to be deleted"
                }
            });

            // Act - realiza o delete
            var response = await _client.DeleteAsync($"/api/v1/comment?id={commentId}");

            // Assert
            response.EnsureSuccessStatusCode();
            var deleteResult = await response.Content.ReadFromJsonAsync<ServiceOutput<bool>>();
            Assert.True(deleteResult.Output);
        }

    }
}
