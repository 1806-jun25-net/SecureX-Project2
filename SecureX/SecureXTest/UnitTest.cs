using System;
using Xunit;
using FluentAssertions;
using SecureXLibrary;

namespace SecureXTest
{
    public class UnitTest
    {
        private readonly SecureXRepository sx = new SecureXRepository();

        private readonly Transaction transact1pos = new Transaction("Carl", DateTime.Now, 500.00m);
        private readonly Transaction transact1neg = new Transaction("Mano", DateTime.Now, -20.00m);
        private readonly CreditCard credit1debt = new CreditCard(500m, 200.00m, 234563);
        private readonly CreditCard credit1nodebt = new CreditCard(1000.00m, 0m, 234563);

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

    }
}
