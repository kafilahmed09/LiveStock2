using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LIVESTOCK.Areas.website.Controllers
{
    [Area("website")]
    public class WebAdminController : Controller
    {
        [Authorize(Roles = "Website")]
        public IActionResult Index()
        {
            return View();
        }
    }
}