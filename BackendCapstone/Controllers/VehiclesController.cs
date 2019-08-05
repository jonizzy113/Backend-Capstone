using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using BackendCapstone.Data;
using BackendCapstone.Models;
using BackendCapstone.Models.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;

namespace BackendCapstone.Controllers
{
    [Authorize]
    public class VehiclesController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public VehiclesController(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        private Task<ApplicationUser> GetCurrentUserAsync() => _userManager.GetUserAsync(HttpContext.User);

        // GET: Vehicles
        public async Task<IActionResult> Index()
        {
            var viewModel = new VehicleIndexViewModel();
            var user = await GetCurrentUserAsync();
            List<Vehicle> vehicles = GetVehicles();
            viewModel.Vehicles = vehicles;
            viewModel.IsOffice = user.UserTypeId == 1;
            return View(viewModel);
        }

        public async Task<IActionResult> MyIndex()
        {
            var currentUser = await GetCurrentUserAsync();
            var applicationDbContext = _context.Vehicles.Where(v => v.BrokerId == currentUser.Id)
                .Include(v => v.Broker)
                .Include(v => v.Customer)
                .Include(v => v.Salesman);
            return View(await applicationDbContext.ToListAsync());


        }

        public async Task<IActionResult> SoldCars()
        {
            var applicationDbContext  = _context.Vehicles.Where(v => v.CustomerId != null)
                .Include(v => v.Broker)
                .Include(v => v.Customer)
                .Include(v => v.Salesman);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Vehicles/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var vehicle = await _context.Vehicles
                .Include(v => v.Broker)
                .Include(v => v.Customer)
                .Include(v => v.Salesman)
                .FirstOrDefaultAsync(m => m.VehicleId == id);
            if (vehicle == null)
            {
                return NotFound();
            }

            return View(vehicle);
        }

        // GET: Vehicles/Create
        public IActionResult Create()
        {
            var viewModel = new VehicleCreateViewModel
            {
                Brokers = _context.ApplicationUsers.ToList()
            };
            List<ApplicationUser> brokers = GetAllBrokers();
            viewModel.Brokers = brokers;
            return View(viewModel);
        }


        // POST: Vehicles/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(VehicleCreateViewModel viewModel)
        {
            ModelState.Remove("Vehicle.Customer");
            ModelState.Remove("Vehicle.Salesman");
            ModelState.Remove("Vehicle.Broker");
            if (ModelState.IsValid)
            {
                var vehicle = viewModel.Vehicle;
                _context.Add(vehicle);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            viewModel.Brokers = await _context.ApplicationUsers.ToListAsync();
            return View(viewModel);
        }

        // GET: Vehicles/Edit/5
        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var vehicle = _context.Vehicles.Find(id);
            if(vehicle == null)
            {
                return NotFound();
            }
            var viewModel = new VehicleEditViewModel()
            {
                Vehicle = vehicle,
                Broker = _context.ApplicationUsers.ToList(),
                Customer = _context.Customers.ToList(),
                Salesman = _context.ApplicationUsers.ToList()
            };
            List<ApplicationUser> salesman = GetAllSalesman();
            List<ApplicationUser> brokers = GetAllBrokers();
            List<Customer> customers = GetAllCustomers();
            viewModel.Customer = customers;
            viewModel.Broker = brokers;
            viewModel.Salesman = salesman;
            if(viewModel == null)
            {
                return NotFound();
            }
            return View(viewModel);
        }

        // POST: Vehicles/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id,  VehicleEditViewModel viewModel)
        {
            ModelState.Remove("Vehicle.Customer");
            ModelState.Remove("Vehicle.Salesman");
            ModelState.Remove("Vehicle.Broker");
            var vehicle = viewModel.Vehicle;
            if (id != vehicle.VehicleId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(vehicle);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!VehicleExists(vehicle.VehicleId))
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
            viewModel.Salesman = GetAllSalesman();
            viewModel.Broker = GetAllBrokers();
            viewModel.Customer = GetAllCustomers();
            return View(viewModel);
        }

        public IActionResult EditSoldVehicle(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var vehicle = _context.Vehicles.Find(id);
            if (vehicle == null)
            {
                return NotFound();
            }
            var viewModel = new VehicleEditViewModel()
            {
                Vehicle = vehicle,
                Broker = _context.ApplicationUsers.ToList(),
                Customer = _context.Customers.ToList(),
                Salesman = _context.ApplicationUsers.ToList()
            };
            List<ApplicationUser> salesman = GetAllSalesman();
            List<ApplicationUser> brokers = GetAllBrokers();
            List<Customer> customers = GetAllCustomers();
            viewModel.Customer = customers;
            viewModel.Broker = brokers;
            viewModel.Salesman = salesman;
            if (viewModel == null)
            {
                return NotFound();
            }
            return View(viewModel);
        }

        // POST: Vehicles/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditSoldVehicle(int id, VehicleEditViewModel viewModel)
        {
            ModelState.Remove("Vehicle.Customer");
            ModelState.Remove("Vehicle.Salesman");
            ModelState.Remove("Vehicle.Broker");
            var vehicle = viewModel.Vehicle;
            if (id != vehicle.VehicleId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(vehicle);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!VehicleExists(vehicle.VehicleId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(SoldCars));
            }
            viewModel.Salesman = GetAllSalesman();
            viewModel.Broker = GetAllBrokers();
            viewModel.Customer = GetAllCustomers();
            return View(viewModel);
        }

        // GET: Vehicles1/Edit/5
        public   async Task<IActionResult> EditInventory(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var vehicle = _context.Vehicles.Find(id);
            if (vehicle == null)
            {
                return NotFound();
            }
            var viewModel = new VehicleEditViewModel()
            {
                Vehicle = vehicle,
                Broker = _context.ApplicationUsers.ToList(),
            };
            List<ApplicationUser> brokers = GetAllBrokers();
            viewModel.Broker = brokers;
            if (viewModel == null)
            {
                return NotFound();
            }
            return View(viewModel);
        }

        // POST: Vehicles1/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditInventory(int id, VehicleEditViewModel viewModel)
        {
            ModelState.Remove("Vehicle.Customer");
            ModelState.Remove("Vehicle.Salesman");
            ModelState.Remove("Vehicle.Broker");
            var vehicle = viewModel.Vehicle;
            if (id != vehicle.VehicleId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(vehicle);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!VehicleExists(vehicle.VehicleId))
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
            viewModel.Broker = GetAllBrokers();
            return View(viewModel);
        }


        // GET: Vehicles/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var vehicle = await _context.Vehicles
                .Include(v => v.Broker)
                .Include(v => v.Customer)
                .Include(v => v.Salesman)
                .FirstOrDefaultAsync(m => m.VehicleId == id);
            if (vehicle == null)
            {
                return NotFound();
            }

            return View(vehicle);
        }

        // POST: Vehicles/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var vehicle = await _context.Vehicles.FindAsync(id);
            _context.Vehicles.Remove(vehicle);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool VehicleExists(int id)
        {
            return _context.Vehicles.Any(e => e.VehicleId == id);
        }
        private List<ApplicationUser> GetAllBrokers()
        {
            var applicationUsers = _context.ApplicationUsers
                .Include(u => u.UserType)
                .Where(u => u.UserType.Type == "Broker").ToList();
            return applicationUsers;
        }
        private List<ApplicationUser> GetAllSalesman()
        {
            var applicationUsers = _context.ApplicationUsers
                .Include(u => u.UserType)
                .Where(u => u.UserType.Type == "Salesman").ToList();
            return applicationUsers;
        }
        private List<Vehicle> GetVehicles()
        {
            var allVehicles = _context.Vehicles
                .Include(v => v.Broker)
                .Include(v => v.Customer)
                .Include(v => v.Salesman)
                .Where(v => v.CustomerId == null)
                .ToList();

            return allVehicles;
        }
        private List<Customer> GetAllCustomers()
        {
            var allCustomers = _context.Customers.ToList();
            return allCustomers;
        }
    }
}
