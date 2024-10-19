using Blog.Bases.Services;
using Blog.Entities.Authors;
using Blog.Mvc.Models;
using Blog.Services.Abstractions;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

public class AccountController : Controller
{
    private readonly UserManager<IdentityUser> _userManager;
    private readonly IAuthorService _authorService;

    public AccountController(UserManager<IdentityUser> userManager,
        IAuthorService authorService)
    {
        _userManager = userManager;
        _authorService = authorService;
    }

    [HttpGet]
    public IActionResult Register()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Register(RegisterViewModel model)
    {
        if (ModelState.IsValid)
        {
            var user = new IdentityUser { UserName = model.Email, Email = model.Email, NormalizedUserName = model.FullName, EmailConfirmed = true };
            var result = await _userManager.CreateAsync(user, model.Password);

            if (result.Succeeded)
            {
                _ = _authorService.CreateAuthorAsync(new ServiceInput<AuthorInput>()
                {
                    Input = new AuthorInput()
                    {
                        Id = Guid.Parse(user.Id), 
                        IdentityUser = Guid.Parse(user.Id),
                        Name = model.FullName
                    }
                }).Result;

                return RedirectToAction("Index", "Post");
            }

            foreach (var error in result.Errors)
            {
                ModelState.AddModelError(string.Empty, error.Description);
            }
        }

        // Se algo deu errado, retorne a view de registro com erros
        return View(model);
    }

    private async Task ExecuteSpecificAction(string userId)
    {
        // Implementação da ação específica que usa o userId
        // Por exemplo, criar uma entrada em outra tabela do banco de dados
    }
}
