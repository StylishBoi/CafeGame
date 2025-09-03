using UnityEngine;

public static class StreakManager
{
    public static int Streak;
    public static int NegativeStreak;
    
    public static void StreakIncrease()
    {
        NegativeStreak = 0;
        Streak++;
        UIManager.Instance.StreakUpdate();
    }
    
    public static void NegativeStreakIncrease()
    {
        Streak = 0;
        NegativeStreak++;
        UIManager.Instance.StreakUpdate();
    }
}
