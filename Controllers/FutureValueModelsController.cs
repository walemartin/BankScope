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
    public class FutureValueModelsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public FutureValueModelsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: FutureValueModels
        public async Task<IActionResult> Index()
        {
           

            return View(await _context.FutureValueModel.ToListAsync());
        }

        // GET: FutureValueModels/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var futureValueModel = await _context.FutureValueModel
                .FirstOrDefaultAsync(m => m.ID == id);
            if (futureValueModel == null)
            {
                return NotFound();
            }

            return View(futureValueModel);
        }

        // GET: FutureValueModels/Create
        public IActionResult Create()
        {
            Random fn = new Random();
            var st = fn.Next();

            FutureValueModel fv = new FutureValueModel()
            {
                ContractNo = st.ToString(),
            };   
            return View(fv);
        }

        // POST: FutureValueModels/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,MonthlyInvestment,YearlyInterestRate,Years")] FutureValueModel futureValueModel,FutureInv fv)
        {
            if (ModelState.IsValid)
            {
                int months = futureValueModel.Years * 12;
                decimal monthlyInterestRate = futureValueModel.YearlyInterestRate / 12 / 100;
                decimal futureValue = 0;
                for (int i = 0; i < months; i++)
                {
                    futureValue = (futureValue + futureValueModel.MonthlyInvestment) * (1 + monthlyInterestRate);
                    fv.ContractNo = futureValueModel.ContractNo;
                    fv.Investment = futureValue;
                    _context.Add(fv);
                    await _context.SaveChangesAsync();
                }
                _context.Add(futureValueModel);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(futureValueModel);
        }

        // GET: FutureValueModels/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var futureValueModel = await _context.FutureValueModel.FindAsync(id);
            if (futureValueModel == null)
            {
                return NotFound();
            }
            return View(futureValueModel);
        }

        // POST: FutureValueModels/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,MonthlyInvestment,YearlyInterestRate,Years")] FutureValueModel futureValueModel)
        {
            if (id != futureValueModel.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(futureValueModel);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!FutureValueModelExists(futureValueModel.ID))
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
            return View(futureValueModel);
        }

        // GET: FutureValueModels/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var futureValueModel = await _context.FutureValueModel
                .FirstOrDefaultAsync(m => m.ID == id);
            if (futureValueModel == null)
            {
                return NotFound();
            }

            return View(futureValueModel);
        }

        // POST: FutureValueModels/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var futureValueModel = await _context.FutureValueModel.FindAsync(id);
            _context.FutureValueModel.Remove(futureValueModel);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool FutureValueModelExists(int id)
        {
            return _context.FutureValueModel.Any(e => e.ID == id);
        }
    }
}
