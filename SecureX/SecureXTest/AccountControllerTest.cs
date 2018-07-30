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
        public async void ShouldGetByID_Account()
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
        public async void ShouldGetAll_Account()
        {

            Mock<ISecureXRepository> MoqRepo = new Mock<ISecureXRepository>();
            List<Account> NewList = new List<Account>();

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

            MoqRepo.Setup(X => X.AddAccount(acc1));
            MoqRepo.Setup(x => x.AddAccount(acc2));
            MoqRepo.Setup(x => x.Save());
            MoqRepo.Setup(x => x.GetAccounts()).ReturnsAsync(NewList);
            var con = new AccountController(MoqRepo.Object);
            var result = await con.GetAll();

            result.Should().BeEquivalentTo(NewList);
        }



        //[Fact]
        //[CustomAssertion]
        //public async void ShouldDelete_Account()
        //{

        //    Mock<ISecureXRepository> MoqRepo = new Mock<ISecureXRepository>();

        //    Account acc1 = new Account()
        //    {
        //        Id = 323,
        //        Funds = 100m,
        //        AccountType = "S"
        //    };

        //    MoqRepo.Setup(x => x.AddAccount(acc1));
        //    MoqRepo.Setup(x => x.Save());
        //    MoqRepo.Setup(x => x.DeleteAccount(acc1.Id));
        //    MoqRepo.Setup(x => x.Save());

        //    var con = new AccountController(MoqRepo.Object);

        //    var result = await con.GetById(acc1.Id);

        //    result.Value.Should().Be(null);
        //}

    } 
}

