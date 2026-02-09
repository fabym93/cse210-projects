using System;

namespace MindfulnessApp
{
    class Program
    {
       // Exceeding requirements implemented:
    // - In ReflectionActivity: questions are shuffled and cycled to prevent repetition 
    //   until all have been used at least once in the session.
        static void Main(string[] args)
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("Menu Options:");
                Console.WriteLine("  1. Start breathing activity");
                Console.WriteLine("  2. Start reflection activity");
                Console.WriteLine("  3. Start listing activity");
                Console.WriteLine("  4. Quit");
                Console.Write("Select a choice from the menu: ");

                string choice = Console.ReadLine()?.Trim();

                Activity activity = choice switch
                {
                    "1" => new BreathingActivity(),
                    "2" => new ReflectionActivity(),
                    "3" => new ListingActivity(),
                    "4" => null,
                    _   => null
                };

                if (activity == null)
                {
                    if (choice == "4")
                    {
                        Console.WriteLine("\nThank you for using the Mindfulness program. Goodbye!");
                        break;
                    }
                    Console.WriteLine("Invalid option. Press Enter to continue...");
                    Console.ReadLine();
                    continue;
                }

                activity.Run();
            }
        }
    }
}