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

        public Actions [,] matrix { get; set; }
        public Dictionary<Actions, int>[] percentagesOfActionsPerQ { get; set; }
        public Dictionary<Actions, List<KeyValuePair<int, string>>> scoresProbabilitiesPerAction { get; set; }

        private Random generateRandomNum;
        private int numCols;
        private int numRows;
        private int numSteps;

        public int[][] population;
        public List<KeyValuePair<int, int>> populationIndividualScores;
        public int individualLength;

        public int numOfBinaryDigitsForStartCells;
        public int numOfBinaryDigitsForSteps;
        public int possibleDirections = 4;

        public const int CELL_VALUE_OUT_OF_BOUNDS = -10;
        public const int NUM_GENERATIONS = 10;

        // -----------------------------
        //  Public methods
        // -----------------------------

        public Genetics(int numCols, int numRows, int numSteps)
        {
            this.numCols = numCols;
            this.numRows = numRows;
            this.numSteps = numSteps;
            this.generateRandomNum = new Random();
            this.generatePercentagesOfActionsPerQ();
            this.generateScoreProbabilitiesPerAction();
        }

        public void createBoard()
        {
            this.matrix = new Actions[this.numCols, this.numRows];
            for (int i = 0; i < this.numCols; i++)
            {
                for (int j = 0; j < this.numRows; j++)
                {
                    this.matrix[i, j] = generateCellContent(i,j);
                }
            }
        }

        public Image getIconForPos(int i, int j)
        {
            return this.transformActionEnumToImage(i, j);
        }

        public void generatePopulation(int populationSize)
        {
            this.numOfBinaryDigitsForStartCells = (int)Math.Ceiling(Math.Log(this.numRows, 2));
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
            //int[][] selectedIndividuals = this.selection();
            //int[][] crossedIndividuals = this.crossover();
            //int[][] mutatedIndividuals = this.mutation();
            //this.population = selectedIndividuals.Concat(crossedIndividuals).Concat(mutatedIndividuals).ToArray(); ;
        }

        public void generateScores()
        {
            this.populationIndividualScores = new List<KeyValuePair<int, int>>();
            for (int i = 0; i < this.population.Length; i++)
                this.populationIndividualScores.Add(new KeyValuePair<int, int>(i, this.fitness(this.population[i])));
            this.populationIndividualScores.Sort((x, y) => -1 * x.Value.CompareTo(y.Value));
        }

        public Tuple<int, int>[] getBestIndividualCellsPath()
        {
            return calculatePathOfIndividual(this.population[this.populationIndividualScores[0].Key]);
        }


        public bool isCellInsideMap(int x, int y)
        {
            return (x >= 0) && (x < this.numCols) && (y >= 0) && (y < this.numRows);
        }

        // TODO: Probably should be changed as a private method and the test for this is included in the test for the whole individual path
        public int calculateSquareValue(int x, int y)
        {
            if (this.isCellInsideMap(x, y))
            {
                Actions squareAction = this.matrix[x, y];
                int squareValue = 0;
                for (int i=0; i<this.scoresProbabilitiesPerAction[squareAction].Count; i++)
                {
                    squareValue += this.scoresProbabilitiesPerAction[squareAction][i].Key;
                }
                return squareValue;
            }
            else
            {
                return Genetics.CELL_VALUE_OUT_OF_BOUNDS;
            }
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
                individualScore += this.calculateSquareValue(individualCellsPath[i].Item1, individualCellsPath[i].Item2);
            }
            // Now we should check the squares the individual is visiting and
            // must have a data structure with the possible values of each action
            // E.G. advisor is a random number between -15 and 5
            // Finish is -50 (failure) or 50 (success)
            // To sort multiple winning solutions we add the number of remaining squares to the score
            // E.G. If winning in the 5th square out of 20, the score will be 50 + 15 = 65;
            return individualScore;
        }

        //private int[][] selection() {
            //int[][] selectedIndividuals = new int[this.population.Length / 2][];
            //selectedIndividuals[0] = new int[this.individualLength];
            //return selectedIndividuals;
        //}

        //private int[][] crossover() {
            //int[][] crossedIndividuals = new int[this.population.Length / 2][];
            //crossedIndividuals[0] = new int[this.individualLength];
            //return crossedIndividuals;
        //}

        //private int[][] mutation() {
            //int[][] mutatedIndividuals = new int[this.population.Length / 2][];
            //mutatedIndividuals[0] = new int[this.individualLength];
            //return mutatedIndividuals;
        //}

        private Tuple<int, int> calculateCellFromPreviousAndMovement(Tuple<int, int> previousCell, int[] movement)
        {
            // TODO: Something should be done if movements lead to a non-existing square...
            // Probably in the fitness function it should be considered like -10 (going out of the map)
            Tuple<int, int> newSquare = new Tuple<int, int>(-1, -1);
            if (this.movingRight(movement))
            {
                newSquare = new Tuple<int, int>(previousCell.Item1 + 1, previousCell.Item2);
            }
            else if (this.movingDown(movement))
            {
                newSquare = new Tuple<int, int>(previousCell.Item1, previousCell.Item2 - 1);
            }
            else if (this.movingLeft(movement))
            {
                newSquare = new Tuple<int, int>(previousCell.Item1 - 1, previousCell.Item2);
            }
            else if (this.movingUp(movement))
            {
                newSquare = new Tuple<int, int>(previousCell.Item1, previousCell.Item2 + 1);
            }
            return newSquare;
        }

        private bool movingRight(int[] movement)
        {
            return (movement[0] == 0) && (movement[1] == 0);
        }

        private bool movingDown(int[] movement)
        {
            return (movement[0] == 0) && (movement[1] == 1);
        }

        private bool movingLeft(int[] movement)
        {
            return (movement[0] == 1) && (movement[1] == 0);
        }

        private bool movingUp(int[] movement)
        {
            return (movement[0] == 1) && (movement[1] == 1);
        }

        private Tuple<int, int> calculateCellFromBinaryFirstPosition(int[] firstSquare)
        {
            int decimalValue = this.binaryArrayToDecimalInt(firstSquare);
            // Number of rows may not be a power of 2. If decimal value is too big, make it circular
            int decimalValueAdjustedToMax = decimalValue % this.numRows;
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

        private void generateScoreProbabilitiesPerAction()
        {
            this.scoresProbabilitiesPerAction = new Dictionary<Actions, List<KeyValuePair<int, string>>>();
            this.scoresProbabilitiesPerAction[Actions.None] = new List<KeyValuePair<int, string>>();
            this.scoresProbabilitiesPerAction[Actions.None].Add(new KeyValuePair<int, string>(-1, "Feeling that things don't go as fast as expeceted..."));
            this.scoresProbabilitiesPerAction[Actions.None].Add(new KeyValuePair<int, string>(-1, "Feeling that things don't go as fast as expeceted..."));
            this.scoresProbabilitiesPerAction[Actions.None].Add(new KeyValuePair<int, string>(0, "Just one more day in the startup world!"));
            this.scoresProbabilitiesPerAction[Actions.None].Add(new KeyValuePair<int, string>(0, "Just one more day in the startup world!"));
            this.scoresProbabilitiesPerAction[Actions.None].Add(new KeyValuePair<int, string>(0, "Just one more day in the startup world!"));
            this.scoresProbabilitiesPerAction[Actions.None].Add(new KeyValuePair<int, string>(0, "Just one more day in the startup world!"));
            this.scoresProbabilitiesPerAction[Actions.None].Add(new KeyValuePair<int, string>(0, "Just one more day in the startup world!"));
            this.scoresProbabilitiesPerAction[Actions.None].Add(new KeyValuePair<int, string>(0, "Just one more day in the startup world!"));
            this.scoresProbabilitiesPerAction[Actions.None].Add(new KeyValuePair<int, string>(1, "One day closer to success!"));
            this.scoresProbabilitiesPerAction[Actions.None].Add(new KeyValuePair<int, string>(1, "One day closer to success!"));
            this.scoresProbabilitiesPerAction[Actions.Advisor] = new List<KeyValuePair<int, string>>();
            this.scoresProbabilitiesPerAction[Actions.Advisor].Add(new KeyValuePair<int, string>(-15, "The 'advisor' turned out to be a liar, had no idea but took big shares"));
            this.scoresProbabilitiesPerAction[Actions.Advisor].Add(new KeyValuePair<int, string>(-10, "The 'advisor' had no idea, gave wrong advice and company suffered"));
            this.scoresProbabilitiesPerAction[Actions.Advisor].Add(new KeyValuePair<int, string>(-6, "You realized the 'advisor' won't help at all"));
            this.scoresProbabilitiesPerAction[Actions.Advisor].Add(new KeyValuePair<int, string>(-6, "You realized the 'advisor' won't help at all"));
            this.scoresProbabilitiesPerAction[Actions.Advisor].Add(new KeyValuePair<int, string>(-4, "The 'advisor' just wanted to sell his/her services"));
            this.scoresProbabilitiesPerAction[Actions.Advisor].Add(new KeyValuePair<int, string>(-4, "The 'advisor' just wanted to sell his/her services"));
            this.scoresProbabilitiesPerAction[Actions.Advisor].Add(new KeyValuePair<int, string>(-4, "The 'advisor' just wanted to sell his/her services"));
            this.scoresProbabilitiesPerAction[Actions.Advisor].Add(new KeyValuePair<int, string>(2, "The 'advisor' nows about the market and may be helful"));
            this.scoresProbabilitiesPerAction[Actions.Advisor].Add(new KeyValuePair<int, string>(5, "The 'advisor' is connected to investors in the field"));
            this.scoresProbabilitiesPerAction[Actions.Advisor].Add(new KeyValuePair<int, string>(10, "The 'advisor' will bring important customers"));
            this.scoresProbabilitiesPerAction[Actions.Circus] = new List<KeyValuePair<int, string>>();
            this.scoresProbabilitiesPerAction[Actions.Circus].Add(new KeyValuePair<int, string>(-5, "You should have been working instead; you wasted a lot of time"));
            this.scoresProbabilitiesPerAction[Actions.Circus].Add(new KeyValuePair<int, string>(-5, "You should have been working instead; you wasted a lot of time"));
            this.scoresProbabilitiesPerAction[Actions.Circus].Add(new KeyValuePair<int, string>(-5, "You should have been working instead; you wasted a lot of time"));
            this.scoresProbabilitiesPerAction[Actions.Circus].Add(new KeyValuePair<int, string>(-5, "You should have been working instead; you wasted a lot of time"));
            this.scoresProbabilitiesPerAction[Actions.Circus].Add(new KeyValuePair<int, string>(-5, "You should have been working instead; you wasted a lot of time"));
            this.scoresProbabilitiesPerAction[Actions.Circus].Add(new KeyValuePair<int, string>(-2, "You just wasted some time going there, not that bad"));
            this.scoresProbabilitiesPerAction[Actions.Circus].Add(new KeyValuePair<int, string>(-2, "You just wasted some time going there, not that bad"));
            this.scoresProbabilitiesPerAction[Actions.Circus].Add(new KeyValuePair<int, string>(2, "Maybe someone you met today will help you in future"));
            this.scoresProbabilitiesPerAction[Actions.Circus].Add(new KeyValuePair<int, string>(5, "You built useful connections (either partners or potential investors)"));
            this.scoresProbabilitiesPerAction[Actions.Circus].Add(new KeyValuePair<int, string>(7, "You built very useful connections (someone important or well connected)"));
            this.scoresProbabilitiesPerAction[Actions.Team] = new List<KeyValuePair<int, string>>();
            this.scoresProbabilitiesPerAction[Actions.Team].Add(new KeyValuePair<int, string>(-15, "You picked a troublemaker as founder and gave him/her 50% of shares"));
            this.scoresProbabilitiesPerAction[Actions.Team].Add(new KeyValuePair<int, string>(-3, "The new team member has just left college and is inexperienced"));
            this.scoresProbabilitiesPerAction[Actions.Team].Add(new KeyValuePair<int, string>(-2, "Another person with the same profile joined"));
            this.scoresProbabilitiesPerAction[Actions.Team].Add(new KeyValuePair<int, string>(2, "Regular worker joined the company"));
            this.scoresProbabilitiesPerAction[Actions.Team].Add(new KeyValuePair<int, string>(2, "Regular worker joined the company"));
            this.scoresProbabilitiesPerAction[Actions.Team].Add(new KeyValuePair<int, string>(2, "Regular worker joined the company"));
            this.scoresProbabilitiesPerAction[Actions.Team].Add(new KeyValuePair<int, string>(5, "Talented employee joined the company"));
            this.scoresProbabilitiesPerAction[Actions.Team].Add(new KeyValuePair<int, string>(5, "Talented employee joined the company"));
            this.scoresProbabilitiesPerAction[Actions.Team].Add(new KeyValuePair<int, string>(7, "Talented employee with startup experience joined the company"));
            this.scoresProbabilitiesPerAction[Actions.Team].Add(new KeyValuePair<int, string>(7, "Talented person with startup experience and connections in the field joined as co-founder"));
            this.scoresProbabilitiesPerAction[Actions.Product] = new List<KeyValuePair<int, string>>();
            this.scoresProbabilitiesPerAction[Actions.Product].Add(new KeyValuePair<int, string>(-3, "You invested too much (time and money) in your MVP"));
            this.scoresProbabilitiesPerAction[Actions.Product].Add(new KeyValuePair<int, string>(3, "You released an MVP or a very small increment to test the market"));
            this.scoresProbabilitiesPerAction[Actions.Product].Add(new KeyValuePair<int, string>(3, "You released an MVP or a very small increment to test the market"));
            this.scoresProbabilitiesPerAction[Actions.Product].Add(new KeyValuePair<int, string>(3, "You released an MVP or a very small increment to test the market"));
            this.scoresProbabilitiesPerAction[Actions.Product].Add(new KeyValuePair<int, string>(3, "You released an MVP or a very small increment to test the market"));
            this.scoresProbabilitiesPerAction[Actions.Product].Add(new KeyValuePair<int, string>(3, "You released an MVP or a very small increment to test the market"));
            this.scoresProbabilitiesPerAction[Actions.Product].Add(new KeyValuePair<int, string>(4, "You released new features after listening to customer feedback and testing the market"));
            this.scoresProbabilitiesPerAction[Actions.Product].Add(new KeyValuePair<int, string>(4, "You released new features after listening to customer feedback and testing the market"));
            this.scoresProbabilitiesPerAction[Actions.Product].Add(new KeyValuePair<int, string>(4, "You released new features after listening to customer feedback and testing the market"));
            this.scoresProbabilitiesPerAction[Actions.Product].Add(new KeyValuePair<int, string>(5, "You embraced Agile methodologies: product delivery is optimized and work environment improved significantly"));
            this.scoresProbabilitiesPerAction[Actions.Feedback] = new List<KeyValuePair<int, string>>();
            this.scoresProbabilitiesPerAction[Actions.Feedback].Add(new KeyValuePair<int, string>(1, "You included polls in your product"));
            this.scoresProbabilitiesPerAction[Actions.Feedback].Add(new KeyValuePair<int, string>(2, "You read customer emails and comments"));
            this.scoresProbabilitiesPerAction[Actions.Feedback].Add(new KeyValuePair<int, string>(2, "You read customer emails and comments"));
            this.scoresProbabilitiesPerAction[Actions.Feedback].Add(new KeyValuePair<int, string>(3, "You did one usability test with a friend"));
            this.scoresProbabilitiesPerAction[Actions.Feedback].Add(new KeyValuePair<int, string>(3, "You did one usability test with a friend"));
            this.scoresProbabilitiesPerAction[Actions.Feedback].Add(new KeyValuePair<int, string>(5, "You did one usability test with a potential customer"));
            this.scoresProbabilitiesPerAction[Actions.Feedback].Add(new KeyValuePair<int, string>(5, "You did one usability test with a potential customer"));
            this.scoresProbabilitiesPerAction[Actions.Feedback].Add(new KeyValuePair<int, string>(6, "You are tracking user events and reviewing analytics to improve"));
            this.scoresProbabilitiesPerAction[Actions.Feedback].Add(new KeyValuePair<int, string>(6, "You are tracking user events and reviewing analytics to improve"));
            this.scoresProbabilitiesPerAction[Actions.Feedback].Add(new KeyValuePair<int, string>(8, "You performed an A/B test to be sure which change better"));
            this.scoresProbabilitiesPerAction[Actions.Investor] = new List<KeyValuePair<int, string>>();
            this.scoresProbabilitiesPerAction[Actions.Investor].Add(new KeyValuePair<int, string>(2, "You get money from FFF"));
            this.scoresProbabilitiesPerAction[Actions.Investor].Add(new KeyValuePair<int, string>(3, "An investor with no tech experience nor startup experience joined"));
            this.scoresProbabilitiesPerAction[Actions.Investor].Add(new KeyValuePair<int, string>(5, "An investor with tech experience but no startup experience joined"));
            this.scoresProbabilitiesPerAction[Actions.Investor].Add(new KeyValuePair<int, string>(7, "An investor with startup experience (in other fields) joined"));
            this.scoresProbabilitiesPerAction[Actions.Investor].Add(new KeyValuePair<int, string>(7, "An investor with startup experience (in other fields) joined"));
            this.scoresProbabilitiesPerAction[Actions.Investor].Add(new KeyValuePair<int, string>(10, "An investor with startup experience in your field joined"));
            this.scoresProbabilitiesPerAction[Actions.Investor].Add(new KeyValuePair<int, string>(10, "An investor with startup experience in your field joined"));
            this.scoresProbabilitiesPerAction[Actions.Investor].Add(new KeyValuePair<int, string>(15, "An investor with startup experience and contacts in your field joined, bringing customers"));
            this.scoresProbabilitiesPerAction[Actions.Investor].Add(new KeyValuePair<int, string>(15, "An investor with startup experience and contacts in your field joined, bringing customers"));
            this.scoresProbabilitiesPerAction[Actions.Doubts] = new List<KeyValuePair<int, string>>();
            this.scoresProbabilitiesPerAction[Actions.Doubts].Add(new KeyValuePair<int, string>(-2, "You have doubts and feel lost"));
            this.scoresProbabilitiesPerAction[Actions.Doubts].Add(new KeyValuePair<int, string>(-1, "You have doubts and feel lost"));
            this.scoresProbabilitiesPerAction[Actions.Doubts].Add(new KeyValuePair<int, string>(-1, "You have doubts and feel lost"));
            this.scoresProbabilitiesPerAction[Actions.Doubts].Add(new KeyValuePair<int, string>(-1, "You have doubts and feel lost"));
            this.scoresProbabilitiesPerAction[Actions.Doubts].Add(new KeyValuePair<int, string>(-1, "You have doubts and feel lost"));
            this.scoresProbabilitiesPerAction[Actions.Doubts].Add(new KeyValuePair<int, string>(-1, "You have doubts and feel lost"));
            this.scoresProbabilitiesPerAction[Actions.Doubts].Add(new KeyValuePair<int, string>(1, "You have doubts, but that motivates you to try new things"));
            this.scoresProbabilitiesPerAction[Actions.Doubts].Add(new KeyValuePair<int, string>(1, "You have doubts, but that motivates you to try new things"));
            this.scoresProbabilitiesPerAction[Actions.Doubts].Add(new KeyValuePair<int, string>(1, "You have doubts, but that motivates you to try new things"));
            this.scoresProbabilitiesPerAction[Actions.Doubts].Add(new KeyValuePair<int, string>(2, "You have doubts, but that motivates you to try new things"));
            this.scoresProbabilitiesPerAction[Actions.Sales] = new List<KeyValuePair<int, string>>();
            this.scoresProbabilitiesPerAction[Actions.Sales].Add(new KeyValuePair<int, string>(5, "Sold the product to a small customer (or small group)"));
            this.scoresProbabilitiesPerAction[Actions.Sales].Add(new KeyValuePair<int, string>(5, "Sold the product to a small customer (or small group)"));
            this.scoresProbabilitiesPerAction[Actions.Sales].Add(new KeyValuePair<int, string>(5, "Sold the product to a small customer (or small group)"));
            this.scoresProbabilitiesPerAction[Actions.Sales].Add(new KeyValuePair<int, string>(5, "Sold the product to a small customer (or small group)"));
            this.scoresProbabilitiesPerAction[Actions.Sales].Add(new KeyValuePair<int, string>(5, "Sold the product to a small customer (or small group)"));
            this.scoresProbabilitiesPerAction[Actions.Sales].Add(new KeyValuePair<int, string>(5, "Sold the product to a small customer (or small group)"));
            this.scoresProbabilitiesPerAction[Actions.Sales].Add(new KeyValuePair<int, string>(5, "Sold the product to a small customer (or small group)"));
            this.scoresProbabilitiesPerAction[Actions.Sales].Add(new KeyValuePair<int, string>(10, "Sold the product to a medium-size customer (or medium-size group)"));
            this.scoresProbabilitiesPerAction[Actions.Sales].Add(new KeyValuePair<int, string>(10, "Sold the product to a medium-size customer (or medium-size group)"));
            this.scoresProbabilitiesPerAction[Actions.Sales].Add(new KeyValuePair<int, string>(20, "Sold the product to a big customer (or big group)"));
            this.scoresProbabilitiesPerAction[Actions.BadNews] = new List<KeyValuePair<int, string>>();
            this.scoresProbabilitiesPerAction[Actions.BadNews].Add(new KeyValuePair<int, string>(-15, "Bad news..."));
            this.scoresProbabilitiesPerAction[Actions.BadNews].Add(new KeyValuePair<int, string>(-10, "Bad news..."));
            this.scoresProbabilitiesPerAction[Actions.BadNews].Add(new KeyValuePair<int, string>(-5, "Bad news..."));
            this.scoresProbabilitiesPerAction[Actions.BadNews].Add(new KeyValuePair<int, string>(-5, "Bad news..."));
            this.scoresProbabilitiesPerAction[Actions.BadNews].Add(new KeyValuePair<int, string>(-5, "Bad news..."));
            this.scoresProbabilitiesPerAction[Actions.BadNews].Add(new KeyValuePair<int, string>(-5, "Bad news..."));
            this.scoresProbabilitiesPerAction[Actions.BadNews].Add(new KeyValuePair<int, string>(-5, "Bad news..."));
            this.scoresProbabilitiesPerAction[Actions.BadNews].Add(new KeyValuePair<int, string>(-2, "Bad news..."));
            this.scoresProbabilitiesPerAction[Actions.BadNews].Add(new KeyValuePair<int, string>(-2, "Bad news..."));
            this.scoresProbabilitiesPerAction[Actions.BadNews].Add(new KeyValuePair<int, string>(-1, "Bad news..."));
        }

        private void generatePercentagesOfActionsPerQ()
        {
            this.percentagesOfActionsPerQ = new Dictionary<Actions, int>[4];
            this.percentagesOfActionsPerQ[0] = new Dictionary<Actions, int>();
            this.percentagesOfActionsPerQ[0][Actions.None]     = 65;
            this.percentagesOfActionsPerQ[0][Actions.Advisor]  = 11;
            this.percentagesOfActionsPerQ[0][Actions.Circus]   = 8;
            this.percentagesOfActionsPerQ[0][Actions.Team]     = 4;
            this.percentagesOfActionsPerQ[0][Actions.Product]  = 2;
            this.percentagesOfActionsPerQ[0][Actions.Feedback] = 2;
            this.percentagesOfActionsPerQ[0][Actions.Investor] = 1;
            this.percentagesOfActionsPerQ[0][Actions.Doubts] = 4;
            this.percentagesOfActionsPerQ[0][Actions.Sales] = 1;
            this.percentagesOfActionsPerQ[0][Actions.BadNews] = 2;
            this.percentagesOfActionsPerQ[1] = new Dictionary<Actions, int>();
            this.percentagesOfActionsPerQ[1][Actions.None] = 55;
            this.percentagesOfActionsPerQ[1][Actions.Advisor] = 8;
            this.percentagesOfActionsPerQ[1][Actions.Circus] = 13;
            this.percentagesOfActionsPerQ[1][Actions.Team] = 4;
            this.percentagesOfActionsPerQ[1][Actions.Product] = 6;
            this.percentagesOfActionsPerQ[1][Actions.Feedback] = 4;
            this.percentagesOfActionsPerQ[1][Actions.Investor] = 1;
            this.percentagesOfActionsPerQ[1][Actions.Doubts] = 3;
            this.percentagesOfActionsPerQ[1][Actions.Sales] = 1;
            this.percentagesOfActionsPerQ[1][Actions.BadNews] = 5;
            this.percentagesOfActionsPerQ[2] = new Dictionary<Actions, int>();
            this.percentagesOfActionsPerQ[2][Actions.None] = 55;
            this.percentagesOfActionsPerQ[2][Actions.Advisor] = 6;
            this.percentagesOfActionsPerQ[2][Actions.Circus] = 6;
            this.percentagesOfActionsPerQ[2][Actions.Team] = 4;
            this.percentagesOfActionsPerQ[2][Actions.Product] = 5;
            this.percentagesOfActionsPerQ[2][Actions.Feedback] = 7;
            this.percentagesOfActionsPerQ[2][Actions.Investor] = 3;
            this.percentagesOfActionsPerQ[2][Actions.Doubts] = 2;
            this.percentagesOfActionsPerQ[2][Actions.Sales] = 4;
            this.percentagesOfActionsPerQ[2][Actions.BadNews] = 8;
            this.percentagesOfActionsPerQ[3] = new Dictionary<Actions, int>();
            this.percentagesOfActionsPerQ[3][Actions.None] = 70;
            this.percentagesOfActionsPerQ[3][Actions.Advisor] = 2;
            this.percentagesOfActionsPerQ[3][Actions.Circus] = 4;
            this.percentagesOfActionsPerQ[3][Actions.Team] = 3;
            this.percentagesOfActionsPerQ[3][Actions.Product] = 3;
            this.percentagesOfActionsPerQ[3][Actions.Feedback] = 5;
            this.percentagesOfActionsPerQ[3][Actions.Investor] = 3;
            this.percentagesOfActionsPerQ[3][Actions.Doubts] = 0;
            this.percentagesOfActionsPerQ[3][Actions.Sales] = 7;
            this.percentagesOfActionsPerQ[3][Actions.BadNews] = 3;
        }

        private Actions generateCellContent(int i, int j)
        {
            Actions cellContent;
            int randomNumber = this.generateRandomNum.Next(100);
            if (i < (this.numCols / 4))
            {
                cellContent = generateCellBasedOnQAndRandomNumber(0, randomNumber);
            }
            else if (i < (2 * this.numCols / 4))
            {
                cellContent = generateCellBasedOnQAndRandomNumber(1, randomNumber);
            }
            else if (i < (3 * this.numCols / 4))
            {
                cellContent = generateCellBasedOnQAndRandomNumber(2, randomNumber);
            }
            else
            {
                cellContent = generateCellBasedOnQAndRandomNumber(3, randomNumber);
            }
            return cellContent;
        }

        private Actions generateCellBasedOnQAndRandomNumber(int quarter, int randomNumber)
        {
            int currentRange = 0, i = 0;
            Actions currentAction = Actions.None;
            while (currentRange < randomNumber)
            {
                currentRange += this.percentagesOfActionsPerQ[quarter][(Actions)i];
                currentAction = (Actions)i;
                i++;
            }
            return currentAction;
        }

        private Image transformActionEnumToImage(int i, int j)
        {
            switch (this.matrix[i, j]) {
                case Actions.None:
                    return null;
                    break;
                case Actions.Advisor:
                    return GeneticStartupsWindows.Properties.Resources.advisor_greedy;
                    break;
                case Actions.Circus:
                    return GeneticStartupsWindows.Properties.Resources.startups_circus;
                    break;
                case Actions.Team:
                    return GeneticStartupsWindows.Properties.Resources.entrepreneur_team;
                    break;
                case Actions.Product:
                    return GeneticStartupsWindows.Properties.Resources.product_release;
                    break;
                case Actions.Feedback:
                    return GeneticStartupsWindows.Properties.Resources.entrepreneur_customer_feedback;
                    break;
                case Actions.Investor:
                    return GeneticStartupsWindows.Properties.Resources.investor;
                    break;
                case Actions.Doubts:
                    return GeneticStartupsWindows.Properties.Resources.entrepreneur_starting;
                    break;
                case Actions.Sales:
                    return GeneticStartupsWindows.Properties.Resources.entrepreneur_success;
                    break;
                case Actions.BadNews:
                    return GeneticStartupsWindows.Properties.Resources.entrepreneur_failure;
                    break;
                default:
                    return null;
                    break;
            }
        }
    }
}
