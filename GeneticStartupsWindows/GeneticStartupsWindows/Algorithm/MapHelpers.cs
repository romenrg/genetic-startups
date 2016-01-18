using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeneticStartupsWindows.Algorithm
{
    public class MapHelpers
    {
        public Dictionary<Genetics.Actions, int>[] actionProbabilityPerQ { get; set; }
        public Dictionary<Genetics.Actions, List<KeyValuePair<int, string>>> possibleScoresPerAction { get; set; }
        public Genetics.Actions[,] matrix { get; set; }
        public int numCols;
        public int numRows;
        private Random generateRandomNum;

        public MapHelpers(int numCols, int numRows) {
            this.numCols = numCols;
            this.numRows = numRows;
            this.generateRandomNum = new Random();
            this.generatePercentagesOfActionsPerQ();
            this.generateScorePossibilitiesPerAction();
        }

        public void createBoard()
        {
            this.matrix = new Genetics.Actions[this.numCols, this.numRows];
            for (int i = 0; i < this.numCols; i++)
            {
                for (int j = 0; j < this.numRows; j++)
                {
                    this.matrix[i, j] = generateCellContent(i, j);
                }
            }
        }

        public bool movingRight(int[] movement)
        {
            return ((movement[0] == 0) && (movement[1] == 0)) || ((movement[0] == 1) && (movement[1] == 0));
        }

        public bool movingDown(int[] movement)
        {
            return (movement[0] == 0) && (movement[1] == 1);
        }

        public bool movingLeft(int[] movement)
        {
            return false;
        }

        public bool movingUp(int[] movement)
        {
            return (movement[0] == 1) && (movement[1] == 1);
        }

        public bool isCellInsideMap(int x, int y)
        {
            return (x >= 0) && (x < this.numCols) && (y >= 0) && (y < this.numRows);
        }

        public int calculateSquareValue(int x, int y)
        {
            if (this.isCellInsideMap(x, y))
            {
                Genetics.Actions squareAction = this.matrix[x, y];
                int squareValue = 0;
                for (int i = 0; i < this.possibleScoresPerAction[squareAction].Count; i++)
                {
                    squareValue += this.possibleScoresPerAction[squareAction][i].Key;
                }
                return squareValue;
            }
            else
            {
                return Genetics.CELL_VALUE_OUT_OF_BOUNDS;
            }
        }

        public void generateScorePossibilitiesPerAction()
        {
            this.possibleScoresPerAction = new Dictionary<Genetics.Actions, List<KeyValuePair<int, string>>>();
            this.possibleScoresPerAction[Genetics.Actions.None] = new List<KeyValuePair<int, string>>();
            this.possibleScoresPerAction[Genetics.Actions.None].Add(new KeyValuePair<int, string>(-1, "Feeling that things don't go as fast as expeceted..."));
            this.possibleScoresPerAction[Genetics.Actions.None].Add(new KeyValuePair<int, string>(-1, "Feeling that things don't go as fast as expeceted..."));
            this.possibleScoresPerAction[Genetics.Actions.None].Add(new KeyValuePair<int, string>(0, "Just one more day in the startup world!"));
            this.possibleScoresPerAction[Genetics.Actions.None].Add(new KeyValuePair<int, string>(0, "Just one more day in the startup world!"));
            this.possibleScoresPerAction[Genetics.Actions.None].Add(new KeyValuePair<int, string>(0, "Just one more day in the startup world!"));
            this.possibleScoresPerAction[Genetics.Actions.None].Add(new KeyValuePair<int, string>(0, "Just one more day in the startup world!"));
            this.possibleScoresPerAction[Genetics.Actions.None].Add(new KeyValuePair<int, string>(0, "Just one more day in the startup world!"));
            this.possibleScoresPerAction[Genetics.Actions.None].Add(new KeyValuePair<int, string>(0, "Just one more day in the startup world!"));
            this.possibleScoresPerAction[Genetics.Actions.None].Add(new KeyValuePair<int, string>(1, "One day closer to success!"));
            this.possibleScoresPerAction[Genetics.Actions.None].Add(new KeyValuePair<int, string>(1, "One day closer to success!"));
            this.possibleScoresPerAction[Genetics.Actions.Advisor] = new List<KeyValuePair<int, string>>();
            this.possibleScoresPerAction[Genetics.Actions.Advisor].Add(new KeyValuePair<int, string>(-15, "The 'advisor' turned out to be a liar, had no idea but took big shares"));
            this.possibleScoresPerAction[Genetics.Actions.Advisor].Add(new KeyValuePair<int, string>(-10, "The 'advisor' had no idea, gave wrong advice and company suffered"));
            this.possibleScoresPerAction[Genetics.Actions.Advisor].Add(new KeyValuePair<int, string>(-6, "You realized the 'advisor' won't help at all"));
            this.possibleScoresPerAction[Genetics.Actions.Advisor].Add(new KeyValuePair<int, string>(-6, "You realized the 'advisor' won't help at all"));
            this.possibleScoresPerAction[Genetics.Actions.Advisor].Add(new KeyValuePair<int, string>(-4, "The 'advisor' just wanted to sell his/her services"));
            this.possibleScoresPerAction[Genetics.Actions.Advisor].Add(new KeyValuePair<int, string>(-4, "The 'advisor' just wanted to sell his/her services"));
            this.possibleScoresPerAction[Genetics.Actions.Advisor].Add(new KeyValuePair<int, string>(-4, "The 'advisor' just wanted to sell his/her services"));
            this.possibleScoresPerAction[Genetics.Actions.Advisor].Add(new KeyValuePair<int, string>(2, "The 'advisor' nows about the market and may be helful"));
            this.possibleScoresPerAction[Genetics.Actions.Advisor].Add(new KeyValuePair<int, string>(5, "The 'advisor' is connected to investors in the field"));
            this.possibleScoresPerAction[Genetics.Actions.Advisor].Add(new KeyValuePair<int, string>(10, "The 'advisor' will bring important customers"));
            this.possibleScoresPerAction[Genetics.Actions.Circus] = new List<KeyValuePair<int, string>>();
            this.possibleScoresPerAction[Genetics.Actions.Circus].Add(new KeyValuePair<int, string>(-5, "You should have been working instead; you wasted a lot of time"));
            this.possibleScoresPerAction[Genetics.Actions.Circus].Add(new KeyValuePair<int, string>(-5, "You should have been working instead; you wasted a lot of time"));
            this.possibleScoresPerAction[Genetics.Actions.Circus].Add(new KeyValuePair<int, string>(-5, "You should have been working instead; you wasted a lot of time"));
            this.possibleScoresPerAction[Genetics.Actions.Circus].Add(new KeyValuePair<int, string>(-5, "You should have been working instead; you wasted a lot of time"));
            this.possibleScoresPerAction[Genetics.Actions.Circus].Add(new KeyValuePair<int, string>(-5, "You should have been working instead; you wasted a lot of time"));
            this.possibleScoresPerAction[Genetics.Actions.Circus].Add(new KeyValuePair<int, string>(-2, "You just wasted some time going there, not that bad"));
            this.possibleScoresPerAction[Genetics.Actions.Circus].Add(new KeyValuePair<int, string>(-2, "You just wasted some time going there, not that bad"));
            this.possibleScoresPerAction[Genetics.Actions.Circus].Add(new KeyValuePair<int, string>(2, "Maybe someone you met today will help you in future"));
            this.possibleScoresPerAction[Genetics.Actions.Circus].Add(new KeyValuePair<int, string>(5, "You built useful connections (either partners or potential investors)"));
            this.possibleScoresPerAction[Genetics.Actions.Circus].Add(new KeyValuePair<int, string>(7, "You built very useful connections (someone important or well connected)"));
            this.possibleScoresPerAction[Genetics.Actions.Team] = new List<KeyValuePair<int, string>>();
            this.possibleScoresPerAction[Genetics.Actions.Team].Add(new KeyValuePair<int, string>(-15, "You picked a troublemaker as founder and gave him/her 50% of shares"));
            this.possibleScoresPerAction[Genetics.Actions.Team].Add(new KeyValuePair<int, string>(-3, "The new team member has just left college and is inexperienced"));
            this.possibleScoresPerAction[Genetics.Actions.Team].Add(new KeyValuePair<int, string>(-2, "Another person with the same profile joined"));
            this.possibleScoresPerAction[Genetics.Actions.Team].Add(new KeyValuePair<int, string>(2, "Regular worker joined the company"));
            this.possibleScoresPerAction[Genetics.Actions.Team].Add(new KeyValuePair<int, string>(2, "Regular worker joined the company"));
            this.possibleScoresPerAction[Genetics.Actions.Team].Add(new KeyValuePair<int, string>(2, "Regular worker joined the company"));
            this.possibleScoresPerAction[Genetics.Actions.Team].Add(new KeyValuePair<int, string>(5, "Talented employee joined the company"));
            this.possibleScoresPerAction[Genetics.Actions.Team].Add(new KeyValuePair<int, string>(5, "Talented employee joined the company"));
            this.possibleScoresPerAction[Genetics.Actions.Team].Add(new KeyValuePair<int, string>(7, "Talented employee with startup experience joined the company"));
            this.possibleScoresPerAction[Genetics.Actions.Team].Add(new KeyValuePair<int, string>(7, "Talented person with startup experience and connections in the field joined as co-founder"));
            this.possibleScoresPerAction[Genetics.Actions.Product] = new List<KeyValuePair<int, string>>();
            this.possibleScoresPerAction[Genetics.Actions.Product].Add(new KeyValuePair<int, string>(-3, "You invested too much (time and money) in your MVP"));
            this.possibleScoresPerAction[Genetics.Actions.Product].Add(new KeyValuePair<int, string>(3, "You released an MVP or a very small increment to test the market"));
            this.possibleScoresPerAction[Genetics.Actions.Product].Add(new KeyValuePair<int, string>(3, "You released an MVP or a very small increment to test the market"));
            this.possibleScoresPerAction[Genetics.Actions.Product].Add(new KeyValuePair<int, string>(3, "You released an MVP or a very small increment to test the market"));
            this.possibleScoresPerAction[Genetics.Actions.Product].Add(new KeyValuePair<int, string>(3, "You released an MVP or a very small increment to test the market"));
            this.possibleScoresPerAction[Genetics.Actions.Product].Add(new KeyValuePair<int, string>(3, "You released an MVP or a very small increment to test the market"));
            this.possibleScoresPerAction[Genetics.Actions.Product].Add(new KeyValuePair<int, string>(4, "You released new features after listening to customer feedback and testing the market"));
            this.possibleScoresPerAction[Genetics.Actions.Product].Add(new KeyValuePair<int, string>(4, "You released new features after listening to customer feedback and testing the market"));
            this.possibleScoresPerAction[Genetics.Actions.Product].Add(new KeyValuePair<int, string>(4, "You released new features after listening to customer feedback and testing the market"));
            this.possibleScoresPerAction[Genetics.Actions.Product].Add(new KeyValuePair<int, string>(5, "You embraced Agile methodologies: product delivery is optimized and work environment improved significantly"));
            this.possibleScoresPerAction[Genetics.Actions.Feedback] = new List<KeyValuePair<int, string>>();
            this.possibleScoresPerAction[Genetics.Actions.Feedback].Add(new KeyValuePair<int, string>(1, "You included polls in your product"));
            this.possibleScoresPerAction[Genetics.Actions.Feedback].Add(new KeyValuePair<int, string>(2, "You read customer emails and comments"));
            this.possibleScoresPerAction[Genetics.Actions.Feedback].Add(new KeyValuePair<int, string>(2, "You read customer emails and comments"));
            this.possibleScoresPerAction[Genetics.Actions.Feedback].Add(new KeyValuePair<int, string>(3, "You did one usability test with a friend"));
            this.possibleScoresPerAction[Genetics.Actions.Feedback].Add(new KeyValuePair<int, string>(3, "You did one usability test with a friend"));
            this.possibleScoresPerAction[Genetics.Actions.Feedback].Add(new KeyValuePair<int, string>(5, "You did one usability test with a potential customer"));
            this.possibleScoresPerAction[Genetics.Actions.Feedback].Add(new KeyValuePair<int, string>(5, "You did one usability test with a potential customer"));
            this.possibleScoresPerAction[Genetics.Actions.Feedback].Add(new KeyValuePair<int, string>(6, "You are tracking user events and reviewing analytics to improve"));
            this.possibleScoresPerAction[Genetics.Actions.Feedback].Add(new KeyValuePair<int, string>(6, "You are tracking user events and reviewing analytics to improve"));
            this.possibleScoresPerAction[Genetics.Actions.Feedback].Add(new KeyValuePair<int, string>(8, "You performed an A/B test to be sure which change better"));
            this.possibleScoresPerAction[Genetics.Actions.Investor] = new List<KeyValuePair<int, string>>();
            this.possibleScoresPerAction[Genetics.Actions.Investor].Add(new KeyValuePair<int, string>(2, "You get money from FFF"));
            this.possibleScoresPerAction[Genetics.Actions.Investor].Add(new KeyValuePair<int, string>(3, "An investor with no tech experience nor startup experience joined"));
            this.possibleScoresPerAction[Genetics.Actions.Investor].Add(new KeyValuePair<int, string>(3, "An investor with no tech experience nor startup experience joined"));
            this.possibleScoresPerAction[Genetics.Actions.Investor].Add(new KeyValuePair<int, string>(5, "An investor with tech experience but no startup experience joined"));
            this.possibleScoresPerAction[Genetics.Actions.Investor].Add(new KeyValuePair<int, string>(7, "An investor with startup experience (in other fields) joined"));
            this.possibleScoresPerAction[Genetics.Actions.Investor].Add(new KeyValuePair<int, string>(7, "An investor with startup experience (in other fields) joined"));
            this.possibleScoresPerAction[Genetics.Actions.Investor].Add(new KeyValuePair<int, string>(10, "An investor with startup experience in your field joined"));
            this.possibleScoresPerAction[Genetics.Actions.Investor].Add(new KeyValuePair<int, string>(10, "An investor with startup experience in your field joined"));
            this.possibleScoresPerAction[Genetics.Actions.Investor].Add(new KeyValuePair<int, string>(15, "An investor with startup experience and contacts in your field joined, bringing customers"));
            this.possibleScoresPerAction[Genetics.Actions.Investor].Add(new KeyValuePair<int, string>(15, "An investor with startup experience and contacts in your field joined, bringing customers"));
            this.possibleScoresPerAction[Genetics.Actions.Doubts] = new List<KeyValuePair<int, string>>();
            this.possibleScoresPerAction[Genetics.Actions.Doubts].Add(new KeyValuePair<int, string>(-2, "You have doubts and feel lost"));
            this.possibleScoresPerAction[Genetics.Actions.Doubts].Add(new KeyValuePair<int, string>(-1, "You have doubts and feel lost"));
            this.possibleScoresPerAction[Genetics.Actions.Doubts].Add(new KeyValuePair<int, string>(-1, "You have doubts and feel lost"));
            this.possibleScoresPerAction[Genetics.Actions.Doubts].Add(new KeyValuePair<int, string>(-1, "You have doubts and feel lost"));
            this.possibleScoresPerAction[Genetics.Actions.Doubts].Add(new KeyValuePair<int, string>(-1, "You have doubts and feel lost"));
            this.possibleScoresPerAction[Genetics.Actions.Doubts].Add(new KeyValuePair<int, string>(-1, "You have doubts and feel lost"));
            this.possibleScoresPerAction[Genetics.Actions.Doubts].Add(new KeyValuePair<int, string>(1, "You have doubts, but that motivates you to try new things"));
            this.possibleScoresPerAction[Genetics.Actions.Doubts].Add(new KeyValuePair<int, string>(1, "You have doubts, but that motivates you to try new things"));
            this.possibleScoresPerAction[Genetics.Actions.Doubts].Add(new KeyValuePair<int, string>(1, "You have doubts, but that motivates you to try new things"));
            this.possibleScoresPerAction[Genetics.Actions.Doubts].Add(new KeyValuePair<int, string>(2, "You have doubts, but that motivates you to try new things"));
            this.possibleScoresPerAction[Genetics.Actions.Sales] = new List<KeyValuePair<int, string>>();
            this.possibleScoresPerAction[Genetics.Actions.Sales].Add(new KeyValuePair<int, string>(7, "Sold the product to a small customer (or small group)"));
            this.possibleScoresPerAction[Genetics.Actions.Sales].Add(new KeyValuePair<int, string>(7, "Sold the product to a small customer (or small group)"));
            this.possibleScoresPerAction[Genetics.Actions.Sales].Add(new KeyValuePair<int, string>(7, "Sold the product to a small customer (or small group)"));
            this.possibleScoresPerAction[Genetics.Actions.Sales].Add(new KeyValuePair<int, string>(7, "Sold the product to a small customer (or small group)"));
            this.possibleScoresPerAction[Genetics.Actions.Sales].Add(new KeyValuePair<int, string>(7, "Sold the product to a small customer (or small group)"));
            this.possibleScoresPerAction[Genetics.Actions.Sales].Add(new KeyValuePair<int, string>(7, "Sold the product to a small customer (or small group)"));
            this.possibleScoresPerAction[Genetics.Actions.Sales].Add(new KeyValuePair<int, string>(7, "Sold the product to a small customer (or small group)"));
            this.possibleScoresPerAction[Genetics.Actions.Sales].Add(new KeyValuePair<int, string>(15, "Sold the product to a medium-size customer (or medium-size group)"));
            this.possibleScoresPerAction[Genetics.Actions.Sales].Add(new KeyValuePair<int, string>(15, "Sold the product to a medium-size customer (or medium-size group)"));
            this.possibleScoresPerAction[Genetics.Actions.Sales].Add(new KeyValuePair<int, string>(25, "Sold the product to a big customer (or big group)"));
            this.possibleScoresPerAction[Genetics.Actions.BadNews] = new List<KeyValuePair<int, string>>();
            this.possibleScoresPerAction[Genetics.Actions.BadNews].Add(new KeyValuePair<int, string>(-15, "Bad news..."));
            this.possibleScoresPerAction[Genetics.Actions.BadNews].Add(new KeyValuePair<int, string>(-10, "Bad news..."));
            this.possibleScoresPerAction[Genetics.Actions.BadNews].Add(new KeyValuePair<int, string>(-5, "Bad news..."));
            this.possibleScoresPerAction[Genetics.Actions.BadNews].Add(new KeyValuePair<int, string>(-5, "Bad news..."));
            this.possibleScoresPerAction[Genetics.Actions.BadNews].Add(new KeyValuePair<int, string>(-5, "Bad news..."));
            this.possibleScoresPerAction[Genetics.Actions.BadNews].Add(new KeyValuePair<int, string>(-5, "Bad news..."));
            this.possibleScoresPerAction[Genetics.Actions.BadNews].Add(new KeyValuePair<int, string>(-5, "Bad news..."));
            this.possibleScoresPerAction[Genetics.Actions.BadNews].Add(new KeyValuePair<int, string>(-2, "Bad news..."));
            this.possibleScoresPerAction[Genetics.Actions.BadNews].Add(new KeyValuePair<int, string>(-2, "Bad news..."));
            this.possibleScoresPerAction[Genetics.Actions.BadNews].Add(new KeyValuePair<int, string>(-1, "Bad news..."));
        }

        public void generatePercentagesOfActionsPerQ()
        {
            this.actionProbabilityPerQ = new Dictionary<Genetics.Actions, int>[4];
            this.actionProbabilityPerQ[0] = new Dictionary<Genetics.Actions, int>();
            this.actionProbabilityPerQ[0][Genetics.Actions.None] = 65;
            this.actionProbabilityPerQ[0][Genetics.Actions.Advisor] = 11;
            this.actionProbabilityPerQ[0][Genetics.Actions.Circus] = 8;
            this.actionProbabilityPerQ[0][Genetics.Actions.Team] = 4;
            this.actionProbabilityPerQ[0][Genetics.Actions.Product] = 2;
            this.actionProbabilityPerQ[0][Genetics.Actions.Feedback] = 2;
            this.actionProbabilityPerQ[0][Genetics.Actions.Investor] = 1;
            this.actionProbabilityPerQ[0][Genetics.Actions.Doubts] = 4;
            this.actionProbabilityPerQ[0][Genetics.Actions.Sales] = 1;
            this.actionProbabilityPerQ[0][Genetics.Actions.BadNews] = 2;
            this.actionProbabilityPerQ[1] = new Dictionary<Genetics.Actions, int>();
            this.actionProbabilityPerQ[1][Genetics.Actions.None] = 55;
            this.actionProbabilityPerQ[1][Genetics.Actions.Advisor] = 8;
            this.actionProbabilityPerQ[1][Genetics.Actions.Circus] = 13;
            this.actionProbabilityPerQ[1][Genetics.Actions.Team] = 4;
            this.actionProbabilityPerQ[1][Genetics.Actions.Product] = 6;
            this.actionProbabilityPerQ[1][Genetics.Actions.Feedback] = 4;
            this.actionProbabilityPerQ[1][Genetics.Actions.Investor] = 1;
            this.actionProbabilityPerQ[1][Genetics.Actions.Doubts] = 3;
            this.actionProbabilityPerQ[1][Genetics.Actions.Sales] = 1;
            this.actionProbabilityPerQ[1][Genetics.Actions.BadNews] = 5;
            this.actionProbabilityPerQ[2] = new Dictionary<Genetics.Actions, int>();
            this.actionProbabilityPerQ[2][Genetics.Actions.None] = 55;
            this.actionProbabilityPerQ[2][Genetics.Actions.Advisor] = 6;
            this.actionProbabilityPerQ[2][Genetics.Actions.Circus] = 6;
            this.actionProbabilityPerQ[2][Genetics.Actions.Team] = 4;
            this.actionProbabilityPerQ[2][Genetics.Actions.Product] = 5;
            this.actionProbabilityPerQ[2][Genetics.Actions.Feedback] = 7;
            this.actionProbabilityPerQ[2][Genetics.Actions.Investor] = 3;
            this.actionProbabilityPerQ[2][Genetics.Actions.Doubts] = 2;
            this.actionProbabilityPerQ[2][Genetics.Actions.Sales] = 4;
            this.actionProbabilityPerQ[2][Genetics.Actions.BadNews] = 8;
            this.actionProbabilityPerQ[3] = new Dictionary<Genetics.Actions, int>();
            this.actionProbabilityPerQ[3][Genetics.Actions.None] = 70;
            this.actionProbabilityPerQ[3][Genetics.Actions.Advisor] = 2;
            this.actionProbabilityPerQ[3][Genetics.Actions.Circus] = 4;
            this.actionProbabilityPerQ[3][Genetics.Actions.Team] = 3;
            this.actionProbabilityPerQ[3][Genetics.Actions.Product] = 3;
            this.actionProbabilityPerQ[3][Genetics.Actions.Feedback] = 5;
            this.actionProbabilityPerQ[3][Genetics.Actions.Investor] = 3;
            this.actionProbabilityPerQ[3][Genetics.Actions.Doubts] = 0;
            this.actionProbabilityPerQ[3][Genetics.Actions.Sales] = 7;
            this.actionProbabilityPerQ[3][Genetics.Actions.BadNews] = 3;
        }

        private Genetics.Actions generateCellContent(int i, int j)
        {
            Genetics.Actions cellContent;
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

        private Genetics.Actions generateCellBasedOnQAndRandomNumber(int quarter, int randomNumber)
        {
            int currentRange = 0, i = 0;
            Genetics.Actions currentAction = Genetics.Actions.None;
            while (currentRange < randomNumber)
            {
                currentRange += this.actionProbabilityPerQ[quarter][(Genetics.Actions)i];
                currentAction = (Genetics.Actions)i;
                i++;
            }
            return currentAction;
        }

        public Image getIconForPos(int i, int j)
        {
            return this.transformActionEnumToImage(i, j);
        }

        public Image transformActionEnumToImage(int i, int j)
        {
            switch (this.matrix[i, j])
            {
                case Genetics.Actions.None:
                    return null;
                    break;
                case Genetics.Actions.Advisor:
                    return GeneticStartupsWindows.Properties.Resources.advisor_greedy;
                    break;
                case Genetics.Actions.Circus:
                    return GeneticStartupsWindows.Properties.Resources.startups_circus;
                    break;
                case Genetics.Actions.Team:
                    return GeneticStartupsWindows.Properties.Resources.entrepreneur_team;
                    break;
                case Genetics.Actions.Product:
                    return GeneticStartupsWindows.Properties.Resources.product_release;
                    break;
                case Genetics.Actions.Feedback:
                    return GeneticStartupsWindows.Properties.Resources.entrepreneur_customer_feedback;
                    break;
                case Genetics.Actions.Investor:
                    return GeneticStartupsWindows.Properties.Resources.investor;
                    break;
                case Genetics.Actions.Doubts:
                    return GeneticStartupsWindows.Properties.Resources.entrepreneur_starting;
                    break;
                case Genetics.Actions.Sales:
                    return GeneticStartupsWindows.Properties.Resources.entrepreneur_success;
                    break;
                case Genetics.Actions.BadNews:
                    return GeneticStartupsWindows.Properties.Resources.entrepreneur_failure;
                    break;
                default:
                    return null;
                    break;
            }
        }
    }
}
