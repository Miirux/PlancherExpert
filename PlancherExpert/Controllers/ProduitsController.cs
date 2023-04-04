 using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using PlancherExpert.Models;

namespace PlancherExpert.Controllers
{
    public class ProduitsController : Controller
    {
        private readonly PlancherExpertContext _context;

        public ProduitsController(PlancherExpertContext context)
        {
            _context = context;
        }

        // GET: Produits
        public async Task<IActionResult> Index()
        {
            return _context.CouvrePlanchers != null ?
                          View(await _context.CouvrePlanchers.ToListAsync()) :
                          Problem("Entity set 'PlancherExpertContext.CouvrePlanchers'  is null.");
        }

        // GET: Produits/Create
        public IActionResult Create(int id)
        {
            var product = _context.CouvrePlanchers.Find(id);
            if (product == null)
            {
                return NotFound();
            }
            
            ViewData["TypePlancher"] = product.TypePlancher.ToString();
            ViewData["PrixMat"] = product.PrixMat.ToString();
            ViewData["PrixMainOeuvre"] = product.PrixMainOeuvre.ToString();
            ViewData["IdClient"] = new SelectList(_context.Clients, "Id", "Id");
            return View();
        }

        // POST: Produits/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,TypePlancher,Superficie,PrixMat,PrixMainOeuvre,IdClient")] Commande commande)
        {
            if (ModelState.IsValid)
            {
                _context.Add(commande);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["IdClient"] = new SelectList(_context.Clients, "Id", "Id", commande.IdClient);
            return View(commande);
        }

        private bool CommandeExists(int id)
        {
          return (_context.Commandes?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
