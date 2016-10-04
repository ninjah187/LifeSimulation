using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LifeSimulation.Core
{
    public class OrganismFoodCollisionResponse : CollisionResponse<IOrganism, IFood>
    {
        public OrganismFoodCollisionResponse()
            : base((organism, food) => organism.Eat(food))
        {
        }
    }
}
