using System;
using System.Collections.Generic;
using System.Text;

namespace SecureXTest
{
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
        public class UserControllerTest
        {

         /* public int Id { get; set; }
            public string UserName { get; set; }
            public string Password { get; set; }
            public string FirstName { get; set; }
            public string LastName { get; set; }
            public int? CustomerId { get; set; }
            public int? EmployeeId { get; set; } */

            [Fact]
            [CustomAssertion]
            public async void ShouldGetUserByIDAsync()
            {

                Mock<ISecureXRepository> MoqRepo = new Mock<ISecureXRepository>();
                User user = new User()
                {
                    Id = 323,
                    UserName = "Bob",
                    FirstName = "Bob",
                    LastName = "Jole",
                    CustomerId = 11,
                    EmployeeId = null
                };

                MoqRepo.Setup(x => x.GetUserById(user.Id)).ReturnsAsync(user);
                var con = new UserController(MoqRepo.Object);
                var result = await con.GetById(user.Id);

                result.Value.Should().BeEquivalentTo(user);
            }


            [Fact]
            [CustomAssertion]
            public async void ShouldGetAllUsers()
            {

                Mock<ISecureXRepository> MoqRepo = new Mock<ISecureXRepository>();
                List<User> TestList = new List<User>();

                User user1 = new User()
                {
                    Id = 323,
                    UserName = "Bob",
                    FirstName = "Bob",
                    LastName = "Jole",
                    CustomerId = 11,
                    EmployeeId = null
                };

                User user2 = new User()
                {
                    Id = 3223,
                    UserName = "Sam",
                    FirstName = "Slew",
                    LastName = "Cew",
                    CustomerId = null,
                    EmployeeId = 1000
                };

                TestList.Add(user1);
                TestList.Add(user2);

                MoqRepo.Setup(x => x.GetUsers()).ReturnsAsync(TestList);
                var con = new UserController(MoqRepo.Object);
                var result = await con.GetAll();

                result.Should().BeEquivalentTo(TestList);
            }

        }
    }


}
