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
            UIAritmetics uiAritmetics = new UIAritmetics();
            System.Drawing.Size tableSize = new System.Drawing.Size(50, 50);
            System.Drawing.Size containerSize = new System.Drawing.Size(100, 100);
            System.Drawing.Point tableLocation = uiAritmetics.calculateTableLocationCenteredInContainer(tableSize, containerSize);
            Assert.AreEqual(25, tableLocation.X);
        }

        [TestMethod()]
        public void calculateClientSizeBiggerTableTest()
        {
            UIAritmetics uiAritmetics = new UIAritmetics();
            System.Drawing.Size tableSize = new System.Drawing.Size(100, 100);
            System.Drawing.Size menuStripSize = new System.Drawing.Size(100, 15);
            int cellHeight = 10;
            int cellWidth = 10;
            System.Drawing.Size button1Size = new System.Drawing.Size(20, 15);
            System.Drawing.Size button2Size = new System.Drawing.Size(20, 15);
            System.Drawing.Size clientSize = uiAritmetics.calculateClientSize(tableSize, menuStripSize, cellHeight, cellWidth, button1Size, button2Size);
            Assert.AreEqual(120, clientSize.Width);
        }

        [TestMethod()]
        public void calculateClientSizeSmallerTableTest()
        {
            UIAritmetics uiAritmetics = new UIAritmetics();
            System.Drawing.Size tableSize = new System.Drawing.Size(100, 100);
            System.Drawing.Size menuStripSize = new System.Drawing.Size(100, 15);
            int cellHeight = 10;
            int cellWidth = 10;
            System.Drawing.Size button1Size = new System.Drawing.Size(45, 15);
            System.Drawing.Size button2Size = new System.Drawing.Size(45, 15);
            System.Drawing.Size clientSize = uiAritmetics.calculateClientSize(tableSize, menuStripSize, cellHeight, cellWidth, button1Size, button2Size);
            Assert.AreEqual(150, clientSize.Width);
        }
    }

}