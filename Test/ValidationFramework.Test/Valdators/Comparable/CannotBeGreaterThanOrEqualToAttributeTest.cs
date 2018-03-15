using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ValidationFramework.Test.Valdators
{
    [TestClass]
    public class CannotBeGreaterThanOrEqualToAttributeTest
    {
        #region Public Methods

        [TestMethod]
        public void GetDefaultMessage()
        {
            Assert.AreEqual("Value cannot be greater than or equal to {0}.", new CannotBeGreaterThanOrEqualToAttribute(1).GetDefaultMessage());
        }

        [TestMethod]
        public void GetDefaultMessageKey()
        {
            Assert.AreEqual("CannotBeGreaterThanOrEqualTo", new CannotBeGreaterThanOrEqualToAttribute(1).GetDefaultMessageKey());
        }

        [TestMethod]
        public void GetParameters()
        {
            CollectionAssert.AreEqual(new object[] { 9 }, new CannotBeGreaterThanOrEqualToAttribute(9).MessageParameters.ToList());
        }

        [DataRow(null, 1, true)]
        [DataRow(1, 1, !true)]
        [DataRow(1d, 2d, !false)]
        [DataRow(2f, 1f, !true)]
        [DataRow("a", "a", !true)]
        [DataRow("A", "A", !true)]
        [DataRow("a", "A", !false)]
        [DataRow("A", "a", !true)]
        [DataRow('x', 'x', !true)]
        [DataRow('x', 'y', !false)]
        [DataRow('y', 'x', !true)]
        [DataTestMethod]
        public void IsValid(IComparable value, IComparable maxValue, bool expected)
        {
            var attribute = new CannotBeGreaterThanOrEqualToAttribute(maxValue);

            Assert.AreEqual(expected, attribute.IsValid(value));
        }

        [TestMethod]
        public void IsValid2()
        {
            Assert.AreEqual(true, new CannotBeGreaterThanOrEqualToAttribute('b').IsValid(DBNull.Value));

            Assert.AreEqual(!true, new CannotBeGreaterThanOrEqualToAttribute((byte)1).IsValid((byte)9));
            Assert.AreEqual(!true, new CannotBeGreaterThanOrEqualToAttribute((double)1).IsValid((double)9));
            Assert.AreEqual(!true, new CannotBeGreaterThanOrEqualToAttribute((float)1).IsValid((float)9));
            Assert.AreEqual(!true, new CannotBeGreaterThanOrEqualToAttribute((long)1).IsValid((long)9));
            Assert.AreEqual(!true, new CannotBeGreaterThanOrEqualToAttribute((int)1).IsValid((int)9));
            Assert.AreEqual(!true, new CannotBeGreaterThanOrEqualToAttribute((sbyte)1).IsValid((sbyte)9));
            Assert.AreEqual(!true, new CannotBeGreaterThanOrEqualToAttribute((short)1).IsValid((short)9));
            Assert.AreEqual(!true, new CannotBeGreaterThanOrEqualToAttribute((uint)1).IsValid((uint)9));
            Assert.AreEqual(!true, new CannotBeGreaterThanOrEqualToAttribute((ulong)1).IsValid((ulong)9));
            Assert.AreEqual(!true, new CannotBeGreaterThanOrEqualToAttribute((ushort)1).IsValid((ushort)9));
            Assert.AreEqual(!true, new CannotBeGreaterThanOrEqualToAttribute((decimal)1).IsValid((decimal)9));
        }

        [DataRow(1, "a", true)]
        [DataTestMethod]
        public void IsValidFail(IComparable value, IComparable maxValue, bool expected)
        {
            var attribute = new CannotBeGreaterThanOrEqualToAttribute(maxValue);

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