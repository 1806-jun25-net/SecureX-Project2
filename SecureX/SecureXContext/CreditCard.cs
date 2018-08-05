using System;
using System.Collections.Generic;

namespace SecureXContext
{
    public partial class CreditCard
    {
        public int Id { get; set; }
        public decimal CreditLine { get; set; }
        public decimal CreditLimit { get; set; }
        public decimal CurrentDebt { get; set; }
        public long CreditCardNumber { get; set; }
        public int CustomerId { get; set; }
        public string Status { get; set; }

        public Customer Customer { get; set; }
    }
}
