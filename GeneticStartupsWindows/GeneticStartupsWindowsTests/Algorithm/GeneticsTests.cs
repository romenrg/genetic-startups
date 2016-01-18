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
        public void generatePopulationBinaryChromosomesTest()
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
            Assert.AreEqual(10, genetics.individualsSortedByScore.Count);
        }

        [TestMethod()]
        public void generateScoresAndSortIndividualsTest()
        {
            Genetics genetics = new Genetics(3, 3, 2);
            genetics.mapHelpers.matrix = new Genetics.Actions[3, 3];
            genetics.mapHelpers.matrix[0, 0] = Genetics.Actions.Circus;
            genetics.mapHelpers.matrix[0, 1] = Genetics.Actions.Team;
            genetics.mapHelpers.matrix[0, 2] = Genetics.Actions.Advisor;
            genetics.mapHelpers.matrix[1, 0] = Genetics.Actions.Sales;
            genetics.mapHelpers.matrix[1, 1] = Genetics.Actions.Investor;
            genetics.mapHelpers.matrix[1, 2] = Genetics.Actions.Circus;
            genetics.mapHelpers.matrix[2, 0] = Genetics.Actions.None;
            genetics.mapHelpers.matrix[2, 1] = Genetics.Actions.None;
            genetics.mapHelpers.matrix[2, 2] = Genetics.Actions.BadNews;
            genetics.generatePopulation(3);
            genetics.population[0] = new int[] { 1, 0, 0, 1, 0, 1 }; // Starting 0,2; down; down
            genetics.population[1] = new int[] { 0, 1, 0, 0, 0, 1 }; // Starting 0,1; right; down
            genetics.population[2] = new int[] { 1, 0, 0, 0, 0, 0 }; // Starting 0,2; right; right
            genetics.generateScores();
            Assert.AreEqual(1, genetics.individualsSortedByScore[0].Key);
            Assert.AreEqual(0, genetics.individualsSortedByScore[1].Key);
            Assert.AreEqual(2, genetics.individualsSortedByScore[2].Key);
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
                                               new Tuple<int, int>(2, 3), new Tuple<int, int>(3, 3)};
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
                                               new Tuple<int, int>(2, 5), new Tuple<int, int>(3, 5)};
            Assert.AreEqual(expectedPath[0], individualPath[0]);
            Assert.AreEqual(expectedPath[1], individualPath[1]);
            Assert.AreEqual(expectedPath[4], individualPath[4]);
        }
    }
}