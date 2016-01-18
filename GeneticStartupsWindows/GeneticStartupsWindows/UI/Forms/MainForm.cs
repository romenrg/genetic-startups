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
    public partial class MainForm : Form
    {
        private Genetics genetics;
        private int numCols = 24;
        private int numRows = 11;
        private int numSteps = 35;
        private int cellWidth = 45;
        private int cellHeight = 45;
        private int cellPadding = 5;
        private UIAritmetics uiAritmetics;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.PictureBox[,] tableCells;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem settingsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem mapSizeToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem infoToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem algorithmToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem squaresToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem licenseToolStripMenuItem;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.PictureBox pictureBox2;

        // -----------------------------
        //  Public methods
        // -----------------------------

        public MainForm()
        {
            //InitializeComponent();
            // Initialize helpers
            this.uiAritmetics = new UIAritmetics();
            // Generate first screen (including table)
            this.createBasicLayout();
            this.createDynamicTable();
            this.setLayout();
        }

        public void createBasicLayout()
        {
            //Icon
            this.Icon = GeneticStartupsWindows.Properties.Resources.entrepreneur_starting_ico;
            // Creating elements
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.label1 = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.settingsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.mapSizeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.infoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.algorithmToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.squaresToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.licenseToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tableLayoutPanel1.SuspendLayout();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // label1
            this.label1.Name = "label1";
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Size = new System.Drawing.Size(200, 23);
            this.label1.AutoSize = true;
            this.label1.TabIndex = 4;
            this.label1.Text = "......... Welcome to Genetic Startups world .........";
            // button1
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(92, 23);
            this.button1.TabIndex = 1;
            this.button1.Text = "Generate Map";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // button2
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(90, 23);
            this.button2.TabIndex = 2;
            this.button2.Text = "Start Evolution";
            this.button2.Enabled = false;
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            this.button2.Enabled = false;
            // Form1
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "Form1";
            this.Text = "Genetic Startups, by @romenrg";
            this.Load += new System.EventHandler(this.Form1_Load);
            // menuStrip1
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
                                                this.infoToolStripMenuItem,
                                                this.settingsToolStripMenuItem
                                          });
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(473, 24);
            this.menuStrip1.TabIndex = 3;
            this.menuStrip1.Text = "Menu";
            // infoToolStripMenuItem
            this.infoToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
                this.algorithmToolStripMenuItem,
                this.squaresToolStripMenuItem,
                this.licenseToolStripMenuItem
            });
            this.infoToolStripMenuItem.Name = "infoToolStripMenuItem";
            this.infoToolStripMenuItem.Size = new System.Drawing.Size(61, 20);
            this.infoToolStripMenuItem.Text = "Info";
            // algorithmToolStripMenuItem
            this.algorithmToolStripMenuItem.Name = "algorithmStripMenuItem";
            this.algorithmToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.algorithmToolStripMenuItem.Text = "The Algorithm";
            this.algorithmToolStripMenuItem.Click += new System.EventHandler(this.algorithmToolStripMenuItem_Click);
            // squaresToolStripMenuItem
            this.squaresToolStripMenuItem.Name = "squaresToolStripMenuItem";
            this.squaresToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.squaresToolStripMenuItem.Text = "Square Types";
            this.squaresToolStripMenuItem.Click += new System.EventHandler(this.squaresToolStripMenuItem_Click);
            // licenseToolStripMenuItem
            this.licenseToolStripMenuItem.Name = "licenseStripMenuItem";
            this.licenseToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.licenseToolStripMenuItem.Text = "License";
            this.licenseToolStripMenuItem.Click += new System.EventHandler(this.licenseToolStripMenuItem_Click);
            // settingsToolStripMenuItem
            this.settingsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mapSizeToolStripMenuItem});
            this.settingsToolStripMenuItem.Name = "settingsToolStripMenuItem";
            this.settingsToolStripMenuItem.Size = new System.Drawing.Size(61, 20);
            this.settingsToolStripMenuItem.Text = "Settings";
            // mapSizeToolStripMenuItem
            this.mapSizeToolStripMenuItem.Name = "mapSizeToolStripMenuItem";
            this.mapSizeToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.mapSizeToolStripMenuItem.Text = "Map Size";
            this.mapSizeToolStripMenuItem.Click += new System.EventHandler(this.mapSizeToolStripMenuItem_Click);
        }

        public void createDynamicTable()
        {
            this.tableLayoutPanel1.Controls.Clear();
            this.tableLayoutPanel1.RowStyles.Clear();
            this.tableLayoutPanel1.CellBorderStyle = System.Windows.Forms.TableLayoutPanelCellBorderStyle.Inset;
            this.tableLayoutPanel1.ColumnCount = this.numCols;
            this.tableLayoutPanel1.RowCount = this.numRows;
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableCells = new System.Windows.Forms.PictureBox[this.numCols, this.numRows];
            for (int i=0; i<this.numCols; i++)
            {
                this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, this.cellHeight));
                for (int j=0; j<this.numRows; j++) {
                    this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, this.cellWidth));
                    this.tableCells[i, j] = new System.Windows.Forms.PictureBox();
                    this.tableLayoutPanel1.Controls.Add(this.tableCells[i, j], i, j);
                }
            }
            int tableWidth = (this.numCols * (this.cellWidth + 2)) + 2;
            int tableHeight = (this.numRows * (this.cellHeight + 2)) + 2;
            this.tableLayoutPanel1.Size = new System.Drawing.Size(tableWidth, tableHeight);
        }

        public void setLayout()
        {
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = this.uiAritmetics.calculateClientSize(this.tableLayoutPanel1.Size, this.menuStrip1.Size, this.cellHeight, this.cellWidth, this.button1.Size, this.button2.Size);
            this.tableLayoutPanel1.Location = this.uiAritmetics.calculateTableLocationCenteredInContainer(this.tableLayoutPanel1.Size, this.ClientSize);
            this.label1.Location = new System.Drawing.Point(this.ClientSize.Width / 2 - this.label1.Size.Width / 2, this.menuStrip1.Size.Height + this.cellHeight * 2 / 5);
            this.button1.Location = new System.Drawing.Point(this.ClientSize.Width / 5, this.tableLayoutPanel1.Size.Height + this.menuStrip1.Size.Height + this.cellHeight * 5 / 3);
            this.button2.Location = new System.Drawing.Point(this.ClientSize.Width / 5 * 4 - this.button2.Size.Width, this.tableLayoutPanel1.Size.Height + this.menuStrip1.Size.Height + this.cellHeight * 5 / 3);
            this.Load += new System.EventHandler(this.Form1_Load);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        public void generateMap()
        {
            // In future maybe set the number of columns and rows like
            // http://stackoverflow.com/questions/15623461/adding-pictureboxes-to-tablelayoutpanel-is-very-slow
            Cursor.Current = Cursors.WaitCursor;
            this.genetics = new Genetics(this.numCols, this.numRows, this.numSteps);
            this.genetics.createBoard();
            for (int i = 0; i < this.tableLayoutPanel1.ColumnCount; i++)
            {
                for (int j = 0; j < this.tableLayoutPanel1.RowCount; j++)
                {
                    this.tableCells[i, j].Image = this.genetics.getIconForPos(i, j);
                    this.tableCells[i, j].SizeMode = PictureBoxSizeMode.StretchImage;
                }
            }
            Cursor.Current = Cursors.Default;
            this.button2.Enabled = true;
        }


        // -----------------------------
        //  Private methods
        // -----------------------------
        private async Task showBestCandidateOfGeneration()
        {
            Tuple<int, int>[] bestIndividualCellsPath = genetics.getBestIndividualCellsPath();
            int x, y;
            for (int j = 0; j < bestIndividualCellsPath.Length; j++)
            {
                x = bestIndividualCellsPath[j].Item1;
                y = bestIndividualCellsPath[j].Item2;
                if (genetics.isCellInsideMap(x, y))
                {
                    this.applyStylesToVisitedCell(x, y);
                    await Task.Delay(750);
                }
            }
        }

        private void applyStylesToVisitedCell(int x, int y)
        {
            this.tableCells[x, y].Padding = new Padding(this.cellPadding);
            if (this.tableCells[x, y].BackColor == Color.LightBlue)
            {
                this.tableCells[x, y].BackColor = Color.DeepSkyBlue;
            }
            else if (this.tableCells[x, y].BackColor == Color.DeepSkyBlue)
            {
                this.tableCells[x, y].BackColor = Color.Blue;
            }
            else if (this.tableCells[x, y].BackColor == Color.Blue)
            {
                this.tableCells[x, y].BackColor = Color.Blue;
            }
            else
            {
                this.tableCells[x, y].BackColor = Color.LightBlue;
            }
        }

        private void cleanMap()
        {
            for (int i = 0; i<this.numCols; i++)
            {
                for (int j=0; j<this.numRows; j++)
                {
                    this.tableCells[i, j].BackColor = default(Color);
                    this.tableCells[i, j].Padding = new Padding(0);
                }
            }
        }


        // -----------------------------
        //  Event Listeners
        // -----------------------------

        private void button1_Click(object sender, EventArgs e)
        {
            this.generateMap();
            this.button2.Enabled = true;
        }

        private async void button2_Click(object sender, EventArgs e)
        {
            this.genetics.generatePopulation(25);
            this.genetics.generateScores();
            this.label1.Text = "Generation: 1" + " / " + Genetics.NUM_GENERATIONS + ". Best score (displayed): " + genetics.individualsSortedByScore[0].Value;
            for (int i = 1; i < Genetics.NUM_GENERATIONS; i++)
            {
                await this.showBestCandidateOfGeneration();
                this.cleanMap();
                await Task.Delay(750);
                this.genetics.newGeneration();
                this.genetics.generateScores();
                this.label1.Text = "Generation: " + (i + 1) + " / " + Genetics.NUM_GENERATIONS + ". Best score (displayed): " + genetics.individualsSortedByScore[0].Value;
            }
            await this.showBestCandidateOfGeneration();
        }

        private void mapSizeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MapSizeform form2 = new MapSizeform();
            var result = form2.ShowDialog();
            if (result == DialogResult.OK)
            {
                Cursor.Current = Cursors.WaitCursor;
                this.numCols = form2.numCols;
                this.numRows = form2.numRows;
                this.numSteps = this.numCols + this.numRows;
                this.createDynamicTable();
                this.setLayout();
                Cursor.Current = Cursors.Default;
            }
        }

        private void algorithmToolStripMenuItem_Click(object sender, EventArgs e)
        {
            TheAlgorithmForm theAlgorithmform = new TheAlgorithmForm();
            var result = theAlgorithmform.ShowDialog();
            if (result == DialogResult.OK)
            {
                Cursor.Current = Cursors.WaitCursor;
                Cursor.Current = Cursors.Default;
            }
        }

        private void squaresToolStripMenuItem_Click(object sender, EventArgs e)
        {
            TypesOfSquaresForm form3 = new TypesOfSquaresForm();
            var result = form3.ShowDialog();
            if (result == DialogResult.OK)
            {
                Cursor.Current = Cursors.WaitCursor;
                Cursor.Current = Cursors.Default;
            }
        }

        private void licenseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LicenseForm licenseForm = new LicenseForm();
            var result = licenseForm.ShowDialog();
            if (result == DialogResult.OK)
            {
                Cursor.Current = Cursors.WaitCursor;
                Cursor.Current = Cursors.Default;
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }
    }
}
