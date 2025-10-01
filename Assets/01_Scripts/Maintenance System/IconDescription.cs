using UnityEngine;

public class IconDescription : MonoBehaviour
{
    [SerializeField] GameObject description;
    
    void Start()
    {
        description.SetActive(false);
    }
    void OnMouseOver()
    {
        Debug.Log("Mouse Is Over");
        description.SetActive(true);;
    }
    void OnMouseExit()
    {
        description.SetActive(false);
    }
}
