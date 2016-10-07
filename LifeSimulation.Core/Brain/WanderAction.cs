using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LifeSimulation.Core
{
    public class WanderAction : IBrainAction
    {
        public void Execute(IGameObject obj, params IGameObject[] otherObjects)
        {
            var movingObject = (IMovingGameObject) obj;

            movingObject.Mover.Move(obj, otherObjects);
        }
    }
}
