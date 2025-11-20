using UnityEngine;
using UnityEngine.Events;

public class DirtyEffect : MonoBehaviour
{
    [SerializeField] private GameObject icon;
    [SerializeField] private int maxTillDirty = 5;
    
    private float _timerTillDirty = 0;
    private bool _isActive;
    
    private EffectManager _effectManager;

    void Start()
    {
        _effectManager = FindFirstObjectByType<EffectManager>();
    }
    void FixedUpdate()
    {
        if (MaintenanceManager.CurrentMaintenanceEvents.Count != 0)
        {
            _timerTillDirty += Time.deltaTime;
        }
        else if (_timerTillDirty != 0)
        {
            _timerTillDirty = 0;
        }
        
        if (_timerTillDirty > maxTillDirty && !_isActive)
        {
            _isActive = true;
            _effectManager.NegativeEffectInc(icon);
        }
        else if (MaintenanceManager.CurrentMaintenanceEvents.Count == 0 && _isActive)
        {
            _timerTillDirty = 0;
            _isActive = false;
            _effectManager.NegativeEffectDec(icon);
        }
    }
}
