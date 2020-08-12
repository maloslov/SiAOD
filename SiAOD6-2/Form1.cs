// Thanks to "irreal" for their help with circles 
// from https://stackoverflow.com/questions/29631529/

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SiAOD6_2
{
    public partial class Form1 : Form
    {
        private List<MyCircle> myCircles = new List<MyCircle>();
        private MyCircle activeCircle;
        private const int RADIUS = 15;
        private bool drawing = false;
        private bool dragging = false;
        private bool choosingStartEnd = false;
        private int[] startEnd = { 0, 0 };
        private readonly SolidBrush textColor = new SolidBrush(Color.White);
        private Pen pen = new Pen(new SolidBrush(Color.Black), 4);
        private Point dragOffset;
        
        public Form1()
        {
            InitializeComponent();
        }

        #region Node Visualisation & Controls
        //Node class
        public class MyCircle
        {
            public Point Point { get; set; }
            public int Index { get; }
            public SolidBrush myBrush { get; }
            

            public MyCircle(Point point, int index, Color color)
            {
                Point = point;
                Index = index;
                myBrush = new SolidBrush(color);
            }

            public void setColor(Color color)
            {
                myBrush.Color = color;
            }
        }
        private MyCircle CheckIfCircleClicked(Point point)
        {
            //brush.Color = (Color.Red);
            return myCircles.FirstOrDefault(
                    circle =>
                        Math.Abs(circle.Point.X - point.X) < RADIUS &&
                        Math.Abs(circle.Point.Y - point.Y) < RADIUS);
        }

        private void pictureBox1_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                return;
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
                    pictureBox1.Invalidate();

                }
                else
                {
                    dragging = true;
                    dragOffset = new Point(activeCircle.Point.X - e.Location.X, activeCircle.Point.Y - e.Location.Y);
                }
            }

        }

        private void pictureBox1_MouseMove(object sender, MouseEventArgs e)
        {
            if (dragging)
            {
                activeCircle.Point = new Point(e.Location.X + dragOffset.X, e.Location.Y + dragOffset.Y);
                pictureBox1.Invalidate();
            }
        }

        private void pictureBox1_MouseUp(object sender, MouseEventArgs e)
        {

            if (e.Button != MouseButtons.Left)
                return;

            if (activeCircle == null)
                return;

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
            pictureBox1.Invalidate();
        }

        private void pictureBox1_Paint(object sender, PaintEventArgs e)
        {

            Graphics g = e.Graphics;
            g.Clear(Color.White);

            foreach (var circle in myCircles.Where(c => c != activeCircle))
            {
                g.DrawEllipse(pen, (circle.Point.X - RADIUS),
                    (circle.Point.Y - RADIUS), (RADIUS * 2), (RADIUS * 2));
                g.FillEllipse(circle.myBrush, (circle.Point.X - RADIUS), 
                    (circle.Point.Y - RADIUS), (RADIUS * 2), (RADIUS * 2));
                g.DrawString(circle.Index.ToString(), Font, textColor, 
                    circle.Point.X - RADIUS/2, circle.Point.Y - RADIUS/2);

            }

            if (activeCircle != null)
            {
                g.DrawEllipse(pen, (activeCircle.Point.X - RADIUS),
                    (activeCircle.Point.Y - RADIUS), (RADIUS * 2), (RADIUS * 2));
                g.FillEllipse(activeCircle.myBrush, (activeCircle.Point.X - RADIUS),
                    (activeCircle.Point.Y - RADIUS), (RADIUS * 2), (RADIUS * 2));
                g.DrawString(activeCircle.Index.ToString(), Font, textColor, 
                    activeCircle.Point.X - RADIUS/2, activeCircle.Point.Y - RADIUS/2);
            }
        }

        #endregion

        private void txtBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (Char.IsDigit(e.KeyChar) || e.KeyChar == 8 || e.KeyChar == 32)
                e.Handled = false;
            else e.Handled = true;
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
                pictureBox1.Invalidate();
            }
        }
    }
}
