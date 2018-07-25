using SecureXContext;
using System;
using System.Collections.Generic;
using System.Text;

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

        public void AddUser()
        {

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
        public CreditCard CalculateCardTransaction(Transaction Transaction, CreditCard CreditCard)
        {

            return CreditCard.CalculateCardTransaction(Transaction, CreditCard);
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
    }
}
