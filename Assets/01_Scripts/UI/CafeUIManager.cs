using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class CafeUIManager : MonoBehaviour
{
    [Header("UI Images")] [SerializeField] private Image resultFlash;
    [SerializeField] private Image bottomFlash;
    [SerializeField] private Image fullScreenFade;
    [SerializeField] private Image endHourScreen;

    [Header("Fade Color")] [SerializeField]
    private Color badResultColor;

    [SerializeField] private Color mediocreResultColor;
    [SerializeField] private Color goodResultColor;
    [SerializeField] private Color eventFlashColor;

    [Header("Other UI Elements")]
    [SerializeField] private GameObject rushHourReminder;
    [SerializeField] private GameObject rushHourScreen;
    [SerializeField] private GameObject endResultScreen;
    [SerializeField] private GameObject rushOverScreen;
    [SerializeField] private GameObject inventoryScreen;
    [SerializeField] private TextMeshProUGUI streakText;

    private GameObject UI;
    private UIFadeEffects _uiFadeEffects;


    public static CafeUIManager Instance;

    void Awake()
    {
        _uiFadeEffects = GetComponent<UIFadeEffects>();

        UI = transform.GetChild(0).gameObject;

        if (Instance == null)
        {
            Instance = this;
            // DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        StartCoroutine(StartOfDayFade());
    }

    public void HideUI()
    {
        UI.SetActive(false);
    }

    public void HideInventory()
    {
        inventoryScreen.SetActive(false);
    }

    public void ShowUI()
    {
        UI.SetActive(true);
    }

    public void Success()
    {
        StartCoroutine(_uiFadeEffects.ImageFlashEvent(resultFlash, goodResultColor));
    }

    public void Mediocre()
    {
        StartCoroutine(_uiFadeEffects.ImageFlashEvent(resultFlash, mediocreResultColor));
    }
    public void Failure()
    {
        StartCoroutine(_uiFadeEffects.ImageFlashEvent(resultFlash, badResultColor));
    }

    public void RushHour()
    {
        rushHourReminder.SetActive(true);
        StartCoroutine(_uiFadeEffects.ImageFlashEvent(resultFlash, eventFlashColor, rushHourScreen));
    }

    public void RushOver()
    {
        rushHourReminder.SetActive(false);
        StartCoroutine(_uiFadeEffects.ImageFlashEvent(resultFlash, eventFlashColor, rushOverScreen));
    }

    public IEnumerator StartOfDayFade()
    {
        fullScreenFade.gameObject.SetActive(false);
        yield return StartCoroutine(_uiFadeEffects.DoFadeIn(fullScreenFade, Color.white));
        GameManager.Instance.StartDay();
    }

    public void EndOfDayFade()
    {
        StartCoroutine(EndOfDayFadeEnumerator());
    }

    private IEnumerator EndOfDayFadeEnumerator()
    {
        GameManager.Instance.EndFullDay();
        endResultScreen.SetActive(false);
        AudioManager.Instance.ChangeBGM(AudioManager.Instance.mainmenuMusic);
        yield return StartCoroutine(_uiFadeEffects.DoFadeOut(fullScreenFade, Color.black));
        fullScreenFade.gameObject.SetActive(true);
        GameManager.Instance.SendToMenu();
        ScoreSystem.EndDay();
        SceneManager.LoadScene("PredayScene");
    }
    
    
    public void ScoreResult()
    {
        HideUI();
        endResultScreen.gameObject.SetActive(true);
        HideInventory();
    }

    public void DayOver()
    {
        GameManager.Instance.EndCafeDay();
        //StartCoroutine(_uiFadeEffects.DoFadeOut(endHourScreen, Color.black));
        StartCoroutine(_uiFadeEffects.ImageFlashEvent(resultFlash, eventFlashColor));
        AudioManager.Instance.PlaySfx(AudioManager.Instance.dayEndSFX);
    }


    public void NewItem()
    {
        StartCoroutine(_uiFadeEffects.ImageFlashEvent(bottomFlash, Color.white));
    }

    public void StreakUpdate()
    {
        streakText.text = StreakManager.Streak.ToString();
    }
}