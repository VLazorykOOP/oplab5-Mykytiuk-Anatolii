using System;
using System.Drawing;
using System.Windows.Forms;

namespace WindowsFormsApp3
{
    public partial class Form1 : Form
    {
        private const int MaxIterations = 12;
        private const int LineLength = 70;
        private Random random = new Random();

        public Form1()
        {
            InitializeComponent();
            this.Text = "Dragon Curve";
            this.WindowState = FormWindowState.Maximized; // Встановлюємо вікно на весь екран

            pictureBox1.Size = this.ClientSize;
            this.Controls.Add(pictureBox1);

            pictureBox1.Paint += new PaintEventHandler(this.pictureBox1_Paint);

            button1.Click += new EventHandler(this.button1_Click);

            this.Load += new EventHandler(this.Form1_Load);
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void pictureBox1_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;

            float centerX = pictureBox1.ClientSize.Width / 2;
            float centerY = pictureBox1.ClientSize.Height / 2;

            for (int i = 0; i < 60; i++)
            {
                float offsetX = (float)(random.NextDouble() - 0.5) * 400;
                float offsetY = (float)(random.NextDouble() - 0.5) * 400;

                PointF startPoint = new PointF(centerX + offsetX, centerY + offsetY);

                double angle = random.NextDouble() * 2 * Math.PI;
                PointF endPoint = new PointF(
                    startPoint.X + (float)(LineLength * Math.Cos(angle)),
                    startPoint.Y + (float)(LineLength * Math.Sin(angle))
                );


                int redValue = random.Next(0, 50);
                int greenValue = random.Next(60, 100);
                int blueValue = random.Next(200, 256);
                Pen pen = new Pen(Color.FromArgb(255, redValue, greenValue, blueValue));

                DrawDragonCurve(g, pen, startPoint, endPoint, MaxIterations, true);
            }
        }

        private void DrawDragonCurve(Graphics g, Pen pen, PointF p1, PointF p2, int iterations, bool isRightTurn)
        {
            if (iterations == 0)
            {
                g.DrawLine(pen, p1, p2);
            }
            else
            {
                float midX = (p1.X + p2.X) / 2;
                float midY = (p1.Y + p2.Y) / 2;

                float deltaX = (p2.X - p1.X) / 2;
                float deltaY = (p2.Y - p1.Y) / 2;

                PointF midPoint;
                if (isRightTurn)
                {
                    midPoint = new PointF(midX - deltaY, midY + deltaX);
                }
                else
                {
                    midPoint = new PointF(midX + deltaY, midY - deltaX);
                }

                DrawDragonCurve(g, pen, p1, midPoint, iterations - 1, true);
                DrawDragonCurve(g, pen, midPoint, p2, iterations - 1, false);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            pictureBox1.Invalidate();
        }
    }
}
