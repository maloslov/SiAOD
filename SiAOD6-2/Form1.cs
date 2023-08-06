// Thanks to "irreal" for their help with circles 
// from https://stackoverflow.com/questions/29631529/

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

namespace SiAOD6_2
{
    public partial class Form1 : Form
    {
        Form2 form2 = new Form2();
        private List<MyCircle> myCircles = new List<MyCircle>();
        private MyCircle activeCircle;
        private MyCircle bufCircle;
        private Color bufColor;
        private Font font = new Font(FontFamily.GenericSansSerif, 14, FontStyle.Bold);
        private const int RADIUS = 16;
        private bool drawing = false;
        private bool dragging = false;
        private bool connecting = false;
        private int[] startEnd = { 0, 0 };
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
            //List< Tuple<index,cost> >
            public List<Tuple<int,int>> Neighbours { get; }
            

            public MyCircle(Point point, int index, Color color)
            {
                Point = point;
                Index = index;
                MyBrush = new SolidBrush(color);
                Neighbours = new List<Tuple<int,int>>();
            }

            public void setColor(Color color)
            {
                MyBrush.Color = color;
            }
            public void addNeighbour(int index, int cost)
            {
                Neighbours.Add(Tuple.Create(index,cost));
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
            if (e.Button == MouseButtons.Right)
            {
                if (connecting)
                {
                    activeCircle = CheckIfCircleClicked(e.Location);
                    if(activeCircle == bufCircle)
                    {
                        activeCircle.setColor(bufColor);
                    }
                    else if(activeCircle != null)
                    {
                        if (bufCircle.Neighbours.Exists(c => c.Item1.Equals(activeCircle.Index)))
                        {
                            bufCircle.Neighbours.Remove((bufCircle.Neighbours.Find(c => 
                                c.Item1.Equals(activeCircle.Index))));
                        }
                        else
                        {
                            form2.ShowDialog();
                            bufCircle.addNeighbour(activeCircle.Index, Convert.ToInt32(form2.Text));
                        }
                         
                        bufCircle.setColor(bufColor);
                    }
                        
                    connecting = false;
                }
                else
                {
                    activeCircle = CheckIfCircleClicked(e.Location);

                    if (activeCircle != null)
                    {
                        bufColor = activeCircle.MyBrush.Color;
                        activeCircle.setColor(Color.LightGreen);
                        bufCircle = activeCircle;
                        connecting = true;
                    }
                }
            }
            else
            {
                drawing = false;
                dragging = true;

                activeCircle = CheckIfCircleClicked(e.Location);
                if (activeCircle == null)
                {
                    activeCircle = new MyCircle(e.Location, myCircles.Count + 1, Color.Green);

                    myCircles.Add(activeCircle);

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
            else if (drawing)
            {
                myCircles.Add(activeCircle);
            }
            dragging = false;
            drawing = false;
            pictureBox1.Refresh();
        }

        private void pictureBox1_Paint(object sender, PaintEventArgs e)
        {

            Graphics g = e.Graphics;
            g.Clear(Color.White);
            //specifying arrow
            arrow.CustomEndCap = new AdjustableArrowCap(5f,7f);
            arrow.CustomStartCap = new AdjustableArrowCap(0.1f,0.1f);

            

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
                        Math.Abs(circle.Point.X - 5 * (circle.Point.X - point2.Point.X)/7), //debug this
                        Math.Abs(circle.Point.Y - 5 * (circle.Point.Y - point2.Point.Y)/7)); //debug this
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
                    (circle.Point.X - 4*RADIUS/5), (circle.Point.Y - 4*RADIUS/5));
            }
        }

