using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private Image transitionImage;
    private Color _currentColor;
    private bool _transitioning;

    void Start()
    {
        transitionImage.color=new Color(0,0,0,0);
    }
    public void StartGame()
    {
        _transitioning = true;
    }

    void Update()
    {
        if (_transitioning)
        {
            _currentColor=transitionImage.color;
            if (_currentColor.a <= 1)
            {
                _currentColor.a += Time.deltaTime/2;
                
                if (_currentColor.a >= 1)
                {
                    _transitioning = false;
                    SceneManager.LoadScene("CafePrototype");
                }

                transitionImage.color = _currentColor;
            }
        }
    }
}
