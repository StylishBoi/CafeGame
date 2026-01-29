using UnityEngine;
using TMPro;
using UnityEngine.SocialPlatforms.Impl;

public class StatsMenu : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI dayText;
    [SerializeField] private TextMeshProUGUI allTimeCostumerText;
    [SerializeField] private TextMeshProUGUI allMoneyText;
    [SerializeField] private TextMeshProUGUI highestStreakText;
    [SerializeField] private GameObject predayCanvas;

    private void OnEnable()
    {
        int count = ScoreSystem.DayCount - 1;
        dayText.text = "Days completed " + count;
        allTimeCostumerText.text = ScoreSystem.AllCustomersEver.ToString();
        allMoneyText.text=ScoreSystem.AllMoneyEver.ToString();
        highestStreakText.text=StreakManager.HighestStreak.ToString();
    }
    
    public void PredayMenu()
    {
        gameObject.SetActive(false);
        predayCanvas.SetActive(true);
    }
}
