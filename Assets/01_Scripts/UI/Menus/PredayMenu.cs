using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.UI;

public class PredayMenu : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI dayText;
    [SerializeField] private Button saveButton;
    [SerializeField] private GameObject statsCanvas;
    [SerializeField] private TextMeshProUGUI bankText;

    void OnEnable()
    {
        dayText.text = "Day " + ScoreSystem.DayCount;
        bankText.text = ScoreSystem.TotalMoneyScore.ToString("0000000");
        if (ScoreSystem.DayCount == 1)
        {
            saveButton.interactable = false;
        }
    }

    public void StartDay()
    {
        GameManager.Instance.StartGame();
        ScoreSystem.StartDay();
        SceneManager.LoadScene("CafePrototype");
    }

    public void StatsMenu()
    {
        gameObject.SetActive(false);
        statsCanvas.SetActive(true);
    }
    public void SaveGame()
    {
        SaveSystem.SaveGame();
    }

    public void MainMenu()
    {
        //StartCoroutine(MainMenuFade());
        SceneManager.LoadScene("MainMenu");
    }
}