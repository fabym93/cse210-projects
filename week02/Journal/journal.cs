using System;
using System.Collections.Generic;
using System.IO;

public class Journal
{
    private List<Entry> _entries = new List<Entry>();

    public List<Entry> Entries => _entries;

    public void AddEntry(Entry newEntry)
    {
        if (newEntry == null) throw new ArgumentNullException(nameof(newEntry));
        _entries.Add(newEntry);
    }

    public void DisplayAll()
    {
        foreach (var entry in _entries)
        {
            entry.Display();
            Console.WriteLine(new string('-', 40));
        }
    }

    public void SaveToFile(string file)
    {
        using (var writer = new StreamWriter(file))
        {
            foreach (var e in _entries)
            {
                writer.WriteLine($"{e.Date}|{e.PromptText}|{e.EntryText}");
            }
        }
    }

    public void LoadFromFile(string file)
    {
        if (!File.Exists(file)) return;

        _entries.Clear();
        foreach (var line in File.ReadAllLines(file))
        {
            var parts = line.Split('|');
            if (parts.Length >= 3)
            {
                var entry = new Entry(parts[0], parts[1], parts[2]);
                _entries.Add(entry);
            }
        }
    }
}