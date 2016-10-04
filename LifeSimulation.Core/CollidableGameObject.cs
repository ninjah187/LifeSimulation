using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LifeSimulation.Core
{
    public class CollidableGameObject : GameObject, ICollidableGameObject
    {
        public ICircleHitBox HitBox { get; set; }
        public bool IsObstacle { get; set; }

        public CollidableGameObject(Point position, double size, ICircleHitBox hitBox)
            : base(position, size)
        {
            HitBox = hitBox;

            HitBox.Update(this);
        }
    }
}
