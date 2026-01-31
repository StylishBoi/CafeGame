using System;
using UnityEngine;
using System.Collections.Generic;
using System.Collections;
using UnityEngine.PlayerLoop;
using UnityEngine.Serialization;
using Random = UnityEngine.Random;

public class MilkshakeMinigame : MonoBehaviour
{
    [Header("Minigames Awards")]
    [SerializeField] Item goodMinigameItem;
    [SerializeField] Item badMinigameItem;
    
    [Header("Minigame")]
    [SerializeField] GameObject minigameHeader;
    
    //Creates the numerified chain
    private int[] arrowOrderInts = new int[4];
    
    [Header("Buttons")]
    //Visualies the buttons
    public List<SpriteRenderer> arrowSpots = new List<SpriteRenderer>();
    public List<ArrowButton> listOfArrowButtons = new List<ArrowButton>();
    
    [Header("Animation")]
    //Animate milkshake
    [SerializeField] private GameObject milkshake;
    private bool _repeat;
    //Milkshake move positions
    public Transform[] shakePositions = new Transform[2];

    //Verify the presses
    private int _currentPress;
    private int _currentPosition;

    //Takes the arrows sprites from the inspector
    public Sprite[] spritesOfArrows = new Sprite[4];
    
    
    void Awake()
    {
        //Gets the milkshame gameobject
        milkshake = GameObject.Find("MilkShake");
        
        //Creates the list of arrows icons
        SpriteRenderer[] arrowArray = gameObject.GetComponentsInDirectChildren<SpriteRenderer>();
        arrowSpots = new List<SpriteRenderer>(arrowArray);
        
        //Create the list of arrow backdrops
        ArrowButton[] gameArray = GetComponentsInChildren<ArrowButton>();
        listOfArrowButtons = new List<ArrowButton>(gameArray);
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

    private void OnDisable()
    {
        
        _currentPosition = 0;
        _currentPress = 4;
        MakeArrowsAppear();
    }
    
    void Update()
    {
        if (MinigameInput.Instance.GetMoveUPressed())
        {
            _currentPress = 0;
            Verification();
        }
        else if (MinigameInput.Instance.GetMoveRPressed())
        {
            _currentPress = 1;
            Verification();
        }
        else if (MinigameInput.Instance.GetMoveDPressed())
        {
            _currentPress = 2;
            Verification();
        }
        else if (MinigameInput.Instance.GetMoveLPressed())
        {
            _currentPress = 3;
            Verification();
        }
    }

    void Verification()
    {
        if (_currentPress == arrowOrderInts[_currentPosition])
        {
            listOfArrowButtons[_currentPosition].GoodClicked();
            _currentPosition++;
                
            if (_repeat)
            {
                milkshake.transform.position = shakePositions[0].position;
                _repeat = false;
            }
            else if (!_repeat)
            {
                milkshake.transform.position = shakePositions[1].position;
                _repeat = true;
            }
        }
        else
        {
            listOfArrowButtons[_currentPosition].BadClicked();
            InventoryManager.Instance.AddItem(badMinigameItem);
            StartCoroutine(MinigameLeave());
        }

        if (_currentPosition > arrowOrderInts.Length - 1)
        {
            InventoryManager.Instance.AddItem(goodMinigameItem);
            StartCoroutine(MinigameLeave());
        }
    }
    
    void MakeArrowsAppear()
    {
        //Randomly choose which arrow will be present in the list
        // 0 - Up | 1 - Right | 2 - Down | 3 - Left
        for (int i = 0; i < 4; i++)
        {
            int random = Random.Range(0, 4);
            arrowOrderInts[i]=random;
            Debug.Log(arrowOrderInts[i]);
        }
        
        //Changes the icon in the arrow spot with the corresponding arrow
        foreach (SpriteRenderer arrow in arrowSpots)
        {
            arrow.sprite = spritesOfArrows[arrowOrderInts[arrowSpots.IndexOf(arrow)]];
        }
    }

    IEnumerator MinigameLeave()
    {
        yield return new WaitForSeconds(0.5f);
        
        MinigameManager.Instance.MiniGameEnd();
        minigameHeader.SetActive(false);
    }
}
