using System.Collections.Generic;
using FileSysClassLibrary;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace FyleSystemVisitor.Tests
{
    [TestClass]
    public class Tests
    {
        [TestMethod]
        public void SearchFromIntelFolder()
        {
            // Arrange
            var a = "C:\\Intel";


            var expected = new List<string>
            {
                "C:\\Intel\\gp",
                "C:\\Intel\\Logs",
                "IntelCpHDCPSvc.log",
                "IntelCPHS.log",
                "IntelGFXCoin.log"
            };
            //Act
            var actual = new List<string>();
            var FSM = new FileSystemVisitor(a);
            foreach (string VARIABLE in FSM)
                actual.Add(VARIABLE);
            //Assert
            for (var i = 0; i < actual.Count; i++)
                Assert.AreEqual(actual[i], expected[i]);
        }

        [TestMethod]
        public void StarEventInvoked()
        {
            var a = "C:\\Intel";
            var expected = 1;

            var actual = 0;
            var FSM = new FileSystemVisitor(a);
            FSM.Start += () => actual++;
            foreach (var VARIABLE in FSM)
            {
            }

            Assert.AreEqual(actual, expected);
        }

        [TestMethod]
        public void DirectoryFoundEventInvoked()
        {
            var a = "C:\\Intel";
            var expected = 1;

            var actual = 0;
            var FSM = new FileSystemVisitor(a);
            FSM.Finish += () => actual++;
            FSM.DirectoryFound += found;

            foreach (var VARIABLE in FSM)
            {
            }

            void found(object sender, DirectoryEventArgs arg)
            {
                if (arg.Directory.Name == "Logs")
                    arg.CanRun = false;
            }

            Assert.AreEqual(actual, expected);
        }
    }
}