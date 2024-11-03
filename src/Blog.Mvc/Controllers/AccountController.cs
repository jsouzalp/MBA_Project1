using Blog.Bases.Services;
using Blog.Entities.Authentication;
using Blog.Entities.Authors;
using Blog.Mvc.Models;
using Blog.Services.Abstractions;
using Blog.Services.Implementations;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

public class AccountController : Controller
{
    private readonly IAuthenticationService _authenticationService;
    private readonly SignInManager<IdentityUser> _signInManager;
    private readonly UserManager<IdentityUser> _userManager;

    public AccountController(IAuthenticationService authenticationService,
        SignInManager<IdentityUser> signInManager,
        UserManager<IdentityUser> userManager)
    {
        _authenticationService = authenticationService;
        _signInManager = signInManager;
        _userManager = userManager;
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
            var result = await _authenticationService.RegisterUserAsync(false, new ServiceInput<AuthenticationInput>()
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

    [HttpGet]
    public IActionResult Login()
    {
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Login(LoginViewModel model)
    {
        if (ModelState.IsValid)
        {
            var result = await _authenticationService.LoginUserAsync(false, new ServiceInput<AuthenticationInput>()
            {
                Input = new AuthenticationInput()
                {
                    Email = model.Email, 
                    Password = model.Password
                }
            });

            if (result.Success)
            {
                return RedirectToAction("Index", "Post");
            }
            else
            {
                ModelState.AddModelError(string.Empty, "Falha ao fazer login.");
            }
        }

        return View(model);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Logout()
    {
        await _signInManager.SignOutAsync();
        return RedirectToAction("Index", "Post");
    }
}
