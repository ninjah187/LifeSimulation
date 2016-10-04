using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LifeSimulation.Core
{
    public interface ICollisionEngineRunSummary
    {
        Dictionary<ICollidableGameObject, List<ICollidableGameObject>> Collisions { get; }
        void AddCollision(ICollidableGameObject obj1, ICollidableGameObject obj2);
    }
}
