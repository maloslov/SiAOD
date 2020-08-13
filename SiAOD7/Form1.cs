using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace SiAOD7
{
    public partial class Form1 : Form
    {
        private int[] size = { 0, 0 };
        private int[] startEnd = { 0, 0 };
        private List<MyCell> allCells = new List<MyCell>();
        private MyCell activeCell; //= new MyCell(0,25,25,75,125);
        private Pen pen = new Pen(Brushes.LightGray, 7);
        private bool closing = false;
        private bool opening = false; 

        public Form1()
        {
            InitializeComponent();
        }

        #region GUI
        public class MyCell
        {
            public Point point { get; set; }
            public Point Size { get; set; }
            public int Index { get; set; }
            public Brush brush { get; set; }

            public MyCell(int ind, int x1, int y1, int x2, int y2)
            {
                Index = ind;
                point = new Point(x1, y1);
                Size = new Point(x2, y2);
                brush = Brushes.Green;
            }
            public void setBrush(Brush b)
            {
                brush = b;
            }
            public void changeSize(int x1, int y1, int x2, int y2)
            {
                point = new Point(x1, y1);
                Size = new Point(x2, y2);
            }
        }
        
        private MyCell WhichCellClicked(Point point)
        {
            return allCells.FirstOrDefault(cell =>  
                    (point.X > cell.point.X && point.X < cell.Size.X &&
                    point.Y > cell.point.Y && point.Y < cell.Size.Y));
        }
        
        //draw new map or redraw size
        private void RedrawMap(bool flag, int w, int h)
        {
            int k = 1;
            int[] xy = { 1, 1 };

            int dx = pictureBox1.Bounds.Width / w;
            int dy = pictureBox1.Bounds.Height / h;

            if (flag)
            {
                allCells.Clear();
                startEnd[0] = 0;
                startEnd[1] = 0;


                for (int y = 0; y < size[1]; y++)
                {
                    for (int x = 0; x < size[0]; x++)
                    {
                        activeCell = new MyCell(k, xy[0], xy[1], xy[0] + dx, xy[1] + dy);
                        allCells.Add(activeCell);
                        k++;
                        xy[0] += dx;
                    }
                    xy[0] = 1;
                    xy[1] += dy;
                }
            }
            else
            {
                for (int y = 0; y < size[1]; y++)
                {
                    for (int x = 0; x < size[0]; x++)
                    {
                        activeCell = allCells.First(cell => cell.Index == k);
                        activeCell.changeSize(xy[0], xy[1], xy[0] + dx, xy[1] + dy);
                        k++;
                        xy[0] += dx;
                    }
                    xy[0] = 1;
                    xy[1] += dy;
                }

            }
            pictureBox1.Refresh();
        }

        private void pictureBox1_Resize(object sender, EventArgs e)
        {
            if(allCells.Count > 0 && size[0] != 0 && size[1] != 0)
                RedrawMap(false, size[0], size[1]);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                size[0] = Convert.ToInt32(textBox1.Text);
                size[1] = Convert.ToInt32(textBox2.Text);
            }
            catch (FormatException)
            {
                MessageBox.Show("Wrong map size input!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            if(size[0] > 0 && size[1] > 0)
                RedrawMap(true, size[0], size[1]);
            else MessageBox.Show("Wrong map size input!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private void pictureBox1_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            g.Clear(Color.White);

            foreach(var cell in allCells)
            {
                g.DrawRectangle(pen, cell.point.X, cell.point.Y, 
                    cell.Size.X - cell.point.X, cell.Size.Y - cell.point.Y);
                g.FillRectangle(cell.brush, cell.point.X, cell.point.Y, 
                    cell.Size.X - cell.point.X, cell.Size.Y - cell.point.Y);
            }

        }

        private void pictureBox1_MouseDown(object sender, MouseEventArgs e)
        {
            activeCell = WhichCellClicked(e.Location);
            if (activeCell != null &&
                    (activeCell.Index != startEnd[0] &&
                    activeCell.Index != startEnd[1]))
            {
                if (e.Button == MouseButtons.Left)
                {
                    closing = true;
                    opening = false;
                    activeCell.setBrush(Brushes.Blue);
                }
                else if (e.Button == MouseButtons.Right)
                {
                    closing = false;
                    opening = true;
                    activeCell.setBrush(Brushes.Green);
                }
                pictureBox1.Refresh();
            }
        }

        private void pictureBox1_MouseMove(object sender, MouseEventArgs e)
        {
            activeCell = WhichCellClicked(e.Location);
            if (activeCell != null &&
                    (activeCell.Index != startEnd[0] &&
                    activeCell.Index != startEnd[1]))
            {
                if (closing)
                {
                    activeCell.setBrush(Brushes.Blue);
                }
                else if (opening)
                {
                    activeCell.setBrush(Brushes.Green);
                }
                pictureBox1.Refresh();
            }
        }

        private void pictureBox1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            activeCell = WhichCellClicked(e.Location);

            if (activeCell != null)
            {

                if (startEnd[0] == 0 && startEnd[1] != activeCell.Index)
                {
                    activeCell.setBrush(Brushes.Orange);
                    startEnd[0] = activeCell.Index;
                }
                else if (startEnd[0] == activeCell.Index)
                {
                    activeCell.setBrush(Brushes.Green);
                    startEnd[0] = 0;

                }
                if (startEnd[1] == 0 && startEnd[0] != activeCell.Index)
                {
                    activeCell.setBrush(Brushes.OrangeRed);
                    startEnd[1] = activeCell.Index;
                }
                else if (startEnd[1] == activeCell.Index)
                {
                    activeCell.setBrush(Brushes.Green);
                    startEnd[1] = 0;
                }
            }
            pictureBox1.Refresh();
        }

        private void pictureBox1_MouseUp(object sender, MouseEventArgs e)
        {
            closing = false;
            opening = false;
        }


        #endregion

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (Char.IsDigit(e.KeyChar) || e.KeyChar == 8)
                e.Handled = false;
            else e.Handled = true;
        }
    }
}
