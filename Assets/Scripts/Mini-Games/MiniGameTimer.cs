using UnityEngine;
using TMPro;

public class MiniGameTimer : MonoBehaviour
{
    [SerializeField] private float timer = 10f;

    [SerializeField] TextMeshProUGUI countdownText;
    
    void FixedUpdate()
    {
        if (timer > 0)
        {
            //Keeps updating the timer
            timer -=1*Time.deltaTime;
            countdownText.text = timer.ToString("0");
        }
        else if (timer <= 0)
        {
            //Stops the timer
            timer = 0;
            countdownText.text = timer.ToString("0");
            MinigameManager.MiniGameEnd();
        }
    }
}
