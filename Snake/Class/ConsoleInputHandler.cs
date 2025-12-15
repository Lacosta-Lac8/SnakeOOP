using Snake.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace Snake.Class
{
    public class ConsoleInputHandler : IInputHandler 
    {
        public ConsoleKeyInfo? GetInput()
        {
            ConsoleKeyInfo? currentInput = null;
            while (Console.KeyAvailable)
            {
                currentInput = Console.ReadKey(true);
            }
            return currentInput;
        }
    }
}
