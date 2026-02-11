using UnityEngine;
using UnityEngine.Serialization;

public class TeaBubble : MonoBehaviour
{
    private GameObject _bubbleTexture;
    private TeaBubbleManager _teaBubbleManager;
    private bool _bubbleActive;

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
            AudioManager.Instance.PlaySfx(AudioManager.Instance.teaBubbleSFX);
            _teaBubbleManager.MoveToNextBubble();
        }
    }
    
    private void Deactivate()
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
