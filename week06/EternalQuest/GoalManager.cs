using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;

public class GoalManager
{
    private readonly List<Goal> _goals;
    private int _score;

    public GoalManager()
    {
        _goals = new List<Goal>();
        _score = 0;
    }

    private GoalManager(int score, List<Goal> goals)
    {
        _score = score;
        _goals = goals;
    }

    public int Score => _score;
    public IReadOnlyList<Goal> Goals => _goals.AsReadOnly();
    public int Level => (_score / 500) + 1;

    public void AddGoal(Goal goal)
    {
        _goals.Add(goal);
    }

    public int RecordEvent(int goalIndex)
    {
        int earnedPoints = _goals[goalIndex].RecordEvent();
        _score += earnedPoints;
        return earnedPoints;
    }

    public void Save(string fileName)
    {
        GameSaveData saveData = new GameSaveData { Score = _score };
        foreach (Goal goal in _goals)
        {
            saveData.Goals.Add(goal.ToSaveData());
        }

        string json = JsonSerializer.Serialize(saveData, new JsonSerializerOptions { WriteIndented = true });
        File.WriteAllText(fileName, json);
    }

    public static GoalManager Load(string fileName)
    {
        string json = File.ReadAllText(fileName);
        GameSaveData saveData = JsonSerializer.Deserialize<GameSaveData>(json);
        if (saveData == null || saveData.Goals == null)
        {
            throw new InvalidDataException("The save file does not contain valid quest data.");
        }

        List<Goal> goals = new List<Goal>();
        foreach (GoalSaveData data in saveData.Goals)
        {
            switch (data.Type)
            {
                case "simple":
                    goals.Add(new SimpleGoal(data.Name, data.Description, data.Points, data.IsComplete));
                    break;
                case "eternal":
                    goals.Add(new EternalGoal(data.Name, data.Description, data.Points, data.TimesRecorded));
                    break;
                case "checklist":
                    goals.Add(new ChecklistGoal(data.Name, data.Description, data.Points, data.TargetCount, data.CompletionBonus, data.TimesRecorded));
                    break;
                default:
                    throw new InvalidDataException($"Unknown goal type '{data.Type}'.");
            }
        }

        return new GoalManager(saveData.Score, goals);
    }
}
