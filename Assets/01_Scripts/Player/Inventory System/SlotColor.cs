using System;
using UnityEngine;
using UnityEngine.UI;

public class SlotColor : MonoBehaviour
{
    [SerializeField] private Color badColor;
    [SerializeField] private Color goodColor;
    private Color neutralColor;

    private Image backdrop_;

    private void Start()
    {
        neutralColor= GetComponent<Image>().color;
        backdrop_=GetComponent<Image>();
    }

    public void SetQualityColor(string quality)
    {
        if (quality=="Good")
        {
            backdrop_.color=goodColor;
        }
        else
        {
            
            backdrop_.color=badColor;
        }
    }

    public void SetNeutral()
    {
        backdrop_.color=neutralColor;
    }

}
