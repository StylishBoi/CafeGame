using TMPro;
using UnityEngine;
using UnityEngine.Serialization;

public class ScoreEffect : MonoBehaviour
{
    private float _effectTimer;
    private TextMeshProUGUI _effectText;
    public int effectScore;
    
    private Color _baseColor;

    void Start()
    {
        if (TryGetComponent(out _effectText)) {}
        
        _effectText.text = effectScore.ToString();

        if (effectScore < 5)
        {
            _effectText.color = new Color32(255, 150, 0, 255);
            _baseColor = _effectText.color;
        }
        else
        {
            _effectText.color = new Color32(0, 200, 0, 255);
            _baseColor = _effectText.color;
        }
    }
    void Update()
    {
        transform.localPosition += new Vector3(0f, 100f, 0f)*Time.deltaTime;
        _effectText.color -= new Color(_baseColor.r, _baseColor.g, _baseColor.b, 0.5f) * Time.deltaTime;
        
        _effectTimer += Time.deltaTime;
        if (_effectTimer >= 2f)
        {
            Destroy(this.gameObject);
        }
    }
}
