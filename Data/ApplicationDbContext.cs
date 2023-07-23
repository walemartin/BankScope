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
        public DbSet<Car> Car { get; set; }
        public DbSet<Product> Product { get; set; }
        public DbSet<FutureValueModel> FutureValueModel { get; set; }
        public DbSet<FutureInv> FutureInv { get; set; }
        public DbSet<BankAccount> BankAccount { get; set; }
        public DbSet<BankDeposit> BankDeposit { get; set; }
        public DbSet<BankWithDrawal> BankWithDrawal { get; set; }
        public DbSet<BankTransfer> BankTransfer { get; set; }
        public DbSet<InterBankTransfer> InterBankTransfer { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            Random rnd = new Random();
            modelBuilder.Entity<BankAccount>()
                .HasData(
                    new BankAccount
                    {
                        ID=1,
                        Name = "Isaac Newton",
                        AccNo = "PL"+rnd.Next(10000,20000),
                        AvailableBal=1000
                    },
                    new BankAccount
                    {
                        ID=2,
                        Name = "John Doe",
                        AccNo = "PL" + rnd.Next(10000, 20000),
                        AvailableBal = 1200
                    },
                    new BankAccount
                    {ID = 3,
                        Name = "Micheal Faraday",
                        AccNo = "PL" + rnd.Next(10000, 20000),
                        AvailableBal = 600
                    }
                );
        }
    }
}
