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
    public class BankTransfersController : Controller
    {
        private readonly ApplicationDbContext _context;

        public BankTransfersController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: BankTransfers
        public async Task<IActionResult> Index()
        {
            return View(await _context.BankTransfer.ToListAsync());
        }

        // GET: BankTransfers/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var bankTransfer = await _context.BankTransfer
                .FirstOrDefaultAsync(m => m.ID == id);
            if (bankTransfer == null)
            {
                return NotFound();
            }

            return View(bankTransfer);
        }

        // GET: BankTransfers/Create
        public async Task<IActionResult> Create(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var bankAccount = await _context.BankAccount.FindAsync(id);
            if (bankAccount == null)
            {
                return NotFound();
            }
            var BcK = new BankTransfer()
            {
                AccNo = bankAccount.AccNo,
                Amount=bankAccount.AvailableBal
            };
            return View(BcK);
        }
        public async Task DeductionAsync(string abc,decimal def)
        {
            //deduction from account
            var bDel = (from a in _context.BankAccount
                        where a.AccNo == abc
                        select a).FirstOrDefault();

            bDel.AvailableBal -= def;
            _context.Entry(bDel).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }
        // POST: BankTransfers/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,AccNo,Amount,Narration,AccNo2")] BankTransfer bankTransfer)
        {
            if (ModelState.IsValid)
            {
                //addition to another account
                var bTran = (from a in _context.BankAccount
                          where a.AccNo == bankTransfer.AccNo2
                          select a).FirstOrDefault();

                bTran.AvailableBal += bankTransfer.Amount;
                _context.Entry(bTran).State = EntityState.Modified;
                await DeductionAsync(bankTransfer.AccNo, bankTransfer.Amount);


                //_context.Add(bankTransfer);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index", "BankAccounts", null);
               // return RedirectToAction(nameof(Index));
            }
            return View(bankTransfer);
        }

        // GET: BankTransfers/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var bankTransfer = await _context.BankTransfer.FindAsync(id);
            if (bankTransfer == null)
            {
                return NotFound();
            }
            return View(bankTransfer);
        }

        // POST: BankTransfers/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,AccNo,Amount,Narration,AccNo2")] BankTransfer bankTransfer)
        {
            if (id != bankTransfer.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(bankTransfer);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BankTransferExists(bankTransfer.ID))
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
            return View(bankTransfer);
        }

        // GET: BankTransfers/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var bankTransfer = await _context.BankTransfer
                .FirstOrDefaultAsync(m => m.ID == id);
            if (bankTransfer == null)
            {
                return NotFound();
            }

            return View(bankTransfer);
        }

        // POST: BankTransfers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var bankTransfer = await _context.BankTransfer.FindAsync(id);
            _context.BankTransfer.Remove(bankTransfer);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool BankTransferExists(int id)
        {
            return _context.BankTransfer.Any(e => e.ID == id);
        }
    }
}
