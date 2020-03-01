using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BES.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BES.Areas.LMS.Controllers
{
    public class BaseController : Controller
    {
        protected readonly ApplicationDbContext _context;
        private readonly IHttpContextAccessor _httpContext;
        public short Inbox { get; set; }
        public short AcceptedRequests { get; set; }
        public short RejectedRequests { get; set; }
        public bool IsSpervisor { get; set; }
        public bool IsPD { get; set; }
        public bool IsHR { get; set; }
        public bool IsAccountExit { get; set; }
        public int EmployeeID { get; set; }
        public int SectionID { get; set; }
        public BaseController(ApplicationDbContext context,IHttpContextAccessor httpContextAccessor)
        {
            _context = context;
            _httpContext = httpContextAccessor;
            var loginName = httpContextAccessor.HttpContext.User.Identity.Name;
            var employee = _context.Employee.Where(a => a.Name == loginName).FirstOrDefault();            
            if(employee != null)
            {
                SectionID = employee.SectionID;
                EmployeeID = employee.EmployeeID;
                IsAccountExit = true;
                IsSpervisor = employee.IsSectionHead;
                if (IsSpervisor)
                {
                    Inbox = (short)_context.LeaveRequest.Include(a=>a.Employee).Count(a => a.Employee.SectionID == SectionID && a.ApprovedBySection == 0 && a.EmployeeID != EmployeeID);
                    AcceptedRequests = (short)_context.LeaveRequest.Include(a => a.Employee).Count(a => a.Employee.SectionID == employee.SectionID && a.ApprovedBySection == 1 && a.EmployeeID != EmployeeID);
                    RejectedRequests = (short)_context.LeaveRequest.Include(a => a.Employee).Count(a => a.Employee.SectionID == employee.SectionID && a.ApprovedBySection == 2 && a.EmployeeID != EmployeeID);
                }
                IsPD = EmployeeID == 3 ? true : false;//Static
                if (IsPD)
                {
                    Inbox = (short)_context.LeaveRequest.Include(a => a.Employee).Count(a => a.ApprovedBySection == 1 && a.ApprovedByPD == 0);
                    AcceptedRequests = (short)_context.LeaveRequest.Include(a => a.Employee).Count(a => a.ApprovedBySection == 1 && a.ApprovedByPD == 1);
                    RejectedRequests = (short)_context.LeaveRequest.Include(a => a.Employee).Count(a => a.ApprovedBySection == 1 && a.ApprovedByPD == 2);
                }
                IsHR = EmployeeID == 81 ? true : false;//Static
            }                                   
        }          
    }
}