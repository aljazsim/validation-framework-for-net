using System;
using System.Linq;
using System.Text.RegularExpressions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ValidationFramework.Test.Valdators
{
    [TestClass]
    public class MustMatchAttributeTest
    {
        #region Public Methods

        [TestMethod]
        public void GetDefaultMessage()
        {
            Assert.AreEqual("Value must match {0}.", new MustMatchAttribute("^[a-z]+$").GetDefaultMessage());
        }

        [TestMethod]
        public void GetDefaultMessageKey()
        {
            Assert.AreEqual("MustMatch", new MustMatchAttribute("^[a-z]+$").GetDefaultMessageKey());
        }

        [TestMethod]
        public void GetParameters()
        {
            CollectionAssert.AreEqual(new object[] { "^[a-z]+$" }, new MustMatchAttribute("^[a-z]+$").MessageParameters.ToList());
        }

        [TestMethod]
        public void IsValid()
        {
            var attribute = new MustMatchAttribute("^[a-z]+$", RegexOptions.IgnoreCase);

            Assert.AreEqual(true, attribute.IsValid(null));
            Assert.AreEqual(true, attribute.IsValid(DBNull.Value));
            Assert.AreEqual(false, attribute.IsValid(string.Empty));
            Assert.AreEqual(false, attribute.IsValid(" "));
            Assert.AreEqual(true, attribute.IsValid("abc"));
            Assert.AreEqual(true, attribute.IsValid("ABC"));
            Assert.AreEqual(false, attribute.IsValid("ab1c"));
        }

        [DataRow(1)]
        [DataRow('c')]
        [DataTestMethod]
        public void IsValidFail(object value)
        {
            var attribute = new MustMatchAttribute("^[a-z]*$", RegexOptions.IgnoreCase);

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