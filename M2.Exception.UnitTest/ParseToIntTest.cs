using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Task2;

namespace M2.Exception.UnitTest
{
    [TestClass]
    public class ParseToIntTest
    {
        [TestMethod]
        public void ParseStringToInt_Success()
        {
            var strNumber = "1234";
            var result = ParseNumber.Parse(strNumber);

            Assert.AreEqual(1234, result);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void ThrowIfCannotParse()
        {
            ParseNumber.Parse(null);
        }

        [TestMethod]
        public void ParseNegativeStringToInt_Success()
        {
            var strNumber = "-1000";
            var result = ParseNumber.Parse(strNumber);

            Assert.AreEqual(-1000, result);
        }

        [TestMethod]
        [ExpectedException(typeof(OverflowException))]
        public void ThrowOverflowException()
        {
            ParseNumber.Parse("111111111111111111111");
        }

        [TestMethod]
        [ExpectedException(typeof(FormatException))]
        public void ThrowFormatException()
        {
            ParseNumber.Parse("1b1");
        }

        [TestMethod]
        [ExpectedException(typeof(OverflowException))]
        public void ThrowOverflowExceptionBound()
        {
            ParseNumber.Parse("2147483648");
        }
    }
}