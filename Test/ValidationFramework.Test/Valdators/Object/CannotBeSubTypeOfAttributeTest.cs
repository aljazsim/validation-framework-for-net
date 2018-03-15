using System;
using System.Collections;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ValidationFramework.Test.Valdators
{
    [TestClass]
    public class CannotBeSubTypeOfAttributeTest
    {
        #region Public Methods

        [TestMethod]
        public void GetDefaultMessage()
        {
            Assert.AreEqual("Value cannot be sub-type of {0}.", new CannotBeSubTypeOfAttribute(typeof(string)).GetDefaultMessage());
        }

        [TestMethod]
        public void GetDefaultMessageKey()
        {
            Assert.AreEqual("CannotBeSubTypeOf", new CannotBeSubTypeOfAttribute(typeof(string)).GetDefaultMessageKey());
        }

        [TestMethod]
        public void GetParameters()
        {
            CollectionAssert.AreEqual(new object[] { "String" }, new CannotBeSubTypeOfAttribute(typeof(string)).MessageParameters.ToList());
        }

        [TestMethod]
        public void IsValid()
        {
            var attribute = new CannotBeSubTypeOfAttribute(typeof(IEnumerable));

            Assert.AreEqual(true, attribute.IsValid(null));
            Assert.AreEqual(true, attribute.IsValid(DBNull.Value));
            Assert.AreEqual(true, attribute.IsValid(1));
            Assert.AreEqual(false, attribute.IsValid("aaa"));
            Assert.AreEqual(false, attribute.IsValid(Array.Empty<string>()));
            Assert.AreEqual(false, attribute.IsValid(new string[] { "aaa" }));
        }

        #endregion Public Methods
    }
}