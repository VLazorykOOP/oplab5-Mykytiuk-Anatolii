using System;
using System.Drawing;
using System.Windows.Forms;

namespace WindowsFormsApp3
{
    public partial class Ermit_line : Form
    {
        private PointF P1;
        private PointF P2;
        private PointF V1;
        private PointF V2;

        public Ermit_line()
        {
            InitializeComponent();
            this.Text = "Hermite Curve";
            this.Size = new Size(800, 600);

            button1.Text = "Draw Hermite Curve";
            button1.Click += Button1_Click;
            pictureBox1.Paint += PictureBox1_Paint;

            textBox1.Text = "100";
            textBox2.Text = "200";
            textBox3.Text = "400";
            textBox4.Text = "200";
            textBox5.Text = "100";
            textBox6.Text = "-150";
            textBox7.Text = "100";
            textBox8.Text = "-150";
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            try
            {
                P1 = new PointF(float.Parse(textBox1.Text), float.Parse(textBox2.Text));
                P2 = new PointF(float.Parse(textBox3.Text), float.Parse(textBox4.Text));
                V1 = new PointF(float.Parse(textBox5.Text), float.Parse(textBox6.Text));
                V2 = new PointF(float.Parse(textBox7.Text), float.Parse(textBox8.Text));

                pictureBox1.Invalidate();
            }
            catch (FormatException)
            {
                MessageBox.Show("Please enter valid numbers in the text boxes.", "Invalid Input", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void PictureBox1_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            Pen pen = new Pen(Color.Blue, 2);

            DrawHermiteCurve(g, pen, P1, P2, V1, V2);

            DrawPoint(g, Brushes.Red, P1);
            DrawPoint(g, Brushes.Red, P2);

            DrawVector(g, Pens.Green, P1, V1);
            DrawVector(g, Pens.Green, P2, V2);
        }

        private void DrawHermiteCurve(Graphics g, Pen pen, PointF P1, PointF P2, PointF V1, PointF V2)
        {
            const int steps = 100;
            PointF prevPoint = P1;

            for (int i = 1; i <= steps; i++)
            {
                float t = i / (float)steps;
                float h1 = 2 * t * t * t - 3 * t * t + 1;
                float h2 = -2 * t * t * t + 3 * t * t;
                float h3 = t * t * t - 2 * t * t + t;
                float h4 = t * t * t - t * t;

                float x = h1 * P1.X + h2 * P2.X + h3 * V1.X + h4 * V2.X;
                float y = h1 * P1.Y + h2 * P2.Y + h3 * V1.Y + h4 * V2.Y;

                PointF curPoint = new PointF(x, y);
                g.DrawLine(pen, prevPoint, curPoint);
                prevPoint = curPoint;
            }
        }

        private void DrawPoint(Graphics g, Brush brush, PointF point)
        {
            const int size = 5;
            g.FillEllipse(brush, point.X - size / 2, point.Y - size / 2, size, size);
        }

        private void DrawVector(Graphics g, Pen pen, PointF start, PointF vector)
        {
            PointF end = new PointF(start.X + vector.X, start.Y + vector.Y);
            g.DrawLine(pen, start, end);

            const float arrowSize = 10;
            float angle = (float)Math.Atan2(vector.Y, vector.X);
            PointF arrowPoint1 = new PointF(
                end.X - arrowSize * (float)Math.Cos(angle - Math.PI / 6),
                end.Y - arrowSize * (float)Math.Sin(angle - Math.PI / 6));
            PointF arrowPoint2 = new PointF(
                end.X - arrowSize * (float)Math.Cos(angle + Math.PI / 6),
                end.Y - arrowSize * (float)Math.Sin(angle + Math.PI / 6));
            g.DrawLine(pen, end, arrowPoint1);
            g.DrawLine(pen, end, arrowPoint2);
        }

        private void Ermit_line_Load(object sender, EventArgs e)
        {
        }
    }
}
