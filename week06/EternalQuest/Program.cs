using System;
using System.IO;

public class Program
{
    public static void Main(string[] args)
    {
        GoalManager manager = new GoalManager();
        bool running = true;

        while (running)
        {
            Console.WriteLine();
            Console.WriteLine($"Eternal Quest - {ScoreSummary(manager)}");
            Console.WriteLine("1. Create a new goal");
            Console.WriteLine("2. Record goal progress");
            Console.WriteLine("3. Show goals");
            Console.WriteLine("4. Save quest");
            Console.WriteLine("5. Load quest");
            Console.WriteLine("6. Quit");
            Console.Write("Choose an option: ");

            switch (Console.ReadLine())
            {
                case "1":
                    CreateGoal(manager);
                    break;
                case "2":
                    RecordGoal(manager);
                    break;
                case "3":
                    ShowGoals(manager);
                    break;
                case "4":
                    Save(manager);
                    break;
                case "5":
                    manager = Load(manager);
                    break;
                case "6":
                    running = false;
                    break;
                default:
                    Console.WriteLine("Please select a number from 1 to 6.");
                    break;
            }
        }

        Console.WriteLine("Keep going on your Eternal Quest!");
    }

    private static string ScoreSummary(GoalManager manager)
    {
        return $"Score: {manager.Score} | Level {manager.Level}";
    }

    private static void CreateGoal(GoalManager manager)
    {
        Console.WriteLine("Goal type: 1. Simple  2. Eternal  3. Checklist");
        string type = ReadRequired("Choose a type: ");
        if (type != "1" && type != "2" && type != "3")
        {
            Console.WriteLine("That goal type is not available.");
            return;
        }

        string name = ReadRequired("Goal name: ");
        string description = ReadRequired("Short description: ");
        int points = ReadPositiveInt("Points for each completion: ");

        if (type == "1")
        {
            manager.AddGoal(new SimpleGoal(name, description, points));
        }
        else if (type == "2")
        {
            manager.AddGoal(new EternalGoal(name, description, points));
        }
        else
        {
            int targetCount = ReadPositiveInt("Number of completions required: ");
            int bonus = ReadNonNegativeInt("Bonus points on completion: ");
            manager.AddGoal(new ChecklistGoal(name, description, points, targetCount, bonus));
        }

        Console.WriteLine("Goal created.");
    }

    private static void RecordGoal(GoalManager manager)
    {
        if (manager.Goals.Count == 0)
        {
            Console.WriteLine("Create a goal before recording progress.");
            return;
        }

        ShowGoals(manager);
        int selection = ReadIntInRange("Which goal did you complete? ", 1, manager.Goals.Count);
        int oldLevel = manager.Level;
        int points = manager.RecordEvent(selection - 1);
        if (points == 0)
        {
            Console.WriteLine("That goal is already complete, so no points were awarded.");
            return;
        }

        Console.WriteLine($"Nice work! You earned {points} points.");
        if (manager.Level > oldLevel)
        {
            Console.WriteLine($"Level up! You reached level {manager.Level}.");
        }
    }

    private static void ShowGoals(GoalManager manager)
    {
        Console.WriteLine($"Current score: {manager.Score} points - Level {manager.Level}");
        if (manager.Goals.Count == 0)
        {
            Console.WriteLine("No goals yet.");
            return;
        }

        for (int index = 0; index < manager.Goals.Count; index++)
        {
            Console.WriteLine($"{index + 1}. {manager.Goals[index].GetStatus()}");
        }
    }

    private static void Save(GoalManager manager)
    {
        string fileName = ReadRequired("Save file name (for example quest.json): ");
        try
        {
            manager.Save(fileName);
            Console.WriteLine($"Quest saved to {fileName}.");
        }
        catch (IOException exception)
        {
            Console.WriteLine($"Could not save the quest: {exception.Message}");
        }
        catch (UnauthorizedAccessException exception)
        {
            Console.WriteLine($"Could not save the quest: {exception.Message}");
        }
    }

    private static GoalManager Load(GoalManager currentManager)
    {
        string fileName = ReadRequired("Save file name to load: ");
        try
        {
            GoalManager manager = GoalManager.Load(fileName);
            Console.WriteLine("Quest loaded.");
            return manager;
        }
        catch (Exception exception) when (exception is IOException || exception is UnauthorizedAccessException || exception is System.Text.Json.JsonException || exception is InvalidDataException)
        {
            Console.WriteLine($"Could not load the quest: {exception.Message}");
            return currentManager;
        }
    }

    private static string ReadRequired(string prompt)
    {
        while (true)
        {
            Console.Write(prompt);
            string value = Console.ReadLine();
            if (!string.IsNullOrWhiteSpace(value))
            {
                return value.Trim();
            }

            Console.WriteLine("Please enter a value.");
        }
    }

    private static int ReadPositiveInt(string prompt)
    {
        while (true)
        {
            int value = ReadInt(prompt);
            if (value > 0)
            {
                return value;
            }

            Console.WriteLine("Enter a number greater than zero.");
        }
    }

    private static int ReadNonNegativeInt(string prompt)
    {
        while (true)
        {
            int value = ReadInt(prompt);
            if (value >= 0)
            {
                return value;
            }

            Console.WriteLine("Enter zero or a positive number.");
        }
    }

    private static int ReadInt(string prompt)
    {
        while (true)
        {
            Console.Write(prompt);
            if (int.TryParse(Console.ReadLine(), out int value))
            {
                return value;
            }

            Console.WriteLine("Please enter a whole number.");
        }
    }

    private static int ReadIntInRange(string prompt, int minimum, int maximum)
    {
        while (true)
        {
            int value = ReadInt(prompt);
            if (value >= minimum && value <= maximum)
            {
                return value;
            }

            Console.WriteLine($"Choose a goal from {minimum} to {maximum}.");
        }
    }

    // Creativity: the quest has persistent levels, gaining one level for every 500 points. Level-up messages celebrate milestones as the user records progress.
}
