using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ValidationFramework.Test.Valdators
{
    [TestClass]
    public class CannotContainNullAttributeTest
    {
        #region Public Methods

        [TestMethod]
        public void GetDefaultMessage()
        {
            Assert.AreEqual("Value cannot contain null.", new CannotContainNullAttribute().GetDefaultMessage());
        }

        [TestMethod]
        public void GetDefaultMessageKey()
        {
            Assert.AreEqual("CannotContainNull", new CannotContainNullAttribute().GetDefaultMessageKey());
        }

        [TestMethod]
        public void GetParameters()
        {
            CollectionAssert.AreEqual(Array.Empty<object>(), new CannotContainNullAttribute().MessageParameters.ToList());
        }

        [TestMethod]
        public void IsValid()
        {
            var attribute = new CannotContainNullAttribute();

            Assert.AreEqual(true, attribute.IsValid(null));
            Assert.AreEqual(true, attribute.IsValid(DBNull.Value));
            Assert.AreEqual(true, attribute.IsValid(string.Empty));
            Assert.AreEqual(false, attribute.IsValid(new object[] { null }));
            Assert.AreEqual(true, attribute.IsValid(new object[] { 1 }));
            Assert.AreEqual(true, attribute.IsValid(new object[] { 1, 2 }));
            Assert.AreEqual(false, attribute.IsValid(new object[] { 1, 2, false, null, 3 }));
            Assert.AreEqual(true, attribute.IsValid(new object[] { 1, 2, 3 }));
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