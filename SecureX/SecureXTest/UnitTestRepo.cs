using System;
using Xunit;
using FluentAssertions;
using SecureXLibrary;
using Microsoft.EntityFrameworkCore;

namespace SecureXTest
{
    public class UnitTestRepo
    {
        private readonly Transaction transact1pos = new Transaction("Carl", DateTime.Now, 500.00m);
        private readonly Transaction transact1neg = new Transaction("Mano", DateTime.Now, -20.00m);
        private readonly CreditCard credit1debt = new CreditCard(500m, 200.00m, 234563);
        private readonly CreditCard credit1nodebt = new CreditCard(1000.00m, 0m, 234563);

        private readonly Bank Bank1 = new Bank
        {
           Reserves = 500000m,
           City = "Reston"
        };

        private readonly Account Acc1 = new Account
        {

            Id = 1,
            AccountType = "Checking",
            Funds = 500m,
            CustomerId = 1,
            Status = "Active"

        };

        private readonly CreditCard CC1 = new CreditCard
        {

            CreditLimit = 500m,
            CurrentDebt = 500m,
            CreditCardNumber = 5,
            CustomerId = 1

        };

        private readonly Customer Cus1 = new Customer
        {
            Address = "Test",
            PhoneNumber = 10,
            City = "Test",
            UserName = "Test"
        };

        private readonly Transaction Trans1 = new Transaction
        {
            Recipient = "Test",
            DateOfTransaction = DateTime.Now,
            TransactionAmount = 500m
        };

        private readonly User User1 = new User
        {
               UserName = "Test",
               FirstName = "Test",
               LastName = "Test"
    };

        [CustomAssertion]
        [Fact]
        //ELA
        public void ShouldCalculateCreditCardCorrectly1()
        {
            var c1  = credit1debt.CalculateCardTransaction(transact1pos, credit1debt);
            var expectedc1 = new CreditCard(800.00m, 0, 234563);
            c1.Should().BeEquivalentTo(expectedc1);
        }

        [CustomAssertion]
        [Fact]
        //ELA
        public void ShouldCalculateCreditCardCorrectly2()
        {
            var c2 = credit1nodebt.CalculateCardTransaction(transact1pos, credit1nodebt);
            var expectedc2 = new CreditCard(1500.00m, 0, 234563);
            c2.Should().BeEquivalentTo(expectedc2);
        }

        [CustomAssertion]
        [Fact]
        //ELA
        public void ShouldCalculateCreditCardCorrectly3()
        {
            var c3  = credit1debt.CalculateCardTransaction(transact1neg, credit1debt);
            var expectedc3 = new CreditCard(480.00m, 220.00m, 234563);
            c3.Should().BeEquivalentTo(expectedc3);
        }

        [CustomAssertion]
        [Fact]
        //ELA
        public void ShouldCalculateCreditCardCorrectly4()
        {
            var c4 = credit1nodebt.CalculateCardTransaction(transact1neg, credit1nodebt);
            var expectedc4 = new CreditCard(980.00m, 20.00m, 234563);
            c4.Should().BeEquivalentTo(expectedc4);
        }

        [Fact]
        public async void ShouldAddAccount()
        {
            var options = new DbContextOptionsBuilder<SecureXContext.SecureXdbContext>()
                 .UseInMemoryDatabase(databaseName: "testXdb")
                 .Options;


            using (var context = new SecureXContext.SecureXdbContext(options))
            {
                var service = new SecureXRepository(context);

                await service.AddAccount(Acc1);
                await service.Save();
            }


            using (var context = new SecureXContext.SecureXdbContext(options))
            {
                Assert.Equal(1, context.Account.CountAsync().Result);

            }
        }

        [Fact]
        public async void ShouldAddCreditCard()
        {
            var options = new DbContextOptionsBuilder<SecureXContext.SecureXdbContext>()
                 .UseInMemoryDatabase(databaseName: "testXdb")
                 .Options;


            using (var context = new SecureXContext.SecureXdbContext(options))
            {
                var service = new SecureXRepository(context);

                await service.AddCreditCard(CC1);
                await service.Save();
            }


            using (var context = new SecureXContext.SecureXdbContext(options))
            {
                Assert.Equal(1, context.CreditCard.CountAsync().Result);

            }
        }

        [Fact]
        public async void ShouldAddCustomer()
        {
            var options = new DbContextOptionsBuilder<SecureXContext.SecureXdbContext>()
                 .UseInMemoryDatabase(databaseName: "testXdb")
                 .Options;


            using (var context = new SecureXContext.SecureXdbContext(options))
            {
                var service = new SecureXRepository(context);

                await service.AddCustomer(Cus1);
                await service.Save();
            }


            using (var context = new SecureXContext.SecureXdbContext(options))
            {
                Assert.Equal(1, context.Customer.CountAsync().Result);

            }
        }

        [Fact]
        public async void ShouldAddUser()
        {

            var options = new DbContextOptionsBuilder<SecureXContext.SecureXdbContext>()
           .UseInMemoryDatabase(databaseName: "testXdb")
             .Options;


            using (var context = new SecureXContext.SecureXdbContext(options))
            {
                var service = new SecureXRepository(context);

                await service.AddUser(User1);
                await service.Save();
            }


            using (var context = new SecureXContext.SecureXdbContext(options))
            {
                Assert.Equal(1, context.User.CountAsync().Result);

            }
        }

        [Fact]
        public async void ShouldAddBank()
        {

            var options = new DbContextOptionsBuilder<SecureXContext.SecureXdbContext>()
           .UseInMemoryDatabase(databaseName: "testXdb")
             .Options;


            using (var context = new SecureXContext.SecureXdbContext(options))
            {
                var service = new SecureXRepository(context);

                await service.AddBank(Bank1);
                await service.Save();
            }


            using (var context = new SecureXContext.SecureXdbContext(options))
            {
                Assert.Equal(1, context.Bank.CountAsync().Result);

            }
        }


    }
}
