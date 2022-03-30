using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace Elasticity
{
    class Ball
    {
        public float X { get; private set; }
        public float Y { get; private set; }
        public float Vx { get; private set; }
        public float Vy { get; private set; }
        public float Ax { get; private set; }
        public float Ay { get; private set; }
        public float Mass { get; }
        public float Radius { get; }
        public Color Color { get; }

        public Ball( float x, float y, float mass, float radius, Color color)
        {
            X = x;
            Y = y;
            Mass = mass;
            Radius = radius;
            Color = color;
        }

        public Ball( float x, float y, float mass, float radius)
            : this(x, y, mass, radius, Color.Red)
        {

        }

        public void Move()
        {
            ApplyAcceleration();
            ApplyVelocity();
            ResetAcceleration();
        }

        public void ExertForce(float fx, float fy)
        {
            Ax += fx / Mass;
            Ay += fy / Mass;
        }

        private void ApplyAcceleration()
        {
            Vx += Ax;
            Vy += Ay;
        }

        private void ApplyVelocity()
        {
            float dt = Physics.Time;

            X += Vx * dt;
            Y += Vy * dt;
        }

        private void ResetAcceleration()
        {
            Ax = 0;
            Ay = 0;
        }
    }
}
