using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ValidationFramework.Test.Valdators
{
    [TestClass]
    public class MustBeBetweenAttributeTest
    {
        #region Public Methods

        [TestMethod]
        public void GetDefaultMessage()
        {
            Assert.AreEqual("Value must be between {0} and {1}.", new MustBeBetweenAttribute(1, 2, true).GetDefaultMessage());
        }

        [TestMethod]
        public void GetDefaultMessageKey()
        {
            Assert.AreEqual("MustBeBetween", new MustBeBetweenAttribute(1, 2, true).GetDefaultMessageKey());
        }

        [TestMethod]
        public void GetParameters()
        {
            CollectionAssert.AreEqual(new object[] { 'b', 'e' }, new MustBeBetweenAttribute('b', 'e', true).MessageParameters.ToList());
            CollectionAssert.AreEqual(new object[] { 10, 20 }, new MustBeBetweenAttribute(10, 20, true).MessageParameters.ToList());
        }

        [DataRow(1, 0, 2, false, true)]
        [DataRow(1d, 0d, 2d, true, true)]
        [DataRow(0f, 0f, 2f, true, true)]
        [DataRow(2f, 0, 2, true, true)]
        [DataRow(0d, 0, 2, false, false)]
        [DataRow(2, 0, 2, false, false)]
        [DataRow(null, 0, 2, true, true)]
        [DataRow("b", "a", "c", false, true)]
        [DataRow("b", "a", "c", true, true)]
        [DataRow("a", "a", "c", true, true)]
        [DataRow("c", "a", "c", true, true)]
        [DataRow("a", "a", "c", false, false)]
        [DataRow("c", "a", "c", false, false)]
        [DataTestMethod]
        public void IsValid(IComparable value, IComparable min, IComparable max, bool inclusive, bool expected)
        {
            var attribute = new MustBeBetweenAttribute(min, max, inclusive);

            Assert.AreEqual(expected, attribute.IsValid(value));
        }

        [TestMethod]
        public void IsValid2()
        {
            Assert.AreEqual(false, new MustBeBetweenAttribute('b', 'e', true).IsValid('f'));
            Assert.AreEqual(true, new MustBeBetweenAttribute('b', 'e', true).IsValid('d'));

            Assert.AreEqual(true, new MustBeBetweenAttribute((byte)1, (byte)10, true).IsValid((byte)9));
            Assert.AreEqual(false, new MustBeBetweenAttribute((byte)1, (byte)10, true).IsValid((byte)19));

            Assert.AreEqual(true, new MustBeBetweenAttribute((double)1, (double)10, true).IsValid((double)9));
            Assert.AreEqual(false, new MustBeBetweenAttribute((double)1, (double)10, false).IsValid((double)19));

            Assert.AreEqual(true, new MustBeBetweenAttribute((float)1, (float)10, true).IsValid((float)9));
            Assert.AreEqual(false, new MustBeBetweenAttribute((float)1, (float)10, true).IsValid((float)19));

            Assert.AreEqual(true, new MustBeBetweenAttribute((long)1, (long)10, true).IsValid((long)9));
            Assert.AreEqual(false, new MustBeBetweenAttribute((long)1, (long)10, true).IsValid((long)19));

            Assert.AreEqual(true, new MustBeBetweenAttribute((int)1, (int)10, true).IsValid((int)9));
            Assert.AreEqual(false, new MustBeBetweenAttribute((int)1, (int)10, true).IsValid((int)19));

            Assert.AreEqual(true, new MustBeBetweenAttribute((sbyte)1, (sbyte)10, true).IsValid((sbyte)9));
            Assert.AreEqual(false, new MustBeBetweenAttribute((sbyte)1, (sbyte)10, true).IsValid((sbyte)19));

            Assert.AreEqual(true, new MustBeBetweenAttribute((short)1, (short)10, true).IsValid((short)9));
            Assert.AreEqual(false, new MustBeBetweenAttribute((short)1, (short)10, true).IsValid((short)19));

            Assert.AreEqual(true, new MustBeBetweenAttribute((uint)1, (uint)10, true).IsValid((uint)9));
            Assert.AreEqual(false, new MustBeBetweenAttribute((uint)1, (uint)10, true).IsValid((uint)19));

            Assert.AreEqual(true, new MustBeBetweenAttribute((ulong)1, (ulong)10, true).IsValid((ulong)9));
            Assert.AreEqual(false, new MustBeBetweenAttribute((ulong)1, (ulong)10, true).IsValid((ulong)19));

            Assert.AreEqual(true, new MustBeBetweenAttribute((ushort)1, (ushort)10, true).IsValid((ushort)9));
            Assert.AreEqual(false, new MustBeBetweenAttribute((ushort)1, (ushort)10, true).IsValid((ushort)19));

            Assert.AreEqual(true, new MustBeBetweenAttribute((decimal)1, (decimal)10, true).IsValid((decimal)9));
            Assert.AreEqual(false, new MustBeBetweenAttribute((decimal)1, (decimal)10, true).IsValid((decimal)19));
        }

        [DataRow(1, 0d, 2, false)]
        [DataRow(1, 0, 2d, false)]
        [DataTestMethod]
        public void IsValidFail(IComparable value, IComparable min, IComparable max, bool inclusive)
        {
            var attribute = new MustBeBetweenAttribute(min, max, inclusive);

            try
            {
                attribute.IsValid(value);

                Assert.Fail();
            }
            catch (ArgumentException ex)
            {
                Assert.AreEqual($"Value must be of type {max.GetType().Name}.", ex.Message);
            }
        }

        #endregion Public Methods
    }
}