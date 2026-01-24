using UnityEngine;
using UnityEngine.Serialization;

[System.Serializable]
public class SavedStats
{
    public int totalMoneyScore = 0;
    public int dayCount = 0;

    public SavedStats()
    {
        totalMoneyScore = ScoreSystem.TotalMoneyScore;
        dayCount = ScoreSystem.DayCount;
    }
}
