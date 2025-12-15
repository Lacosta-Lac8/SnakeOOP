using Snake.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace Snake.Class.Bonus
{
    public class GrowthBonus : IGameBonus
    {
        public string Name => "Growth (4 cells)";
        public int DurationMs => 1;
        public char Symbol => 'G';
        public ConsoleColor Color => ConsoleColor.Green;

        public void Apply(GameEngine engine) => engine.GrowSnake(2);
        public void Revert(GameEngine engine) { }
    }
}
