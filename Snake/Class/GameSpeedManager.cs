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

        public GameSpeedManager(int baseSleepTime, int accelerationStep, int minSleepTime = 10)
        {
            this.baseSleepTime = baseSleepTime;
            this.accelerationStep = accelerationStep;
            this.minSleepTime = minSleepTime;
        }

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

            int actualSleepTime = Math.Max(minSleepTime, desiredSleepTime);

            if (isVerticalMovement)
                actualSleepTime *= 2;
            return actualSleepTime;
        }
    }
}
