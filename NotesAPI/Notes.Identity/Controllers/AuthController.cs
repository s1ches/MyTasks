using Microsoft.AspNetCore.Mvc;
using Notes.Identity.Models;

namespace Notes.Identity.Controllers;

[Route("[controller]/[action]")]
public class AuthController : Controller
{
    [HttpGet]
    public IActionResult Login(string returnUrl)
    {
        var viewModel = new LoginViewModel { ReturnUrl = returnUrl };
        
        return View();
    }

    [HttpPost]
    public IActionResult Login(LoginViewModel model)
    {
        return View(model);
    }
}