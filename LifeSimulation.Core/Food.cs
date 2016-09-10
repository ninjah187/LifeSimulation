using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LifeSimulation.Core
{
    public class Food : GameObject, IFood
    {
        ICircleHitBox _hitBox;
        ICircleHitBoxCollisionDetector _organismCollisions;

        public Food(Point position, ICircleHitBox hitBox, ICircleHitBoxCollisionDetector organismCollisions)
            : base(position, 5)
        {
            _hitBox = hitBox;
            _organismCollisions = organismCollisions;

            _hitBox.Update(Position, Size);
        }

        public override void Update()
        {
            if (_organismCollisions.Collides(_hitBox))
            {
                Console.WriteLine("> food collision");
            }
        }
    }
}
