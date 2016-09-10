using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LifeSimulation.Core
{
    public interface ICircleHitBox
    {
        Point Center { get; set; }
        double Radius { get; set; }

        void Update(Point topLeft, double size);
    }
}
