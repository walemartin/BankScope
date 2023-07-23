using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CarScope.Data;
using CarScope.Models;

namespace CarScope.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BankDepositApiController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public BankDepositApiController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/BankDepositApi
        [HttpGet]
        public async Task<ActionResult<IEnumerable<BankDeposit>>> GetBankDeposit()
        {
            return await _context.BankDeposit.ToListAsync();
        }

        // GET: api/BankDepositApi/5
        [HttpGet("{id}")]
        public async Task<ActionResult<BankDeposit>> GetBankDeposit(int id)
        {
            var bankDeposit = await _context.BankDeposit.FindAsync(id);

            if (bankDeposit == null)
            {
                return NotFound();
            }

            return bankDeposit;
        }

        // PUT: api/BankDepositApi/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutBankDeposit(int id, BankDeposit bankDeposit)
        {
            if (id != bankDeposit.ID)
            {
                return BadRequest();
            }

            _context.Entry(bankDeposit).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!BankDepositExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/BankDepositApi
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<BankDeposit>> PostBankDeposit(BankDeposit bankDeposit)
        {
            _context.BankDeposit.Add(bankDeposit);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetBankDeposit", new { id = bankDeposit.ID }, bankDeposit);
        }

        // DELETE: api/BankDepositApi/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<BankDeposit>> DeleteBankDeposit(int id)
        {
            var bankDeposit = await _context.BankDeposit.FindAsync(id);
            if (bankDeposit == null)
            {
                return NotFound();
            }

            _context.BankDeposit.Remove(bankDeposit);
            await _context.SaveChangesAsync();

            return bankDeposit;
        }

        private bool BankDepositExists(int id)
        {
            return _context.BankDeposit.Any(e => e.ID == id);
        }
    }
}
