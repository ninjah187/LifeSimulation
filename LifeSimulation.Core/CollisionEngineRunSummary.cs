using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LifeSimulation.Core
{
    public class CollisionEngineRunSummary : ICollisionEngineRunSummary
    {
        public Dictionary<ICollidableGameObject, List<ICollidableGameObject>> Collisions { get; }

        public CollisionEngineRunSummary()
        {
            Collisions = new Dictionary<ICollidableGameObject, List<ICollidableGameObject>>();
        }

        public void AddCollision(ICollidableGameObject obj1, ICollidableGameObject obj2)
        {
            Add(obj1, obj2);
            Add(obj2, obj1);
        }

        void Add(ICollidableGameObject obj1, ICollidableGameObject obj2)
        {
            if (!Collisions.ContainsKey(obj1))
            {
                Collisions.Add(obj1, new List<ICollidableGameObject>());
            }

            if (Collisions[obj1].Contains(obj2))
            {
                return;
            }

            Collisions[obj1].Add(obj2);
        }
    }
}
