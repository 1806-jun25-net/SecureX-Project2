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
    public class CreditCardControllerTest
    {

    /*  public int Id { get; set; }
        public decimal CreditLimit { get; set; }
        public decimal CurrentDebt { get; set; }
        public int CreditCardNumber { get; set; }
        public int CustomerId { get; set; } */

        [Fact]
        [CustomAssertion]
        public async void ShouldGetByID_CreditCard()
        {

            Mock<ISecureXRepository> MoqRepo = new Mock<ISecureXRepository>();
            CreditCard cc1 = new CreditCard()
            {
                Id = 133,
                CreditLimit = 1000m,
                CurrentDebt = 1000m,
                CreditCardNumber = 2234923,
                CustomerId = 33
            };

            MoqRepo.Setup(x => x.GetCreditCardById(cc1.Id)).ReturnsAsync(cc1);
            var con = new CreditCardController(MoqRepo.Object);
            var result = await con.GetById(cc1.Id);

            result.Value.Should().BeEquivalentTo(cc1);
        }


        [Fact]
        [CustomAssertion]
        public async void ShouldGetAll_CreditCard()
        {

            Mock<ISecureXRepository> MoqRepo = new Mock<ISecureXRepository>();
            List<CreditCard> TestList = new List<CreditCard>();

            CreditCard cc1 = new CreditCard()
            {
                Id = 133,
                CreditLimit = 1000m,
                CurrentDebt = 1000m,
                CreditCardNumber = 2234923,
                CustomerId = 33
            };

            CreditCard cc2 = new CreditCard()
            {
                Id = 153,
                CreditLimit = 1500m,
                CurrentDebt = 1200m,
                CreditCardNumber = 1134923,
                CustomerId = 500
            };

            TestList.Add(cc1);
            TestList.Add(cc2);

            MoqRepo.Setup(x => x.GetCreditCards()).ReturnsAsync(TestList);
            var con = new CreditCardController(MoqRepo.Object);
            var result = await con.GetAll();

            result.Should().BeEquivalentTo(TestList);
        }

    }
}

