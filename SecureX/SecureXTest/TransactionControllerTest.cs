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
    public class TransactionControllerTest
    {

      /*public int Id { get; set; }
        public int AccountId { get; set; }
        public string Recipient { get; set; }
        public DateTime DateOfTransaction { get; set; }
        public decimal TransactionAmount { get; set; }*/

        [Fact]
        [CustomAssertion]
        public async void ShouldGetByID_Transaction()
        {

            Mock<ISecureXRepository> MoqRepo = new Mock<ISecureXRepository>();
            Transaction trans1 = new Transaction()
            {
                Id = 303,
                DateOfTransaction = DateTime.Now,
            };

            MoqRepo.Setup(x => x.GetTransactionById(trans1.Id)).ReturnsAsync(trans1);
            var con = new TransactionController(MoqRepo.Object);
            var result = await con.GetById(trans1.Id);

            result.Value.Should().BeEquivalentTo(trans1);
        }


        //[Fact]
        //[CustomAssertion]
        //public async void ShouldGetAll_Transaction()
        //{

        //    Mock<ISecureXRepository> MoqRepo = new Mock<ISecureXRepository>();
        //    List<Transaction> NewList = new List<Transaction>();

        //    Transaction trans1 = new Transaction()
        //    {
        //        Id = 323,
        //        Funds = 100m,
        //        AccountType = "S"
        //    };

        //    Transaction acc2 = new Transaction()
        //    {
        //        Id = 324,
        //        Funds = 1020m,
        //        AccountType = "C"
        //    };

        //    NewList.Add(acc1);
        //    NewList.Add(acc2);

        //    MoqRepo.Setup(X => X.AddAccount(acc1)).Returns(Task.CompletedTask);
        //    MoqRepo.Setup(x => x.AddAccount(acc2)).Returns(Task.CompletedTask);
        //    MoqRepo.Setup(x => x.Save()).Returns(Task.CompletedTask);
        //    MoqRepo.Setup(x => x.GetAccounts()).ReturnsAsync(NewList);

        //    var con = new AccountController(MoqRepo.Object);
        //    var result = await con.GetAll();

        //    result.Should().BeEquivalentTo(NewList);
        //}

    }
}

