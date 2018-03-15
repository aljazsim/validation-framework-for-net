using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ValidationFramework.Test.Valdators
{
    [TestClass]
    public class CannotContainDuplicatesAttributeTest
    {
        #region Public Methods

        [TestMethod]
        public void GetDefaultMessage()
        {
            Assert.AreEqual("Value cannot contain duplicates.", new CannotContainDuplicatesAttribute().GetDefaultMessage());
        }

        [TestMethod]
        public void GetDefaultMessageKey()
        {
            Assert.AreEqual("CannotContainDuplicates", new CannotContainDuplicatesAttribute().GetDefaultMessageKey());
        }

        [TestMethod]
        public void IsValid()
        {
            var attribute = new CannotContainDuplicatesAttribute();

            Assert.AreEqual(true, attribute.IsValid(null));
            Assert.AreEqual(true, attribute.IsValid(DBNull.Value));
            Assert.AreEqual(true, attribute.IsValid(string.Empty));
            Assert.AreEqual(true, attribute.IsValid("a"));
            Assert.AreEqual(false, attribute.IsValid("aa"));
            Assert.AreEqual(true, attribute.IsValid("abc"));
            Assert.AreEqual(false, attribute.IsValid("abac"));
            Assert.AreEqual(false, attribute.IsValid("abca"));
            Assert.AreEqual(true, attribute.IsValid(new int[] { 1, 2, 3 }));
            Assert.AreEqual(false, attribute.IsValid(new int[] { 1, 2, 1, 3 }));
        }

        [TestMethod]
        public void GetParameters()
        {
            CollectionAssert.AreEqual(Array.Empty<object>(), new CannotContainDuplicatesAttribute().MessageParameters.ToList());
        }

        [DataRow(1)]
        [DataRow('c')]
        [DataTestMethod]
        public void IsValidFail(object value)
        {
            var attribute = new CannotContainDuplicatesAttribute();

            try
            {
                attribute.IsValid(value);

                Assert.Fail();
            }
            catch (ArgumentException ex)
            {
                Assert.AreEqual("Value must be subtype of IEnumerable.", ex.Message);
            }
        }

        #endregion Public Methods
    }
}