using TMPro;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.SocialPlatforms.Impl;

public class DayEndUIScreen : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI dayText;
    [SerializeField] private TextMeshProUGUI customerText;
    [SerializeField] private TextMeshProUGUI moneyText;
    [SerializeField] private TextMeshProUGUI streakText;
    [SerializeField] private TextMeshProUGUI bankText;

    private void OnEnable()
    {
        dayText.text = "Day " + ScoreSystem.DayCount + " Result";
        customerText.text = ScoreSystem.CustomersServed.ToString();
        moneyText.text=ScoreSystem.MoneyScore.ToString();
        streakText.text=StreakManager.HighestStreak.ToString();
        bankText.text = ScoreSystem.TotalMoneyScore.ToString("0000000");
    }
}
