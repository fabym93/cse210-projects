using System;

class Program
{
    static void Main(string[] args)
    {
        var journal = new Journal();
        var promptGen = new PromptGenerator();

        bool exit = false;
        while (!exit)
        {
            Console.WriteLine("Please select an option:");
            Console.WriteLine("1) New Entry");
            Console.WriteLine("2) Show All Entries");
            Console.WriteLine("3) Save");
            Console.WriteLine("4) Load");
            Console.WriteLine("5) Exit");
            Console.Write("Option: ");

            var input = Console.ReadLine();

            switch (input)
            {
                case "1":
                    CreateNewEntry(journal, promptGen);
                    break;
                case "2":
                    journal.DisplayAll();
                    break;
                case "3":
                    Console.Write("Save file name (e.g., journal.txt): ");
                    var fileSave = Console.ReadLine();
                    journal.SaveToFile(fileSave);
                    Console.WriteLine("Saved.");
                    break;
                case "4":
                    Console.Write("Load file name (e.g., journal.txt): ");
                    var fileLoad = Console.ReadLine();
                    journal.LoadFromFile(fileLoad);
                    Console.WriteLine("Loaded (if existed).");
                    break;
                case "5":
                    exit = true;
                    break;
                default:
                    Console.WriteLine("Invalid option.");
                    break;
            }

            Console.WriteLine();
        }
    }

    private static void CreateNewEntry(Journal journal, PromptGenerator promptGen)
    {
        string date = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
        Console.WriteLine("Use a random prompt? (y/n)");
        var useRandom = Console.ReadLine()?.ToLower() == "y";

        string prompt = useRandom ? promptGen.GetRandomPrompt() : "Enter your prompt:";
        Console.WriteLine($"Prompt: {prompt}");

        Console.Write("Entry: ");
        var entryText = Console.ReadLine();

        var entry = new Entry(date, prompt, entryText);
        journal.AddEntry(entry);
        Console.WriteLine("Entry added.");
    }
}