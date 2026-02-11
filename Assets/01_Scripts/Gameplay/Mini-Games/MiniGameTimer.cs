using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class MiniGameTimer : MonoBehaviour
{
    [Header("Minigames Award")]
    [SerializeField] Item badMinigameItem;
    
    [Header("Minigame")]
    [SerializeField] GameObject minigameHeader;
    
    [Header("Timer Info")]
    [SerializeField] private float maxTime = 10f;
    [SerializeField] private Image timerBar;
    [SerializeField] TextMeshProUGUI countdownText;
    private float timeLeft;


    void OnEnable()
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
            AudioManager.Instance.PlaySfx(AudioManager.Instance.itemReceiveSFX);
            MinigameManager.Instance.MiniGameEnd();
            minigameHeader.SetActive(false);
        }
    }
}
