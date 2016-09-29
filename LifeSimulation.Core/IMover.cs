using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LifeSimulation.Core
{
    public interface IMover
    {
        Vector Direction { get; set; }

        IMapCollisionDetector MapCollisionDetector { get; set; }

        int CurrentStep { get; set; }
        int DirectionChangeStepsLimit { get; set; }

        void ApplyForce(Vector force);
        void Move(ICollidableGameObject gameObject, params ICollidableGameObject[] obstacles);
    }
}
