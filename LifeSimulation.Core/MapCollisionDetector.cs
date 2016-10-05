using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LifeSimulation.Core
{
    public class MapCollisionDetector : IMapCollisionDetector
    {
        protected IEnvironment Environment { get; set; }

        public MapCollisionDetector(IEnvironment environment)
        {
            Environment = environment;
        }

        public bool Collides(ICircleHitBox hitBox)
        {
            return hitBox.Center.X - hitBox.Radius <= 0
                || hitBox.Center.Y - hitBox.Radius <= 0
                || hitBox.Center.X + hitBox.Radius >= Environment.Width
                || hitBox.Center.Y + hitBox.Radius >= Environment.Height;
        }
    }
}
