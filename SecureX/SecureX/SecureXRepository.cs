using SecureXContext;
using System;
using System.Collections.Generic;
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

        //Controller
        public void AddMoney()
        {

        }

        public void AuthorizeNewLocation()
        {

        }

        public void AutoPayBills()
        {

        }

        public void CalculateDebt()
        {

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

        public void CalculateCreditLeft()
        {

        }

        public void CheckReserveAmount()
        {

        }

        public IEnumerable<Account> GetAccountsByUser()
        {
            return null;
        }

        public void GetAccountInformation()
        {

        }

        public void GetCreditInformation()
        {

        }

        public void GetTransactionByUser()
        {

        }

        public void LoginUsers()
        {

        }
    }
}
