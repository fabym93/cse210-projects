using System;
using System.Collections.Generic;

namespace MindfulnessApp
{

    //// Exceeding requirements improvement: Questions are shuffled at the start of the activity 
    // and displayed in random order without repeating any until all have been used at least once.
    // 
    // Location in the code:
    // - ReflectionActivity.cs, Run() method:
    //   - Around here: List<string> questions = new List<string>(_questions); Shuffle(questions);
    //   - Inside the while loop: if (index >= questions.Count) { reshuffle and reset index = 0 }
    // 
    // How it works:
    // 1. A copy of the original questions list is created and shuffled once when the activity begins.
    // 2. Questions are shown sequentially in that random order (index 0, 1, 2...).
    // 3. When all questions have been used (index >= Count), the list is reshuffled completely 
    //    and the index is reset to 0, allowing a new cycle without immediate repetitions.
    // This ensures greater variety and ensures the user sees different questions before any repetition occurs.
    public class ReflectionActivity : Activity
    {
        private readonly List<string> _prompts = new()
        {
            "Think of a time when you stood up for someone else.",
            "Think of a time when you did something really difficult.",
            "Think of a time when you helped someone in need.",
            "Think of a time when you did something truly selfless."
        };

        private readonly List<string> _questions = new()
        {
            "Why was this experience meaningful to you?",
            "Have you ever done anything like this before?",
            "How did you get started?",
            "How did you feel when it was complete?",
            "What made this time different than other times when you were not as successful?",
            "What is your favorite thing about this experience?",
            "What could you learn from this experience that applies to other situations?",
            "What did you learn about yourself through this experience?",
            "How can you keep this experience in mind in the future?"
        };

        public ReflectionActivity()
            : base("Reflection",
                   "This activity will help you reflect on times in your life when you have shown strength " +
                   "and resilience. This will help you recognize the power you have and how you can use it " +
                   "in other aspects of your life.")
        {
        }

        public override void Run()
        {
            DisplayStartingMessage();

            Random rand = new();
            string prompt = _prompts[rand.Next(_prompts.Count)];

            Console.WriteLine("Consider the following prompt:\n");
            Console.WriteLine($" --- {prompt} --- \n");
            Console.Write("When you have something in mind, press enter to continue.");
            Console.ReadLine();

            Console.WriteLine("\nNow ponder on each of the following questions as they relate to this experience.");
            Console.Write("You may begin in: ");
            ShowCountdown(5);
            Console.Clear();

            // Shuffle once at the beginning
            List<string> questions = new(_questions);
            Shuffle(questions); // using helper method below

            int index = 0;

            DateTime endTime = DateTime.Now.AddSeconds(Duration);

            while (DateTime.Now < endTime)
            {
                if (index >= questions.Count)
                {
                    // Reshuffle when we run out
                    questions = new List<string>(_questions);
                    Shuffle(questions);
                    index = 0;
                }

                Console.Write($"> {questions[index]}  ");
                ShowSpinner(10);
                Console.WriteLine();
                index++;
            }

            DisplayEndingMessage();
        }

        private static void Shuffle<T>(List<T> list)
        {
            Random rng = new Random();
            int n = list.Count;
            while (n > 1)
            {
                n--;
                int k = rng.Next(n + 1);
                (list[k], list[n]) = (list[n], list[k]);
            }
        }
    }
}