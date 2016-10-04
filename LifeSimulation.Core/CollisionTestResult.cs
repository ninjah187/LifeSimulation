using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LifeSimulation.Core
{
    public abstract class CollisionTestResult : ICollisionTestResult
    {
        public ICollidableGameObject TestTarget { get; }

        public CollisionTestResult(ICollidableGameObject testTarget)
        {
            TestTarget = testTarget;
        }
    }
}
