using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LifeSimulation.Core
{
    public class Environment : IEnvironment
    {
        public Point Center => new Point(Width / 2, Height / 2);

        public double Width { get; set; }
        public double Height { get; set; }
    }
}
