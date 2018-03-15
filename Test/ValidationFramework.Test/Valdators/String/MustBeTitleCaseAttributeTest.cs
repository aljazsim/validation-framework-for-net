using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ValidationFramework.Test.Valdators
{
    [TestClass]
    public class MustBeTitleCaseAttributeTest
    {
        #region Public Methods

        [TestMethod]
        public void GetDefaultMessage()
        {
            Assert.AreEqual("Value must be title case.", new MustBeTitleCaseAttribute().GetDefaultMessage());
        }

        [TestMethod]
        public void GetDefaultMessageKey()
        {
            Assert.AreEqual("MustBeTitleCase", new MustBeTitleCaseAttribute().GetDefaultMessageKey());
        }

        [TestMethod]
        public void GetParameters()
        {
            CollectionAssert.AreEqual(Array.Empty<object>(), new MustBeTitleCaseAttribute().MessageParameters.ToList());
        }

        [TestMethod]
        public void IsValid()
        {
            var attribute = new MustBeTitleCaseAttribute();

            Assert.AreEqual(true, attribute.IsValid(null));
            Assert.AreEqual(true, attribute.IsValid(DBNull.Value));
            Assert.AreEqual(true, attribute.IsValid(string.Empty));
            Assert.AreEqual(true, attribute.IsValid(" "));
            Assert.AreEqual(false, attribute.IsValid("abc"));
            Assert.AreEqual(false, attribute.IsValid("ABC"));
            Assert.AreEqual(true, attribute.IsValid("A B C "));
            Assert.AreEqual(true, attribute.IsValid("Mary Had A Little Lamb."));
            Assert.AreEqual(false, attribute.IsValid("Mary Had a Little Lamb."));
        }

        [DataRow(1)]
        [DataRow('c')]
        [DataTestMethod]
        public void IsValidFail(object value)
        {
            var attribute = new MustBeTitleCaseAttribute();

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