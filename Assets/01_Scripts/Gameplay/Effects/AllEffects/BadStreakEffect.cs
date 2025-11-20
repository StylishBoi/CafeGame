using UnityEngine;
using UnityEngine.Events;

public class BadStreakEffect : MonoBehaviour
{
    [SerializeField] private GameObject icon;
    private bool _isActive;
    
    private EffectManager _effectManager;

    void Start()
    {
        _effectManager = FindFirstObjectByType<EffectManager>();
    }
    void FixedUpdate()
    {
        if (StreakManager.NegativeStreak>=2 && !_isActive)
        {
            _isActive = true;
            _effectManager.NegativeEffectInc(icon);
        }
        else if (StreakManager.NegativeStreak == 0 && _isActive)
        {
            _isActive = false;
            _effectManager.NegativeEffectDec(icon);
        }
    }
}
