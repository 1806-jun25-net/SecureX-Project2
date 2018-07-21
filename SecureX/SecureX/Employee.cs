using System;
using System.Collections.Generic;
using System.Text;

namespace SecureXLibrary
{
    class Employee
    {
        public Employee(string firstName, string lastName, string password)
        {
            FirstName = firstName;
            LastName = lastName;
            Password = password;
        }

        private string FirstName { get; set; }
        private string LastName { get; set; }
        private string Password { get; set; }
    }
}
