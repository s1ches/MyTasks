using IdentityServer4.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Notes.Identity.Models;

namespace Notes.Identity.Controllers;

[Route("[controller]/[action]")]
public class AuthController : Controller
{
    private SignInManager<AppUser> _signInManager;
    
    private readonly UserManager<AppUser> _userManager;
    
    private readonly IIdentityServerInteractionService _interactionService;

    public AuthController(SignInManager<AppUser> signInManager,
        UserManager<AppUser> userManager,
        IIdentityServerInteractionService interactionService)
    {
        _signInManager = signInManager;
        _userManager = userManager;
        _interactionService = interactionService;
    }


    [HttpGet]
    public IActionResult Login(string returnUrl)
    {
        var viewModel = new LoginViewModel { ReturnUrl = returnUrl };
        
        return View(viewModel);
    }

    [HttpPost]
    public async Task<IActionResult> Login(LoginViewModel model)
    {
        if (!ModelState.IsValid)
            return View(model);

        var user = await _userManager.FindByNameAsync(model.UserName);

        if (user is null)
        {
            ModelState.AddModelError(string.Empty, "User not found");
            return View(model);
        }

        var result = await _signInManager.PasswordSignInAsync(model.UserName, model.Password, 
            isPersistent: false, lockoutOnFailure: false);

        if (result.Succeeded)
            return Redirect(model.ReturnUrl);
        
        ModelState.AddModelError(string.Empty, "Login error");
        return View(model);
    }

    [HttpGet]
    public IActionResult Register(string returnUrl)
    {
        var model = new RegisterViewModel { ReturnUrl = returnUrl };

        return View(model);
    }

    [HttpPost]
    public async Task<IActionResult> Register(RegisterViewModel model)
    {
        if (!ModelState.IsValid)
            return View(model);

        var user = new AppUser { UserName = model.UserName };

        var result = await _userManager.CreateAsync(user, model.Password);

        if (result.Succeeded)
        {
            await _signInManager.SignInAsync(user, isPersistent: false);
            return Redirect(model.ReturnUrl);
        }
        
        ModelState.AddModelError(string.Empty, "Error occured");
        return View(model);
    }

    [HttpGet]
    public async Task<IActionResult> Logout(string logoutId)
    {
        await _signInManager.SignOutAsync();
        var logoutRequest = await _interactionService.GetLogoutContextAsync(logoutId);
        return Redirect(logoutRequest.PostLogoutRedirectUri);
    }
}