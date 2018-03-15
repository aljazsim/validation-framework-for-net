using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ValidationFramework.Test.Valdators
{
    [TestClass]
    public class MustBeUpperCaseAttributeTest
    {
        #region Public Methods

        [TestMethod]
        public void GetDefaultMessage()
        {
            Assert.AreEqual("Value must be upper case.", new MustBeUpperCaseAttribute().GetDefaultMessage());
        }

        [TestMethod]
        public void GetDefaultMessageKey()
        {
            Assert.AreEqual("MustBeUpperCase", new MustBeUpperCaseAttribute().GetDefaultMessageKey());
        }

        [TestMethod]
        public void GetParameters()
        {
            CollectionAssert.AreEqual(Array.Empty<object>(), new MustBeUpperCaseAttribute().MessageParameters.ToList());
        }

        [TestMethod]
        public void IsValid()
        {
            var attribute = new MustBeUpperCaseAttribute();

            Assert.AreEqual(true, attribute.IsValid(null));
            Assert.AreEqual(true, attribute.IsValid(DBNull.Value));
            Assert.AreEqual(true, attribute.IsValid(string.Empty));
            Assert.AreEqual(true, attribute.IsValid(" "));
            Assert.AreEqual(false, attribute.IsValid("abc"));
            Assert.AreEqual(true, attribute.IsValid("ABC"));
            Assert.AreEqual(false, attribute.IsValid("ab1c"));
            Assert.AreEqual(true, attribute.IsValid(" AB 1 C "));
            Assert.AreEqual(false, attribute.IsValid(" ab 1 c "));
        }

        [DataRow(1)]
        [DataRow('c')]
        [DataTestMethod]
        public void IsValidFail(object value)
        {
            var attribute = new MustBeUpperCaseAttribute();

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