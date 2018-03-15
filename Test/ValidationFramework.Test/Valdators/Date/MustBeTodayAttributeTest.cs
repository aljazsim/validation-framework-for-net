using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ValidationFramework.Test.Valdators
{
    [TestClass]
    public class MustBeTodayAttributeTest
    {
        #region Public Methods

        [TestMethod]
        public void GetDefaultMessage()
        {
            Assert.AreEqual("Value must be today.", new MustBeTodayAttribute().GetDefaultMessage());
        }

        [TestMethod]
        public void GetDefaultMessageKey()
        {
            Assert.AreEqual("MustBeToday", new MustBeTodayAttribute().GetDefaultMessageKey());
        }

        [TestMethod]
        public void GetParameters()
        {
            CollectionAssert.AreEqual(Array.Empty<object>(), new MustBeTodayAttribute().MessageParameters.ToList());
        }

        [TestMethod]
        public void IsValid()
        {
            DateTime? dateNullable = (DateTime?)new DateTime(2018, 3, 4);
            var attribute = new MustBeTodayAttribute();

            Assert.AreEqual(true, attribute.IsValid(null));
            Assert.AreEqual(true, attribute.IsValid(DBNull.Value));
            Assert.AreEqual(true, attribute.IsValid(DateTime.Today));
            Assert.AreEqual(false, attribute.IsValid(DateTime.Today.AddDays(1)));
            Assert.AreEqual(false, attribute.IsValid(DateTime.Today.AddDays(-1)));
            Assert.AreEqual(true, attribute.IsValid(DateTime.Now));
            Assert.AreEqual(true, attribute.IsValid(DateTime.Now.AddMinutes(-1)));
            Assert.AreEqual(true, attribute.IsValid(DateTime.Now.AddMinutes(1)));
            Assert.AreEqual(true, attribute.IsValid(DateTime.Today as DateTime?));
            Assert.AreEqual(true, attribute.IsValid(null as DateTime?));
            Assert.AreEqual(false, attribute.IsValid(dateNullable));
        }

        [DataRow('c')]
        [DataRow("number")]
        [DataTestMethod]
        public void IsValidFail(object value)
        {
            var attribute = new MustBeTodayAttribute();

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