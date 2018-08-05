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
           Id = 4000,
           Reserves = 500000m,
           City = "Reston"
        };

        private readonly Account Acc1 = new Account
        {

            Id = 500,
            AccountType = "Checking",
            Funds = 500m,
            CustomerId = 1,
            Status = "Active"

        };

        private readonly Employee Emp1 = new Employee
        {

            Id = 1234

        };

        private readonly CreditCard CC1 = new CreditCard
        {
            Id = 30,
            CreditLimit = 500m,
            CurrentDebt = 500m,
            CreditCardNumber = 5

        };

        private readonly Customer Cus1 = new Customer
        {
            Id = 10,
            Address = "Test",
            PhoneNumber = 10,
            City = "Test",
            UserName = "Test"
        };

        private readonly Transaction Trans1 = new Transaction
        {
            Id = 123,
            Recipient = "Test",
            DateOfTransaction = DateTime.Now,
            TransactionAmount = 500m
        };

        private readonly User User1 = new User
        {
            Id = 408,
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
                Assert.Equal(1, context.Account.FindAsync(Acc1.Id).Id);

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
                Assert.Equal(1, context.CreditCard.FindAsync(CC1.Id).Id);

            }
        }

        [Fact]
        public async void ShouldUpdateCreditCard()
        {

            var options = new DbContextOptionsBuilder<SecureXContext.SecureXdbContext>()
           .UseInMemoryDatabase(databaseName: "testXdb")
             .Options;

            using (var context = new SecureXContext.SecureXdbContext(options))
            {
                var service = new SecureXRepository(context);

                await service.AddCreditCard(CC1);
                await service.Save();
                CC1.CreditLimit = 111500m;
                await service.UpdateCreditCard(CC1);
                await service.Save();
            }


            using (var context = new SecureXContext.SecureXdbContext(options))
            {
                Assert.Equal(111500m, CC1.CreditLimit);
                Assert.Equal(30, CC1.Id);

            }
        }

        [Fact]
        public async void ShouldDeleteCreditCard()
        {

            var options = new DbContextOptionsBuilder<SecureXContext.SecureXdbContext>()
           .UseInMemoryDatabase(databaseName: "testXdb")
             .Options;

            using (var context = new SecureXContext.SecureXdbContext(options))
            {
                var service = new SecureXRepository(context);

                await service.AddCreditCard(CC1);
                await service.Save();
                await service.DeleteCreditCard(CC1.Id);
                await service.Save();
            }


            using (var context = new SecureXContext.SecureXdbContext(options))
            {
                Assert.Equal(0, context.CreditCard.CountAsync().Result);

            }
        }

        [Fact]
        [CustomAssertion]
        public async void ShouldGetCreditCardByID()
        {

            var options = new DbContextOptionsBuilder<SecureXContext.SecureXdbContext>()
           .UseInMemoryDatabase(databaseName: "testXdb")
             .Options;

            CreditCard CC2;

            using (var context = new SecureXContext.SecureXdbContext(options))
            {
                var service = new SecureXRepository(context);
                
                await service.AddCreditCard(CC1);
                CC2 = await service.GetCreditCardById(CC1.Id);
                await service.Save();
            }


            using (var context = new SecureXContext.SecureXdbContext(options))
            {
                CC2.Should().BeEquivalentTo(CC1);         
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
                Assert.Equal(1, context.Customer.FindAsync(Cus1.Id).Id);

            }
        }

     
        [Fact]
        public async void ShouldUpdateCustomer()
        {

            var options = new DbContextOptionsBuilder<SecureXContext.SecureXdbContext>()
           .UseInMemoryDatabase(databaseName: "testXdb")
             .Options;

            using (var context = new SecureXContext.SecureXdbContext(options))
            {
                var service = new SecureXRepository(context);

                await service.AddCustomer(Cus1);
                await service.Save();
                Cus1.PhoneNumber = 1294123;
                await service.UpdateCustomer(Cus1);
                await service.Save();
            }


            using (var context = new SecureXContext.SecureXdbContext(options))
            {
                Assert.Equal(1294123, Cus1.PhoneNumber);
                Assert.Equal(10, Cus1.Id);

            }
        }

        [Fact]
        public async void ShouldDeleteCustomer()
        {

            var options = new DbContextOptionsBuilder<SecureXContext.SecureXdbContext>()
           .UseInMemoryDatabase(databaseName: "testXdb")
             .Options;

            using (var context = new SecureXContext.SecureXdbContext(options))
            {
                var service = new SecureXRepository(context);

                await service.AddCustomer(Cus1);
                await service.Save();
                await service.DeleteCustomer(Cus1.Id);
                await service.Save();
            }


            using (var context = new SecureXContext.SecureXdbContext(options))
            {
                Assert.Equal(0, context.Customer.CountAsync().Result);

            }
        }


        [Fact]
        [CustomAssertion]
        public async void ShouldGetAccountByID()
        {

            var options = new DbContextOptionsBuilder<SecureXContext.SecureXdbContext>()
           .UseInMemoryDatabase(databaseName: "testXdb")
             .Options;

            Account Acc2;

            using (var context = new SecureXContext.SecureXdbContext(options))
            {
                var service = new SecureXRepository(context);

                await service.AddAccount(Acc1);
                Acc2 = await service.GetAccountById(Acc1.Id);
                await service.Save();
            }


            using (var context = new SecureXContext.SecureXdbContext(options))
            {
                Acc2.Should().BeEquivalentTo(Acc1);
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
                Assert.Equal(1, context.User.FindAsync(User1.Id).Id);

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
                Assert.Equal(1, context.Bank.FindAsync(Bank1.Id).Id);


            }
        }

        [Fact]
        public async void ShouldAddEmployee()
        {

            var options = new DbContextOptionsBuilder<SecureXContext.SecureXdbContext>()
           .UseInMemoryDatabase(databaseName: "testXdb")
             .Options;


            using (var context = new SecureXContext.SecureXdbContext(options))
            {
                var service = new SecureXRepository(context);

                await service.AddEmployee(Emp1);
                await service.Save();
            }


            using (var context = new SecureXContext.SecureXdbContext(options))
            {
                Assert.Equal(1, context.Employee.FindAsync(Emp1.Id).Id);


            }
        }

        [Fact]
        public async void ShouldUpdateAccount()
        {

            var options = new DbContextOptionsBuilder<SecureXContext.SecureXdbContext>()
           .UseInMemoryDatabase(databaseName: "testXdb")
             .Options;


            using (var context = new SecureXContext.SecureXdbContext(options))
            {
                var service = new SecureXRepository(context);

                await service.AddAccount(Acc1);
                await service.Save();
                Acc1.Funds = -500m;
                await service.UpdateAccount(Acc1);
                await service.Save();
            }


            using (var context = new SecureXContext.SecureXdbContext(options))
            {
                Assert.Equal(-500m, Acc1.Funds);
                Assert.Equal(500, Acc1.Id);
            }
        }

    }
}
