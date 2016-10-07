using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LifeSimulation.Core
{
    public class BrainOrganism : Organism
    {
        public BrainOrganism(Point position, ICircleHitBox hitBox, IMover mover) 
            : base(position, hitBox, mover)
        {
        }
    }
}
