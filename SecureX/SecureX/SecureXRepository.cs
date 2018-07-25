using SecureXContext;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SecureXLibrary
{
    class SecureXRepository
    {
        private readonly SecureXdbContext _db;

        public SecureXRepository(SecureXdbContext db)
        {
            _db = db ?? throw new ArgumentNullException(nameof(db));
        }

        //ELA
        //Controller
        public void AddMoney(decimal deposit, int id)
        {
            var accounts = _db.Account;

            if (deposit != 0)
            {

                try
                {
                    foreach (var account in accounts)
                    {
                        if (account.Id == id)
                        {
                            account.Funds += deposit;
                            _db.SaveChanges();
                        }
                    }
                }

                catch (Exception e)
                {
                    e.ToString();
                }

            }

            else

            {

            Console.WriteLine("Deposit value was zero. No changes were made.");
                    
            }

        }

        public void AuthorizeNewLocation()
        {

        }

        public void AutoPayBills()
        {

        }

        //ELA
        public CreditCard CalculateDebt(Transaction Transaction, CreditCard CreditCard)
        {
            
            CreditCard.CurrentDebt += Transaction.TransactionAmount;
            return CreditCard;

        }

        public void CalculateInterest()
        {

        }

        public void ChangeUserLocation()
        {

        }

        public IEnumerable<Transaction> SortTransactionsByDate()
        {
            return null;
        }

        public void TransferMoney()
        {

        }

        public void WithdrawlMoney()
        {

        }



        //Repo
        public void AddMoneyToReserve()
        {

        }

        public void ApproveUser()
        {

        }

        public void BlockUser()
        {

        }

        //ELA
        //Business logic in repo? 
        public CreditCard CalculateCreditLeft(Transaction Transaction, CreditCard CreditCard)
        {

            CreditCard.CreditLimit += Transaction.TransactionAmount;
            return CreditCard;

        }

        public void CheckReserveAmount()
        {

        }

        //ELA
        public IEnumerable<Account> GetAccountsByUser(User User)
        {

            var accounts = _db.Account;
            List<Account> UserAccounts = new List<Account>();

            try
            {
                foreach(var account in accounts)
                {

                    if (User.Id == account.Id)
                    {
                        UserAccounts.Add(Mapper.Map(account));
                    }

                }
                
            }

            catch(Exception e)
            {
                e.ToString();
            }

            if (UserAccounts.Count == 0)
            {
                Console.WriteLine("Returned a null List<Account> -> 'GetAccountsByUser'");
            }

            return UserAccounts;
        }

        //ELA
        public Account GetAccountInformation(int id)
        {

            var accounts = _db.Account;
            try
            {

                foreach (var account in accounts)
                {
                    if (account.Id == id)
                    {

                        return Mapper.Map(account);
                    }
                }

            

            }

            catch (Exception e)
            {
                e.ToString();
            }

            Console.WriteLine("Returned null -> 'GetAccountInformation'");
            return null;


        }

        //ELA
        public CreditCard GetCreditInformation(int id)
        {
            var creditcards = _db.CreditCard;

            try
            {

                foreach (var creditcard in creditcards)
                {

                    if (creditcard.Id == id)
                    {
                        return Mapper.Map(creditcard);
                    }
                }

            }

            catch(Exception e)
            {
                e.ToString();
            }

            Console.WriteLine("Returned null -> 'GetCreditInformation'");
            return null;

        }

        //ELA
        public IEnumerable<Transaction> GetTransactionsByUser(User User)
        {

            var transactions = _db.Transaction;
            List<Transaction> UserTransactions = new List<Transaction>();

            try
            {
                foreach (var transaction in transactions)
                {

                    if (User.Id == transaction.Id)
                    {
                        UserTransactions.Add(Mapper.Map(transaction));
                    }

                }

            }

            catch (Exception e)
            {
                e.ToString();
            }

            if (UserTransactions.Count == 0)
            {
                Console.WriteLine("Returned a null List<Transaction> -> 'GetTransactionsByUser'");
            }

            return UserTransactions;
        }

        //TODO: ELA
        public void LoginUsers(User User)
        {
            if (User.EmployeeId != null)
            {
                //return employee?
            }



        }

        // Account
        public IEnumerable<Account> GetAccounts()
        {
            return Mapper.Map(_db.Account);
        }

        public Account GetAccountById(int id)
        {
            return Mapper.Map(_db.Account.First(x => x.Id == id));
        }

        public void AddAccount(Account account)
        {
            _db.Add(Mapper.Map(account));
        }

        public void DeleteAccount(int accountId)
        {
            _db.Remove(_db.Account.Find(accountId));
        }

        public void UpdateAccount(Account account)
        {
            _db.Entry(_db.Account.Find(account.Id)).CurrentValues.SetValues(Mapper.Map(account));
        }

        // Bank
        public IEnumerable<Bank> GetBanks()
        {
            return Mapper.Map(_db.Bank);
        }

        public Bank GetBankById(int id)
        {
            return Mapper.Map(_db.Bank.First(x => x.Id == id));
        }

        public void AddBank(Bank bank)
        {
            _db.Add(Mapper.Map(bank));
        }

        public void DeleteBank(int bankId)
        {
            _db.Remove(_db.Bank.Find(bankId));
        }

        public void UpdateBank(Bank bank)
        {
            _db.Entry(_db.Bank.Find(bank.Id)).CurrentValues.SetValues(Mapper.Map(bank));
        }

        // CreditCard
        public IEnumerable<CreditCard> GetCreditCards()
        {
            return Mapper.Map(_db.CreditCard);
        }

        public CreditCard GetCreditCardById(int id)
        {
            return Mapper.Map(_db.CreditCard.First(x => x.Id == id));
        }

        public void AddCreditCard(CreditCard creditCard)
        {
            _db.Add(Mapper.Map(creditCard));
        }

        public void DeleteCreditCard(int creditCardId)
        {
            _db.Remove(_db.CreditCard.Find(creditCardId));
        }

        public void UpdateCreditCard(CreditCard creditCard)
        {
            _db.Entry(_db.CreditCard.Find(creditCard.Id)).CurrentValues.SetValues(Mapper.Map(creditCard));
        }

        // Customer
        public IEnumerable<Customer> GetCustomers()
        {
            return Mapper.Map(_db.Customer);
        }

        public Customer GetCustomerById(int id)
        {
            return Mapper.Map(_db.Customer.First(x => x.Id == id));
        }

        public void AddCustomer(Customer customer)
        {
            _db.Add(Mapper.Map(customer));
        }

        public void DeleteCustomer(int customerId)
        {
            _db.Remove(_db.Customer.Find(customerId));
        }

        public void UpdateCustomer(Customer customer)
        {
            _db.Entry(_db.Customer.Find(customer.Id)).CurrentValues.SetValues(Mapper.Map(customer));
        }

        // Employee
        public IEnumerable<Employee> GetEmployees()
        {
            return Mapper.Map(_db.Employee);
        }

        public Employee GetEmployeeById(int id)
        {
            return Mapper.Map(_db.Employee.First(x => x.Id == id));
        }

        public void AddEmployee(Employee employee)
        {
            _db.Add(Mapper.Map(employee));
        }

        public void DeleteEmployee(int employeeId)
        {
            _db.Remove(_db.Employee.Find(employeeId));
        }

        public void UpdateEmployee(Employee employee)
        {
            _db.Entry(_db.Employee.Find(employee.Id)).CurrentValues.SetValues(Mapper.Map(employee));
        }

        // Transaction
        public IEnumerable<Transaction> GetTransactions()
        {
            return Mapper.Map(_db.Transaction);
        }

        public Transaction GetTransactionById(int id)
        {
            return Mapper.Map(_db.Transaction.First(x => x.Id == id));
        }

        public void AddTransaction(Transaction transaction)
        {
            _db.Add(Mapper.Map(transaction));
        }

        public void DeleteTransaction(int transactionId)
        {
            _db.Remove(_db.Transaction.Find(transactionId));
        }

        public void UpdateTransaction(Transaction transaction)
        {
            _db.Entry(_db.Transaction.Find(transaction.Id)).CurrentValues.SetValues(Mapper.Map(transaction));
        }

        // User
        public IEnumerable<User> GetUsers()
        {
            return Mapper.Map(_db.User);
        }

        public User GetUserById(int id)
        {
            return Mapper.Map(_db.User.First(x => x.Id == id));
        }

        public void AddUser(User user)
        {
            _db.Add(Mapper.Map(user));
        }

        public void DeleteUser(int userId)
        {
            _db.Remove(_db.User.Find(userId));
        }

        public void UpdateUser(User user)
        {
            _db.Entry(_db.User.Find(user.Id)).CurrentValues.SetValues(Mapper.Map(user));
        }

        // Save
        public void Save()
        {
            _db.SaveChanges();
        }
    }
}
