using System;
using System.Collections.Generic;
using System.Text;

namespace SecureXLibrary
{
    public class Account
    {
        public int Id { get; set; }
        public string AccountType { get; set; }
        public decimal? Funds { get; set; }

        // methods
        public bool NotOverdraw(decimal amount)
        {
            var check = false;
            if (Funds - amount > 0) check = true;

            return check;
        }

        public void Withdraw(decimal amount)
        {
            if(NotOverdraw(amount))Funds -= amount;
        }

        public void Deposit(decimal amount)
        {
            Funds += amount;
        }


        public decimal CalculateInterest()
        {
            var interest = 0.00m;
            var APY = 0.01m; // yearly rate
            var MPY = APY / 12; // monthly rate

            if (AccountType == "S")
            {
                interest = (decimal)Funds * MPY; // calculate interest for next month
            }

            return interest;
        }
    }
}
