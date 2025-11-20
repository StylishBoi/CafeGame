using UnityEngine;

public class NPCAI : MonoBehaviour
{
    [Header("Visual Cue")] [SerializeField]
    private GameObject visualCue;

    [SerializeField] private GameObject interactionCue;
    [SerializeField] private Item[] listOfItems;
    [SerializeField] private SpriteRenderer imageRequested;

    [Header("Timer Visual")] [SerializeField]
    private SpriteRenderer timerSprite;

    private float _totalRadius;

    [Header("Movement")] public Vector2Int targetPosition;

    [Header("Arrival Phase")] [SerializeField] [Range(0, 1)]
    private float arrivalFactor = 1f;

    [Header("Wait Phase")] [SerializeField] [Range(0, 1)]
    private float waitFactor = 1f;

    [SerializeField] private float timeToWait = 15f;

    [Header("Eat Phase")] [SerializeField] [Range(0, 1)]
    private float eatFactor = 1f;

    [SerializeField] private float timeToEat = 10f;

    [Header("Leave Happy Phase")] [SerializeField] [Range(0, 1)]
    private float leaveHappyFactor = 1f;

    [Header("Leave Unhappy Phase")] [SerializeField] [Range(0, 1)]
    private float leaveUnhappyFactor = 1f;

    [Header("Exiting Phase")] [SerializeField] [Range(0, 1)]
    private float exitFactor = 1f;

    [Header("FSM States Variables")] public float waitTimer;
    public bool servedGood;
    public bool servedBad;
    public float eatTimer;

    //Animator Components
    private Animator _animator;

    private const string _horizontal = "Horizontal";
    private const string _vertical = "Vertical";
    private const string _lastHorizontal = "LastHorizontal";
    private const string _lastVertical = "LastVertical";

    private Vector2 _oldMovement = Vector2.zero;
    private Vector2 _movement;

    //Target Components
    private UnitController _unitController;
    private Transform _targetSeat;
    private Transform _spawnpoint;
    private GameObject _targetName;

    //AI Components
    private int _random;
    private Item _desiredItem;
    public ItemPickUp itemPickUp;
    private CafeUIManager _uIManager;
    private MoneyScoreUI _moneyScoreUI;
    private bool _playerInRange;

    public float ArrivalFactor
    {
        get => arrivalFactor;
        set => arrivalFactor = value;
    }

    public float WaitFactor
    {
        get => waitFactor;
        set => waitFactor = value;
    }

    public float EatFactor
    {
        get => eatFactor;
        set => eatFactor = value;
    }

    public float LeaveHappyFactor
    {
        get => leaveHappyFactor;
        set => leaveHappyFactor = value;
    }

    public float LeaveUnhappyFactor
    {
        get => leaveUnhappyFactor;
        set => leaveUnhappyFactor = value;
    }

    public float ExitFactor
    {
        get => exitFactor;
        set => exitFactor = value;
    }

    private void Start()
    {
        //Find components
        _targetSeat = transform.parent;
        _targetName = transform.parent.gameObject;
        _spawnpoint = GameObject.FindGameObjectWithTag("Spawnpoint").transform;
        _uIManager = GameObject.FindGameObjectWithTag("UI").GetComponent<CafeUIManager>();
        _moneyScoreUI = FindFirstObjectByType<MoneyScoreUI>();
        _animator = GetComponentInChildren<Animator>();
        if (TryGetComponent(out _unitController))

                //New AI Path
                targetPosition = new Vector2Int((int)_targetSeat.position.x, (int)_targetSeat.position.y);
        _unitController.NPCInfo(new Vector2Int((int)transform.position.x, (int)transform.position.y), targetPosition,
            transform);

        //Set up the icon visualizer
        visualCue.SetActive(false);
        interactionCue.SetActive(false);

        //Desired item set-up
        _random = Random.Range(0, listOfItems.Length);
        _desiredItem = listOfItems[_random];
        imageRequested.sprite = _desiredItem.image;

        //Set up the wait timer
        waitTimer = timeToWait;
        eatTimer = timeToEat;
    }

