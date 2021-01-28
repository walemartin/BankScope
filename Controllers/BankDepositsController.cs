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
    public class BankDepositsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public BankDepositsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: BankDeposits
        public async Task<IActionResult> Index()
        {
            return View(await _context.BankDeposit.ToListAsync());
        }

        // GET: BankDeposits/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var bankDeposit = await _context.BankDeposit
                .FirstOrDefaultAsync(m => m.ID == id);
            if (bankDeposit == null)
            {
                return NotFound();
            }

            return View(bankDeposit);
        }

        // GET: BankDeposits/Create
        public IActionResult Create()
        {
           
            return View();
        }

        // POST: BankDeposits/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,AccNo,Narration,Amount,Balance")] BankDeposit bankDeposit)
        {
            if (ModelState.IsValid)
            {
                var bn = (from a in _context.BankAccount
                          where a.AccNo == bankDeposit.AccNo
                          select a).FirstOrDefault();

                //_context.BankDeposit.Where(x => x.AccNo == bankDeposit.AccNo).(p => new BankAccount()
                //{
                //AvailableBal += bankDeposit.Amount
                //});

               //var yourEntity = await context.FirstOrDefaultAsync(e => e.Id = 1);
                bn.AvailableBal = bankDeposit.Amount+bn.AvailableBal;
                
                _context.Entry(bn).State = EntityState.Modified;
                _context.Add(bankDeposit);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(bankDeposit);
        }

        // GET: BankDeposits/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var bankDeposit = await _context.BankDeposit.FindAsync(id);
            if (bankDeposit == null)
            {
                return NotFound();
            }
            return View(bankDeposit);
        }

        // POST: BankDeposits/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,AccNo,Narration,Amount,Balance")] BankDeposit bankDeposit)
        {
            if (id != bankDeposit.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(bankDeposit);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BankDepositExists(bankDeposit.ID))
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
            return View(bankDeposit);
        }

        // GET: BankDeposits/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var bankDeposit = await _context.BankDeposit
                .FirstOrDefaultAsync(m => m.ID == id);
            if (bankDeposit == null)
            {
                return NotFound();
            }

            return View(bankDeposit);
        }

        // POST: BankDeposits/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var bankDeposit = await _context.BankDeposit.FindAsync(id);
            _context.BankDeposit.Remove(bankDeposit);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool BankDepositExists(int id)
        {
            return _context.BankDeposit.Any(e => e.ID == id);
        }
    }
}
