using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LifeSimulation.Core
{
    public interface IMover
    {
        Vector Direction { get; set; }

        int CurrentStep { get; set; }
        int DirectionChangeStepsLimit { get; set; }
        
        void Move(IGameObject gameObject, params IGameObject[] objects);
        void ChangeDirection(IGameObject gameObject, params IGameObject[] objects);
        void RollbackMove(IGameObject gameObject);
        void MoveTo(IGameObject gameObject, Point position);
    }
}
