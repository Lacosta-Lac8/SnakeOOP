using Snake.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace Snake.Class.Bonus
{
    public class SlowBonus : IGameBonus
    {
        public string Name => "Slow Down";
        public int DurationMs => 4000;
        public char Symbol => 'Z';
        public ConsoleColor Color => ConsoleColor.Blue;

        public void Apply(GameEngine engine) => engine.DefaultSleep *= 2;
        public void Revert(GameEngine engine) => engine.DefaultSleep /= 2;
    }
}
