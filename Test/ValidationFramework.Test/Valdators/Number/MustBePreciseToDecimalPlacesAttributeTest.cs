using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ValidationFramework.Test.Valdators
{
    [TestClass]
    public class MustBePreciseToDecimalPlacesAttributeTest
    {
        #region Public Methods

        [TestMethod]
        public void GetDefaultMessage()
        {
            Assert.AreEqual("Value must be precise to {0} decimal places.", new MustBePreciseToDecimalPlacesAttribute(5).GetDefaultMessage());
        }

        [TestMethod]
        public void GetDefaultMessageKey()
        {
            Assert.AreEqual("MustBePreciseToDecimalPlaces", new MustBePreciseToDecimalPlacesAttribute(5).GetDefaultMessageKey());
        }

        [TestMethod]
        public void GetParameters()
        {
            CollectionAssert.AreEqual(new object[] { 5 }, new MustBePreciseToDecimalPlacesAttribute(5).MessageParameters.ToList());
        }

        [TestMethod]
        public void IsValid()
        {
            var attribute = new MustBePreciseToDecimalPlacesAttribute(0);

            Assert.AreEqual(true, attribute.IsValid(null));
            Assert.AreEqual(true, attribute.IsValid(DBNull.Value));

            Assert.AreEqual(true, attribute.IsValid(1));
            Assert.AreEqual(true, attribute.IsValid(1.0));
            Assert.AreEqual(true, attribute.IsValid(0));
            Assert.AreEqual(true, attribute.IsValid(0.0));
            Assert.AreEqual(true, attribute.IsValid(0.0000000));
            Assert.AreEqual(false, attribute.IsValid(1.1));
            Assert.AreEqual(false, attribute.IsValid(1.11));
            Assert.AreEqual(false, attribute.IsValid(1.111));

            attribute = new MustBePreciseToDecimalPlacesAttribute(1);

            Assert.AreEqual(true, attribute.IsValid(1));
            Assert.AreEqual(true, attribute.IsValid(1.0));
            Assert.AreEqual(true, attribute.IsValid(1.1));
            Assert.AreEqual(false, attribute.IsValid(1.11));
            Assert.AreEqual(false, attribute.IsValid(1.111));

            attribute = new MustBePreciseToDecimalPlacesAttribute(2);

            Assert.AreEqual(true, attribute.IsValid(1));
            Assert.AreEqual(true, attribute.IsValid(1.0));
            Assert.AreEqual(true, attribute.IsValid(1.1));
            Assert.AreEqual(true, attribute.IsValid(1.11));

            attribute = new MustBePreciseToDecimalPlacesAttribute(3);

            Assert.AreEqual(true, attribute.IsValid(1));
            Assert.AreEqual(true, attribute.IsValid(1.0));
            Assert.AreEqual(true, attribute.IsValid(1.1));
            Assert.AreEqual(true, attribute.IsValid(1.11));
            Assert.AreEqual(true, attribute.IsValid(1.111));

            attribute = new MustBePreciseToDecimalPlacesAttribute(4);

            Assert.AreEqual(true, attribute.IsValid(1f));
            Assert.AreEqual(true, attribute.IsValid(1.0f));
            Assert.AreEqual(true, attribute.IsValid(1.1f));
            Assert.AreEqual(true, attribute.IsValid(1.1f));
            Assert.AreEqual(true, attribute.IsValid(1.111f));
            Assert.AreEqual(true, attribute.IsValid(1.1111f));

            attribute = new MustBePreciseToDecimalPlacesAttribute(4);

            Assert.AreEqual(true, attribute.IsValid(1m));
            Assert.AreEqual(true, attribute.IsValid(1.0m));
            Assert.AreEqual(true, attribute.IsValid(1.1m));
            Assert.AreEqual(true, attribute.IsValid(1.11m));
            Assert.AreEqual(true, attribute.IsValid(1.111m));
            Assert.AreEqual(true, attribute.IsValid(1.1111m));
        }

        [DataRow('c')]
        [DataRow("number")]
        [DataTestMethod]
        public void IsValidFail(object value)
        {
            var attribute = new MustBePreciseToDecimalPlacesAttribute(8);

            try
            {
                attribute.IsValid(value);

                Assert.Fail();
            }
            catch (ArgumentException ex)
            {
                Assert.AreEqual("Invalid value type.", ex.Message);
            }
        }

        #endregion Public Methods
    }
}