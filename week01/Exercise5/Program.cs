using System;

class Program
{
    // 1) Displays the message, "Welcome to the Program!"
    static void DisplayWelcome()
    {
        Console.WriteLine("Welcome to the Program!");
    }

    // 2) PromptUserName - Asks for and returns the user's name (as a string)
    static string PromptUserName()
    {
        Console.Write("Please enter your name: ");
        string name = Console.ReadLine();
        return name;
    }

    // 3) PromptUserNumber - Asks for and returns the user's favorite number (as an integer)
    static int PromptUserNumber()
    {
        int number;
        while (true)
        {
            Console.Write("Please enter your favorite number: ");
            string input = Console.ReadLine();
            if (int.TryParse(input, out number))
            {
                return number;
            }
            Console.WriteLine("Invalid input. Please enter a valid integer.");
        }
    }

    // 4) SquareNumber - Accepts an integer as a parameter and returns that number squared (as an integer)
    static int SquareNumber(int n)
    {
        return n * n;
    }

    // 5) DisplayResult - Accepts the user's name and the squared number and displays them.
    static void DisplayResult(string name, int squared)
    {
        Console.WriteLine($"{name}, the square of your number is {squared}");
    }

    static void Main(string[] args)
    {
        // Call each function in order, saving return values and passing data as needed
        DisplayWelcome();

        string userName = PromptUserName();
        int userNumber = PromptUserNumber();
        int squared = SquareNumber(userNumber);

        DisplayResult(userName, squared);
    }
}