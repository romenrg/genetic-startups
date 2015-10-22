using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeneticStartupsWindows
{
    class Genetics
    {
        public enum Actions { Advisor, Circus, Team, Product, Feedback, Investor}
        public enum States { Confusion, Success, Failure}

        private Actions [,] matrix;
        private int numCols;
        private int numRows;

        public Genetics(int numCols, int numRows)
        {
            this.numCols = numCols;
            this.numRows = numRows;
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

        private Actions generateCellContent(int i, int j)
        {
            Actions cellContent;
            int randomNumber = new Random().Next(100);
            if (i < (this.numCols / 4))
            {
                cellContent = generateCellContentFirstQuarter(randomNumber);
            }
            else
            {
                cellContent = generateCellContentSecondQuarter(randomNumber);
            }
            return cellContent;
        }

        private Actions generateCellContentFirstQuarter(int randomNumber)
        {
            return Actions.Advisor;
        }

        private Actions generateCellContentSecondQuarter(int randomNumber)
        {
            return Actions.Circus;
        }

        private Image transformActionEnumToImage(int i, int j)
        {
            switch (this.matrix[i, j]) {
                case Actions.Advisor:
                    return GeneticStartupsWindows.Properties.Resources.entrepreneur_starting;
                    break;
                case Actions.Circus:
                    return GeneticStartupsWindows.Properties.Resources.startups_circus;
                    break;
                default:
                    return GeneticStartupsWindows.Properties.Resources.startups_circus;
                    break;
            }
        }
    }
}
