using UnityEngine;
using UnityEngine.SceneManagement;

public class Trashcan : MonoBehaviour
{
    [Header("Visualisers")]
    [SerializeField] private GameObject visualCue;
    
    [Header("Sprite Appeareance")]
    [SerializeField] private Sprite unFilledSprite;
    [SerializeField] private Sprite filledSprite;

    private SpriteRenderer _spriteRenderer;
    public static int FillageRate;
    private bool _playerInRange;
    
    private void Awake()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _playerInRange = false;
        visualCue.SetActive(false);
    }

    void Update()
    {
        if (_playerInRange)
        {
            visualCue.SetActive(true);
            if (InputManager.GetInstance().GetInteractPressed())
            {
                Item receivedItem=InventoryManager.Instance.GetSelectedItem(true);
                if (receivedItem != null && FillageRate != 3)
                {
                    Debug.Log("Used item : " +receivedItem);
                    FillageRate++;
                }
                else if (FillageRate == 3)
                {
                    SceneManager.LoadScene("Trashcan", LoadSceneMode.Additive);
                    FillageRate = 0;
                }
                else
                {
                    Debug.Log("No item used!");
                }
            }
        }
        else
        {
            visualCue.SetActive(false);
        }
        
        if (FillageRate == 3 && _spriteRenderer.sprite == unFilledSprite)
        {
            _spriteRenderer.sprite = filledSprite;
        }
        else if (FillageRate != 3 && _spriteRenderer.sprite == filledSprite)
        {
            _spriteRenderer.sprite = unFilledSprite;
        }
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
