using System;
using System.Collections.Generic;
using System.Text;

namespace SecureXLibrary
{
   

    public class CreditCard
    {
        public CreditCard()
        {

        }

        public CreditCard(decimal creditLimit, decimal currentDebt, int creditCardNumber)
        {
            CreditLimit = creditLimit;
            CurrentDebt = currentDebt;
            CreditCardNumber = creditCardNumber;
        }

        public int Id { get; set; }
        public decimal CreditLimit { get; set; }
        public decimal CurrentDebt { get; set; }
        public int CreditCardNumber { get; set; }
        public int CustomerId { get; set; }


        //ELA
        public CreditCard CalculateCardTransaction(Transaction Transaction, CreditCard CreditCard)
        {
            var ta = Transaction.TransactionAmount;

            if (ta == 0)
            {
                Console.WriteLine("Transaction was zero in 'CalculateDebt'");
                return CreditCard;
            }

            if (ta > 0)
            {
                if (CurrentDebt > 0)
                {

                    if (CurrentDebt + ta > 0)
                    {
                        CreditLimit += (ta - CurrentDebt);
                        CurrentDebt = 0;
                    }


                }

                else if (CurrentDebt == 0)
                {
                    CreditLimit += ta;
                }


            }

            else if (ta < 0)
            {
                if (CreditLimit - ta < 0)
                {
                    Console.WriteLine("Credit limit is negative, invalid transaction amount -> CalculateCardTransaction.");
                    return CreditCard;
                }

                else
                {
                    CreditLimit -= Math.Abs(ta);
                    CurrentDebt += Math.Abs(ta);

                }


            }


            return CreditCard;

        }
    }



}
