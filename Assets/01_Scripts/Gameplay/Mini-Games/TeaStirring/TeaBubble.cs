using UnityEngine;
using UnityEngine.Serialization;

public class TeaBubble : MonoBehaviour
{
    private GameObject _bubbleTexture;
    private TeaBubbleManager _teaBubbleManager;

    void Start()
    {
        _bubbleTexture = gameObject.transform.GetChild(0).gameObject;
        _teaBubbleManager = GetComponentInParent<TeaBubbleManager>();
    }

    void OnDisable()
    {
        Destroy(gameObject);
    }

    void OnMouseOver()
    {
        Debug.Log("MouseOver");
        AudioManager.Instance.PlaySfx(AudioManager.Instance.teaBubbleSFX);
        _teaBubbleManager.MoveToNextBubble();
        Deactivate();
    }

    private void Deactivate()
    {
        Destroy(gameObject);
    }
}