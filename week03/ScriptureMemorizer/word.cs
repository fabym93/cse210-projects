// Word: represents a single word and whether it is hidden
public class Word
{
    private string _text;
    private bool _hidden;

    public Word(string text)
    {
        _text = text;
        _hidden = false;
    }

    public void Hide()
    {
        _hidden = true;
    }

    public bool IsHidden => _hidden;

    // Display: if hidden, underscores of same length; otherwise original text
    public string GetDisplay()
    {
        if (!_hidden)
            return _text;
        // Replace with underscores, preserving length
        return new string('_', _text.Length);
    }

    public string Text => _text;
}