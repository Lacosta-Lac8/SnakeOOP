using Snake.Class.Bonus;
using Snake.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace Snake.Class
{
    public class FoodManager
    {
        private List<Food> activeFoods = new List<Food>();
        private Random random = new Random();
        private int width, height;
        private List<Type> bonusTypes = new List<Type> { typeof(SpeedBonus), typeof(SlowBonus), typeof(GrowthBonus) };

        public FoodManager(int width, int height)
        {
            this.width = width;
            this.height = height;
        }

        public void GenerateNewSimpleFood(List<(int x, int y)> snakeBody)
        {
            var newFood = new Food();
            newFood.SetSimpleFood(new SimpleFood());
            GenerateCoordinates(newFood, 1, 1, snakeBody);
            activeFoods.Add(newFood);
        }

        public void GenerateNewBonusFood(List<(int x, int y)> snakeBody)
        {
            Type bonusType = bonusTypes[random.Next(bonusTypes.Count)];
            IGameBonus bonusInstance = (IGameBonus)Activator.CreateInstance(bonusType);

            var newFood = new Food();
            newFood.SetBonus(bonusInstance, 5000);

            bool isLargeFood = newFood.Bonus is GrowthBonus;
            int foodWidth = isLargeFood ? 2 : 1;
            int foodHeight = isLargeFood ? 2 : 1;

            GenerateCoordinates(newFood, foodWidth, foodHeight, snakeBody);
            activeFoods.Add(newFood);
        }

        public bool RemoveExpiredFood(IGameRenderer renderer)
        {
            var expired = activeFoods.Where(f => f.ExpiryTime.HasValue && DateTime.Now > f.ExpiryTime.Value).ToList();
            bool bonusRemoved = false;

            foreach (var food in expired)
            {
                EraseSingleFood(renderer, food);
                activeFoods.Remove(food);

                if (food.Bonus != null)
                {
                    bonusRemoved = true;
                }
            }
            return bonusRemoved;
        }

        public void EraseSingleFood(IGameRenderer renderer, Food foodItem)
        {
            foreach (var pos in foodItem.Positions)
            {
                renderer.RenderObject(pos.x, pos.y, " ");
            }
        }

        public void Draw(IGameRenderer renderer)
        {
            foreach (var foodItem in activeFoods)
            {
                foreach (var pos in foodItem.Positions)
                {
                    renderer.RenderObject(pos.x, pos.y, foodItem.Symbol.ToString(), foodItem.Color);
                }
            }
        }

        public void Erase(IGameRenderer renderer)
        {
            foreach (var foodItem in activeFoods)
            {
                foreach (var pos in foodItem.Positions)
                {
                    renderer.RenderObject(pos.x, pos.y, " ");
                }
            }
        }

        public Food CheckCollision(int nextX, int nextY)
        {
            return activeFoods.FirstOrDefault(foodItem => foodItem.Positions.Any(p => p.x == nextX && p.y == nextY));
        }

        public void RemoveFood(Food foodItem)
        {
            activeFoods.Remove(foodItem);
        }

        private void GenerateCoordinates(Food newFood, int widthFood, int heightFood, List<(int x, int y)> snakeBody)
        {
            bool collision;
            do
            {
                int startx = random.Next(1, this.width - widthFood);
                int starty = random.Next(1, this.height - heightFood);

                newFood.Positions.Clear();
                for (int x = startx; x < startx + widthFood; x++)
                {
                    for (int y = starty; y < starty + heightFood; y++)
                    {
                        newFood.Positions.Add((x, y));
                    }
                }

                collision = snakeBody != null && snakeBody.Any(snakePart =>
                    newFood.Positions.Any(foodPart => foodPart.x == snakePart.x && foodPart.y == snakePart.y)
                );
            } while (collision);
        }
    }
}
