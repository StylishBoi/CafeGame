using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    public InventorySlot[] inventorySlots;
    public GameObject inventoryItemPrefab;
    public int slotsInUsage;

    private int selectedSlot = -1;
    
    public static InventoryManager Instance;
    
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    
    private void Start()
    {
        ChangeSelectedSlot(0);
    }

    private void Update()
    {
        if (Input.inputString != null)
        {
            bool isNumber=int.TryParse(Input.inputString, out int number);
            if (isNumber && number > 0 && number < 4)
            {
                ChangeSelectedSlot(number-1);
            }
        }
    }
    
    void ChangeSelectedSlot(int newValue)
    {
        if (selectedSlot >= 0)
        {
            inventorySlots[selectedSlot].Deselect();  
        }
        
        inventorySlots[newValue].Select();
        selectedSlot = newValue;
    }
    
    public bool AddItem(Item item)
    {
        //Find any empty slot
        for (int i = 0; i < inventorySlots.Length; i++)
        {
            InventorySlot slot = inventorySlots[i];
            InventoryItem itemInSlot=slot.GetComponentInChildren<InventoryItem>();
            Debug.Log(slot.GetComponentInChildren<InventoryItem>());
            if (itemInSlot == null)
            {
                SpawnNewItem(item, slot);
                slotsInUsage++;
                return true;
            }
        }
        
        return false;
    }

    void SpawnNewItem(Item item, InventorySlot slot)
    {
        GameObject newItemGo=Instantiate(inventoryItemPrefab, slot.transform);
        
        newItemGo.name = "FOOD : " + item.name;
        slot.transform.GetChild(0).GetComponent<SlotColor>().SetQualityColor(item.quality.ToString());
        
        InventoryItem inventoryItem = newItemGo.GetComponent<InventoryItem>();
        inventoryItem.InitialiseItem(item);
    }

    public Item GetSelectedItem(bool use)
    {
        InventorySlot slot = inventorySlots[selectedSlot];
        InventoryItem itemInSlot = slot.GetComponentInChildren<InventoryItem>();
        if (itemInSlot != null)
        {
            Item item = itemInSlot.item;
            
            if (use == true)
            {
                slot.transform.GetChild(0).GetComponent<SlotColor>().SetNeutral();
                Destroy(itemInSlot.gameObject);
                slotsInUsage--;
            }
            return item;
        }
        return null;
    }
}
