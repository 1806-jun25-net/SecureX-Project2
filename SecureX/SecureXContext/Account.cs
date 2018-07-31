using System;
using System.Collections.Generic;

namespace SecureXContext
{
    public partial class Account
    {
        public Account()
        {
            Transaction = new HashSet<Transaction>();
        }

        public int Id { get; set; }
        public string AccountType { get; set; }
        public decimal Funds { get; set; }

        public ICollection<Transaction> Transaction { get; set; }
    }
}
