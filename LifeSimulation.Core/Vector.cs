using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LifeSimulation.Core
{
    public struct Vector
    {
        public Vector(double x, double y)
        {
            X = x;
            Y = y;
        }

        public double X { get; set; }
        public double Y { get; set; }

        //public static bool operator ==(Vector v, Vector v2)
        //    => v.X == v2.X && v.Y == v2.Y;

        //public static bool operator !=(Vector v, Vector v2)
        //    => v.X != v2.X && v.Y != v2.Y;
    }
}
