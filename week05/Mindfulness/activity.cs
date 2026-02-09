//Base Class

using System;
using System.Collections.Generic;
using System.Threading;

namespace MindfulnessApp
{
    public abstract class Activity
    {
        protected string Name { get; }
        protected string Description { get; }
        protected int Duration { get; private set; }

        protected Activity(string name, string description)
        {
            Name = name;
            Description = description;
        }

        public abstract void Run();

        /// <summary>
        /// Displays the welcome message, activity description, and asks for session duration.
        /// </summary>
        protected void DisplayStartingMessage()
        {
            Console.Clear();
            Console.WriteLine($"Starting {Name} Activity");
            Console.WriteLine("-----------------------------------");
            Console.WriteLine(Description);
            Console.WriteLine();

            Console.Write("How long, in seconds, would you like for your session? ");

            int seconds;
            while (!int.TryParse(Console.ReadLine(), out seconds) || seconds <= 0)
            {
                Console.Write("Please enter a positive number of seconds: ");
            }

            Duration = seconds;

            Console.Clear();
            Console.WriteLine("Get ready...");
            ShowSpinner(4);
            Console.WriteLine();
        }

        /// <summary>
        /// Shows the completion message with congratulations and duration summary.
        /// </summary>
        protected void DisplayEndingMessage()
        {
            Console.WriteLine("\nGood job!");
            ShowSpinner(3);
            Console.WriteLine($"\nYou have completed another {Duration} seconds of the {Name} Activity.");
            ShowSpinner(4);
            Console.WriteLine("\nPress Enter to return to the menu...");
            Console.ReadLine();
        }

        /// <summary>
        /// Displays a simple spinner animation for the given number of seconds.
        /// </summary>
        /// <param name="seconds">How long the spinner should run</param>
        protected void ShowSpinner(int seconds)
        {
            string[] spinner = { "|", "/", "-", "\\" };
            DateTime endTime = DateTime.Now.AddSeconds(seconds);

            int i = 0;
            while (DateTime.Now < endTime)
            {
                Console.Write(spinner[i]);
                Thread.Sleep(200);
                Console.Write("\b \b");
                i = (i + 1) % spinner.Length;
            }
        }

        /// <summary>
        /// Shows a countdown timer with a custom message (e.g. "Breathe in...").
        /// The message stays visible while the number counts down.
        /// </summary>
        /// <param name="message">The fixed text to show (e.g. "Breathe in...")</param>
        /// <param name="seconds">How many seconds to count down from</param>
        protected void ShowCountdown(string message, int seconds)
        {
            for (int i = seconds; i >= 1; i--)
            {
                // Move cursor to start of line and clear previous content
                Console.Write("\r" + new string(' ', 60) + "\r");

                // Display message + current number
                Console.Write($"{message} {i}   ");

                Thread.Sleep(1000);
            }

            // Final cleanup: leave only the message with ellipsis
            Console.Write("\r" + new string(' ', 60) + "\r");
            Console.Write($"{message}... ");
        }

        /// <summary>
        /// Legacy/simple countdown (used only if no custom message is needed).
        /// Counts down numbers only.
        /// </summary>
        /// <param name="seconds">How many seconds to count down from</param>
        protected void ShowCountdown(int seconds)
        {
            for (int i = seconds; i >= 1; i--)
            {
                Console.Write("\r" + new string(' ', 20) + "\r");
                Console.Write($"{i}   ");
                Thread.Sleep(1000);
            }
            Console.Write("\r" + new string(' ', 20) + "\r");
        }
    }
}