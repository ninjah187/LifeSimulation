using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LifeSimulation.Core
{
    public interface IEnvironment
    {
        Point Center { get; }

        double Width { get; }
        double Height { get; }
    }
}
