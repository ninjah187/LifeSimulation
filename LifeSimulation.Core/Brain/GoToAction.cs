using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LifeSimulation.Core
{
    public class GoToAction : IBrainAction
    {
        public Point Destination { get; }

        public GoToAction(Point destination)
        {
            Destination = destination;
        }

        public void Execute(IGameObject obj, params IGameObject[] otherObjects)
        {
            var movingObject = (IMovingGameObject) obj;

            var direction = obj.Position.GetDirectionTo(Destination);

            movingObject.Mover.Move(movingObject, direction);
        }
    }
}
