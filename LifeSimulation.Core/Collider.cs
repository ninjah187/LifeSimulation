using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LifeSimulation.Core
{
    public class Collider : ICollider
    {
        public Collider()
        {
        }

        public void Hit<T1, T2>(T1 obj1, T2 obj2, Action<T1, T2> what)
            where T1 : ICollidableGameObject
            where T2 : ICollidableGameObject
        {
            var hits = (obj1.HitBox.Center - obj2.HitBox.Center).Length < obj1.HitBox.Radius + obj2.HitBox.Radius;

            if (hits)
            {
                what(obj1, obj2);
            }
        }
    }
}
