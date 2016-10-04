using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DotNetNinja.TypeFiltering;

namespace LifeSimulation.Core
{
    public class CollisionResponse<TColl1, TColl2> : ICollisionResponse
        where TColl1 : class, ICollidableGameObject
        where TColl2 : class, ICollidableGameObject
    {
        public Action<TColl1, TColl2> Response { get; }

        public CollisionResponse(Action<TColl1, TColl2> response)
        {
            Response = response;
        }

        public virtual void Run(ICompleteCollisionTestResult testResult)
        {
            foreach (var obj2 in testResult.CollidesWith)
            {
                testResult.TestTarget
                    .When<TColl1>(c1 => obj2.When<TColl2>(c2 => Response(c1, c2)))
                    .BreakIfRecognized()
                    .When<TColl2>(c2 => obj2.When<TColl1>(c1 => Response(c1, c2)));
            }
        }
    }
}
