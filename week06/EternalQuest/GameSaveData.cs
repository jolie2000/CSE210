using System.Collections.Generic;

public class GameSaveData
{
    public int Score { get; set; }
    public List<GoalSaveData> Goals { get; set; }

    public GameSaveData()
    {
        Goals = new List<GoalSaveData>();
    }
}
