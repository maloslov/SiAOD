using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SiAOD6
{
    public partial class Form1 : Form
    {
        private List<MyCircle> myCircles = new List<MyCircle>();
        private MyCircle activeCircle;
        private Font font = new Font(FontFamily.GenericSansSerif, 14, FontStyle.Bold);
        private const int RADIUS = 16;
        private bool dragging = false;
        private readonly SolidBrush textColor = new SolidBrush(Color.White);
        private Pen pen = new Pen(Brushes.Black, 2);
        private Pen arrow = new Pen(new SolidBrush((Color.Black)), 5);
        private Point dragOffset;

        public Form1()
        {
            InitializeComponent();
        }

        #region Node GUI
        //Node class
        public class MyCircle
        {
            public Point Point { get; set; }
            public int Index { get; }
            public SolidBrush MyBrush { get; }
            public List<Tuple<int, int>> Neighbours { get; }


            public MyCircle(Point point, int index, Color color)
            {
                Point = point;
                Index = index;
                MyBrush = new SolidBrush(color);
                Neighbours = new List<Tuple<int, int>>();
            }

            public void setColor(Color color)
            {
                MyBrush.Color = color;
            }
            public void addNeighbour(int index, int cost)
            {
                Neighbours.Add(Tuple.Create(index, cost));
            }
        }
        private MyCircle CheckIfCircleClicked(Point point)
        {
            return myCircles.FirstOrDefault(
                    circle =>
                        Math.Abs(circle.Point.X - point.X) < RADIUS &&
                        Math.Abs(circle.Point.Y - point.Y) < RADIUS);
        }

        private void pictureBox1_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                dragging = true;

                activeCircle = CheckIfCircleClicked(e.Location);
                if (activeCircle == null)
                {
                    /*
                    activeCircle = new MyCircle(e.Location, myCircles.Count + 1, Color.Green);

                    myCircles.Add(activeCircle);
                    */
                    dragging = false;
                }
                else
                {
                    dragging = true;
                    dragOffset = new Point(activeCircle.Point.X - e.Location.X, activeCircle.Point.Y - e.Location.Y);
                }
            }
            pictureBox1.Refresh();
        }

        private void pictureBox1_MouseMove(object sender, MouseEventArgs e)
        {
            if (dragging)
            {
                activeCircle.Point = new Point(e.Location.X + dragOffset.X, e.Location.Y + dragOffset.Y);
            }
            pictureBox1.Refresh();
        }

        private void pictureBox1_MouseUp(object sender, MouseEventArgs e)
        {
            if (dragging)
            {
                activeCircle.Point = new Point(e.Location.X + dragOffset.X, e.Location.Y + dragOffset.Y);
            }
            dragging = false;
            pictureBox1.Refresh();
        }

        private void pictureBox1_Paint(object sender, PaintEventArgs e)
        {

            Graphics g = e.Graphics;
            g.Clear(Color.White);
            //specifying arrow
            arrow.CustomEndCap = new AdjustableArrowCap(5f, 7f);
            arrow.CustomStartCap = new AdjustableArrowCap(0.1f, 0.1f);



            //drawing lines
            foreach (var circle in myCircles)
            {
                foreach (var ind in circle.Neighbours)
                {
                    MyCircle point2 =
                        myCircles.FirstOrDefault(c => c.Index.Equals(ind.Item1));
                    g.DrawLine(arrow, circle.Point.X, circle.Point.Y,
                        point2.Point.X, point2.Point.Y);
                    g.DrawString(ind.Item2.ToString(), font, Brushes.Magenta,
                        Math.Abs(circle.Point.X - 5 * (circle.Point.X - point2.Point.X) / 7), //debug this
                        Math.Abs(circle.Point.Y - 5 * (circle.Point.Y - point2.Point.Y) / 7)); //debug this
                }
            }


            //drawing circles
            foreach (var circle in myCircles)
            {
                g.DrawEllipse(pen, (circle.Point.X - RADIUS),
                    (circle.Point.Y - RADIUS), (RADIUS * 2), (RADIUS * 2));
                g.FillEllipse(circle.MyBrush, (circle.Point.X - RADIUS),
                    (circle.Point.Y - RADIUS), (RADIUS * 2), (RADIUS * 2));
                g.DrawString(circle.Index.ToString(), font, textColor,
                    (circle.Point.X - 4 * RADIUS / 5), (circle.Point.Y - 4 * RADIUS / 5));
            }
        }

        #endregion

        private void button1_Click(object sender, EventArgs e)
        {
            myCircles.Clear();

            String[] str = textBox1.Text.Split('\r');
            int n = str.Length;
            int[,] matrix = new int[n, n];
            for(int i = 0; i < n; i++)
            {
                String[] buf = str[i].Split(' ');
                for(int j = 0; j < n; j++)
                {
                    if (i == j)
                    {
                        matrix[i, j] = 0;
                        continue;
                    }
                    try
                    {
                        matrix[i, j] = Convert.ToInt32(buf[j]);
                    }
                    catch (FormatException)
                    {
                        MessageBox.Show("Wrong input!","Error",MessageBoxButtons.OK,MessageBoxIcon.Error);
                        return;
                    }
                    if (matrix[i, j] == 0)
                        matrix[i, j] = 999;
                }
            }

            textBox1.Text = "";
            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    textBox1.AppendText(matrix[i, j].ToString() + " ");
                }
                if(i < n-1)
                    textBox1.AppendText("\r\n");
            }

            //graph visualisation
            for (int i = 0; i < n; i++)
            {
                activeCircle = new MyCircle(new Point(100, 100), i, Color.Green);
                for(int j = 0; j < n; j++)
                {
                    if (matrix[i, j] != 0 && matrix[i, j] != 999)
                        activeCircle.addNeighbour(j, matrix[i, j]);
                }
                myCircles.Add(activeCircle);
            }
            pictureBox1.Refresh();

            //search paths and count time
            Stopwatch time = new Stopwatch();
            time.Start();
            int[,] res = Johnson.johnsonStart(matrix);
            time.Stop();

            if (res == null)
                return;

           
            textBox2.AppendText("\r\n");
            for(int i = 0; i < n; i++)
            {
                textBox2.AppendText("\r\n");
                for(int j = 0; j < n; j++)
                {
                    textBox2.AppendText("\t" + res[i, j].ToString());
                }
            }
            textBox2.AppendText("\r\nTime: "+time.Elapsed.ToString()+"\r\n");
        }

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (Char.IsDigit(e.KeyChar) || 
                e.KeyChar == 32 || e.KeyChar == 8 || 
                e.KeyChar == '-' ||
                e.KeyChar == 13)
                e.Handled = false;
            else e.Handled = true;
        }
    }
}
