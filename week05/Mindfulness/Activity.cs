using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading;

public abstract class Activity
{
    private readonly string _activityName;
    private readonly string _description;
    private int _durationSeconds;

    protected Activity(string activityName, string description)
    {
        _activityName = activityName;
        _description = description;
    }

    protected string ActivityName => _activityName;
    protected int DurationSeconds => _durationSeconds;

    public void Run()
    {
        StartActivity();
        RunActivity();
        EndActivity();
    }

    protected abstract void RunActivity();

    private void StartActivity()
    {
        Console.Clear();
        Console.WriteLine($"Welcome to the {_activityName}.");
        Console.WriteLine();
        Console.WriteLine(_description);
        Console.WriteLine();
        _durationSeconds = ReadDuration();
        Console.WriteLine();
        Console.WriteLine("Get ready...");
        ShowSpinner(3);
    }

    private void EndActivity()
    {
        Console.WriteLine();
        Console.WriteLine("Well done!");
        ShowSpinner(2);
        Console.WriteLine();
        Console.WriteLine($"You have completed the {_activityName} for {_durationSeconds} seconds.");
        ShowSpinner(3);
    }

    private static int ReadDuration()
    {
        while (true)
        {
            Console.Write("How long, in seconds, would you like for your session? ");
            string input = Console.ReadLine();
            if (int.TryParse(input, out int seconds) && seconds > 0)
            {
                return seconds;
            }

            Console.WriteLine("Please enter a whole number greater than zero.");
        }
    }

    protected void ShowSpinner(int seconds)
    {
        char[] frames = { '|', '/', '-', '\\' };
        Stopwatch timer = Stopwatch.StartNew();
        int frame = 0;
        while (timer.Elapsed.TotalSeconds < seconds)
        {
            Console.Write(frames[frame]);
            Thread.Sleep(250);
            Console.Write('\b');
            frame = (frame + 1) % frames.Length;
        }
    }

    protected void ShowCountdown(int seconds)
    {
        for (int remaining = seconds; remaining > 0; remaining--)
        {
            Console.Write(remaining);
            Thread.Sleep(1000);
            Console.Write("\b \b");
        }
    }

    protected void ShowTimedCountdown(int seconds, Stopwatch activityTimer)
    {
        for (int remaining = seconds; remaining > 0 && activityTimer.Elapsed.TotalSeconds < _durationSeconds; remaining--)
        {
            Console.Write(remaining);
            int waitMilliseconds = Math.Min(1000, Math.Max(0, (_durationSeconds * 1000) - (int)activityTimer.ElapsedMilliseconds));
            Thread.Sleep(waitMilliseconds);
            Console.Write("\b \b");
        }
    }

    protected string ReadLineUntil(Stopwatch activityTimer)
    {
        string result = "";
        while (activityTimer.Elapsed.TotalSeconds < _durationSeconds)
        {
            if (!Console.KeyAvailable)
            {
                Thread.Sleep(50);
                continue;
            }

            ConsoleKeyInfo key = Console.ReadKey(intercept: true);
            if (key.Key == ConsoleKey.Enter)
            {
                Console.WriteLine();
                return result;
            }

            if (key.Key == ConsoleKey.Backspace)
            {
                if (result.Length > 0)
                {
                    result = result.Substring(0, result.Length - 1);
                    Console.Write("\b \b");
                }
            }
            else if (!char.IsControl(key.KeyChar))
            {
                result += key.KeyChar;
                Console.Write(key.KeyChar);
            }
        }

        Console.WriteLine();
        return result;
    }

    protected static Queue<T> CreateRandomCycle<T>(IEnumerable<T> items)
    {
        List<T> shuffledItems = items.ToList();
        for (int index = shuffledItems.Count - 1; index > 0; index--)
        {
            int swapIndex = Random.Shared.Next(index + 1);
            (shuffledItems[index], shuffledItems[swapIndex]) = (shuffledItems[swapIndex], shuffledItems[index]);
        }

        return new Queue<T>(shuffledItems);
    }
}
