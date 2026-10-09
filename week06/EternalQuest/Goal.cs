using System;

public abstract class Goal
{
    private readonly string _name;
    private readonly string _description;
    private readonly int _points;

    protected Goal(string name, string description, int points)
    {
        _name = name;
        _description = description;
        _points = points;
    }

    public string Name => _name;
    public string Description => _description;
    public int Points => _points;

    public abstract bool IsComplete { get; }
    public abstract int RecordEvent();
    public abstract string GetStatus();
    public abstract GoalSaveData ToSaveData();

    protected string GetDisplayPrefix()
    {
        return $"{(IsComplete ? "[X]" : "[ ]")} {_name} ({_description})";
    }
}
