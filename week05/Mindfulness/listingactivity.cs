using System;
using System.Collections.Generic;

namespace MindfulnessApp
{
    public class ListingActivity : Activity
    {
        private readonly List<string> _prompts = new()
        {
            "Who are people that you appreciate?",
            "What are personal strengths of yours?",
            "Who are people that you have helped this week?",
            "When have you felt the Holy Ghost this month?",
            "Who are some of your personal heroes?"
        };

        public ListingActivity()
            : base("Listing",
                   "This activity will help you reflect on the good things in your life by having you " +
                   "list as many things as you can in a certain area.")
        {
        }

        public override void Run()
        {
            DisplayStartingMessage();

            Random rand = new();
            string prompt = _prompts[rand.Next(_prompts.Count)];

            Console.WriteLine("List as many responses you can to the following prompt:\n");
            Console.WriteLine($" --- {prompt} --- \n");

            Console.Write("You may begin in: ");
            ShowCountdown(5);
            Console.WriteLine("\n");

            DateTime endTime = DateTime.Now.AddSeconds(Duration);
            int count = 0;

            while (DateTime.Now < endTime)
            {
                Console.Write("> ");
                string input = Console.ReadLine();
                if (!string.IsNullOrWhiteSpace(input))
                {
                    count++;
                }
            }

            Console.WriteLine($"\nYou listed {count} items!");
            DisplayEndingMessage();
        }
    }
}