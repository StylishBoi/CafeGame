using UnityEngine;

public class DishwasherEffect : MonoBehaviour
{
    [SerializeField] private GameObject icon;
    [SerializeField] private int maxTillDirty = 5;

    private float _timerTillDirty = 0;
    private bool _isActive;

    private EffectManager _effectManager;

    void Start()
    {
        _effectManager = FindFirstObjectByType<EffectManager>();
        _isActive = false;
    }

    void FixedUpdate()
    {
        if (MaintenanceManager.dishwasherState == MaintenanceManager.WashingMachineState.NeedsCleaning)
        {
            _timerTillDirty += Time.deltaTime;
        }
        else
        {
            _timerTillDirty = 0;
        }

        if (_timerTillDirty > maxTillDirty && !_isActive)
        {
            _isActive = true;
            _effectManager.NegativeEffectInc(icon);
        }
        else if ((MaintenanceManager.dishwasherState == MaintenanceManager.WashingMachineState.Empty ||
                  MaintenanceManager.dishwasherState == MaintenanceManager.WashingMachineState.HasCleaning) &&
                 _isActive)
        {
            _timerTillDirty = 0;
            _isActive = false;
            _effectManager.NegativeEffectDec(icon);
        }
    }
}