using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace LifeSimulation.Core
{
    public struct Point
    {
        public double X { get; set; }
        public double Y { get; set; }

        public Point(double x, double y)
        {
            X = x;
            Y = y;
        }

        public static Point operator +(Point point, Vector vector)
            => new Point
            {
                X = point.X + vector.X,
                Y = point.Y + vector.Y
            };

        public static Point operator -(Point point, Vector vector)
            => new Point
            {
                X = point.X - vector.X,
                Y = point.Y - vector.Y
            };
    }
}
