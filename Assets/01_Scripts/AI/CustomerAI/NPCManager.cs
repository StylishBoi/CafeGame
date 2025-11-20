using UnityEngine;
using UnityEngine.Serialization;

public class NPCManager : MonoBehaviour
{
    [Header("NPCs Settings")] [SerializeField]
    private GameObject npcPrefab;

    [SerializeField] private GameObject spawnPosition;

    [Header("NPCs Cooldowns")] [SerializeField]
    private float minNormalCoolDown;

    [SerializeField] private float maxNormalCoolDown;
    [SerializeField] private float minRushHourCoolDown;
    [SerializeField] private float maxRushHourCoolDown;
    private bool _isRushHour;

    private float _costumerCoolDown;
    private float _customerTimer;
    private GameObject[] _seats;
    private CafeUIManager _cafeUIManager;

    [SerializeField] private TimeManager timeManager;

    private void OnEnable()
    {
        TimeManager.OnHourChanged += RushHour;
    }

    private void OnDisable()
    {
        TimeManager.OnHourChanged -= RushHour;
    }

    void Start()
    {
        _seats = GameObject.FindGameObjectsWithTag("Seat");
        Debug.Log(_seats.Length);
        _cafeUIManager = GameObject.FindGameObjectWithTag("UI").GetComponent<CafeUIManager>();
    }

    void Update()
    {
        if (GameManager.Instance.State == GameState.CafePlay)
        {
            if (_isRushHour)
            {
                if (_customerTimer >= _costumerCoolDown)
                {
                    AddCustomer();
                    _customerTimer = 0;
                    _costumerCoolDown = Random.Range(minRushHourCoolDown, maxRushHourCoolDown);
                }
            }
            else
            {
                if (_customerTimer >= _costumerCoolDown)
                {
                    AddCustomer();
                    _customerTimer = 0;
                    _costumerCoolDown = Random.Range(minNormalCoolDown, maxNormalCoolDown);
                }
            }

            _customerTimer += Time.deltaTime;
        }
        else if (GameManager.Instance.State == GameState.BasicPlay)
        {
            EndOfDay();
        }
    }

    void RushHour()
    {
        if (TimeManager.Hour == timeManager.rushHours[0].startHour ||
            TimeManager.Hour == timeManager.rushHours[1].startHour)
        {
            Debug.Log("Rush Hour !");
            _isRushHour = true;
            _costumerCoolDown = 2f;
            _cafeUIManager.RushHour();
        }
        else if (TimeManager.Hour == timeManager.rushHours[0].endHour ||
                 TimeManager.Hour == timeManager.rushHours[1].endHour)
        {
            Debug.Log("Rush Hour is finished");
            _isRushHour = false;
            _cafeUIManager.RushOver();
        }
        else if (TimeManager.Hour == timeManager.endDayHour)
        {
            Debug.Log("Day is finished");
            GameManager.Instance.SwitchState(GameState.BasicPlay);
            _cafeUIManager.DayOver();
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
        CafeUIManager.Instance.EndOfDayFade();
        GameManager.Instance.SwitchState(GameState.Cinematic);
    }
    //1 - Setup end of day procedure
    //2 - Make rush hour text last longer
}