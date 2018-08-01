using FluentAssertions;
using Moq;
using SecureXLibrary;
using SecureXWebApi.Controllers;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace SecureXTest
{
    public class CustomerControllerTest
    {
     /* public int Id { get; set; }
        public string Address { get; set; }
        public long PhoneNumber { get; set; }
        public string City { get; set; }*/

        [Fact]
        [CustomAssertion]
        public async void ShouldGetByID_Customer()
        {

            Mock<ISecureXRepository> MoqRepo = new Mock<ISecureXRepository>();

            Customer cus1 = new Customer()
            {
                Id = 901,
                Address = "102 Sterling Ave.",
                PhoneNumber = 3492033399,
                City = "Sterling"
            };

            MoqRepo.Setup(x => x.GetCustomerById(cus1.Id)).ReturnsAsync(cus1);
            var con = new CustomerController(MoqRepo.Object);
            var result = await con.GetById(cus1.Id);

            result.Value.Should().BeEquivalentTo(cus1);
        }


        [Fact]
        [CustomAssertion]
        public async void ShouldGetAll_Customer()
        {

            Mock<ISecureXRepository> MoqRepo = new Mock<ISecureXRepository>();
            List<Customer> NewList = new List<Customer>();

            Customer cus1 = new Customer()
            {
                Id = 901,
                Address = "102 Sterling Ave.",
                PhoneNumber = 3492033399,
                City = "Sterling"
            };

            Customer cus2 = new Customer()
            {
                Id = 501,
                Address = "105 Herndon Ave.",
                PhoneNumber = 3420331399,
                City = "Herndon"
            };

            NewList.Add(cus1);
            NewList.Add(cus2);

            MoqRepo.Setup(X => X.AddCustomer(cus1)).Returns(Task.CompletedTask);
            MoqRepo.Setup(x => x.AddCustomer(cus2)).Returns(Task.CompletedTask);
            MoqRepo.Setup(x => x.Save()).Returns(Task.CompletedTask);
            MoqRepo.Setup(x => x.GetCustomers()).ReturnsAsync(NewList);

            var con = new CustomerController(MoqRepo.Object);
            var result = await con.GetAll();

            result.Should().BeEquivalentTo(NewList);
        }
    }
}
