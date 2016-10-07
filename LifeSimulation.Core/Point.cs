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

        /// <summary>
        /// Get normalized vector of direction from current point to destination.
        /// </summary>
        /// <param name="destination"></param>
        /// <returns></returns>
        public Vector GetDirectionTo(Point destination)
        {
            var direction = new Vector
            {
                X = this.X < destination.X ? 1 : this.X > destination.X ? -1 : 0,
                Y = this.Y < destination.Y ? 1 : this.Y > destination.Y ? -1 : 0
            };

            return direction;
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

        public static Vector operator +(Point point, Point point2)
            => new Vector
            {
                X = point.X + point2.X,
                Y = point.Y + point2.Y
            };

        public static Vector operator -(Point point, Point point2)
            => new Vector
            {
                X = point.X - point2.X,
                Y = point.Y - point2.Y
            };
    }
}
