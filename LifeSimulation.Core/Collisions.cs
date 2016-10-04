using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LifeSimulation.Core
{
    public class Collisions : ICollisions
    {
        public ICollidableGameObject Object { get; }
        public List<ICollidableGameObject> CollidesWith { get; }

        public Collisions(ICollidableGameObject @object)
        {
            Object = @object;
            CollidesWith = new List<ICollidableGameObject>();
        }
    }
}
