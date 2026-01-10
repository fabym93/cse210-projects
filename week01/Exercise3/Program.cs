using System;

class Program
{
    static void Main(string[] args)
    {
        Random randomGenerator = new Random();
        bool playAgain = true;

        while (playAgain)
        {
            int magicNumber = randomGenerator.Next(1, 101); // 1 to 100
            int guess = -1;
            int attempts = 0;

            Console.WriteLine("Guess the number between 1 and 100.");

            while (guess != magicNumber)
            {
                Console.Write("What is your guess? ");
                string input = Console.ReadLine();
                if (!int.TryParse(input, out guess))
                {
                    Console.WriteLine("Please enter a valid number.");
                    continue;
                }

                attempts++;

                if (magicNumber > guess)
                {
                    Console.WriteLine("Higher");
                }
                else if (magicNumber < guess)
                {
                    Console.WriteLine("Lower");
                }
                else
                {
                    Console.WriteLine("You guessed it!");
                }
            }

            // Inform how many guesses were made
            Console.WriteLine($"It took you {attempts} guesses to guess the number.");

            // Ask if the user wants to play again
            Console.Write("Would you like to play again? (yes/no): ");
            string response = Console.ReadLine().Trim().ToLower();

            if (response != "yes")
            {
                playAgain = false;
            }
            // If response is "yes", the loop continues and a new game starts
        }

        Console.WriteLine("Thanks for playing. Bye!");
    }
}