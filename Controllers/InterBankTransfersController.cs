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
    public class InterBankTransfersController : Controller
    {
        private readonly ApplicationDbContext _context;

        public InterBankTransfersController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: InterBankTransfers
        public async Task<IActionResult> Index()
        {
            return View(await _context.InterBankTransfer.ToListAsync());
        }

        // GET: InterBankTransfers/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var interBankTransfer = await _context.InterBankTransfer
                .FirstOrDefaultAsync(m => m.ID == id);
            if (interBankTransfer == null)
            {
                return NotFound();
            }

            return View(interBankTransfer);
        }

        // GET: InterBankTransfers/Create
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
            var BcK = new InterBankTransfer()
            {
                AccNo = bankAccount.AccNo,
                AvailableBal = bankAccount.AvailableBal
            };
            return View(BcK);
        }
        public async Task DeductionAsync(string abc, decimal def)
        {
            //deduction from account
            var bDel = (from a in _context.BankAccount
                        where a.AccNo == abc
                        select a).FirstOrDefault();

            bDel.AvailableBal -= def;
            _context.Entry(bDel).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }
        // POST: InterBankTransfers/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,AccNo,AvailableBal,Narration,AccNo2,TransferAmount")] InterBankTransfer interBankTransfer)
        {
            if (ModelState.IsValid)
            {
                var bDel = (from a in _context.BankAccount
                            where a.AccNo == interBankTransfer.AccNo
                            select a).FirstOrDefault();
                if (interBankTransfer.TransferAmount < bDel.AvailableBal)
                {
                    var bTran = (from a in _context.BankAccount
                                 where a.AccNo == interBankTransfer.AccNo2
                                 select a).FirstOrDefault();

                    bTran.AvailableBal += interBankTransfer.TransferAmount;
                    _context.Entry(bTran).State = EntityState.Modified;
                    await DeductionAsync(interBankTransfer.AccNo, interBankTransfer.TransferAmount);



                    await _context.SaveChangesAsync();
                    return RedirectToAction("Index", "BankAccounts", null);
                }
                //addition to another account
                else
                {
                    ViewBag.Message = string.Format("Hello {0}.\nthe current transfer amount {1} is higher than {2}, \ninsufficient funds", DateTime.Now.ToString(), interBankTransfer.TransferAmount, bDel.AvailableBal);
                    return View(interBankTransfer);
                }
                
                
            }
            return View(interBankTransfer);
        }

        // GET: InterBankTransfers/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var interBankTransfer = await _context.InterBankTransfer.FindAsync(id);
            if (interBankTransfer == null)
            {
                return NotFound();
            }
            return View(interBankTransfer);
        }

        // POST: InterBankTransfers/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,AccNo,AvailableBal,Narration,AccNo2,TransferAmount")] InterBankTransfer interBankTransfer)
        {
            if (id != interBankTransfer.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(interBankTransfer);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!InterBankTransferExists(interBankTransfer.ID))
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
            return View(interBankTransfer);
        }

        // GET: InterBankTransfers/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var interBankTransfer = await _context.InterBankTransfer
                .FirstOrDefaultAsync(m => m.ID == id);
            if (interBankTransfer == null)
            {
                return NotFound();
            }

            return View(interBankTransfer);
        }

        // POST: InterBankTransfers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var interBankTransfer = await _context.InterBankTransfer.FindAsync(id);
            _context.InterBankTransfer.Remove(interBankTransfer);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool InterBankTransferExists(int id)
        {
            return _context.InterBankTransfer.Any(e => e.ID == id);
        }
    }
}
