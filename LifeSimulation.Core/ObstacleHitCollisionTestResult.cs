using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LifeSimulation.Core
{
    public class ObstacleHitCollisionTestResult : CollisionTestResult, IObstacleHitCollisionTestResult
    {
        public ICollidableGameObject Obstacle { get; }

        public ObstacleHitCollisionTestResult(ICollidableGameObject testTarget, ICollidableGameObject obstacle)
            : base(testTarget)
        {
            Obstacle = obstacle;
        }
    }
}
