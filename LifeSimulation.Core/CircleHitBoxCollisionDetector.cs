using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LifeSimulation.Core
{
    public class CircleHitBoxCollisionDetector : ICircleHitBoxCollisionDetector
    {
        IEnumerable<ICircleHitBox> _targetHitBoxes;

        public CircleHitBoxCollisionDetector(IEnumerable<ICircleHitBox> targetHitBoxes)
        {
            _targetHitBoxes = targetHitBoxes;
        }

        public bool Collides(ICircleHitBox hitBox)
        {
            foreach (var targetHitBox in _targetHitBoxes)
            {
                if (targetHitBox.Collides(hitBox))
                {
                    return true;
                }
            }

            return false;
        }
    }
}
