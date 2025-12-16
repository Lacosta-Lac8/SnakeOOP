using System;
using System.Collections.Generic;
using System.Text;

namespace Snake.Class
{
    public class GameSpeedManager
    {
        private readonly int baseSleepTime;
        private readonly int accelerationStep;
        private readonly int minSleepTime;
        private readonly List<double> modifiers = new List<double>();

        public GameSpeedManager(int baseSleepTime, int accelerationStep, int minSleepTime = 10)
        {
            this.baseSleepTime = baseSleepTime;
            this.accelerationStep = accelerationStep;
            this.minSleepTime = minSleepTime;
        }

        public void AddModifier(double multiplier) => modifiers.Add(multiplier);
        public void RemoveModifier(double multiplier) => modifiers.Remove(multiplier);

        public int CalculateActuslSleepTime(int score, bool isVerticalMovement)
        {
            int desiredSleepTime = score switch
            {
                < 10 => baseSleepTime,
                < 30 => baseSleepTime - accelerationStep,
                < 70 => baseSleepTime - accelerationStep * 2,
                < 120 => baseSleepTime - accelerationStep * 3,
                < 150 => baseSleepTime - accelerationStep * 4,
                _ => baseSleepTime - accelerationStep * 5
            };

            double totalMultiplier = 1.0;
            foreach (var mod in modifiers)
            {
                totalMultiplier *= mod;
            }

            double finalSleep = desiredSleepTime * totalMultiplier;


            int result = Math.Max(minSleepTime, (int)finalSleep);
            return isVerticalMovement ? result * 2 : result;
        }
    }
}
