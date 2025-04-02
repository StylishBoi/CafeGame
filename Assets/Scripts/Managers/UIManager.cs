using UnityEngine;

public class UIManager : MonoBehaviour
{
    public GameObject UI;
    void Awake()
    {
        UI = transform.GetChild(0).gameObject;
        UI.SetActive(true);
        
    }

    public void HideUI()
    {
        UI.SetActive(false);
    }
    
    public void ShowUI()
    {
        UI.SetActive(true);
    }
}

//Fix later
