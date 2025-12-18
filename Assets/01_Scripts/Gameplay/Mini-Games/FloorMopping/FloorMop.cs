using System;
using UnityEngine;

public class FloorMop : MonoBehaviour
{
    //Sweep mechanics
    bool repeat = false;
    int sweepsTotal = 10;
    int sweepsCount;

    //Sprite animation
    [SerializeField] SpriteRenderer spriteRenderer_;
    [SerializeField] private GameObject leftArrow;
    [SerializeField] private GameObject rightArrow;
    private float currentOpacity = 1f;
    Color color;

    private void Start()
    {
        leftArrow.SetActive(false);
    }

    void FixedUpdate()
    {
        if (MinigameInput.Instance.GetMoveLPressed() && repeat)
        {
            this.transform.localPosition += new Vector3(-80f, 0f, 0f)*Time.deltaTime;
            SweepEvent();
        }
        else if (MinigameInput.Instance.GetMoveRPressed() && !repeat)
        {
            this.transform.localPosition += new Vector3(80f, 0f, 0f)*Time.deltaTime;
            SweepEvent();
        }

        if (sweepsCount >= sweepsTotal)
        {
            MinigameManager.Instance.MiniGameEnd();
        }
    }

    void SweepEvent()
    {
        //Prepare for next sweep
        repeat=!repeat;
        sweepsCount++;
        leftArrow.SetActive(repeat);
        rightArrow.SetActive(!repeat);
        
        //Modify the opacity
        color=spriteRenderer_.color;
        currentOpacity -= (1f/sweepsTotal);
        spriteRenderer_.color = new Color(color.r, color.g, color.b, currentOpacity);
    }
}
