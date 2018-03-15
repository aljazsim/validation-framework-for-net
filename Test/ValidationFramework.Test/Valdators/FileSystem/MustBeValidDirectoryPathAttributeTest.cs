using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ValidationFramework.Test.Valdators
{
    [TestClass]
    public class MustBeValidDirectoryPathAttributeTest
    {
        #region Public Methods

        [TestMethod]
        public void GetDefaultMessage()
        {
            Assert.AreEqual("Value must be a valid directory path.", new MustBeValidDirectoryPathAttribute().GetDefaultMessage());
        }

        [TestMethod]
        public void GetDefaultMessageKey()
        {
            Assert.AreEqual("MustBeValidDirectoryPath", new MustBeValidDirectoryPathAttribute().GetDefaultMessageKey());
        }

        [TestMethod]
        public void GetParameters()
        {
            CollectionAssert.AreEqual(Array.Empty<object>(), new MustBeValidDirectoryPathAttribute().MessageParameters.ToList());
        }

        [TestMethod]
        public void IsValid()
        {
            var attribute = new MustBeValidDirectoryPathAttribute();

            Assert.AreEqual(true, attribute.IsValid(null));
            Assert.AreEqual(true, attribute.IsValid(DBNull.Value));
            Assert.AreEqual(false, attribute.IsValid(string.Empty));
            Assert.AreEqual(false, attribute.IsValid(" "));
            Assert.AreEqual(true, attribute.IsValid("abc"));
            Assert.AreEqual(true, attribute.IsValid(@"C:\Windows\tmp"));
            Assert.AreEqual(false, attribute.IsValid(@"C:\Wi|ndows\tmp"));
        }

        [DataRow(1)]
        [DataRow('c')]
        [DataTestMethod]
        public void IsValidFail(object value)
        {
            var attribute = new MustBeValidDirectoryPathAttribute();

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