using CarScope.Data;
using CarScope.Data.Migrations;
using CarScope.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BankDeposit = CarScope.Models.BankDeposit;
using BankTransfer = CarScope.Models.BankTransfer;
using BankWithDrawal = CarScope.Models.BankWithDrawal;

namespace CarScope.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BankServiceController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        public BankServiceController(ApplicationDbContext context)
        {
            _context = context;
            
        }
        [Route("Emp/All")]
        public string GetAllEmployees()
        {
            return "Response from GetAllEmployees Method";
        }
        [Route("Emp/ById")]
        public string GetEmployeeById()
        {
            return "Response from GetEmployeeById Method";
        }
        [HttpGet]
        [Route("getbankaccount")]
        public async Task<ActionResult<IEnumerable<Models.BankAccount>>> GetBankAccount()
        {
            return await _context.BankAccount.ToListAsync();
        }

        [HttpPost]
        [Route("postbankdeposit")]
        public async Task<ActionResult<BankDeposit>> PostBankAccount(BankDeposit bankDeposit)
        {
          
                var bn = (from a in _context.BankAccount
                          where a.AccNo == bankDeposit.AccNo
                          select a).FirstOrDefault();
                bn.AvailableBal += bankDeposit.Amount;
            bankDeposit.Balance = bn.AvailableBal;

                _context.Entry(bn).State = EntityState.Modified;
                _context.Add(bankDeposit);
                await _context.SaveChangesAsync();
                return CreatedAtAction("GetBankAccount", new { id = bankDeposit.ID }, bankDeposit);
            
        }

        // GET: api/BankService/5
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

        [HttpGet]
        [Route("getbankdeposit")]
        public async Task<ActionResult<IEnumerable<BankDeposit>>> GetBankDeposit()
        {
            return await _context.BankDeposit.ToListAsync();
        }

        

        public async Task DeductionAsync(string abc, decimal def)
        {
            //deduction from account
            var bacc=_context.BankAccount.FirstOrDefault(a=>a.AccNo == abc);
            bacc.AvailableBal -= def;
            _context.Entry(bacc).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }
        [HttpPost]
        [Route("postbanktransfer")]
        public async Task<ActionResult<BankTransfer>> PostBankTransfer(BankTransfer bankTransfer)
        {
            //addition to another account
            var bTran = (from a in _context.BankAccount
                         where a.AccNo == bankTransfer.AccNo2
                         select a).FirstOrDefault();

            bTran.AvailableBal += bankTransfer.Amount;
            _context.Entry(bTran).State = EntityState.Modified;
            await DeductionAsync(bankTransfer.AccNo, bankTransfer.Amount);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetBankTransfer", new { id = bankTransfer.ID }, bankTransfer);
        }

        [HttpPost]
        [Route("postbankwithdraw")]
        public async Task<ActionResult<BankWithDrawal>> PostBankWithdraw(BankWithDrawal bankWithDrawal)
        {
            _context.BankWithDrawal.Add(bankWithDrawal);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetBankAccount", new { id = bankWithDrawal.ID }, bankWithDrawal);
        }

    }
}
