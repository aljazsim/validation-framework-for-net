using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ValidationFramework.Test.Valdators
{
    [TestClass]
    public class MustBeLessThanAttributeTest
    {
        #region Public Methods

        [TestMethod]
        public void GetDefaultMessage()
        {
            Assert.AreEqual("Value must be less than {0}.", new MustBeLessThanAttribute(1).GetDefaultMessage());
        }

        [TestMethod]
        public void GetDefaultMessageKey()
        {
            Assert.AreEqual("MustBeLessThan", new MustBeLessThanAttribute(1).GetDefaultMessageKey());
        }

        [TestMethod]
        public void GetParameters()
        {
            CollectionAssert.AreEqual(new object[] { 9 }, new MustBeLessThanAttribute(9).MessageParameters.ToList());
        }

        [DataRow(null, 1, true)]
        [DataRow(1, 1, false)]
        [DataRow(2d, 1d, false)]
        [DataRow(1f, 2f, true)]
        [DataRow("a", "a", false)]
        [DataRow("A", "A", false)]
        [DataRow("A", "a", false)]
        [DataRow("a", "A", true)]
        [DataRow('x', 'x', false)]
        [DataRow('y', 'x', false)]
        [DataRow('x', 'y', true)]
        [DataTestMethod]
        public void IsValid(IComparable value, IComparable maxValue, bool expected)
        {
            var attribute = new MustBeLessThanAttribute(maxValue);

            Assert.AreEqual(expected, attribute.IsValid(value));
        }

        [TestMethod]
        public void IsValid2()
        {
            Assert.AreEqual(true, new MustBeLessThanAttribute('b').IsValid(DBNull.Value));

            Assert.AreEqual(!true, new MustBeLessThanAttribute((byte)1).IsValid((byte)9));
            Assert.AreEqual(!true, new MustBeLessThanAttribute((double)1).IsValid((double)9));
            Assert.AreEqual(!true, new MustBeLessThanAttribute((float)1).IsValid((float)9));
            Assert.AreEqual(!true, new MustBeLessThanAttribute((long)1).IsValid((long)9));
            Assert.AreEqual(!true, new MustBeLessThanAttribute((int)1).IsValid((int)9));
            Assert.AreEqual(!true, new MustBeLessThanAttribute((sbyte)1).IsValid((sbyte)9));
            Assert.AreEqual(!true, new MustBeLessThanAttribute((short)1).IsValid((short)9));
            Assert.AreEqual(!true, new MustBeLessThanAttribute((uint)1).IsValid((uint)9));
            Assert.AreEqual(!true, new MustBeLessThanAttribute((ulong)1).IsValid((ulong)9));
            Assert.AreEqual(!true, new MustBeLessThanAttribute((ushort)1).IsValid((ushort)9));
            Assert.AreEqual(!true, new MustBeLessThanAttribute((decimal)1).IsValid((decimal)9));
        }

        [DataRow(1, "a", true)]
        [DataTestMethod]
        public void IsValidFail(IComparable value, IComparable maxValue, bool expected)
        {
            var attribute = new MustBeLessThanAttribute(maxValue);

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