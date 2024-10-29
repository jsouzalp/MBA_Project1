using Blog.Bases.Services;
using Blog.Entities.Authentication;
using Blog.Entities.Authors;
using Blog.Mvc.Models;
using Blog.Services.Implementations;
using Microsoft.AspNetCore.Mvc;

public class AccountController : Controller
{
    private readonly AuthenticationService _authenticationService;
    public AccountController(AuthenticationService authenticationService)
    {
        _authenticationService = authenticationService;
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
            var result = await _authenticationService.RegisterUserAsync(new ServiceInput<AuthenticationInput>()
            {
                Input = new AuthenticationInput()
                {
                    Email = model.Email,
                    Password = model.Password,
                    FullName = model.FullName
                }
            });

            if (result.Success)
            {
                return RedirectToAction("Index", "Post");
            }

            foreach (var error in result.Errors)
            {
                ModelState.AddModelError(error.Code, error.Message);
            }
        }

        return View(model);
    }
}
