using System;
using System.Collections;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ValidationFramework.Test.Valdators
{
    [TestClass]
    public class MustBeSubTypeOfAttributeTest
    {
        #region Public Methods

        [TestMethod]
        public void GetDefaultMessage()
        {
            Assert.AreEqual("Value must be sub-type of {0}.", new MustBeSubTypeOfAttribute(typeof(string)).GetDefaultMessage());
        }

        [TestMethod]
        public void GetDefaultMessageKey()
        {
            Assert.AreEqual("MustBeSubTypeOf", new MustBeSubTypeOfAttribute(typeof(string)).GetDefaultMessageKey());
        }

        [TestMethod]
        public void GetParameters()
        {
            CollectionAssert.AreEqual(new object[] { "String" }, new MustBeSubTypeOfAttribute(typeof(string)).MessageParameters.ToList());
        }

        [TestMethod]
        public void IsValid()
        {
            var attribute = new MustBeSubTypeOfAttribute(typeof(IEnumerable));

            Assert.AreEqual(true, attribute.IsValid(null));
            Assert.AreEqual(true, attribute.IsValid(DBNull.Value));
            Assert.AreEqual(false, attribute.IsValid(1));
            Assert.AreEqual(true, attribute.IsValid("aaa"));
            Assert.AreEqual(true, attribute.IsValid(Array.Empty<string>()));
            Assert.AreEqual(true, attribute.IsValid(new string[] { "aaa" }));
        }

        #endregion Public Methods
    }
}