    void FixedUpdate()
    {
        if (arrivalFactor > 0)
        {
            _movement = new Vector2(transform.position.x - _oldMovement.x, transform.position.y - _oldMovement.y);
            _animator.SetFloat(_horizontal, _movement.x);
            _animator.SetFloat(_vertical, _movement.y);

            if (_movement != Vector2.zero)
            {
                _animator.SetFloat(_lastHorizontal, _movement.x);
                _animator.SetFloat(_lastVertical, _movement.y);
            }

            _oldMovement = transform.position;
        }

        if (waitFactor > 0)
        {
            _animator.SetFloat(_horizontal, 0);
            _animator.SetFloat(_vertical, 0);

            if (_targetName.name == "ChairL")
            {
                _animator.SetFloat(_lastHorizontal, -1);
                _animator.SetFloat(_lastVertical, _movement.y);
            }
            else
            {
                _animator.SetFloat(_lastHorizontal, 1);
                _animator.SetFloat(_lastVertical, _movement.y);
            }

            Wait();
        }

        if (eatFactor > 0)
        {
            //Empty
        }

        /*if (leaveHappyFactor>0)
        {
            //Empty
        }*/
        if (leaveUnhappyFactor > 0 || leaveHappyFactor > 0)
        {
            _movement = new Vector2(transform.position.x - _oldMovement.x, transform.position.y - _oldMovement.y);
            _animator.SetFloat(_horizontal, _movement.x);
            _animator.SetFloat(_vertical, _movement.y);

            if (_movement != Vector2.zero)
            {
                _animator.SetFloat(_lastHorizontal, _movement.x);
                _animator.SetFloat(_lastVertical, _movement.y);
            }

            _oldMovement = transform.position;
        }

        if (exitFactor > 0)
        {
            Exit();
        }
    }

    void Wait()
    {
        //Update timer and the colors for it
        _totalRadius += (Time.deltaTime * 360f) / timeToWait;

        timerSprite.material.SetFloat("_Arc1", _totalRadius);

        if (_totalRadius >= 180f && timerSprite.color != Color.yellow)
        {
            timerSprite.color = Color.yellow;
        }

        if (_totalRadius >= 270f && timerSprite.color != Color.red)
        {
            timerSprite.color = Color.red;
        }

        visualCue.SetActive(true);

        if (_playerInRange)
        {
            interactionCue.SetActive(true);
            if (InputManager.GetInstance().GetInteractPressed())
            {
                Item localItem = InventoryManager.Instance.GetSelectedItem(false);

                if (_desiredItem == localItem || _desiredItem.itemCode == localItem.itemCode - 1)
                {
                    switch (localItem.quality)
                    {
                        case Item.ItemQuality.Bad:
                            _uIManager.Success();
                            ScoreSystem.IncreaseScore(localItem.score);
                            break;
                        case Item.ItemQuality.Good:
                            _uIManager.Mediocre();
                            ScoreSystem.IncreaseScore(localItem.score);
                            break;
                        default:
                            Debug.LogWarning("Invalid item quality");
                            break;
                    }

                    _moneyScoreUI.ScoreIncrease();
                    StreakManager.StreakIncrease();
                    servedGood = true;
                }
                else
                {
                    _uIManager.Failure();
                    StreakManager.NegativeStreakIncrease();
                    servedBad = true;
                }
                itemPickUp.UseSelectedItem();
            }
        }
        else
        {
            interactionCue.SetActive(false);
        }
    }

    void Exit()
    {
        Destroy(gameObject);
    }

    public void ClientUIDisabled()
    {
        interactionCue.SetActive(false);
        visualCue.SetActive(false);
    }

    public void NPCLeave()
    {
        targetPosition = new Vector2Int((int)_spawnpoint.position.x, (int)_spawnpoint.position.y);
        _unitController.NPCInfo(new Vector2Int((int)transform.position.x, (int)transform.position.y), targetPosition,
            transform);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            _playerInRange = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            _playerInRange = false;
        }
    }
}