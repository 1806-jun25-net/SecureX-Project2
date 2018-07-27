using System;
using System.Collections.Generic;
using System.Text;

namespace SecureXLibrary
{
    public class Bank
    {
        public int Id { get; set; }
        public decimal Reserves { get; set; }
        public string City { get; set; }

        private readonly decimal MinimumReserves = 1500000.00m;

        // methods
        public bool NotOverdraw(decimal amount)
        {
            var check = false;
            if (Reserves - amount > MinimumReserves) check = true;

            return check;
        }

        public void Withdraw(decimal amount)
        {
            Reserves -= amount;
        }

        public void Deposit(decimal amount)
        {
            Reserves += amount;
        }
    }
}
