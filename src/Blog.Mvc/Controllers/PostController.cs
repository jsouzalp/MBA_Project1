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

        [AllowAnonymous]
        public IActionResult Index()
        {
            var posts = _postService.FilterPostsAsync(new Entities.Posts.FilterPostInput()
            {
                Page = 1,
                RecordsPerPage = 1000,
            }).Result;

            IEnumerable<PostViewModel> result = new List<PostViewModel>();

            if (!posts.Success)
            {
                ViewData["ErrorMessages"] = posts.Errors;
            }
            else
            {
                result = (from x in posts.Output
                          select new PostViewModel()
                          {
                              Id = x.Id,
                              AuthorId = x.AuthorId,
                              AuthorName = x.AuthorName,
                              Date = x.Date,
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

                if (!result.Success)
                {
                    ViewData["ErrorMessages"] = result.Errors;
                }
                else
                {
                    return RedirectToAction(nameof(Index));
                }
            }

            return View(postViewModel);
        }

        [AllowAnonymous]
        public IActionResult Details(Guid id)
        {
            var postDetailsViewModel = new PostViewModel();
            var post = _postService.GetPostAsync(id).Result;

            if (!post.Success)
            {
                ViewData["ErrorMessages"] = post.Errors;
            }
            else
            {
                postDetailsViewModel.Id = post.Output.Id;
                postDetailsViewModel.AuthorId = post.Output.AuthorId;
                postDetailsViewModel.AuthorName = post.Output.AuthorName;
                postDetailsViewModel.Date = post.Output.Date;
                postDetailsViewModel.Title = post.Output.Title;
                postDetailsViewModel.Message = post.Output.Message;
                postDetailsViewModel.Comments = (from x in post.Output.Comments
                                                 select new CommentViewModel()
                                                 {
                                                     Id = x.Id,
                                                     PostId = x.PostId,
                                                     CommentAuthorId = x.CommentAuthorId,
                                                     CommentAuthorName = x.CommentAuthorName,
                                                     Date = x.Date,
                                                     Message = x.Message
                                                 }).ToList();
            }

            return View(postDetailsViewModel);
        }

        public IActionResult Edit(Guid id)
        {
            PostViewModel result = new PostViewModel();
            var post = _postService.GetPostAsync(id).Result;

            if (post == null)
            {
                return NotFound();
            }

            if (!post.Success)
            {
                ViewData["ErrorMessages"] = post.Errors;
            }
            else
            {
                result.Id = post.Output.Id;
                result.AuthorId = post.Output.AuthorId;
                result.AuthorName = post.Output.AuthorName;
                result.Date = post.Output.Date;
                result.Title = post.Output.Title;
                result.Message = post.Output.Message;                
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

                if (!result.Success)
                {
                    ViewData["ErrorMessages"] = result.Errors;
                }
                else
                {
                    return RedirectToAction(nameof(Index));
                }
            }

            return View(postViewModel);
        }

        public IActionResult Delete(Guid id)
        {
            var result = _postService.RemovePostAsync(id).Result;
            return RedirectToAction(nameof(Index));
        }
    }
}
