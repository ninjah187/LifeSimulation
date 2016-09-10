using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DotNetNinja.NotifyPropertyChanged;

namespace LifeSimulation.Core
{
    public class RandomMover : PropertyChangedNotifier, IMover
    {
        protected static Random Random { get; } = new Random();

        public Vector Direction
        {
            get { return _direction; }
            set { SetProperty(ref _direction, value); }
        }
        Vector _direction;

        public ICircleHitBox HitBox { get; }

        IMapCollisionDetector _mapCollisionDetector;

        int _currentStep;
        int _directionChangeStepsLimit;

        public RandomMover(ICircleHitBox hitBox, IMapCollisionDetector mapCollisionDetector)
        {
            HitBox = hitBox;
            _mapCollisionDetector = mapCollisionDetector;
        }

        public void ApplyForce(Vector force)
        {
        }

        public void Move(IGameObject gameObject)
        {
            if (_currentStep >= _directionChangeStepsLimit)
            {
                ChangeDirection();
            }

            _currentStep++;

            var newPosition = gameObject.Position + Direction;

            HitBox.Update(newPosition, gameObject.Size);

            while (_mapCollisionDetector.Collides(HitBox))
            {
                ChangeDirection();
                newPosition = gameObject.Position + Direction;
                HitBox.Update(newPosition, gameObject.Size);
            }

            gameObject.Position = newPosition;
        }

        void ChangeDirection()
        {
            _currentStep = 0;
            _directionChangeStepsLimit = Random.Next(5, 21);

            //var oldMin = 0;
            //var oldMax = 1;
            //var newMin = -1;
            //var newMax = 1;

            Direction = new Vector
            {
                X = Random.Next(-1, 2),
                Y = Random.Next(-1, 2)
                //X = (((Random.NextDouble() - oldMin) * (newMax - newMin)) / (oldMax - oldMin)) + newMin,
                //Y = (((Random.NextDouble() - oldMin) * (newMax - newMin)) / (oldMax - oldMin)) + newMin
            };
        }
    }
}
