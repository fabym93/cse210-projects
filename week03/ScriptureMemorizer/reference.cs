using System;

// Reference: encapsulates book, chapter, and verse information
public class Reference
{
    private string _book;
    private int _chapter;
    private int _startVerse;
    private int _endVerse;
    private bool _isRange;

    // Single verse constructor
    public Reference(string book, int chapter, int verse)
    {
        _book = book;
        _chapter = chapter;
        _startVerse = verse;
        _endVerse = verse;
        _isRange = false;
    }

    // Verse range constructor
    public Reference(string book, int chapter, int startVerse, int endVerse)
    {
        _book = book;
        _chapter = chapter;
        _startVerse = startVerse;
        _endVerse = endVerse;
        _isRange = true;
    }

    public override string ToString()
    {
        if (_isRange)
            return $"{_book} {_chapter}:{_startVerse}-{_endVerse}";
        else
            return $"{_book} {_chapter}:{_startVerse}";
    }

    // Expose whether it's a range
    public bool IsRange => _isRange;
    public int StartVerse => _startVerse;
    public int EndVerse => _endVerse;
}