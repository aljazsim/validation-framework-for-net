using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ValidationFramework.Test.Valdators
{
    [TestClass]
    public class MustBeValidUriAttributeTest
    {
        #region Public Methods

        [TestMethod]
        public void GetDefaultMessage()
        {
            Assert.AreEqual("Value must be a valid URI.", new MustBeValidUriAttribute().GetDefaultMessage());
        }

        [TestMethod]
        public void GetDefaultMessageKey()
        {
            Assert.AreEqual("MustBeValidUri", new MustBeValidUriAttribute().GetDefaultMessageKey());
        }

        [TestMethod]
        public void GetParameters()
        {
            CollectionAssert.AreEqual(Array.Empty<object>(), new MustBeValidUriAttribute().MessageParameters.ToList());
        }

        [TestMethod]
        public void IsValid()
        {
            var attribute = new MustBeValidUriAttribute();

            Assert.AreEqual(true, attribute.IsValid(null));
            Assert.AreEqual(true, attribute.IsValid(DBNull.Value));
            Assert.AreEqual(false, attribute.IsValid(string.Empty));
            Assert.AreEqual(false, attribute.IsValid("sdfdsf"));
            Assert.AreEqual(false, attribute.IsValid("www.google.com"));
            Assert.AreEqual(false, attribute.IsValid("google.com"));
            Assert.AreEqual(true, attribute.IsValid("http://google"));
            Assert.AreEqual(true, attribute.IsValid("http://www.google.com/"));
            Assert.AreEqual(true, attribute.IsValid("https://www.google.com/"));
            Assert.AreEqual(true, attribute.IsValid("https://www.google.com"));
            Assert.AreEqual(true, attribute.IsValid("https://www.google.com/index.html"));
            Assert.AreEqual(true, attribute.IsValid("https://www.google.com/search?text=aaa"));

            attribute.HostOnly = true;

            Assert.AreEqual(true, attribute.IsValid("http://www.google.com/"));
            Assert.AreEqual(true, attribute.IsValid("https://www.google.com/"));
            Assert.AreEqual(true, attribute.IsValid("https://www.google.com"));
            Assert.AreEqual(false, attribute.IsValid("https://www.google.com/index.html"));
            Assert.AreEqual(false, attribute.IsValid("https://www.google.com/search?text=aaa"));

            attribute.ValidSchemas = new string[] { "https" };

            Assert.AreEqual(false, attribute.IsValid("http://www.google.com/"));
            Assert.AreEqual(false, attribute.IsValid("ftp://www.google.com/"));
            Assert.AreEqual(true, attribute.IsValid("https://www.google.com/"));
        }

        [DataRow(1)]
        [DataRow('c')]
        [DataTestMethod]
        public void IsValidFail(object value)
        {
            var attribute = new MustBeValidUriAttribute();

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