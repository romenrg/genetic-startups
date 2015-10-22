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
        public enum Actions { None=0, Advisor=1, Circus=2, Team=3, Product=4, Feedback=5, Investor=6}
        public enum States { Confusion=0, Success=1, Failure=2}

        public Actions [,] matrix { get; set; }
        public Dictionary<Actions, int>[] percentagesOfActionsPerQ { get; set; }

        private Random generateRandomNum;

        private int numCols;
        private int numRows;

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


        // -----------------------------
        //  Private methods
        // -----------------------------

        private void generatePercentagesOfActionsPerQ()
        {
            percentagesOfActionsPerQ = new Dictionary<Actions, int>[4];
            percentagesOfActionsPerQ[0] = new Dictionary<Actions, int>();
            percentagesOfActionsPerQ[0][Actions.None]     = 65;
            percentagesOfActionsPerQ[0][Actions.Advisor]  = 15;
            percentagesOfActionsPerQ[0][Actions.Circus]   = 10;
            percentagesOfActionsPerQ[0][Actions.Team]     = 4;
            percentagesOfActionsPerQ[0][Actions.Product]  = 2;
            percentagesOfActionsPerQ[0][Actions.Feedback] = 3;
            percentagesOfActionsPerQ[0][Actions.Investor] = 1;
            percentagesOfActionsPerQ[1] = new Dictionary<Actions, int>();
            percentagesOfActionsPerQ[1][Actions.None] = 45;
            percentagesOfActionsPerQ[1][Actions.Advisor] = 10;
            percentagesOfActionsPerQ[1][Actions.Circus] = 23;
            percentagesOfActionsPerQ[1][Actions.Team] = 8;
            percentagesOfActionsPerQ[1][Actions.Product] = 7;
            percentagesOfActionsPerQ[1][Actions.Feedback] = 5;
            percentagesOfActionsPerQ[1][Actions.Investor] = 2;
            percentagesOfActionsPerQ[2] = new Dictionary<Actions, int>();
            percentagesOfActionsPerQ[2][Actions.None] = 55;
            percentagesOfActionsPerQ[2][Actions.Advisor] = 5;
            percentagesOfActionsPerQ[2][Actions.Circus] = 10;
            percentagesOfActionsPerQ[2][Actions.Team] = 7;
            percentagesOfActionsPerQ[2][Actions.Product] = 10;
            percentagesOfActionsPerQ[2][Actions.Feedback] = 10;
            percentagesOfActionsPerQ[2][Actions.Investor] = 3;
            percentagesOfActionsPerQ[3] = new Dictionary<Actions, int>();
            percentagesOfActionsPerQ[3][Actions.None] = 70;
            percentagesOfActionsPerQ[3][Actions.Advisor] = 3;
            percentagesOfActionsPerQ[3][Actions.Circus] = 5;
            percentagesOfActionsPerQ[3][Actions.Team] = 5;
            percentagesOfActionsPerQ[3][Actions.Product] = 3;
            percentagesOfActionsPerQ[3][Actions.Feedback] = 9;
            percentagesOfActionsPerQ[3][Actions.Investor] = 5;
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
                currentRange += percentagesOfActionsPerQ[quarter][(Actions)i];
                currentAction = (Actions)i;
                i++;
            }
            return currentAction;
        }

        private Actions generateCellContentSecondQuarter(int randomNumber)
        {
            return Actions.Circus;
        }

        private Image transformActionEnumToImage(int i, int j)
        {
            //public enum Actions { None=0, Advisor=1, Circus=2, Team=3, Product=4, Feedback=5, Investor=6}
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
                default:
                    return null;
                    break;
            }
        }
    }
}
