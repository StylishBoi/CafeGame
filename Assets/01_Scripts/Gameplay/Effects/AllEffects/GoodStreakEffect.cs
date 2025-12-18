using UnityEngine;
using UnityEngine.Events;

public class GoodStreakEffect : MonoBehaviour
{
    [SerializeField] private GameObject icon;
    private bool _isActive;
    
    private EffectManager _effectManager;

    void Start()
    {
        _effectManager = FindFirstObjectByType<EffectManager>();
        _isActive = false;
    }
    void FixedUpdate()
    {
        if (StreakManager.Streak>=2 && !_isActive)
        {
            _isActive = true;
            _effectManager.PositiveEffectInc(icon);
        }
        else if (StreakManager.Streak == 0 && _isActive)
        {
            _isActive = false;
            _effectManager.PositiveEffectDec(icon);
        }
    }
}
