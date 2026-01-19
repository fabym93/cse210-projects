using System;

// COMMENTS ABOUT EXCEEDING REQUIREMENTS
//The program selects a passage at random at startup and runs the same flow 
//(press Enter to hide a word, quit to exit). 
//It uses the same 4 files: Reference.cs, Word.cs, Scripture.cs, and Program.cs

// Main program: choose a random Old Testament passage and run the interaction
public class Program
{
    public static void Main(string[] args)
    {
        // Scripture library (Old Testament) - short example texts
        var scripturesLibrary = new List<(Reference reference, string text)>
        {
            // Genesis 1:1 (example text)
            (new Reference("Genesis", 1, 1),
             "In the beginning God created the heavens and the earth."),

            // Deuteronomy 6:4-5 (short example text)
            (new Reference("Deuteronomy", 6, 4, 5),
             "Hear, O Israel: The LORD our God, the LORD is one. You shall love the LORD your God with all your heart, and with all your soul and with all your strength."),

            // Psalm 23:1-3 (short example text)
            (new Reference("Psalms", 23, 1, 3),
             "The LORD is my shepherd; I shall not want. He makes me lie down in green pastures. He leads me beside still waters."),

            // Proverbs 3:5-6
            (new Reference("Proverbs", 3, 5, 6),
             "Trust in the LORD with all your heart and lean not on your own understanding; in all your ways submit to him, and he will make your paths straight.")
        };

        // Pick a passage at random from the library
        var random = new Random();
        var chosen = scripturesLibrary[random.Next(scripturesLibrary.Count)];

        // Create the Scripture with the selected passage
        var scripture = new Scripture(chosen.reference, chosen.text);

        // Interaction flow
        Console.Clear();
        Console.WriteLine("Scripture Memorizer - randomly selected passage:");
        Console.WriteLine();
        Console.WriteLine(scripture.GetDisplay());
        Console.WriteLine();
        Console.WriteLine("Press Enter to hide a word, or type 'quit' to exit.");

        while (true)
        {
            var input = Console.ReadLine();
            if (input != null && input.Trim().Equals("quit", StringComparison.OrdinalIgnoreCase))
            {
                break;
            }

            // Hide a random non-hidden word
            scripture.HideRandomWord();

            // Clear and redraw
            Console.Clear();
            Console.WriteLine(scripture.GetDisplay());

            // End if all words are hidden
            if (scripture.IsFullyHidden())
            {
                Console.WriteLine();
                Console.WriteLine("All words are hidden. Press any key to exit.");
                Console.ReadKey(intercept: true);
                break;
            }

            Console.WriteLine();
            Console.WriteLine("Press Enter to hide another word, or type 'quit' to exit.");
        }
    }
}