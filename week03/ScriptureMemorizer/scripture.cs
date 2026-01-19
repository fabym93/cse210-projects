using System;
using System.Collections.Generic;
using System.Linq;

// Scripture: holds the reference and the list of word tokens
public class Scripture
{
    private Reference _reference;
    private List<Word> _words;
    private Random _random;

    public Scripture(Reference reference, string text)
    {
        _reference = reference;
        _words = Tokenize(text);
        _random = new Random();
    }

    // Tokenize by spaces; punctuation stays with words
    private List<Word> Tokenize(string text)
    {
        var tokens = text.Split(' ', StringSplitOptions.RemoveEmptyEntries);
        var list = new List<Word>();
        foreach (var t in tokens)
        {
            list.Add(new Word(t));
        }
        return list;
    }

    // Display: reference + script
    public string GetDisplay()
    {
        var displayWords = _words.Select(w => w.GetDisplay());
        return $"{_reference}\n{string.Join(" ", displayWords)}";
    }

    // Hide a single random non-hidden word; if all hidden, do nothing
    public void HideRandomWord()
    {
        var notHidden = _words.Where(w => !w.IsHidden).ToList();
        if (notHidden.Count == 0) return;
        var toHide = notHidden[_random.Next(notHidden.Count)];
        toHide.Hide();
    }

    // Hide multiple words (optional utility)
    public void HideRandomWords(int count)
    {
        if (count <= 0) return;
        var remaining = _words.Where(w => !w.IsHidden).ToList();
        if (remaining.Count == 0) return;

        int toHideCount = Math.Min(count, remaining.Count);

        for (int i = 0; i < toHideCount; i++)
        {
            var pool = _words.Where(w => !w.IsHidden).ToList();
            if (pool.Count == 0) break;
            var pick = pool[new Random().Next(pool.Count)];
            pick.Hide();
        }
    }

    // Check if all words hidden
    public bool IsFullyHidden()
    {
        return _words.All(w => w.IsHidden);
    }

    // For external use (e.g., tests)
    public int VisibleWordCount => _words.Count(w => !w.IsHidden);

    // Expose reference for display
    public Reference Reference => _reference;
}