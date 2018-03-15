using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ValidationFramework.Test.Valdators
{
    [TestClass]
    public class MustBeNullAttributeTest
    {
        #region Public Methods

        [TestMethod]
        public void GetDefaultMessage()
        {
            Assert.AreEqual("Value must be null.", new MustBeNullAttribute().GetDefaultMessage());
        }

        [TestMethod]
        public void GetDefaultMessageKey()
        {
            Assert.AreEqual("MustBeNull", new MustBeNullAttribute().GetDefaultMessageKey());
        }

        [TestMethod]
        public void GetParameters()
        {
            CollectionAssert.AreEqual(Array.Empty<object>(), new MustBeNullAttribute().MessageParameters.ToList());
        }

        [TestMethod]
        public void IsValid()
        {
            var attribute = new MustBeNullAttribute();

            Assert.AreEqual(false, attribute.IsValid(null));
            Assert.AreEqual(false, attribute.IsValid(DBNull.Value));
            Assert.AreEqual(true, attribute.IsValid(1));
            Assert.AreEqual(true, attribute.IsValid("aaa"));
            Assert.AreEqual(true, attribute.IsValid(Array.Empty<string>()));
            Assert.AreEqual(true, attribute.IsValid(new string[] { "aaa" }));
        }

        #endregion Public Methods
    }
}