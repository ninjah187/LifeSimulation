using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LifeSimulation.Core
{
    public class MapExceededCollisionTestResult : CollisionTestResult, IMapExceededCollisionTestResult
    {
        public MapExceededCollisionTestResult(ICollidableGameObject testTarget)
            : base(testTarget)
        {
        }
    }
}
