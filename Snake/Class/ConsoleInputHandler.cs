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
            return Console.KeyAvailable ? Console.ReadKey(true) : (ConsoleKeyInfo?)null;
        }
    }
}
