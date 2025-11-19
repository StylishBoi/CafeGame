using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;
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

    [Header("Other UI Elements")] [SerializeField]
    private GameObject rushHourScreen;

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
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        StartOfDayFade();
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

    public void RushHour()
    {
        StartCoroutine(_uiFadeEffects.ImageFlashEvent(resultFlash, eventFlashColor, rushHourScreen));
    }

    public void RushOver()
    {
        StartCoroutine(_uiFadeEffects.ImageFlashEvent(resultFlash, eventFlashColor, rushOverScreen));
    }

    public void StartOfDayFade()
    {
        StartCoroutine(_uiFadeEffects.DoFadeIn(fullScreenFade, Color.white));
        TimeManager.InTransition = false;
    }

    public void EndOfDayFade()
    {
        TimeManager.InTransition = true;
        HideInventory();
        StartCoroutine(_uiFadeEffects.DoFadeOut(fullScreenFade, Color.black));
    }

    public void DayOver()
    {
        TimeManager.InTransition = true;
        StartCoroutine(_uiFadeEffects.DoFadeOut(endHourScreen, Color.black));
        StartCoroutine(_uiFadeEffects.ImageFlashEvent(resultFlash, eventFlashColor));
    }

    public void Mediocre()
    {
        StartCoroutine(_uiFadeEffects.ImageFlashEvent(resultFlash, mediocreResultColor));
    }

    public void NewItem()
    {
        StartCoroutine(_uiFadeEffects.ImageFlashEvent(bottomFlash, Color.white));
    }

    public void Failure()
    {
        StartCoroutine(_uiFadeEffects.ImageFlashEvent(resultFlash, badResultColor));
    }

    public void StreakUpdate()
    {
        streakText.text = StreakManager.Streak.ToString();
    }
}