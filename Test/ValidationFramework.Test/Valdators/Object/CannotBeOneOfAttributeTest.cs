using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ValidationFramework.Test.Valdators
{
    [TestClass]
    public class CannotBeOneOfAttributeTest
    {
        #region Public Methods

        [TestMethod]
        public void GetDefaultMessage()
        {
            Assert.AreEqual("Value cannot be one of: {0}.", new CannotBeOneOfAttribute("a").GetDefaultMessage());
        }

        [TestMethod]
        public void GetDefaultMessageKey()
        {
            Assert.AreEqual("CannotBeOneOf", new CannotBeOneOfAttribute("a").GetDefaultMessageKey());
        }

        [TestMethod]
        public void GetParameters()
        {
            CollectionAssert.AreEqual(new object[] { "a" }, new CannotBeOneOfAttribute("a").MessageParameters.ToList());
        }

        [TestMethod]
        public void IsValid()
        {
            var attribute = new CannotBeOneOfAttribute(0, 1, 2, 3);

            Assert.AreEqual(true, attribute.IsValid(null));
            Assert.AreEqual(true, attribute.IsValid(DBNull.Value));
            Assert.AreEqual(!false, attribute.IsValid(-1));
            Assert.AreEqual(!false, attribute.IsValid(5));
            Assert.AreEqual(!true, attribute.IsValid(0));
            Assert.AreEqual(!true, attribute.IsValid(1));
            Assert.AreEqual(!true, attribute.IsValid(2));
            Assert.AreEqual(!true, attribute.IsValid(3));
        }

        #endregion Public Methods
    }
}