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
        public async void ShouldGetAccountGetByIDAsync()
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

    } 
}

