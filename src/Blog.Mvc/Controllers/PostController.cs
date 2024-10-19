using Blog.Bases.Services;
using Blog.Entities.Posts;
using Blog.Mvc.Models;
using Blog.Services.Abstractions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Blog.Mvc.Controllers
{
    [Authorize]
    public class PostController : Controller
    {
        private readonly IPostService _postService;

        public PostController(IPostService postService)
        {
            _postService = postService;
        }

        public IActionResult Index()
        {
            var posts = _postService.FilterPostsAsync(new Entities.Posts.FilterPostInput()
            {
                Page = 1,
                RecordsPerPage = 1000,
            }).Result;

            IEnumerable<PostViewModel> result = new List<PostViewModel>();

            if (posts != null)
            {
                result = (from x in posts.Output
                          select new PostViewModel()
                          {
                              Id = x.Id,
                              AuthorId = x.AuthorId,
                              AuthorName = x.AuthorName,
                              Title = x.Title,
                              Message = x.Message
                          }).ToList();
            }
            return View(result);
        }

        // GET: Post/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Post/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(PostViewModel postViewModel)
        {
            if (ModelState.IsValid)
            {
                var userId = User.FindFirstValue(ClaimTypes.NameIdentifier); 
                postViewModel.AuthorId = Guid.Parse(userId); 

                var result = _postService.CreatePostAsync(new ServiceInput<PostInput>()
                {
                    Input = new PostInput()
                    {
                        AuthorId = postViewModel.AuthorId,
                        Title = postViewModel.Title,
                        Message = postViewModel.Message
                    }
                }).Result;

                if (!result.Success && result.Errors.Any())
                {
                    // Armazena os erros na ViewBag
                    ViewBag.ErrorMessages = result.Errors.Select(x => x.Message).ToList();
                    return View(postViewModel);
                }

                return RedirectToAction(nameof(Index));
            }

            return View(postViewModel);
        }

        public IActionResult Details(Guid id)
        {
            var post = _postService.GetPostAsync(id).Result;

            if (post == null || !post.Success)
            {
                return NotFound();
            }

            var postDetailsViewModel = new PostViewModel
            {
                Id = post.Output.Id,
                AuthorId = post.Output.AuthorId,
                AuthorName = post.Output.AuthorName,
                Title = post.Output.Title,  
                Message = post.Output.Message,
                Comments = (from x in post.Output.Comments
                            select new CommentViewModel()
                            {
                                Id = x.Id, 
                                PostId = x.PostId,
                                CommentAuthorId = x.CommentAuthorId,
                                CommentAuthorName = x.CommentAuthorName,
                                Message = x.Message
                            }).ToList()
            };

            return View(postDetailsViewModel);
        }

        public IActionResult Edit(Guid id)
        {
            var post = _postService.GetPostAsync(id).Result;
            if (post == null)
            {
                return NotFound();
            }

            PostViewModel result = new PostViewModel();

            if (post != null && post.Success)
            {
                result = new PostViewModel()
                {
                    Id = post.Output.Id,
                    AuthorId = post.Output.AuthorId,
                    AuthorName = post.Output.AuthorName,
                    Title = post.Output.Title,
                    Message = post.Output.Message
                };
            }

            return View(result);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(PostViewModel postViewModel)
        {
            if (ModelState.IsValid)
            {
                var result = _postService.UpdatePostAsync(new ServiceInput<PostInput>()
                {                    
                    Input = new PostInput()
                    {
                        Id = postViewModel.Id,
                        AuthorId = postViewModel.AuthorId,
                        Title = postViewModel.Title,
                        Message = postViewModel.Message
                    }
                }).Result;

                if (!result.Success && result.Errors.Any())
                {
                    ViewBag.ErrorMessages = result.Errors.Select(x => x.Message).ToList();
                    return View(postViewModel);
                }

                return RedirectToAction(nameof(Index));
            }

            return View(postViewModel);
        }

        public IActionResult Delete(Guid id)
        {
            var post = _postService.GetPostAsync(id).Result;
            if (post == null)
            {
                return NotFound();
            }

            var result = _postService.RemovePostAsync(id).Result;

            return RedirectToAction(nameof(Index));
        }
    }
}
