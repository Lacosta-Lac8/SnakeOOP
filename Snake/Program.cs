using Snake.Class;
using Snake.Interface;

class Program
{
    static void Main(string[] args)
    {
        int width = 40, height = 20;
        Console.SetWindowSize(width + 5, height + 3);
        Console.SetBufferSize(width + 5, height + 3);

        var snake = new Snake.Class.Snake(width / 2, height / 2);
        IInputHandler inputHandler = new ConsoleInputHandler();
        IGameRenderer renderer = new ConsoleGameRenderer();
        FoodManager foodManager = new FoodManager(width, height);

        int baseSpeed = 300;
        int accelerationStep = 50;
        int minSpeed = 10;
        GameSpeedManager speedManager = new GameSpeedManager(baseSpeed, accelerationStep, minSpeed);

        GameEngine game = new GameEngine(snake, inputHandler, renderer, foodManager, width, height, speedManager);
        game.RunGameLoop();
    }
}

