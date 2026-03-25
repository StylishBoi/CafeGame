using UnityEngine;
using UnityEngine.UI;

public class InventorySlot : MonoBehaviour
{
   public Image image;
   public Color selectedColor, notSelectedColor;

   private void Awake()
   {
      Deselect();
   }
   
   public void Select()
   {
      gameObject.transform.localScale=new Vector3(1.2f, 1.2f, 1.2f);
      image.color = selectedColor;
   }

   public void Deselect()
   {
      gameObject.transform.localScale=new Vector3(1f, 1f, 1f);
      image.color = notSelectedColor;
   }
}
