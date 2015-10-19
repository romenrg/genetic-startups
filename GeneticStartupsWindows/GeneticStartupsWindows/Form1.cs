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
            //InitializeComponent();

            // Example on how to create all the layout programatically
            // http://stackoverflow.com/questions/28255438/programmatically-create-panel-and-add-picture-boxes

            createBasicLayout();
            createDynamicTable();
            setLayout();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void createBasicLayout()
        {
            // 
            // Creating elements
            // 
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.settingsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.mapSizeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tableLayoutPanel1.SuspendLayout();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // button1
            // 
            //this.button1.Location = new System.Drawing.Point(16, 362);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(92, 23);
            this.button1.TabIndex = 1;
            this.button1.Text = "Generate Map";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // button2
            // 
            //this.button2.Location = new System.Drawing.Point(366, 362);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(90, 23);
            this.button2.TabIndex = 2;
            this.button2.Text = "Start Evolution";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // Form1
            // 
            //this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            //this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            //this.ClientSize = new System.Drawing.Size(473, 408);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "Form1";
            this.Text = "Form1";
            //this.Load += new System.EventHandler(this.Form1_Load);
            //this.tableLayoutPanel1.ResumeLayout(false);
            //((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            //((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            //this.ResumeLayout(false);
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.settingsToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(473, 24);
            this.menuStrip1.TabIndex = 3;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // settingsToolStripMenuItem
            // 
            this.settingsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mapSizeToolStripMenuItem});
            this.settingsToolStripMenuItem.Name = "settingsToolStripMenuItem";
            this.settingsToolStripMenuItem.Size = new System.Drawing.Size(61, 20);
            this.settingsToolStripMenuItem.Text = "Settings";
            // 
            // mapSizeToolStripMenuItem
            // 
            this.mapSizeToolStripMenuItem.Name = "mapSizeToolStripMenuItem";
            this.mapSizeToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.mapSizeToolStripMenuItem.Text = "Map Size";
            this.mapSizeToolStripMenuItem.Click += new System.EventHandler(this.mapSizeToolStripMenuItem_Click);
        }

        private void createDynamicTable()
        {
            this.tableLayoutPanel1.Controls.Clear();
            this.tableLayoutPanel1.RowStyles.Clear();
            this.tableLayoutPanel1.CellBorderStyle = System.Windows.Forms.TableLayoutPanelCellBorderStyle.Inset;
            this.tableLayoutPanel1.ColumnCount = this.numCols;
            this.tableLayoutPanel1.RowCount = this.numRows;
            //this.tableLayoutPanel1.Location = new System.Drawing.Point(16, 35);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableCells = new System.Windows.Forms.PictureBox[numCols, numRows];
            for (int i=0; i<numCols; i++)
            {
                this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, this.cellHeight));
                for (int j=0; j<numRows; j++) {
                    this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, this.cellWidth));
                    this.tableCells[i, j] = new System.Windows.Forms.PictureBox();
                    this.tableLayoutPanel1.Controls.Add(this.tableCells[i, j], i, j);
                }
            }
            int tableWidth = (numCols * (this.cellWidth + 2)) + 2;
            int tableHeight = (numRows * (this.cellHeight + 2)) + 2;
            this.tableLayoutPanel1.Size = new System.Drawing.Size(tableWidth, tableHeight);
        }


        private void setLayout()
        {
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;

            if((this.tableLayoutPanel1.Size.Width + this.cellWidth) < (this.button1.Size.Width + this.button1.Size.Width))
            {
                this.ClientSize = new System.Drawing.Size(this.button1.Size.Width + this.button1.Size.Width + this.cellWidth * 4, this.tableLayoutPanel1.Size.Height + menuStrip1.Size.Height + this.cellHeight * 3);
            }
            else
            {
                this.ClientSize = this.tableLayoutPanel1.Size + new System.Drawing.Size(this.cellWidth * 2, menuStrip1.Size.Height + this.cellHeight * 3);
            }

            this.tableLayoutPanel1.Location = new System.Drawing.Point((this.ClientSize.Width - this.tableLayoutPanel1.Size.Width) / 2, (this.ClientSize.Height - this.tableLayoutPanel1.Size.Height) / 2);

            this.button1.Location = new System.Drawing.Point(this.ClientSize.Width / 5, this.tableLayoutPanel1.Size.Height + menuStrip1.Size.Height + this.cellHeight * 5 / 3);
            this.button2.Location = new System.Drawing.Point(this.ClientSize.Width / 5 * 4 - this.button2.Size.Width, this.tableLayoutPanel1.Size.Height + menuStrip1.Size.Height + this.cellHeight * 5 / 3);
            this.Load += new System.EventHandler(this.Form1_Load);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();
        }


        private void generateMap()
        {
            // In future maybe set the number of columns and rows like
            // http://stackoverflow.com/questions/15623461/adding-pictureboxes-to-tablelayoutpanel-is-very-slow

            // Starting
            //this.pictureBox1.Image = GeneticStartupsWindows.Properties.Resources.entrepreneur_starting;
            //pictureBox1.SizeMode = PictureBoxSizeMode.Zoom;

            Cursor.Current = Cursors.WaitCursor;

            for (int i = 0; i < this.tableLayoutPanel1.ColumnCount; i++)
            {
                for (int j = 0; j < this.tableLayoutPanel1.RowCount; j++)
                {
                    this.tableCells[i, j].Image = GeneticStartupsWindows.Properties.Resources.entrepreneur_starting;
                    this.tableCells[i, j].SizeMode = PictureBoxSizeMode.Zoom;
                }
            }

            Cursor.Current = Cursors.Default;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.generateMap();
        }

        private void button2_Click(object sender, EventArgs e)
        {

        }

        private void mapSizeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form2 form2 = new Form2();
            var result = form2.ShowDialog();
            if (result == DialogResult.OK)
            {
                Cursor.Current = Cursors.WaitCursor;
                this.numCols = form2.numCols;
                this.numRows = form2.numRows;
                this.createDynamicTable();
                this.setLayout();
                Cursor.Current = Cursors.Default;
            }

        }

        private int numCols = 20;
        private int numRows = 15;
        private int cellWidth = 30;
        private int cellHeight = 30;
    }
}
