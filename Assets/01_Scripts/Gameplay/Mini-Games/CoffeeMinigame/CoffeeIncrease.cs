using UnityEngine;
using UnityEngine.SceneManagement;

public class CoffeeIncrease : MonoBehaviour
{
    [Header("Minigames Awards")]
    [SerializeField] private Item goodMinigameItem;
    [SerializeField] private Item badMinigameItem;
    
    [Header("Minigame")]
    [SerializeField] private GameObject minigameHeader;
    [SerializeField] private GameObject triggerZone;
    [SerializeField] private Transform[] triggerZones;
    
    [Header("Gameobjects")]
    [SerializeField] private GameObject liquidAsset;
    [SerializeField] private GameObject topLiquidAsset;
    
    private bool _pipeStopped;
    private bool _success;
    
    private Vector3 _originalLiquidPosition;
    private Vector3 _originalLiquidScalePosition;
    
    private Vector3 _originalTopPosition;

    void Start()
    {
        _originalLiquidPosition=liquidAsset.transform.position;
        _originalLiquidScalePosition=liquidAsset.transform.localScale;
        
        _originalTopPosition=topLiquidAsset.transform.position;
    }

    void OnEnable()
    {
        AudioManager.Instance.PlaySfx(AudioManager.Instance.coffeePourSFX);
        
        int rand = Random.Range(0, 3);
        triggerZone.transform.position=triggerZones[rand].position;
    }

    void OnDisable()
    {
        Reset();
    }
    
    void FixedUpdate()
    {
        if (_pipeStopped != true)
        {
            liquidAsset.transform.localScale += new Vector3(0f, 0.6f, 0f)*Time.deltaTime;
            liquidAsset.transform.localPosition += new Vector3(0f, 0.2f, 0f)*Time.deltaTime;
            topLiquidAsset.transform.localPosition += new Vector3(0f, 0.4f, 0f)*Time.deltaTime;
        }
        
        if (MinigameInput.Instance.GetInteractPressed() && !_pipeStopped)
        {
            Debug.Log(MinigameInput.Instance.GetInteractPressed());
            _pipeStopped = true;
            
            if (_success)
            {
                AudioManager.Instance.StopSfx();
                InventoryManager.Instance.AddItem(goodMinigameItem);
                AudioManager.Instance.PlaySfx(AudioManager.Instance.itemReceiveSFX);
                MinigameManager.Instance.MiniGameEnd();
            }
            else
            {
                AudioManager.Instance.StopSfx();
                InventoryManager.Instance.AddItem(badMinigameItem);
                AudioManager.Instance.PlaySfx(AudioManager.Instance.itemReceiveSFX);
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
            AudioManager.Instance.StopSfx();
            _pipeStopped = true;
            InventoryManager.Instance.AddItem(badMinigameItem);
            AudioManager.Instance.PlaySfx(AudioManager.Instance.itemReceiveSFX);
            MinigameManager.Instance.MiniGameEnd();
            minigameHeader.SetActive(false);
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
        _success = false;
        
        liquidAsset.transform.position = _originalLiquidPosition;
        liquidAsset.transform.localScale = _originalLiquidScalePosition;
        topLiquidAsset.transform.position = _originalTopPosition;
    }
    
}
