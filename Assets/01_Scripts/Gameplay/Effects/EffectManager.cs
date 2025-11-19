using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class EffectManager : MonoBehaviour
{
    [Header("Total Effect")] public static bool PositiveEffect;
    public static bool NegativeEffect;
    private static int _negativeCount;
    private static int _positiveCount;

    [Header("Dirty Effect")]
    //[SerializeField] private Image dirtyIcon;
    [SerializeField]
    private GameObject dirtyIcon;

    [SerializeField] private int maxTillDirty = 5;
    private float _timerTillDirty = 0;
    private bool _isDirty;

    [Header("Streak Effect")] [SerializeField]
    private GameObject streakIcon;

    private bool _isInStreak;

    [Header("Negative Streak Effect")] [SerializeField]
    private GameObject badStreakIcon;

    private bool _isInBadStreak;

    [Header("Trashcan Effect")] [SerializeField]
    private GameObject trashcanIcon;

    private bool _isTrashed;

    [Header("Effect Display")] [SerializeField]
    private RectTransform effectPanel;

    private List<GameObject> _effectSlots = new List<GameObject>();
    private static int _totalEffect;

    private void FixedUpdate()
    {
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

    private void ModifyEffectList(GameObject effect)
    {
        if (_effectSlots.Contains(effect))
        {
            _effectSlots.Remove(effect);
        }
        else
        {
            _effectSlots.Add(effect);
        }

        UpdateEffectDisplay();
    }

    public void PositiveEffectInc(GameObject effectImage)
    {
        _positiveCount++;
        _totalEffect++;
        ModifyEffectList(effectImage);
    }

    public void PositiveEffectDec(GameObject effectImage)
    {
        _positiveCount--;
        _totalEffect--;
        ModifyEffectList(effectImage);
    }

    public void NegativeEffectInc(GameObject effectImage)
    {
        _negativeCount++;
        _totalEffect++;
        ModifyEffectList(effectImage);
    }

    public void NegativeEffectDec(GameObject effectImage)
    {
        _negativeCount--;
        _totalEffect--;
        ModifyEffectList(effectImage);
    }

    private void UpdateEffectDisplay()
    {
        foreach (Transform child in effectPanel.transform)
        {
            Destroy(child.gameObject);
        }

        //Creates the list of health slots
        GameObject newSlot;
        int rowIncrease = 0;
        int columnIncrease = 1;

        for (int i = 0; i < _totalEffect; i++)
        {
            if (i % 4 == 0)
            {
                rowIncrease++;
                columnIncrease = 1;
            }
            else
            {
                columnIncrease++;
            }

            newSlot = Instantiate(_effectSlots[i], effectPanel);
            newSlot.transform.position = new Vector3(effectPanel.position.x + 250, effectPanel.position.y - 200) +
                                         new Vector3(-columnIncrease * 100, rowIncrease * 100, 0);
            newSlot.name = ("EffectSlot " + (i));
        }
    }
}