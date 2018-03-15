using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ValidationFramework.Test.Exceptions
{
    [TestClass]
    public class ValidationExceptionTest
    {
        [TestMethod]
        public void TestException()
        {
            ValidationMessageCollection messages = new ValidationMessageCollection();
            IValidatable source = new Example();

            messages.Add(new ValidationMessage("messageKey", "defaultMessage: {0}{1}", new string[] { "X", "Y" }, source, "PropertyA", ValidationLevel.Warning, ValidationContext.Default, 5));

            try
            {
                throw new ValidationException(messages);
            }
            catch (ValidationException ex)
            {
                Assert.IsNotNull(ex);
                Assert.AreEqual(1, ex.ValidaitonMessages.Count);
                Assert.AreEqual("defaultMessage: XY", ex.ValidaitonMessages[0].Message);
                Assert.AreEqual(source, ex.ValidaitonMessages[0].ValidationSource);
                Assert.AreEqual("PropertyA", ex.ValidaitonMessages[0].PropertyName);
                Assert.AreEqual(ValidationLevel.Warning, ex.ValidaitonMessages[0].ValidationLevel);
                Assert.AreEqual(5, ex.ValidaitonMessages[0].ValidationPriority);
                Assert.AreEqual(ValidationContext.Default, ex.ValidaitonMessages[0].ValidationContext);
            }
        }
    }
}