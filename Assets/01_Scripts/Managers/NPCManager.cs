using UnityEngine;
using UnityEngine.Serialization;

public class NPCManager : MonoBehaviour
{
    [Header("NPCs Settings")]
    [SerializeField] private GameObject npcPrefab;
    [SerializeField] private GameObject spawnPosition;
    
    [Header("NPCs Cooldowns")]
    [SerializeField] private float minNormalCoolDown;
    [SerializeField] private float maxNormalCoolDown;
    [SerializeField] private float minRushHourCoolDown;
    [SerializeField] private float maxRushHourCoolDown;
    private bool _isRushHour;
    public static bool IsDayOver;
    private bool _dayReallyOver;
    
    private float _costumerCoolDown;
    private float _customerTimer;
    private GameObject[] _seats;
    private UIManager _uiManager;
    
    private void OnEnable()
    {
        TimeManager.OnHourChanged+=RushHour;
    }

    private void OnDisable()
    {
        TimeManager.OnHourChanged-=RushHour;
    }
    void Start()
    {
        _seats = GameObject.FindGameObjectsWithTag("Seat");
        Debug.Log(_seats.Length);
        _uiManager = GameObject.FindGameObjectWithTag("UI").GetComponent<UIManager>();
        _dayReallyOver = false;
        IsDayOver = false;
    }

    void Update()
    {
        if (!IsDayOver && !TimeManager.InTransition)
        {
            if (_isRushHour)
            {
                if (_customerTimer >= _costumerCoolDown)
                {
                    AddCustomer();
                    _customerTimer = 0;
                    _costumerCoolDown=Random.Range(minRushHourCoolDown, maxRushHourCoolDown);
                }
            }
            else
            {
                if (_customerTimer >= _costumerCoolDown)
                {
                    AddCustomer();
                    _customerTimer = 0;
                    _costumerCoolDown=Random.Range(minNormalCoolDown, maxNormalCoolDown);
                }
            }
            _customerTimer += Time.deltaTime;
        }
        else if(!_dayReallyOver && IsDayOver)
        {
            EndOfDay();
        }
    }

    void RushHour()
    {
        if (TimeManager.Hour == 11 || TimeManager.Hour == 17)
        {
            Debug.Log("Rush Hour !");
            _isRushHour = true;
            _costumerCoolDown = 2f;
            _uiManager.RushHour();
        }
        else if (TimeManager.Hour == 13 || TimeManager.Hour == 19)
        {
            Debug.Log("Rush Hour is finished");
            _isRushHour = false;
            _uiManager.RushOver();
        }
        else if (TimeManager.Hour == 21)
        {
            Debug.Log("Day is finished");
            IsDayOver = true;
            _uiManager.DayOver();
        }
    }
    
    void AddCustomer()
    {
        for (int i = 0; i < _seats.Length; i++)
        {
            if (_seats[i].transform.childCount == 0)
            {
                Instantiate(npcPrefab, spawnPosition.transform.position, Quaternion.identity, _seats[i].transform);
                break;
            }
        }
    }
    
    void EndOfDay()
    {
        for (int i = 0; i < _seats.Length; i++)
        {
            if (_seats[i].transform.childCount != 0)
            {
                return;
            }
        }
        Debug.Log("All clients left");
        _dayReallyOver=true;
        UIManager.Instance.EndOfDayFade();
    }
    //1 - Setup end of day procedure
    //2 - Make rush hour text last longer
}
