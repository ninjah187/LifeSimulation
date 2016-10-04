﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LifeSimulation.Core
{
    public class FoodTrackingMover : Mover
    {
        public override void ChangeDirection(IGameObject gameObject, params IGameObject[] objects)
        {
            CurrentStep = 0;

            //var closestFood = nearby.OfType<IFood>().Aggregate((curMin, f) => curMin == null || curMin.)
            var closestFood = GetClosestFood(gameObject, objects);

            if (closestFood != null && Random.NextDouble() < 0.9)
            {
                DirectionChangeStepsLimit = 1;
                FollowClosestFood(gameObject, closestFood);
            }
            else
            {
                DirectionChangeStepsLimit = Random.Next(5, 21);
                Direction = new Vector
                {
                    X = Random.Next(-1, 2),
                    Y = Random.Next(-1, 2)
                };
            }
        }

        IFood GetClosestFood(IGameObject gameObject, IGameObject[] objects)
        {
            IFood closestFood = null;
            var smallestFoodDistance = double.MaxValue;
            foreach (var food in objects.OfType<IFood>())
            {
                if (closestFood == null)
                {
                    closestFood = food;
                    smallestFoodDistance = (gameObject.Position - food.Position).Length;
                    continue;
                }

                var currentFoodDistance = (gameObject.Position - food.Position).Length;

                if (currentFoodDistance < smallestFoodDistance)
                {
                    smallestFoodDistance = currentFoodDistance;
                    closestFood = food;
                }
            }
            return closestFood;
        }

        void FollowClosestFood(IGameObject gameObject, IFood closestFood)
        {
            var direction = new Vector();

            if (gameObject.Position.X < closestFood.Position.X)
            {
                direction.X = 1;
            }
            else if (gameObject.Position.X == closestFood.Position.X)
            {
                direction.X = 0;
            }
            else if (gameObject.Position.X > closestFood.Position.X)
            {
                direction.X = -1;
            }

            if (gameObject.Position.Y < closestFood.Position.Y)
            {
                direction.Y = 1;
            }
            else if (gameObject.Position.Y == closestFood.Position.Y)
            {
                direction.Y = 0;
            }
            else if (gameObject.Position.Y > closestFood.Position.Y)
            {
                direction.Y = -1;
            }

            Direction = direction;
        }
    }
}