using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DotNetNinja.TypeFiltering;

namespace LifeSimulation.Core
{
    public class CollisionEngine : ICollisionEngine
    {
        IMapCollisionDetector _mapCollisionDetector;
        List<ICollisionResponse> _collisionResponses;

        public CollisionEngine(IMapCollisionDetector mapCollisionDetector)
        {
            _mapCollisionDetector = mapCollisionDetector;
            _collisionResponses = new List<ICollisionResponse>
            {
                new OrganismFoodCollisionResponse()
            };
        }

        //public void Run(IGameObject[] objects)
        //{
        //    var collidableObjects = objects.OfType<ICollidableGameObject>().ToArray();

        //    for (int i = 0; i < collidableObjects.Length; i++)
        //    {
        //        var obj1 = collidableObjects[i];

        //        var canPass = true;

        //        obj1.When<IMovingGameObject>(o => 
        //        {
        //            o.Mover.Move(o, objects);

        //            if (_mapCollisionDetector.Collides(obj1.HitBox))
        //            {
        //                canPass = false;
        //            }
        //        });

        //        for (int j = i + 1; j < collidableObjects.Length; j++)
        //        {
        //            var obj2 = collidableObjects[j];

        //            if (obj1.HitBox.Collides(obj2.HitBox))
        //            {
        //                if (obj1.IsObstacle && obj2.IsObstacle)
        //                {
        //                    canPass = false;
        //                }
        //                Handle(obj1, obj2);
        //            }
        //        }
        //    }
        //}

        public ICollisionEngineRunSummary Run(IGameObject[] objects)
        {
            var collidableObjects = objects.OfType<ICollidableGameObject>().ToArray();

            var runSummary = new CollisionEngineRunSummary();

            for (int i = 0; i < collidableObjects.Length; i++)
            {
                var @object = collidableObjects[i];

                @object.When<IMovingGameObject>(o => o.Mover.Move(o, objects));

                ICollisionTestResult collisionTestResult = null;
                while (true)
                {
                    collisionTestResult = TestCollisions(@object, collidableObjects.Skip(i + 1), runSummary);

                    if (ShouldRepeatCollisionTest(collisionTestResult))
                    {
                        @object
                            .When<IMovingGameObject>(o =>
                            {
                                o.Mover.RollbackMove(o);
                                o.Mover.ChangeDirection(o, objects);
                                o.Mover.Move(o, objects);
                            })
                            .ThrowIfNotRecognized();
                    }
                    else
                    {
                        break;
                    }
                }

                foreach (var response in _collisionResponses)
                {
                    response.Run((ICompleteCollisionTestResult) collisionTestResult);
                }
            }

            return runSummary;
        }

        /// <summary>
        /// Checks collisions of @object with remainingObjects. Returns null if @object is obstacle and map bounds or other obstacle were collided
        /// (meaning the @object needs to change its position in order to proceed).
        /// </summary>
        /// <param name="object">Target object for checking collisions.</param>
        /// <param name="remainingObjects">Remaining objects from collidable game objects collection.</param>
        ICollisionTestResult TestCollisions(ICollidableGameObject @object, IEnumerable<ICollidableGameObject> remainingObjects,
                                            ICollisionEngineRunSummary runSummary)
        {
            if (_mapCollisionDetector.Collides(@object.HitBox))
            {
                return new MapExceededCollisionTestResult(@object);
            }

            var collidesWith = new List<ICollidableGameObject>();

            foreach (var otherObject in remainingObjects)
            {
                if (@object.HitBox.Collides(otherObject.HitBox))
                {
                    if (ObstacleTest(@object, otherObject))
                    {
                        return new ObstacleHitCollisionTestResult(@object, otherObject);
                    }

                    collidesWith.Add(otherObject);
                    runSummary.AddCollision(@object, otherObject);
                }
            }
            
            //@object.When<IOrganism>(o => o.IsObstacle = true);

            return new CompleteCollisionTestResult(@object, collidesWith);
        }

        bool ShouldRepeatCollisionTest(ICollisionTestResult testResult)
        {
            var shouldRepeat = false;

            testResult
                .When<IMapExceededCollisionTestResult>(r => shouldRepeat = true)
                .When<IObstacleHitCollisionTestResult>(r => shouldRepeat = true);
                //.When<IObstacleHitCollisionTestResult>(r =>
                //{
                //    shouldRepeat = true;

                //    if (IsOrganismClone(r.TestTarget) || IsOrganismClone(r.Obstacle))
                //    {
                //        shouldRepeat = false;
                //    }
                //});

            return shouldRepeat;
        }

        bool ObstacleTest(ICollidableGameObject @object, ICollidableGameObject otherObject)
        {
            var isObstacle = @object.IsObstacle && otherObject.IsObstacle;

            Action<IOrganism> organismObstacleCondition = o => isObstacle &= !o.IsClone;

            @object.When<IOrganism>(o => organismObstacleCondition(o));
            otherObject.When<IOrganism>(o => organismObstacleCondition(o));

            return isObstacle;
        }

        bool IsOrganismClone(ICollidableGameObject obj)
        {
            return (obj as IOrganism)?.IsClone ?? false;
        }
    }
}
