using System;
using System.CodeDom;
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
    }
}
