using Snake.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace Snake.Class
{
    public class GameEngine
    {
        private readonly Snake snake;
        private readonly Food food;
        private readonly IInputHandler inputHandler;
        private readonly IGameRenderer renderer;
        private bool gameOver = false;
        private int width = 40, height = 20;
        private int score = 0;
        private readonly int maxGameCells;
        private const int DefaultSleep = 300;
        private const int acceleration = 50;

        public GameEngine(Snake snake, Food food, IInputHandler inputHandler, IGameRenderer renderer)
        {
            this.snake = snake;
            this.food = food;
            this.inputHandler = inputHandler;
            this.renderer = renderer;

            maxGameCells = (width - 2) * (height - 2);
        }

        public void RunGameLoop()
        {
            renderer.HideCursor();

            Console.ForegroundColor = ConsoleColor.DarkBlue;
            DrawBorders();
            Console.ResetColor();

            food.Generate(snake.Body);

            while (!gameOver)
            {
                bool grew = false;
                var tailPosition = snake.Body.Last();

                snake.ChangeDirection(inputHandler.GetInput());
                if (snake.WillCollideWithFood(food))
                {
                    grew = true;
                    score += 10;
                    food.Generate(snake.Body);
                }

                snake.Move(grew);

                if (snake.CheckCollision(width, height))
                {
                    gameOver = true;
                    break;
                }

                if (!grew)
                {
                    renderer.RenderObject(tailPosition.x,tailPosition.y, ' ');
                }

                DrawGameObjects();
                Console.ForegroundColor = ConsoleColor.DarkYellow;
                renderer.RenderScore(score, height);
                Console.ResetColor();

                int desiredSleepTime = score switch
                {
                    < 100 => DefaultSleep,
                    < 500 => DefaultSleep - acceleration,
                    < 1000 => DefaultSleep - acceleration * 2,
                    < 2000 => DefaultSleep - acceleration * 3,
                    < 5000 => DefaultSleep - acceleration * 4,
                    _      => DefaultSleep - acceleration * 5
                };

                int actualSleepTime = Math.Max(10, desiredSleepTime);
                Thread.Sleep(actualSleepTime);
            }

            if (snake.Body.Count >= maxGameCells)
            {
                renderer.RenderWin(score);
            }
            
            renderer.RenderGameOver(score);
        }

        private void DrawBorders()
        {
            for (int i = 0; i < width; i++)
            {
                renderer.RenderObject(i, 0, '#');
                renderer.RenderObject(i, height - 1, '#');
            }

            for (int i = 0; i < height; i++)
            {
                renderer.RenderObject(0, i, '#');
                renderer.RenderObject(width - 1, i, '#');
            }
        }

        private void DrawGameObjects()
        {
            foreach (var part in snake.Body)
            {
                Console.ForegroundColor = ConsoleColor.DarkGreen;
                renderer.RenderObject(part.x, part.y, snake.symbol);
                Console.ResetColor();
            }
            Console.ForegroundColor = ConsoleColor.DarkMagenta;
            renderer.RenderObject(food.x, food.y, food.symbol);
            Console.ResetColor();
        }
    }
}
