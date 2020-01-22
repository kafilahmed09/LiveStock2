using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BES.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BES.Areas.LMS.Controllers
{
    [Area("LMS")]
    public class LeaveSummariesController : Controller
    {
        private readonly ApplicationDbContext _context;
        public LeaveSummariesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: LMS/EmpLeaveSummaries
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.LeaveSummaries;
            return View(await applicationDbContext.ToListAsync());
        }
    }
}