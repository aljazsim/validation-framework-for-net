using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ValidationFramework.Test
{
    [TestClass]
    public class ValidationMessageCollectionTest
    {
        [TestMethod]
        public void TestValidationMessageCollection()
        {
            var message1 = new ValidationMessage("message1", "Test1 {0}.", new object[] { 5 }, new Example(), "Property1", ValidationLevel.Error, "Validation context 2", 0);
            var message2 = new ValidationMessage("message2", "Test2 {0}.", new object[] { 5 }, new Example(), "Property2", ValidationLevel.Error, "Validation context 2", 0);
            var message3 = new ValidationMessage("message3", "Test3 {0}.", new object[] { 5 }, new Example(), "Property3", ValidationLevel.Error, "Validation context 2", 0);
            var message4 = new ValidationMessage("message4", "Test4 {0}.", new object[] { 5 }, new Example(), "Property4", ValidationLevel.Info, "Validation context 2", 0);
            var message5 = new ValidationMessage("message5", "Test5 {0}.", new object[] { 5 }, message4.ValidationSource, "Property4", ValidationLevel.Warning, "Validation context 2", 0);
            var messages = new ValidationMessageCollection();

            messages.Add(message1);
            messages.Add(message2);
            messages.Add(message3);
            messages.Insert(messages.Count, message4);

            Assert.AreEqual(message1, messages[0]);
            Assert.AreEqual(message2, messages[1]);
            Assert.AreEqual(message3, messages[2]);
            Assert.AreEqual(message4, messages[3]);

            Assert.AreEqual(1, messages[message1.ValidationSource, "Property1", "Validation context 2"].Count);
            Assert.AreEqual(message1, messages[message1.ValidationSource, "Property1", "Validation context 2"][0]);
            Assert.AreEqual(1, messages[message3.ValidationSource, message3.PropertyName, "Validation context 2"].Count);
            Assert.AreEqual(message3, messages[message3.ValidationSource, message3.PropertyName, "Validation context 2"][0]);

            Assert.AreEqual(0, messages[null as IValidatable].Count);
            Assert.AreEqual(0, messages[null as IValidatable, null as string].Count);
            Assert.AreEqual(0, messages[null as IValidatable, "aaa" as string].Count);
            Assert.AreEqual(0, messages[new Example() as IValidatable, null as string].Count);

            Assert.IsFalse(messages.Contains(null));
            Assert.IsTrue(messages.Contains(message3));

            messages.Add(message5);

            Assert.AreEqual(4, messages.Count);
            Assert.AreEqual(message5, messages.Last());

            messages[3] = message4;

            Assert.AreEqual(4, messages.Count);
            Assert.AreEqual(message1, messages[0]);
            Assert.AreEqual(message2, messages[1]);
            Assert.AreEqual(message3, messages[2]);
            Assert.AreEqual(message4, messages[3]);

            var validationSource = new Example();
            var message6 = new ValidationMessage("message1", "Test1 {0}.", new object[] { 7 }, validationSource, "Property1", ValidationLevel.Error, null, 0);
            var message7 = new ValidationMessage("message2", "Test2 {0}.", new object[] { 8 }, validationSource, "Property1", ValidationLevel.Error, null, 0);
            var message8 = new ValidationMessage("message3", "Test3 {0}.", new object[] { 9 }, validationSource, "Property2", ValidationLevel.Error, "Validation context 2", 0);

            messages.Add(message6);
            messages.Add(message7);
            messages.Insert(messages.Count, message8);

            var mergedMessage = messages.Merge(validationSource, "Property1");

            Assert.AreEqual("Test1 7.\r\nTest2 8.", mergedMessage.Message);
            Assert.AreEqual(ValidationLevel.Error, mergedMessage.ValidationLevel);

            MergedValidationMessage.GetMergedMessages = x => string.Join(" * ", x);

            var mergedMessage2 = messages.Merge(validationSource, "Property1");

            Assert.AreEqual("Test1 7. * Test2 8.", mergedMessage2.Message);
            Assert.AreEqual(ValidationLevel.Error, mergedMessage2.ValidationLevel);

            var mergedMessage4 = messages.Merge(validationSource, "Property2");

            Assert.AreEqual("Test3 9.", mergedMessage4.Message);
            Assert.AreEqual(ValidationLevel.Error, mergedMessage4.ValidationLevel);

            var mergedMessage5 = messages.Merge(validationSource);

            Assert.AreEqual("Test1 7. * Test2 8. * Test3 9.", mergedMessage5.Message);
            Assert.AreEqual(ValidationLevel.Error, mergedMessage5.ValidationLevel);

            MergedValidationMessage.GetMergedMessages = null;
        }
    }
}