using FinalProjectMVC.ViewModels.Account;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Domain.Helpers.Enums;
using Domain.Identity;
using Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages;

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



        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ForgotPassword(string email)
        {
            if (string.IsNullOrEmpty(email))
            {
                ModelState.AddModelError(string.Empty, "Email is required.");
                return View();
            }

            var user = await _userManager.FindByEmailAsync(email);

            if (user == null || !(await _userManager.IsEmailConfirmedAsync(user)))
            {
                ModelState.AddModelError(string.Empty, "Invalid email or email not confirmed.");
                return View();
            }

            var token = await _userManager.GeneratePasswordResetTokenAsync(user);
            var callbackUrl = Url.Action("ResetPassword", "Account", new { email, token }, Request.Scheme);

            string subject = "Reset Password";
            string html;

            using (StreamReader reader = new("wwwroot/templates/resetpassword.html"))
            {
                html = reader.ReadToEnd();
            }

            html = html.Replace("{{reset-link}}", callbackUrl);

            _emailService.Send(user.Email, subject, html);

            TempData["SuccessMessage"] = "Password reset link has been sent to your email.";
            return RedirectToAction("Login");
        }

        [HttpGet]
        public IActionResult ResetPassword(string email, string token)
        {
            if (string.IsNullOrEmpty(email) || string.IsNullOrEmpty(token))
            {
                return RedirectToAction("Error", "Home");
            }

            var model = new ResetPasswordVM { Email = email, Token = token };
            return View(model);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ResetPassword(ResetPasswordVM model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            if (string.IsNullOrEmpty(model.Username) || string.IsNullOrEmpty(model.Password) || string.IsNullOrEmpty(model.ConfirmPassword))
            {
                TempData["ErrorMessage"] = "All fields are required.";
                return View(model);
            }

            if (model.Password != model.ConfirmPassword)
            {
                TempData["ErrorMessage"] = "New Password and Confirm Password do not match.";
                return View(model);
            }

            if (model.Password.Length < 8 ||
                !model.Password.Any(char.IsUpper) ||
                !model.Password.Any(char.IsDigit) ||
                !model.Password.Any(c => "!@#$%^&*(),.?\":{}|<>".Contains(c)))
            {
                TempData["ErrorMessage"] = "Password must be at least 8 characters long, contain an uppercase letter, a number, and a special character.";
                return View(model);
            }

           
            var user = await _userManager.FindByNameAsync(model.Username);

            if (user == null)
            {
                TempData["ErrorMessage"] = "Invalid request. Username not found.";
                return View(model);
            }

            
            var token = await _userManager.GeneratePasswordResetTokenAsync(user);
            var result = await _userManager.ResetPasswordAsync(user, token, model.Password);

            if (result.Succeeded)
            {
                TempData["SuccessMessage"] = "Your password has been reset successfully.";
                return RedirectToAction("Login");
            }

            foreach (var error in result.Errors)
            {
                TempData["ErrorMessage"] = error.Description;
            }

            return View(model);
        }







    }
}
