using FluentAssertions;
using Moq;
using SecureXLibrary;
using SecureXWebApi.Controllers;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace SecureXTest
{

    /*  public int Id { get; set; }
        public decimal Reserves { get; set; }
        public string City { get; set; }
        private readonly decimal MinimumReserves = 1500000.00m;*/

    public class BankControllerTest {

    [Fact]
    [CustomAssertion]
    public async void ShouldGetByID_Bank()
    {

        Mock<ISecureXRepository> MoqRepo = new Mock<ISecureXRepository>();
        Bank bank1 = new Bank()
        {
            Id = 609,
            Reserves = 12100m,
            City = "Sydney"
        };

        MoqRepo.Setup(x => x.GetBankById(bank1.Id)).ReturnsAsync(bank1);
        var con = new BankController(MoqRepo.Object);
        var result = await con.GetById(bank1.Id);

        result.Value.Should().BeEquivalentTo(bank1);
    }


    [Fact]
    [CustomAssertion]
    public async void ShouldGetAll_Bank()
    {

        Mock<ISecureXRepository> MoqRepo = new Mock<ISecureXRepository>();
        List<Bank> TestList = new List<Bank>();

         Bank bank1 = new Bank()
        {
            Id = 609,
            Reserves = 12100m,
            City = "Sydney"
        };

          Bank bank2 = new Bank()
        {
            Id = 610,
            Reserves = 52100m,
            City = "Bangkok"
        };

        TestList.Add(bank1);
        TestList.Add(bank2);

        MoqRepo.Setup(x => x.GetBanks()).ReturnsAsync(TestList);
        var con = new BankController(MoqRepo.Object);
        var result = await con.GetAll();

        result.Should().BeEquivalentTo(TestList);
    }


        //[Fact]
        //[CustomAssertion]
        //public async void ShouldUpdate_Bank()
        //{

        //    Mock<ISecureXRepository> MoqRepo = new Mock<ISecureXRepository>();

        //    Bank bank1 = new Bank()
        //    {
        //        Id = 609,
        //        Reserves = 12100m,
        //        City = "Sydney"
        //    };

        //    MoqRepo.Setup(x => x.AddBank(bank1));
        //    MoqRepo.Setup(x => x.Save());
        //    MoqRepo.Setup(x => x.UpdateBank(bank1));
        //    var con = new BankController(MoqRepo.Object);
        //    var result = await con.Update(bank1);
        //    result.Should().BeEquivalentTo(bank1);
        //}
    } 
}

