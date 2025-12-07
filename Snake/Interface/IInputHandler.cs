using System;
using System.Collections.Generic;
using System.Text;

namespace Snake.Interface
{
    public interface IInputHandler
    {
        ConsoleKeyInfo? GetInput();
    }
}
