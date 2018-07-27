using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SecureXLibrary
{
    public class Mapper
    {
        public static Account Map(SecureXContext.Account account) => new Account
        {
            Id = account.Id,
            AccountType = account.AccountType,
            Funds = account.Funds
        };

        public static SecureXContext.Account Map(Account account) => new SecureXContext.Account
        {
            Id = account.Id,
            AccountType = account.AccountType,
            Funds = account.Funds
        };

        public static Bank Map(SecureXContext.Bank bank) => new Bank
        {
            Id = bank.Id,
            Reserves = bank.Reserves,
            City = bank.City
        };

        public static SecureXContext.Bank Map(Bank bank) => new SecureXContext.Bank
        {
            Id = bank.Id,
            Reserves = bank.Reserves,
            City = bank.City
        };

        public static CreditCard Map(SecureXContext.CreditCard creditcard) => new CreditCard
        {
            Id = creditcard.Id,
            CurrentDebt = creditcard.CurrentDebt,
            CreditCardNumber = creditcard.CreditCardNumber,
            CustomerId = creditcard.CustomerId
        };

        public static SecureXContext.CreditCard Map(CreditCard creditcard) => new SecureXContext.CreditCard
        {
            Id = creditcard.Id,
            CurrentDebt = creditcard.CurrentDebt,
            CreditCardNumber = creditcard.CreditCardNumber,
            CustomerId = creditcard.CustomerId
        };

        public static Customer Map(SecureXContext.Customer customer) => new Customer
        {
            Id = customer.Id,
            Address = customer.Address,
            PhoneNumber = customer.PhoneNumber,
            City = customer.City
        };

        public static SecureXContext.Customer Map(Customer customer) => new SecureXContext.Customer
        {
            Id = customer.Id,
            Address = customer.Address,
            PhoneNumber = customer.PhoneNumber,
            City = customer.City
        };

        public static Employee Map(SecureXContext.Employee employee) => new Employee
        {
            Id = employee.Id,
            BankId = employee.BankId
        };

        public static SecureXContext.Employee Map(Employee employee) => new SecureXContext.Employee
        {
            Id = employee.Id,
            BankId = employee.BankId
        };

        public static Transaction Map(SecureXContext.Transaction transaction) => new Transaction
        {
            Id = transaction.Id,
            AccountId = transaction.AccountId,
            Recipient = transaction.Recipient,
            DateOfTransaction = transaction.DateOfTransaction,
            TransactionAmount = transaction.TransactionAmount
        };

        public static SecureXContext.Transaction Map(Transaction transaction) => new SecureXContext.Transaction
        {
            Id = transaction.Id,
            AccountId = transaction.AccountId,
            Recipient = transaction.Recipient,
            DateOfTransaction = transaction.DateOfTransaction,
            TransactionAmount = transaction.TransactionAmount
        };

        public static User Map(SecureXContext.User user) => new User
        {
            Id = user.Id,
            UserName = user.UserName,
            Password = user.Password,
            FirstName = user.FirstName,
            LastName = user.LastName,
            CustomerId = user.CustomerId,
            EmployeeId = user.EmployeeId
        };

        public static SecureXContext.User Map(User user) => new SecureXContext.User
        {
            Id = user.Id,
            UserName = user.UserName,
            Password = user.Password,
            FirstName = user.FirstName,
            LastName = user.LastName,
            CustomerId = user.CustomerId,
            EmployeeId = user.EmployeeId
        };

        public static IEnumerable<Account> Map(IEnumerable<SecureXContext.Account> account) => account.Select(Map);
        public static IEnumerable<SecureXContext.Account> Map(IEnumerable<Account> account) => account.Select(Map);

        public static IEnumerable<Bank> Map(IEnumerable<SecureXContext.Bank> bank) => bank.Select(Map);
        public static IEnumerable<SecureXContext.Bank> Map(IEnumerable<Bank> bank) => bank.Select(Map);

        public static IEnumerable<CreditCard> Map(IEnumerable<SecureXContext.CreditCard> creditcard) => creditcard.Select(Map);
        public static IEnumerable<SecureXContext.CreditCard> Map(IEnumerable<CreditCard> creditcard) => creditcard.Select(Map);

        public static IEnumerable<Customer> Map(IEnumerable<SecureXContext.Customer> customer) => customer.Select(Map);
        public static IEnumerable<SecureXContext.Customer> Map(IEnumerable<Customer> customer) => customer.Select(Map);

        public static IEnumerable<Employee> Map(IEnumerable<SecureXContext.Employee> employee) => employee.Select(Map);
        public static IEnumerable<SecureXContext.Employee> Map(IEnumerable<Employee> employee) => employee.Select(Map);

        public static IEnumerable<Transaction> Map(IEnumerable<SecureXContext.Transaction> transaction) => transaction.Select(Map);
        public static IEnumerable<SecureXContext.Transaction> Map(IEnumerable<Transaction> transaction) => transaction.Select(Map);

        public static IEnumerable<User> Map(IEnumerable<SecureXContext.User> user) => user.Select(Map);
        public static IEnumerable<SecureXContext.User> Map(IEnumerable<User> user) => user.Select(Map);

    }
}
