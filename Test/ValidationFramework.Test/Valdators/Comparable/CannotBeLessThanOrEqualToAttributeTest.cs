using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ValidationFramework.Test.Valdators
{
    [TestClass]
    public class CannotBeLessThanOrEqualToAttributeTest
    {
        #region Public Methods

        [TestMethod]
        public void GetDefaultMessage()
        {
            Assert.AreEqual("Value cannot be less than or equal to {0}.", new CannotBeLessThanOrEqualToAttribute(1).GetDefaultMessage());
        }

        [TestMethod]
        public void GetDefaultMessageKey()
        {
            Assert.AreEqual("CannotBeLessThanOrEqualTo", new CannotBeLessThanOrEqualToAttribute(1).GetDefaultMessageKey());
        }

        [TestMethod]
        public void GetParameters()
        {
            CollectionAssert.AreEqual(new object[] { 9 }, new CannotBeLessThanOrEqualToAttribute(9).MessageParameters.ToList());
        }

        [DataRow(null, 1, true)]
        [DataRow(1, 1, !true)]
        [DataRow(2d, 1d, !false)]
        [DataRow(1f, 2f, !true)]
        [DataRow("a", "a", !true)]
        [DataRow("A", "A", !true)]
        [DataRow("A", "a", !false)]
        [DataRow("a", "A", !true)]
        [DataRow('x', 'x', !true)]
        [DataRow('y', 'x', !false)]
        [DataRow('x', 'y', !true)]
        [DataTestMethod]
        public void IsValid(IComparable value, IComparable maxValue, bool expected)
        {
            var attribute = new CannotBeLessThanOrEqualToAttribute(maxValue);

            Assert.AreEqual(expected, attribute.IsValid(value));
        }

        [TestMethod]
        public void IsValid2()
        {
            Assert.AreEqual(true, new CannotBeLessThanOrEqualToAttribute('b').IsValid(DBNull.Value));

            Assert.AreEqual(true, new CannotBeLessThanOrEqualToAttribute((byte)1).IsValid((byte)9));
            Assert.AreEqual(true, new CannotBeLessThanOrEqualToAttribute((double)1).IsValid((double)9));
            Assert.AreEqual(true, new CannotBeLessThanOrEqualToAttribute((float)1).IsValid((float)9));
            Assert.AreEqual(true, new CannotBeLessThanOrEqualToAttribute((long)1).IsValid((long)9));
            Assert.AreEqual(true, new CannotBeLessThanOrEqualToAttribute((int)1).IsValid((int)9));
            Assert.AreEqual(true, new CannotBeLessThanOrEqualToAttribute((sbyte)1).IsValid((sbyte)9));
            Assert.AreEqual(true, new CannotBeLessThanOrEqualToAttribute((short)1).IsValid((short)9));
            Assert.AreEqual(true, new CannotBeLessThanOrEqualToAttribute((uint)1).IsValid((uint)9));
            Assert.AreEqual(true, new CannotBeLessThanOrEqualToAttribute((ulong)1).IsValid((ulong)9));
            Assert.AreEqual(true, new CannotBeLessThanOrEqualToAttribute((ushort)1).IsValid((ushort)9));
            Assert.AreEqual(true, new CannotBeLessThanOrEqualToAttribute((decimal)1).IsValid((decimal)9));
        }

        [DataRow(1, "a", true)]
        [DataTestMethod]
        public void IsValidFail(IComparable value, IComparable maxValue, bool expected)
        {
            var attribute = new CannotBeLessThanOrEqualToAttribute(maxValue);

            try
            {
                attribute.IsValid(value);

                Assert.Fail();
            }
            catch (FormatException ex)
            {
                Assert.AreEqual("The input string 'a' was not in a correct format.", ex.Message);
            }

            #endregion Public Methods
        }
    }
}