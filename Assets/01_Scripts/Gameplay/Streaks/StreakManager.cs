using UnityEngine;

public static class StreakManager
{
    public static int Streak;
    public static int HighestStreak;
    public static int NegativeStreak;
    
    public static void StreakIncrease()
    {
        NegativeStreak = 0;
        Streak++;
        CafeUIManager.Instance.StreakUpdate();
        UpdateHighestStreak();
    }
    
    public static void NegativeStreakIncrease()
    {
        Streak = 0;
        NegativeStreak++;
        CafeUIManager.Instance.StreakUpdate();
    }

    private static void UpdateHighestStreak()
    {
        if (HighestStreak < Streak)
        {
            HighestStreak = Streak;
        }
    }
}
