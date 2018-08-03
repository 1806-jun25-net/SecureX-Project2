using System;
using System.Collections.Generic;

namespace SecureXContext
{
    public partial class Customer
    {
        public Customer()
        {
            CreditCard = new HashSet<CreditCard>();
        }

        public int Id { get; set; }
        public string Address { get; set; }
        public long PhoneNumber { get; set; }
        public string City { get; set; }
        public string UserName { get; set; }

        public User User { get; set; }
        public ICollection<CreditCard> CreditCard { get; set; }
    }
}
