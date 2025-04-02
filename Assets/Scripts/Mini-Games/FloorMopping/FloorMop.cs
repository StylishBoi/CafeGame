using UnityEngine;

public class FloorMop : MonoBehaviour
{
    //Sweep mechanics
    bool repeat = false;
    int sweepsTotal = 20;
    int sweepsCount;

    //Sprite animation
    [SerializeField] SpriteRenderer spriteRenderer_;
    private float currentOpacity = 1f;
    Color color;

    void FixedUpdate()
    {
        if (MinigameInput.GetInstance().GetMoveLPressed() && repeat)
        {
            Debug.Log("Mopping left");
            this.transform.localPosition += new Vector3(-80f, 0f, 0f)*Time.deltaTime;
            SweepEvent();
        }
        else if (MinigameInput.GetInstance().GetMoveRPressed() && !repeat)
        {
            Debug.Log("Mopping right");
            this.transform.localPosition += new Vector3(80f, 0f, 0f)*Time.deltaTime;
            SweepEvent();
        }

        if (sweepsCount >= sweepsTotal)
        {
            MinigameManager.MiniGameEnd();
        }
    }

    void SweepEvent()
    {
        //Prepare for next sweep
        repeat=!repeat;
        sweepsCount++;
        
        //Modify the opacity
        color=spriteRenderer_.color;
        currentOpacity -= 0.05f;
        spriteRenderer_.color = new Color(color.r, color.g, color.b, currentOpacity);
    }
}
