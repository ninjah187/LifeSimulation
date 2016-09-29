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

        public IMapCollisionDetector MapCollisionDetector { get; set; }

        public int CurrentStep { get; set; }
        public int DirectionChangeStepsLimit { get; set; }

        public Mover(IMapCollisionDetector mapCollisionDetector)
        {
            MapCollisionDetector = mapCollisionDetector;
        }

        public void ApplyForce(Vector force)
        {
        }

        public void Move(ICollidableGameObject gameObject, params ICollidableGameObject[] obstacles)
        {
            if (CurrentStep >= DirectionChangeStepsLimit)
            {
                ChangeDirection();
            }

            CurrentStep++;

            var newPosition = gameObject.Position + Direction;

            gameObject.HitBox.Update(newPosition, gameObject.Size);

            var canPass = true;

            do
            {
                canPass = !MapCollisionDetector.Collides(gameObject.HitBox);

                canPass &= !CollidesWithObstacles(gameObject, obstacles);

                if (!canPass)
                {
                    ChangeDirection();
                    newPosition = gameObject.Position + Direction;
                    gameObject.HitBox.Update(newPosition, gameObject.Size);
                }
            } while (!canPass);

            gameObject.Position = newPosition;
        }

        bool CollidesWithObstacles(ICollidableGameObject gameObject, IEnumerable<ICollidableGameObject> obstacles)
        {
            foreach (var obj in obstacles)
            {
                var areClones = AreClones(gameObject, obj);

                if (gameObject.HitBox.Collides(obj.HitBox))
                {
                    if (!areClones)
                    {
                        return true;   
                    }
                }
                else
                {
                    if (areClones)
                    {
                        gameObject.When<IOrganism>(o =>
                        {
                            obj.When<IOrganism>(o2 =>
                            {
                                o.Clones.Remove(o2);
                                o2.Clones.Remove(o);
                            });
                        });
                    }
                }
            }

            return false;
        }

        bool AreClones(IGameObject gameObject, IGameObject obj)
        {
            var isClone = false;

            gameObject.When<IOrganism>(o =>
            {
                obj.When<IOrganism>(o2 =>
                {
                    isClone = o.Clones.Contains(o2) || o2.Clones.Contains(o);
                });
            });

            return isClone;
        }

        void ChangeDirection()
        {
            CurrentStep = 0;
            DirectionChangeStepsLimit = Random.Next(5, 21);

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
