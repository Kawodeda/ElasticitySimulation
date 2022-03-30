using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace Elasticity
{
    class Renderer
    {
        public Bitmap Bmp { get; private set; }
        private Graphics _graphics;
        public int CanvasWidth { get; set; }
        public int CanvasHeight { get; set; }

        public Renderer(int canvasWidth, int canvasHeight)
        {
            CanvasWidth = canvasWidth;
            CanvasHeight = canvasHeight;
            Bmp = new Bitmap(CanvasWidth, CanvasHeight);
            _graphics = Graphics.FromImage(Bmp);
        }

        public void Reset()
        {
            Bmp.Dispose();
            _graphics.Dispose();

            Bmp = new Bitmap(CanvasWidth, CanvasHeight);
            _graphics = Graphics.FromImage(Bmp);
        }

        public void Draw(List<Ball> balls, List<Bond> bonds)
        {
            SolidBrush brush = new SolidBrush(Color.Red);

            for (int i = 0; i < balls.Count; i++)
            {
                brush.Color = balls[i].Color;
                float x = balls[i].X - balls[i].Radius;
                float y = balls[i].Y - balls[i].Radius;

                _graphics.FillEllipse(brush, x, y, balls[i].Radius * 2, balls[i].Radius * 2);
            }

            for(int i = 0; i < bonds.Count; i++)
            {
                Bond bond = bonds[i];
                Ball ball = bond.Ball;
                brush.Color = Color.Black;

                float x1, y1;

                switch(bond.Mode)
                {
                    case BondMode.BallToPoint:
                        x1 = bond.X;
                        y1 = bond.Y;
                        break;

                    case BondMode.BallToBall:
                        x1 = bond.Ball2.X;
                        y1 = bond.Ball2.Y;
                        break;

                    default:
                        x1 = bond.X;
                        y1 = bond.Y;
                        break;
                }

                float x2 = ball.X;
                float y2 = ball.Y;

                if(bond.Mode == BondMode.BallToPoint)
                    _graphics.FillEllipse(brush, x1 - 4, y1 - 4, 8, 8);

                var red = (int)(Math.Abs(bond.Stress) * 5);
                if (red > 255)
                    red = 255;
                if (red < 0)
                    red = 0;
                brush.Color = Color.FromArgb(255, red, 0, 255 - red);
                var width = (float)Math.Sqrt(bond.S);

                _graphics.DrawLine(new Pen(brush, width), x1, y1, x2, y2);
            }
        }
    }
}
