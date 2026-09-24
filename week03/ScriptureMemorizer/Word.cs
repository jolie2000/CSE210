using System;

// Represents one word and manages whether that word is visible or hidden.
public class Word
{
    // The original word is kept so it can be displayed until it is hidden.
    private string _text;
    private bool _isHidden;

    public Word(string text)
    {
        _text = text;
        _isHidden = false;
    }

    public bool IsHidden()
    {
        // Scripture uses this to find words that can still be hidden.
        return _isHidden;
    }

    public void Hide()
    {
        // Once hidden, a word stays hidden for the rest of the practice session.
        _isHidden = true;
    }

    public string GetDisplayText()
    {
        // Show the original word while it is visible.
        if (!_isHidden)
        {
            return _text;
        }

        // Replace each letter or number with an underscore, but retain punctuation.
        // For example, "heart;" becomes "_____;".
        char[] hiddenText = _text.ToCharArray();
        for (int index = 0; index < hiddenText.Length; index++)
        {
            if (char.IsLetterOrDigit(hiddenText[index]))
            {
                hiddenText[index] = '_';
            }
        }

        return new string(hiddenText);
    }
}
