using Microsoft.EntityFrameworkCore;
using SecureXContext;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SecureXLibrary
{
    public class SecureXRepository
    {
        private readonly SecureXdbContext _db;

        public SecureXRepository(SecureXdbContext db)
        {
            _db = db ?? throw new ArgumentNullException(nameof(db));
        }

        public SecureXRepository()
        {
        }

        //ELA async
        public async void AddMoney(decimal deposit, Account account)
        {
            account.Funds += deposit;
            _db.Entry(await _db.Account.FindAsync(account.Id)).CurrentValues.SetValues(Mapper.Map(account));
            await _db.SaveChangesAsync();
        }

        public async void AuthorizeNewLocation()
        {
            //await
        }

        public Transaction AutoPayBills(DateTime date, Transaction Transaction, Account Account )
        {
            DateTime now = DateTime.Now;

            if (date.Month == now.Month && date.Day == date.Day)
            {
                Account.Funds -= Transaction.TransactionAmount;
                return Transaction;
            }

            return null;

        }

        //ELA async not necessary
        public CreditCard CalculateDebt(Transaction Transaction, CreditCard CreditCard)
        {

            return CreditCard.CalculateCardTransaction(Transaction, CreditCard);

        }

        //ELA async not necessary
        public decimal CalculateInterest(Account account)
        {
            return account.CalculateInterest();
        }

        public void ChangeUserLocation()
        {

        }

        //ELA async
        public async Task<IEnumerable<Transaction>> SortTransactionsByDate()
        {
            return Mapper.Map(await _db.Transaction.OrderByDescending(x => x.DateOfTransaction).ToListAsync());
        }

        //ELA async
        public async void TransferMoney(Transaction Transaction, Account Account1, Account Account2)
        {
            Account1.Funds -= Transaction.TransactionAmount;
            Account2.Funds += Transaction.TransactionAmount;

            _db.Entry(await _db.Account.FindAsync(Account1.Id)).CurrentValues.SetValues(Mapper.Map(Account1));
            _db.Entry(await _db.Account.FindAsync(Account2.Id)).CurrentValues.SetValues(Mapper.Map(Account2));
            await _db.SaveChangesAsync();
        }

        //ELA async
        public async void WithdrawlMoney(Transaction Transaction, Account Account)
        {
            Account.Funds -= Transaction.TransactionAmount;
            _db.Entry(await _db.Account.FindAsync(Account.Id)).CurrentValues.SetValues(Mapper.Map(Account));
            await _db.SaveChangesAsync();
        }

        //ELA async
        public async void AddMoneyToReserve(Bank Bank, decimal amount)
        {
            Bank.Reserves += amount;
            _db.Entry(await _db.Account.FindAsync(Bank.Id)).CurrentValues.SetValues(Mapper.Map(Bank));
            await _db.SaveChangesAsync();

        }

        public void ApproveUser()
        {

        }

        public void BlockUser()
        {

        }


        //Move to Bank class??? ELA
        public decimal CheckReserveAmount(Bank Bank)
        {
            return Bank.Reserves;
        }

        //ELA async not necessary
        public IEnumerable<Account> GetAccountsByUser(User User)
        {
            var accounts = _db.Account;
            List<Account> list = new List<Account>();

            foreach (var account in accounts)
            {
                if (User.Id == account.Id)
                {
                    list.Add(Mapper.Map(account));
                }
            }

            return list;
        }

        //ELA async 
        public async Task<Account> GetAccountInformation(int id)
        {
            return Mapper.Map(await _db.Account.FirstOrDefaultAsync(x => x.Id == id));
        }

        //ELA, async
        public async Task<CreditCard> GetCreditInformation(int id)
        {
            return Mapper.Map(await _db.CreditCard.FirstOrDefaultAsync(x => x.Id == id));
        }

        //ELA async not necessary
        public IEnumerable<Transaction> GetTransactionsByUser(User User)
        {
            var Transactions = _db.Transaction;
            List<Transaction> list = new List<Transaction>();

            foreach (var transaction in Transactions)
            {
                if (User.Id == transaction.Id)
                {
                    list.Add(Mapper.Map(transaction));
                }
            }

            return list;
        }

        //TODO: ELA ///////////////////////////////////////////////////////////////////////
        public void LoginUsers(User User)
        {
            if (User.EmployeeId != null)
            {
                //return employee?
            }
        }

        //ELA async
        public async Task<IEnumerable<Account>> GetAccounts()
        {
            return Mapper.Map(await _db.Account.ToListAsync());
        }

        //ELA async
        public async Task<Account> GetAccountById(int id)
        {
            return  Mapper.Map(await _db.Account.FirstOrDefaultAsync(x => x.Id == id));
        }

        //ELA async
        public async void AddAccount(Account account)
        {
            await _db.AddAsync(Mapper.Map(account));
            await _db.SaveChangesAsync();
        }

        //ELA async
        public async void DeleteAccount(int accountId)
        {
            _db.Remove(await _db.Account.FindAsync(accountId));
            await _db.SaveChangesAsync();
        }

        //ELA async
        public async void UpdateAccount(Account account)
        {
            _db.Entry(await _db.Account.FindAsync(account.Id)).CurrentValues.SetValues(Mapper.Map(account));
            await _db.SaveChangesAsync();
        }

        //ELA async
        public async Task<IEnumerable<Bank>> GetBanks()
        {
            return Mapper.Map(await _db.Bank.ToListAsync());
        }

        //ELA async
        public async Task<Bank> GetBankById(int id)
        {
            return Mapper.Map(await _db.Bank.FirstOrDefaultAsync(x => x.Id == id));
        }

        //ELA async
        public async void AddBank(Bank bank)
        {
            await _db.AddAsync(Mapper.Map(bank));
            await _db.SaveChangesAsync();
        }

        //ELA Removed 'DeleteBank' 
        //Reasoning: a bank should not need to ever be deleted

        //ELA async
        public async void UpdateBank(Bank bank)
        {
            _db.Entry(_db.Bank.Find(bank.Id)).CurrentValues.SetValues(Mapper.Map(bank));
            await _db.SaveChangesAsync();
        }

        //ELA async
        public async Task<IEnumerable<CreditCard>> GetCreditCards()
        {
            return Mapper.Map(await _db.CreditCard.ToListAsync());
        }


        //ELA async
        public async Task<CreditCard> GetCreditCardById(int id)
        {
            return Mapper.Map(await _db.CreditCard.FirstOrDefaultAsync(x => x.Id == id));
        }

        //ELA async
        public async void AddCreditCard(CreditCard creditCard)
        {
            await _db.AddAsync(Mapper.Map(creditCard));
            await _db.SaveChangesAsync();
        }

        //ELA async
        public async void DeleteCreditCard(int creditCardId)
        {
            _db.Remove(_db.CreditCard.Find(creditCardId));
            await _db.SaveChangesAsync();
        }

        //ELA async
        public async void UpdateCreditCard(CreditCard creditCard)
        {
            _db.Entry(_db.CreditCard.Find(creditCard.Id)).CurrentValues.SetValues(Mapper.Map(creditCard));
            await _db.SaveChangesAsync();
        }

        //ELA async
        public async Task<IEnumerable<Customer>> GetCustomers()
        {
            return Mapper.Map(await _db.Customer.ToListAsync());
        }

        //ELA async
        public async Task<Customer> GetCustomerById(int id)
        {
            return Mapper.Map(await _db.Customer.FirstOrDefaultAsync(x => x.Id == id));
        }

        //ELA async
        public async void AddCustomer(Customer customer)
        {
            await _db.AddAsync(Mapper.Map(customer));
            await _db.SaveChangesAsync();
        }

        //ELA async
        public async void DeleteCustomer(int customerId)
        {
            _db.Remove(_db.Customer.Find(customerId));
            await _db.SaveChangesAsync();
        }

        //ELA async
        public async void UpdateCustomer(Customer customer)
        {
            _db.Entry(_db.Customer.Find(customer.Id)).CurrentValues.SetValues(Mapper.Map(customer));
            await _db.SaveChangesAsync();
        }

        //ELA async
        public async Task<IEnumerable<Employee>> GetEmployees()
        {
            return Mapper.Map(await _db.Employee.ToListAsync());
        }

        //ELA async
        public async Task<Employee> GetEmployeeById(int id)
        {
            return Mapper.Map(await _db.Employee.FirstOrDefaultAsync(x => x.Id == id));
        }

        //ELA async
        public async void AddEmployee(Employee employee)
        {
            await _db.AddAsync(Mapper.Map(employee));
            await _db.SaveChangesAsync();
        }

        //ELA async
        public async void DeleteEmployee(int employeeId)
        {
            _db.Remove(_db.Employee.FindAsync(employeeId));
            await _db.SaveChangesAsync();
        }

        //ELA async
        public async void UpdateEmployee(Employee employee)
        {
            _db.Entry(_db.Employee.FindAsync(employee.Id)).CurrentValues.SetValues(Mapper.Map(employee));
            await _db.SaveChangesAsync();
        }

        //ELA async
        public async Task<IEnumerable<Transaction>> GetTransactions()
        {
            return Mapper.Map(await _db.Transaction.ToListAsync());
        }

        //ELA async
        public async Task<Transaction> GetTransactionById(int id)
        {
            return Mapper.Map(await _db.Transaction.FirstOrDefaultAsync(x => x.Id == id));
        }

        //ELA async
        public async void AddTransaction(Transaction transaction)
        {
           await _db.AddAsync(Mapper.Map(transaction));
           await _db.SaveChangesAsync();
        }

        //ELA async
        public async void DeleteTransaction(int transactionId)
        {
            _db.Remove(_db.Transaction.Find(transactionId));
            await _db.SaveChangesAsync();
        }

        //ELA
        //Removed UpdateTransaction
        //Reasoning: transactions should not be mutable for security purposes
        
        //ELA async
        public async Task<IEnumerable<User>> GetUsers()
        {
            return Mapper.Map(await _db.User.ToListAsync());
        }

        //ELA async
        public async Task<User> GetUserById(int id)
        {
            return Mapper.Map(await _db.User.FirstOrDefaultAsync(x => x.Id == id));
        }

        //ELA async
        public async void AddUser(User user)
        {
           await _db.AddAsync(Mapper.Map(user));
           await _db.SaveChangesAsync();
        }

        //ELA async
        public async void DeleteUser(int userId)
        {
            _db.Remove(_db.User.Find(userId));
            await _db.SaveChangesAsync();
        }

        //ELA async
        public async void UpdateUser(User user)
        {
             _db.Entry(_db.User.Find(user.Id)).CurrentValues.SetValues(Mapper.Map(user));
             await _db.SaveChangesAsync();
        }

        //ELA async
        public async void Save()
        {
            await _db.SaveChangesAsync();
        }
    }
}
