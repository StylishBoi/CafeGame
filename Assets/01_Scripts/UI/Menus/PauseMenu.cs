using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuScript : MonoBehaviour
{
    private Canvas thisCanvas;
    private UIFadeEffects _uiFadeEffects;
    [SerializeField] private Image fullScreenFade;

    public void Start()
    {
        thisCanvas = gameObject.GetComponent<Canvas>();
        _uiFadeEffects = gameObject.GetComponent<UIFadeEffects>();
    }
    
    public void SwitchCanvas(Canvas canvas)
    {
        thisCanvas.enabled = !thisCanvas.enabled;
        canvas.enabled = !canvas.enabled;
    }

    public void UnpauseGame()
    {
        thisCanvas.enabled = !thisCanvas.enabled;
        GameManager.Instance.UnPause();
    }

    public void MainMenu()
    {
        StartCoroutine(MainMenuFade());
    }

    private IEnumerator MainMenuFade()
    {
        GameManager.Instance.SwitchState(GameState.Cinematic);
        yield return StartCoroutine(_uiFadeEffects.DoFadeOut(fullScreenFade, Color.black));
        fullScreenFade.gameObject.SetActive(true);
        GameManager.Instance.SendToMenu();
        SceneManager.LoadScene("MainMenu");
    }
}
