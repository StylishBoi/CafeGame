using UnityEngine;
using Pathfinding;
using TMPro;
using UnityEngine.Serialization;

public class NPCAI : MonoBehaviour
{
    [Header("Visual Cue")]
    [SerializeField] private GameObject visualCue;
    [SerializeField] private GameObject interactionCue;
    [SerializeField] private Item[] listOfItems;
    [SerializeField] private SpriteRenderer imageRequested;

    [Header("Timer Visual")]
    [SerializeField] private SpriteRenderer timerSprite;
    private float _totalRadius;
    
    [Header("Arrival Phase")]
    [SerializeField] [Range(0, 1)] private float arrivalFactor=1f;
    [SerializeField] private float movementSpeed = 1f;
    
    [Header("Wait Phase")]
    [SerializeField] [Range(0, 1)] private float waitFactor=1f;
    [SerializeField] private float timeToWait=15f;
    
    [Header("Eat Phase")]
    [SerializeField] [Range(0, 1)] private float eatFactor=1f;
    [SerializeField] private float timeToEat=10f;
    
    [Header("Leave Happy Phase")]
    [SerializeField] [Range(0, 1)] private float leaveHappyFactor=1f;
    
    [Header("Leave Unhappy Phase")]
    [SerializeField] [Range(0, 1)] private float leaveUnhappyFactor=1f;
    
    [Header("Exiting Phase")]
    [SerializeField] [Range(0, 1)] private float exitFactor=1f;
    
    [Header("FSM States Variables")]
    public float waitTimer;
    public bool servedGood;
    public bool servedBad;
    public float eatTimer;
    public AIPath aiPath;
    
    //Target Components
    private Transform _targetSeat;
    private Transform _spawnpoint;
    
    //AI Components
    private int _random;
    private Item _desiredItem;
    public ItemPickUp itemPickUp;
    private UIManager _uIManager;
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
    
    private void Awake()
    {
        //Find components
        _targetSeat = transform.parent;
        _spawnpoint=GameObject.FindGameObjectWithTag("Spawnpoint").transform;
        _uIManager= GameObject.FindGameObjectWithTag("UI").GetComponent<UIManager>();
        if (TryGetComponent(out aiPath)) 
        
        //Gets the walk path
        aiPath.maxSpeed = movementSpeed;
        aiPath.destination = _targetSeat.position;
    }
    
    private void Start()
    {
        //Set up the icon visualizer
        visualCue.SetActive(false);
        interactionCue.SetActive(false);
        
        //Desired item set-up
        _random = Random.Range(0, listOfItems.Length);
        _desiredItem = listOfItems[_random];
        imageRequested.sprite=_desiredItem.image;
        
        //Set up the wait timer
        waitTimer = timeToWait;
        eatTimer = timeToEat;
    }

    void FixedUpdate()
    {
        if (arrivalFactor > 0)
        {
            //Empty
        }
        if (waitFactor>0)
        {
            Wait();
        }
        if (eatFactor>0)
        {
            //Empty
        }
        if (leaveHappyFactor>0)
        {
            //Empty
        }
        if (leaveUnhappyFactor>0)
        {
            //Empty
        }
        if (exitFactor>0)
        {
            Exit();
        }
    }
    
    void Wait()
    {
        //Update timer and the colors for it
        _totalRadius += (Time.deltaTime*360f)/timeToWait;
        
        timerSprite.material.SetFloat("_Arc1", _totalRadius);

        if (_totalRadius >= 180f && timerSprite.color!=Color.yellow)
        {
            timerSprite.color = Color.yellow;
        }
        if (_totalRadius >= 270f && timerSprite.color!=Color.red)
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

                if (_desiredItem == localItem && localItem.quality == _desiredItem.quality)
                {
                    _uIManager.Success();
                    Score.ScoreIncrease(localItem.score);
                    StreakManager.StreakIncrease();
                    servedGood = true;
                }
                else if (_desiredItem.itemCode == localItem.itemCode-1 && localItem.quality != _desiredItem.quality)
                {
                    _uIManager.Mediocre();
                    Score.ScoreIncrease(localItem.score);
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
        aiPath.destination = _spawnpoint.position;
        Debug.Log("New path destination : " + aiPath.destination);
        interactionCue.SetActive(false);
        visualCue.SetActive(false);
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
    
    //1 - Fix milkshake mini-game
    //2 - Fix UI overlay
    //3 - Add randomness to coffee mini-game
    //4 - Add proper timer to NPCs
    //5 - Add more mini-games
}
