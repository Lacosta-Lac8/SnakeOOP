using Snake.Interface;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Threading;

namespace Snake.Class
{
    public class GameEngine
    {
        private readonly Snake snake;
        private readonly IInputHandler inputHandler;
        private readonly IGameRenderer renderer;
        private readonly GameSpeedManager speedManager;
        private bool gameOver = false;
        private bool isGameWon = false;
        private int width = 40, height = 20;
        private int score = 0;
        private const int acceleration = 50;
        private readonly int maxScore;
        public int DefaultSleep { get; set;} = 300;
        private readonly FoodManager foodManager;

        private class ActiveBonus
        {
            public IGameBonus bonus { get; }
            public DateTime ExpiryTime { get; }

            public ActiveBonus(IGameBonus bonus)
            {
                this.bonus = bonus;
                ExpiryTime = DateTime.Now.AddMilliseconds(bonus.DurationMs);
            }
        }

        private List<ActiveBonus> activeBonuses = new List<ActiveBonus>();

        public GameEngine(Snake snake, IInputHandler inputHandler, IGameRenderer renderer, FoodManager foodManager, int width, int height, GameSpeedManager speedManager)
        {
            this.snake = snake;
            this.inputHandler = inputHandler;
            this.renderer = renderer;
            this.foodManager = foodManager;
            this.width = width;
            this.height = height;
            this.speedManager = speedManager;

            maxScore = (width - 2) * (height - 2) * 10;

            foodManager.GenerateNewSimpleFood(snake.Body);
            foodManager.GenerateNewBonusFood(snake.Body);
        }

        public void RunGameLoop()
        {
            renderer.HideCursor();

            Console.ForegroundColor = ConsoleColor.DarkBlue;
            DrawBorders();
            Console.ResetColor();

            while (!gameOver)
            {
                EraseSnake();

                int actualSleepTime = speedManager.CalculateActuslSleepTime(score, snake.Dy != 0);

                var input = inputHandler.GetInput();
                snake.ChangeDirection(input);

                foodManager.RemoveExpiredFood(renderer);
                CheckExpiredBonuses();

                var nextx = snake.x + snake.Dx;
                var nexty = snake.y + snake.Dy;
                var eatenFood = foodManager.CheckCollision(nextx, nexty);

                if (eatenFood != null)
                {
                    foodManager.EraseSingleFood(renderer, eatenFood);

                    if (eatenFood.Bonus != null)
                    {
                        eatenFood.Bonus.Apply(this);
                        if (eatenFood.Bonus.DurationMs > 0)
                        {
                            activeBonuses.Add(new ActiveBonus(eatenFood.Bonus));
                        }
                        score += 10;
                        foodManager.GenerateNewBonusFood(snake.Body);
                    }
                    else if (eatenFood.Simple != null)
                    {
                        GrowSnake(1);
                        score += 10;
                        foodManager.GenerateNewSimpleFood(snake.Body);
                    }
                    foodManager.RemoveFood(eatenFood);
                }

                snake.Move(false);

                if (snake.x <= 0 || snake.x >= width - 1 || snake.y <= 0 || snake.y >= height - 1 || snake.CheckCollisionWithSelf())
                {
                    gameOver = true;
                    isGameWon = false;

                }

                if (score >= maxScore && !gameOver)
                {
                    gameOver = true;
                    isGameWon = true;
                }

                DrawGameObjects();
                DrawScore();

                Thread.Sleep(actualSleepTime);
            }

            renderer.ClearScreen();

            if (isGameWon)
            {
                WinScreen();
            }
            else
            {
                GameOverScreen();
            }
        }

        public void GrowSnake(int segments)
        {
            for (int i = 0; i < segments; i++)
                snake.Body.Add((snake.x, snake.y));
        }

        public void CheckExpiredBonuses()
        {
            var expiredBonuses = activeBonuses.Where(b => DateTime.Now > b.ExpiryTime).ToList();
            foreach (var expired in expiredBonuses)
            {
                expired.bonus.Revert(this);
                activeBonuses.Remove(expired);
            }
        }

        private void DrawBorders()
        {
            for (int i = 0; i < width; i++)
            {
                renderer.RenderObject(i, 0, "#", ConsoleColor.DarkBlue);
                renderer.RenderObject(i, height - 1, "#", ConsoleColor.DarkBlue);
            }

            for (int i = 0; i < height; i++)
            {
                renderer.RenderObject(0, i, "#", ConsoleColor.DarkBlue);
                renderer.RenderObject(width - 1, i, "#", ConsoleColor.DarkBlue);
            }
        }

        private void DrawGameObjects()
        {
            foreach (var part in snake.Body)
            {
                renderer.RenderObject(part.x, part.y, snake.symbol.ToString(), snake.Color);
            }
            foodManager.Draw(renderer);
        }

        private void DrawScore()
        {
            renderer.RenderObject(0, height, $"Score: {score}   ", ConsoleColor.DarkYellow);
        }

        private void GameOverScreen()
        {
            renderer.ClearScreen();
            string message = "Game Over";
            renderer.RenderObject(width / 2 - message.Length / 2, height / 2, message, ConsoleColor.DarkRed);
        }

        private void WinScreen()
        {
            renderer.ClearScreen();
            string message = "You Win! :";
            renderer.RenderObject(width / 2 - message.Length / 2, height / 2, message, ConsoleColor.DarkYellow);
        }

        private void EraseSnake()
        {
            foreach (var part in snake.Body)
            {
                renderer.RenderObject(part.x, part.y, " ", ConsoleColor.White);
            }
        }
    }
}
