using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LIVESTOCK.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;

namespace LIVESTOCK.Areas.website.Controllers
{
    [Area("website")]    
    public class BaselineController : Controller
    {
        
        protected readonly ApplicationDbContext _context;
        protected string[] Modes = null;
        protected string[,] ImportantLinks;
        public BaselineController(ApplicationDbContext context)
        {            
            _context = context;
            Modes = _context.SocialMedia.Select(a => a.Address).ToArray<string>();
            var ImportantLinkList = _context.ImportantLink.Select(p => new { p.Name,p.Address}).ToList();

            int counter = 0;
            ImportantLinks = new string[2,ImportantLinkList.Count()];
            foreach(var val in ImportantLinkList)
            {
                ImportantLinks[0, counter] = val.Name;
                ImportantLinks[1, counter] = val.Address;
                counter++;
            }
        }                
    }
}