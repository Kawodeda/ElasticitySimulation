using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Elasticity
{
    public partial class Form1 : Form
    {
        Renderer renderer;
        List<Ball> balls = new List<Ball>();
        List<Bond> bonds = new List<Bond>();
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            renderer = new Renderer(pictureBox1.Width, pictureBox1.Height);
            Physics.Balls = balls;
            Physics.Bonds = bonds;

            balls.Add(new Ball(100, 100, 100, 12, Color.Navy));
            balls.Add(new Ball(400, 50, 100, 12, Color.Green));
            balls.Add(new Ball(350, 50, 50, 9, Color.Green));
            balls.Add(new Ball(400, 400, 300, 14, Color.Purple));

            bonds.Add(new Bond(balls[0], 200, 100, 100, 4, 100.0f));
            bonds.Add(new Bond(balls[1], 500, 50, 100, 4, 100.0f));
            bonds.Add(new Bond(balls[1], balls[2], 50, 1, 90.0f));
            bonds.Add(new Bond(balls[3], 300, 300, 140, 4, 100.0f));
            bonds.Add(new Bond(balls[3], 500, 300, 140, 4, 100.0f));
            bonds.Add(new Bond(balls[3], 300, 500, 140, 4, 100.0f));
            bonds.Add(new Bond(balls[3], 500, 500, 140, 4, 100.0f));

            timer1.Enabled = true;
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            renderer.CanvasWidth = pictureBox1.Width;
            renderer.CanvasHeight = pictureBox1.Height;
            renderer.Reset();
            renderer.Draw(balls, bonds);

            Physics.Time = (trackBar1.Value) / 10f;
            Physics.Run();

            pictureBox1.Image = renderer.Bmp;
        }

        private void trackBar1_Scroll(object sender, EventArgs e)
        {

        }
    }
}
