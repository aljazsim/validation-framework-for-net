using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ValidationFramework.Test.Valdators
{
    [TestClass]
    public class MustBeGreaterThanOrEqualToAttributeTest
    {
        #region Public Methods

        [TestMethod]
        public void GetDefaultMessage()
        {
            Assert.AreEqual("Value must be greater than or equal to {0}.", new MustBeGreaterThanOrEqualToAttribute(1).GetDefaultMessage());
        }

        [TestMethod]
        public void GetDefaultMessageKey()
        {
            Assert.AreEqual("MustBeGreaterThanOrEqualTo", new MustBeGreaterThanOrEqualToAttribute(1).GetDefaultMessageKey());
        }

        [TestMethod]
        public void GetParameters()
        {
            CollectionAssert.AreEqual(new object[] { 9 }, new MustBeGreaterThanOrEqualToAttribute(9).MessageParameters.ToList());
        }

        [DataRow(null, 1, true)]
        [DataRow(1, 1, true)]
        [DataRow(1d, 2d, false)]
        [DataRow(2f, 1f, true)]
        [DataRow("a", "a", true)]
        [DataRow("A", "A", true)]
        [DataRow("a", "A", false)]
        [DataRow("A", "a", true)]
        [DataRow('x', 'x', true)]
        [DataRow('x', 'y', false)]
        [DataRow('y', 'x', true)]
        [DataTestMethod]
        public void IsValid(IComparable value, IComparable maxValue, bool expected)
        {
            var attribute = new MustBeGreaterThanOrEqualToAttribute(maxValue);

            Assert.AreEqual(expected, attribute.IsValid(value));
        }

        [TestMethod]
        public void IsValid2()
        {
            Assert.AreEqual(true, new MustBeGreaterThanOrEqualToAttribute('b').IsValid(DBNull.Value));

            Assert.AreEqual(true, new MustBeGreaterThanOrEqualToAttribute((byte)1).IsValid((byte)9));
            Assert.AreEqual(true, new MustBeGreaterThanOrEqualToAttribute((double)1).IsValid((double)9));
            Assert.AreEqual(true, new MustBeGreaterThanOrEqualToAttribute((float)1).IsValid((float)9));
            Assert.AreEqual(true, new MustBeGreaterThanOrEqualToAttribute((long)1).IsValid((long)9));
            Assert.AreEqual(true, new MustBeGreaterThanOrEqualToAttribute((int)1).IsValid((int)9));
            Assert.AreEqual(true, new MustBeGreaterThanOrEqualToAttribute((sbyte)1).IsValid((sbyte)9));
            Assert.AreEqual(true, new MustBeGreaterThanOrEqualToAttribute((short)1).IsValid((short)9));
            Assert.AreEqual(true, new MustBeGreaterThanOrEqualToAttribute((uint)1).IsValid((uint)9));
            Assert.AreEqual(true, new MustBeGreaterThanOrEqualToAttribute((ulong)1).IsValid((ulong)9));
            Assert.AreEqual(true, new MustBeGreaterThanOrEqualToAttribute((ushort)1).IsValid((ushort)9));
            Assert.AreEqual(true, new MustBeGreaterThanOrEqualToAttribute((decimal)1).IsValid((decimal)9));
        }

        [DataRow(1, "a", true)]
        [DataTestMethod]
        public void IsValidFail(IComparable value, IComparable maxValue, bool expected)
        {
            var attribute = new MustBeGreaterThanOrEqualToAttribute(maxValue);

            try
            {
                attribute.IsValid(value);

                Assert.Fail();
            }
            catch (FormatException ex)
            {
                Assert.AreEqual("The input string 'a' was not in a correct format.", ex.Message);
            }
        }

        #endregion Public Methods
    }
}