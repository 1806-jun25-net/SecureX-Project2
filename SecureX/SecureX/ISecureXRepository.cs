using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SecureXLibrary
{
    public interface ISecureXRepository
    {
        Task AddAccount(Account account);
        Task AddBank(Bank bank);
        Task AddCreditCard(CreditCard creditCard);
        Task AddCustomer(Customer customer);
        Task AddEmployee(Employee employee);
        Task<Account> AddMoney(decimal deposit, Account account);
        Task<Bank> AddMoneyToReserve(Bank Bank, decimal amount);
        Task AddTransaction(Transaction transaction);
        Task AddUser(User user);
        Task<Transaction> AutoPayBills(DateTime date, Transaction Transaction, Account Account);
        CreditCard CalculateDebt(Transaction Transaction, CreditCard CreditCard);
        decimal CalculateInterest(Account account);
        Task ChangeCustomerLocation(Customer Customer, Bank Bank);
        decimal CheckReserveAmount(Bank Bank);
        Task DeleteAccount(int accountId);
        Task DeleteCreditCard(int creditCardId);
        Task DeleteCustomer(int customerId);
        Task DeleteEmployee(int employeeId);
        Task DeleteTransaction(int transactionId);
        Task DeleteUser(int userId);
        Task<Account> GetAccountById(int id);
        Task<Account> GetAccountInformation(int id);
        Task<IEnumerable<Account>> GetAccounts();
        IEnumerable<Account> GetAccountsByUser(User User);
        Task<Bank> GetBankById(int id);
        Task<IEnumerable<Bank>> GetBanks();
        Task<CreditCard> GetCreditCardById(int id);
        Task<IEnumerable<CreditCard>> GetCreditCards();
        Task<CreditCard> GetCreditInformation(int id);
        Task<Customer> GetCustomerById(int id);
        Task<IEnumerable<Customer>> GetCustomers();
        Task<Employee> GetEmployeeById(int id);
        Task<IEnumerable<Employee>> GetEmployees();
        Task<Transaction> GetTransactionById(int id);
        Task<IEnumerable<Transaction>> GetTransactions();
        IEnumerable<Transaction> GetTransactionsByUser(User User);
        Task<User> GetUserById(int id);
        Task<IEnumerable<User>> GetUsers();
        void LoginUsers(User User);
        Task Save();
        Task<IEnumerable<Transaction>> SortTransactionsByDate();
        Task<Transaction> TransferMoney(Transaction Transaction, Account Account1, Account Account2);
        Task UpdateAccount(Account account);
        Task UpdateBank(Bank bank);
        Task UpdateCreditCard(CreditCard creditCard);
        Task UpdateCustomer(Customer customer);
        Task UpdateEmployee(Employee employee);
        Task UpdateUser(User user);
        Task<Transaction> WithdrawlMoney(Transaction Transaction, Account Account);
    }
}