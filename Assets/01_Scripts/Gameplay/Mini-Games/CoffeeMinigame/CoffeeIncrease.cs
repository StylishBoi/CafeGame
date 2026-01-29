using UnityEngine;
using UnityEngine.SceneManagement;

public class CoffeeIncrease : MonoBehaviour
{
    [Header("Minigames Awards")]
    [SerializeField] private Item goodMinigameItem;
    [SerializeField] private Item badMinigameItem;
    
    [Header("Minigame")]
    [SerializeField] private GameObject minigameHeader;
    
    private bool _pipeStopped;
    private bool _success;
    
    private Vector3 _pipePosition;
    private Vector3 _pipeScale;

    void Start()
    {
        _pipePosition=transform.position;
        _pipeScale=transform.localScale;
    }

    void OnDisable()
    {
        Reset();
    }
    void FixedUpdate()
    {
        if (_pipeStopped != true)
        {
            this.transform.localScale += new Vector3(0f, 0.4f, 0f)*Time.deltaTime;
            this.transform.localPosition += new Vector3(0f, 0.2f, 0f)*Time.deltaTime;
        }
        
        if (MinigameInput.Instance.GetInteractPressed() && _pipeStopped == false)
        {
            Debug.Log(MinigameInput.Instance.GetInteractPressed());
            _pipeStopped = true;
            
            if (_success)
            {
                InventoryManager.Instance.AddItem(goodMinigameItem);
                MinigameManager.Instance.MiniGameEnd();
            }
            else
            {
                InventoryManager.Instance.AddItem(badMinigameItem);
                MinigameManager.Instance.MiniGameEnd();
            }
            minigameHeader.SetActive(false);
        }
    }
    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("Collision Occured");
        
        if (collision.CompareTag("EndZone"))
        {
            Debug.Log("Not a success");
            _pipeStopped = true;
            InventoryManager.Instance.AddItem(badMinigameItem);
            MinigameManager.Instance.MiniGameEnd();
        }
        
        else if (collision.CompareTag("SuccessZone"))
        {
            Debug.Log("Currently successful");
            _success = true;
        }
    }

    private void Reset()
    {
        _pipeStopped = false;
        transform.localScale = _pipeScale;
        transform.position = _pipePosition;
    }
    
}
