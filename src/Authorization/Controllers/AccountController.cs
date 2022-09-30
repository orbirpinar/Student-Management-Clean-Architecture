using System.Security.Claims;
using Authorization.Entities;
using Authorization.ViewModels;
using MassTransit;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Shared.Events;

namespace Authorization.Controllers
{
    public class AccountController : Controller
    {
        private readonly SignInManager<User> _signInManager;
        private readonly UserManager<User> _userManager;
        private readonly IBus _bus;


        public AccountController(SignInManager<User> signInManager, UserManager<User> userManager, IBus bus)
        {
            _signInManager = signInManager;
            _userManager = userManager;
            _bus = bus;
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult Login(string returnUrl = null)
        {
            ViewData["ReturnUrl"] = returnUrl;
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            ViewData["ReturnUrl"] = model.ReturnUrl;

            if (!ModelState.IsValid)
            {
                return View();
            }

            var result = await _signInManager.PasswordSignInAsync(model.Username, model.Password, model.RememberMe, false);
            if (result.Succeeded)
            {
                // TODO RAISE LOGIN EVENT
                var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                await _bus.Publish <UserLoggedIn>(new {Id = userId, UserName = model.Username, Email = model.Username});
                if (!string.IsNullOrEmpty(model.ReturnUrl))
                {
                    return Redirect(model.ReturnUrl);
                }

                return RedirectToAction("Index", "Home");
            }

            ModelState.AddModelError("", "Username or password incorrect");

            return View();
        }

        [AllowAnonymous]
        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterViewModel register)
        {
            if (!ModelState.IsValid) return View();
            var user = new User
            {
                UserName = register.Username, 
                Email = register.Email,
                Firstname = register.Firstname,
                Lastname = register.Lastname
            };
            var result = await _userManager.CreateAsync(user, register.Password);
            if (result.Succeeded)
            {
                await _bus.Publish<UserRegistered>(new
                {
                    Id = new Guid(user.Id),
                    Username = user.UserName,
                    user.Email,
                    user.Firstname,
                    user.Lastname
                });
                await _signInManager.SignInAsync(user, false);
                return RedirectToAction("Index", "Home");
            }

            foreach (var error in result.Errors)
            {
                ModelState.AddModelError("", error.Description);
            }
            
            return View();
        }


        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            
            return RedirectToAction(nameof(HomeController.Index), "Home");
        }
    }
}