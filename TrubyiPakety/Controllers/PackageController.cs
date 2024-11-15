using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using TrubyiPakety.Models;
using TrubyiPakety.ViewModel;

namespace TrubyiPakety.Controllers
{
    public class PackageController : Controller
    {
        private readonly TrubyiPaketyDbContext _context;

        public PackageController(TrubyiPaketyDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var packages = _context.Packages.ToList();
            return View(packages);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Package package)
        {
            if (ModelState.IsValid)
            {
                package.Date = DateTime.SpecifyKind(package.Date, DateTimeKind.Utc);
                _context.Packages.Add(package);
                _context.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            return View(package);
        }

        public IActionResult AddPipesToPackage(int packageId)
        {
            var package = _context.Packages.Include(p => p.Pipes).FirstOrDefault(p => p.Id == packageId);
            if (package == null) return NotFound();
            var pipes = _context.Pipes.Where(p => p.PackageId == null).ToList();
            var model = new AddPipesToPackageViewModel() { Package = package, AvailablePipes = pipes };
            return View(model);
        }

        [HttpPost]
        public IActionResult AddPipesToPackage(int packageId, List<int> selectedPipeIds)
        {
            var package = _context.Packages.Include(p => p.Pipes).FirstOrDefault(p => p.Id == packageId);
            if (package == null) return NotFound();

            var pipes = _context.Pipes.Where(p => selectedPipeIds.Contains(p.Id)).ToList();
            foreach (var pipe in pipes)
            {
                pipe.PackageId = packageId;
            }
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        public IActionResult RemovePipeFromPackage(int pipeId)
        {
            var pipe = _context.Pipes.Find(pipeId);
            if (pipe != null)
            {
                pipe.PackageId = null;
                _context.SaveChanges();
            }
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Edit(int id)
        {
            var package = _context.Packages
                .Include(p => p.Pipes)
                .FirstOrDefault(p => p.Id == id);

            if (package == null) return NotFound();

            return View(package);
        }


        [HttpPost]
        public IActionResult Edit(Package package)
        {
            if (ModelState.IsValid)
            {
                package.Date = DateTime.SpecifyKind(package.Date, DateTimeKind.Utc);
                _context.Update(package);
                _context.SaveChanges();
                return RedirectToAction(nameof(Index));
            }

            return View(package);
        }

        public IActionResult Delete(int id)
        {
            var package = _context.Packages.FirstOrDefault(p => p.Id == id);
            if (package == null)
            {
                return NotFound();
            }
            return View(package);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            var package = _context.Packages.FirstOrDefault(p => p.Id == id);
            if (package != null)
            {
                _context.Packages.Remove(package);
                _context.SaveChanges();
            }
            return RedirectToAction(nameof(Index));
        }

        
    }
}
