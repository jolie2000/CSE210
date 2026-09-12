using System;

public class Entry
{
    public string _date;
    public string _promptText;
    public string _entryText;
    public int _wordCount;

    public void Display()
    {
        Console.WriteLine($"Date: {_date}");
        Console.WriteLine($"Prompt: {_promptText}");
        Console.WriteLine($"Response: {_entryText}");
        Console.WriteLine($"Word count: {_wordCount}");
        Console.WriteLine();
    }
}
