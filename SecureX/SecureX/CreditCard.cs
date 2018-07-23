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
    }
}
