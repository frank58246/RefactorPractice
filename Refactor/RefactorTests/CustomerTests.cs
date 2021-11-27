using Microsoft.VisualStudio.TestTools.UnitTesting;
using Refactor;
using System;
using System.Collections.Generic;
using System.Text;
using FluentAssertions;

namespace Refactor.Tests
{
    public class RentalParameter
    {
        public string Name { get; set; }
        public int Type { get; set; }
        public int RentDays { get; set; }

        public RentalParameter(string name, int type, int rentDays)
        {
            Name = name;
            Type = type;
            RentDays = rentDays;
        }
    }

    [TestClass()]
    public class CustomerTests
    {
        [TestMethod()]
        public void statementTest()
        {
            // Arrange
            var customer = new Customer("Frank");
            var retalList = new List<RentalParameter>()
            {
                new RentalParameter("StartWars",Movie.NEW_RELEASE,3),
                new RentalParameter("Avatar",Movie.CHILDRENS,1),
                new RentalParameter("Matrixs",Movie.REGULAR,5),
                new RentalParameter("KongFu",Movie.NEW_RELEASE,3),
            };
            retalList.ForEach(x => customer.addRental(ToRental(x)));

            var expexted = "Rental Record for Frank\n\tStartWars\t9\n\tAvatar\t1.5\n\tMatrixs\t6.5\n\tKongFu\t9\nAmount owed is 26\nYou earned 6 frequent renter popints";
            // Act
            var actual = customer.statement();

            // Assert
            Assert.AreEqual(actual, expexted);
        }

        private Rental ToRental(RentalParameter parameter)
        {
            var movie = new Movie(parameter.Name, parameter.Type);
            return new Rental(movie, parameter.RentDays);
        }
    }
}