using SecureXLibrary;
using System;

namespace SecureX
{
    public class Customer
    {
        public Customer(int phoneNumber, string firstName, string lastName, string password, decimal creditLimit, decimal creditScore)
        {
            PhoneNumber = phoneNumber;
            FirstName = firstName;
            LastName = lastName;
            Password = password;
            CreditLimit = creditLimit;
            CreditScore = creditScore;
        }

        private int PhoneNumber { get; set; }
        private string FirstName { get; set; }
        private string LastName { get; set; }
        private string Password { get; set; }
        private decimal CreditLimit { get; set; }
        private decimal CreditScore { get; set; }


    }
}
