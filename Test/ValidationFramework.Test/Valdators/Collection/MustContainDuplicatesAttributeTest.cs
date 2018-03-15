using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ValidationFramework.Test.Valdators
{
    [TestClass]
    public class MustContainDuplicatesAttributeTest
    {
        #region Public Methods

        [TestMethod]
        public void GetDefaultMessage()
        {
            Assert.AreEqual("Value must contain duplicates.", new MustContainDuplicatesAttribute().GetDefaultMessage());
        }

        [TestMethod]
        public void GetDefaultMessageKey()
        {
            Assert.AreEqual("MustContainDuplicates", new MustContainDuplicatesAttribute().GetDefaultMessageKey());
        }

        [TestMethod]
        public void GetParameters()
        {
            CollectionAssert.AreEqual(Array.Empty<object>(), new MustContainDuplicatesAttribute().MessageParameters.ToList());
        }

        [TestMethod]
        public void IsValid()
        {
            var attribute = new MustContainDuplicatesAttribute();

            Assert.AreEqual(true, attribute.IsValid(null));
            Assert.AreEqual(true, attribute.IsValid(DBNull.Value));
            Assert.AreEqual(false, attribute.IsValid(string.Empty));
            Assert.AreEqual(!true, attribute.IsValid("a"));
            Assert.AreEqual(!false, attribute.IsValid("aa"));
            Assert.AreEqual(!true, attribute.IsValid("abc"));
            Assert.AreEqual(!false, attribute.IsValid("abac"));
            Assert.AreEqual(!false, attribute.IsValid("abca"));
            Assert.AreEqual(!true, attribute.IsValid(new int[] { 1, 2, 3 }));
            Assert.AreEqual(!false, attribute.IsValid(new int[] { 1, 2, 1, 3 }));
        }

        [DataRow(1)]
        [DataRow('c')]
        [DataTestMethod]
        public void IsValidFail(object value)
        {
            var attribute = new MustContainDuplicatesAttribute();

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