using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LifeSimulation.Core
{
    public interface ICollider
    {
        void Hit<T1, T2>(T1 obj1, T2 obj2, Action<T1, T2> what)
            where T1 : ICollidableGameObject
            where T2 : ICollidableGameObject;
    }
}
