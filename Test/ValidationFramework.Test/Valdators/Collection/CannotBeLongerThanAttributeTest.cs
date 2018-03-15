using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ValidationFramework.Test.Valdators
{
    [TestClass]
    public class CannotBeLongerThanAttributeTest
    {
        #region Public Methods

        [TestMethod]
        public void GetDefaultMessage()
        {
            Assert.AreEqual("Value cannot have more than or equal to {0} items.", new CannotBeLongerThanAttribute(3).GetDefaultMessage());
        }

        [TestMethod]
        public void GetDefaultMessageKey()
        {
            Assert.AreEqual("CannotBeLongerThan", new CannotBeLongerThanAttribute(3).GetDefaultMessageKey());
        }

        [TestMethod]
        public void GetParameters()
        {
            CollectionAssert.AreEqual(new object[] { 5 }, new CannotBeLongerThanAttribute(5).MessageParameters.ToList());
        }

        [TestMethod]
        public void IsValid()
        {
            var attribute = new CannotBeLongerThanAttribute(3);

            Assert.AreEqual(true, attribute.IsValid(null));
            Assert.AreEqual(true, attribute.IsValid(DBNull.Value));
            Assert.AreEqual(!false, attribute.IsValid(string.Empty));
            Assert.AreEqual(!false, attribute.IsValid("a"));
            Assert.AreEqual(!false, attribute.IsValid("aa"));
            Assert.AreEqual(!false, attribute.IsValid("aaa"));
            Assert.AreEqual(!true, attribute.IsValid("aaaa"));
        }

        [DataRow(1)]
        [DataRow('c')]
        [DataTestMethod]
        public void IsValidFail(object value)
        {
            var attribute = new CannotBeLongerThanAttribute(3);

            try
            {
                attribute.IsValid(value);

                Assert.Fail();
            }
            catch (ArgumentException ex)
            {
                Assert.AreEqual("Value must be subtype of IEnumerable.", ex.Message);
            }
        }

        #endregion Public Methods
    }
}