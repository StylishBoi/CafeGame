public static class ScoreSystem
{
    //All time stats
    public static int AllMoneyEver = 0;
    public static int AllCustomersEver = 0;
    public static int HighestStreakEver = 0;
    
    //Persists through the days
    public static int TotalMoneyScore = 0;
    public static int DayCount = 1;
    
    //Resets everyday
    public static int CustomersServed = 0;
    public static int MoneyScore = 0;
    public static int LastScore = 0;

    public static void StartDay()
    {
        CustomersServed = 0;
        MoneyScore = 0;
        LastScore = 0;
        EffectManager.DayReset();
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

    public static void EndDay()
    {
        //Add up previous stats
        if (HighestStreakEver < StreakManager.HighestStreak)
        {
            HighestStreakEver=StreakManager.HighestStreak;
        }
        AllMoneyEver+=MoneyScore;
        AllCustomersEver+=CustomersServed;
        DayCount++;
        
        //Resets stats
        CustomersServed = 0;
        MoneyScore = 0;
        LastScore = 0;
    }

    public static void LoadData()
    {
        SavedStats savedStats = SaveSystem.LoadStats();
        TotalMoneyScore = savedStats.totalMoneyScore;
        DayCount = savedStats.dayCount;
    }
}