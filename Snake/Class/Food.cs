using Snake.Class.Bonus;
using Snake.Interface;
using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Text;

namespace Snake.Class
{
    public class Food
    {
        public List<(int x, int y)> Positions { get; private set; } = new List<(int x, int y)>();
        public IGameBonus Bonus { get; private set; }
        public SimpleFood Simple { get; private set; }
        public DateTime? ExpiryTime { get; private set; }
        private List<Type> bonusTypes = new List<Type> { typeof(SpeedBonus), typeof(SlowBonus), typeof(GrowthBonus) };
        public char Symbol => (Bonus?.Symbol ?? Simple?.Symbol) ?? '*';
        public ConsoleColor Color => (Bonus?.Color ?? Simple?.Color) ?? ConsoleColor.White;
        public void SetBonus(IGameBonus Bonus, int durationMs)
        {
            this.Bonus = Bonus;
            this.Simple = null;
            this.ExpiryTime = DateTime.Now.AddMilliseconds(durationMs);
        }
        public void SetSimpleFood(SimpleFood simpleFood)
        {
            this.Simple = simpleFood;
            this.Bonus = null;
            this.ExpiryTime = null;
        }
    }
}
