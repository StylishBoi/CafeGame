using UnityEngine;
using UnityEngine.Rendering;

public  class GameMaster

{
    public static Item[] _itemsList;
    public static int Lives;
    
    public static void AddItem()
    {
        for (int i = 0; i < 3; i++)
        {
            if (_itemsList[i] == null)
            {
                
            }
        }
    }
    
    public static void RemoveItem(int slotToRemove)
    {
    }
}
