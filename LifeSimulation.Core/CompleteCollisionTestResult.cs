using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LifeSimulation.Core
{
    public class CompleteCollisionTestResult : CollisionTestResult, ICompleteCollisionTestResult
    {
        public List<ICollidableGameObject> CollidesWith { get; }

        public CompleteCollisionTestResult(ICollidableGameObject testTarget, List<ICollidableGameObject> collidesWith)
            : base(testTarget)
        {
            CollidesWith = collidesWith;
        }
    }
}
