using System;
using System.Collections.Generic;

// Represents a complete scripture: its reference and the individual words in its text.
public class Scripture
{
    // The scripture owns its words and handles all scripture-specific behavior.
    private Reference _reference;
    private List<Word> _words;
    private Random _random;

    public Scripture(Reference reference, string text)
    {
        _reference = reference;
        _words = new List<Word>();
        _random = new Random();

        // Split the supplied scripture text into Word objects.
        string[] textWords = text.Split(' ', StringSplitOptions.RemoveEmptyEntries);
        foreach (string textWord in textWords)
        {
            _words.Add(new Word(textWord));
        }
    }

    public void HideRandomWords(int numberToHide)
    {
        // Build a list of only visible words so each selection makes progress.
        List<Word> visibleWords = new List<Word>();
        foreach (Word word in _words)
        {
            if (!word.IsHidden())
            {
                visibleWords.Add(word);
            }
        }

        // Do not attempt to hide more words than are still visible.
        int wordsToHide = Math.Min(numberToHide, visibleWords.Count);
        for (int count = 0; count < wordsToHide; count++)
        {
            // Remove the chosen word from this temporary list to avoid selecting it twice.
            int randomIndex = _random.Next(visibleWords.Count);
            visibleWords[randomIndex].Hide();
            visibleWords.RemoveAt(randomIndex);
        }
    }

    public bool IsCompletelyHidden()
    {
        // The program ends only after every Word reports that it is hidden.
        foreach (Word word in _words)
        {
            if (!word.IsHidden())
            {
                return false;
            }
        }

        return true;
    }

    public string GetDisplayText()
    {
        // Ask each Word for its current display form, then join the results.
        List<string> displayWords = new List<string>();
        foreach (Word word in _words)
        {
            displayWords.Add(word.GetDisplayText());
        }

        return $"{_reference}\n{string.Join(" ", displayWords)}";
    }
}
