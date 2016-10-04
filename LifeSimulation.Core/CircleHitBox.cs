using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LifeSimulation.Core
{
    public class CircleHitBox : ICircleHitBox
    {
        public Point Center { get; set; }
        public double Radius { get; set; }

        public void Update(IGameObject gameObject)
        {
            var topLeft = gameObject.Position;
            var size = gameObject.Size;

            Radius = size / 2;

            Center = new Point
            {
                X = topLeft.X + Radius,
                Y = topLeft.Y + Radius
            };
        }

        public bool Collides(ICircleHitBox hitBox)
            => (hitBox.Center - Center).Length < hitBox.Radius + Radius;
    }
}
