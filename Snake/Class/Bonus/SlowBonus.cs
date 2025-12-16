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

        public void Apply(GameEngine engine) => engine.speedManager.AddModifier(2.0);
        public void Revert(GameEngine engine) => engine.speedManager.RemoveModifier(2.0);
    }
}
