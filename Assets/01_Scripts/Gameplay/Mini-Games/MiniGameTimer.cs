using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class MiniGameTimer : MonoBehaviour
{
    [Header("Minigames Award")]
    [SerializeField] Item badMinigameItem;
    
    [SerializeField] private float maxTime = 10f;
    [SerializeField] private Image timerBar;
    private float timeLeft;

    [SerializeField] TextMeshProUGUI countdownText;

    void Start()
    {
        timeLeft = maxTime;
    }
    void FixedUpdate()
    {
        if (timeLeft > 0)
        {
            //Keeps updating the timer
            timeLeft -=1*Time.deltaTime;
            countdownText.text = timeLeft.ToString("0");
            timerBar.fillAmount = timeLeft / maxTime;
        }
        else if (timeLeft <= 0)
        {
            //Stops the timer
            timeLeft = 0;
            countdownText.text = timeLeft.ToString("0");
            InventoryManager.Instance.AddItem(badMinigameItem);
            MinigameManager.MiniGameEnd();
        }
    }
}
