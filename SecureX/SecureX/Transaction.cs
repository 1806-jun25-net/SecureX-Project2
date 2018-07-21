using System;
using System.Collections.Generic;
using System.Text;

namespace SecureXLibrary
{
    class Transaction
    {
        public Transaction(string recipient, DateTime dateOfTransaction, decimal transactionAmount)
        {
            Recipient = recipient;
            DateOfTransaction = dateOfTransaction;
            TransactionAmount = transactionAmount;
        }

        //Add CustomerID here?
        private string Recipient { get; set; }
        private DateTime DateOfTransaction { get; set; } = DateTime.Now;
        private decimal TransactionAmount { get; set; }

    }
}
