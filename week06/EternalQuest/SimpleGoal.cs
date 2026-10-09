public class SimpleGoal : Goal
{
    private bool _isComplete;

    public SimpleGoal(string name, string description, int points, bool isComplete = false)
        : base(name, description, points)
    {
        _isComplete = isComplete;
    }

    public override bool IsComplete => _isComplete;

    public override int RecordEvent()
    {
        if (_isComplete)
        {
            return 0;
        }

        _isComplete = true;
        return Points;
    }

    public override string GetStatus()
    {
        return $"{GetDisplayPrefix()} — {Points} points";
    }

    public override GoalSaveData ToSaveData()
    {
        return new GoalSaveData("simple", Name, Description, Points, _isComplete, 0, 0, 0);
    }
}
