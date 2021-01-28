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
    public class BankWithDrawalsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public BankWithDrawalsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: BankWithDrawals
        public async Task<IActionResult> Index()
        {
            return View(await _context.BankWithDrawal.ToListAsync());
        }

        // GET: BankWithDrawals/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var bankWithDrawal = await _context.BankWithDrawal
                .FirstOrDefaultAsync(m => m.ID == id);
            if (bankWithDrawal == null)
            {
                return NotFound();
            }

            return View(bankWithDrawal);
        }

        // GET: BankWithDrawals/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: BankWithDrawals/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,AccNo,Narration,Amount,Balance")] BankWithDrawal bankWithDrawal)
        {
            if (ModelState.IsValid)
            {
                _context.Add(bankWithDrawal);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(bankWithDrawal);
        }

        // GET: BankWithDrawals/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var bankWithDrawal = await _context.BankWithDrawal.FindAsync(id);
            if (bankWithDrawal == null)
            {
                return NotFound();
            }
            return View(bankWithDrawal);
        }

        // POST: BankWithDrawals/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,AccNo,Narration,Amount,Balance")] BankWithDrawal bankWithDrawal)
        {
            if (id != bankWithDrawal.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(bankWithDrawal);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BankWithDrawalExists(bankWithDrawal.ID))
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
            return View(bankWithDrawal);
        }

        // GET: BankWithDrawals/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var bankWithDrawal = await _context.BankWithDrawal
                .FirstOrDefaultAsync(m => m.ID == id);
            if (bankWithDrawal == null)
            {
                return NotFound();
            }

            return View(bankWithDrawal);
        }

        // POST: BankWithDrawals/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var bankWithDrawal = await _context.BankWithDrawal.FindAsync(id);
            _context.BankWithDrawal.Remove(bankWithDrawal);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool BankWithDrawalExists(int id)
        {
            return _context.BankWithDrawal.Any(e => e.ID == id);
        }
    }
}
