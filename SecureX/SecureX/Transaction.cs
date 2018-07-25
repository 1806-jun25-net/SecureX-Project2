using System;
using System.Collections.Generic;
using System.Text;

namespace SecureXLibrary
{
    public class Transaction
    {

        public Transaction()
        {

        }
         
        public Transaction(string recipient, DateTime dateOfTransaction, decimal transactionAmount)
        {
            Recipient = recipient;
            DateOfTransaction = dateOfTransaction;
            TransactionAmount = transactionAmount;
        }

        public int Id { get; set; }
        public int AccountId { get; set; }
        public string Recipient { get; set; }
        public DateTime DateOfTransaction { get; set; }
        public decimal TransactionAmount { get; set; }

        public Transaction()
        {
            DateOfTransaction = DateTime.Now;
        }
    }
}
