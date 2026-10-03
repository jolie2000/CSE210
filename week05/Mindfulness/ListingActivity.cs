using System;
using System.Collections.Generic;
using System.Diagnostics;

public class ListingActivity : Activity
{
    private static readonly string[] _promptOptions =
    {
        "Who are people that you appreciate?",
        "What are personal strengths of yours?",
        "Who are people that you have helped this week?",
        "When have you felt grateful this month?",
        "Who are some of your personal heroes?"
    };
    private static readonly Queue<string> _prompts = CreateRandomCycle(_promptOptions);

    public ListingActivity()
        : base("Listing Activity", "This activity will help you reflect on the good things in your life by having you list as many things as you can in a certain area.")
    {
    }

    protected override void RunActivity()
    {
        Console.WriteLine("List as many responses as you can to the following prompt:");
        Console.WriteLine($"--- {GetNextPrompt()} ---");
        Console.Write("You may begin in: ");
        ShowCountdown(5);
        Console.WriteLine();
        Console.WriteLine("Start listing items below. Press Enter after each item.");

        List<string> responses = new List<string>();
        Stopwatch activityTimer = Stopwatch.StartNew();
        while (activityTimer.Elapsed.TotalSeconds < DurationSeconds)
        {
            string response = ReadLineUntil(activityTimer);
            if (!string.IsNullOrWhiteSpace(response))
            {
                responses.Add(response.Trim());
            }
        }

        Console.WriteLine($"You listed {responses.Count} item(s).");
    }

    private static string GetNextPrompt()
    {
        if (_prompts.Count == 0)
        {
            Queue<string> nextCycle = CreateRandomCycle(_promptOptions);
            while (nextCycle.Count > 0)
            {
                _prompts.Enqueue(nextCycle.Dequeue());
            }
        }

        return _prompts.Dequeue();
    }
}
