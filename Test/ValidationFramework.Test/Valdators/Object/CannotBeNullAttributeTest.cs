using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ValidationFramework.Test.Valdators
{
    [TestClass]
    public class CannotBeNullAttributeTest
    {
        #region Public Methods

        [TestMethod]
        public void GetDefaultMessage()
        {
            Assert.AreEqual("Value cannot be null.", new CannotBeNullAttribute().GetDefaultMessage());
        }

        [TestMethod]
        public void GetDefaultMessageKey()
        {
            Assert.AreEqual("CannotBeNull", new CannotBeNullAttribute().GetDefaultMessageKey());
        }

        [TestMethod]
        public void GetParameters()
        {
            CollectionAssert.AreEqual(Array.Empty<object>(), new CannotBeNullAttribute().MessageParameters.ToList());
        }

        [TestMethod]
        public void IsValid()
        {
            var attribute = new CannotBeNullAttribute();

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