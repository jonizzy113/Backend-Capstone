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

namespace BackendCapstone.Controllers
{
    public class VehiclesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public VehiclesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Vehicles
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Vehicles.Include(v => v.Broker).Include(v => v.Customer).Include(v => v.Salesman);
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
            //var applicationDbContext = _context.ApplicationUsers
            //    .Include(u => u.UserType)
            //    .Where(u => u.UserType.Type == "Broker").ToListAsync();
            //ViewData["BrokerId"] = new SelectList(_context.ApplicationUsers, "Id", "Id");
            //return View();
        }

        // POST: Vehicles/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create( VehicleCreateViewModel viewModel)
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
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var vehicle = await _context.Vehicles.FindAsync(id);
            if (vehicle == null)
            {
                return NotFound();
            }
            ViewData["BrokerId"] = new SelectList(_context.ApplicationUsers, "Id", "Id", vehicle.BrokerId);
            ViewData["CustomerId"] = new SelectList(_context.Customers, "CustomerId", "Address", vehicle.CustomerId);
            ViewData["SalesmanId"] = new SelectList(_context.ApplicationUsers, "Id", "Id", vehicle.SalesmanId);
            return View(vehicle);
        }

        // POST: Vehicles/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("VehicleId,Make,Model,Mileage,BrokerPrice,AddedCost,BrokerId,SalesmanId,CustomerId,SoldPrice")] Vehicle vehicle)
        {
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
            ViewData["BrokerId"] = new SelectList(_context.ApplicationUsers, "Id", "Id", vehicle.BrokerId);
            ViewData["CustomerId"] = new SelectList(_context.Customers, "CustomerId", "Address", vehicle.CustomerId);
            ViewData["SalesmanId"] = new SelectList(_context.ApplicationUsers, "Id", "Id", vehicle.SalesmanId);
            return View(vehicle);
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
            var applicationUsers =_context.ApplicationUsers
                .Include(u => u.UserType)
                .Where(u => u.UserType.Type == "Broker").ToList();
            return applicationUsers;
        }
    }
}
