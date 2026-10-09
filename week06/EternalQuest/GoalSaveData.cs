public class GoalSaveData
{
    public string Type { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public int Points { get; set; }
    public bool IsComplete { get; set; }
    public int TimesRecorded { get; set; }
    public int TargetCount { get; set; }
    public int CompletionBonus { get; set; }

    public GoalSaveData()
    {
    }

    public GoalSaveData(string type, string name, string description, int points, bool isComplete, int timesRecorded, int targetCount, int completionBonus)
    {
        Type = type;
        Name = name;
        Description = description;
        Points = points;
        IsComplete = isComplete;
        TimesRecorded = timesRecorded;
        TargetCount = targetCount;
        CompletionBonus = completionBonus;
    }
}
