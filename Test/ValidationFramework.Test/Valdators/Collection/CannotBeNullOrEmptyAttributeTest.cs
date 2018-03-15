using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ValidationFramework.Test.Valdators
{
    [TestClass]
    public class CannotBeNullOrEmptyAttributeTest
    {
        #region Public Methods

        [TestMethod]
        public void GetDefaultMessage()
        {
            Assert.AreEqual("Value cannot be null or empty.", new CannotBeNullOrEmptyAttribute().GetDefaultMessage());
        }

        [TestMethod]
        public void GetDefaultMessageKey()
        {
            Assert.AreEqual("CannotBeNullOrEmpty", new CannotBeNullOrEmptyAttribute().GetDefaultMessageKey());
        }

        [TestMethod]
        public void GetParameters()
        {
            CollectionAssert.AreEqual(Array.Empty<object>(), new CannotBeNullOrEmptyAttribute().MessageParameters.ToList());
        }

        [TestMethod]
        public void IsValid()
        {
            var attribute = new CannotBeNullOrEmptyAttribute();

            Assert.AreEqual(!true, attribute.IsValid(null));
            Assert.AreEqual(!true, attribute.IsValid(DBNull.Value));
            Assert.AreEqual(!true, attribute.IsValid(string.Empty));
            Assert.AreEqual(!false, attribute.IsValid("aaa"));
            Assert.AreEqual(!true, attribute.IsValid(Array.Empty<string>()));
            Assert.AreEqual(!false, attribute.IsValid(new string[] { "aaa" }));
            Assert.AreEqual(!true, attribute.IsValid(Array.Empty<int>()));
            Assert.AreEqual(!false, attribute.IsValid(new int[] { 1 }));
        }

        [DataRow(1)]
        [DataRow('c')]
        [DataTestMethod]
        public void IsValidFail(object value)
        {
            var attribute = new CannotBeNullOrEmptyAttribute();

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