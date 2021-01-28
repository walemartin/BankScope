using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CarScope.Models
{
    public class FutureValueModel
    {
        
        public FutureValueModel()
        {
           
        }
        public int ID { get; set; }
        

        public string ContractNo { get; set; }
        public decimal MonthlyInvestment { get; set; }
        public decimal YearlyInterestRate { get; set; }
        public int Years { get; set; }
        public decimal CalculateFutureValue()
        {
            int months = Years * 12;
            decimal monthlyInterestRate = YearlyInterestRate / 12 / 100;
            decimal futureValue = 0;
            for (int i = 0; i < months; i++)
            {
                futureValue = (futureValue + MonthlyInvestment) * (1 + monthlyInterestRate);
            }
            return futureValue;
        }
    }
    public class FutureInv
    {
        public int ID { get; set; }
        public string ContractNo { get; set; }
        public decimal Investment { get; set; }
    }
}
