using System;
using System.Linq;
using System.Text.RegularExpressions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ValidationFramework.Test.Valdators
{
    [TestClass]
    public class CannotMatchAttributeTest
    {
        #region Public Methods

        [TestMethod]
        public void GetDefaultMessage()
        {
            Assert.AreEqual("Value cannot match {0}.", new CannotMatchAttribute("^[a-z]+$").GetDefaultMessage());
        }

        [TestMethod]
        public void GetDefaultMessageKey()
        {
            Assert.AreEqual("CannotMatch", new CannotMatchAttribute("^[a-z]+$").GetDefaultMessageKey());
        }

        [TestMethod]
        public void GetParameters()
        {
            CollectionAssert.AreEqual(new object[] { "^[a-z]+$" }, new CannotMatchAttribute("^[a-z]+$").MessageParameters.ToList());
        }

        [TestMethod]
        public void IsValid()
        {
            var attribute = new CannotMatchAttribute("^[a-z]+$", RegexOptions.IgnoreCase);

            Assert.AreEqual(true, attribute.IsValid(null));
            Assert.AreEqual(true, attribute.IsValid(DBNull.Value));
            Assert.AreEqual(true, attribute.IsValid(string.Empty));
            Assert.AreEqual(true, attribute.IsValid(" "));
            Assert.AreEqual(false, attribute.IsValid("abc"));
            Assert.AreEqual(false, attribute.IsValid("ABC"));
            Assert.AreEqual(true, attribute.IsValid("ab1c"));
        }

        [DataRow(1)]
        [DataRow('c')]
        [DataTestMethod]
        public void IsValidFail(object value)
        {
            var attribute = new CannotMatchAttribute("^[a-z]*$", RegexOptions.IgnoreCase);

            try
            {
                attribute.IsValid(value);

                Assert.Fail();
            }
            catch (ArgumentException ex)
            {
                Assert.AreEqual("Value must be of type String.", ex.Message);
            }
        }

        #endregion Public Methods
    }
}