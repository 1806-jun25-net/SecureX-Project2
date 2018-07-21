using System;
using System.Collections.Generic;
using System.Text;

namespace SecureXLibrary
{
    class Account
    {
        public Account(decimal funds, int currentDebt, char accountType)
        {
            Funds = funds;
            CurrentDebt = currentDebt;
            AccountType = accountType;
        }

        private decimal Funds { get; set; }
        private int CurrentDebt { get; set; }
        private char AccountType { get; set; }

    }
}
