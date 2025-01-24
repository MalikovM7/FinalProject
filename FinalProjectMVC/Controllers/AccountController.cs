using FinalProjectMVC.ViewModels.Account;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Domain.Helpers.Enums;
using Domain.Identity;
using Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

namespace FinalProjectMVC.Controllers
{
    public class AccountController : Controller
    {

        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IEmailService _emailService;
        public AccountController(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager, RoleManager<IdentityRole> roleManager, IEmailService emailService)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
            _emailService = emailService;
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }



        [HttpGet]
        public async Task<IActionResult> Register()
        {
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterVM request)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            if (request.Password != request.ConfirmPassword)
            {
                ModelState.AddModelError("ConfirmPassword", "Passwords do not match. Please try again.");
                return View();
            }

            AppUser user = new()
            {
                FullName = request.FullName,
                Email = request.Email,
                UserName = request.Username,
            };

            var result = await _userManager.CreateAsync(user, request.Password);

            if (!result.Succeeded)
            {
                foreach (var item in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, item.Description);
                }

                return View();
            }

            
            if (!await _roleManager.RoleExistsAsync(Roles.Member.ToString()))
            {
                await _roleManager.CreateAsync(new IdentityRole(Roles.Member.ToString()));
            }

            await _userManager.AddToRoleAsync(user, Roles.Member.ToString());

            string token = await _userManager.GenerateEmailConfirmationTokenAsync(user);
            string url = Url.Action("ConfirmEmail", "Account", new { userId = user.Id, token }, Request.Scheme, Request.Host.ToString());

            string subject = "Register confirmation email";

            string html = string.Empty;

            using (StreamReader reader = new("wwwroot/templates/verification.html"))
            {
                html = reader.ReadToEnd();
            }

            html = html.Replace("{{confirm-link}}", url);

            _emailService.Send(user.Email, subject, html);

            return RedirectToAction(nameof(VerifyEmail));
        }

        [HttpGet]
        [HttpGet]
        public async Task<IActionResult> ConfirmEmail(string userId, string token)
        {
            if (string.IsNullOrEmpty(userId) || string.IsNullOrEmpty(token))
            {
                
                return RedirectToAction("Error", "Home"); 
            }

            var user = await _userManager.FindByIdAsync(userId);

            if (user == null)
            {
               
                return RedirectToAction("Error", "Home"); 
            }

            var result = await _userManager.ConfirmEmailAsync(user, token);

            if (result.Succeeded)
            {
                
                await _signInManager.SignInAsync(user, false);

                return RedirectToAction("Index", "Home");
            }

            
            return RedirectToAction("Error", "Home"); 
        }


        [HttpGet]
        public IActionResult VerifyEmail()
        {
            return View();
        }




        [HttpGet]
        public async Task<IActionResult> Login()
        {
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginVM request)
        {
            if (!ModelState.IsValid) return View();

            var existUser = await _userManager.FindByEmailAsync(request.UsernameOrEmail);

            if (existUser is null)
            {
                existUser = await _userManager.FindByNameAsync(request.UsernameOrEmail);
            }

            if (existUser is null)
            {
                ModelState.AddModelError(string.Empty, "Login Failed");
                return View();
            }


            var result = await _signInManager.PasswordSignInAsync(existUser, request.Password, false, false);

            if (!result.Succeeded)
            {
                ModelState.AddModelError(string.Empty, "Login Failed");
                return View();
            }


            return RedirectToAction("Index", "Home");

        }



        [HttpGet]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateRoles()
        {
            foreach (Roles role in Enum.GetValues(typeof(Roles)))
            {
                if (!await _roleManager.RoleExistsAsync(role.ToString()))
                {
                    await _roleManager.CreateAsync(new IdentityRole { Name = role.ToString() });
                }
            }
            return Ok();
        }


        [HttpGet]
        [Authorize]
        public ActionResult ChangePassword()
        {
            return View();
        }

        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ChangePassword(ChangePasswordViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var result = await _userManager.ChangePasswordAsync(await _userManager.FindByIdAsync(userId), model.CurrentPassword, model.NewPassword);

            if (result.Succeeded)
            {
                TempData["SuccessMessage"] = "Your password has been changed successfully.";
                return RedirectToAction("Index", "Home");
            }

            foreach (var error in result.Errors)
            {
                ModelState.AddModelError("", error.Description);
            }

            return View(model);
        }






    }
}
