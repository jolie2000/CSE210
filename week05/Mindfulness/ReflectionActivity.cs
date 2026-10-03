using System;
using System.Collections.Generic;
using System.Diagnostics;

public class ReflectionActivity : Activity
{
    private static readonly string[] _promptOptions =
    {
        "Think of a time when you stood up for someone else.",
        "Think of a time when you did something really difficult.",
        "Think of a time when you helped someone in need.",
        "Think of a time when you did something truly selfless."
    };
    private static readonly Queue<string> _prompts = CreateRandomCycle(_promptOptions);
    private readonly Queue<string> _questions;

    public ReflectionActivity()
        : base("Reflection Activity", "This activity will help you reflect on times in your life when you have shown strength and resilience. This will help you recognize the power you have and how you can use it in other aspects of your life.")
    {
        _questions = CreateRandomCycle(new[]
        {
            "Why was this experience meaningful to you?",
            "Have you ever done anything like this before?",
            "How did you get started?",
            "How did you feel when it was complete?",
            "What made this time different than other times when you were not as successful?",
            "What is your favorite thing about this experience?",
            "What could you learn from this experience that applies to other situations?",
            "What did you learn about yourself through this experience?",
            "How can you keep this experience in mind in the future?"
        });
    }

    protected override void RunActivity()
    {
        Console.WriteLine("Consider the following prompt:");
        Console.WriteLine($"--- {GetNextPrompt()} ---");
        ShowSpinner(5);

        Stopwatch activityTimer = Stopwatch.StartNew();
        while (activityTimer.Elapsed.TotalSeconds < DurationSeconds)
        {
            if (_questions.Count == 0)
            {
                RefillQuestions();
            }

            Console.WriteLine();
            Console.WriteLine($"> {_questions.Dequeue()}");
            ShowSpinner(Math.Min(5, Math.Max(1, DurationSeconds - (int)activityTimer.Elapsed.TotalSeconds)));
        }
    }

    private void RefillQuestions()
    {
        _questions.Enqueue("Why was this experience meaningful to you?");
        _questions.Enqueue("Have you ever done anything like this before?");
        _questions.Enqueue("How did you get started?");
        _questions.Enqueue("How did you feel when it was complete?");
        _questions.Enqueue("What made this time different than other times when you were not as successful?");
        _questions.Enqueue("What is your favorite thing about this experience?");
        _questions.Enqueue("What could you learn from this experience that applies to other situations?");
        _questions.Enqueue("What did you learn about yourself through this experience?");
        _questions.Enqueue("How can you keep this experience in mind in the future?");
        Queue<string> shuffledQuestions = CreateRandomCycle(_questions);
        _questions.Clear();
        while (shuffledQuestions.Count > 0)
        {
            _questions.Enqueue(shuffledQuestions.Dequeue());
        }
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
