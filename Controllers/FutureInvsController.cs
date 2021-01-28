using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using CarScope.Data;
using CarScope.Models;

namespace CarScope.Controllers
{
    public class FutureInvsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public FutureInvsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: FutureInvs
        public async Task<IActionResult> Index()
        {
            return View(await _context.FutureInv.ToListAsync());
        }

        // GET: FutureInvs/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var futureInv = await _context.FutureInv
                .FirstOrDefaultAsync(m => m.ID == id);
            if (futureInv == null)
            {
                return NotFound();
            }

            return View(futureInv);
        }

        // GET: FutureInvs/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: FutureInvs/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,ContractNo,Investment")] FutureInv futureInv)
        {
            if (ModelState.IsValid)
            {
                _context.Add(futureInv);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(futureInv);
        }

        // GET: FutureInvs/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var futureInv = await _context.FutureInv.FindAsync(id);
            if (futureInv == null)
            {
                return NotFound();
            }
            return View(futureInv);
        }

        // POST: FutureInvs/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,ContractNo,Investment")] FutureInv futureInv)
        {
            if (id != futureInv.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(futureInv);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!FutureInvExists(futureInv.ID))
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
            return View(futureInv);
        }

        // GET: FutureInvs/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var futureInv = await _context.FutureInv
                .FirstOrDefaultAsync(m => m.ID == id);
            if (futureInv == null)
            {
                return NotFound();
            }

            return View(futureInv);
        }

        // POST: FutureInvs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var futureInv = await _context.FutureInv.FindAsync(id);
            _context.FutureInv.Remove(futureInv);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool FutureInvExists(int id)
        {
            return _context.FutureInv.Any(e => e.ID == id);
        }
    }
}
