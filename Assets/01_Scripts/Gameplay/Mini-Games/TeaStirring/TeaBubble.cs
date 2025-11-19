using UnityEngine;

public class TeaBubble : MonoBehaviour
{
    public GameObject _bubbleTexture;
    public TeaBubbleManager _teaBubbleManager;
    public bool _bubbleActive;

    void Awake()
    {
        _bubbleTexture = gameObject.transform.GetChild(0).gameObject;
        _teaBubbleManager = GetComponentInParent<TeaBubbleManager>();
        Deactivate();
    }
    void OnMouseOver()
    {
        if (_bubbleActive==true)
        {
            Deactivate();
            _teaBubbleManager.MoveToNextBubble();
        }
    }
    
    public void Deactivate()
    {
        //Deactivate the target
        _bubbleActive = false;
        _bubbleTexture.SetActive(false);
    }
    
    public void Activate()
    {
    
        //Activate the target
        _bubbleActive = true;
        _bubbleTexture.SetActive(true);
    }
}
