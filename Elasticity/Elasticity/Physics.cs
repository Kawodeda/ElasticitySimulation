using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Elasticity
{
    static class Physics
    {
        public static float Time = 0.0f;
        public const float g = 0.2f;
        public static List<Ball> Balls { get; set; }
        public static List<Bond> Bonds { get; set; }

        public static void Run()
        {
            for(int i = 0; i < Balls.Count; i++)
            {
                GravityForce(Balls[i]);
            }

            for (int i = 0; i < Bonds.Count; i++)
            {
                ElasticityForce(Bonds[i]);      
            }

            for (int i = 0; i < Balls.Count; i++)
            {
                Balls[i].Move();
            }
        }

        private static void GravityForce(Ball ball)
        {
            ball.ExertForce(0, g * ball.Mass * Time);
        }

        private static void ElasticityForce(Bond bond)
        {
            bond.UpdateLength();

            float dl = bond.OriginLength - bond.Length;
            //if (dl > 0)
            //    dl = 0;
            float force = bond.K * dl;

            float dx, dy;

            switch(bond.Mode)
            {
                case BondMode.BallToBall:
                    dx = bond.Ball.X - bond.Ball2.X;
                    dy = bond.Ball.Y - bond.Ball2.Y;
                    break;

                default:
                    dx = bond.Ball.X - bond.X;
                    dy = bond.Ball.Y - bond.Y;
                    break;
            }

            float fx = (dx / bond.Length) * force * (Time);
            float fy = (dy / bond.Length) * force * (Time);

            bond.Stress = force / bond.S;
            switch(bond.Mode)
            {
                case BondMode.BallToPoint:
                    bond.Ball.ExertForce(fx, fy);
                    break;

                case BondMode.BallToBall:
                    bond.Ball.ExertForce(fx, fy);
                    bond.Ball2.ExertForce(-fx, -fy);
                    break;
            }
        }
    }
}
