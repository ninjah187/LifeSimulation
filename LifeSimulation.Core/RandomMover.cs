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

        public Point Position
        {
            get { return _position; }
            set { SetProperty(ref _position, value); }
        }
        Point _position;

        public Vector Direction
        {
            get { return _direction; }
            set { SetProperty(ref _direction, value); }
        }
        Vector _direction;

        public double Size
        {
            get { return _size; }
            set { SetProperty(ref _size, value); }
        }
        double _size;

        public ICircleHitBox HitBox { get; }

        IMapCollisionDetector _mapCollisionDetector;

        int _currentStep;
        int _directionChangeStepsLimit;

        public RandomMover(Point position, ICircleHitBox hitBox, IMapCollisionDetector mapCollisionDetector)
        {
            Position = position;

            HitBox = hitBox;
            _mapCollisionDetector = mapCollisionDetector;
        }

        public void ApplyForce(Vector force)
        {
        }

        public void Move()
        {
            if (_currentStep >= _directionChangeStepsLimit)
            {
                ChangeDirection();
            }

            _currentStep++;

            var newPosition = Position + Direction;

            HitBox.Update(newPosition, Size);

            while (_mapCollisionDetector.Collides(HitBox))
            {
                ChangeDirection();
                newPosition = Position + Direction;
                HitBox.Update(newPosition, Size);
            }

            Position = newPosition;
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
