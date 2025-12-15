using Snake.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace Snake.Class.Bonus
{
    public class SpeedBonus : IGameBonus
    {
        public string Name => "Speed Boost";
        public int DurationMs => 5000;
        public char Symbol => 'S';
        public ConsoleColor Color => ConsoleColor.Cyan;

        public void Apply(GameEngine engine) => engine.DefaultSleep /= 2;
        public void Revert(GameEngine engine) => engine.DefaultSleep *= 2;
    }
}
