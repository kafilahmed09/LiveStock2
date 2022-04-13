using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LIVESTOCK.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace LIVESTOCK.Pages
{
    public class IndexModel : PageModel
    {
        private readonly UserManager<ApplicationUser> _userManager;
        public string ReturnUrl { get; set; }
        public IndexModel(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }
        //public void OnGet()
        //{                        
        //}
        public async Task<IActionResult> OnGetAsync(string returnUrl = null)
        {
            if (User.Identity.IsAuthenticated)
            {
                var user = await _userManager.FindByNameAsync(User.Identity.Name);
                // Get the roles for the user
                var roles = await _userManager.GetRolesAsync(user);
                if (roles.ElementAt(0).ToString() == "CovidAdmin")
                {
                    return RedirectToAction("Index", "PatientResults", new { Area = "CovidLab" });
                }
                if (roles.ElementAt(0).ToString() == "CovidLab")
                {
                    return RedirectToAction("Dashboard", "PatientResults", new { Area = "CovidLab" });
                }
                if (roles.ElementAt(0).ToString() == "Website")
                {
                    return RedirectToAction("Index", "Site", new { Area = "Website" });
                }
                if (roles.ElementAt(0).ToString() == "SMS")
                {
                    return RedirectToAction("Index", "Home", new { Area = "SMS" });
                }
            }
            else
            {
                return RedirectToAction("Index", "Site",new { Area = "Website"});
            }

            return Page();
        }
    }
}
