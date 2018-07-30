using System;
using System.Collections.Generic;
using System.Text;
using FluentAssertions;
using Moq;
using SecureXLibrary;
using SecureXWebApi.Controllers;
using System.Threading.Tasks;
using Xunit;

namespace SecureXTest
{
    public class AccountControllerTest
    {

        [Fact]
        [CustomAssertion]
        public async void ShouldGetAccountByIDAsync()
        {

            Mock<ISecureXRepository> MoqRepo = new Mock<ISecureXRepository>();
            Account acc = new Account()
            {
                Id = 33,
                Funds = 100m,
                AccountType = "S"
            };

            MoqRepo.Setup(x => x.GetAccountById(acc.Id)).ReturnsAsync(acc);
            var con = new AccountController(MoqRepo.Object);
            var result = await con.GetById(acc.Id);

            result.Value.Should().BeEquivalentTo(acc);
        }


        [Fact]
        [CustomAssertion]
        public async void ShouldGetAllAccounts()
        {

            Mock<ISecureXRepository> MoqRepo = new Mock<ISecureXRepository>();
            List<Account> TestList = new List<Account>();

            Account acc1 = new Account()
            {
                Id = 323,
                Funds = 100m,
                AccountType = "S"
            };

            Account acc2 = new Account()
            {
                Id = 324,
                Funds = 1020m,
                AccountType = "C"
            };

            TestList.Add(acc1);
            TestList.Add(acc2);

            MoqRepo.Setup(x => x.GetAccounts()).ReturnsAsync(TestList);
            var con = new AccountController(MoqRepo.Object);
            var result = await con.GetAll();

            result.Should().BeEquivalentTo(TestList);
        }

    } 
}

