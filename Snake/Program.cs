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
        var food = new Food(width, height);
        IInputHandler inputHandler = new ConsoleInputHandler();
        IGameRenderer renderer = new ConsoleGameRenderer();

        GameEngine game = new GameEngine(snake, food, inputHandler, renderer);
        game.RunGameLoop();
    }
}

