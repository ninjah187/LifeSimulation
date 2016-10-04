using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LifeSimulation.Core
{
    public class Collision : ICollision
    {
        public ICollidableGameObject Object1 { get; }
        public ICollidableGameObject Object2 { get; }

        public Collision(ICollidableGameObject obj1, ICollidableGameObject obj2)
        {
            Object1 = obj1;
            Object2 = obj2;
        }
    }
}
