public static class ScoreSystem
{
    public static int TotalMoneyScore = 0;
    
    public static int CustomersServed = 0;
    public static int MoneyScore = 0;
    public static int LastScore = 0;

    public static void StartDay()
    {
        CustomersServed = 0;
        MoneyScore = 0;
        LastScore = 0;
        StreakManager.HighestStreak = 0;
        StreakManager.Streak = 0;
    }
    public static void IncreaseScore(int amount)
    {
        if (EffectManager.PositiveEffect)
        {
            MoneyScore += amount + 2;
            LastScore = amount + 2;
        }
        else if (EffectManager.NegativeEffect)
        {
            MoneyScore += amount - 2;
            LastScore = amount - 2;
        }
        else
        {
            MoneyScore += amount;
            LastScore = amount;
        }
        
        TotalMoneyScore += LastScore;
        CustomersServed++;
    }
}