using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Elasticity
{
    enum BondMode
    {
        BallToPoint,
        BallToBall
    }
    class Bond
    {
        public BondMode Mode { get; }
        public Ball Ball { get;}
        public Ball Ball2 { get; }
        public float X { get; }
        public float Y { get; }
        public float Length { get; private set; }
        public float OriginLength { get; }
        public float S { get; }

        private float _E; // Young's modulus
        public float K { get; private set; }
        public float Stress { get; set; }

        private const float _defaultE = 1.0f;

        private const float _defaultS = 1.0f;

        public Bond(Ball ball, float x, float y, float originLength, float s, float E)
            : this(ball, x, y)
        {
            OriginLength = originLength;
            S = s;
            _E = E;
            CalculateHardness();
        }

        public Bond(Ball ball, float x, float y)
        {
            Ball = ball;
            X = x;
            Y = y;
            Mode = BondMode.BallToPoint;
            UpdateLength();
            OriginLength = Length;
            S = _defaultS;
            _E = _defaultE;
            CalculateHardness();
        }

        public Bond(Ball ball1, Ball ball2, float originLength, float s, float E)
            : this(ball1, ball2)
        {
            OriginLength = originLength;
            S = s;
            _E = E;
            CalculateHardness();
        }

        public Bond(Ball ball1, Ball ball2)
        {
            Ball = ball1;
            Ball2 = ball2;
            Mode = BondMode.BallToBall;
            UpdateLength();
            OriginLength = Length;
            S = _defaultS;
            _E = _defaultE;
            CalculateHardness();
        }

        public void UpdateLength()
        {
            switch(Mode)
            {
                case BondMode.BallToPoint:
                    Length = (float)Math.Sqrt((Ball.X - X) * (Ball.X - X) + (Ball.Y - Y) * (Ball.Y - Y));
                    break;

                case BondMode.BallToBall:
                    Length = (float)Math.Sqrt((Ball.X - Ball2.X) * (Ball.X - Ball2.X) + (Ball.Y - Ball2.Y) * (Ball.Y - Ball2.Y));
                    break;
            }
        }

        private void CalculateHardness()
        {
            K = (_E * S) / OriginLength;
        }
    }
}
