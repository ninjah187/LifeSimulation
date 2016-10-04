using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LifeSimulation.Core
{
    public class CollisionDeadlockResolver : ICollisionDeadlockResolver
    {
        static readonly Random _random = new Random();

        Vector[] _directions = new Vector[]
        {
            new Vector(0, 1),
            new Vector(1, 1),
            new Vector(1, 0),
            new Vector(1, -1),
            new Vector(0, -1),
            new Vector(-1, -1),
            new Vector(-1, 0),
            new Vector(-1, 1),
            new Vector(0, 0)
        };

        IMapCollisionDetector _mapCollisionDetector;

        public CollisionDeadlockResolver(IMapCollisionDetector mapCollisionDetector)
        {
            _mapCollisionDetector = mapCollisionDetector;
        }

        public void Resolve(ICollidableGameObject gameObject, IEnumerable<ICollidableGameObject> otherObjects)
        {
            var startIndex = _random.Next(0, _directions.Length);

            for (int multiplier = 1; multiplier <= 100; multiplier++)
            {
                int i = startIndex;
                do
                {
                    if (i == _directions.Length)
                    {
                        i = 0;
                    }
                    
                    if (TryDirection(gameObject, otherObjects, _directions[i], multiplier))
                    {
                        return;
                    }

                    i++;
                } while (i != startIndex);
            }

            throw new CollisionEngineDeadlockException();
        }

        bool TryDirection(ICollidableGameObject gameObject, IEnumerable<ICollidableGameObject> otherObjects,
                          Vector direction, int multiplier)
        {
            var movingObject = (IMovingGameObject) gameObject;

            movingObject.Mover.MoveTo(gameObject, gameObject.Position + direction * multiplier);

            if (_mapCollisionDetector.Collides(gameObject.HitBox))
            {
                return false;
            }

            foreach (var otherObject in otherObjects)
            {
                if (gameObject.IsObstacle && otherObject.IsObstacle &&
                    gameObject.HitBox.Collides(otherObject.HitBox))
                {
                    return false;
                }
            }

            return true;
        }
    }
}
