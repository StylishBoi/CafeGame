using UnityEngine;
using System.Collections.Generic;
using System.Collections;

public class MilkshakeMinigame : MonoBehaviour
{
    [Header("Minigames Awards")]
    [SerializeField] Item goodMinigameItem;
    [SerializeField] Item badMinigameItem;
    
    //Creates the numerified chain
    private int[] arrowOrderInts = new int[4];
    private int random;
    
    //Visualies the buttons
    public List<SpriteRenderer> _arrowSpots = new List<SpriteRenderer>();
    public List<ArrowButton> _listOfArrowButtons = new List<ArrowButton>();
    
    //Animate milkshake
    public GameObject milkshake;
    bool repeat = false;

    //Verify the presses
    private int currentPress;
    private int currentPosition;
    
    //Cooldown to avoid constant pressing
    float buttonCoolDown;

    //Takes the arrows sprites from the inspector
    public Sprite[] spritesOfArrows = new Sprite[4];
    
    void Awake()
    {
        //Gets the milkshame gameobject
        milkshake = GameObject.Find("MilkShake");
        
        //Creates the list of arrows icons
        SpriteRenderer[] arrowArray = gameObject.GetComponentsInDirectChildren<SpriteRenderer>();
        _arrowSpots = new List<SpriteRenderer>(arrowArray);
        
        //Create the list of arrow backdrops
        ArrowButton[] gameArray = GetComponentsInChildren<ArrowButton>();
        _listOfArrowButtons = new List<ArrowButton>(gameArray);
        
        //Randomly choose which arrow will be present in the list
        // 0 - Up | 1 - Right | 2 - Down | 3 - Left
        for (int i = 0; i < 4; i++)
        {
            random = Random.Range(0, 4);
            arrowOrderInts[i]=random;
            Debug.Log(arrowOrderInts[i]);
        }
        MakeArrowsAppear();
    }
    
    //Code that only takes direct children from a component
    public static List<T> GetComponentsInDirectChildren<T>(GameObject gameObject) where T : Component
    {
        int length = gameObject.transform.childCount;
        List<T> components = new List<T>(length);
        for (int i = 0; i < length; i++)
        {
            T comp = gameObject.transform.GetChild(i).GetComponent<T>();
            if (comp != null) components.Add(comp);
        }
        return components;
    }
    void Update()
    {
        buttonCoolDown += Time.deltaTime;
        
        if (MinigameInput.Instance.GetMoveUPressed())
        {
            Debug.Log("Up was pressed");
            currentPress = 0;
        }
        else if (MinigameInput.Instance.GetMoveRPressed())
        {
            Debug.Log("Right was pressed");
            currentPress = 1;
        }
        else if (MinigameInput.Instance.GetMoveDPressed())
        {
            Debug.Log("Down was pressed");
            currentPress = 2;
        }
        else if (MinigameInput.Instance.GetMoveLPressed())
        {
            Debug.Log("Left was pressed");
            currentPress = 3;
        }

            
        if (Input.anyKey && buttonCoolDown > 0.5f)
        {
            if (currentPress == arrowOrderInts[currentPosition])
            {
                _listOfArrowButtons[currentPosition].GoodClicked();
                currentPosition++;
                buttonCoolDown = 0;
                
                if (repeat)
                {
                    milkshake.transform.localPosition += new Vector3(0, 50f, 0f)*Time.deltaTime;
                    repeat = false;
                }
                else if (!repeat)
                {
                    milkshake.transform.localPosition += new Vector3(0, -50f, 0f)*Time.deltaTime;
                    repeat = true;
                }
            }
            else
            {
                _listOfArrowButtons[currentPosition].BadClicked();
                buttonCoolDown = 0;
                InventoryManager.Instance.AddItem(badMinigameItem);
                StartCoroutine(waiter());
            }

            if (currentPosition > arrowOrderInts.Length - 1)
            {
                InventoryManager.Instance.AddItem(goodMinigameItem);
                StartCoroutine(waiter());
            }
        }
    }

    void MakeArrowsAppear()
    {
        //Changes the icon in the arrow spot with the corresponding arrow
        foreach (SpriteRenderer arrow in _arrowSpots)
        {
            arrow.sprite = spritesOfArrows[arrowOrderInts[_arrowSpots.IndexOf(arrow)]];
        }
    }

    IEnumerator waiter()
    {
        yield return new WaitForSeconds(0.5f);
        
        MinigameManager.MiniGameEnd();
    }
}
