using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LifeSimulation.Core
{
    public interface ICollisionDeadlockResolver
    {
        void Resolve(ICollidableGameObject gameObject, IEnumerable<ICollidableGameObject> otherObjects);
    }
}
