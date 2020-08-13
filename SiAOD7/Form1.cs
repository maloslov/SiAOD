using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace SiAOD7
{
    public partial class Form1 : Form
    {
        private Stopwatch time = new Stopwatch();
        private int[] size = { 0, 0 };
        private int[] startEnd = { 0, 0 };
        private List<MyCell> allCells = new List<MyCell>();
        private MyCell activeCell;
        private Pen pen = new Pen(Brushes.LightGray, 5);
        private bool closing = false;
        private bool opening = false;

        #region astar var
        private Map2D map;
        private Waypoint way = new Waypoint(new Point(-1,-1), null);
        #endregion

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
            public Point Loc { get; set; }

            public MyCell(int ind, 
                int x1, int y1, 
                int x2, int y2,
                int a, int b)
            {
                Index = ind;
                point = new Point(x1, y1);
                Size = new Point(x2, y2);
                brush = Brushes.Green;
                Loc = new Point(a, b);
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

                map = new Map2D(w,h);

                for (int y = 0; y < h; y++)
                {
                    for (int x = 0; x < w; x++)
                    {
                        activeCell = new MyCell(k, 
                            xy[0], xy[1], xy[0] + dx, 
                            xy[1] + dy, x, y);
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
                for (int y = 0; y < h; y++)
                {
                    for (int x = 0; x < w; x++)
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
                    map.changeCost(activeCell, int.MaxValue);
                }
                else if (e.Button == MouseButtons.Right)
                {
                    closing = false;
                    opening = true;
                    activeCell.setBrush(Brushes.Green);
                    map.changeCost(activeCell, 0);
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
                    map.changeCost(activeCell, int.MaxValue);
                }
                else if (opening)
                {
                    activeCell.setBrush(Brushes.Green);
                    map.changeCost(activeCell, 0);
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
                    map.changeStart(activeCell, false);
                }
                else if (startEnd[0] == activeCell.Index)
                {
                    activeCell.setBrush(Brushes.Green);
                    startEnd[0] = 0;
                    map.changeStart(activeCell, true);

                }
                if (startEnd[1] == 0 && startEnd[0] != activeCell.Index)
                {
                    activeCell.setBrush(Brushes.OrangeRed);
                    startEnd[1] = activeCell.Index;
                    map.changeEnd(activeCell, false);
                }
                else if (startEnd[1] == activeCell.Index)
                {
                    activeCell.setBrush(Brushes.Green);
                    startEnd[1] = 0;
                    map.changeEnd(activeCell, true);
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

        private void button2_Click(object sender, EventArgs e)
        {
            String savepic;
            if (saveFileDialog1.ShowDialog() == DialogResult.Cancel)
            {
                return;
            }

            savepic = saveFileDialog1.FileName;
            Bitmap pic = new Bitmap(pictureBox1.Width, pictureBox1.Height);
            pictureBox1.DrawToBitmap(pic, pictureBox1.ClientRectangle);
            pic.Save(savepic, System.Drawing.Imaging.ImageFormat.Png);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if(startEnd[0] == 0 || startEnd[1] == 0)
            {
                return;
            }
            else
            {
                foreach (var c in allCells)
                {
                    if(c.brush == Brushes.Yellow || c.brush == Brushes.Cyan)
                        c.setBrush(Brushes.Green);
                }

                time.Start();
                Dictionary<Point, Waypoint> di = AstarPathfind.computePath(map);
                time.Stop();

                txtLog.AppendText("\r\nTime: " + time.Elapsed.ToString() + "\r\n");

                List<MyCell> others = allCells.FindAll(c => 
                    c.brush != Brushes.Orange &&
                    c.brush != Brushes.OrangeRed && 
                    c.brush != Brushes.Blue);

                int i = 1;
                foreach(var d in di)
                {
                    way = d.Value;

                    if (checkBox1.Checked)
                        way = di.ElementAt(di.Count - 1).Value;

                    while (way != null)
                    {
                        Point loc = way.Loc;
                        if(others != null)
                            activeCell = others.FirstOrDefault(c => (c.Loc == loc));
                        if (activeCell != null)
                            if (i == di.Count)
                                activeCell.setBrush(Brushes.Cyan);
                            else
                                activeCell.setBrush(Brushes.Yellow);
                        

                        way = way.PrevWaypoint;
                        pictureBox1.Refresh();
                    }
                    i++;

                    if (checkBox1.Checked)
                        break;
                }
            }
        }



    }
}
