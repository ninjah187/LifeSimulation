﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LifeSimulation.Core
{
    public class CircleHitBox : ICircleHitBox
    {
        public Point Center { get; set; }
        public double Radius { get; set; }

        public void Update(Point topLeft, double size)
        {
            Radius = size / 2;

            Center = new Point
            {
                X = topLeft.X + Radius,
                Y = topLeft.Y + Radius
            };
        }
    }
}