        private void pictureBox1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            activeCircle = CheckIfCircleClicked(e.Location);
            if(activeCircle != null)
            {
                if(startEnd[0] == 0 && startEnd[1] != activeCircle.Index)
                {
                    activeCircle.setColor(Color.Orange);
                    startEnd[0] = activeCircle.Index;
                }
                else if(startEnd[0] == activeCircle.Index)
                {
                    activeCircle.setColor(Color.Green);
                    startEnd[0] = 0;

                }
                if(startEnd[1] == 0 && startEnd[0] != activeCircle.Index)
                {
                    activeCircle.setColor(Color.OrangeRed);
                    startEnd[1] = activeCircle.Index;
                }
                else if(startEnd[1] == activeCircle.Index)
                {
                    activeCircle.setColor(Color.Green);
                    startEnd[1] = 0;
                }
            }
                pictureBox1.Refresh();
        }
        private void btnReset_Click(object sender, EventArgs e)
        {
            myCircles.Clear();
            startEnd[0] = 0;
            startEnd[1] = 0;
            drawing = false;
            dragging = false;
            connecting = false;
            pictureBox1.Refresh();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            String savepic;
            if (saveFileDialog1.ShowDialog() == DialogResult.Cancel)
            {
                return;
            }

            savepic = saveFileDialog1.FileName;
            Bitmap pic = new Bitmap(pictureBox1.Width,pictureBox1.Height);
            pictureBox1.DrawToBitmap(pic, pictureBox1.ClientRectangle);
            pic.Save(savepic,System.Drawing.Imaging.ImageFormat.Png);
        }

        #endregion

        private void btnStart_Click(object sender, EventArgs e)
        {
            if (!textBox2.Text.Equals(""))
            {

                String[] str = textBox2.Text.Split('\r');
                int n = str.Length;
                int[,] matrix = new int[n, n];
                for (int i = 0; i < n; i++)
                {
                    String[] buf = str[i].Split(' ');
                    for (int j = 0; j < n; j++)
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
                            MessageBox.Show("Wrong input!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return;
                        }
                        if (matrix[i, j] == 0)
                            matrix[i, j] = int.MaxValue;
                    }
                }

                textBox2.Text = "";
                for (int i = 0; i < n; i++)
                {
                    for (int j = 0; j < n; j++)
                    {
                        textBox2.AppendText(matrix[i, j].ToString() + " ");
                    }
                    if (i < n - 1)
                        textBox2.AppendText("\r\n");
                }

                myCircles.Clear();

                //graph visualisation
                for (int i = 0; i < n; i++)
                {
                    activeCircle = new MyCircle(new Point(100, 100), i, Color.Green);
                    for (int j = 0; j < n; j++)
                    {
                        if (matrix[i, j] != 0 && matrix[i, j] != int.MaxValue)
                            activeCircle.addNeighbour(j, matrix[i, j]);
                    }
                    myCircles.Add(activeCircle);
                }
                pictureBox1.Refresh();
                compute(matrix);
            }
            else
            {

                if (myCircles.Count > 1)
                {
                    int[,] matrix = new int[myCircles.Count, myCircles.Count];

                    //forming matrix
                    for (int i = 0; i < myCircles.Count; i++)
                    {
                        activeCircle = myCircles.FirstOrDefault(c => c.Index == i+1);
                        //Tuple neibs = activeCircle.Neighbours.
                        for (int j = 0; j < myCircles.Count; j++)
                        {
                            if (i == j /*|| activeCircle == null*/)
                            {
                                matrix[i, j] = 0;
                                continue;
                            }

                            Tuple<int, int> pair = activeCircle.Neighbours.FirstOrDefault(c => c.Item1.Equals(j+1));

                            if (pair == null)
                            {
                                matrix[i, j] = 0;
                            }
                            else
                            {
                                matrix[i, j] = pair.Item2;
                            }
                            //

                        }
                    }

                    for (int i = 0; i < matrix.GetLength(0); i++)
                    {
                        for (int j = 0; j < matrix.GetLength(1); j++)
                        {
                            textBox2.AppendText(matrix[i, j] + "\t");
                        }
                        textBox2.AppendText("\r\n");
                    }
                    compute(matrix);
                }
            }
        }
        private void compute(int[,] matrix) { 
            
            int n = matrix.GetLength(0);
            //search paths and count time
            Stopwatch time = new Stopwatch();
            time.Start();
            int[,] res = Johnson.johnsonStart(matrix);
            time.Stop();

            if (res == null)
                return;


            //textBox1.AppendText("\r\n");
            for (int i = 0; i < n; i++)
            {
                //textBox1.AppendText("\r\n");
                for (int j = 0; j < n; j++)
                {
                    textBox1.AppendText(" " + res[i, j].ToString());
                }
            }
            textBox1.AppendText("\r\nTime: " + time.Elapsed.ToString() + "\r\n");
            
        }
    }
}
