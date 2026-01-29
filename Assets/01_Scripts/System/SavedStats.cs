using UnityEngine;
using UnityEngine.Serialization;

[System.Serializable]
public class SavedStats
{
    public int allMoneyEver = 0;
    public int allCustomersEver = 0;
    public int highestStreakEver = 0;
    
    public int totalMoneyScore = 0;
    public int dayCount = 0;

    public SavedStats()
    {
        allMoneyEver = ScoreSystem.AllMoneyEver;
        allCustomersEver = ScoreSystem.AllCustomersEver;
        highestStreakEver = ScoreSystem.HighestStreakEver;
        
        totalMoneyScore = ScoreSystem.TotalMoneyScore;
        dayCount = ScoreSystem.DayCount;
    }
}
