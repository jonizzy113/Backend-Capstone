using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using BackendCapstone.Data;
using BackendCapstone.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;
using BackendCapstone.Models.ViewModels;

namespace BackendCapstone.Controllers
{
    [Authorize]
    public class UserController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        private Task<ApplicationUser> GetCurrentUserAsync() => _userManager.GetUserAsync(HttpContext.User);


        public UserController(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public async Task<IActionResult> Index()
        {
            return View(await _context.UserType.ToListAsync());
        }

        public async Task<IActionResult> GetSalesman()
        {
            var viewModel = new SalesmanIndexViewModel();
            var user = await GetCurrentUserAsync();
            List<ApplicationUser> salesman = GetAllSalesman();
            viewModel.Salesman = salesman;
            viewModel.IsOffice = user.UserTypeId == 1;

            return View(viewModel);
        }
        public async Task<IActionResult> GetBroker()
        {
            var viewModel = new BrokerIndexViewModel();
            var user = await GetCurrentUserAsync();
            List<ApplicationUser> broker = GetAllBrokers();
            viewModel.Broker = broker;
            viewModel.IsOffice = user.UserTypeId == 1;

            return View(viewModel);
        }
        private bool UserExits(string id)
        {
            return _context.ApplicationUsers.Any(e => e.Id == id);
        }
        private List<ApplicationUser> GetAllSalesman()
        {
            var applicationUsers = _context.ApplicationUsers
                .Include(u => u.UserType)
                .Where(u => u.UserType.Type == "Salesman").ToList();
            return applicationUsers;
        }
        private List<ApplicationUser> GetAllBrokers()
        {
            var applicationUsers = _context.ApplicationUsers
                .Include(u => u.UserType)
                .Where(u => u.UserType.Type == "Broker").ToList();
            return applicationUsers;
        }
    }
}
