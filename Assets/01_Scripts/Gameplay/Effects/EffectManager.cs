using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EffectManager : MonoBehaviour
{
    [Header("Total Effect")] public static bool PositiveEffect;
    public static bool NegativeEffect;
    private static int _negativeCount;
    private static int _positiveCount;

    [Header("Status Colors")] [SerializeField]
    private Color goodStatus;

    [SerializeField] private Color neutralStatus;
    [SerializeField] private Color badStatus;
    [SerializeField] private Image backgroundImage;

    [Header("Dirty Effect")] [SerializeField]
    private GameObject dirtyIcon;

    [Header("Streak Effect")] [SerializeField]
    private GameObject streakIcon;

    [Header("Negative Streak Effect")] [SerializeField]
    private GameObject badStreakIcon;

    [Header("Trashcan Effect")] [SerializeField]
    private GameObject trashcanIcon;

    [Header("Effect Display")] [SerializeField]
    private RectTransform effectPanel;

    private List<GameObject> _effectSlots = new List<GameObject>();
    private static int _totalEffect;

    private void StatusCheck()
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

        StatusCheck();
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
        if (PositiveEffect)
        {
            backgroundImage.color = goodStatus;
        }
        else if (NegativeEffect)
        {
            backgroundImage.color = badStatus;
        }
        else
        {
            backgroundImage.color = neutralStatus;
        }

        foreach (Transform child in effectPanel.transform)
        {
            Destroy(child.gameObject);
        }

        //Creates the list of effect slots
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
            newSlot.transform.position = new Vector3(effectPanel.position.x + 230, effectPanel.position.y - 180) +
                                         new Vector3(-columnIncrease * 100, rowIncrease * 100, 0);
            newSlot.name = ("EffectSlot " + (i));
        }
    }
}