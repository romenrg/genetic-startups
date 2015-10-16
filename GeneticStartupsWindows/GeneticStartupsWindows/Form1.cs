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
            createDynamicTable(20, 15);
            setFinalLayout();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void createBasicLayout()
        {
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            //this.pictureBox1 = new System.Windows.Forms.PictureBox();
            //this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.tableLayoutPanel1.SuspendLayout();
            //((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            //((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
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
            this.Name = "Form1";
            this.Text = "Form1";
            //this.Load += new System.EventHandler(this.Form1_Load);
            //this.tableLayoutPanel1.ResumeLayout(false);
            //((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            //((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            //this.ResumeLayout(false);
        }

        private void createDynamicTable(int numCols, int numRows)
        {
            this.tableLayoutPanel1.CellBorderStyle = System.Windows.Forms.TableLayoutPanelCellBorderStyle.Inset;
            this.tableLayoutPanel1.ColumnCount = numCols;
            this.tableLayoutPanel1.RowCount = numRows;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(16, 12);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableCells = new System.Windows.Forms.PictureBox[numCols, numRows];
            for (int i=0; i<numCols; i++)
            {
                this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, cellHeight));
                for (int j=0; j<numRows; j++) {
                    this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, cellWidth));
                    this.tableCells[i, j] = new System.Windows.Forms.PictureBox();
                    this.tableLayoutPanel1.Controls.Add(this.tableCells[i, j], i, j);
                }
            }
            int tableWidth = (numCols + 1) * cellWidth;
            int tableHeight = (numRows + 1) * cellHeight;
            this.tableLayoutPanel1.Size = new System.Drawing.Size(tableWidth, tableHeight);
        }

        private void setFinalLayout()
        {
            this.ClientSize = this.tableLayoutPanel1.Size + new System.Drawing.Size(this.cellWidth * 3 / 2, this.cellHeight * 3 / 2);
            this.button1.Location = new System.Drawing.Point(this.tableLayoutPanel1.Size.Width / 5, this.tableLayoutPanel1.Size.Height + this.cellHeight / 2);
            this.button2.Location = new System.Drawing.Point(this.tableLayoutPanel1.Size.Width / 5 * 4 - this.button2.Size.Width / 2, this.tableLayoutPanel1.Size.Height + this.cellHeight / 2);
            this.Load += new System.EventHandler(this.Form1_Load);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.ResumeLayout(false);
        }

        private void button1_Click(object sender, EventArgs e)
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

        private void button2_Click(object sender, EventArgs e)
        {

        }


        private int cellWidth = 40;
        private int cellHeight = 40;

    }
}
