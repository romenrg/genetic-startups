using Microsoft.VisualStudio.TestTools.UnitTesting;
using GeneticStartupsWindows;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeneticStartupsWindows.Tests
{
    [TestClass()]
    public class GeneticsTests
    {
        [TestMethod()]
        public void createBoardFirstQuarterDistributionTest()
        {
            Genetics genetics = new Genetics(20, 20);
            genetics.createBoard();
            int totalEmtyCells = 0, totalAdvisorCells = 0;
            int percentageEmtyCells = 0, percentageAdvisorCells = 0;
            for (int i=0; i < genetics.matrix.GetLength(0) / 4; i++)
            {
                for (int j = 0; j < genetics.matrix.GetLength(0); j++)
                {
                    if (genetics.matrix[i, j] == Genetics.Actions.None)
                    {
                        totalEmtyCells++;
                    }
                    else if (genetics.matrix[i, j] == Genetics.Actions.Advisor)
                    {
                        totalAdvisorCells++;
                    }
                }
            }
            int numberOfCells1Q = 20 * 20 / 4;
            percentageEmtyCells = (totalEmtyCells * 100) / numberOfCells1Q;
            percentageAdvisorCells = (totalAdvisorCells * 100) / numberOfCells1Q;
            int numerOfEmptyCells1Q = numberOfCells1Q * genetics.percentagesOfActionsPerQ[0][Genetics.Actions.None] / 100;
            int numerOfAdvisorCells1Q = numberOfCells1Q * genetics.percentagesOfActionsPerQ[0][Genetics.Actions.Advisor] / 100;
            Assert.AreNotEqual(0, numerOfEmptyCells1Q);
            Assert.AreNotEqual(0, numerOfAdvisorCells1Q);
            Assert.AreNotEqual(numberOfCells1Q, numerOfEmptyCells1Q);
            Assert.AreNotEqual(numberOfCells1Q, numerOfAdvisorCells1Q);
        }

        //[TestMethod()]
        //public void getIconForPosTest()
        //{
        //    Genetics genetics = new Genetics(20, 20);
        //    genetics.matrix[0, 0] = Genetics.Actions.Advisor;
        //    System.Drawing.Image img = genetics.getIconForPos(0, 0);
        //    Assert.AreEqual(GeneticStartupsWindows.Properties.Resources.startups_circus, img);
        //}
    }
}