using System.Collections;
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
    [SerializeField] private GameObject savePanel;
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
        AudioManager.Instance.ChangeBGM(AudioManager.Instance.cafeMusic);
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
        AudioManager.Instance.PlaySfx(AudioManager.Instance.savedGameSFX);
        StartCoroutine(PauseEffect());
    }

    public void MainMenu()
    {
        //StartCoroutine(MainMenuFade());
        SceneManager.LoadScene("MainMenu");
    }
    
    private IEnumerator PauseEffect()
    {
        float t = 0f;
        
        savePanel.SetActive(true);
        saveButton.interactable = false;
        
        while (t < 2f)
        {
            t += Time.deltaTime / 1;
            yield return null;
        }
        
        savePanel.SetActive(false);
        saveButton.interactable = true;
    }
}