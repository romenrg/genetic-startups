using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeneticStartupsWindows
{
    public class UIAritmetics
    {
        public System.Drawing.Point calculateTableLocationCenteredInContainer(Size tableSize, System.Drawing.Size containerSize)
        {
            System.Drawing.Point location = new System.Drawing.Point((containerSize.Width - tableSize.Width) / 2, (containerSize.Height - tableSize.Height) / 2);
            return location;
        }

        public System.Drawing.Size calculateClientSize(Size tableLayoutPanelSize, Size menuStripSize, int cellHeight, int cellWidth, Size button1Size, Size button2Size)
        {
            System.Drawing.Size clientSize;
            int clientHeight = tableLayoutPanelSize.Height + menuStripSize.Height + cellHeight * 3;
            int clientwidthBasedOnTable = tableLayoutPanelSize.Width + cellWidth * 2;
            int clientWidthBasedOnButtons = (button1Size.Width + button2Size.Width) * 5 / 3;
            if (clientwidthBasedOnTable < clientWidthBasedOnButtons)
            {
                clientSize = new System.Drawing.Size(clientWidthBasedOnButtons, clientHeight);
            }
            else
            {
                clientSize = new System.Drawing.Size(clientwidthBasedOnTable, clientHeight);
            }
            return clientSize;
        }

    }
}
