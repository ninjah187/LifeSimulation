using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DotNetNinja.TypeFiltering;
using DotNetNinja.NotifyPropertyChanged;

namespace LifeSimulation.Core
{
    public class Mover : PropertyChangedNotifier, IMover
    {
        protected static Random Random { get; } = new Random();

        public Vector Direction
        {
            get { return _direction; }
            set { SetProperty(ref _direction, value); }
        }
        Vector _direction;

        public int CurrentStep { get; set; }
        public int DirectionChangeStepsLimit { get; set; }

        Point _oldPosition;

        public void Move(IGameObject gameObject, params IGameObject[] objects)
        {
            if (CurrentStep >= DirectionChangeStepsLimit)
            {
                ChangeDirection(gameObject, objects);
            }

            CurrentStep++;

            var newPosition = gameObject.Position + Direction;

            PlaceIn(gameObject, newPosition);
        }

        public void Move(IGameObject gameObject, Vector direction)
        {
            Direction = direction;

            var newPosition = gameObject.Position + Direction;

            PlaceIn(gameObject, newPosition);
        }

        public void PlaceIn(IGameObject gameObject, Point position)
        {
            _oldPosition = gameObject.Position;
            gameObject.Position = position;
            gameObject.When<ICollidableGameObject>(o => o.HitBox.Update(gameObject));
        }

        public void RollbackMove(IGameObject gameObject)
        {
            PlaceIn(gameObject, _oldPosition);
        }

        public virtual void ChangeDirection(IGameObject gameObject, params IGameObject[] objects)
        {
            CurrentStep = 0;
            DirectionChangeStepsLimit = Random.Next(5, 21);

            Direction = new Vector
            {
                X = Random.Next(-1, 2),
                Y = Random.Next(-1, 2)
            };
        }
    }
}
