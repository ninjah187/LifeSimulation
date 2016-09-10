using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LifeSimulation.Core
{
    public class Food : GameObject, IFood
    {
        public Food(Point position)
            : base(position, 5)
        {
        }
    }
}
