using System;
using System.Collections.Generic;

namespace SecureXContext
{
    public partial class Employee
    {
        public int Id { get; set; }
        public int BankId { get; set; }

        public Bank IdNavigation { get; set; }
        public User User { get; set; }
    }
}
