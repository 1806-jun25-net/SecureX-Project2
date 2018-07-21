using System;
using System.Collections.Generic;
using System.Text;

namespace SecureXLibrary
{
    class CreditCard
    {
        public CreditCard(char hasCreditCard, char accountType, int creditCardNumber)
        {
            HasCreditCard = hasCreditCard;
            AccountType = accountType;
            CreditCardNumber = creditCardNumber;
        }

        private char HasCreditCard { get; set; } = 'N';
        private char AccountType { get; set; } = 'S';
        private int CreditCardNumber { get; set; } = 0000000000000000;

    }
}
