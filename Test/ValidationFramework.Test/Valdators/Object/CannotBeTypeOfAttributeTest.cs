using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ValidationFramework.Test.Valdators
{
    [TestClass]
    public class CannotBeTypeOfAttributeTest
    {
        #region Public Methods

        [TestMethod]
        public void GetDefaultMessage()
        {
            Assert.AreEqual("Value cannot be type of {0}.", new CannotBeTypeOfAttribute(typeof(string)).GetDefaultMessage());
        }

        [TestMethod]
        public void GetDefaultMessageKey()
        {
            Assert.AreEqual("CannotBeTypeOf", new CannotBeTypeOfAttribute(typeof(string)).GetDefaultMessageKey());
        }

        [TestMethod]
        public void GetParameters()
        {
            CollectionAssert.AreEqual(new object[] { "String" }, new CannotBeTypeOfAttribute(typeof(string)).MessageParameters.ToList());
        }

        [TestMethod]
        public void IsValid()
        {
            var attribute = new CannotBeTypeOfAttribute(typeof(string));

            Assert.AreEqual(true, attribute.IsValid(null));
            Assert.AreEqual(true, attribute.IsValid(DBNull.Value));
            Assert.AreEqual(true, attribute.IsValid(1));
            Assert.AreEqual(false, attribute.IsValid("aaa"));
            Assert.AreEqual(false, attribute.IsValid(string.Empty));
            Assert.AreEqual(true, attribute.IsValid(Array.Empty<string>()));
            Assert.AreEqual(true, attribute.IsValid(new string[] { "aaa" }));
        }

        #endregion Public Methods
    }
}