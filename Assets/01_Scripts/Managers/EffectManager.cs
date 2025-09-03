using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;
public class EffectManager : MonoBehaviour
{
    [Header("Total Effect")] 
    public static bool PositiveEffect;
    public static bool NegativeEffect;
    private int _negativeCount;
    private int _positiveCount;
    
    [Header("Dirty Effect")]
    //[SerializeField] private Image dirtyIcon;
    [SerializeField] private GameObject dirtyIcon;
    [SerializeField] private int maxTillDirty = 5;
    private float _timerTillDirty=0;
    private bool _isDirty;
    
    [Header("Streak Effect")]
    [SerializeField] private GameObject streakIcon;
    private bool _isInStreak;
    
    [Header("Negative Streak Effect")]
    [SerializeField] private GameObject badStreakIcon;
    private bool _isInBadStreak;
    
    [Header("Trashcan Effect")]
    [SerializeField] private GameObject trashcanIcon;
    private bool _isTrashed;
    
    [Header("Effect Display")]
    [SerializeField] private RectTransform effectPanel;
    private List<GameObject> _effectSlots = new List<GameObject>();
    private int _totalEffect;

    private void FixedUpdate()
    {
        //Dirty effect
        if (_timerTillDirty > maxTillDirty && !_isDirty)
        {
            _isDirty = true;
            _totalEffect++;
            _negativeCount++;
            DirtyEffect();
        }
        else if(MaintenanceManager.CurrentMaintenanceEvents.Count!=0 && !_isDirty)
        {
            _timerTillDirty += Time.deltaTime;
        }
        else if (MaintenanceManager.CurrentMaintenanceEvents.Count == 0 && _isDirty)
        {
            _isDirty = false;
            _timerTillDirty = 0;
            _negativeCount--;
            _totalEffect--;
            DirtyEffect();
        }

        //Streak effect
        if (_effectSlots.Contains(streakIcon) && !_isInStreak)
        {
            StreakEffect();
        }
        else if (StreakManager.Streak>=2 && !_isInStreak)
        {
            _isInStreak = true;
            _totalEffect++;
            _positiveCount++;
            StreakEffect();
        }
        else if (StreakManager.Streak == 0 && _isInStreak)
        {
            _positiveCount--;
            _totalEffect--;
            _isInStreak = false;
        }
        
        //Negative streak effect
        if (_effectSlots.Contains(badStreakIcon) && !_isInBadStreak)
        {
            BadStreakEffect();
        }
        else if (StreakManager.NegativeStreak>=2 && !_isInBadStreak)
        {
            _isInBadStreak = true;
            _totalEffect++;
            _negativeCount++;
            BadStreakEffect();
        }
        else if (StreakManager.NegativeStreak == 0 && _isInBadStreak)
        {
            _negativeCount--;
            _totalEffect--;
            _isInBadStreak = false;
        }
        
        //Trashcan effect
        if (_effectSlots.Contains(trashcanIcon) && !_isTrashed)
        {
            TrashcanEffect();
        }
        else if (Trashcan.FillageRate==3 && !_isTrashed)
        {
            _isTrashed = true;
            _totalEffect++;
            _negativeCount++;
            TrashcanEffect();
        }
        else if (Trashcan.FillageRate==0 && _isTrashed)
        {
            _negativeCount--;
            _totalEffect--;
            _isTrashed = false;
        }
        

        //Status effects
        if (_negativeCount > _positiveCount)
        {
            NegativeEffect = true;
        }
        else if (_positiveCount > _negativeCount)
        {
            PositiveEffect = true;
        }
        else
        {
            NegativeEffect = false;
            PositiveEffect = false;
        }
        
    }

    private void DirtyEffect()
    {
        if (_effectSlots.Contains(dirtyIcon))
        {
            _effectSlots.Remove(dirtyIcon);
        }
        else
        {
            _effectSlots.Add(dirtyIcon);
        }
        UpdateEffectDisplay();
    }
    
    private void StreakEffect()
    {
        if (_effectSlots.Contains(streakIcon))
        {
            _effectSlots.Remove(streakIcon);
        }
        else
        {
            _effectSlots.Add(streakIcon);
        }
        UpdateEffectDisplay();
    }
    
    private void BadStreakEffect()
    {
        if (_effectSlots.Contains(badStreakIcon))
        {
            _effectSlots.Remove(badStreakIcon);
        }
        else
        {
            _effectSlots.Add(badStreakIcon);
        }
        UpdateEffectDisplay();
    }
    
    private void TrashcanEffect()
    {
        if (_effectSlots.Contains(trashcanIcon))
        {
            _effectSlots.Remove(trashcanIcon);
        }
        else
        {
            _effectSlots.Add(trashcanIcon);
        }
        UpdateEffectDisplay();
    }

    private void UpdateEffectDisplay()
    {
        foreach (Transform child in effectPanel.transform) {
            Destroy(child.gameObject);
        }
        
        //Creates the list of health slots
        GameObject newSlot;
        int rowIncrease=0;
        int columnIncrease=1;
        
        for (int i = 0; i < _totalEffect; i++)
        {
            if (i % 4== 0)
            {
                rowIncrease++;
                columnIncrease = 1;
            }
            else
            {
                columnIncrease++;
            }
            
            newSlot = Instantiate(_effectSlots[i], effectPanel);
            newSlot.transform.position = new Vector3(effectPanel.position.x+250, effectPanel.position.y-200) +
                                         new Vector3(-columnIncrease*100, rowIncrease*100, 0);
            newSlot.name=("EffectSlot " + (i));
        }
    }
}
