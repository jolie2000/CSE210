using System;
using System.IO;

public class Program
{
    // Exceeds core requirements:
    // - Uses RFC 4180-style CSV escaping so commas and quotation marks open cleanly in Excel.
    // - Tracks and saves each response's word count.
    // - Prevents empty journal responses from being saved.
    public static void Main(string[] args)
    {
        Journal journal = new Journal();
        PromptGenerator promptGenerator = new PromptGenerator();
        string userChoice = "";

        while (userChoice != "5")
        {
            Console.WriteLine("Please select one of the following choices:");
            Console.WriteLine("1. Write");
            Console.WriteLine("2. Display");
            Console.WriteLine("3. Load");
            Console.WriteLine("4. Save");
            Console.WriteLine("5. Quit");
            Console.Write("What would you like to do? ");
            userChoice = Console.ReadLine() ?? "";
            Console.WriteLine();

            switch (userChoice)
            {
                case "1":
                    WriteEntry(journal, promptGenerator);
                    break;
                case "2":
                    journal.DisplayAll();
                    break;
                case "3":
                    LoadJournal(journal);
                    break;
                case "4":
                    SaveJournal(journal);
                    break;
                case "5":
                    Console.WriteLine("Goodbye!");
                    break;
                default:
                    Console.WriteLine("Please enter a number from 1 to 5.\n");
                    break;
            }
        }
    }

    private static void WriteEntry(Journal journal, PromptGenerator promptGenerator)
    {
        string prompt = promptGenerator.GetRandomPrompt();
        Console.WriteLine($"Prompt: {prompt}");
        Console.Write("> ");
        string response = Console.ReadLine() ?? "";

        if (string.IsNullOrWhiteSpace(response))
        {
            Console.WriteLine("An empty response was not saved. Please write something for your journal.\n");
            return;
        }

        Entry newEntry = new Entry
        {
            _date = DateTime.Now.ToShortDateString(),
            _promptText = prompt,
            _entryText = response,
            _wordCount = CountWords(response)
        };

        journal.AddEntry(newEntry);
        Console.WriteLine($"Entry saved. Word count: {newEntry._wordCount}\n");
    }

    private static void LoadJournal(Journal journal)
    {
        Console.Write("What is the filename? ");
        string fileName = Console.ReadLine() ?? "";

        try
        {
            journal.LoadFromFile(fileName);
            Console.WriteLine("Journal loaded successfully.\n");
        }
        catch (Exception exception) when (exception is IOException || exception is UnauthorizedAccessException || exception is FormatException)
        {
            Console.WriteLine($"Unable to load the journal: {exception.Message}\n");
        }
    }

    private static void SaveJournal(Journal journal)
    {
        Console.Write("What is the filename? ");
        string fileName = Console.ReadLine() ?? "";

        try
        {
            journal.SaveToFile(fileName);
            Console.WriteLine("Journal saved successfully.\n");
        }
        catch (Exception exception) when (exception is IOException || exception is UnauthorizedAccessException)
        {
            Console.WriteLine($"Unable to save the journal: {exception.Message}\n");
        }
    }

    private static int CountWords(string text)
    {
        return text.Split((char[])null, StringSplitOptions.RemoveEmptyEntries).Length;
    }
}
