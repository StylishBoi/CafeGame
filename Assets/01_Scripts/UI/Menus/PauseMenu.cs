using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PauseMenu : MonoBehaviour
{
    public static PauseMenu Instance { get; private set; }
    
    [SerializeField] private Canvas pauseMenu;
    
    //private UIFadeEffects _uiFadeEffects;
    [SerializeField] private Image fullScreenFade;

    public void Awake()
    {
        //_uiFadeEffects = FindFirstObjectByType<UIFadeEffects>();
        
        if (Instance && Instance != this) Destroy(gameObject);
        else Instance = this;
        Debug.Log(Instance);
    }
    
    public void SwitchCanvas(Canvas canvas)
    {
        pauseMenu.enabled = !pauseMenu.enabled;
        canvas.enabled = !canvas.enabled;
    }

    public void PauseGame()
    {
        pauseMenu.enabled = true;
        GameManager.Instance.Pause();
    }
    
    public void UnpauseGame()
    {
        pauseMenu.enabled = false;
        GameManager.Instance.UnPause();
    }

    public void MainMenu()
    {
        pauseMenu.enabled = false;
        //StartCoroutine(MainMenuFade());
        SceneManager.LoadScene("MainMenu");
    }

    // private IEnumerator MainMenuFade()
    // {
    //     GameManager.Instance.SwitchState(GameState.Cinematic);
    //     yield return StartCoroutine(_uiFadeEffects.DoFadeOut(fullScreenFade, Color.black));
    //     fullScreenFade.gameObject.SetActive(true);
    //     GameManager.Instance.SendToMenu();
    //     SceneManager.LoadScene("MainMenu");
    // }
}
