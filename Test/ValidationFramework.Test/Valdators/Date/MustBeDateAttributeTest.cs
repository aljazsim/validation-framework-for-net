using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ValidationFramework.Test.Valdators
{
    [TestClass]
    public class MustBeDateAttributeTest
    {
        #region Public Methods

        [TestMethod]
        public void GetDefaultMessage()
        {
            Assert.AreEqual("Value must be a date.", new MustBeDateAttribute().GetDefaultMessage());
        }

        [TestMethod]
        public void GetDefaultMessageKey()
        {
            Assert.AreEqual("MustBeADate", new MustBeDateAttribute().GetDefaultMessageKey());
        }

        [TestMethod]
        public void GetParameters()
        {
            CollectionAssert.AreEqual(Array.Empty<object>(), new MustBeDateAttribute().MessageParameters.ToList());
        }

        [TestMethod]
        public void IsValid()
        {
            DateTime? dateNullable = new DateTime(2018, 3, 4);
            var attribute = new MustBeDateAttribute();

            Assert.AreEqual(true, attribute.IsValid(DateTime.Today as DateTime?));
            Assert.AreEqual(true, attribute.IsValid(null as DateTime?));
            Assert.AreEqual(true, attribute.IsValid(DateTime.Today));
            Assert.AreEqual(false, attribute.IsValid(DateTime.Now));

            Assert.AreEqual(true, attribute.IsValid(null as DateTime?));
            Assert.AreEqual(false, attribute.IsValid(DateTime.Now as DateTime?));
            Assert.AreEqual(true, attribute.IsValid(dateNullable));
        }

        [DataRow('c')]
        [DataRow("number")]
        [DataTestMethod]
        public void IsValidFail(object value)
        {
            var attribute = new MustBeDateAttribute();

            try
            {
                attribute.IsValid(value);

                Assert.Fail();
            }
            catch (ArgumentException ex)
            {
                Assert.AreEqual("Value must be of type DateTime.", ex.Message);
            }
        }

        #endregion Public Methods
    }
}