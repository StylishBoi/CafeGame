using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private Image transitionImage;
    [SerializeField] private Button loadButton;
    private Color _currentColor;

    void Start()
    {
        transitionImage.color=new Color(0,0,0,0);

        if (!SaveSystem.HasData())
        {
            loadButton.interactable = false;
        }

        if (AudioManager.Instance.musicSource.clip == null)
        {
            AudioManager.Instance.PlayBGM(AudioManager.Instance.mainmenuMusic);
        }
    }
    public void StartGame()
    {
        SceneManager.LoadScene("PredayScene");
    }
    public void LoadGame()
    {
        if(SaveSystem.HasData())
        {
            ScoreSystem.LoadData();
            SceneManager.LoadScene("PredayScene");
        }
    }
    
    public void QuitGame()
    {
        Application.Quit();
    }

    // void Update()
    // {
    //     if (GameManager.Instance.State==GameState.Cinematic)
    //     {
    //         _currentColor=transitionImage.color;
    //         if (_currentColor.a <= 1)
    //         {
    //             _currentColor.a += Time.deltaTime/2;
    //             
    //             if (_currentColor.a >= 1)
    //             {
    //                 SceneManager.LoadScene("CafePrototype");
    //             }
    //
    //             transitionImage.color = _currentColor;
    //         }
    //     }
    // }
}
