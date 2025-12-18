using UnityEngine;




public class NPCManager : MonoBehaviour
{
    private enum RushModes
    {
        RushOn,
        RushOff
    }
    
    [Header("NPCs Settings")]
    [SerializeField] private GameObject npcPrefab;
    [SerializeField] private GameObject spawnPosition;

    [Header("NPCs Cooldowns")]
    [SerializeField] private float minNormalCoolDown;
    [SerializeField] private float maxNormalCoolDown;
    [SerializeField] private float minRushHourCoolDown;
    [SerializeField] private float maxRushHourCoolDown;

    private bool _isRushHour;
    private float _costumerCoolDown;
    private float _customerTimer;
    
    private RushModes _currentRushMode=RushModes.RushOff;
    private GameObject[] _seats;
    private CafeUIManager _cafeUIManager;

    private void OnEnable()
    {
        TimeManager.OnRushStart += StartRushHour;
        TimeManager.OnRushOver += EndRushHour;
    }

    private void OnDisable()
    {
        TimeManager.OnRushStart -= StartRushHour;
        TimeManager.OnRushOver -= EndRushHour;
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

    void StartRushHour()
    {
        if (_currentRushMode == RushModes.RushOff)
        {
            Debug.Log("Rush Hour !");
            _isRushHour = true;
            _costumerCoolDown = 2f;
            _cafeUIManager.RushHour();
            _currentRushMode= RushModes.RushOn;
        }
    }

    void EndRushHour()
    {
        if (_currentRushMode == RushModes.RushOn)
        {
            Debug.Log("Rush Hour is finished");
            _isRushHour = false;
            _cafeUIManager.RushOver();
            _currentRushMode = RushModes.RushOff;
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

        if(MinigameManager.Instance.State==MinigameState.OutGame)
        {
            GameManager.Instance.SwitchState(GameState.TextBox);
            CafeUIManager.Instance.ScoreResult();
        }
    }
    //1 - Setup end of day procedure
    //2 - Make rush hour text last longer
}