using Snake.Class;
using System;
using System.Collections.Generic;
using System.Text;

namespace Snake.Interface
{
    public interface IGameBonus
    {
        string Name { get; }
        int DurationMs { get; }
        char Symbol { get; }
        ConsoleColor Color { get; }

        void Apply(GameEngine engine);
        void Revert(GameEngine engine);
    }
}
