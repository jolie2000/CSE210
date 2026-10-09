public class ChecklistGoal : Goal
{
    private readonly int _targetCount;
    private readonly int _completionBonus;
    private int _timesRecorded;

    public ChecklistGoal(string name, string description, int points, int targetCount, int completionBonus, int timesRecorded = 0)
        : base(name, description, points)
    {
        _targetCount = targetCount;
        _completionBonus = completionBonus;
        _timesRecorded = timesRecorded;
    }

    public override bool IsComplete => _timesRecorded >= _targetCount;

    public override int RecordEvent()
    {
        if (IsComplete)
        {
            return 0;
        }

        _timesRecorded++;
        return Points + (IsComplete ? _completionBonus : 0);
    }

    public override string GetStatus()
    {
        return $"{GetDisplayPrefix()} — Completed {_timesRecorded}/{_targetCount} times; {Points} points each, {_completionBonus} point bonus";
    }

    public override GoalSaveData ToSaveData()
    {
        return new GoalSaveData("checklist", Name, Description, Points, IsComplete, _timesRecorded, _targetCount, _completionBonus);
    }
}
