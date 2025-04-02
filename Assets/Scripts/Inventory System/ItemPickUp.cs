using UnityEngine;

public class ItemPickUp : MonoBehaviour
{
    public InventoryManager inventoryManager;
    public Item itemToPickUp;

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

    public void RemoveSelectedItem()
    {
        Item receivedItem=inventoryManager.GetSelectedItem(true);
        if (receivedItem != null)
        {
            Debug.Log("Received item : " +receivedItem);
        }
        else
        {
            Debug.Log("No item received!");
        }
    }
}
