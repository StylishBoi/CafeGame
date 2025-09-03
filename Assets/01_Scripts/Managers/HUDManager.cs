using UnityEngine;
using UnityEngine.UIElements;

public class HUDManager : MonoBehaviour
{
    private UIDocument _uiDocument;
    
    private static Label _goodRatingLabel;
    private static Label _badRatingLabel;
    private static Label _midRatingLabel;
    
    void Start()
    {
        _uiDocument=GetComponent<UIDocument>();
        
        _goodRatingLabel = _uiDocument.rootVisualElement.Q<Label>("Good");
        _badRatingLabel = _uiDocument.rootVisualElement.Q<Label>("Bad");
        _midRatingLabel = _uiDocument.rootVisualElement.Q<Label>("Mid");
        
        _goodRatingLabel.style.display=DisplayStyle.None;
        _badRatingLabel.style.display=DisplayStyle.None;
        _midRatingLabel.style.display=DisplayStyle.None;
    }

    public static void SetGood()
    {
        _goodRatingLabel.style.display=DisplayStyle.None;
    }
}
