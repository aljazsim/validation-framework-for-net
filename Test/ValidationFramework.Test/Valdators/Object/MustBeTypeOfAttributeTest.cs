using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ValidationFramework.Test.Valdators
{
    [TestClass]
    public class MustBeTypeOfAttributeTest
    {
        #region Public Methods

        [TestMethod]
        public void GetDefaultMessage()
        {
            Assert.AreEqual("Value must be type of {0}.", new MustBeTypeOfAttribute(typeof(string)).GetDefaultMessage());
        }

        [TestMethod]
        public void GetDefaultMessageKey()
        {
            Assert.AreEqual("MustBeTypeOf", new MustBeTypeOfAttribute(typeof(string)).GetDefaultMessageKey());
        }

        [TestMethod]
        public void GetParameters()
        {
            CollectionAssert.AreEqual(new object[] { "String" }, new MustBeTypeOfAttribute(typeof(string)).MessageParameters.ToList());
        }

        [TestMethod]
        public void IsValid()
        {
            var attribute = new MustBeTypeOfAttribute(typeof(string));

            Assert.AreEqual(true, attribute.IsValid(null));
            Assert.AreEqual(true, attribute.IsValid(DBNull.Value));
            Assert.AreEqual(false, attribute.IsValid(1));
            Assert.AreEqual(true, attribute.IsValid("aaa"));
            Assert.AreEqual(true, attribute.IsValid(string.Empty));
            Assert.AreEqual(false, attribute.IsValid(Array.Empty<string>()));
            Assert.AreEqual(false, attribute.IsValid(new string[] { "aaa" }));
        }

        #endregion Public Methods
    }
}