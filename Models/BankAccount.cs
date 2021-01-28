using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CarScope.Models
{
    public class BankAccount
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string AccNo { get; set; }

        public decimal AvailableBal { get; set; }

    }
    public class BankDeposit
    {
        public int ID { get; set; }
        public string AccNo { get; set; }
        public string Narration { get; set; }
        public decimal Amount { get; set; }
        public decimal Balance { get; set; }
    }
    public class BankWithDrawal
    {
        public int ID { get; set; }
        public string AccNo { get; set; }
        public string Narration { get; set; }
        public decimal Amount { get; set; }
        public decimal Balance { get; set; }
    }
    public class BankTransfer
    {
        public int ID { get; set; }
        public string AccNo { get; set; }
        public decimal Amount { get; set; }
        public string Narration { get; set; }
        public string AccNo2 { get; set; }
        
        
    }
    public class InterBankTransfer
    {
        public int ID { get; set; }
        public string AccNo { get; set; }
        public decimal AvailableBal { get; set; }
        public string Narration { get; set; }
        public string AccNo2 { get; set; }
        public decimal TransferAmount { get; set; }


    }
}
