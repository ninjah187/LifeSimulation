using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LifeSimulation.Core
{
    public interface IMover
    {
        Point Position { get; }

        void ApplyForce(Vector force);
        void Move();
    }
}
