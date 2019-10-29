using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using BES.Data;
using BES.Services;
using BES.Data.DataAnnotations;
using BES.Services.Mail;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Authorization;

namespace BES.Pages.Account
{
    [Authorize(Roles = "Administrator")]
    public class RegisterModel : PageModel
    {
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ILogger<LoginModel> _logger;
        private readonly IMailManager _emailSender;
        private RoleManager<IdentityRole> RoleManager;

        public RegisterModel(
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            ILogger<LoginModel> logger,
            IMailManager emailSender,
            RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _logger = logger;
            _emailSender = emailSender;
            RoleManager = roleManager;
        }

        [BindProperty]
        public InputModel Input { get; set; }

        public string ReturnUrl { get; set; }

        public class InputModel
        {
            [Required]
            [Display(Name = "Full Name")]
            public string FullName { get; set; }

            [Required]
            [EmailAddress]
            [Display(Name = "Email")]
            public string Email { get; set; }

            [Required]
            [StringLength(100, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 6)]
            [RegularExpression(@"^((?=.*[a-z])(?=.*[A-Z])(?=.*\d).+$)")]
            [DataType(DataType.Password)]
            [Display(Name = "Password")]
            public string Password { get; set; }

            [DataType(DataType.Password)]
            [Display(Name = "Confirm password")]
            [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
            public string ConfirmPassword { get; set; }

            [Display(Name = "Agreement")]
            [IsTrueRequired(ErrorMessage = "You must agree the terms.")]
            public bool IsAgree { get; set; }

            public string SectionRole { get; set; }
           
        }

        public void OnGet(string returnUrl = null)
        {
            ReturnUrl = returnUrl;
        }

        private readonly ApplicationDbContext _context;
        public async Task<IActionResult> OnPostAsync(string returnUrl = null)
        {
            ReturnUrl = returnUrl;
            if (ModelState.IsValid)
            {
                var user = new ApplicationUser { UserName = Input.FullName, Email = Input.Email, PhoneNumber = Input.Password, Role = Input.SectionRole};
                var result = await _userManager.CreateAsync(user, Input.Password);
                if (result.Succeeded)
                {
                    if (!await RoleManager.RoleExistsAsync(user.Role))
                    {
                        var Role = new IdentityRole(user.Role);
                        var res = await RoleManager.CreateAsync(Role);
                        if (res.Succeeded)
                        {
                            await _userManager.AddToRoleAsync(user, user.Role);
                            _logger.LogInformation("User created a new account with password.");
                            var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                            var callbackUrl = Url.EmailConfirmationLink(user.Id, code, Request.Scheme);
                            await _emailSender.SendEmailConfirmationAsync(Input.Email, callbackUrl);
                        }
                    }
                    else
                    {
                        await _userManager.AddToRoleAsync(user, user.Role);
                        _logger.LogInformation("User created a new account with password.");
                        var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                        var callbackUrl = Url.EmailConfirmationLink(user.Id, code, Request.Scheme);
                        await _emailSender.SendEmailConfirmationAsync(Input.Email, callbackUrl);
                    }
                    
                    return LocalRedirect(Url.GetLocalUrl(returnUrl));
                }
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }

            // If we got this far, something failed, redisplay form
            return Page();
        }
    }
}
