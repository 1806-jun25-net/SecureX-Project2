using System;
using System.Collections.Generic;

namespace SecureXContext
{
    public partial class User
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int? CustomerId { get; set; }
        public int? EmployeeId { get; set; }

        public Employee Id1 { get; set; }
        public Customer IdNavigation { get; set; }
    }
}
