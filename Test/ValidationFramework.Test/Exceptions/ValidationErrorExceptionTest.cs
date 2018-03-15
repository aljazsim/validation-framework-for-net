using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ValidationFramework.Test.Exceptions
{
    [TestClass]
    public class ValidationErrorExceptionTest
    {
        [TestMethod]
        public void TestException()
        {
            try
            {
                throw new ValidationErrorException("test message", typeof(string), typeof(Validatable), "prop", new Exception());
            }
            catch (ValidationErrorException ex)
            {
                Assert.IsNotNull(ex);
                Assert.AreEqual("test message", ex.Message);
                Assert.AreEqual(typeof(string), ex.ValidationAttributeType);
                Assert.AreEqual(typeof(Validatable), ex.ValidationSourceType);
                Assert.AreEqual("prop", ex.PropertyName);
                Assert.AreEqual(typeof(Exception), ex.InnerException.GetType());
            }
        }
    }
}