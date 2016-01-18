using GeneticStartupsWindows.Algorithm;
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
    public class MapHelpersTests
    {
        [TestMethod()]
        public void generatePercentagesTest()
        {
            MapHelpers mapHelpers = new MapHelpers(20, 20);
            int totalPercentages;
            for (int i=0; i< 4; i++)
            {
                totalPercentages = mapHelpers.actionProbabilityPerQ[i][Genetics.Actions.None] +
                mapHelpers.actionProbabilityPerQ[i][Genetics.Actions.Advisor] +
                mapHelpers.actionProbabilityPerQ[i][Genetics.Actions.Circus] +
                mapHelpers.actionProbabilityPerQ[i][Genetics.Actions.Team] +
                mapHelpers.actionProbabilityPerQ[i][Genetics.Actions.Product] +
                mapHelpers.actionProbabilityPerQ[i][Genetics.Actions.Feedback] +
                mapHelpers.actionProbabilityPerQ[i][Genetics.Actions.Investor] +
                mapHelpers.actionProbabilityPerQ[i][Genetics.Actions.Doubts] +
                mapHelpers.actionProbabilityPerQ[i][Genetics.Actions.Sales] +
                mapHelpers.actionProbabilityPerQ[i][Genetics.Actions.BadNews];
                Assert.AreEqual(100, totalPercentages);
            }
        }

        [TestMethod()]
        public void possibleScoresAndExplanationsPerActionTest()
        {
            MapHelpers mapHelpers = new MapHelpers(20, 20);
            Assert.AreEqual(-1, mapHelpers.possibleScoresPerAction[Genetics.Actions.None][0].Key);
            Assert.AreEqual("Feeling that things don't go as fast as expeceted...", mapHelpers.possibleScoresPerAction[Genetics.Actions.None][0].Value);
        }

        [TestMethod()]
        public void createBoardFirstQuarterDistributionTest()
        {
            MapHelpers mapHelpers = new MapHelpers(20, 20);
            mapHelpers.createBoard();
            int totalEmtyCells = 0, totalAdvisorCells = 0;
            int percentageEmtyCells = 0, percentageAdvisorCells = 0;
            for (int i=0; i < mapHelpers.matrix.GetLength(0) / 4; i++)
            {
                for (int j = 0; j < mapHelpers.matrix.GetLength(0); j++)
                {
                    if (mapHelpers.matrix[i, j] == Genetics.Actions.None)
                    {
                        totalEmtyCells++;
                    }
                    else if (mapHelpers.matrix[i, j] == Genetics.Actions.Advisor)
                    {
                        totalAdvisorCells++;
                    }
                }
            }
            int numberOfCells1Q = 20 * 20 / 4;
            percentageEmtyCells = (totalEmtyCells * 100) / numberOfCells1Q;
            percentageAdvisorCells = (totalAdvisorCells * 100) / numberOfCells1Q;
            int numerOfEmptyCells1Q = numberOfCells1Q * mapHelpers.actionProbabilityPerQ[0][Genetics.Actions.None] / 100;
            int numerOfAdvisorCells1Q = numberOfCells1Q * mapHelpers.actionProbabilityPerQ[0][Genetics.Actions.Advisor] / 100;
            Assert.AreNotEqual(0, numerOfEmptyCells1Q);
            Assert.AreNotEqual(0, numerOfAdvisorCells1Q);
            Assert.AreNotEqual(numberOfCells1Q, numerOfEmptyCells1Q);
            Assert.AreNotEqual(numberOfCells1Q, numerOfAdvisorCells1Q);
        }

        [TestMethod()]
        public void calculateSquareValueTest()
        {
            MapHelpers mapHelpers = new MapHelpers(20, 20);
            mapHelpers.matrix = new Genetics.Actions[1, 1];
            mapHelpers.matrix[0, 0] = Genetics.Actions.Advisor;
            int squareValue = mapHelpers.calculateSquareValue(0,0);
            Assert.AreEqual(-32, squareValue);
        }

        [TestMethod()]
        public void getProperIconForPosTest()
        {
            MapHelpers mapHelpers = new MapHelpers(20, 20);
            mapHelpers.matrix = new Genetics.Actions[1, 1];
            mapHelpers.matrix[0, 0] = Genetics.Actions.None;
            System.Drawing.Image img = mapHelpers.getIconForPos(0, 0);
            Assert.AreEqual(null, img);
        }
    }
}