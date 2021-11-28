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
        public void statementTest_1()
        {
            // Arrange
            var customer = new Customer("Frank");
            var parameterList = new List<RentalParameter>()
            {
                new RentalParameter("StartWars",Movie.NEW_RELEASE,3),
                new RentalParameter("Avatar",Movie.CHILDRENS,1),
                new RentalParameter("Matrixs",Movie.REGULAR,5),
                new RentalParameter("KongFu",Movie.NEW_RELEASE,3),
            };
            parameterList.ForEach(x => customer.addRental(ToRental(x)));

            var expexted = "Rental Record for Frank\n\tStartWars\t9\n\tAvatar\t1.5\n\tMatrixs\t6.5\n\tKongFu\t9\nAmount owed is 26\nYou earned 6 frequent renter popints";
            // Act
            var actual = customer.statement();

            // Assert
            Assert.AreEqual(actual, expexted);
        }

        [TestMethod()]
        public void statementTest_2()
        {
            // Arrange
            var customer = new Customer("Frank");
            var parameterList = new List<RentalParameter>()
            {
                new RentalParameter("StartWars",Movie.NEW_RELEASE,3),
                new RentalParameter("StartWars2",Movie.NEW_RELEASE,4),
                new RentalParameter("Catch Me if You Can",Movie.NEW_RELEASE,5),
                new RentalParameter("Avatar",Movie.CHILDRENS,1),
                new RentalParameter("Matrixs",Movie.REGULAR,5),
                new RentalParameter("KongFu",Movie.NEW_RELEASE,3),
            };
            parameterList.ForEach(x => customer.addRental(ToRental(x)));

            var expexted = "Rental Record for Frank\n\tStartWars\t9\n\tStartWars2\t12\n\tCatch Me if You Can\t15\n\tAvatar\t1.5\n\tMatrixs\t6.5\n\tKongFu\t9\nAmount owed is 53\nYou earned 10 frequent renter popints";

            // Act
            var actual = customer.statement();

            // Assert
            Assert.AreEqual(actual, expexted);
        }

        [TestMethod()]
        public void statementTest_3()
        {
            // Arrange
            var customer = new Customer("Tina");
            var parameterList = new List<RentalParameter>()
            {
                new RentalParameter("Stay",Movie.NEW_RELEASE,1),
                new RentalParameter("Butterfly Effect",Movie.NEW_RELEASE,1),
                new RentalParameter("Catch Me if You Can",Movie.NEW_RELEASE,1),
                new RentalParameter("Avatar",Movie.CHILDRENS,1),
                new RentalParameter("Matrixs",Movie.REGULAR,1),
                new RentalParameter("KongFu",Movie.CHILDRENS,1),
            };
            parameterList.ForEach(x => customer.addRental(ToRental(x)));

            var expexted = "Rental Record for Tina\n\tStay\t3\n\tButterfly Effect\t3\n\tCatch Me if You Can\t3\n\tAvatar\t1.5\n\tMatrixs\t2\n\tKongFu\t1.5\nAmount owed is 14\nYou earned 6 frequent renter popints";

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