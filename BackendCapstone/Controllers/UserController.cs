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


        // GET: User/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var userType = await _context.UserType
                .FirstOrDefaultAsync(m => m.Id == id);
            if (userType == null)
            {
                return NotFound();
            }

            return View(userType);
        }

        // GET: User/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: User/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Type")] UserType userType)
        {
            if (ModelState.IsValid)
            {
                _context.Add(userType);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(userType);
        }

        // GET: User/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var userType = await _context.UserType.FindAsync(id);
            if (userType == null)
            {
                return NotFound();
            }
            return View(userType);
        }

        // POST: User/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Type")] UserType userType)
        {
            if (id != userType.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(userType);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!UserTypeExists(userType.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(userType);
        }

        // GET: User/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var userType = await _context.UserType
                .FirstOrDefaultAsync(m => m.Id == id);
            if (userType == null)
            {
                return NotFound();
            }

            return View(userType);
        }

        // POST: User/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var userType = await _context.UserType.FindAsync(id);
            _context.UserType.Remove(userType);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool UserTypeExists(int id)
        {
            return _context.UserType.Any(e => e.Id == id);
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
