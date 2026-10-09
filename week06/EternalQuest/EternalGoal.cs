public class EternalGoal : Goal
{
    private int _timesRecorded;

    public EternalGoal(string name, string description, int points, int timesRecorded = 0)
        : base(name, description, points)
    {
        _timesRecorded = timesRecorded;
    }

    public override bool IsComplete => false;

    public override int RecordEvent()
    {
        _timesRecorded++;
        return Points;
    }

    public override string GetStatus()
    {
        return $"{GetDisplayPrefix()} — {Points} points each time; recorded {_timesRecorded} time(s)";
    }

    public override GoalSaveData ToSaveData()
    {
        return new GoalSaveData("eternal", Name, Description, Points, false, _timesRecorded, 0, 0);
    }
}
