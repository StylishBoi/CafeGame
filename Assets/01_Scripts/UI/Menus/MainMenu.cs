using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private Image transitionImage;
    private Color _currentColor;

    void Start()
    {
        transitionImage.color=new Color(0,0,0,0);
    }
    public void StartGame()
    {
        GameManager.Instance.StartGame();
        ScoreSystem.StartDay();
    }

    void Update()
    {
        if (GameManager.Instance.State==GameState.Cinematic)
        {
            _currentColor=transitionImage.color;
            if (_currentColor.a <= 1)
            {
                _currentColor.a += Time.deltaTime/2;
                
                if (_currentColor.a >= 1)
                {
                    SceneManager.LoadScene("CafePrototype");
                }

                transitionImage.color = _currentColor;
            }
        }
    }
}
