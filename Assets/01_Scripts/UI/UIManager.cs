using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [Header("Screen Effect")]
    [SerializeField] private Image resultFlash;
    [SerializeField] private Image bottomFlash;
    [SerializeField] private Image fullScreenFade;
    [SerializeField] private float timeToFade;
    
    [Header("Fade Color")]
    [SerializeField] private Color badResult;
    [SerializeField] private Color mediocreResult;
    [SerializeField] private Color goodResult;
    [SerializeField] private Color eventFlash;
    
    private GameObject UI;
    [Header("UI Game Objects")]
    [SerializeField] private GameObject resultScreen;
    [SerializeField] private GameObject rushHourScreen;
    [SerializeField] private GameObject rushOverScreen;
    [SerializeField] private GameObject endHourScreen;
    [SerializeField] private GameObject bottomFlashScreen;
    [SerializeField] private GameObject inventoryScreen;
    
    private bool _sideFade;
    private bool _bottomFade;
    private bool _fullFade;
    private bool _oppositeFade=true;
    
    private Color _currentColor;
    
    [SerializeField] private TextMeshProUGUI streakText;
    
    public static UIManager Instance;
    
    void Awake()
    {
        UI = transform.GetChild(0).gameObject;
        UI.SetActive(true);
        
        resultScreen.SetActive(false);
        rushHourScreen.SetActive(false);
        rushOverScreen.SetActive(false);
        endHourScreen.SetActive(false);
        bottomFlashScreen.SetActive(false);
            
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

    void Update()
    {
        if (_sideFade)
        {
            resultScreen.SetActive(true);
            _currentColor=resultFlash.color;
            if (_currentColor.a >= 0)
            {
                if (rushHourScreen.activeSelf || rushOverScreen.activeSelf || endHourScreen.activeSelf)
                {
                    _currentColor.a -= Time.deltaTime/2.5f;
                }
                else
                {
                    _currentColor.a -= Time.deltaTime;
                }
                
                if (_currentColor.a <= 0)
                {
                    _sideFade = false;
                    resultScreen.SetActive(false);

                    if (rushHourScreen.activeSelf)
                    {
                        rushHourScreen.SetActive(false);
                    }
                    else if (rushOverScreen.activeSelf)
                    {
                        rushOverScreen.SetActive(false);
                    }
                    else if (endHourScreen.activeSelf)
                    {
                        endHourScreen.SetActive(false);
                    }
                }
                resultFlash.color = _currentColor;
            }
        }
        
        if (_bottomFade)
        {
            bottomFlashScreen.SetActive(true);
            _currentColor=bottomFlash.color;
            if (_currentColor.a >= 0)
            {
                _currentColor.a -= Time.deltaTime;
                
                if (_currentColor.a <= 0)
                {
                    _bottomFade = false;
                    bottomFlashScreen.SetActive(false);
                }

                bottomFlash.color = _currentColor;
            }
        }
        if (_fullFade)
        {
            _currentColor=fullScreenFade.color;
            if (_currentColor.a <= 1)
            {
                _currentColor.a += Time.deltaTime/5;
                
                if (_currentColor.a >= 1)
                {
                    _fullFade=false;
                    SceneManager.LoadScene("MainMenu");
                }

                fullScreenFade.color = _currentColor;
            }
        }
        if (_oppositeFade)
        {
            _currentColor=fullScreenFade.color;
            if (_currentColor.a > 0)
            {
                _currentColor.a -= Time.deltaTime/3;
                
                fullScreenFade.color = _currentColor;
            }
            else
            {
                TimeManager.InTransition = false;
                Debug.Log("This has been false");
                _oppositeFade=false;
            }
        }
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
        resultFlash.color = goodResult;
        _sideFade = true;
    }

    public void RushHour()
    {
        rushHourScreen.SetActive(true);
        resultFlash.color = eventFlash;
        _sideFade = true;
    }
    public void RushOver()
    {
        rushOverScreen.SetActive(true);
        resultFlash.color = eventFlash;
        _sideFade = true;
    }
    
    public void EndOfDayFade()
    {
        TimeManager.InTransition = true;
        HideInventory();
        _fullFade = true;
    }
    
    public void DayOver()
    {
        TimeManager.InTransition = true;
        endHourScreen.SetActive(true);
        resultFlash.color = eventFlash;
        _sideFade = true;
    }
    public void Mediocre()
    {
        resultFlash.color = mediocreResult;
        _sideFade = true;
    }
    
    public void NewItem()
    {
        bottomFlash.color = Color.white;
        _bottomFade = true;
    }
    
    public void Failure()
    {
        resultFlash.color = badResult;
        _sideFade = true;
    }

    public void StreakUpdate()
    {
        streakText.text = StreakManager.Streak.ToString();
    }
}
