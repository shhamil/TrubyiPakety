using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using TrubyiPakety.Models;
using TrubyiPakety.ViewModel;

namespace TrubyiPakety.Controllers
{
    public class PipesController : Controller
    {
        private readonly TrubyiPaketyDbContext _context;

        public PipesController(TrubyiPaketyDbContext context)
        {
            _context = context;
        }

        public IActionResult Index(string filter = null)
        {
            var pipes = _context.Pipes.AsEnumerable();
            if (!string.IsNullOrEmpty(filter))
            {
                pipes = pipes.Where(p => p.Quality == filter);
            }

            var viewModel = new PipeIndexViewModel
            {
                Pipes = pipes.ToList(),
                TotalCount = pipes.Count(),
                GoodCount = pipes.Count(p => p.Quality == "Good"),
                DefectiveCount = pipes.Count(p => p.Quality == "Defective"),
                TotalWeight = pipes.Sum(p => p.Weight)
            };

            return View(viewModel);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Pipe pipe)
        {
            if (ModelState.IsValid)
            {
                _context.Pipes.Add(pipe);
                _context.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            return View(pipe);
        }

        public IActionResult Edit(int id)
        {
            var pipe = _context.Pipes.FirstOrDefault(p => p.Id == id);
            if (pipe == null || pipe.PackageId != null)
            {
                return NotFound();
            }
            return View(pipe);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Pipe pipe)
        {
            if (ModelState.IsValid)
            {
                _context.Update(pipe);
                _context.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            return View(pipe);
        }

        public IActionResult Delete(int id)
        {
            var pipe = _context.Pipes.FirstOrDefault(p => p.Id == id);
            if (pipe == null || pipe.PackageId != null)
            {
                return NotFound();
            }
            return View(pipe);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            var pipe = _context.Pipes.FirstOrDefault(p => p.Id == id);
            if (pipe != null && pipe.PackageId != null)
            {
                _context.Pipes.Remove(pipe);
                _context.SaveChanges();
            }
            return RedirectToAction(nameof(Index));
        }
    }
}
