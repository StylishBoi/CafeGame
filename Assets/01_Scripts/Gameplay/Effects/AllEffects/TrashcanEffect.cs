using UnityEngine;
using UnityEngine.Events;

public class TrashcanEffect : MonoBehaviour
{
    [SerializeField] private GameObject icon;
    private bool _isActive;
    
    private EffectManager _effectManager;

    void Start()
    {
        _effectManager = FindObjectOfType<EffectManager>();
    }
    void FixedUpdate()
    {
        if (Trashcan.FillageRate>=3 && !_isActive)
        {
            _isActive = true;
            _effectManager.NegativeEffectInc(icon);
        }
        else if (Trashcan.FillageRate == 0 && _isActive)
        {
            _isActive = false;
            _effectManager.NegativeEffectDec(icon);
        }
    }
}
