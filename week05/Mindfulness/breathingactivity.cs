using System;

namespace MindfulnessApp
{
    public class BreathingActivity : Activity
    {
        public BreathingActivity()
            : base("Breathing",
                   "This activity will help you relax by walking you through breathing in and out slowly. " +
                   "Clear your mind and focus on your breathing.")
        {
        }

        public override void Run()
        {
            DisplayStartingMessage();

            DateTime endTime = DateTime.Now.AddSeconds(Duration);

            while (DateTime.Now < endTime)
            {
                ShowCountdown("Breathe in...", 4);

                if (DateTime.Now >= endTime) break;

                Console.WriteLine();  // line break to separate inhalation and exhalation
                ShowCountdown("Now breathe out...", 6);
                Console.WriteLine();  // Another jump to make the next cycle clearer.

}

            DisplayEndingMessage();
        }
    }
}