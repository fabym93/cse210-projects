using System;

public class Entry
{
    public string Date { get; }
    public string PromptText { get; }
    public string EntryText { get; }

    public Entry(string date, string promptText, string entryText)
    {
        Date = date;
        PromptText = promptText;
        EntryText = entryText;
    }

    public void Display()
    {
        Console.WriteLine($"Fecha: {Date}");
        Console.WriteLine($"Prompt: {PromptText}");
        Console.WriteLine($"Entrada: {EntryText}");
    }
}