using Microsoft.VisualStudio.TestTools.UnitTesting;
using GeneticStartupsWindows;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeneticStartupsWindows.Tests
{
    [TestClass()]
    public class UIAritmeticsTests
    {
        [TestMethod()]
        public void calculateTableLocationCenteredInContainerTest()
        {
            System.Drawing.Size tableSize = new System.Drawing.Size(50, 50);
            System.Drawing.Size containerSize = new System.Drawing.Size(100, 100);
            UIAritmetics uiAritmetics = new UIAritmetics();
            System.Drawing.Point tableLocation = uiAritmetics.calculateTableLocationCenteredInContainer(tableSize, containerSize);
            Assert.AreEqual(25, tableLocation.X);
        }
    }
}