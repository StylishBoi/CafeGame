using TMPro;
using UnityEngine;

public class DayEndUIScreen : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI customerText;
    [SerializeField] private TextMeshProUGUI moneyText;
    [SerializeField] private TextMeshProUGUI streakText;
    [SerializeField] private TextMeshProUGUI totalScoreText;

    private void OnEnable()
    {
        customerText.text = ScoreSystem.CustomersServed.ToString();
        moneyText.text=ScoreSystem.MoneyScore.ToString();
        streakText.text=StreakManager.HighestStreak.ToString();
        totalScoreText.text=ScoreSystem.TotalMoneyScore.ToString();
    }
}
