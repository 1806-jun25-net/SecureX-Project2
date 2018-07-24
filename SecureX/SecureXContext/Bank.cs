using System;
using System.Collections.Generic;

namespace SecureXContext
{
    public partial class Bank
    {
        public int Id { get; set; }
        public decimal Reserves { get; set; }
        public string City { get; set; }

        public Employee Employee { get; set; }
    }
}
