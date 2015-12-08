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
        public void generatePercentagesTest()
        {
            Genetics genetics = new Genetics(20, 20, 19);
            int totalPercentages;
            for (int i=0; i< 4; i++)
            {
                totalPercentages = genetics.percentagesOfActionsPerQ[i][Genetics.Actions.None] +
                genetics.percentagesOfActionsPerQ[i][Genetics.Actions.Advisor] +
                genetics.percentagesOfActionsPerQ[i][Genetics.Actions.Circus] +
                genetics.percentagesOfActionsPerQ[i][Genetics.Actions.Team] +
                genetics.percentagesOfActionsPerQ[i][Genetics.Actions.Product] +
                genetics.percentagesOfActionsPerQ[i][Genetics.Actions.Feedback] +
                genetics.percentagesOfActionsPerQ[i][Genetics.Actions.Investor] +
                genetics.percentagesOfActionsPerQ[i][Genetics.Actions.Doubts] +
                genetics.percentagesOfActionsPerQ[i][Genetics.Actions.Sales] +
                genetics.percentagesOfActionsPerQ[i][Genetics.Actions.BadNews];
                Assert.AreEqual(100, totalPercentages);
            }
        }

        [TestMethod()]
        public void generateScoreProbabilitiesPerActionTest()
        {
            Genetics genetics = new Genetics(20, 20, 19);
            Assert.AreEqual(-1, genetics.scoresProbabilitiesPerAction[Genetics.Actions.None][0].Key);
            Assert.AreEqual("Feeling that things don't go as fast as expeceted...", genetics.scoresProbabilitiesPerAction[Genetics.Actions.None][0].Value);
        }

        [TestMethod()]
        public void createBoardFirstQuarterDistributionTest()
        {
            Genetics genetics = new Genetics(20, 20, 19);
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

        [TestMethod()]
        public void generatePopulationTest()
        {
            Genetics genetics = new Genetics(20, 20, 19);
            genetics.generatePopulation(10);
            Assert.AreEqual(5, genetics.numOfBinaryDigitsForStartCells);
            Assert.AreEqual(2*19, genetics.numOfBinaryDigitsForSteps);
            Assert.AreEqual(10, genetics.population.Length);
            Assert.IsTrue(genetics.population[0][0] < 2, "Encoding of individuals must be binary and 1st digit was: "+ genetics.population[0][0]);
        }

        [TestMethod()]
        public void generateScoresArrayTest()
        {
            Genetics genetics = new Genetics(20, 20, 19);
            genetics.createBoard();
            genetics.generatePopulation(10);
            genetics.generateScores();
            Assert.AreEqual(10, genetics.populationIndividualScores.Count);
        }

        [TestMethod()]
        public void generateScoresTest()
        {
            Genetics genetics = new Genetics(3, 3, 2);
            genetics.matrix = new Genetics.Actions[3, 3];
            genetics.matrix[0, 0] = Genetics.Actions.Circus;
            genetics.matrix[0, 1] = Genetics.Actions.Team;
            genetics.matrix[0, 2] = Genetics.Actions.Advisor;
            genetics.matrix[1, 0] = Genetics.Actions.Sales;
            genetics.matrix[1, 1] = Genetics.Actions.Investor;
            genetics.matrix[1, 2] = Genetics.Actions.Circus;
            genetics.matrix[2, 0] = Genetics.Actions.None;
            genetics.matrix[2, 1] = Genetics.Actions.None;
            genetics.matrix[2, 2] = Genetics.Actions.BadNews;
            genetics.generatePopulation(3);
            //genetics.population = new int[3][];
            genetics.population[0] = new int[] { 1, 0, 0, 1, 0, 1 }; // Starting 0,2; down; down
            genetics.population[1] = new int[] { 0, 1, 0, 0, 0, 1 }; // Starting 0,1; right; down
            genetics.population[2] = new int[] { 1, 0, 0, 0, 0, 0 }; // Starting 0,2; right; right
            genetics.generateScores();
            // TODO: THIS TEST IS FAILING!! ******************************************
            //Order of population elements based on scores (1st mus be element 1, 2nd, element 0 and 3rd element 2)
            Assert.AreEqual(1, genetics.populationIndividualScores[0].Key);
            Assert.AreEqual(0, genetics.populationIndividualScores[1].Key);
            Assert.AreEqual(2, genetics.populationIndividualScores[2].Key);
            //Values

        }

        [TestMethod()]
        public void newGenerationTest()
        {
            Genetics genetics = new Genetics(3, 3, 2);
            genetics.matrix = new Genetics.Actions[3, 3];
            genetics.matrix[0, 0] = Genetics.Actions.None;
            genetics.matrix[0, 1] = Genetics.Actions.Advisor;
            genetics.matrix[0, 2] = Genetics.Actions.Team;
            genetics.matrix[1, 0] = Genetics.Actions.Circus;
            genetics.matrix[1, 1] = Genetics.Actions.Investor;
            genetics.matrix[1, 2] = Genetics.Actions.None;
            genetics.matrix[2, 0] = Genetics.Actions.Product;
            genetics.matrix[2, 1] = Genetics.Actions.Sales;
            genetics.matrix[2, 2] = Genetics.Actions.BadNews;
            genetics.population = new int[3][];
            genetics.population[0] = new int[] { 0, 0, 0, 0, 0, 0 }; // Startin 0,0; right; right
            genetics.population[1] = new int[] { 1, 0, 1, 1, 0, 0 }; // Startin 0,2; up; right
            genetics.population[2] = new int[] { 1, 1, 0, 1, 1, 0 }; // Startin 0,1; down; left (4 -> 1 starting and left of 0 delivers -10
        }

        [TestMethod()]
        public void calculateSquareValueTest()
        {
            Genetics genetics = new Genetics(20, 20, 19);
            genetics.matrix = new Genetics.Actions[1, 1];
            genetics.matrix[0, 0] = Genetics.Actions.Advisor;
            int squareValue = genetics.calculateSquareValue(0,0);
            Assert.AreEqual(-32, squareValue);
        }

        [TestMethod()]
        public void calculatePathOfIndividualTest()
        {
            Genetics genetics = new Genetics(5, 5, 4);
            genetics.generatePopulation(10);
            int[] individual = { 1, 0, 0,
                                 0, 0,
                                 0, 0,
                                 0, 1,
                                 1, 0};
            Tuple<int, int>[] individualPath = genetics.calculatePathOfIndividual(individual);
            Tuple<int, int>[] expectedPath = { new Tuple<int, int>(0, 4), new Tuple<int, int>(1, 4), new Tuple<int, int>(2, 4),
                                               new Tuple<int, int>(2, 3), new Tuple<int, int>(1, 3)};
            Assert.AreEqual(expectedPath[0], individualPath[0]);
            Assert.AreEqual(expectedPath[1], individualPath[1]);
            Assert.AreEqual(expectedPath[4], individualPath[4]);
        }

        [TestMethod()]
        public void calculatePathOfIndividualOutOfBoundsTest()
        {
            Genetics genetics = new Genetics(5, 5, 4);
            genetics.generatePopulation(10);
            int[] individual = { 1, 0, 0,
                                 0, 0,
                                 0, 0,
                                 1, 1,
                                 1, 0};
            Tuple<int, int>[] individualPath = genetics.calculatePathOfIndividual(individual);
            Tuple<int, int>[] expectedPath = { new Tuple<int, int>(0, 4), new Tuple<int, int>(1, 4), new Tuple<int, int>(2, 4),
                                               new Tuple<int, int>(2, 5), new Tuple<int, int>(1, 5)};
            Assert.AreEqual(expectedPath[0], individualPath[0]);
            Assert.AreEqual(expectedPath[1], individualPath[1]);
            Assert.AreEqual(expectedPath[4], individualPath[4]);
        }

        //[TestMethod()]
        //public void getIconForPosTest()
        //{
        //    Genetics genetics = new Genetics(20, 20, 19);
        //    genetics.matrix[0, 0] = Genetics.Actions.Advisor;
        //    System.Drawing.Image img = genetics.getIconForPos(0, 0);
        //    Assert.AreEqual(GeneticStartupsWindows.Properties.Resources.startups_circus, img);
        //}
    }
}