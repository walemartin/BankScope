using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using CarScope.Models;

namespace CarScope.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public DbSet<CarScope.Models.Car> Car { get; set; }
        public DbSet<CarScope.Models.Product> Product { get; set; }
        public DbSet<CarScope.Models.FutureValueModel> FutureValueModel { get; set; }
        public DbSet<CarScope.Models.FutureInv> FutureInv { get; set; }
        public DbSet<CarScope.Models.BankAccount> BankAccount { get; set; }
        public DbSet<CarScope.Models.BankDeposit> BankDeposit { get; set; }
        public DbSet<CarScope.Models.BankWithDrawal> BankWithDrawal { get; set; }
        public DbSet<CarScope.Models.BankTransfer> BankTransfer { get; set; }
        public DbSet<CarScope.Models.InterBankTransfer> InterBankTransfer { get; set; }
    }
}
