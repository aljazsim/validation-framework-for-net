using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ValidationFramework.Test.Valdators
{
    [TestClass]
    public class CannotBeEqualToAttributeTest
    {
        #region Public Methods

        [TestMethod]
        public void GetDefaultMessage()
        {
            Assert.AreEqual("Value cannot be equal to {0}.", new CannotBeEqualToAttribute(1).GetDefaultMessage());
        }

        [TestMethod]
        public void GetDefaultMessageKey()
        {
            Assert.AreEqual("CannotBeEqualTo", new CannotBeEqualToAttribute(1).GetDefaultMessageKey());
        }

        [TestMethod]
        public void GetParameters()
        {
            CollectionAssert.AreEqual(new object[] { 9 }, new CannotBeEqualToAttribute(9).MessageParameters.ToList());
        }

        [DataRow(1, 1, !true)]
        [DataRow(1, 2, !false)]
        [DataRow(0d, 0d, !true)]
        [DataRow(-1f, 1f, !false)]
        [DataRow("a", "a", !true)]
        [DataRow("A", "A", !true)]
        [DataRow("a", "A", !false)]
        [DataRow('x', 'x', !true)]
        [DataRow('x', 'y', !false)]
        [DataTestMethod]
        public void IsValid(IComparable value, IComparable comparedValue, bool expected)
        {
            var attribute = new CannotBeEqualToAttribute(comparedValue);

            Assert.AreEqual(expected, attribute.IsValid(value));
        }

        [DataTestMethod]
        public void IsValid2()
        {
            var attribute = new CannotBeEqualToAttribute(5);

            Assert.AreEqual(!false, attribute.IsValid(null));
            Assert.AreEqual(!false, attribute.IsValid(DBNull.Value));

            attribute = new CannotBeEqualToAttribute(null);

            Assert.AreEqual(!false, attribute.IsValid(5));
            Assert.AreEqual(!true, attribute.IsValid(null));
            Assert.AreEqual(!false, attribute.IsValid(DBNull.Value));

            attribute = new CannotBeEqualToAttribute(DBNull.Value);

            Assert.AreEqual(!false, attribute.IsValid(5));
            Assert.AreEqual(!false, attribute.IsValid(null));
            Assert.AreEqual(!true, attribute.IsValid(DBNull.Value));
        }

        [DataRow(1, "a", true)]
        [DataTestMethod]
        public void IsValidFail(IComparable value, IComparable comparedValue, bool expected)
        {
            var attribute = new CannotBeEqualToAttribute(comparedValue);

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