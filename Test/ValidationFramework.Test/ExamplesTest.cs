using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ValidationFramework.Test
{
    [TestClass]
    public class ExamplesTest
    {
        [TestMethod]
        public void TestExample2()
        {
            Example2 example;

            example = new Example2();
            example.Name = null;

            Assert.AreEqual(1, example.Validate().Count);
            Assert.AreEqual("Value cannot be null.", example.Validate()[0].Message);
            Assert.AreEqual(nameof(example.Name), example.Validate()[0].PropertyName);
            Assert.AreEqual(null, example.Validate()[0].ValidationContext);
            Assert.AreEqual(ValidationLevel.Error, example.Validate()[0].ValidationLevel);
            Assert.AreEqual(0, example.Validate()[0].ValidationPriority);
            Assert.AreEqual(example, example.Validate()[0].ValidationSource);
            Assert.IsFalse(example.IsValid());
            Assert.IsFalse(example.IsValid(nameof(example.Name)));
        }

        [TestMethod]
        public void TestExample3()
        {
            Example3 example;

            example = new Example3();
            example.Name = null;

            try
            {
                example.Validate();

                Assert.Fail();
            }
            catch (ValidationErrorException ex)
            {
                Assert.AreEqual("Unhandeled validation exception occured.", ex.Message);
            }
        }
    }
}