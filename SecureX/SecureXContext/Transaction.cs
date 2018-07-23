using System;
using System.Collections.Generic;

namespace SecureXContext
{
    public partial class Transaction
    {
        public int Id { get; set; }
        public int AccountId { get; set; }
        public string Recipient { get; set; }
        public DateTime DateOfTransaction { get; set; }
        public decimal TransactionAmount { get; set; }

        public Account Account { get; set; }
    }
}
