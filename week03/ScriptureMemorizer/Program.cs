using System;
using System.Collections.Generic;

public class Program
{
    // Exceeding core requirements:
    // This program contains a library of three scriptures and randomly chooses one
    // at the beginning of each practice session. The core requirement is to store
    // one scripture, so the library gives the user more varied practice.
    //
    // It also chooses only words that are still visible when hiding words. This
    // means every Enter key press hides new words and the user never loses a turn
    // because a previously hidden word was selected.
    public static void Main(string[] args)
    {
        // Choose a scripture, then continue until the user quits or all words hide.
        Scripture scripture = GetRandomScripture();
        string response = "";

        while (response.ToLower() != "quit" && !scripture.IsCompletelyHidden())
        {
            // Redraw the whole scripture after every response.
            ClearConsole();
            Console.WriteLine(scripture.GetDisplayText());
            Console.WriteLine();
            Console.Write("Press Enter to hide words or type 'quit' to finish: ");
            response = Console.ReadLine() ?? "quit";

            if (response.ToLower() != "quit")
            {
                // Enter (or any response other than quit) hides three more words.
                scripture.HideRandomWords(3);
            }
        }

        // Show the final state, including the fully hidden scripture when completed.
        ClearConsole();
        Console.WriteLine(scripture.GetDisplayText());
        Console.WriteLine();
        Console.WriteLine(scripture.IsCompletelyHidden() ? "All words are hidden. Great work!" : "Goodbye!");
    }

    private static Scripture GetRandomScripture()
    {
        // This is the scripture library used by the creativity feature.
        List<Scripture> scriptures = new List<Scripture>
        {
            new Scripture(
                new Reference("Proverbs", 3, 5, 6),
                "Trust in the Lord with all thine heart; and lean not unto thine own understanding. In all thy ways acknowledge him, and he shall direct thy paths."),
            new Scripture(
                new Reference("John", 3, 16),
                "For God so loved the world, that he gave his only begotten Son, that whosoever believeth in him should not perish, but have everlasting life."),
            new Scripture(
                new Reference("Philippians", 4, 13),
                "I can do all things through Christ which strengtheneth me.")
        };

        Random random = new Random();
        return scriptures[random.Next(scriptures.Count)];
    }

    private static void ClearConsole()
    {
        try
        {
            // Clearing creates the visual effect of words disappearing in place.
            Console.Clear();
        }
        catch (System.IO.IOException)
        {
            // Console.Clear is unavailable when output is redirected (for example, during testing).
        }
    }
}
