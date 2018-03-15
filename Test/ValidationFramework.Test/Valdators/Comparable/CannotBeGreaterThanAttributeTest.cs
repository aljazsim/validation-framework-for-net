using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ValidationFramework.Test.Valdators
{
    [TestClass]
    public class CannotBeGreaterThanAttributeTest
    {
        #region Public Methods

        [TestMethod]
        public void GetDefaultMessage()
        {
            Assert.AreEqual("Value cannot be greater than {0}.", new CannotBeGreaterThanAttribute(1).GetDefaultMessage());
        }

        [TestMethod]
        public void GetDefaultMessageKey()
        {
            Assert.AreEqual("CannotBeGreaterThan", new CannotBeGreaterThanAttribute(1).GetDefaultMessageKey());
        }

        [TestMethod]
        public void GetParameters()
        {
            CollectionAssert.AreEqual(new object[] { 9 }, new CannotBeGreaterThanAttribute(9).MessageParameters.ToList());
        }

        [DataRow(null, 1, true)]
        [DataRow(1, 1, !false)]
        [DataRow(1d, 2d, !false)]
        [DataRow(2f, 1f, !true)]
        [DataRow("a", "a", !false)]
        [DataRow("A", "A", !false)]
        [DataRow("a", "A", !false)]
        [DataRow("A", "a", !true)]
        [DataRow('x', 'x', !false)]
        [DataRow('x', 'y', !false)]
        [DataRow('y', 'x', !true)]
        [DataTestMethod]
        public void IsValid(IComparable value, IComparable maxValue, bool expected)
        {
            var attribute = new CannotBeGreaterThanAttribute(maxValue);

            Assert.AreEqual(expected, attribute.IsValid(value));
        }

        [TestMethod]
        public void IsValid2()
        {
            Assert.AreEqual(true, new CannotBeGreaterThanAttribute('b').IsValid(DBNull.Value));

            Assert.AreEqual(!true, new CannotBeGreaterThanAttribute((byte)1).IsValid((byte)9));
            Assert.AreEqual(!true, new CannotBeGreaterThanAttribute((double)1).IsValid((double)9));
            Assert.AreEqual(!true, new CannotBeGreaterThanAttribute((float)1).IsValid((float)9));
            Assert.AreEqual(!true, new CannotBeGreaterThanAttribute((long)1).IsValid((long)9));
            Assert.AreEqual(!true, new CannotBeGreaterThanAttribute((int)1).IsValid((int)9));
            Assert.AreEqual(!true, new CannotBeGreaterThanAttribute((sbyte)1).IsValid((sbyte)9));
            Assert.AreEqual(!true, new CannotBeGreaterThanAttribute((short)1).IsValid((short)9));
            Assert.AreEqual(!true, new CannotBeGreaterThanAttribute((uint)1).IsValid((uint)9));
            Assert.AreEqual(!true, new CannotBeGreaterThanAttribute((ulong)1).IsValid((ulong)9));
            Assert.AreEqual(!true, new CannotBeGreaterThanAttribute((ushort)1).IsValid((ushort)9));
            Assert.AreEqual(!true, new CannotBeGreaterThanAttribute((decimal)1).IsValid((decimal)9));
        }

        [DataRow(1, "a", true)]
        [DataTestMethod]
        public void IsValidFail(IComparable value, IComparable maxValue, bool expected)
        {
            var attribute = new CannotBeGreaterThanAttribute(maxValue);

            try
            {
                attribute.IsValid(value);

                Assert.Fail();
            }
            catch (FormatException ex)
            {
                Assert.AreEqual("Input string was not in a correct format.", ex.Message);
            }
        }

        #endregion Public Methods
    }
}