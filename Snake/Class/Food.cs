using Snake.Interface;
using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Text;

namespace Snake.Class
{
    public class Food : IGameObject 
    {
        public int x { get; private set; }
        public int y { get; private set; }
        public char symbol => '*';
        private Random random = new Random();
        private int width, height;

        public Food(int width, int height)
        {
            this.width = width;
            this.height = height;
            Generate();
        }

        public void Generate(List<(int x, int y)> snakeBody = null) 
        {
            bool onSnake;
            do
            {
                x = random.Next(1, width - 1);
                y = random.Next(1, height - 1);

                onSnake = snakeBody != null && snakeBody.Any(b => b.x == x && b.y == y);
            } while (onSnake);
        }
    }
}
