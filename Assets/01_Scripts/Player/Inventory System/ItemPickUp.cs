using UnityEngine;

public class ItemPickUp : MonoBehaviour
{
    private InventoryManager inventoryManager;
    public Item itemToPickUp;

    void Start()
    {
        inventoryManager= GameObject.Find("InventoryManager").GetComponent<InventoryManager>();
    }

    public void PickUpItem()
    {
        //Verify if the inventory is full before deciding to add or not
        bool result = inventoryManager.AddItem(itemToPickUp);
        
        if (result == true)
        {
            Debug.Log("Item Added");
        }
        else
        {
            Debug.Log("Item Not Added");
        }
    }

    public void GetSelectedItem()
    {
        Item receivedItem=inventoryManager.GetSelectedItem(false);
        if (receivedItem != null)
        {
            Debug.Log("Received item : " +receivedItem);
        }
        else
        {
            Debug.Log("No item received!");
        }
    }
    
    public void UseSelectedItem()
    {
        Item receivedItem=inventoryManager.GetSelectedItem(true);
        if (receivedItem != null)
        {
            Debug.Log("Used item : " +receivedItem);
        }
        else
        {
            Debug.Log("No item used!");
        }
    }
}
