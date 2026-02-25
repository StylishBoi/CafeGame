using UnityEngine;

public class IconDescription : MonoBehaviour
{
    [SerializeField] private float screenPadding;
    private RectTransform _myRectTransform;
    private RectTransform _canvasRect;
    private Vector2 _iconHalfSize;
    private Vector2 _canvasSize;
    
    void Awake()
    {
        _myRectTransform = (RectTransform)transform;
        _canvasRect = GetComponentInParent<Canvas>().GetComponent<RectTransform>();
        DefineScreenValues();
    }

    void OnEnable()
    {
        Vector2 pos = _myRectTransform.anchoredPosition;
        
        pos.x = (int)(Mathf.Clamp(pos.x, -_canvasSize.x * 0.5f + _iconHalfSize.x + screenPadding,
            _canvasSize.x * 0.5f - _iconHalfSize.x - screenPadding));
        
        _myRectTransform.anchoredPosition = new Vector3(pos.x, pos.y, 0);
    }
    
    void DefineScreenValues()
    {
        _canvasSize = _canvasRect.rect.size;
        _iconHalfSize = new Vector2(_myRectTransform.rect.width * 0.5f, _myRectTransform.rect.height * 0.5f);
    }
}
