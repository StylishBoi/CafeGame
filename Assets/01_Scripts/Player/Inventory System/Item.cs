using UnityEngine;

[CreateAssetMenu(menuName = "Scriptable object/Item")]
public class Item : ScriptableObject
{
    public Sprite image;
    public int itemCode;
    public ItemQuality quality;
    public int score;

    public enum ItemQuality
    {
        Bad,
        Good
    }
}
