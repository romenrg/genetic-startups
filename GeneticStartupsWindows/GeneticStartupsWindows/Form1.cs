using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GeneticStartupsWindows
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            // Example on how to create all the layout programatically
            // http://stackoverflow.com/questions/28255438/programmatically-create-panel-and-add-picture-boxes
        }

        private void button1_Click(object sender, EventArgs e)
        {
            // In future maybe set the number of columns and rows like
            // http://stackoverflow.com/questions/15623461/adding-pictureboxes-to-tablelayoutpanel-is-very-slow

            // Starting
            this.pictureBox1.Image = GeneticStartupsWindows.Properties.Resources.entrepreneur_starting;
            pictureBox1.SizeMode = PictureBoxSizeMode.Zoom;
            //Properties.entrepreneur_starting.jpg");
        }

        private void button2_Click(object sender, EventArgs e)
        {

        }
    }
}
