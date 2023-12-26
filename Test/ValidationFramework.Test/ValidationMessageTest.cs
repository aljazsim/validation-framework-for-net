using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ValidationFramework.Test
{
    [TestClass]
    public class ValidationMessageTest
    {
        [TestMethod]
        public void TestValidationMessage()
        {
            string messageKey = "message 1";
            string defaultMessage = "Validation error message: {0}, {1}.";
            IEnumerable<object> messageArguments = new object[] { 5, 10 };
            IValidatable validationSource = new Example();
            string propertyName = "FullName";
            ValidationLevel validationLevel = ValidationLevel.Info;
            string validationContext = ValidationContext.Default;
            int validationPriority = 6;

            var message1 = new ValidationMessage(messageKey, defaultMessage, messageArguments, validationSource, propertyName, validationLevel, validationContext, validationPriority);
            var message2 = new ValidationMessage(messageKey, defaultMessage, messageArguments, validationSource, propertyName, validationLevel, validationContext, validationPriority);

            Assert.AreEqual("Validation error message: 5, 10.", message1.Message);
            Assert.AreEqual(propertyName, message1.PropertyName);
            Assert.AreEqual(validationContext, message1.ValidationContext);
            Assert.AreEqual(validationLevel, message1.ValidationLevel);
            Assert.AreEqual(validationPriority, message1.ValidationPriority);
            Assert.AreEqual(validationSource, message1.ValidationSource);
            Assert.AreEqual("Example.FullName (message: \"Validation error message: 5, 10.\", level: Info, context: default, priority: 6)", message1.ToString());

            Assert.AreEqual(message1.Message, message2.Message);
            Assert.AreEqual(message1.PropertyName, message2.PropertyName);
            Assert.AreEqual(message1.ValidationContext, message2.ValidationContext);
            Assert.AreEqual(message1.ValidationLevel, message2.ValidationLevel);
            Assert.AreEqual(message1.ValidationPriority, message2.ValidationPriority);
            Assert.AreEqual(message1.ValidationSource, message2.ValidationSource);

            Assert.AreEqual(true, message1 == message2);
            Assert.AreEqual(0, message1.CompareTo(message2));
            Assert.AreEqual(true, message1.Equals(message2));
            Assert.AreEqual(true, message1.Equals(message2 as object));
            Assert.IsTrue(message1.GetHashCode() != 0);

            ValidationMessage.GetLocalizedMessage = x => "Napaka pri validaciji: {0}, {1}.";

            var message3 = new ValidationMessage(messageKey, defaultMessage, messageArguments, validationSource, propertyName, validationLevel, validationContext, 8);

            Assert.AreEqual("Napaka pri validaciji: 5, 10.", message3.Message);
            Assert.AreEqual(propertyName, message3.PropertyName);
            Assert.AreEqual(validationContext, message3.ValidationContext);
            Assert.AreEqual(validationLevel, message3.ValidationLevel);
            Assert.AreEqual(8, message3.ValidationPriority);
            Assert.AreEqual(validationSource, message3.ValidationSource);

            Assert.AreEqual(true, message1 != message3);
            Assert.AreEqual(-1, message1.CompareTo(message3));
            Assert.AreEqual(false, message1.Equals(message3));
            Assert.AreEqual(false, message1.Equals(message3 as object));
            Assert.AreEqual(false, message1.Equals(null));
            Assert.AreEqual(false, message1.Equals(null as object));
            Assert.AreEqual(false, message1 == null);
            Assert.AreEqual(false, null == message1);

            ValidationMessage.GetLocalizedMessage = x => "Napaka pri validaciji: {5}.";

            var message4 = new ValidationMessage(messageKey, "Test {0}", messageArguments, validationSource, propertyName, validationLevel, "new item", 8);

            Assert.AreEqual("Napaka pri validaciji: {5}.", message4.Message);
            Assert.IsTrue(message4.GetHashCode() != 0);

            ValidationMessage.GetLocalizedMessage = null;
        }
    }
}