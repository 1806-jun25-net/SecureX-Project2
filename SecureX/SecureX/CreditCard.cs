using System;
using System.Collections.Generic;
using System.Text;

namespace SecureXLibrary
{
    public class CreditCard
    {
        public int Id { get; set; }
        public decimal CreditLimit { get; set; }
        public decimal CurrentDebt { get; set; }
        public int CreditCardNumber { get; set; }
        public int CustomerId { get; set; }

        // methods
        public void ChargeCredit(decimal amount)
        {
            CreditLimit -= amount;
            CurrentDebt += amount;
        }

        public void PayBalance(decimal amount)
        {
            if (amount > CurrentDebt) // check if paid amount is larger than the debt balance
            {
                amount -= CurrentDebt; // deduct debt from amount
                CurrentDebt = 0.00m; // clear debt balance
                CreditLimit += amount; // add excess amount to credit limit                
            }
            else
            {
                CurrentDebt -= amount;
            }            
        }
    }
}
