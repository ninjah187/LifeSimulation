using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LifeSimulation.Core
{
    public interface ICompleteCollisionTestResult : ICollisionTestResult
    {
        List<ICollidableGameObject> CollidesWith { get; }
    }
}
