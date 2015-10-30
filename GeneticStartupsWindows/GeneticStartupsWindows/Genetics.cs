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

        private Random generateRandomNum;
        private int numCols;
        private int numRows;

        public bool[][] population;
        public List<KeyValuePair<int, int>> populationIndividualScores;

        public int numOfBinaryDigitsForStartCells;
        public int numOfBinaryDigitsForSteps;
        public int possibleDirections = 4;

        // -----------------------------
        //  Public methods
        // -----------------------------

        public Genetics(int numCols, int numRows)
        {
            this.numCols = numCols;
            this.numRows = numRows;
            this.generateRandomNum = new Random();
            this.generatePercentagesOfActionsPerQ();
        }

        public void createBoard()
        {
            matrix = new Actions[this.numCols, this.numRows];
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

        public void generatePopulation(int populationSize, int stepsAllowed)
        {
            this.numOfBinaryDigitsForStartCells = (int)Math.Ceiling(Math.Log(this.numRows, 2));
            this.numOfBinaryDigitsForSteps = (int)Math.Ceiling(Math.Log(this.possibleDirections, 2)) * stepsAllowed;
            //this.population = new bool[populationSize, numOfBinaryDigitsForStartCells + numOfBinaryDigitsForSteps];
            this.population = new bool[populationSize][];
            for (int i = 0; i < populationSize; i++)
                this.population[i] = new bool[numOfBinaryDigitsForStartCells + numOfBinaryDigitsForSteps];
        }

        public void generateScores()
        {
            this.populationIndividualScores = new List<KeyValuePair<int, int>>();
            for (int i = 0; i < this.population.Length; i++)
                this.populationIndividualScores.Add(new KeyValuePair<int, int>(i, this.fitness(this.population[i])));
            this.populationIndividualScores.Sort((x, y) => x.Value.CompareTo(y.Value));
        }


        // -----------------------------
        //  Private methods
        // -----------------------------

        private int fitness(bool[] individual)
        {
            // Now we should check the squares the individual is visiting and
            // must have a data structure with the possible values of each action
            // E.G. advisor is a random number between -15 and 5
            // Finish is -50 (failure) or 50 (success)
            // To sort multiple winning solutions we add the number of remaining squares to the score
            // E.G. If winning in the 5th square out of 20, the score will be 50 + 15 = 65;
            return 0;
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
                cellContent = generateCellContentBasedOnQ(0, randomNumber);
            }
            else if (i < (2 * this.numCols / 4))
            {
                cellContent = generateCellContentBasedOnQ(1, randomNumber);
            }
            else if (i < (3 * this.numCols / 4))
            {
                cellContent = generateCellContentBasedOnQ(2, randomNumber);
            }
            else
            {
                cellContent = generateCellContentBasedOnQ(3, randomNumber);
            }
            return cellContent;
        }

        private Actions generateCellContentBasedOnQ(int quarter, int randomNumber)
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
