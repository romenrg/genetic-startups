using GeneticStartupsWindows.Algorithm;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeneticStartupsWindows
{
    public class Genetics
    {
        public enum Actions { None=0, Advisor=1, Circus=2, Team=3, Product=4, Feedback=5, Investor=6, Doubts=7, Sales=8, BadNews=9}
        public enum States { Confusion=0, Success=1, Failure=2}

        private Random generateRandomNum;
        private int numSteps;

        public int[][] population;
        public List<KeyValuePair<int, int>> individualsSortedByScore;
        public int individualLength;

        public int numOfBinaryDigitsForStartCells;
        public int numOfBinaryDigitsForSteps;
        public int possibleDirections = 4;

        public const int CELL_VALUE_OUT_OF_BOUNDS = -10;
        public const int NUM_GENERATIONS = 15;

        public MapHelpers mapHelpers;

        // -----------------------------
        //  Public methods
        // -----------------------------

        public Genetics(int numCols, int numRows, int numSteps)
        {
            this.numSteps = numSteps;
            this.generateRandomNum = new Random();
            this.mapHelpers = new MapHelpers(numCols, numRows);
        }

        public void generatePopulation(int populationSize)
        {
            this.numOfBinaryDigitsForStartCells = (int)Math.Ceiling(Math.Log(this.mapHelpers.numRows, 2));
            this.numOfBinaryDigitsForSteps = (int)Math.Ceiling(Math.Log(this.possibleDirections, 2)) * this.numSteps;
            this.population = new int[populationSize][];
            for (int i = 0; i < populationSize; i++)
            {
                this.individualLength = numOfBinaryDigitsForStartCells + numOfBinaryDigitsForSteps;
                this.population[i] = new int[this.individualLength];
                for (int j=0; j< this.individualLength; j++)
                    this.population[i][j] = this.generateRandomNum.Next(2);
            }
        }

        public void newGeneration()
        {
            int elementsToMutate = this.population.Length / 3;
            int elementsToCross = (this.population.Length / 3) / 2 * 2;
            int elementsToSelect = this.population.Length - elementsToMutate - elementsToCross;
            int[][] selectedIndividuals = this.selection(elementsToSelect);
            int[][] crossedIndividuals = this.crossover(elementsToCross);
            int[][] mutatedIndividuals = this.mutation(elementsToMutate);
            this.population = selectedIndividuals.Concat(crossedIndividuals).Concat(mutatedIndividuals).ToArray(); ;
        }

        public void generateScores()
        {
            this.individualsSortedByScore = new List<KeyValuePair<int, int>>();
            for (int i = 0; i < this.population.Length; i++)
                this.individualsSortedByScore.Add(new KeyValuePair<int, int>(i, this.fitness(this.population[i])));
            this.individualsSortedByScore.Sort((x, y) => -1 * x.Value.CompareTo(y.Value));
        }

        public Tuple<int, int>[] getBestIndividualCellsPath()
        {
            return calculatePathOfIndividual(this.population[this.individualsSortedByScore[0].Key]);
        }

        
        //TODO: Probably should also be a private method called inside "fitness"
        public Tuple<int, int>[] calculatePathOfIndividual(int[] individual)
        {
            Tuple<int, int>[] cellsPath = new Tuple<int, int>[this.numSteps + 1];
            cellsPath[0] = this.calculateCellFromBinaryFirstPosition(individual.Skip(0).Take(this.numOfBinaryDigitsForStartCells).ToArray());
            int startingPositionOfMovement = this.numOfBinaryDigitsForStartCells;
            int numOfCellsToSkip = this.numOfBinaryDigitsForStartCells;
            Tuple<int, int> previousCell = cellsPath[0];
            for (int i=1; i<=this.numSteps; i++)
            {
                cellsPath[i] = this.calculateCellFromPreviousAndMovement(previousCell, individual.Skip(numOfCellsToSkip).Take(2).ToArray());
                previousCell = cellsPath[i];
                numOfCellsToSkip += 2;
            }
            return cellsPath;
        }


        // -----------------------------
        //  Private methods
        // -----------------------------

        private int fitness(int[] individual)
        {
            int individualScore = 0;
            Tuple<int, int>[] individualCellsPath = this.calculatePathOfIndividual(individual);
            for (int i=0; i<individualCellsPath.Length; i++)
            {
                individualScore += this.mapHelpers.calculateSquareValue(individualCellsPath[i].Item1, individualCellsPath[i].Item2);
            }
            return individualScore;
        }

        private int[][] selection(int elementsToSelect) {
            int[][] selectedIndividuals = new int[elementsToSelect][];
            for (int i=0; i< elementsToSelect; i++)
            {
                selectedIndividuals[i] = (int[])(this.population[this.individualsSortedByScore[i].Key]).Clone();
            }
            return selectedIndividuals;
        }

        private int[][] crossover(int elementsToCross) {
            int[][] crossedIndividuals = new int[elementsToCross][];
            for (int i=0; i < elementsToCross; i+=2)
            {
                int randomIndexOfFirstIndividualToMutate = this.generateRandomNum.Next(this.population.Length);
                int randomIndexOfSecondIndividualToMutate = this.generateRandomNum.Next(this.population.Length);
                int[] firstElementToCross = (int[])this.population[randomIndexOfFirstIndividualToMutate].Clone();
                int[] secondElementToCross = (int[])this.population[randomIndexOfSecondIndividualToMutate].Clone();
                int[] newFirstHalf = firstElementToCross.Skip(0).Take(this.individualLength / 2).ToArray();
                int[] newSecondHalf = secondElementToCross.Skip(this.individualLength / 2).Take(this.individualLength).ToArray();
                crossedIndividuals[i] = new int[this.individualLength];
                newFirstHalf.CopyTo(crossedIndividuals[i], 0);
                newSecondHalf.CopyTo(crossedIndividuals[i], newFirstHalf.Length);
                int[] oppositeFirstHalf = secondElementToCross.Skip(0).Take(this.individualLength / 2).ToArray();
                int[] oppositeSecondHalf = firstElementToCross.Skip(this.individualLength / 2).Take(this.individualLength).ToArray();
                crossedIndividuals[i+1] = new int[this.individualLength];
                oppositeFirstHalf.CopyTo(crossedIndividuals[i+1], 0);
                oppositeSecondHalf.CopyTo(crossedIndividuals[i+1], newFirstHalf.Length);
            }
            return crossedIndividuals;
        }

        private int[][] mutation(int elementsToMutate) {
            int[][] mutatedIndividuals = new int[elementsToMutate][];
            for (int i = 0; i < elementsToMutate; i++)
            {
                int randomIndexOfIndividualToMutate = this.generateRandomNum.Next(this.population.Length);
                mutatedIndividuals[i] = (int[])this.population[randomIndexOfIndividualToMutate].Clone();
                int randomElementOfndividual = this.generateRandomNum.Next(mutatedIndividuals[i].Length - 1);
                if (mutatedIndividuals[i][randomElementOfndividual] == 1)
                    mutatedIndividuals[i][randomElementOfndividual] = 0;
                else
                    mutatedIndividuals[i][randomElementOfndividual] = 1;
            }
            return mutatedIndividuals;
        }

        private Tuple<int, int> calculateCellFromPreviousAndMovement(Tuple<int, int> previousCell, int[] movement)
        {
            Tuple<int, int> newSquare = new Tuple<int, int>(-1, -1);
            if (this.mapHelpers.movingRight(movement))
            {
                newSquare = new Tuple<int, int>(previousCell.Item1 + 1, previousCell.Item2);
            }
            else if (this.mapHelpers.movingDown(movement))
            {
                newSquare = new Tuple<int, int>(previousCell.Item1, previousCell.Item2 - 1);
            }
            else if (this.mapHelpers.movingLeft(movement))
            {
                newSquare = new Tuple<int, int>(previousCell.Item1 - 1, previousCell.Item2);
            }
            else if (this.mapHelpers.movingUp(movement))
            {
                newSquare = new Tuple<int, int>(previousCell.Item1, previousCell.Item2 + 1);
            }
            return newSquare;
        }


        private Tuple<int, int> calculateCellFromBinaryFirstPosition(int[] firstSquare)
        {
            int decimalValue = this.binaryArrayToDecimalInt(firstSquare);
            // Number of rows may not be a power of 2. If decimal value is too big, make it circular
            int decimalValueAdjustedToMax = decimalValue % this.mapHelpers.numRows;
            return new Tuple<int, int>(0, decimalValueAdjustedToMax);
        }

        private int binaryArrayToDecimalInt(int[] binary)
        {
            int decimalValue = 0;
            int highestPower = binary.Length - 1;
            for (int i = highestPower; i >= 0; i--)
            {
                decimalValue += binary[highestPower - i] * (int)Math.Pow(2, i);
            }
            return decimalValue;
        }

        // ----------------------------------
        //  Proxies for Map Helper functions
        // ----------------------------------
        public void createBoard()
        {
            this.mapHelpers.createBoard();
        }

        public Image getIconForPos(int i, int j)
        {
            return this.mapHelpers.getIconForPos(i, j);
        }

        public bool isCellInsideMap(int x, int y)
        {
            return this.mapHelpers.isCellInsideMap(x, y);
        }
    }
}
