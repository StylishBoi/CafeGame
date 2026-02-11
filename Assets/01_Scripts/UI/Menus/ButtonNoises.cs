using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ButtonNoises : MonoBehaviour
{
    private Button button;

    private void Start()
    {
        button = GetComponent<Button>();
    }

    public void HandleMouseEnter() 
    {
        if (button.IsInteractable())
        {
            AudioManager.Instance.PlaySfx(AudioManager.Instance.buttonSelectSFX);
        }
    }
    
    public void HandleMousePress() 
    {
        if (button.IsInteractable())
        {
            AudioManager.Instance.PlaySfx(AudioManager.Instance.buttonPressSFX);
        }
        else
        {
            AudioManager.Instance.PlaySfx(AudioManager.Instance.buttonErrorSFX);
        }
    }
}
