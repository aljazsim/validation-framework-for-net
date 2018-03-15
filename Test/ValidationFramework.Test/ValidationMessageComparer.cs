using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ValidationFramework.Test
{
    [TestClass]
    public class ValidationMessageComparerTest
    {
        [TestMethod]
        public void Test()
        {
            var validationSource = new Example();
            var comparer = new ValidationMessageComparer();
            var message1 = new ValidationMessage("a", "message", null, validationSource, "PropertyA", ValidationLevel.Info, null, 0);
            var message2 = new ValidationMessage("a", "message", null, validationSource, "PropertyA", ValidationLevel.Info, null, 0);

            Assert.AreEqual(0, comparer.Compare(message1, message2));

            message1 = new ValidationMessage("a", "essage", null, validationSource, "PropertyA", ValidationLevel.Info, null, 0);
            message2 = new ValidationMessage("a", "message", null, validationSource, "PropertyA", ValidationLevel.Info, null, 0);

            Assert.AreEqual(-1, comparer.Compare(message1, message2));

            message1 = new ValidationMessage("a", "message", null, validationSource, "PropertyA", ValidationLevel.Info, null, 0);
            message2 = new ValidationMessage("b", "message", null, validationSource, "PropertyA", ValidationLevel.Info, null, 0);

            Assert.AreEqual(0, comparer.Compare(message1, message2));

            message1 = new ValidationMessage("a", "message", null, validationSource, "PropertyA", ValidationLevel.Info, null, 0);
            message2 = new ValidationMessage("a", "message", null, validationSource, "PropertyB", ValidationLevel.Info, null, 0);

            Assert.AreEqual(-1, comparer.Compare(message1, message2));

            message1 = new ValidationMessage("a", "message", null, validationSource, "PropertyA", ValidationLevel.Error, null, 0);
            message2 = new ValidationMessage("a", "message", null, validationSource, "PropertyA", ValidationLevel.Info, null, 0);

            Assert.AreEqual(1, comparer.Compare(message1, message2));

            Assert.AreEqual(1, comparer.Compare(message1, null));
            Assert.AreEqual(-1, comparer.Compare(null, message2));
            Assert.AreEqual(0, comparer.Compare(null, null));

            message1 = new ValidationMessage("a", "message", null, new Example(), "PropertyA", ValidationLevel.Info, null, 0);
            message2 = new ValidationMessage("a", "message", null, new Example2(), "PropertyA", ValidationLevel.Info, null, 0);

            Assert.AreEqual(-1, comparer.Compare(message1, message2));

            message1 = new ValidationMessage("a", "message", null, validationSource, "PropertyA", ValidationLevel.Info, null, 0);
            message2 = new ValidationMessage("a", "message", null, validationSource, "PropertyA", ValidationLevel.Info, "new", 0);

            Assert.AreEqual(-1, comparer.Compare(message1, message2));

            message1 = new ValidationMessage("a", "message", null, validationSource, "PropertyA", ValidationLevel.Info, "new", 0);
            message2 = new ValidationMessage("a", "message", null, validationSource, "PropertyA", ValidationLevel.Info, null, 0);

            Assert.AreEqual(1, comparer.Compare(message1, message2));

            message1 = new ValidationMessage("a", "message", null, validationSource, "PropertyA", ValidationLevel.Info, null, 1);
            message2 = new ValidationMessage("a", "message", null, validationSource, "PropertyA", ValidationLevel.Info, null, 0);

            Assert.AreEqual(1, comparer.Compare(message1, message2));
            Assert.AreEqual(message1.GetHashCode(), comparer.GetHashCode(message1));
        }
    }
}