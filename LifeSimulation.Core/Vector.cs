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
            _length = null;
        }

        public double X { get; set; }
        public double Y { get; set; }

        public double Length => _length ?? (double) (_length = Math.Sqrt(X * X + Y * Y));
        double? _length;

        public static Vector operator -(Vector v)
            => new Vector
            {
                X = -v.X,
                Y = -v.Y
            };

        public static Vector operator *(Vector v1, Vector v2)
            => new Vector
            {
                X = v1.X * v2.X,
                Y = v1.Y * v2.Y
            };

        public static Vector operator *(Vector v, int multiplier)
            => new Vector
            {
                X = v.X * multiplier,
                Y = v.Y * multiplier
            };

        //public static bool operator ==(Vector v, Vector v2)
        //    => v.X == v2.X && v.Y == v2.Y;

        //public static bool operator !=(Vector v, Vector v2)
        //    => v.X != v2.X && v.Y != v2.Y;
    }
}
